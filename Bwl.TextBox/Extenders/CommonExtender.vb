Public MustInherit Class CommonExtender


    Public MustOverride Sub ConnectTo(textbox As TextBoxEx)

    Public MustOverride Sub DisconnectFrom(textbox As TextBoxEx)

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
