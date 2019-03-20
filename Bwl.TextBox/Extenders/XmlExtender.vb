Imports System.Drawing

Public Class XmlExtender
    Inherits CommonExtender

    Public Overrides Sub ConnectTo(textbox As TextBoxEx)
        DisconnectFrom(textbox)
        AddHandler textbox.BeforeDrawLine, AddressOf TextBoxEx_BeforeDrawLine
    End Sub

    Public Overrides Sub DisconnectFrom(textbox As TextBoxEx)
        Try
            RemoveHandler textbox.BeforeDrawLine, AddressOf TextBoxEx_BeforeDrawLine
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TextBoxEx_BeforeDrawLine(sender As Object, index As Integer, line As TextLine)
        ReDim line.Attributes(line.Text.Length - 1)
        Dim tokens = TokenizeXml(line.Text)
        Dim open As New TextAttribute With {.ForeColor = Color.Blue}
        Dim close As New TextAttribute With {.ForeColor = Color.DarkBlue}
        Dim digit As New TextAttribute With {.ForeColor = Color.DarkGoldenrod}
        Dim str As New TextAttribute With {.ForeColor = Color.Purple}
        Dim comment As New TextAttribute With {.ForeColor = Color.DarkGreen}
        For Each token In tokens
            If IsNumeric(token.Str) Then token.ApplyAttribute(line.Attributes, digit)
            If token.Str.StartsWith("""") Then token.ApplyAttribute(line.Attributes, str)
            If token.Str.StartsWith("<") Then token.ApplyAttribute(line.Attributes, open)
            If token.Str.StartsWith("</") Then token.ApplyAttribute(line.Attributes, close)
        Next
        For i = 0 To line.Text.Length - 1
            If line.Text(i) = "'" Then
                For j = i To line.Text.Length - 1
                    line.Attributes(j) = comment
                Next
                Exit For
            End If
        Next

    End Sub

    Friend Function TokenizeXml(str As String) As List(Of Token)
        Dim tokens As New List(Of Token)
        Dim started = -1
        For i = 0 To str.Length - 1
            Select Case (str(i))
                Case " ", "(", ")", ",", "[", "]", "'", ">"
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

End Class
