Public Class App

    Private Sub App_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim path = Command().Replace("""", "")
        Try
            OpenPath(path)
        Catch ex As Exception
        End Try
    End Sub

    Public Sub OpenPath(path As String)
        FilesTree1.LoadTree(path)
        ToolStripComboBox1.Items.Clear()
        For Each target In FilesTree1.Root.ExecutableTargets
            ToolStripComboBox1.Items.Add(target)
        Next
        If ToolStripComboBox1.Items.Count > 0 Then
            ToolStripComboBox1.SelectedIndex = 0
        End If
    End Sub

    Private Sub BuildAllToolStripMenuItem_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub FilesTree1_FileOpenRequest(item As SolutionItem) Handles FilesTree1.FileOpenRequest
        FileEditor1.OpenFile(item)
    End Sub

    Private Sub tsbSaveAll_Click(sender As Object, e As EventArgs) Handles tsbSaveAll.Click
        Try
            FilesTree1.Root.SaveAll()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub App_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        If FilesTree1.Root.SaveAllWithAsk = DialogResult.Cancel Then e.Cancel = True
    End Sub

    Private Sub tsbBuildAll_Click(sender As Object, e As EventArgs) Handles tsbBuildAll.Click
        If FilesTree1.Root.SaveAllWithAsk = DialogResult.OK Then
            StopRunning()
            Dim task As New BuildTask(FilesTree1.SolutionsList(0).FullPath, "Debug")
            ErrorsList1.AssociatedBuildTask = task
            task.ForceRebuild = True
            task.Build("")
        End If
    End Sub

    Private Sub tsbSave_Click(sender As Object, e As EventArgs) Handles tsbSave.Click
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

    Private _runningTarget As Process

    Private Sub tabRun_Click(sender As Object, e As EventArgs) Handles tabRun.Click
        If ToolStripComboBox1.SelectedItem IsNot Nothing Then
            Dim target As ExecutableTarget = ToolStripComboBox1.SelectedItem
            If FilesTree1.Root.SaveAllWithAsk = DialogResult.OK Then
                StopRunning()
                Dim task As New BuildTask(FilesTree1.SolutionsList(0).FullPath, "Debug")
                ErrorsList1.AssociatedBuildTask = task
                task.ForceRebuild = True
                task.Build("")
                If task.Success = False Then
                    If MsgBox("Build failed, run anyway?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return
                End If
                _runningTarget = New Process
                _runningTarget.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(target.FullPath)
                _runningTarget.StartInfo.FileName = target.FullPath
                _runningTarget.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                Try
                    _runningTarget.Start()
                Catch ex As Exception
                    MsgBox("Run failed: " + ex.Message)
                End Try
            End If

        End If
    End Sub

    Private Sub tsbStop_Click(sender As Object, e As EventArgs) Handles tsbStop.Click
        StopRunning()
    End Sub

    Private Sub StopRunning()
        If _runningTarget IsNot Nothing Then
            _runningTarget.Kill()
        End If
    End Sub

    Private Sub tsbOpenSolution_Click(sender As Object, e As EventArgs) Handles tsbOpenSolution.Click
        Dim dlg As New OpenFileDialog
        dlg.Filter = "Solutions|*.sln"
        If dlg.ShowDialog = DialogResult.OK Then
            OpenPath(dlg.FileName)
        End If
    End Sub
End Class
