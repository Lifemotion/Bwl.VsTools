﻿Imports System.IO

Public Class ProjectRenamer

    Public Shared Sub ProcessFolder(folder As String, ignoredFolders As String, oldProjectName As String, newProjectName As String, ignoredFileExtensions As String)
        Console.WriteLine("Start processing " + folder)
        Dim changed = False
        Dim newDirName = RenameString(folder, oldProjectName, newProjectName)
        If newDirName.ToLower <> folder.ToLower Then
            Directory.Move(folder, newDirName)
        End If
        Try
            Dim files = Directory.GetFiles(newDirName)
            For Each file In files
                Dim ext = IO.Path.GetExtension(file)
                If Not String.IsNullOrWhiteSpace(ext) Then
                    If ignoredFileExtensions.ToLower.Contains(ext.Replace(".", "").ToLower) Then
                        Continue For
                    End If
                End If

                Dim newFileName = RenameString(file, oldProjectName, newProjectName)
                Dim text = IO.File.ReadAllText(file)
                text = RenameString(text, oldProjectName, newProjectName, changed)
                If changed Or Not String.Equals(file, newFileName, StringComparison.InvariantCultureIgnoreCase) Then
                    IO.File.Delete(file)
                    IO.File.WriteAllText(newFileName, text)
                End If
                Console.WriteLine("File processed " + file)
            Next

            Dim dirs = Directory.GetDirectories(newDirName)
            For Each subDir In dirs
                If Not ignoredFolders.ToLower.Contains(Path.GetFileName(subDir).ToLower) Then
                    ProcessFolder(subDir, ignoredFolders, oldProjectName, newProjectName, ignoredFileExtensions)
                End If
            Next
        Catch ex As Exception
            Console.WriteLine("Error processing " + folder + ", " + ex.ToString)
        End Try
        Console.WriteLine("Finish processing " + folder)
    End Sub
    Private Shared Function RenameString(source As String, oldWord As String, newWord As String, Optional ByRef changed As Boolean = False) As String
        Dim res = source
        changed = False
        Dim lowerOld = oldWord.ToLower
        While (True)
            Dim i = res.ToLower.IndexOf(lowerOld)
            If i = -1 Then
                Exit While
            End If
            res = res.Remove(i, oldWord.Length)
            res = res.Insert(i, newWord)
            changed = True
        End While

        Return res
    End Function

End Class