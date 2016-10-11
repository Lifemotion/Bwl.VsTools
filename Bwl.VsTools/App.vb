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
            Next

        End If
    End Sub

End Module
