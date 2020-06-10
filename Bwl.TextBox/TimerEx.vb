Imports System.Threading
Imports Bwl.TextBoxEx
Public Class TimerEx
    Private _handler As ThreadStart
    Private _period As Integer
    Private _once As Boolean
    Private _startTime As DateTime
    Private _abort As Boolean

    Private _thread As Threading.Thread
    Public Sub New(handler As ThreadStart, periodMs As Integer, Optional once As Boolean = True, Optional startNow As Boolean = False)
        _handler = handler
        _period = periodMs
        _once = once
        If startNow Then StartOrReset()
    End Sub
    Private Sub StartOrReset()
        If _thread Is Nothing Then
            _thread = New Threading.Thread(AddressOf WorkCycle)
            _thread.IsBackground = True
            _startTime = Now
            _abort = False
            _thread.Start()
        Else
            _startTime = Now
        End If
    End Sub

    Private Sub WorkCycle()
        Do While Not _abort
            If (Now - _startTime).TotalMilliseconds >= _period Then
                _handler()
                If _once Then
                    Exit Do
                Else
                    _startTime = Now
                End If
            End If
            Thread.Sleep(1)
        Loop
        _thread = Nothing
    End Sub

    Public Sub Abort()
        _abort = True
    End Sub

End Class


