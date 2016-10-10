Imports System.Windows.Forms

Module App
    Sub Main()
        Dim cmd = Command()
        If cmd > "" Then
            Dim bt As New BuildTool
            Dim filters = cmd.Split(" ")
            For Each part In filters
                If part.Trim.ToLower = "-m" Then bt.AdditionalBuildOptions += "-m "
            Next
            bt.BuildAll(filters)
        Else
            Console.WriteLine("Using [-debug] [-release] (*|sln-file-mask)")
        End If
    End Sub

End Module
