Public Class FilesEditor
    Private _item As SolutionItem
    Private _itemText As String

    Public Sub SetAssociatedItem(item As SolutionItem)
        _item = item
        TextBox1.Text = IO.File.ReadAllText(item.FullPath)
        AddHandler TextBox1.TextChanged, Sub()
                                             _item.UnsavedContent = TextBox1.Text
                                         End Sub
    End Sub

    Public Sub SetCursor(line As Integer, column As Integer)
        Dim pos = TextBox1.GetFirstCharIndexFromLine(line - 1)
        Dim pos2 = TextBox1.GetFirstCharIndexFromLine(line)
        If (pos > -1) Then
            TextBox1.Select()
            TextBox1.Select(pos + column - 1, pos2 - pos-column-1)
            TextBox1.ScrollToCaret()
        End If
    End Sub
End Class
