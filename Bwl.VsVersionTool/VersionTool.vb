Module VersionTool
    Sub Main()
        Console.WriteLine("Bwl VS Version Tool, " + My.Application.Info.Version.ToString)
        Console.WriteLine("")
        Dim dir = IO.Directory.GetCurrentDirectory
        Dim gitCommit = GetGitVersion(dir)
        Dim dateVersion = "1." + Now.ToString("yyyy") + "." + Now.ToString("MMdd") + "." + Now.ToString("hhmm")
        Console.WriteLine("Git commit: " + gitCommit)
        Console.WriteLine("Date-based version: " + dateVersion)
        Console.WriteLine("")

        Dim csInfos = IO.Directory.GetFiles(dir, "AssemblyInfo.cs", IO.SearchOption.AllDirectories)
        For Each info In csInfos
            Console.WriteLine(info.Replace(dir, ""))
            ProcessCsInfo(info, gitCommit, dateVersion)
        Next

        Dim vbInfos = IO.Directory.GetFiles(dir, "AssemblyInfo.vb", IO.SearchOption.AllDirectories)
        For Each info In vbInfos
            Console.WriteLine(info.Replace(dir, ""))
            ProcessVbInfo(info, gitCommit, dateVersion)
        Next
    End Sub

    Private Sub ProcessCsInfo(file As String, description As String, version As String)
        Dim lines = IO.File.ReadAllLines(file, Text.Encoding.UTF8)
        Dim newLines As New List(Of String)
        For Each line In lines
            If line.Length > 0 AndAlso line(0) = "[" Then
                If line.ToLower.Contains("[assembly: AssemblyDescription(".ToLower) Then
                    line = "[assembly: AssemblyDescription(""" + description + """)]"
                End If
                If line.ToLower.Contains("[assembly: AssemblyVersion(".ToLower) Then
                    line = "[assembly: AssemblyVersion(""" + version + """)]"
                End If
                If line.ToLower.Contains("<Assembly: AssemblyFileVersion(".ToLower) Then
                    line = "[assembly: AssemblyFileVersion(""" + version + """)]"
                End If
            End If
            newLines.Add(line)
        Next
        IO.File.WriteAllLines(file, newLines, Text.Encoding.UTF8)
    End Sub

    Private Sub ProcessVbInfo(file As String, description As String, version As String)
        Dim lines = IO.File.ReadAllLines(file, Text.Encoding.UTF8)
        Dim newLines As New List(Of String)
        For Each line In lines
            If line.Length > 0 AndAlso line(0) = "<" Then
                If line.ToLower.Contains("<Assembly: AssemblyDescription(".ToLower) Then
                    line = "<Assembly: AssemblyDescription(""" + description + """)>"
                End If
                If line.ToLower.Contains("<Assembly: AssemblyVersion(".ToLower) Then
                    line = "<Assembly: AssemblyVersion(""" + version + """)>"
                End If
                If line.ToLower.Contains("<Assembly: AssemblyFileVersion(".ToLower) Then
                    line = "<Assembly: AssemblyFileVersion(""" + version + """)>"
                End If
            End If
            newLines.Add(line)
        Next
        IO.File.WriteAllLines(file, newLines, Text.Encoding.UTF8)
    End Sub

    Private Function GetGitVersion(dir As String) As String
        Dim prc As New Process
        prc.StartInfo.CreateNoWindow = True
        prc.StartInfo.RedirectStandardOutput = True
        prc.StartInfo.RedirectStandardError = True
        prc.StartInfo.StandardOutputEncoding = Text.Encoding.UTF8
        prc.StartInfo.FileName = "git"
        prc.StartInfo.Arguments = "log --oneline -1"
        prc.StartInfo.WorkingDirectory = dir
        prc.StartInfo.UseShellExecute = False
        Try
            prc.Start()
            Dim result1 = prc.StandardOutput.ReadToEnd
            Dim result2 = prc.StandardError.ReadToEnd
            prc.WaitForExit()
            Dim result = result1 + result2
            result = result.Replace(vbCr, "").Replace(vbLf, "")
            Return result
        Catch ex As Exception
            Return ""
        End Try
    End Function
End Module
