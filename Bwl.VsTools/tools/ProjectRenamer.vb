Imports System.IO

Public Class ProjectRenamer

    Private Shared _initialDir = ""

    Public Shared Sub ProcessFolder(folder As String, ignoredFolders As String, oldProjectName As String, newProjectName As String, ignoredFileExtensions As String)
        Console.WriteLine("Start processing " + folder)
        Dim changed = False

        Console.WriteLine("Start renaming the initial folder...")
        Dim dirName = folder
        Try
            Dim newDirName = RenameString(folder, oldProjectName, newProjectName)
            If newDirName.ToLower <> folder.ToLower Then
                Directory.Move(folder, newDirName)
                dirName = newDirName
            End If
            Console.WriteLine("Finished renaming the initial folder.")
        Catch ex As Exception
            Console.WriteLine("Cannot rename initial folder. Error: " + ex.Message.ToString())
        End Try
        If String.IsNullOrWhiteSpace(_initialDir) Then _initialDir = dirName

        Try
            Dim files = Directory.GetFiles(dirName)
            For Each file In files
                Console.WriteLine("Start processing file " + file)
                Dim ext = Path.GetExtension(file)
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

            Dim dirs = Directory.GetDirectories(dirName)
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
        Dim isPath = False
        If res.StartsWith(_initialDir) Then
            res = res.Remove(0, _initialDir.Length)
            isPath = True
        End If

        changed = False
        Dim lowerOld = oldWord.ToLower
        Dim lastIndex = 0
        While (True)

            Dim i = res.ToLower.IndexOf(lowerOld, lastIndex, StringComparison.Ordinal)
            If i = -1 Then
                Exit While
            End If

            Dim allowRename As Boolean
            If (oldWord.Length > newWord.Length) Then
                Dim checkWord = If((i - 1 + oldWord.Length) < res.Length, res.Substring(i, oldWord.Length), "")
                If Not String.Equals(checkWord, "") Then
                    If String.Equals(checkWord, oldWord) Then
                        allowRename = True
                    Else
                        allowRename = False
                    End If
                Else
                    allowRename = False
                End If
            Else
                Dim checkWord = If((i - 1 + newWord.Length) < res.Length, res.Substring(i, newWord.Length), "")
                If Not String.Equals(checkWord, "") Then
                    If String.Equals(checkWord, newWord) Then
                        allowRename = False
                    Else
                        allowRename = True
                    End If
                Else
                    allowRename = True
                End If
            End If

            If allowRename Then
                res = res.Remove(i, oldWord.Length)
                res = res.Insert(i, newWord)
            End If
            lastIndex = i + newWord.Length
            changed = True
        End While

        If isPath Then
            res = res.Insert(0, _initialDir)
        End If
        Return res
    End Function

End Class
