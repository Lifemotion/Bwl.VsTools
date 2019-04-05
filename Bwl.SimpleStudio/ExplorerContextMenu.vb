
Public Class ExplorerContextMenu
    Private _menuSubkey As String
    Private _cmdSubkey As String

    Public Sub New(extensionName As String, Optional className As String = "*")
        _menuSubkey = className + "\\shell\\" + extensionName
        _cmdSubkey = className + "\\shell\\" + extensionName + "\\command"
    End Sub

    Public Sub AddOrChange(title As String, programPath As String)
        Dim regmenu As Microsoft.Win32.RegistryKey = Nothing
        Dim regcmd As Microsoft.Win32.RegistryKey = Nothing
        Try
            regmenu = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(_menuSubkey)
            If regmenu Is Nothing Then Throw New Exception("Failed to create " + _menuSubkey)
            regmenu.SetValue("", title)
            regcmd = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(_cmdSubkey)
            If regcmd Is Nothing Then Throw New Exception("Failed to create " + _cmdSubkey)
            regcmd.SetValue("", programPath + " %1")
        Catch ex As Exception
            If regmenu IsNot Nothing Then regmenu.Close()
            If regcmd IsNot Nothing Then regcmd.Close()
            Throw ex
        End Try
    End Sub

    Public Sub Delete()
        Microsoft.Win32.Registry.ClassesRoot.DeleteSubKeyTree(_menuSubkey)
    End Sub

    Public Function IsAdded() As Boolean
        Try
            Dim regcmd = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(_cmdSubkey)
            If regcmd IsNot Nothing Then Return True
        Catch ex As Exception
        End Try
        Return False
    End Function
End Class