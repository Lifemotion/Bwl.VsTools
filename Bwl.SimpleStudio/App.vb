Public Class App
    Private _builder As Builder
    Private _runnerDebugger As RunnerDebugger

    Private Sub App_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _builder = New Builder(ErrorsList1)
        _runnerDebugger = New RunnerDebugger(_builder)
        Text = "Bwl.SimpleStudio " + Application.ProductVersion.ToString
        OpenPath(Command().Replace("""", ""))
    End Sub

    Public Sub OpenPath(path As String)
        If path = "" Then Return
        If FilesTree1.Root Is Nothing OrElse FilesTree1.Root.SaveAllWithAsk = DialogResult.OK Then
            FileEditor1.CloseAllTabPages()
            ErrorsList1.AssociatedBuildTask = Nothing
            Try
                FilesTree1.LoadTree(path)
                ShowTargets()
                Text = IO.Path.GetFileNameWithoutExtension(path) + " - Bwl.SimpleStudio " + Application.ProductVersion.ToString
            Catch ex As Exception
                MsgBox("Open path error: " + ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub

    Private Sub App_DragOver(sender As Object, e As DragEventArgs) Handles Me.DragOver
        Dim d = e.Data.GetData("FileDrop")
        If d.length > 0 Then e.Effect = DragDropEffects.Copy
    End Sub

    Private Sub App_DragDrop(sender As Object, e As DragEventArgs) Handles Me.DragDrop
        Try
            OpenPath(e.Data.GetData("FileDrop")(0))
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ShowTargets() Handles tscbConfiguration.TextChanged
        tscbTargets.Items.Clear()
        If FilesTree1.Root IsNot Nothing Then
            For Each target In FilesTree1.Root.ExecutableTargets
                If target.Condition.Contains(tscbConfiguration.Text) Then tscbTargets.Items.Add(target)
            Next
            If tscbTargets.Items.Count > 0 Then tscbTargets.SelectedIndex = 0
        End If
    End Sub

    Private Sub FilesTree1_FileOpenRequest(item As SolutionItem) Handles FilesTree1.FileOpenRequest
        FileEditor1.OpenFile(item)
    End Sub

    Private Sub tsbSaveAll_Click(sender As Object, e As EventArgs) Handles tsbSaveAll.Click, SaveAllFilesToolStripMenuItem.Click
        Try
            FilesTree1.Root.SaveAll()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub App_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _runnerDebugger.StopRunning()
        If FilesTree1.Root IsNot Nothing AndAlso FilesTree1.Root.SaveAllWithAsk = DialogResult.Cancel Then e.Cancel = True
    End Sub

    Private Sub tsbBuildAll_Click(sender As Object, e As EventArgs) Handles tsbBuildAll.Click, BuildAllToolStripMenuItem.Click
        Try
            If FilesTree1.Root IsNot Nothing AndAlso FilesTree1.Root.SaveAllWithAsk = DialogResult.OK Then
                _runnerDebugger.StopRunning()
                _builder.BuildAll(FilesTree1.Root, tscbConfiguration.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, vbCritical)
        End Try

    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click, SaveFileToolStripMenuItem.Click
        Try
            If FileEditor1.SelectedSolutionItem IsNot Nothing Then FileEditor1.SelectedSolutionItem.Save()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub ErrorsList1_BuildMessageDoubleClick(msg As BuildMessage) Handles ErrorsList1.BuildMessageDoubleClick
        Dim item = FilesTree1.Root.FindItemByPath(msg.SourceFile)
        If item IsNot Nothing Then
            FileEditor1.OpenFile(item, msg.SourceFileLine, msg.SourceFileColumn)
        End If
    End Sub

    Private Sub tabRun_Click(sender As Object, e As EventArgs) Handles tabRun.Click, RunSelectedToolStripMenuItem.Click
        If tscbTargets.SelectedItem IsNot Nothing Then
            Dim target As ExecutableTarget = tscbTargets.SelectedItem
            target.Configuration = tscbConfiguration.Text
            _runnerDebugger.RunWithoutDebug(FilesTree1.Root, target)
        End If
    End Sub

    Private Sub tsbStop_Click(sender As Object, e As EventArgs) Handles tsbStop.Click, StopToolStripMenuItem.Click
        _runnerDebugger.StopRunning()
    End Sub

    Private Sub tsbOpenSolution_Click(sender As Object, e As EventArgs) Handles tsbOpenSolution.Click, OpenSolutionToolStripMenuItem.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Solutions|*.sln"
        If dlg.ShowDialog(Me) = DialogResult.OK Then
            OpenPath(dlg.FileName)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub DebugToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles DebugToolStripMenuItem1.Click
        If tscbTargets.SelectedItem IsNot Nothing Then
            Dim target As ExecutableTarget = tscbTargets.SelectedItem
            target.Configuration = tscbConfiguration.Text
            _runnerDebugger.RunWithDebug(FilesTree1.Root, target)
        End If
    End Sub

    Private Sub NewSolutionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewSolutionToolStripMenuItem.Click
        Dim dlg As New NewSolutionDialog
        dlg.ShowDialog(Me)
        If dlg.CreatedSolutionPath > "" Then
            OpenPath(dlg.CreatedSolutionPath)
        End If
    End Sub
End Class
