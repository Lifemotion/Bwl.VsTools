Public Class GitignoreTools
    Public Shared ReadOnly Property GitignoreFile = ".gitignore"

    Public Shared Sub CreateIfNotExist(dir As String)
        If IO.File.Exists(IO.Path.Combine(dir, GitignoreFile)) = False Then
            IO.File.WriteAllText(GitignoreFile, "", System.Text.Encoding.UTF8)
        End If
    End Sub

    Public Shared Function ReadLines(dir As String) As String()
        CreateIfNotExist(dir)
        Dim lines = IO.File.ReadAllLines(IO.Path.Combine(dir, GitignoreFile), System.Text.Encoding.UTF8)
        Return lines
    End Function

    Public Shared Sub WriteLines(dir As String, lines As String())
        CreateIfNotExist(dir)
        IO.File.WriteAllLines(IO.Path.Combine(dir, GitignoreFile), lines, System.Text.Encoding.UTF8)
    End Sub

    Public Shared Sub Open(dir As String)
        CreateIfNotExist(dir)
        Shell("notepad """ + IO.Path.Combine(dir, GitignoreFile) + """", AppWinStyle.NormalFocus)
    End Sub

    Public Shared Sub AddStandardItems(dir As String)
        Dim listToAdd As New List(Of String)
        Dim current = ReadLines(dir)
        If Not current.Contains("bin") Then listToAdd.Add("bin")
        If Not current.Contains("bin") Then listToAdd.Add("bin")
        If Not current.Contains("obj") Then listToAdd.Add("obj")
        If Not current.Contains("debug") Then listToAdd.Add("debug")
        If Not current.Contains("Debug") Then listToAdd.Add("Debug")
        If Not current.Contains("release") Then listToAdd.Add("release")
        If Not current.Contains("Release") Then listToAdd.Add("Release")
        If Not current.Contains("output") Then listToAdd.Add("output")
        If Not current.Contains("logs") Then listToAdd.Add("logs")
        If Not current.Contains("archive.zip") Then listToAdd.Add("archive.zip")
        If Not current.Contains(".vs") Then listToAdd.Add(".vs")
        If Not current.Contains("*.suo") Then listToAdd.Add("*.suo")
        If Not current.Contains("*.atsuo") Then listToAdd.Add("*.atsuo")
        If Not current.Contains("*.psess") Then listToAdd.Add("*.psess")
        If Not current.Contains("*.vspx") Then listToAdd.Add("*.vspx")
        If Not current.Contains("*.bak") Then listToAdd.Add("*.bak")
        If Not current.Contains("*.zip1") Then listToAdd.Add("*.zip1")
        If Not current.Contains("~*") Then listToAdd.Add("~*")
        If listToAdd.Count > 0 Then
            listToAdd.AddRange(current)
            WriteLines(dir, listToAdd.ToArray)
        End If
    End Sub
End Class
