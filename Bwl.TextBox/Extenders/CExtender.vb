Imports System.Drawing

Public Class CExtender

    Public Sub ConnectTo(textbox As TextBoxEx)
        DisconnectFrom(textbox)
        AddHandler textbox.BeforeDrawLine, AddressOf TextBoxEx_BeforeDrawLine
        AddHandler textbox.LineEdited, AddressOf TextBoxEx_LineEdited
    End Sub

    Public Sub DisconnectFrom(textbox As TextBoxEx)
        Try
            RemoveHandler textbox.BeforeDrawLine, AddressOf TextBoxEx_BeforeDrawLine
            RemoveHandler textbox.LineEdited, AddressOf TextBoxEx_LineEdited
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBoxEx_LineEdited(sender As Object, lineIndex As Integer, line As TextLine, ByRef needRedraw As Boolean)
        ' Throw New NotImplementedException()
    End Sub

    Private Sub TextBoxEx_BeforeDrawLine(sender As Object, index As Integer, line As TextLine)
        Dim attribs(line.Text.Length - 1) As TextAttribute

        Dim tokens = Tokenize(line.Text)
        Dim keyword As New TextAttribute With {.ForeColor = Color.Blue}
        Dim typedef As New TextAttribute With {.ForeColor = Color.MediumPurple}
        Dim punct As New TextAttribute With {.ForeColor = Color.DarkRed}
        Dim digit As New TextAttribute With {.ForeColor = Color.DarkGoldenrod}
        Dim str As New TextAttribute With {.ForeColor = Color.Purple}
        Dim preproc As New TextAttribute With {.ForeColor = Color.Brown}
        Dim comment As New TextAttribute With {.ForeColor = Color.DarkGreen}
        For Each token In tokens
            Select Case token.Str
                Case "auto", "break", "case", "const", "continue", "do", "else",
                     "enum", "extern", "sizeof", "static", "struct", "switch", "typedef",
                     "union", "void", "volatile", "while", "inline"
                    token.ApplyAttribute(attribs, keyword)
                Case "int", "char", "long", "float", "double", "unsigned", "signed",
                               "uint8_t", "uint16_t", "uint32_t", "int8_t", "int16_t", "int32_t"
                    token.ApplyAttribute(attribs, typedef)
            End Select
            If IsNumeric(token.Str) Then token.ApplyAttribute(attribs, digit)
            If token.Str.StartsWith("""") Then token.ApplyAttribute(attribs, str)
            If token.Str.StartsWith("'") Then token.ApplyAttribute(attribs, str)
        Next
        For i = 0 To line.Text.Length - 1

            Select Case line.Text(i)
                Case "=", "!", "(", ")", ";", ",", "[", "]", "{", "}"
                    attribs(i) = punct
            End Select
        Next

        If line.Text.Trim.StartsWith("#") Then
            For j = 0 To line.Text.Length - 1
                attribs(j) = preproc
            Next
        End If
        If line.Text.Trim.StartsWith("//") Then
            For j = 0 To line.Text.Length - 1
                attribs(j) = comment
            Next
        End If
        line.SetAttributes(attribs)
    End Sub

    Friend Function Tokenize(str As String) As List(Of Token)
        Dim tokens As New List(Of Token)
        Dim started = -1
        For i = 0 To str.Length - 1
            Select Case (str(i))
                Case " ", "(", ")", ",", "[", "]", "'"
                    If started > -1 Then
                        tokens.Add(New Token With {.IndexFrom = started, .IndexTo = i - 1, .Str = str.Substring(.IndexFrom, .IndexTo - .IndexFrom + 1)})
                        started = -1
                    End If
                Case Else
                    If started = -1 Then started = i
            End Select
        Next
        If started > -1 Then tokens.Add(New Token With {.IndexFrom = started, .IndexTo = str.Length - 1, .Str = str.Substring(.IndexFrom, .IndexTo - .IndexFrom + 1)})
        Return tokens
    End Function

    Public Class Token
        Public Str As String
        Public IndexFrom As Integer
        Public IndexTo As Integer
        Public Sub ApplyAttribute(attribArray As TextAttribute(), attrib As TextAttribute)
            For i = IndexFrom To IndexTo
                attribArray(i) = attrib
            Next
        End Sub
    End Class

End Class
