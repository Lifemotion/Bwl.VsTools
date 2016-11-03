Public Class VsToolsForm
    Private _dir As String = IO.Directory.GetCurrentDirectory

    Private Sub buildRelease_Click(sender As Object, e As EventArgs) Handles buildRelease.Click
        Dim bt As New BuildTool
        bt.BuildAll({"-release", "*"})
    End Sub

    Private Sub explorer_Click(sender As Object, e As EventArgs) Handles explorer.Click
        Shell("explorer """ + _dir + """", AppWinStyle.NormalFocus)
    End Sub

    Private Sub projectCheck_Click(sender As Object, e As EventArgs) Handles bProjectCheck.Click
        ProjectCheck.CheckProjects(_dir)
    End Sub

    Private Sub gitClean_Click(sender As Object, e As EventArgs) Handles gitClean.Click
        CleanTool.GitClean(_dir)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Shell("cmd")
    End Sub

    Private Sub buildDebug_Click(sender As Object, e As EventArgs) Handles buildDebug.Click
        Dim bt As New BuildTool
        bt.BuildAll({"-debug", "*"})
    End Sub

    Private Sub openGitIgnore_Click(sender As Object, e As EventArgs) Handles openGitIgnore.Click
        GitignoreTools.Open(_dir)
    End Sub

    Private Sub bAddStandardGittgnore_Click(sender As Object, e As EventArgs) Handles bAddStandardGittgnore.Click
        GitignoreTools.AddStandardItems(_dir)
    End Sub

    Private Sub VsToolsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If _dir.EndsWith("tools") Then
            _dir = IO.Path.GetFullPath(IO.Path.Combine(_dir, ".."))
        End If
        Dim dirname = IO.Path.GetFileName(_dir)
        Text += " " + dirname
    End Sub

    Private Sub licenseFile_Click(sender As Object, e As EventArgs) Handles licenseFile.Click
        Shell("notepad LICENSE", AppWinStyle.NormalFocus)
    End Sub

    Private Sub readmeFile_Click(sender As Object, e As EventArgs) Handles readmeFile.Click
        Shell("notepad README", AppWinStyle.NormalFocus)
    End Sub

    Private Sub renameProject_Click(sender As Object, e As EventArgs) Handles renameProject.Click
        Dim oldProjectName = InputBox("Specify the name of a project which you want to rename:", "Old project name")
        Dim newProjectName = InputBox("Specify the NEW name of a project:", "New project name")

        If String.IsNullOrWhiteSpace(oldProjectName) OrElse String.IsNullOrWhiteSpace(newProjectName) Then
            MessageBox.Show("You did not specified a project name. Project rename will be cancelled.")
        Else
            ProjectRenamer.ProcessFolder(_dir, "refs, docs, bin, obj, tools, .git", oldProjectName, newProjectName, "ico, bmp, png, dll, exe")
        End If
    End Sub
End Class
