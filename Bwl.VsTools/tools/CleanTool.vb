Public Class CleanTool
    Public Shared Sub GitClean(dir As String)
        Dim prc As New Process
        prc.StartInfo.CreateNoWindow = False
        prc.StartInfo.RedirectStandardOutput = False
        prc.StartInfo.RedirectStandardError = False
        prc.StartInfo.FileName = "git"
        prc.StartInfo.Arguments = "clean"
        prc.StartInfo.WorkingDirectory = dir
        prc.StartInfo.UseShellExecute = False
        Try
            prc.Start()
            Console.WriteLine("Git clean ok")
        Catch ex As Exception
            Console.WriteLine("Git clean error: " + ex.Message)
        End Try
    End Sub
End Class
