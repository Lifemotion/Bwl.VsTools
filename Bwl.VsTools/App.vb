Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Module App

    <STAThread>
    Sub Main()
        Dim cmd = Command()
        Dim dir As String = IO.Directory.GetCurrentDirectory

        If cmd = "" Then
            Application.Run(New VsToolsForm)
        Else
            Dim parts = cmd.Split(",")
            For Each part In parts
                part = part.Trim
                If part.StartsWith("!build") Then
                    part = part.Replace("!build", "")
                    Dim bt As New BuildTool
                    Dim filters = part.Split(" ")
                    For Each flt In filters
                        If flt.Trim.ToLower = "-m" Then bt.AdditionalBuildOptions += "-m "
                    Next
                    bt.BuildAll(filters)
                End If
                If part.StartsWith("!git-clean") Then
                    part = part.Replace("!git clean", "")
                    CleanTool.GitClean(dir)
                End If
                If part.StartsWith("!set-version") Then
                    VersionTool.SetVersion(dir)
                End If
                If part.StartsWith("!check-vs-projects") Then
                    ProjectCheck.CheckProjects(dir)
                End If
                If part.StartsWith("!rename") Then
                    Dim folder = dir '-f
                    Dim ignoredFolders = "refs, docs, bin, obj, .git" '-i
                    Dim oldProjectName = "" '-o
                    Dim newProjectName = "" '-n
                    Dim ignoredFileExtensions = "ico, bmp, png, dll, exe" '-e
                    Dim possibleOptions = New String() {"-f", "-i", "-o", "-n", "-e"}

                    part = part.Replace("!rename", "")

                    Dim filters = Regex.Matches(part, "[\""].+?[\""]|[^ ]+").Cast(Of Match)().[Select](Function(m) m.Value).ToArray()
                    
                    For i = 0 To filters.Count-1
                        Dim flt = filters(i)
                        If possibleOptions.Contains(flt.Trim.ToLower) AndAlso i < filters.Count - 1 Then

                            Select Case flt
                                Case possibleOptions(0)
                                    folder = filters(i + 1).Replace("""","")
                                Case possibleOptions(1)
                                    ignoredFolders = filters(i + 1).Replace("""","")
                                Case possibleOptions(2)
                                    oldProjectName = filters(i + 1).Replace("""","")
                                Case possibleOptions(3)
                                    newProjectName = filters(i + 1).Replace("""","")
                                Case possibleOptions(4)
                                    ignoredFileExtensions = filters(i + 1).Replace("""","")
                            End Select
                            i += 1
                        End If
                    Next

                    If String.IsNullOrWhiteSpace(oldProjectName) OrElse String.IsNullOrWhiteSpace(newProjectName) Then
                        Console.WriteLine("ERROR: You did not specified a project name. Project rename will be cancelled.")
                    Else
                        ProjectRenamer.ProcessFolder(folder, ignoredFolders, oldProjectName, newProjectName, ignoredFileExtensions)
                    End If

                End If
            Next

        End If
    End Sub

End Module
