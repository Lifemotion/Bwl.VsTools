Public Class ExecutableTarget
    Public Property ProjectItem As SolutionItem
    Public Property RelativePath As String
    Public Property Configuration As String
    Public Property FullPath As String
    Public Property Built As Boolean

    Public Overrides Function ToString() As String
        Return ProjectItem.Name
    End Function
End Class
