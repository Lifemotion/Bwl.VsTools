Module FileToSrc

    Sub Main()
        Dim fname = Command.Replace("""", "")
        Dim file = IO.File.OpenText(fname)
        Dim basic = IO.File.CreateText(fname + ".VB.txt")
        basic.WriteLine("Dim lines As New List(Of String)")
        While Not file.EndOfStream
            Dim line = file.ReadLine
            basic.WriteLine("lines.Add(""" + line.Replace("""", """""") + """)")

        End While
        basic.Close()
        file.Close()
    End Sub

End Module
