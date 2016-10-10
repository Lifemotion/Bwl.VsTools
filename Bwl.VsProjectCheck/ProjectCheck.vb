Imports System.Xml

Public Class ProjectCheck
    Public Shared Sub CheckProjects(curdir As String)
        Dim clr = Console.ForegroundColor
        Console.WriteLine("Bwl VS Project Check Tool, " + My.Application.Info.Version.ToString)
        Console.WriteLine()
        Dim prjs = IO.Directory.GetFiles(curdir, "*.vbproj", IO.SearchOption.AllDirectories)
        Dim errs As Integer = 0
        For Each prj In prjs
            Console.WriteLine("Checking " + prj)

            Dim xmlPrj As New Xml.XmlDocument
            Dim prjname = IO.Path.GetFileNameWithoutExtension(prj)
            Dim prjdir = IO.Path.GetDirectoryName(prj)
            xmlPrj.Load(prj)
            Dim projectItem = xmlPrj.Item("Project")
            For Each itemGroup As XmlNode In projectItem.ChildNodes
                If itemGroup.Name = "ItemGroup" Then
                    If itemGroup.FirstChild IsNot Nothing AndAlso itemGroup.FirstChild.Name = "Reference" Then
                        For Each refNode As XmlNode In itemGroup.ChildNodes
                            Dim hintPathNode = refNode("HintPath")
                            If hintPathNode IsNot Nothing Then
                                Dim refPath = hintPathNode.InnerText
                                Dim path = IO.Path.GetFullPath(IO.Path.Combine(prjdir, refPath))
                                If path.Contains(curdir) = False Then
                                    Console.ForegroundColor = ConsoleColor.Red
                                    Console.WriteLine("Reference points outside of repository! (" + prjname + ")")
                                    Console.WriteLine(refPath)
                                    Console.WriteLine()
                                    errs += 1
                                Else
                                    If IO.File.Exists(path) = False Then
                                        Console.ForegroundColor = ConsoleColor.Red
                                        Console.WriteLine("Reference not exists! (" + prjname + ")")
                                        Console.WriteLine(refPath)
                                        Console.WriteLine()
                                        errs += 1
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If
            Next
        Next
        If errs = 0 Then
            Console.ForegroundColor = ConsoleColor.Green
            Console.WriteLine("No errors found!")
            errs += 1
        Else
            Console.WriteLine("Errors: " + errs.ToString)
            Console.WriteLine("Press any key")
            Console.ReadLine()
        End If
        Console.ForegroundColor = clr
    End Sub
End Class
