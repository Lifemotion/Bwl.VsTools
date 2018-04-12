Public Class SolutionItem
    Public ReadOnly Property FullPath As String
    Public ReadOnly Property Name As String
    Public ReadOnly Property Extension As String
    Public ReadOnly Property IsDirectory As Boolean
    Public ReadOnly Property IsChanged As Boolean
    Public ReadOnly Property Childs As New List(Of SolutionItem)

    Private _di As IO.DirectoryInfo
    Private _fi As IO.FileSystemInfo

    Public Sub New(path As String)
        If IO.Directory.Exists(path) Then
            _di = New IO.DirectoryInfo(path)
            IsDirectory = True
            Name = _di.Name
            Extension = _di.Extension.ToLower
            FullPath = _di.FullName
        ElseIf IO.File.Exists(path) Then
            _fi = New IO.FileInfo(path)
            IsDirectory = False
            Name = _fi.Name
            Extension = _fi.Extension.ToLower
            FullPath = _fi.FullName
        Else
            Throw New Exception("File or directory not found")
        End If
    End Sub

    Public Function GetFiles() As IO.FileInfo()
        If Not IsDirectory Then Throw New Exception("Not a directory")
        Return _di.GetFiles
    End Function

    Public Function GetDirectories() As IO.DirectoryInfo()
        If Not IsDirectory Then Throw New Exception("Not a directory")
        Return _di.GetDirectories
    End Function

    Private Shared Function LoadSolution(rootPath As String) As SolutionItem
        'path is sln
        Dim root As New SolutionItem(rootPath)
        Dim rootLines = IO.File.ReadAllLines(root.FullPath)
        For Each line In rootLines
            If line.StartsWith("Project(""") Then
                Dim parts = line.Split(",")
                If parts.Length > 1 Then
                    Dim prjFile = IO.Path.Combine(IO.Path.GetDirectoryName(rootPath), parts(1).Replace("""", "").Trim)
                    If IO.File.Exists(prjFile) Then
                        Dim prj As New SolutionItem(prjFile)
                        Dim prjFolder = IO.Path.GetDirectoryName(prj.FullPath)
                        root.Childs.Add(prj)
                        Dim xmldoc As New Xml.XmlDocument
                        Dim fileList As New List(Of String)
                        xmldoc.Load(prjFile)
                        For Each group As Xml.XmlNode In xmldoc.DocumentElement.ChildNodes
                            If group.Name = "ItemGroup" Then
                                For Each item As Xml.XmlNode In group.ChildNodes
                                    If item.Name = "Compile" Then
                                        For Each attr As Xml.XmlAttribute In item.Attributes
                                            If attr.Name = "Include" Then
                                                Dim filePath = IO.Path.Combine(prjFolder, attr.Value)
                                                fileList.Add(filePath)
                                            End If
                                        Next
                                    End If
                                Next
                            End If
                        Next
                        ProcessDirectory(prjFolder, prj, fileList)
                    End If
                End If
            End If
        Next
        Return root
    End Function

    Private Shared Sub ProcessDirectory(path As String, root As SolutionItem, prjFileList As IEnumerable(Of String))
        For Each childDir In IO.Directory.GetDirectories(path)
            Dim childDirNode As New SolutionItem(childDir)
            ProcessDirectory(childDir, childDirNode, prjFileList)
            If childDirNode.Childs.Count > 0 Then
                root.Childs.Add(childDirNode)
            End If
        Next
        For Each childFile In IO.Directory.GetFiles(path)
            If prjFileList.Contains(childFile) Then
                Dim childFileNode As New SolutionItem(childFile)
                root.Childs.Add(childFileNode)
            End If
        Next
    End Sub

    Public Shared Function CreateSolutionTree(rootPath As String) As SolutionItem
        If IO.File.Exists(rootPath) Then
            Return LoadSolution(rootPath)
        End If
        Throw New Exception
    End Function

    Public Overrides Function ToString() As String
        If IsDirectory Then
            Return "[" + Name + "]"
        Else
            Return If(IsChanged, "* ", "") + Name
        End If
    End Function
End Class
