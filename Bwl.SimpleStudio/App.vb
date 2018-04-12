Public Class App
    Private Sub App_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim path = Command().Replace("""", "")
        FilesTree1.LoadTree(path)
    End Sub
End Class
