Module App
    Public Sub Main()
        Dim dir = IO.Directory.GetCurrentDirectory
        VersionTool.SetVersion(dir)
    End Sub
End Module
