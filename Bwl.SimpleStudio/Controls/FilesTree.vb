Public Class FilesTree
    Private _itemsTree As SolutionItem

    Public Event FileSelected(node As TreeNode, repNode As SolutionItem)
    Public Event TreeRefreshed()

    Public ReadOnly Property SelectedNode As TreeNode
        Get
            If Me.InvokeRequired Then
                Return Me.Invoke(Function() tvFiles.SelectedNode)
            Else
                Return tvFiles.SelectedNode
            End If
        End Get
    End Property

    Public ReadOnly Property SelectedSolutionItem As SolutionItem
        Get
            If SelectedNode Is Nothing Then Return Nothing
            Return SelectedNode.Tag
        End Get
    End Property

    Private Sub RepositoryTree_Load(sender As Object, e As EventArgs) Handles Me.Load
        If DesignMode Then Return
    End Sub

    Private Function AddTreeNodesRecursive(parentCollection As TreeNodeCollection, fileNode As SolutionItem, filter As String) As Boolean
        Dim myNode = parentCollection.Add(fileNode.FullPath, fileNode.Name)
        myNode.Tag = fileNode
        RefreshNodeState(myNode)
        Dim good As Boolean = False
        For Each child In fileNode.Childs
            If AddTreeNodesRecursive(myNode.Nodes, child, filter) Then good = True
        Next
        If filter = "" Or fileNode.Name.ToLower.Contains(filter.ToLower) Then
            good = True
        End If
        If good = False Then
            parentCollection.Remove(myNode)
        End If
        Return good
    End Function

    Public Sub RecreateNodes()
        RecreateNodes("")
    End Sub

    Public Sub RecreateNodes(filter As String)
        tvFiles.Nodes.Clear()
        AddTreeNodesRecursive(tvFiles.Nodes, _itemsTree, filter)
        For Each node As TreeNode In tvFiles.Nodes
            node.Expand()
        Next
        RaiseEvent TreeRefreshed()
    End Sub

    Public Sub LoadTree(path As String)
        _itemsTree = SolutionItem.CreateSolutionTree(path)
        RecreateNodes()
    End Sub

    Public Sub RefreshNodeState(node As TreeNode)
        Dim repNode As SolutionItem = node.Tag
        If repNode IsNot Nothing Then
            Dim index = 0
            If repNode.IsDirectory = False Then
                index = 1
                If repNode.Extension = ".vb" Then index = 2
                If repNode.Extension = ".cs" Then index = 2
            End If
            node.SelectedImageIndex = index
            node.ImageIndex = index
        End If
    End Sub

    Public Sub RefreshNodeRecursive(parentNode As TreeNode)
        RefreshNodeState(parentNode)
        For Each child In parentNode.Nodes
            RefreshNodeRecursive(child)
        Next
    End Sub

    Public Sub RefreshAllTree()
        If Me.InvokeRequired Then
            Me.Invoke(Sub() RefreshAllTree())
        Else
            For Each node As TreeNode In tvFiles.Nodes
                RefreshNodeRecursive(node)
            Next
            RaiseEvent TreeRefreshed()
            RaiseEvent FileSelected(SelectedNode, SelectedSolutionItem)
        End If
    End Sub

    Private Sub tvRepositories_MouseDown(sender As Object, e As MouseEventArgs) Handles tvFiles.MouseDown
        tvFiles.SelectedNode = tvFiles.GetNodeAt(e.X, e.Y)
    End Sub

    Private Sub tvRepositories_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tvFiles.AfterSelect
        RaiseEvent FileSelected(SelectedNode, SelectedSolutionItem)
    End Sub

    Private Sub tbFilter_KeyDown(sender As Object, e As KeyEventArgs) Handles tbFilter.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            Dim filter = tbFilter.Text.Trim
            RecreateNodes(filter)
            If filter > "" Then
                tvFiles.ExpandAll()
                tResetFilter.Stop()
                tResetFilter.Start()
            End If
        End If
    End Sub

    Private Sub tResetFilter_Tick(sender As Object, e As EventArgs) Handles tResetFilter.Tick
        tbFilter.Text = ""
        tResetFilter.Stop()
        RecreateNodes()
    End Sub

    Private Sub tbFilter_GotFocus(sender As Object, e As EventArgs) Handles tbFilter.GotFocus
        If tbFilter.Text = "<фильтр>" Then tbFilter.Text = ""
    End Sub
End Class
