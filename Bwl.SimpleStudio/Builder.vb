Public Class Builder
    Private _errorsList As ErrorsList

    Public Sub New(errorsList As ErrorsList)
        _errorsList = errorsList
    End Sub

    Public Function BuildAll(root As RootSolutionItem, configuration As String) As BuildTask
        Dim task As New BuildTask(root.SolutionsList(0).FullPath, configuration)
        _errorsList.AssociatedBuildTask = task
        task.ForceRebuild = True
        task.Build("")
        Return task
    End Function
End Class
