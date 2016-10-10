Public Class VsToolsForm
    Private Sub buildRelease_Click(sender As Object, e As EventArgs) Handles buildRelease.Click
        Dim bt As New BuildTool
        bt.BuildAll({"-release", "*"})
    End Sub

    Private Sub explorer_Click(sender As Object, e As EventArgs) Handles explorer.Click
        Shell("explorer .", AppWinStyle.NormalFocus)
    End Sub

    Private Sub projectCheck_Click(sender As Object, e As EventArgs) Handles bProjectCheck.Click
        ProjectCheck.CheckProjects(IO.Directory.GetCurrentDirectory)
    End Sub

    Private Sub gitClean_Click(sender As Object, e As EventArgs) Handles gitClean.Click
        CleanTool.GitClean(IO.Directory.GetCurrentDirectory)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Shell("cmd")
    End Sub

    Private Sub buildDebug_Click(sender As Object, e As EventArgs) Handles buildDebug.Click
        Dim bt As New BuildTool
        bt.BuildAll({"-debug", "*"})
    End Sub

    Private Sub openGitIgnore_Click(sender As Object, e As EventArgs) Handles openGitIgnore.Click
        If IO.File.Exists(".gitignore") = False Then IO.File.WriteAllText(".gitignore", "", System.Text.Encoding.UTF8)
        Shell("notepad .gitignore", AppWinStyle.NormalFocus)

    End Sub
End Class
