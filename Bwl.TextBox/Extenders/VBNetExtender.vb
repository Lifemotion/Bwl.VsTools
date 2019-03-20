Imports System.Drawing
Imports Bwl.TextBoxEx

Public Class VBNetExtender
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
        Dim tokens = Tokenize(line.Text)
        Dim keyword As New TextAttribute With {.ForeColor = Color.Blue}
        Dim digit As New TextAttribute With {.ForeColor = Color.DarkGoldenrod}
        Dim str As New TextAttribute With {.ForeColor = Color.Purple}
        Dim attrib As New TextAttribute With {.ForeColor = Color.Brown}
        Dim comment As New TextAttribute With {.ForeColor = Color.DarkGreen}
        For Each token In tokens
            Select Case token.Str
                Case "If", "Then", "Each", "Inherits", "As", "Sub", "Private",
                     "Public", "Protected", "Try", "Finally", "Partial", "Class", "End",
                     "Friend", "WithEvents", "Else", "Catch", "New"
                    token.ApplyAttribute(line.Attributes, keyword)
            End Select
            If IsNumeric(token.Str) Then token.ApplyAttribute(line.Attributes, digit)
            If token.Str.StartsWith("""") Then token.ApplyAttribute(line.Attributes, str)
            If token.Str.StartsWith("<") Then token.ApplyAttribute(line.Attributes, attrib)
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

End Class
