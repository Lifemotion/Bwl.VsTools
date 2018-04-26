Public Class ExecutableTarget
    Public Property ProjectItem As SolutionItem
    Public Property RelativePath As String
    Public Property Configuration As String
    Public Property Condition As String
    Public Property FullPath As String
    Public Property Built As Boolean

    Public Overrides Function ToString() As String
        Return System.IO.Path.GetFileNameWithoutExtension(ProjectItem.Name) + " (" + RelativePath + ")"
    End Function
End Class
