Imports Bwl.TextBox

Public Class TestApp
    Private Sub TestApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim lines = IO.File.ReadAllLines("C:\Users\heart\Repositories\Bwl.VsTools\Bwl.TextBox.Test\TestApp.Designer.vb")
        For Each line In lines
            TextBoxEx1.Text.Lines.Add(New TextLine(line))
        Next
        TextBoxEx1.RedrawAll()
    End Sub

    Private Sub TextBoxEx1_BeforeDrawLine(index As Integer, line As TextLine) Handles TextBoxEx1.BeforeDrawLine
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

    Private Function Tokenize(str As String) As List(Of Token)
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
            If started > -1 Then tokens.Add(New Token With {.IndexFrom = started, .IndexTo = str.Length - 1, .Str = str.Substring(.IndexFrom, .IndexTo - .IndexFrom + 1)})
        Next
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

    Private Sub TextBoxEx1_Load(sender As Object, e As EventArgs) Handles TextBoxEx1.Load

    End Sub
End Class
