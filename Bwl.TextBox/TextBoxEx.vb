Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Timers
Imports System.Windows.Forms

Public Class TextBoxEx
    Public Event BeforeDrawLine(sender As TextBoxEx, index As Integer, line As TextLine)
    Public Property TabSize As Integer = 4
    Public Property NewLineSpacesAsPreviousLine As Boolean = True

    'current state
    Private _lines As New List(Of TextLine)({New TextLine})
    Private _currentPosition As TextPosition
    Private _scrollPosition As TextPosition
    Private _selectedStart As TextPosition
    Private _selectedEnd As TextPosition

    'internal
    Private _cursorPen As Pen
    Private _backgoundPen As Pen
    Private _textPen As Pen
    Private _backgoundBrush As Brush
    Private _selectedBrush As Brush
    Private _graphics As Graphics
    Private _focused As Boolean
    Private _clickedStart As TextPosition
    Private _colorScheme As New ColorScheme
    Private _linesPerScreen As Integer
    Private _columnsPerScreen As Integer
    Private _graphicsBmp As Bitmap
    Private _controlGraphics As Graphics
    Private _fontParams As New FontParams
    Private _previousCopiedWholeLine As String = ""
    Private _undoBuffer As New Stack(Of StateSnapshot)
    Private _redoBuffer As New Stack(Of StateSnapshot)
    Private _lastEditedLine As Integer = -1

    Private Class FontParams
        Implements IDisposable

        Public ReadOnly Property Font As Font = New Font("Consolas", 10)
        Public ReadOnly Property CharWidth As Integer = 7
        Public ReadOnly Property CharSpaceWidth As Integer = 8
        Public ReadOnly Property CharHeight As Integer = 16
        Public ReadOnly Property LineHeight As Integer = 17

        Public Sub Dispose() Implements IDisposable.Dispose
            Try
                _Font.Dispose()
            Catch ex As Exception
            End Try
        End Sub
    End Class

    Private Function TextPositionToScreen(position As TextPosition) As Rectangle
        Dim rect As Rectangle
        rect.Width = _fontParams.CharSpaceWidth
        rect.Height = _fontParams.LineHeight
        rect.X = (position.ColumnIndex - _scrollPosition.ColumnIndex) * (_fontParams.CharSpaceWidth)
        rect.Y = (position.LineIndex - _scrollPosition.LineIndex) * (_fontParams.LineHeight)
        Return rect
    End Function

    Public Sub New()
        PrepareGraphics()
        ApplyColorScheme()
        InitializeComponent()
    End Sub

    Public Sub ApplyColorScheme()
        _cursorPen = New Pen(_colorScheme.TextColor)
        _backgoundPen = New Pen(_colorScheme.BackColor)
        _textPen = New Pen(_colorScheme.TextColor)
        _backgoundBrush = New SolidBrush(_colorScheme.BackColor)
        _selectedBrush = New SolidBrush(_colorScheme.SelectedBackColor)
        _graphics.Clear(_colorScheme.BackColor)
    End Sub

    Private Sub PrepareGraphics()
        _graphicsBmp = New Bitmap(Me.Width, Me.Height)
        _graphics = Graphics.FromImage(_graphicsBmp)
        _controlGraphics = CreateGraphics()
        _linesPerScreen = Math.Floor(Me.Height / _fontParams.LineHeight)
        _columnsPerScreen = Math.Floor(Me.Width / _fontParams.CharSpaceWidth)
        BackgroundImage = _graphicsBmp
        BackgroundImageLayout = ImageLayout.None
    End Sub

    Private Sub ShowGraphics()
        _controlGraphics.DrawImage(_graphicsBmp, 0, 0)
    End Sub

    Public ReadOnly Property CurrentLine As TextLine
        Get
            Return _lines(_currentPosition.LineIndex)
        End Get
    End Property

    Public Overrides Property Text As String
        Get
            Throw New Exception("Not supported")
        End Get
        Set(value As String)
            Throw New Exception("Not supported")
        End Set
    End Property
    Private Sub ResizeTimer_Tick(sender As Object, e As EventArgs) Handles ResizeTimer.Tick
        If Width > 0 And Height > 0 Then
            PrepareGraphics()
            RedrawAll()
        End If
        ResizeTimer.Stop()
    End Sub

    Private Sub TextBoxEx_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        ResizeTimer.Stop()
        ResizeTimer.Start()
    End Sub

    Private Shared Function CountStartringSpaces(str As String) As Integer
        Dim i = 0
        While i < str.Length AndAlso str(i) = " "c
            i += 1
        End While
        Return i
    End Function

    Public Sub CopySelectedToClipboard()
        Dim sb As New StringBuilder
        _previousCopiedWholeLine = ""

        If _selectedStart.LineIndex = _selectedEnd.LineIndex Then
            If _selectedStart.ColumnIndex < _selectedEnd.ColumnIndex Then
                sb.Append(_lines(_selectedStart.LineIndex).Text.Substring(_selectedStart.ColumnIndex, _selectedEnd.ColumnIndex - _selectedStart.ColumnIndex + 1))
            Else
                'not selected,copy line
                _previousCopiedWholeLine = _lines(_currentPosition.LineIndex).Text
                sb.Append(_previousCopiedWholeLine)
            End If
        Else
            For i = _selectedStart.LineIndex To _selectedEnd.LineIndex
                If i = _selectedStart.LineIndex Then
                    sb.AppendLine(_lines(i).Text.Substring(_selectedStart.ColumnIndex))
                ElseIf i = _selectedEnd.LineIndex Then
                    If _selectedEnd.ColumnIndex > 0 Then
                        sb.AppendLine(_lines(i).Text.Substring(0, _selectedEnd.ColumnIndex + 1))
                    Else
                        sb.AppendLine("")
                    End If
                Else
                    sb.AppendLine(_lines(i).Text)
                End If
            Next
        End If
        'Dim a = "a,.b,cc.c,.d"
        'Dim parts = a.Split({",.", ",", "."}, StringSplitOptions.None)
        If sb.Length > 0 Then Clipboard.SetText(sb.ToString)
    End Sub

    Public Sub DeleteSelected()
        SaveCurrentState()
        If _selectedStart.LineIndex = _selectedEnd.LineIndex Then
            If _selectedStart.ColumnIndex < _selectedEnd.ColumnIndex Then
                _lines(_selectedStart.LineIndex).Text = _lines(_selectedStart.LineIndex).Text.Remove(_selectedStart.ColumnIndex, _selectedEnd.ColumnIndex - _selectedStart.ColumnIndex + 1)
            Else
                'not selected,delete line
                _lines.RemoveAt(_currentPosition.LineIndex)
            End If
        Else
            Dim result = ""
            If _selectedStart.ColumnIndex > 0 Then result += _lines(_selectedStart.LineIndex).Text.Remove(_selectedStart.ColumnIndex)
            If _selectedEnd.ColumnIndex > 0 Then result += _lines(_selectedEnd.LineIndex).Text.Remove(0, _selectedEnd.ColumnIndex + 1)
            _lines(_selectedStart.LineIndex).Text = result
            For i = _selectedStart.LineIndex To _selectedEnd.LineIndex - 1
                _lines.RemoveAt(_selectedStart.LineIndex + 1)
            Next
        End If
        _currentPosition = _selectedStart
        _selectedEnd = _selectedStart
        RedrawAll()
        RaiseEvent TextChanged(Me)
    End Sub

    Private Sub SaveCurrentState()
        _undoBuffer.Push(StateSnapshot.CreateFrom(Me))
        _redoBuffer.Clear()
    End Sub

    Private Sub PasteClipboard()
        SaveCurrentState()
        Dim textToPaste = Clipboard.GetText
        If textToPaste = _previousCopiedWholeLine Then
            _lines.Insert(_currentPosition.LineIndex, New TextLine(textToPaste))
        Else
            Dim lineText = CurrentLine.Text.Insert(_currentPosition.ColumnIndex, textToPaste)
            Dim parts = lineText.Split({vbCrLf, vbLf, vbCr}, StringSplitOptions.None)
            CurrentLine.Text = parts(0)
            For i = 1 To parts.Length - 1
                _lines.Insert(_currentPosition.LineIndex + i, New TextLine(parts(i)))
            Next
        End If
        _selectedEnd = _selectedStart
        RedrawAll()
        RaiseEvent TextChanged(Me)
    End Sub

    Public Shadows Event TextChanged(sender As Object)
    Public Event LineEdited(sender As Object, lineIndex As Integer, line As TextLine, ByRef needRedraw As Boolean)

    Private Sub CheckLineWasEdited()
        If _lastEditedLine > -1 Then
            Dim needRedraw As Boolean
            RaiseEvent LineEdited(Me, _lastEditedLine, _lines(_lastEditedLine), needRedraw)
            If needRedraw Then
                ClearLine(_lastEditedLine)
                DrawLine(_lastEditedLine)
            End If
            _lastEditedLine = -1
        End If
    End Sub

    Private Sub TextBoxEx_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If _focused Then
            If Not My.Computer.Keyboard.CtrlKeyDown Then
                Select Case e.KeyChar
                    Case vbCr, vbLf
                        Dim oldLine = _lines(_currentPosition.LineIndex).Text.Substring(0, _currentPosition.ColumnIndex)
                        Dim newLine = _lines(_currentPosition.LineIndex).Text.Substring(_currentPosition.ColumnIndex)
                        _currentPosition.ColumnIndex = 0
                        If NewLineSpacesAsPreviousLine Then
                            newLine = Space(CountStartringSpaces(oldLine)) + newLine
                            _currentPosition.ColumnIndex = CountStartringSpaces(oldLine)
                        End If
                        _lines.Insert(_currentPosition.LineIndex + 1, New TextLine)
                        _lines(_currentPosition.LineIndex).Text = oldLine
                        _lines(_currentPosition.LineIndex + 1).Text = newLine
                        _currentPosition.LineIndex += 1
                        RedrawAll()
                        RaiseEvent TextChanged(Me)

                        _InputTimer.Stop() : _inputTimer.Start()
                    Case vbTab
                        If My.Computer.Keyboard.ShiftKeyDown = False Then
                            If _selectedEnd = _selectedStart Then
                                Dim tabsCount = Math.Floor((_currentPosition.ColumnIndex + 1) / TabSize)
                                tabsCount += 1
                                Dim spacesNeeded = tabsCount * TabSize - _currentPosition.ColumnIndex
                                CurrentLine.Text = CurrentLine.Text.Insert(_currentPosition.ColumnIndex, Space(spacesNeeded))
                                _currentPosition.ColumnIndex += spacesNeeded
                                ClearDrawCurrentLine()
                            Else
                                For i = _selectedStart.LineIndex To _selectedEnd.LineIndex
                                    _lines(i).Text = Space(TabSize) + _lines(i).Text
                                    ClearLine(i)
                                    DrawLine(i)
                                Next
                            End If
                            DrawCursor(True, False)
                            RaiseEvent TextChanged(Me)
                            _inputTimer.Stop() : _inputTimer.Start()
                        Else
                            If _selectedEnd = _selectedStart Then
                                Dim spaces = CountStartringSpaces(CurrentLine.Text)
                                Dim spacesToRemove = Math.Min(TabSize, spaces)
                                CurrentLine.Text = CurrentLine.Text.Remove(0, spacesToRemove)
                                _currentPosition.ColumnIndex = Math.Max(0, _currentPosition.ColumnIndex - spacesToRemove)
                                ClearDrawCurrentLine()
                            Else
                                For i = _selectedStart.LineIndex To _selectedEnd.LineIndex
                                    Dim spaces = CountStartringSpaces(_lines(i).Text)
                                    Dim spacesToRemove = Math.Min(TabSize, spaces)
                                    _lines(i).Text = _lines(i).Text.Remove(0, spacesToRemove)
                                    ClearLine(i)
                                    DrawLine(i)
                                Next
                            End If
                            DrawCursor(True, False)
                            RaiseEvent TextChanged(Me)
                            _inputTimer.Stop() : _inputTimer.Start()
                        End If
                        _lastEditedLine = _currentPosition.LineIndex
                    Case vbBack
                        If _selectedEnd <> _selectedStart Then
                            DeleteSelected()
                        Else
                            If _currentPosition.ColumnIndex > 0 Then
                                Dim spaces = CountStartringSpaces(CurrentLine.Text)
                                If spaces >= _currentPosition.ColumnIndex - 1 And ((_currentPosition.ColumnIndex) Mod TabSize = 0) Then
                                    Dim spacesToRemove = Math.Min(TabSize, spaces)
                                    CurrentLine.Text = CurrentLine.Text.Remove(0, spacesToRemove)
                                    _currentPosition.ColumnIndex = Math.Max(0, _currentPosition.ColumnIndex - spacesToRemove)
                                Else
                                    CurrentLine.Text = CurrentLine.Text.Remove(_currentPosition.ColumnIndex - 1, 1)
                                    _currentPosition.ColumnIndex -= 1
                                End If
                                ClearDrawCurrentLine()
                            Else
                                If _currentPosition.LineIndex > 0 Then
                                    _currentPosition.ColumnIndex = _lines(_currentPosition.LineIndex - 1).Text.Length
                                    _lines(_currentPosition.LineIndex - 1).Text += CurrentLine.Text
                                    _lines.RemoveAt(_currentPosition.LineIndex)
                                    _currentPosition.LineIndex -= 1
                                    RedrawAll()
                                End If
                            End If
                        End If
                        RaiseEvent TextChanged(Me)
                        _InputTimer.Stop() : _InputTimer.Start()
                        _lastEditedLine = _currentPosition.LineIndex
                    Case Else
                        If _selectedStart <> _selectedEnd Then DeleteSelected()
                        CurrentLine.Text = CurrentLine.Text.Insert(_currentPosition.ColumnIndex, e.KeyChar.ToString)
                        _currentPosition.ColumnIndex += 1
                        ClearDrawCurrentLine()
                        DrawCursor(True, False)
                        RaiseEvent TextChanged(Me)
                        _inputTimer.Stop() : _inputTimer.Start()
                        _lastEditedLine = _currentPosition.LineIndex
                End Select
            End If
        End If
    End Sub

    Private Class StateSnapshot
        Private _lines As New List(Of TextLine)
        Private _selectedStart As TextPosition
        Private _selectedEnd As TextPosition
        Private _currentPosition As TextPosition
        Private _scrollPosition As TextPosition

        Public Shared Function CreateFrom(tb As TextBoxEx) As StateSnapshot
            Dim ss As New StateSnapshot
            ss._currentPosition = tb._currentPosition
            ss._selectedStart = tb._selectedStart
            ss._selectedEnd = tb._selectedEnd
            ss._scrollPosition = tb._scrollPosition
            For Each line In tb._lines
                ss._lines.Add(New TextLine(line.Text))
            Next
            Return ss
        End Function

        Public Sub ApplyTo(tb As TextBoxEx)
            tb._currentPosition = _currentPosition
            tb._selectedStart = _selectedStart
            tb._selectedEnd = _selectedEnd
            tb._scrollPosition = _scrollPosition
            tb._lines.Clear()
            For Each line In _lines
                tb._lines.Add(New TextLine(line.Text))
            Next
            tb.RedrawAll()
        End Sub

    End Class

    Private Sub ClearDrawCurrentLine()
        ClearLine(_currentPosition.LineIndex)
        DrawLine(_currentPosition.LineIndex)
        ShowGraphics()
    End Sub

    Private Sub ClearLine(index As Integer)
        Dim charRect = TextPositionToScreen(New TextPosition(index, 0))
        _graphics.FillRectangle(_backgoundBrush, 0, charRect.Top, Me.Width, charRect.Height)
    End Sub

    Public ReadOnly Property TextBoxBitmap As Bitmap
        Get
            Return _graphicsBmp
        End Get
    End Property

    Private Sub DrawLine(linexIndex As Integer)
        Dim line = _lines(linexIndex)
        RaiseEvent BeforeDrawLine(Me, linexIndex, line)
        Dim brush = Brushes.Black
        Dim brushColor = Color.Black
        For j = _scrollPosition.ColumnIndex To Math.Min(_scrollPosition.ColumnIndex + _columnsPerScreen, line.Text.Length - 1)
            Dim i = j '- _scrollPosition.ColumnIndex
            Dim requestedColor = Color.Black
            Dim charRect = TextPositionToScreen(New TextPosition(linexIndex, i))
            If line.Attributes IsNot Nothing AndAlso i < line.Attributes.Length AndAlso line.Attributes(i) IsNot Nothing Then
                requestedColor = line.Attributes(i).ForeColor
            End If
            If brushColor <> requestedColor Then
                brushColor = requestedColor
                brush = New SolidBrush(brushColor)
            End If
            If _selectedStart <> _selectedEnd Then
                'something selected
                Dim current = New TextPosition(linexIndex, i)
                If (current >= _selectedStart AndAlso current <= _selectedEnd) Then
                    _graphics.FillRectangle(_selectedBrush, charRect)
                End If
            End If
            _graphics.DrawString(line.Text(j), _fontParams.Font, brush, charRect.Left - 1, charRect.Top)
        Next

        For j = _scrollPosition.ColumnIndex To Math.Min(_scrollPosition.ColumnIndex + _columnsPerScreen, line.OverlayText.Length - 1)
            Dim i = j '- _scrollPosition.ColumnIndex
            Dim requestedColor = _colorScheme.OverlayColor
            Dim charRect = TextPositionToScreen(New TextPosition(linexIndex, i))
            If line.Attributes IsNot Nothing AndAlso i < line.Attributes.Length AndAlso line.Attributes(i) IsNot Nothing Then
                requestedColor = line.Attributes(i).ForeColor
            End If
            If brushColor <> requestedColor Then
                brushColor = requestedColor
                brush = New SolidBrush(brushColor)
            End If
            _graphics.DrawString(line.OverlayText(j), _fontParams.Font, brush, charRect.Left - 1, charRect.Top)
        Next
    End Sub

    Private Sub TextBoxEx_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        _focused = True
        DrawCursor(True, False)
    End Sub

    Private Sub TextBoxEx_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        _focused = False
        DrawCursor(False, True)
    End Sub

    Private Sub DrawCursor(forceDraw As Boolean, forceClean As Boolean)
        Static lastState As Boolean
        Static lastPosition As TextPosition
        Dim offset = 1
        If lastState Then
            Dim rect = TextPositionToScreen(lastPosition)
            lastState = False
            _graphics.DrawLine(_backgoundPen, rect.Left + offset, rect.Top, rect.Left + offset, rect.Bottom)
        Else
            lastState = True
            lastPosition = _currentPosition
            Dim rect = TextPositionToScreen(lastPosition)
            _graphics.DrawLine(_cursorPen, rect.Left + offset, rect.Top, rect.Left + offset, rect.Bottom)
        End If
        If forceDraw And lastState = False Then
            lastState = True
            lastPosition = _currentPosition
            Dim rect = TextPositionToScreen(lastPosition)
            _graphics.DrawLine(_cursorPen, rect.Left + offset, rect.Top, rect.Left + offset, rect.Bottom)
        End If
        If forceClean And lastState = True Then
            lastState = False
            ' lastPosition = _currentPosition
            Dim rect = TextPositionToScreen(lastPosition)
            _graphics.DrawLine(_backgoundPen, rect.Left + offset, rect.Top, rect.Left + offset, rect.Bottom)
        End If
        ShowGraphics()

    End Sub

    Private Sub CursorTimer_Tick(sender As Object, e As EventArgs) Handles CursorTimer.Tick
        If Not DesignMode And _focused Then DrawCursor(False, False)
    End Sub

    Private Sub TextBoxEx_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown
        If e.Control = False And e.Alt = False Then
            Select Case e.KeyCode
                Case Keys.Up : ChangePosition(-1, 0) : e.IsInputKey = True
                Case Keys.Down : ChangePosition(+1, 0) : e.IsInputKey = True
                Case Keys.Left : ChangePosition(0, -1) : e.IsInputKey = True
                Case Keys.Right : ChangePosition(0, +1) : e.IsInputKey = True
                Case Keys.Home : ChangePosition(0, -1000) : e.IsInputKey = True
                Case Keys.End : ChangePosition(0, +1000) : e.IsInputKey = True
                Case Keys.PageDown : ChangePosition(_linesPerScreen, 0) : e.IsInputKey = True
                Case Keys.PageUp : ChangePosition(-_linesPerScreen, 0) : e.IsInputKey = True
                Case Keys.Tab : e.IsInputKey = True
                Case Keys.Delete
                    If _selectedEnd <> _selectedStart Then
                        DeleteSelected()
                    Else
                        If _currentPosition.ColumnIndex < _lines(_currentPosition.LineIndex).Text.Length Then
                            CurrentLine.Text = CurrentLine.Text.Remove(_currentPosition.ColumnIndex, 1)
                            ClearDrawCurrentLine()
                        Else
                            If _currentPosition.LineIndex < _lines.Count - 1 Then
                                CurrentLine.Text += _lines(_currentPosition.LineIndex + 1).Text
                                _lines.RemoveAt(_currentPosition.LineIndex + 1)
                                RedrawAll()
                            End If
                        End If
                        _InputTimer.Stop() : _InputTimer.Start()
                    End If
                    RaiseEvent TextChanged(Me)
            End Select
        End If


        If e.Control = True And e.Alt = False Then
            Select Case e.KeyCode
                Case Keys.C
                    CopySelectedToClipboard()
                Case Keys.V
                    PasteClipboard()
                Case Keys.X
                    CopySelectedToClipboard()
                    DeleteSelected()
                Case Keys.Z
                    Undo()
                Case Keys.Y
                    Redo()
            End Select
        End If

    End Sub

    Public Sub Undo()
        If _undoBuffer.Count > 0 Then
            Dim state = _undoBuffer.Pop
            state.ApplyTo(Me)
            _redoBuffer.Push(state)
        End If
    End Sub

    Public Sub Redo()
        If _redoBuffer.Count > 0 Then
            Dim state = _redoBuffer.Pop
            state.ApplyTo(Me)
            _undoBuffer.Push(state)
        End If
    End Sub

    Public Sub ChangePosition(lineChange As Integer, columnChange As Integer)
        SetPosition(_currentPosition.Add(lineChange, columnChange))
    End Sub

    Public Sub SetPosition(newPosition As TextPosition)
        If newPosition.LineIndex < 0 Then newPosition.LineIndex = 0
        If newPosition.ColumnIndex < 0 Then newPosition.ColumnIndex = 0
        If newPosition.LineIndex > _lines.Count - 1 Then newPosition.LineIndex = _lines.Count - 1
        If newPosition.ColumnIndex > _lines(newPosition.LineIndex).Text.Length Then newPosition.ColumnIndex = _lines(newPosition.LineIndex).Text.Length
        If newPosition.LineIndex <> _currentPosition.LineIndex Or newPosition.ColumnIndex <> _currentPosition.ColumnIndex Then
            _currentPosition = newPosition
            Dim visLine = _currentPosition.LineIndex - _scrollPosition.LineIndex
            If visLine > _linesPerScreen - 2 Then
                _scrollPosition.LineIndex = _currentPosition.LineIndex - _linesPerScreen + 2
                RedrawAll()
            ElseIf visLine < 0 Then
                _scrollPosition.LineIndex = _currentPosition.LineIndex
                RedrawAll()
            End If

            Dim visColumn = _currentPosition.ColumnIndex - _scrollPosition.ColumnIndex
            If visColumn > _columnsPerScreen - 4 Then
                _scrollPosition.ColumnIndex = _currentPosition.ColumnIndex - _columnsPerScreen + 3
                RedrawAll()
            ElseIf visColumn < 0 Then
                _scrollPosition.ColumnIndex = _currentPosition.ColumnIndex
                RedrawAll()
            End If

            DrawCursor(True, False)
        End If
        CheckLineWasEdited()
    End Sub

    Public Sub RedrawAll()
        VScrollBar1.Value = _scrollPosition.LineIndex
        VScrollBar1.Maximum = _lines.Count - 1

        HScrollBar1.Value = _scrollPosition.ColumnIndex
        HScrollBar1.Maximum = _lines.Count - 1

        _graphics.Clear(_colorScheme.BackColor)
        Dim time = Now
        Dim longestLine As Integer
        For i = 0 To _lines.Count - 1
            longestLine = Math.Max(_lines(i).Text.Length, longestLine)
        Next
        Dim horizontalScrollSize = longestLine - _columnsPerScreen + 1
        If horizontalScrollSize < 1 Then
            HScrollBar1.Enabled = False
        Else
            If HScrollBar1.Value > horizontalScrollSize Then HScrollBar1.Value = horizontalScrollSize
            HScrollBar1.Maximum = horizontalScrollSize
            HScrollBar1.Enabled = True
        End If
        For i = _scrollPosition.LineIndex To Math.Min(_scrollPosition.LineIndex + _linesPerScreen, _lines.Count - 1)
            DrawLine(i)
        Next
        Dim ms = (Now - time).TotalMilliseconds
        ShowGraphics()
        CheckLineWasEdited()
    End Sub

    Private Sub TextBoxEx_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If _focused And e.Button = MouseButtons.Left Then
            Dim pos = ScreenToTextPosition(e.X, e.Y)
            SetPosition(pos)
            _clickedStart = pos
            If _selectedStart <> _selectedEnd Then
                'was selected
                Dim a = _selectedStart.LineIndex
                Dim b = _selectedEnd.LineIndex
                _selectedStart = pos
                _selectedEnd = pos
                For i = a To b
                    ClearLine(i)
                    DrawLine(i)
                Next
                ShowGraphics()
            Else
                _selectedStart = pos
                _selectedEnd = pos
            End If
            CheckLineWasEdited()
        End If
    End Sub

    Private Sub TextBoxEx_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If _focused And e.Button = MouseButtons.Left Then
            Dim c = _selectedStart.LineIndex
            Dim d = _selectedEnd.LineIndex

            _selectedStart = _clickedStart
            _selectedEnd = ScreenToTextPosition(e.X, e.Y)
            SetPosition(_selectedEnd)
            If _selectedStart > _selectedEnd Then TextPosition.Swap(_selectedStart, _selectedEnd)

            _selectedStart.ColumnIndex = Math.Min(_selectedStart.ColumnIndex, _lines(_selectedStart.LineIndex).Text.Length - 1)
            _selectedEnd.ColumnIndex = Math.Min(_selectedEnd.ColumnIndex, _lines(_selectedEnd.LineIndex).Text.Length - 1)
            _selectedStart.ColumnIndex = Math.Max(0, _selectedStart.ColumnIndex)
            _selectedEnd.ColumnIndex = Math.Max(0, _selectedEnd.ColumnIndex)
            'If _selectedStart.ColumnIndex < 0 Then Stop
            'If _selectedEnd.ColumnIndex < 0 Then Stop
            Dim a = _selectedStart.LineIndex
            Dim b = _selectedEnd.LineIndex
            'If a < 0 Then a = 0
            'If b > _lines.Count - 1 Then b = _lines.Count - 1
            For i = Math.Min(a, c) To Math.Max(b, d)
                ClearLine(i)
                DrawLine(i)
            Next

            ShowGraphics()
        End If
    End Sub

    Private Function ScreenToTextPosition(x As Integer, y As Integer) As TextPosition
        Dim pos As TextPosition
        pos.LineIndex = Math.Floor(y / _fontParams.LineHeight + _scrollPosition.LineIndex)
        pos.ColumnIndex = Math.Floor(x / _fontParams.CharSpaceWidth + _scrollPosition.ColumnIndex)
        If pos.LineIndex > _lines.Count - 1 Then pos.LineIndex = _lines.Count - 1
        If pos.LineIndex < 0 Then pos.LineIndex = 0
        Return pos
    End Function

    Private Sub TextBoxEx_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        _scrollPosition.LineIndex += -e.Delta / _fontParams.LineHeight
        If _scrollPosition.LineIndex < 0 Then _scrollPosition.LineIndex = 0
        If _scrollPosition.LineIndex > _lines.Count - 1 Then _scrollPosition.LineIndex = _lines.Count - 1
        RedrawAll()
    End Sub

    Public Sub UserDispose()
        _fontParams.Dispose()
        _textPen.Dispose()
        _selectedBrush.Dispose()
        _backgoundPen.Dispose()
        _backgoundBrush.Dispose()
        _cursorPen.Dispose()
        _graphics.Dispose()
        _graphicsBmp.Dispose()
    End Sub

    Private Sub VScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles VScrollBar1.Scroll
        _scrollPosition.LineIndex = e.NewValue
        RedrawAll()
    End Sub

    Private Sub HScrollBar1_Scroll(sender As Object, e As ScrollEventArgs) Handles HScrollBar1.Scroll
        _scrollPosition.ColumnIndex = e.NewValue
        RedrawAll()
    End Sub

    Public ReadOnly Property Lines As IList(Of TextLine)
        Get
            Return _lines.AsReadOnly
        End Get
    End Property

    Public Overrides Function ToString() As String
        Dim sb As New StringBuilder
        For Each line In Lines
            sb.AppendLine(line.Text)
        Next
        Return sb.ToString
    End Function

    Public Sub LoadFromString(str As String)
        Dim lines = str.Split({vbCrLf, vbCr, vbLf}, StringSplitOptions.None)
        _lines.Clear()
        For Each line In lines
            _lines.Add(New TextLine(line))
        Next
        If _lines.Count = 0 Then _lines.Add(New TextLine(""))
        _selectedStart = New TextPosition
        _selectedEnd = New TextPosition
        _currentPosition = New TextPosition
        _undoBuffer.Clear()
        _redoBuffer.Clear()
        SaveCurrentState()
        RedrawAll()
    End Sub

    Private Sub InputTimer_Tick_1(sender As Object, e As EventArgs) Handles InputTimer.Tick
        _InputTimer.Stop()
        SaveCurrentState()
    End Sub


