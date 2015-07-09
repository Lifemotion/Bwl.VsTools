Imports System.Windows.Forms

Class BuildTask
    Dim toolPath = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.EXE"

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

    Public Function Build() As Boolean
        Dim prc As New Process()
        prc.StartInfo.WorkingDirectory = ProjectDirectory
        prc.StartInfo.FileName = toolPath
        prc.StartInfo.UseShellExecute = False
        prc.StartInfo.RedirectStandardOutput = True
        prc.StartInfo.Arguments = """" + ProjectFile + """ /p:Configuration=" + Configuration
        prc.Start()
        Do While prc.StandardOutput.EndOfStream = False
            Dim p = prc.StandardOutput.ReadLine
            Console.WriteLine(p)
        Loop
        Success = (prc.ExitCode = 0)
        Return Success
    End Function
End Class

Module MsBuilder
    Dim dir = IO.Directory.GetCurrentDirectory
    Dim list As New List(Of BuildTask)

    Function GetSolutionsWithOrderMarks(conf As String) As BuildTask()
        Dim list As New List(Of BuildTask)
        Dim i = 1
        Dim sln As String()
        Dim flag = True
        Do
            sln = IO.Directory.GetFiles(dir, "*_B" + i.ToString + "*.sln")
            If sln.Length = 1 Then list.Add(New BuildTask(sln(0), conf))
            i += 1
        Loop While sln.Length = 1
        Return list.ToArray
    End Function

    Function GetAllSolutions(conf As String) As BuildTask()
        Dim list As New List(Of BuildTask)
        Dim sln = IO.Directory.GetFiles(dir, "*.sln")
        For Each sol In sln
            list.Add(New BuildTask(sol, conf))
        Next
        Return list.ToArray
    End Function

    Sub AddSolutionsToBuildList(conf As String)
        Dim tasks = GetSolutionsWithOrderMarks(conf)
        If tasks.Length = 0 Then tasks = GetAllSolutions(conf)
        list.AddRange(tasks)
    End Sub

    Sub BuildAll()
        AddSolutionsToBuildList("Debug")
        AddSolutionsToBuildList("Release")

        If list.Count = 0 Then
            Console.WriteLine("Nothing to build!")
        Else
            Dim success = 0
            For Each task In list
                If task.Build() Then success += 1
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

    Sub Main()
        Console.WriteLine("Bwl VS Build Tool, " + My.Application.Info.Version.ToString)
        Console.WriteLine("")
        BuildAll()
    End Sub

End Module
