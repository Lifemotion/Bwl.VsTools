<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class App
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(App))
        Me.FilesTree1 = New Bwl.SimpleStudio.FilesTree()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.toolbar = New System.Windows.Forms.ToolStrip()
        Me.tsbOpenSolution = New System.Windows.Forms.ToolStripButton()
        Me.tsbSave = New System.Windows.Forms.ToolStripButton()
        Me.tsbSaveAll = New System.Windows.Forms.ToolStripButton()
        Me.tsbBuildAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        Me.tabRun = New System.Windows.Forms.ToolStripButton()
        Me.tsbStop = New System.Windows.Forms.ToolStripButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ErrorsList1 = New Bwl.SimpleStudio.ErrorsList()
        Me.FileEditor1 = New Bwl.SimpleStudio.FilesEditorCollection()
        Me.toolbar.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FilesTree1
        '
        Me.FilesTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilesTree1.Location = New System.Drawing.Point(552, 3)
        Me.FilesTree1.Name = "FilesTree1"
        Me.FilesTree1.Size = New System.Drawing.Size(229, 434)
        Me.FilesTree1.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 641)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(784, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'toolbar
        '
        Me.toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenSolution, Me.tsbSave, Me.tsbSaveAll, Me.tsbBuildAll, Me.ToolStripComboBox1, Me.tabRun, Me.tsbStop})
        Me.toolbar.Location = New System.Drawing.Point(0, 0)
        Me.toolbar.Name = "toolbar"
        Me.toolbar.Size = New System.Drawing.Size(784, 25)
        Me.toolbar.TabIndex = 3
        Me.toolbar.Text = "ToolStrip1"
        '
        'tsbOpenSolution
        '
        Me.tsbOpenSolution.Image = Global.Bwl.SimpleStudio.My.Resources.Resources.document_open
        Me.tsbOpenSolution.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpenSolution.Name = "tsbOpenSolution"
        Me.tsbOpenSolution.Size = New System.Drawing.Size(103, 22)
        Me.tsbOpenSolution.Text = "Open Solution"
        '
        'tsbSave
        '
        Me.tsbSave.Image = Global.Bwl.SimpleStudio.My.Resources.Resources.document_save
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(51, 22)
        Me.tsbSave.Text = "Save"
        '
        'tsbSaveAll
        '
        Me.tsbSaveAll.Image = Global.Bwl.SimpleStudio.My.Resources.Resources.document_save_all
        Me.tsbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveAll.Name = "tsbSaveAll"
        Me.tsbSaveAll.Size = New System.Drawing.Size(68, 22)
        Me.tsbSaveAll.Text = "Save All"
        '
        'tsbBuildAll
        '
        Me.tsbBuildAll.Image = Global.Bwl.SimpleStudio.My.Resources.Resources.emblem_system
        Me.tsbBuildAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBuildAll.Name = "tsbBuildAll"
        Me.tsbBuildAll.Size = New System.Drawing.Size(71, 22)
        Me.tsbBuildAll.Text = "Build All"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(250, 25)
        '
        'tabRun
        '
        Me.tabRun.Image = Global.Bwl.SimpleStudio.My.Resources.Resources.media_playback_start
        Me.tabRun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tabRun.Name = "tabRun"
        Me.tabRun.Size = New System.Drawing.Size(48, 22)
        Me.tabRun.Text = "Run"
        '
        'tsbStop
        '
        Me.tsbStop.Image = Global.Bwl.SimpleStudio.My.Resources.Resources.media_playback_stop
        Me.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbStop.Name = "tsbStop"
        Me.tsbStop.Size = New System.Drawing.Size(51, 22)
        Me.tsbStop.Text = "Stop"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.03841!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.96159!))
        Me.TableLayoutPanel1.Controls.Add(Me.FilesTree1, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ErrorsList1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FileEditor1, 0, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 25)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.47708!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.52292!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(784, 616)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'ErrorsList1
        '
        Me.ErrorsList1.AssociatedBuildTask = Nothing
        Me.TableLayoutPanel1.SetColumnSpan(Me.ErrorsList1, 2)
        Me.ErrorsList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ErrorsList1.Location = New System.Drawing.Point(3, 443)
        Me.ErrorsList1.Name = "ErrorsList1"
        Me.ErrorsList1.Size = New System.Drawing.Size(778, 170)
        Me.ErrorsList1.TabIndex = 1
        '
        'FileEditor1
        '
        Me.FileEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FileEditor1.Location = New System.Drawing.Point(3, 3)
        Me.FileEditor1.Name = "FileEditor1"
        Me.FileEditor1.Size = New System.Drawing.Size(543, 434)
        Me.FileEditor1.TabIndex = 2
        '
        'App
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 663)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.toolbar)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "App"
        Me.Text = "Bwl.SimpleStudio"
        Me.toolbar.ResumeLayout(False)
        Me.toolbar.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents FilesTree1 As FilesTree
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents toolbar As ToolStrip
    Friend WithEvents tsbSave As ToolStripButton
    Friend WithEvents tsbSaveAll As ToolStripButton
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ErrorsList1 As ErrorsList
    Friend WithEvents FileEditor1 As FilesEditorCollection
    Friend WithEvents tsbBuildAll As ToolStripButton
    Friend WithEvents ToolStripComboBox1 As ToolStripComboBox
    Friend WithEvents tabRun As ToolStripButton
    Friend WithEvents tsbStop As ToolStripButton
    Friend WithEvents tsbOpenSolution As ToolStripButton
End Class
