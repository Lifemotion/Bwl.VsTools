Public Class FilesEditor
    Private _item As SolutionItem
    Private _itemText As String
    Private _vbnetExtender As New VBNetExtender
    Private _xmlExtender As New XmlExtender
    Private _cExtender As New CExtender

    Public Sub SetAssociatedItem(item As SolutionItem)
        _item = item
        _vbnetExtender.DisconnectFrom(TextBoxEx1)
        _xmlExtender.DisconnectFrom(TextBoxEx1)
        _cExtender.DisconnectFrom(TextBoxEx1)
        Select Case IO.Path.GetExtension(item.FullPath.ToLower)
            Case ".vb"
                _vbnetExtender.ConnectTo(TextBoxEx1)
            Case ".xml", ".vbproj", ".sln"
                _xmlExtender.ConnectTo(TextBoxEx1)
            Case ".c", ".cpp", ".h"
                _cExtender.ConnectTo(TextBoxEx1)
        End Select
        AddHandler TextBoxEx1.TextChanged, Sub()
                                               _item.UnsavedContent = TextBoxEx1.Lines
                                           End Sub
        TextBoxEx1.LoadFromString(IO.File.ReadAllText(item.FullPath))
    End Sub

    Public Sub SetCursor(line As Integer, column As Integer)
        TextBoxEx1.SetPosition(New TextPosition(line - 1, column - 1))
    End Sub

    Private Sub FilesEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class
