Public Class RootSolutionItem
    Inherits SolutionItem

    Public Property ExecutableTargets As New List(Of ExecutableTarget)

    Public Sub New(path As String)
        MyBase.New(path)
    End Sub

    Public ReadOnly Property SolutionsList As SolutionItem()
        Get
            If Extension = ".sln" Then
                Return {Me}
            Else
                Throw New Exception
            End If
        End Get
    End Property
End Class

Public Class SolutionItem
    Public ReadOnly Property FullPath As String
    Public ReadOnly Property Name As String
    Public ReadOnly Property Extension As String
    Public ReadOnly Property IsDirectory As Boolean
    Public ReadOnly Property Childs As New List(Of SolutionItem)

    Public Event UnsavedContentChanged(source As SolutionItem)

    Private _unsavedContent As String
    Private _di As IO.DirectoryInfo
    Private _fi As IO.FileSystemInfo

    Public Property UnsavedContent As String
        Get
            Return _unsavedContent
        End Get
        Set(value As String)
            _unsavedContent = value
            RaiseEvent UnsavedContentChanged(Me)
        End Set
    End Property

    Public Sub Save()
        If _unsavedContent IsNot Nothing Then
            IO.File.WriteAllText(FullPath, _unsavedContent)
            UnsavedContent = Nothing
        End If
    End Sub

    Public Function FindUnsavedItems() As SolutionItem()
        Dim list As New List(Of SolutionItem)
        FindUnsavedItemsRecursive(list, Me)
        Return list.ToArray
    End Function

    Private Sub FindUnsavedItemsRecursive(List As List(Of SolutionItem), item As SolutionItem)
        If item.UnsavedContent IsNot Nothing Then List.Add(item)
        For Each child In item.Childs
            FindUnsavedItemsRecursive(List, child)
        Next
    End Sub

    Public Sub SaveAll()
        Dim unsaved = FindUnsavedItems()
        For Each file In unsaved
            file.Save()
        Next
    End Sub

    Public Function SaveAllWithAsk() As DialogResult
        Dim unsaved = FindUnsavedItems()
        If unsaved.Length > 0 Then
            Select Case MsgBox("There are unsaved files. Save all changes?", MsgBoxStyle.YesNoCancel)
                Case MsgBoxResult.Cancel
                    Return DialogResult.Cancel
                Case MsgBoxResult.Yes
                    Try
                        For Each file In unsaved
                            file.Save()
                        Next
                        Return DialogResult.OK
                    Catch ex As Exception
                        MsgBox("Save file error: " + Name + " " + ex.Message, MsgBoxStyle.Critical)
                        Return DialogResult.Cancel
                    End Try
                Case MsgBoxResult.No
                    Return DialogResult.OK
                Case Else
                    Return DialogResult.Cancel
            End Select
        Else
            Return DialogResult.OK
        End If
    End Function

    Public Function AskIfUnsaved() As DialogResult
        If UnsavedContent IsNot Nothing Then
            Dim result = MsgBox("File " + Name + " was changed. Save changes?", MsgBoxStyle.YesNoCancel)
            Select Case result
                Case MsgBoxResult.Yes
                    Try
                        Save()
                        Return DialogResult.OK
                    Catch ex As Exception
                        MsgBox("Save file error: " + Name + " " + ex.Message, MsgBoxStyle.Critical)
                        Return DialogResult.Cancel
                    End Try
                Case MsgBoxResult.No
                    UnsavedContent = Nothing
                    Return DialogResult.OK
                Case MsgBoxResult.Cancel
                    Return DialogResult.Cancel
                Case Else
                    Return DialogResult.Cancel
            End Select
        Else
            Return DialogResult.OK
        End If
    End Function

    Public Function FindItemByPath(path As String) As SolutionItem
        Return FindItemByPathRecursive(Me, path)
    End Function

    Private Function FindItemByPathRecursive(item As SolutionItem, path As String) As SolutionItem
        If item.FullPath = path Then Return item
        For Each child In item.Childs
            Dim result = FindItemByPathRecursive(child, path)
            If result IsNot Nothing Then Return result
        Next
        Return Nothing
    End Function

    Public Function Load() As String
        Dim text = IO.File.ReadAllText(FullPath)
        Return text
    End Function



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

    Private Shared Function LoadSolution(rootPath As String) As RootSolutionItem
        'path is sln
        Dim root As New RootSolutionItem(rootPath)
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
                        Dim isExecutable = False
                        Dim assemblyName As String = ""
                        Dim temporaryTargets As New List(Of ExecutableTarget)
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
                            If group.Name = "PropertyGroup" Then
                                Dim condition = ""
                                For Each attr As Xml.XmlAttribute In group.Attributes
                                    If attr.Name = "Condition" Then condition = attr.Value
                                Next

                                For Each item As Xml.XmlNode In group.ChildNodes
                                    If item.Name = "OutputType" AndAlso item.InnerText = "WinExe" Then
                                        isExecutable = True
                                    End If
                                    If item.Name = "AssemblyName" Then
                                        assemblyName = item.InnerText
                                    End If
                                    If item.Name = "OutputPath" Then
                                        Dim target As New ExecutableTarget
                                        target.ProjectItem = prj
                                        target.RelativePath = item.InnerText
                                        target.Condition = condition
                                        temporaryTargets.Add(target)
                                    End If
                                Next
                            End If
                        Next
                        ProcessDirectory(prjFolder, prj, fileList)
                        If isExecutable Then
                            For Each tempTarget In temporaryTargets
                                tempTarget.RelativePath = IO.Path.Combine(tempTarget.RelativePath, assemblyName + ".exe")
                                tempTarget.Configuration = ""
                                tempTarget.FullPath = IO.Path.Combine(prjFolder, tempTarget.RelativePath)
                                root.ExecutableTargets.Add(tempTarget)
                            Next
                        End If
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
            Return If(UnsavedContent IsNot Nothing, "* ", "") + Name
        End If
    End Function
End Class
