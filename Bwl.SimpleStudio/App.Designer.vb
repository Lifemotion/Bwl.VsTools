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
        Me.tscbConfiguration = New System.Windows.Forms.ToolStripComboBox()
        Me.tscbTargets = New System.Windows.Forms.ToolStripComboBox()
        Me.tabRun = New System.Windows.Forms.ToolStripButton()
        Me.tsbStop = New System.Windows.Forms.ToolStripButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ErrorsList1 = New Bwl.SimpleStudio.ErrorsList()
        Me.FileEditor1 = New Bwl.SimpleStudio.FilesEditorCollection()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OpenSolutionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveAllFilesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuildRunToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BuildAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunSelectedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolbar.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FilesTree1
        '
        Me.FilesTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FilesTree1.Location = New System.Drawing.Point(670, 3)
        Me.FilesTree1.Name = "FilesTree1"
        Me.FilesTree1.Size = New System.Drawing.Size(280, 417)
        Me.FilesTree1.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 641)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(953, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'toolbar
        '
        Me.toolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbOpenSolution, Me.tsbSave, Me.tsbSaveAll, Me.tsbBuildAll, Me.tscbConfiguration, Me.tscbTargets, Me.tabRun, Me.tsbStop})
        Me.toolbar.Location = New System.Drawing.Point(0, 24)
        Me.toolbar.Name = "toolbar"
        Me.toolbar.Size = New System.Drawing.Size(953, 25)
        Me.toolbar.TabIndex = 3
        Me.toolbar.Text = "ToolStrip1"
        '
        'tsbOpenSolution
        '
        Me.tsbOpenSolution.Image = CType(resources.GetObject("tsbOpenSolution.Image"), System.Drawing.Image)
        Me.tsbOpenSolution.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOpenSolution.Name = "tsbOpenSolution"
        Me.tsbOpenSolution.Size = New System.Drawing.Size(103, 22)
        Me.tsbOpenSolution.Text = "Open Solution"
        '
        'tsbSave
        '
        Me.tsbSave.Image = CType(resources.GetObject("tsbSave.Image"), System.Drawing.Image)
        Me.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSave.Name = "tsbSave"
        Me.tsbSave.Size = New System.Drawing.Size(51, 22)
        Me.tsbSave.Text = "Save"
        '
        'tsbSaveAll
        '
        Me.tsbSaveAll.Image = CType(resources.GetObject("tsbSaveAll.Image"), System.Drawing.Image)
        Me.tsbSaveAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbSaveAll.Name = "tsbSaveAll"
        Me.tsbSaveAll.Size = New System.Drawing.Size(68, 22)
        Me.tsbSaveAll.Text = "Save All"
        '
        'tsbBuildAll
        '
        Me.tsbBuildAll.Image = CType(resources.GetObject("tsbBuildAll.Image"), System.Drawing.Image)
        Me.tsbBuildAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbBuildAll.Name = "tsbBuildAll"
        Me.tsbBuildAll.Size = New System.Drawing.Size(71, 22)
        Me.tsbBuildAll.Text = "Build All"
        '
        'tscbConfiguration
        '
        Me.tscbConfiguration.Items.AddRange(New Object() {"Debug", "Release"})
        Me.tscbConfiguration.Name = "tscbConfiguration"
        Me.tscbConfiguration.Size = New System.Drawing.Size(100, 25)
        Me.tscbConfiguration.Text = "Debug"
        '
        'tscbTargets
        '
        Me.tscbTargets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.tscbTargets.Name = "tscbTargets"
        Me.tscbTargets.Size = New System.Drawing.Size(250, 25)
        '
        'tabRun
        '
        Me.tabRun.Image = CType(resources.GetObject("tabRun.Image"), System.Drawing.Image)
        Me.tabRun.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tabRun.Name = "tabRun"
        Me.tabRun.Size = New System.Drawing.Size(48, 22)
        Me.tabRun.Text = "Run"
        '
        'tsbStop
        '
        Me.tsbStop.Image = CType(resources.GetObject("tsbStop.Image"), System.Drawing.Image)
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 49)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.47708!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.52292!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(953, 592)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'ErrorsList1
        '
        Me.ErrorsList1.AssociatedBuildTask = Nothing
        Me.TableLayoutPanel1.SetColumnSpan(Me.ErrorsList1, 2)
        Me.ErrorsList1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ErrorsList1.Location = New System.Drawing.Point(3, 426)
        Me.ErrorsList1.Name = "ErrorsList1"
        Me.ErrorsList1.Size = New System.Drawing.Size(947, 163)
        Me.ErrorsList1.TabIndex = 1
        '
        'FileEditor1
        '
        Me.FileEditor1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FileEditor1.Location = New System.Drawing.Point(3, 3)
        Me.FileEditor1.Name = "FileEditor1"
        Me.FileEditor1.Size = New System.Drawing.Size(661, 417)
        Me.FileEditor1.TabIndex = 2
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.BuildRunToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(953, 24)
        Me.MenuStrip1.TabIndex = 5
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenSolutionToolStripMenuItem, Me.SaveFileToolStripMenuItem, Me.SaveAllFilesToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'OpenSolutionToolStripMenuItem
        '
        Me.OpenSolutionToolStripMenuItem.Name = "OpenSolutionToolStripMenuItem"
        Me.OpenSolutionToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.OpenSolutionToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.OpenSolutionToolStripMenuItem.Text = "Open Solution..."
        '
        'SaveFileToolStripMenuItem
        '
        Me.SaveFileToolStripMenuItem.Name = "SaveFileToolStripMenuItem"
        Me.SaveFileToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveFileToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.SaveFileToolStripMenuItem.Text = "Save File"
        '
        'SaveAllFilesToolStripMenuItem
        '
        Me.SaveAllFilesToolStripMenuItem.Name = "SaveAllFilesToolStripMenuItem"
        Me.SaveAllFilesToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.SaveAllFilesToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.SaveAllFilesToolStripMenuItem.Text = "Save All Files"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(234, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'BuildRunToolStripMenuItem
        '
        Me.BuildRunToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BuildAllToolStripMenuItem, Me.RunSelectedToolStripMenuItem, Me.StopToolStripMenuItem})
        Me.BuildRunToolStripMenuItem.Name = "BuildRunToolStripMenuItem"
        Me.BuildRunToolStripMenuItem.Size = New System.Drawing.Size(83, 20)
        Me.BuildRunToolStripMenuItem.Text = "Build && Run"
        '
        'BuildAllToolStripMenuItem
        '
        Me.BuildAllToolStripMenuItem.Name = "BuildAllToolStripMenuItem"
        Me.BuildAllToolStripMenuItem.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
            Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.BuildAllToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.BuildAllToolStripMenuItem.Text = "Build All"
        '
        'RunSelectedToolStripMenuItem
        '
        Me.RunSelectedToolStripMenuItem.Name = "RunSelectedToolStripMenuItem"
        Me.RunSelectedToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RunSelectedToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.RunSelectedToolStripMenuItem.Text = "Run Selected"
        '
        'StopToolStripMenuItem
        '
        Me.StopToolStripMenuItem.Name = "StopToolStripMenuItem"
        Me.StopToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F5), System.Windows.Forms.Keys)
        Me.StopToolStripMenuItem.Size = New System.Drawing.Size(191, 22)
        Me.StopToolStripMenuItem.Text = "Stop"
        '
        'App
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 663)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.toolbar)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "App"
        Me.Text = "Bwl.SimpleStudio"
        Me.toolbar.ResumeLayout(False)
        Me.toolbar.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
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
    Friend WithEvents tscbTargets As ToolStripComboBox
    Friend WithEvents tabRun As ToolStripButton
    Friend WithEvents tsbStop As ToolStripButton
    Friend WithEvents tsbOpenSolution As ToolStripButton
    Friend WithEvents tscbConfiguration As ToolStripComboBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OpenSolutionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveAllFilesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BuildRunToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BuildAllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RunSelectedToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents StopToolStripMenuItem As ToolStripMenuItem
End Class
