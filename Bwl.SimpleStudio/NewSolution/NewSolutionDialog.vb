Public Class NewSolutionDialog
    Public Property CreatedSolutionPath As String = ""

    Private Sub ErrorsList1_Load(sender As Object, e As EventArgs)

    End Sub

    Private Sub bCancel_Click(sender As Object, e As EventArgs) Handles bCancel.Click
        Close()
    End Sub

    Private Sub NewSolutionDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim rnd As New Random
        tbProjectName.Text = "BwlProject_" + rnd.Next.ToString
        tbLocation.Text = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
    End Sub

    Private Sub tbProjectName_TextChanged(sender As Object, e As EventArgs) Handles tbProjectName.TextChanged
        tbSolutionName.Text = tbProjectName.Text
    End Sub

    Private Sub tbLocation_TextChanged(sender As Object, e As EventArgs) Handles tbLocation.TextChanged, tbSolutionName.TextChanged
        tbSolutionPath.Text = IO.Path.Combine(tbLocation.Text, tbSolutionName.Text)
    End Sub

    Private Sub bBrowse_Click(sender As Object, e As EventArgs) Handles bBrowse.Click
        Dim dlg As New OpenFileDialog
        dlg.CheckPathExists = False
        dlg.CheckFileExists = False
        dlg.FileName = "(Directory)"
        If dlg.ShowDialog(Me) = DialogResult.OK Then
            tbLocation.Text = IO.Path.GetDirectoryName(dlg.FileName)
        End If
    End Sub

    Private Sub bOK_Click(sender As Object, e As EventArgs) Handles bOK.Click
        Try
            Dim sln = NewSolutionTools.CreateVbCmdLineSolution(tbSolutionPath.Text, tbProjectName.Text)
            CreatedSolutionPath = sln
            Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Failed to create project")
        End Try
    End Sub
End Class