Public Class NewSolutionTools

    Public Shared Function CreateVbCmdLineSolution(rootPath As String, prjName As String) As String
        IO.Directory.CreateDirectory(rootPath)
        Dim prjPath = IO.Path.Combine(rootPath, prjName)
        IO.Directory.CreateDirectory(prjPath)
        IO.Directory.CreateDirectory(IO.Path.Combine(prjPath, "My Project"))
        GenerateVbAssemblyInfo(IO.Path.Combine(prjPath, "My Project", "AssemblyInfo.vb"), prjName)
        GenerateVbMainCliFile(IO.Path.Combine(prjPath, "Main.vb"))
        Dim files As New List(Of String)
        files.Add("My Project/AssemblyInfo.vb")
        files.Add("Main.vb")
        Dim id = Guid.NewGuid
        GenerateVbProjectFile(IO.Path.Combine(prjPath, prjName + ".vbproj"), id, prjName, files)
        Dim prjs As New List(Of String)
        prjs.Add(IO.Path.Combine(prjPath, prjName + ".vbproj"))
        Dim sln = IO.Path.Combine(rootPath, prjName + ".sln")
        GenerateVsSlnFile(sln, prjs.ToArray)
        Return sln
    End Function

    Public Shared Sub GenerateVbAssemblyInfo(path As String, title As String)
        Dim lines As New List(Of String)

        lines.Add("Imports System")
        lines.Add("Imports System.Reflection")
        lines.Add("Imports System.Runtime.InteropServices")
        lines.Add("")

        lines.Add("<Assembly: AssemblyTitle(""" + title + """)>")
        lines.Add("<Assembly: AssemblyDescription("""")>")
        lines.Add("<Assembly: AssemblyCompany("""")>")
        lines.Add("<Assembly: AssemblyProduct(""" + title + """)>")
        lines.Add("<Assembly: AssemblyCopyright(""Copyright ©  " + Now.Year.ToString + """)>")
        lines.Add("<Assembly: AssemblyTrademark("""")>")

        lines.Add("<Assembly: ComVisible(False)>")

        'TODO
        lines.Add("<Assembly: Guid(""dff2b44c-7d14-47ac-91cc-b97d7b2099b0"")>")

        lines.Add("<Assembly: AssemblyVersion(""1.0.0.0"")>")
        lines.Add("<Assembly: AssemblyFileVersion(""1.0.0.0"")>")

        IO.File.WriteAllLines(path, lines.ToArray)
    End Sub

    Private Shared Function CleanName(name As String) As String
        Dim result = ""
        For Each c In name
            Select Case c
                Case "A" To "Z", "a" To "z", "0" To "9", "."
                    result += c
            End Select
        Next
        If result = "" Then Throw New Exception("Bad Name: " + name)
        Return result
    End Function

    Public Shared Sub GenerateVbProjectFile(path As String, guid As Guid, name As String,
                                            fileslist As IEnumerable(Of String), Optional framework As String = "4.5")
        Dim lines As New List(Of String)
        lines.Add("<?xml version=""1.0"" encoding=""utf-8""?>")
        lines.Add("<Project ToolsVersion=""14.0"" DefaultTargets=""Build"" xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">")
        lines.Add("  <Import Project=""$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props"" Condition=""Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')"" />")
        lines.Add("  <PropertyGroup>")
        lines.Add("    <Configuration Condition="" '$(Configuration)' == '' "">Debug</Configuration>")
        lines.Add("    <Platform Condition="" '$(Platform)' == '' "">AnyCPU</Platform>")
        lines.Add("    <ProjectGuid>{" + guid.ToString.ToUpper + "}</ProjectGuid>")
        lines.Add("    <OutputType>" + "Exe" + "</OutputType>")
        lines.Add("    <StartupObject>Sub Main</StartupObject>")
        lines.Add("    <RootNamespace>" + CleanName(name) + "</RootNamespace>")
        lines.Add("    <AssemblyName>" + name + "</AssemblyName>")
        lines.Add("    <FileAlignment>512</FileAlignment>")
        lines.Add("    <MyType>Console</MyType>")
        lines.Add("    <TargetFrameworkVersion>" + framework + "</TargetFrameworkVersion>")
        lines.Add("  </PropertyGroup>")
        lines.Add("  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' "">")
        lines.Add("    <PlatformTarget>AnyCPU</PlatformTarget>")
        lines.Add("    <DebugSymbols>true</DebugSymbols>")
        lines.Add("    <DebugType>full</DebugType>")
        lines.Add("    <DefineDebug>true</DefineDebug>")
        lines.Add("    <DefineTrace>true</DefineTrace>")
        lines.Add("    <OutputPath>..\debug\App\bin\</OutputPath>")
        lines.Add("    <DocumentationFile>" + name + ".xml</DocumentationFile>")
        lines.Add("    <NoWarn>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022</NoWarn>")
        lines.Add("  </PropertyGroup>")
        lines.Add("  <PropertyGroup Condition="" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' "">")
        lines.Add("    <PlatformTarget>AnyCPU</PlatformTarget>")
        lines.Add("    <DebugType>pdbonly</DebugType>")
        lines.Add("    <DefineDebug>false</DefineDebug>")
        lines.Add("    <DefineTrace>true</DefineTrace>")
        lines.Add("    <Optimize>true</Optimize>")
        lines.Add("    <OutputPath>..\release\App\bin\</OutputPath>")
        lines.Add("    <DocumentationFile>" + name + ".xml</DocumentationFile>")
        lines.Add("    <NoWarn>42016,41999,42017,42018,42019,42032,42036,42020,42021,42022</NoWarn>")
        lines.Add("  </PropertyGroup>")
        lines.Add("  <PropertyGroup>")
        lines.Add("    <OptionExplicit>On</OptionExplicit>")
        lines.Add("  </PropertyGroup>")
        lines.Add("  <PropertyGroup>")
        lines.Add("    <OptionCompare>Binary</OptionCompare>")
        lines.Add("  </PropertyGroup>")
        lines.Add("  <PropertyGroup>")
        lines.Add("    <OptionStrict>Off</OptionStrict>")
        lines.Add("  </PropertyGroup>")
        lines.Add("  <PropertyGroup>")
        lines.Add("    <OptionInfer>On</OptionInfer>")
        lines.Add("  </PropertyGroup>")
        lines.Add("  <ItemGroup>")
        lines.Add("    <Reference Include=""System"" />")
        lines.Add("    <Reference Include=""System.Data"" />")
        lines.Add("    <Reference Include=""System.Xml"" />")
        lines.Add("    <Reference Include=""System.Core"" />")
        lines.Add("    <!--REFS-->")
        lines.Add("  </ItemGroup>")
        lines.Add("  <ItemGroup>")
        lines.Add("    <Import Include=""Microsoft.VisualBasic"" />")
        lines.Add("    <Import Include=""System"" />")
        lines.Add("    <Import Include=""System.Collections"" />")
        lines.Add("    <Import Include=""System.Collections.Generic"" />")
        lines.Add("    <Import Include=""System.Data"" />")
        lines.Add("    <Import Include=""System.Diagnostics"" />")
        lines.Add("    <Import Include=""System.Linq"" />")
        lines.Add("    <Import Include=""System.Threading.Tasks"" />")
        lines.Add("  </ItemGroup>")
        lines.Add("  <ItemGroup>")
        For Each item In fileslist
            lines.Add("    <Compile Include=""" + item + """ />")
        Next
        lines.Add("</ItemGroup>")
        lines.Add("")
        lines.Add("  <!--PRJREFS-->")
        lines.Add("  <Import Project=""$(MSBuildToolsPath)\Microsoft.VisualBasic.targets"" />")
        lines.Add("  <Target Name=""BeforeBuild"">")
        lines.Add("  </Target>")
        lines.Add("  <Target Name=""AfterBuild"">")
        lines.Add("  </Target>")
        lines.Add("</Project>")
        IO.File.WriteAllLines(path, lines.ToArray)

    End Sub

    Public Shared Sub GenerateVbMainCliFile(path As String)
        Dim lines As New List(Of String)
        lines.Add(" Module App")
        lines.Add("")
        lines.Add("    Sub Main()")
        lines.Add("    Console.WriteLine(""HelloWorld"")")
        lines.Add("    Console.ReadLine")
        lines.Add("    ")
        lines.Add("    End Sub")
        lines.Add("")
        lines.Add("End Module")
        IO.File.WriteAllLines(path, lines.ToArray)
    End Sub

    Public Shared Sub GenerateVsSlnFile(path As String, prjs() As String)
        Dim lines As New List(Of String)
        lines.Add("Microsoft Visual Studio Solution File, Format Version 12.00")
        lines.Add("# Visual Studio 15")
        lines.Add("VisualStudioVersion = 15.0.26228.4")
        lines.Add("MinimumVisualStudioVersion = 10.0.40219.1")
        For Each prj In prjs
            Dim ext = IO.Path.GetExtension(prj)
            Dim name = IO.Path.GetFileNameWithoutExtension(prj)
            Dim guid = GetVsProjectGuid(prj)
            If ext = ".vbproj" Then
                Dim prjLine = "Project(""{F184B08F-C81C-45F6-A57F-5ABD9991F28F}"") = """ + name + """, """ + name + "\" + name + ".vbproj"", """ + guid + """"
                lines.Add(prjLine)
                lines.Add("EndProject")
            End If
        Next
        lines.Add("EndProject")
        lines.Add("Global")
        lines.Add("	GlobalSection(SolutionConfigurationPlatforms) = preSolution")
        lines.Add("		Debug|Any CPU = Debug|Any CPU")
        lines.Add("		Release|Any CPU = Release|Any CPU")
        lines.Add("	EndGlobalSection")
        lines.Add("	GlobalSection(ProjectConfigurationPlatforms) = postSolution")
        For Each prj In prjs
            Dim ext = IO.Path.GetExtension(prj)
            Dim name = IO.Path.GetFileNameWithoutExtension(prj)
            Dim guid = GetVsProjectGuid(prj)
            If ext = ".vbproj" Then
                lines.Add("		" + guid + ".Debug|Any CPU.ActiveCfg = Debug|Any CPU")
                lines.Add("		" + guid + ".Debug|Any CPU.Build.0 = Debug|Any CPU")
                lines.Add("		" + guid + ".Release|Any CPU.ActiveCfg = Release|Any CPU")
                lines.Add("		" + guid + ".Release|Any CPU.Build.0 = Release|Any CPU")
            End If
        Next
        lines.Add("	EndGlobalSection")
        lines.Add("	GlobalSection(SolutionProperties) = preSolution")
        lines.Add("		HideSolutionNode = FALSE")
        lines.Add("	EndGlobalSection")
        lines.Add("EndGlobal")

        IO.File.WriteAllLines(path, lines.ToArray)
    End Sub

    Public Shared Function GetVsProjectGuid(path As String) As String
        Dim result = FindInFile(path, "<ProjectGuid>", "</ProjectGuid>")
        If result Is Nothing OrElse result.Length < 10 Then Throw New Exception
        Return result
    End Function

    Public Shared Function FindInFile(file As String, stringStart As String, stringEnd As String) As String
        Dim lines = IO.File.ReadAllLines(file)
        For i = 0 To lines.Length - 1
            If lines(i).Contains(stringStart) And lines(i).Contains(stringEnd) Then
                Dim result = lines(i).Replace(stringStart, "").Replace(stringEnd, "").Trim
                Return result
            End If
        Next
        IO.File.WriteAllLines(file, lines)
        Return Nothing
    End Function

    Public Shared Function ReplaceInFile(file As String, stringStart As String, replace As String, stringEnd As String) As Integer
        Dim replaces As Integer
        Dim lines = IO.File.ReadAllLines(file)
        For i = 0 To lines.Length - 1
            If lines(i).Contains(stringStart) And lines(i).Contains(stringEnd) Then
                lines(i) = stringStart + replace + stringEnd
                replaces += 1
            End If
        Next
        IO.File.WriteAllLines(file, lines)
        Return replaces
    End Function

    Public Shared Function ReplaceInFile(file As String, search As String, replace As String) As Integer
        Dim replaces As Integer
        Dim lines = IO.File.ReadAllLines(file)
        For i = 0 To lines.Length - 1
            If lines(i).Contains(search) Then
                lines(i) = lines(i).Replace(search, replace)
                replaces += 1
            End If
        Next
        IO.File.WriteAllLines(file, lines)
        Return replaces
    End Function
End Class
