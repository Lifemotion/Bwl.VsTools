Public Class ErrorsList
    Private _buildTask As BuildTask

    Public Property AssociatedBuildTask
        Get
            Return _buildTask
        End Get
        Set(value)
            If _buildTask IsNot Nothing Then
                RemoveHandler _buildTask.Logger, AddressOf BuildLogHandler
                RemoveHandler _buildTask.BuildStarted, AddressOf BuildStartedHandler
                RemoveHandler _buildTask.BuildFinished, AddressOf BuildFinishedHandler
            End If
            _buildTask = value
            If _buildTask IsNot Nothing Then
                AddHandler _buildTask.Logger, AddressOf BuildLogHandler
                AddHandler _buildTask.BuildStarted, AddressOf BuildStartedHandler
                AddHandler _buildTask.BuildFinished, AddressOf BuildFinishedHandler
            End If

            If _buildTask Is Nothing Then
                DataGridView1.Rows.Clear()
                _rowMsgs.Clear()
                _buildTask = Nothing
                cbErrors.Text = "Errors"
                cbWarnings.Text = "Warnings"
                cbOther.Text = "Other"
            End If
        End Set
    End Property

    Private Sub BuildStartedHandler(source As BuildTask)
        tbLog.Clear()
        TabControl1.SelectedTab = tabBuildLog
    End Sub

    Private Sub BuildFinishedHandler(source As BuildTask)
        Me.Invoke(Sub() ShowMessages())
        TabControl1.SelectedTab = tabErrors
    End Sub

    Private _rowMsgs As New List(Of BuildMessage)

    Private Sub ShowMessages() Handles cbWarnings.CheckedChanged, cbOther.CheckedChanged, cbErrors.CheckedChanged
        DataGridView1.Rows.Clear()
        _rowMsgs.Clear()
        If _buildTask Is Nothing Then Return
        Dim msgs = _buildTask.LastBuildMessages.ToArray
        Dim errCount = 0
        Dim warnCount = 0
        Dim otherCount = 0
        Dim number = 0
        For Each msg In msgs
            Select Case msg.Type
                Case BuildMessageType.BuildError
                    errCount += 1
                    number += 1
                    If cbErrors.Checked Then
                        DataGridView1.Rows.Add(number, My.Resources._error, msg.Message, GetFileName(msg))
                        _rowMsgs.Add(msg)
                    End If
                Case BuildMessageType.BuildWarning
                    warnCount += 1
                    number += 1
                    If cbWarnings.Checked Then
                        DataGridView1.Rows.Add(number, My.Resources.warning, msg.Message, GetFileName(msg))
                        _rowMsgs.Add(msg)
                    End If
                Case BuildMessageType.Debug Or BuildMessageType.Information
                    otherCount += 1
                    number += 1
                    If cbOther.Checked Then
                        DataGridView1.Rows.Add(number, Nothing, msg.Message, GetFileName(msg))
                        _rowMsgs.Add(msg)
                    End If
            End Select
        Next
        cbErrors.Text = errCount.ToString + " Errors"
        cbWarnings.Text = warnCount.ToString + " Warnings"
        cbOther.Text = otherCount.ToString + " Other"
    End Sub

    Private Function GetFileName(msg As BuildMessage) As String
        Dim text = "-"
        If msg.SourceFile > "" Then
            text = System.IO.Path.GetFileName(msg.SourceFile)
        End If
        text = text + "," + msg.SourceFileLine.ToString + "," + msg.SourceFileColumn.ToString
        Return text
    End Function

    Private Sub BuildLogHandler(type As String, msg As String)
        tbLog.AppendText(msg + vbCrLf)
    End Sub

    Public Event BuildMessageDoubleClick(msg As BuildMessage)


    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        If e.RowIndex >= 0 Then RaiseEvent BuildMessageDoubleClick(_rowMsgs(e.RowIndex))

    End Sub
End Class
