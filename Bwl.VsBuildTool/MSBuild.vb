Public Class MSBuild
    Private Shared toolPaths As String() = {
    "C:\Program Files (x86)\MSBuild\15.0\Bin\MSBuild.EXE",
      "C:\Program Files\MSBuild\15.0\Bin\MSBuild.EXE",
      "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.EXE",
      "C:\Program Files\MSBuild\14.0\Bin\MSBuild.EXE",
      "C:\Program Files (x86)\MSBuild\13.0\Bin\MSBuild.EXE",
      "C:\Program Files\MSBuild\13.0\Bin\MSBuild.EXE",
      "C:\Program Files (x86)\MSBuild\12.0\Bin\MSBuild.EXE",
      "C:\Program Files\MSBuild\12.0\Bin\MSBuild.EXE",
      "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.EXE",
      "C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.EXE"}

    Public Shared ReadOnly Property MSBuildPath As String = ""

    Shared Sub New()
        Rescan()
    End Sub

    Public Shared Sub Rescan()
        _MsbuildPath = ""
        For Each toolPath In toolPaths
            If IO.File.Exists(toolPath) Then
                _MSBuildPath = toolPath
                Return
            End If
        Next
    End Sub
End Class
