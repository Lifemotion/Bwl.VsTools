Public Enum BuildMessageType
    Information
    Debug
    BuildError
    BuildWarning
End Enum
Public Class BuildTask
    Public Property ProjectFile As String
    Public Property ProjectDirectory As String
    Public Property ProjectName As String
    Public Property Configuration As String
    Public Property Success As Boolean
    Public Property LastBuildOutputLines As New List(Of String)
    Public Property LastBuildMessages As New List(Of BuildMessage)
    Public Property ForceRebuild As Boolean

    Public Event Logger(type As String, msg As String)
    Public Event BuildFinished(source As BuildTask)
    Public Event BuildStarted(source As BuildTask)

    Public Overrides Function ToString() As String
        Return ProjectName + " (" + Configuration + ")"
    End Function

    Public Sub New(solutionPath As String, configuration As String)
        If IO.File.Exists(solutionPath) = False Then Throw New ArgumentOutOfRangeException
        ProjectFile = solutionPath
        ProjectName = IO.Path.GetFileNameWithoutExtension(solutionPath)
        ProjectDirectory = IO.Path.GetDirectoryName(solutionPath)
        Me.Configuration = configuration
        Success = False
    End Sub

    Private Function FindTools() As String
        MSBuild.Rescan()
        If MSBuild.MSBuildPath > "" Then Return MSBuild.MSBuildPath
        RaiseEvent Logger("ERR", "Build tools not found!")
        Throw New Exception("Build tools not found!")
    End Function

    Public Function Build(additionalOptions As String) As Boolean
        RaiseEvent BuildStarted(Me)
        LastBuildOutputLines.Clear()
        LastBuildMessages.Clear()
        Dim prc As New Process()
        prc.StartInfo.WorkingDirectory = ProjectDirectory
        prc.StartInfo.FileName = FindTools()
        prc.StartInfo.UseShellExecute = False
        prc.StartInfo.RedirectStandardOutput = True
        prc.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding(1251)
        'prc.StartInfo.StandardOutputEncoding = System.Text.Encoding.GetEncoding(850)
        prc.StartInfo.Arguments = """" + ProjectFile + """ /verbosity:m" +
            " /p:Configuration=" + Configuration +
            additionalOptions +
            If(ForceRebuild, " /t:Clean,Build", "")
        prc.Start()

        Do While prc.StandardOutput.EndOfStream = False
            Dim p = prc.StandardOutput.ReadLine
            RaiseEvent Logger("INF", p)
            LastBuildOutputLines.Add(p)
            If p.Trim.Length > 0 Then LastBuildMessages.Add(New BuildMessage(p))
        Loop
        Success = (prc.ExitCode = 0)
        RaiseEvent BuildFinished(Me)
        Return Success
    End Function
End Class