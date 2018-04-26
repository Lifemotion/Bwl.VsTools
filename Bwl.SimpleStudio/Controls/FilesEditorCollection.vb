Imports Bwl.SimpleStudio

Public Class FilesEditorCollection
    Public Sub OpenFile(item As SolutionItem)
        OpenFile(item, 0, 0)
    End Sub

    Public Sub OpenFile(item As SolutionItem, line As Integer, column As Integer)
        Dim existingPage = FindPageForItem(item.FullPath)
        If existingPage IsNot Nothing Then
            TabControl1.SelectedTab = existingPage
        Else
            Try
                Dim editor As New FilesEditor
                editor.SetAssociatedItem(item)
                Dim tp As New TabPage(item.Name)
                TabControl1.TabPages.Add(tp)
                tp.Controls.Add(editor)
                tp.Tag = item
                editor.Dock = DockStyle.Fill
                TabControl1.SelectedTab = tp
                AddHandler item.UnsavedContentChanged, Sub()
                                                           If item.UnsavedContent Is Nothing And tp.Text.EndsWith("*") Then
                                                               tp.Text = item.Name
                                                           End If
                                                           If item.UnsavedContent IsNot Nothing And tp.Text.EndsWith("*") = False Then
                                                               tp.Text = item.Name + " *"
                                                           End If
                                                       End Sub
                existingPage = tp
            Catch ex As Exception
                MsgBox("Read file error: " + item.Name + " " + ex.Message, MsgBoxStyle.Critical)
            End Try
        End If

        If existingPage IsNot Nothing And line > 0 Then
            Dim editor As FilesEditor = existingPage.Controls(0)
            editor.SetCursor(line, column)
            ' tp.scroll
        End If
    End Sub

    Public Sub CloseAllTabPages()
        For Each tp In TabControl1.TabPages
            CloseTabPage(tp)
        Next
    End Sub

    Public Sub CloseTabPage(tp As TabPage)
        Dim solItem As SolutionItem = tp.Tag
        If solItem.AskIfUnsaved = DialogResult.OK Then
            TabControl1.TabPages.Remove(tp)
            tp.Dispose()
        End If
    End Sub

    Public Function FindPageForItem(filePath As String) As TabPage
        For Each tp As TabPage In TabControl1.TabPages
            If tp.Tag.FullPath.tolower = filePath.ToLower Then
                Return tp
            End If
        Next
        Return Nothing
    End Function

    Public Function SelectedSolutionItem() As SolutionItem
        If TabControl1.SelectedTab Is Nothing Then Return Nothing
        Dim solItem As SolutionItem = TabControl1.SelectedTab.Tag
        Return solItem
    End Function

    Private Sub FilesEditorCollection_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TabControl1_DoubleClick(sender As Object, e As EventArgs) Handles TabControl1.DoubleClick

    End Sub

    Private Sub TabControl1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles TabControl1.MouseDoubleClick
        For i = 0 To TabControl1.TabCount
            If TabControl1.GetTabRect(i).Contains(e.Location) Then
                CloseTabPage(TabControl1.TabPages(i))
                Return
            End If
        Next
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged

    End Sub
End Class
