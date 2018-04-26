Imports System.Runtime.ExceptionServices

Public Class RunnerDebugger
    Private _builder As Builder
    Private _runningTarget As Process

    Public Sub New(builder As Builder)
        _builder = builder
    End Sub

    Public Sub StopRunning()
        If _runningTarget IsNot Nothing Then
            Try
                _runningTarget.Kill()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Public Sub RunWithoutDebug(root As RootSolutionItem, target As ExecutableTarget)
        If root.SaveAllWithAsk = DialogResult.OK Then
            StopRunning()

            If _builder.BuildAll(root, target.Configuration).Success = False Then
                If MsgBox("Build failed, run anyway?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return
            End If

            _runningTarget = New Process
            _runningTarget.StartInfo.WorkingDirectory = IO.Path.GetDirectoryName(target.FullPath)
            _runningTarget.StartInfo.FileName = target.FullPath
            _runningTarget.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            Try
                _runningTarget.Start()
            Catch ex As Exception
                MsgBox("Run failed: " + ex.Message)
            End Try
        End If
    End Sub

    Public Sub RunWithDebug(root As RootSolutionItem, target As ExecutableTarget)
        If root.SaveAllWithAsk = DialogResult.OK Then
            StopRunning()

            If _builder.BuildAll(root, target.Configuration).Success = False Then
                If MsgBox("Build failed, run anyway?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then Return
            End If

            Dim newDomain As AppDomain = AppDomain.CreateDomain("new domain")
            AddHandler newDomain.FirstChanceException, AddressOf test
            AddHandler newDomain.UnhandledException, AddressOf test2

            Dim thr As New Threading.Thread(Sub()

                                                Try
                                                    '  newDomain.BaseDirectory = IO.Path.GetDirectoryName(target.FullPath)
                                                    newDomain.ExecuteAssembly(target.FullPath)
                                                    ' Dim asms = newDomain.GetAssemblies
                                                    ' Dim asb = newDomain.Load(target.FullPath)
                                                Catch ex As Exception
                                                    'MsgBox("1")
                                                    Dim a = 1
                                                End Try
                                            End Sub)
            thr.Start()
        End If
    End Sub

    Public Shared Sub test2(sender As Object, e As UnhandledExceptionEventArgs)
        MsgBox("2")
    End Sub

    Public Shared Sub test(sender As Object, e As FirstChanceExceptionEventArgs)
        MsgBox("3")
    End Sub

End Class
