Module MsBuilder
    Dim path = "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.EXE"
    Dim myp = AppDomain.CurrentDomain.BaseDirectory

    Public Function MsBuild(prj As String) As Boolean
        Dim prc As New Process()
        prc.StartInfo.WorkingDirectory = myp
        prc.StartInfo.FileName = path
        prc.StartInfo.UseShellExecute = False
        prc.StartInfo.RedirectStandardOutput = True
        prc.StartInfo.Arguments = """" + prj + """"
        prc.Start()
        Do While prc.StandardOutput.EndOfStream = False
            Dim p = prc.StandardOutput.ReadLine
            Console.WriteLine(p)
        Loop
        If prc.ExitCode = 0 Then Return True
        Return False
    End Function

    Sub Main()
        Dim i = 1
        Dim sln As String()
        Dim flag = True
        Do
            sln = IO.Directory.GetFiles(myp, "*_B" + i.ToString + "*.sln")
            If sln.Length = 1 Then flag = flag And MsBuild(sln(0))
            i += 1
        Loop While sln.Length = 1
        If i < 3 Then
            sln = IO.Directory.GetFiles(myp, "*.sln")
            If sln.Length = 1 Then flag = flag And MsBuild(sln(0))
            i += 1
        End If

        Console.WriteLine()
        If flag = False Then
            Console.ForegroundColor = ConsoleColor.Red
            Console.WriteLine("Errors!")
        Else
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine("Ok!")
        End If

        Console.ReadLine()
    End Sub

End Module
