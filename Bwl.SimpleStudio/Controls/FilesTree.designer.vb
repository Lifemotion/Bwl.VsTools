<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FilesTree
    Inherits System.Windows.Forms.UserControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Является обязательной для конструктора форм Windows Forms
    Private components As System.ComponentModel.IContainer

    'Примечание: следующая процедура является обязательной для конструктора форм Windows Forms
    'Для ее изменения используйте конструктор форм Windows Form.  
    'Не изменяйте ее в редакторе исходного кода.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FilesTree))
        Me.tvFiles = New System.Windows.Forms.TreeView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.tbFilter = New System.Windows.Forms.TextBox()
        Me.tResetFilter = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'tvFiles
        '
        Me.tvFiles.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tvFiles.HideSelection = False
        Me.tvFiles.ImageIndex = 0
        Me.tvFiles.ImageList = Me.ImageList1
        Me.tvFiles.ItemHeight = 16
        Me.tvFiles.Location = New System.Drawing.Point(0, 22)
        Me.tvFiles.Name = "tvFiles"
        Me.tvFiles.SelectedImageIndex = 0
        Me.tvFiles.Size = New System.Drawing.Size(284, 421)
        Me.tvFiles.TabIndex = 3
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "folder.png")
        Me.ImageList1.Images.SetKeyName(1, "text-x-generic.png")
        Me.ImageList1.Images.SetKeyName(2, "text-x-script.png")
        Me.ImageList1.Images.SetKeyName(3, "package-x-generic.png")
        Me.ImageList1.Images.SetKeyName(4, "user-home.png")
        Me.ImageList1.Images.SetKeyName(5, "image-x-generic.png")
        '
        'tbFilter
        '
        Me.tbFilter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFilter.Location = New System.Drawing.Point(0, 0)
        Me.tbFilter.Name = "tbFilter"
        Me.tbFilter.Size = New System.Drawing.Size(284, 20)
        Me.tbFilter.TabIndex = 5
        Me.tbFilter.Text = "<фильтр>"
        '
        'tResetFilter
        '
        Me.tResetFilter.Interval = 60000
        '
        'FilesTree
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.tbFilter)
        Me.Controls.Add(Me.tvFiles)
        Me.Name = "FilesTree"
        Me.Size = New System.Drawing.Size(284, 443)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tvFiles As TreeView
    Friend WithEvents ImageList1 As ImageList
    Friend WithEvents tbFilter As TextBox
    Friend WithEvents tResetFilter As Timer
End Class