End Class

Public Structure TextPosition
    Public LineIndex As Integer
    Public ColumnIndex As Integer

    Public Sub New(line As Integer, column As Integer)
        LineIndex = line
        ColumnIndex = column
    End Sub

    Public Function Add(addLine As Integer, addColumn As Integer) As TextPosition
        Dim result As TextPosition
        result.LineIndex = LineIndex + addLine
        result.ColumnIndex = ColumnIndex + addColumn
        Return result
    End Function

    Public Overloads Shared Operator <(v1 As TextPosition, v2 As TextPosition) As Boolean
        If v1.LineIndex < v2.LineIndex Then Return True
        If v1.LineIndex = v2.LineIndex And v1.ColumnIndex < v2.ColumnIndex Then Return True
        Return False
    End Operator
    Public Overloads Shared Operator >(v1 As TextPosition, v2 As TextPosition) As Boolean
        If v1.LineIndex > v2.LineIndex Then Return True
        If v1.LineIndex = v2.LineIndex And v1.ColumnIndex > v2.ColumnIndex Then Return True
        Return False
    End Operator
    Public Overloads Shared Operator <=(v1 As TextPosition, v2 As TextPosition) As Boolean
        If v1.LineIndex < v2.LineIndex Then Return True
        If v1.LineIndex = v2.LineIndex And v1.ColumnIndex <= v2.ColumnIndex Then Return True
        Return False
    End Operator
    Public Overloads Shared Operator >=(v1 As TextPosition, v2 As TextPosition) As Boolean
        If v1.LineIndex > v2.LineIndex Then Return True
        If v1.LineIndex = v2.LineIndex And v1.ColumnIndex >= v2.ColumnIndex Then Return True
        Return False
    End Operator
    Public Overloads Shared Operator =(v1 As TextPosition, v2 As TextPosition) As Boolean
        Return v1.LineIndex = v2.LineIndex AndAlso v1.ColumnIndex = v2.ColumnIndex
    End Operator
    Public Overloads Shared Operator <>(v1 As TextPosition, v2 As TextPosition) As Boolean
        Return v1.LineIndex <> v2.LineIndex OrElse v1.ColumnIndex <> v2.ColumnIndex
    End Operator
    Public Shared Sub Swap(ByRef v1 As TextPosition, ByRef v2 As TextPosition)
        Dim tmp = v1
        v1 = v2
        v2 = tmp
    End Sub

    Public Overrides Function ToString() As String
        Return "Line: " + LineIndex.ToString + ", Column: " + ColumnIndex.ToString
    End Function
End Structure

Public Class TextLine
    Private _text As String = ""
    Public ReadOnly Property Attributes As TextAttribute()
    Public ReadOnly Property AttributesValid As Boolean

    Public Property Text As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
            _AttributesValid = False
        End Set
    End Property

    Public Property OverlayText As String = ""

    Public Sub SetAttributes(attrbArray As TextAttribute())
        _Attributes = attrbArray
        _AttributesValid = True
    End Sub

    Public Sub SetAttributeToAll(attrb As TextAttribute)
        Dim attribs(Text.Length - 1) As TextAttribute
        For i = 0 To attribs.Length - 1
            attribs(i) = attrb
        Next
        SetAttributes(attribs)
    End Sub

    Public Sub New()
    End Sub

    Public Sub New(line As String)
        Text = line
    End Sub

    Public Overrides Function ToString() As String
        Return Text
    End Function

End Class

Public Class TextAttribute
    Public Property ForeColor As Color = Color.Transparent
    Public Property BackColor As Color = Color.Transparent
End Class

Public Class ColorScheme
    Public Property BackColor As Color = Color.White
    Public Property TextColor As Color = Color.Black
    Public Property SelectedBackColor As Color = Color.LightBlue
    Public Property OverlayColor As Color = Color.Gray
End Class