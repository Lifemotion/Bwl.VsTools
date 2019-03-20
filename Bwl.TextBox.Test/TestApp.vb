Imports Bwl.TextBoxEx

Public Class TestApp
    Private Sub TestApp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim text = IO.File.ReadAllText("C:\Users\heart\Repositories\Bwl.VsTools\Bwl.TextBox.Test\TestApp.Designer.vb")
        TextBoxEx1.LoadFromString(text)
    End Sub


    Private Sub TextBoxEx1_Load(sender As Object, e As EventArgs) Handles TextBoxEx1.Load

    End Sub
End Class
