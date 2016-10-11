Imports System.Windows.Forms

Public Class BuildTask
    Dim toolPaths As String() = {
        "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.EXE",
        "C:\Program Files\MSBuild\14.0\Bin\MSBuild.EXE",
        "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.EXE",
        "C:\Program Files\MSBuild\12.0\Bin\MSBuild.EXE",
        "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.EXE",
        "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.EXE"}
    Public Property ProjectFile As String
    Public Property ProjectDirectory As String
    Public Property ProjectName As String
    Public Property Configuration As String
    Public Property Success As Boolean
    Public Overrides Function ToString() As String
        Return ProjectName + " (" + Configuration + ")"
    End Function
    Public Sub New(path As String, conf As String)
        If IO.File.Exists(path) = False Then Throw New ArgumentOutOfRangeException
        ProjectFile = path
        ProjectName = IO.Path.GetFileNameWithoutExtension(path)
        ProjectDirectory = IO.Path.GetDirectoryName(path)
        Configuration = conf
        Success = False
    End Sub

    Private Function FindTools() As String
        For Each toolPath In toolPaths
            If IO.File.Exists(toolPath) Then
                Return toolPath
            End If
        Next
        Console.BackgroundColor = ConsoleColor.Red
        Console.WriteLine("Build tools not found!")
        Throw New Exception("Build tools not found!")
    End Function

    Public Function Build(additionalOptions As String) As Boolean
        Dim prc As New Process()
        prc.StartInfo.WorkingDirectory = ProjectDirectory
        prc.StartInfo.FileName = FindTools()
        prc.StartInfo.UseShellExecute = False
        prc.StartInfo.RedirectStandardOutput = True
        prc.StartInfo.Arguments = """" + ProjectFile + """ /p:Configuration=" + Configuration + additionalOptions
        prc.Start()
        Do While prc.StandardOutput.EndOfStream = False
            Dim p = prc.StandardOutput.ReadLine
            Console.WriteLine(p)
        Loop
        Success = (prc.ExitCode = 0)
        Return Success
    End Function
End Class

Public Class BuildTool
    Dim list As New List(Of BuildTask)

    Public Property AdditionalBuildOptions As String = " "

    Public Shared Function GetSolutionsWithOrderMarks(path As String, conf As String) As BuildTask()
        Dim list As New List(Of BuildTask)
        Dim i = 1
        Dim sln As String()
        Dim flag = True
        Do
            sln = IO.Directory.GetFiles(path, "*_B" + i.ToString + "*.sln")
            If sln.Length = 1 Then list.Add(New BuildTask(sln(0), conf))
            i += 1
        Loop While sln.Length = 1
        Return list.ToArray
    End Function

    Public Shared Function GetAllSolutions(path As String, conf As String, filters As String()) As BuildTask()
        Dim list As New List(Of BuildTask)
        Dim sln = IO.Directory.GetFiles(path, "*.sln")
        For Each sol In sln
            Dim fi = New IO.FileInfo(sol)
            For Each flt In filters
                If flt > "" Then
                    If flt = "*" Then
                        list.Add(New BuildTask(sol, conf))
                    ElseIf fi.Name.ToLower.Contains(flt.ToLower) Then
                        list.Add(New BuildTask(sol, conf))
                    End If
                End If
            Next
        Next
        Return list.ToArray
    End Function

    Public Sub AddSolutionsToBuildList(conf As String, filters As String())
        Dim path = IO.Directory.GetCurrentDirectory
        Dim tasks = GetSolutionsWithOrderMarks(path, conf)
        If tasks.Length = 0 Then tasks = GetAllSolutions(path, conf, filters)
        If tasks.Length = 0 Then tasks = GetSolutionsWithOrderMarks(IO.Path.Combine(path, ".."), conf)
        If tasks.Length = 0 Then tasks = GetAllSolutions(IO.Path.Combine(path, ".."), conf, filters)
        list.AddRange(tasks)
    End Sub

    Public Sub BuildAll(filters As String())
        For Each fi In filters
            If fi.ToLower = "-debug" Then AddSolutionsToBuildList("Debug", filters)
            If fi.ToLower = "-release" Then AddSolutionsToBuildList("Release", filters)
        Next
        Build()
    End Sub

    Public Sub Build()
        Console.WriteLine("Bwl VS Build Tool, " + My.Application.Info.Version.ToString)
        Console.WriteLine("")

        If list.Count = 0 Then
            Console.WriteLine("Nothing to build!")
        Else
            Dim success = 0
            For Each task In list
                If task.Build(AdditionalBuildOptions) Then success += 1
            Next
            Console.WriteLine()
            For Each task In list
                If task.Success Then Console.ForegroundColor = ConsoleColor.White : Console.WriteLine(task.ToString + " - ok")
                If task.Success = False Then Console.ForegroundColor = ConsoleColor.Magenta : Console.WriteLine(task.ToString + " - error")
            Next
            Console.WriteLine()
            If success <> list.Count Then
                Console.ForegroundColor = ConsoleColor.Red
                Console.WriteLine("Error! " + (list.Count - success).ToString + " from " + list.Count.ToString + " build failed")
            Else
                Console.ForegroundColor = ConsoleColor.Green
                Console.WriteLine("Ok! " + list.Count.ToString + " solutions built")
            End If
        End If

        Console.ForegroundColor = ConsoleColor.Gray
    End Sub

End Class
