Public Class BuildMessage
    Public Property RawLine As String = ""
    Public Property Type As BuildMessageType
    Public Property Message As String = ""
    Public Property SourceFile As String = ""
    Public Property SourceFileLine As Integer
    Public Property SourceFileColumn As Integer

    Public Sub New()

    End Sub

    Public Sub New(rawLine As String)
        Me.RawLine = rawLine
        Me.Type = BuildMessageType.Debug
        Me.Message = rawLine

        Dim parts = rawLine.Split({": "}, StringSplitOptions.None)
        If parts.Length = 3 Then
            If parts(1).StartsWith("error") Then
                Type = BuildMessageType.BuildError
                Message = _prjFileRegex.Replace(parts(2), "")
                DecodeSourceFileAndPosition(parts(0))
            End If
            If parts(1).StartsWith("warning") Then
                Type = BuildMessageType.BuildWarning
                Message = _prjFileRegex.Replace(parts(2), "")
                DecodeSourceFileAndPosition(parts(0))
            End If
        End If
    End Sub

    Private _positionRegex As New System.Text.RegularExpressions.Regex("\(\d+,\d+\)")
    Private _prjFileRegex As New System.Text.RegularExpressions.Regex("\[.+\]")

    Private Sub DecodeSourceFileAndPosition(text As String)
        Dim posMatches = _positionRegex.Matches(text)
        If posMatches.Count = 1 Then
            Dim posParts = posMatches(0).Value.Replace("(", "").Replace(")", "").Split(",")
            Try
                SourceFileLine = CInt(posParts(0))
                SourceFileColumn = CInt(posParts(1))
                text = text.Replace(posMatches(0).Value, "")
            Catch ex As Exception
            End Try
            SourceFile = text
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return Type.ToString + " " + Message + " " + SourceFile
    End Function
End Class