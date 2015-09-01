Module VersionTool
    Sub Main()
        Console.WriteLine("Bwl VS Version Tool, " + My.Application.Info.Version.ToString)
        Console.WriteLine("")
        Dim dir = IO.Directory.GetCurrentDirectory
        Dim gitCommit = GetGitVersion(dir)
        Dim dateVersion = "1." + Now.ToString("yyyy") + "." + Now.ToString("MMdd") + "." + Now.ToString("HHmm")
        Console.WriteLine("Git commit: " + gitCommit)
        Console.WriteLine("Date-based version: " + dateVersion)
        Console.WriteLine("")

        Dim txtInfos = IO.Directory.GetFiles(dir, "AssemblyInfo.*.txt", IO.SearchOption.AllDirectories)
        For Each txt In txtInfos
            Dim originalName = IO.Path.Combine(IO.Path.GetDirectoryName(txt), IO.Path.GetFileNameWithoutExtension(txt))
            If IO.File.Exists(originalName) = False Then
                Console.WriteLine("AssemblyInfo not found, created from backup")
                IO.File.Copy(txt, originalName)
            End If
        Next

        Dim csInfos = IO.Directory.GetFiles(dir, "AssemblyInfo.cs", IO.SearchOption.AllDirectories)
        For Each info In csInfos
            Console.WriteLine(info.Replace(dir, ""))
            ProcessInfoFile(info, gitCommit, dateVersion)
        Next

        Dim vbInfos = IO.Directory.GetFiles(dir, "AssemblyInfo.vb", IO.SearchOption.AllDirectories)
        For Each info In vbInfos
            Console.WriteLine(info.Replace(dir, ""))
            ProcessInfoFile(info, gitCommit, dateVersion)
        Next
    End Sub

    Private Function ProcessLine(ByRef line As String, description As String, version As String) As Boolean
        If line.Length > 0 AndAlso line(0) = "<" Then
            If line.ToLower.Contains("<Assembly: AssemblyDescription(".ToLower) Then
                line = "<Assembly: AssemblyDescription(""" + description + """)>"
                Return True
            End If
            If line.ToLower.Contains("<Assembly: AssemblyVersion(".ToLower) Then
                line = "<Assembly: AssemblyVersion(""" + version + """)>"
                Return True
            End If
            If line.ToLower.Contains("<Assembly: AssemblyFileVersion(".ToLower) Then
                line = "<Assembly: AssemblyFileVersion(""" + version + """)>"
                Return True
            End If
        End If

        If line.Length > 0 AndAlso line(0) = "[" Then
            If line.ToLower.Contains("[assembly: AssemblyDescription(".ToLower) Then
                line = "[assembly: AssemblyDescription(""" + description + """)]"
                Return True
            End If
            If line.ToLower.Contains("[assembly: AssemblyVersion(".ToLower) Then
                line = "[assembly: AssemblyVersion(""" + version + """)]"
                Return True
            End If
            If line.ToLower.Contains("<Assembly: AssemblyFileVersion(".ToLower) Then
                line = "[assembly: AssemblyFileVersion(""" + version + """)]"
                Return True
            End If
            Return False
        End If
        Return False
    End Function

    Private Sub ProcessInfoFile(file As String, description As String, version As String)
        Dim workingLines = IO.File.ReadAllLines(file, Text.Encoding.UTF8)
        Dim copyLines As String() = {}
        Try
            copyLines = IO.File.ReadAllLines(file + ".txt", Text.Encoding.UTF8)
        Catch ex As Exception
        End Try

        Dim workingLinesToWrite As New List(Of String)
        Dim workingLinesWithoutChanging As New List(Of String)
        Dim copyLinesWithoutChanging As New List(Of String)

        For Each line In workingLines
            If Not ProcessLine(line, description, version) Then workingLinesWithoutChanging.Add(line)
            workingLinesToWrite.Add(line)
        Next

        For Each line In copyLines
            If Not ProcessLine(line, description, version) Then copyLinesWithoutChanging.Add(line)
        Next

        IO.File.WriteAllLines(file, workingLinesToWrite, Text.Encoding.UTF8)

        If copyLinesWithoutChanging.Count <> workingLinesWithoutChanging.Count Then
            IO.File.WriteAllLines(file + ".txt", workingLinesToWrite, Text.Encoding.UTF8)
            Console.WriteLine("File changed, new copy created")
        Else
            For i = 0 To copyLinesWithoutChanging.Count - 1
                If copyLinesWithoutChanging(i) <> workingLinesWithoutChanging(i) Then
                    IO.File.WriteAllLines(file + ".txt", workingLinesToWrite, Text.Encoding.UTF8)
                    Console.WriteLine("File changed, new copy created")
                    Exit For
                End If
            Next
        End If
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
