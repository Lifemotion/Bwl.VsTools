Imports System.Drawing
Imports System.Windows.Forms

Public Class TextBoxEx
    Private _graphics As Graphics
    Private _font As Font = New Font("Consolas", 10)
    Private _focused As Boolean

    Private _cursorPen As Pen
    Private _backgoundPen As Pen
    Private _textPen As Pen
    Private _backgoundBrush As Brush

    Public Shadows Property Text As New Text
    Public Property FontSize As Integer = 9
    Public CurrentPosition As TextPosition
    Public ScrollPosition As TextPosition

    Private Property ColorScheme As New ColorScheme
    Private _linesPerScreen As Integer
    Dim _graphicsBmp As Bitmap
    Private _controlGraphics As Graphics

    Public Sub New()
        PrepareGraphics()
        ApplyColorScheme()
        InitializeComponent()
    End Sub

    Public Sub ApplyColorScheme()
        _cursorPen = New Pen(ColorScheme.TextColor)
        _backgoundPen = New Pen(ColorScheme.BackColor)
        _textPen = New Pen(ColorScheme.TextColor)
        _backgoundBrush = New SolidBrush(ColorScheme.BackColor)
        _graphics.Clear(ColorScheme.BackColor)
    End Sub

    Private Sub PrepareGraphics()
        _graphicsBmp = New Bitmap(Me.Width, Me.Height)
        _graphics = Graphics.FromImage(_graphicsBmp)
        _controlGraphics = CreateGraphics()
        _linesPerScreen = Math.Floor(Me.Height / 17)
        BackgroundImage = _graphicsBmp
    End Sub

    Private Sub ShowGraphics()
        _controlGraphics.DrawImage(_graphicsBmp, 0, 0)
    End Sub

    Public ReadOnly Property CurrentLine As TextLine
        Get
            Return Text.Lines(CurrentPosition.LineIndex)
        End Get
    End Property

    Private Sub TextBoxEx_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        PrepareGraphics()
        RedrawAll()
    End Sub

    Public Property NewlineSpacesAsPreviousLine As Boolean = True

    Private Function CountStartringSpaces(str As String) As Integer
        Dim i = 0
        While i < str.Length AndAlso str(i) = " "c
            i += 1
        End While
        Return i
    End Function

    Public Property TabSize As Integer = 4

    Private Sub TextBoxEx_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If _focused Then
            Select Case e.KeyChar
                Case vbCr, vbLf
                    Dim oldLine = Text.Lines(CurrentPosition.LineIndex).Text.Substring(0, CurrentPosition.ColumnIndex)
                    Dim newLine = Text.Lines(CurrentPosition.LineIndex).Text.Substring(CurrentPosition.ColumnIndex)
                    CurrentPosition.ColumnIndex = 0
                    If NewlineSpacesAsPreviousLine Then
                        newLine = Space(CountStartringSpaces(oldLine)) + newLine
                        CurrentPosition.ColumnIndex = CountStartringSpaces(oldLine)
                    End If
                    Text.Lines.Insert(CurrentPosition.LineIndex + 1, New TextLine)
                    Text.Lines(CurrentPosition.LineIndex).Text = oldLine
                    Text.Lines(CurrentPosition.LineIndex + 1).Text = newLine
                    CurrentPosition.LineIndex += 1
                    RedrawAll()
                Case vbTab
                    If My.Computer.Keyboard.ShiftKeyDown = False Then
                        Dim tabsCount = Math.Floor((CurrentPosition.ColumnIndex + 1) / TabSize)
                        tabsCount += 1
                        Dim spacesNeeded = tabsCount * TabSize - CurrentPosition.ColumnIndex
                        CurrentLine.Text = CurrentLine.Text.Insert(CurrentPosition.ColumnIndex, Space(spacesNeeded))
                        CurrentPosition.ColumnIndex += spacesNeeded
                        DrawCurrentLine()
                        DrawCursor(True)
                    Else
                        Dim spaces = CountStartringSpaces(CurrentLine.Text)
                        Dim spacesToRemove = Math.Min(4, spaces)
                        CurrentLine.Text = CurrentLine.Text.Remove(0, spacesToRemove)
                        CurrentPosition.ColumnIndex = Math.Max(0, CurrentPosition.ColumnIndex - spacesToRemove)

                        DrawCurrentLine()
                        DrawCursor(True)
                    End If
                Case vbBack
                    If CurrentPosition.ColumnIndex > 0 Then
                        CurrentLine.Text = CurrentLine.Text.Remove(CurrentPosition.ColumnIndex - 1, 1)
                        CurrentPosition.ColumnIndex -= 1
                        DrawCurrentLine()
                    Else
                        If CurrentPosition.LineIndex > 0 Then
                            CurrentPosition.ColumnIndex = Text.Lines(CurrentPosition.LineIndex - 1).Text.Length
                            Text.Lines(CurrentPosition.LineIndex - 1).Text += CurrentLine.Text
                            Text.Lines.RemoveAt(CurrentPosition.LineIndex)
                            CurrentPosition.LineIndex -= 1
                            RedrawAll()
                        End If
                    End If
                Case Else
                    CurrentLine.Text = CurrentLine.Text.Insert(CurrentPosition.ColumnIndex, e.KeyChar.ToString)
                    CurrentPosition.ColumnIndex += 1
                    DrawCurrentLine()
                    DrawCursor(True)
            End Select
        End If
    End Sub

    Private Sub DrawCurrentLine()
        ClearLine(CurrentPosition.LineIndex)
        DrawLine(CurrentPosition.LineIndex)
        ShowGraphics()
    End Sub

    Private Sub ClearLine(index As Integer)
        Dim charRect = GetCharRect(New TextPosition(index, 0))
        _graphics.FillRectangle(_backgoundBrush, 0, charRect.Top, Me.Width, charRect.Height)

    End Sub

    Public Event BeforeDrawLine(index As Integer, line As TextLine)

    Private Sub DrawLine(index As Integer)
        Dim line = Text.Lines(index)
        RaiseEvent BeforeDrawLine(index, line)
        Dim brush = Brushes.Black
        Dim brushColor = Color.Black
        For i = 0 To line.Text.Length - 1
            Dim requestedColor = Color.Black
            Dim charRect = GetCharRect(New TextPosition(index, i))
            If i < line.Attributes.Length AndAlso line.Attributes(i) IsNot Nothing Then
                requestedColor = line.Attributes(i).ForeColor
            End If
            If brushColor <> requestedColor Then
                brushColor = requestedColor
                brush = New SolidBrush(brushColor)
            End If
            _graphics.DrawString(line.Text(i), _font, brush, charRect.Left, charRect.Top)
        Next
    End Sub

    Private Function GetCharRect(position As TextPosition) As Rectangle
        Dim rect = New Rectangle
        rect.Width = 8
        rect.Height = 16
        rect.X = position.ColumnIndex * (rect.Width)
        rect.Y = (position.LineIndex - ScrollPosition.LineIndex) * (rect.Height + 1)

        Return rect
    End Function

    Private Sub TextBoxEx_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
        _focused = True
    End Sub

    Private Sub TextBoxEx_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        _focused = False
    End Sub

    Private Sub DrawCursor(forceDraw As Boolean)
        Static lastState As Boolean
        Static lastPosition As TextPosition
        Dim offset = 1
        If lastState Then
            Dim rect = GetCharRect(lastPosition)
            lastState = False
            _graphics.DrawLine(_backgoundPen, rect.Left + offset, rect.Top, rect.Left + offset, rect.Bottom)
        Else
            lastState = True
            lastPosition = CurrentPosition
            Dim rect = GetCharRect(lastPosition)
            _graphics.DrawLine(_cursorPen, rect.Left + offset, rect.Top, rect.Left + offset, rect.Bottom)
        End If
        If forceDraw And lastState = False Then
            lastState = True
            lastPosition = CurrentPosition
            Dim rect = GetCharRect(lastPosition)
            _graphics.DrawLine(_cursorPen, rect.Left + offset, rect.Top, rect.Left + offset, rect.Bottom)
        End If
        ShowGraphics()

    End Sub

    Private Sub cursorTimer_Tick(sender As Object, e As EventArgs) Handles cursorTimer.Tick
        DrawCursor(False)
    End Sub

    Private Sub TextBoxEx_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBoxEx_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles Me.PreviewKeyDown
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
        End Select
    End Sub

    Public Sub ChangePosition(lineChange As Integer, columnChange As Integer)
        SetPosition(CurrentPosition.Add(lineChange, columnChange))
    End Sub

    Public Sub SetPosition(newPosition As TextPosition)
        If newPosition.LineIndex < 0 Then newPosition.LineIndex = 0
        If newPosition.ColumnIndex < 0 Then newPosition.ColumnIndex = 0
        If newPosition.LineIndex > Text.Lines.Count - 1 Then newPosition.LineIndex = Text.Lines.Count - 1
        If newPosition.ColumnIndex > Text.Lines(newPosition.LineIndex).Text.Length Then newPosition.ColumnIndex = Text.Lines(newPosition.LineIndex).Text.Length
        If newPosition.LineIndex <> CurrentPosition.LineIndex Or newPosition.ColumnIndex <> CurrentPosition.ColumnIndex Then
            CurrentPosition = newPosition
            Dim visLine = CurrentPosition.LineIndex - ScrollPosition.LineIndex
            If visLine > _linesPerScreen - 1 Then
                ScrollPosition.LineIndex = CurrentPosition.LineIndex - _linesPerScreen + 1
                RedrawAll()
            End If
            If visLine < 0 Then
                ScrollPosition.LineIndex = CurrentPosition.LineIndex
                RedrawAll()
            End If
            DrawCursor(True)
        End If
    End Sub

    Public Sub RedrawAll()
        _graphics.Clear(ColorScheme.BackColor)
        Dim time = Now
        For i = ScrollPosition.LineIndex To Math.Min(ScrollPosition.LineIndex + _linesPerScreen, Text.Lines.Count - 1)
            DrawLine(i)
        Next
        Dim ms = (Now - time).TotalMilliseconds
        ShowGraphics()
    End Sub

    Private Sub TextBoxEx_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        If _focused Then
            SetPosition(New TextPosition(Math.Floor(e.Y / 17 + ScrollPosition.LineIndex), Math.Floor(e.X / 8 + ScrollPosition.ColumnIndex)))
        End If
    End Sub

    Private Sub TextBoxEx_MouseWheel(sender As Object, e As MouseEventArgs) Handles Me.MouseWheel
        ChangePosition(-e.Delta / 19, 0)
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
End Structure

Public Class Text
    Public ReadOnly Property Lines As New List(Of TextLine)

    Public Sub New()
        Lines.Add(New TextLine)
    End Sub
End Class

Public Class TextLine
    Public Property Text As String = ""
    Public Property Attributes As TextAttribute() = {}

    Public Sub New()

    End Sub
    Public Sub New(line As String)
        Text = line
    End Sub

End Class

Public Class TextAttribute
    Public Property ForeColor As Color = Color.Transparent
    Public Property BackColor As Color = Color.Transparent
End Class

Public Class ColorScheme
    Public Property BackColor As Color = Color.White
    Public Property TextColor As Color = Color.Black
    Public Property SelectedTextColor As Color = Color.White
    Public Property SelectedBackColor As Color = Color.DarkBlue
End Class