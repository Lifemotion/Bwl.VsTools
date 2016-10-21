<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VsToolsForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VsToolsForm))
        Me.buildDebug = New System.Windows.Forms.Button()
        Me.buildRelease = New System.Windows.Forms.Button()
        Me.explorer = New System.Windows.Forms.Button()
        Me.bProjectCheck = New System.Windows.Forms.Button()
        Me.gitClean = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.openGitIgnore = New System.Windows.Forms.Button()
        Me.bAddStandardGittgnore = New System.Windows.Forms.Button()
        Me.licenseFile = New System.Windows.Forms.Button()
        Me.readmeFile = New System.Windows.Forms.Button()
        Me.renameProject = New System.Windows.Forms.Button()
        Me.SuspendLayout
        '
        'buildDebug
        '
        Me.buildDebug.Location = New System.Drawing.Point(12, 80)
        Me.buildDebug.Name = "buildDebug"
        Me.buildDebug.Size = New System.Drawing.Size(94, 23)
        Me.buildDebug.TabIndex = 0
        Me.buildDebug.Text = "Build Debug"
        Me.buildDebug.UseVisualStyleBackColor = true
        '
        'buildRelease
        '
        Me.buildRelease.Location = New System.Drawing.Point(112, 80)
        Me.buildRelease.Name = "buildRelease"
        Me.buildRelease.Size = New System.Drawing.Size(94, 23)
        Me.buildRelease.TabIndex = 1
        Me.buildRelease.Text = "Build Release"
        Me.buildRelease.UseVisualStyleBackColor = true
        '
        'explorer
        '
        Me.explorer.Location = New System.Drawing.Point(12, 12)
        Me.explorer.Name = "explorer"
        Me.explorer.Size = New System.Drawing.Size(94, 23)
        Me.explorer.TabIndex = 2
        Me.explorer.Text = "Explorer"
        Me.explorer.UseVisualStyleBackColor = true
        '
        'bProjectCheck
        '
        Me.bProjectCheck.Location = New System.Drawing.Point(112, 51)
        Me.bProjectCheck.Name = "bProjectCheck"
        Me.bProjectCheck.Size = New System.Drawing.Size(94, 23)
        Me.bProjectCheck.TabIndex = 3
        Me.bProjectCheck.Text = "Project Check"
        Me.bProjectCheck.UseVisualStyleBackColor = true
        '
        'gitClean
        '
        Me.gitClean.Location = New System.Drawing.Point(12, 51)
        Me.gitClean.Name = "gitClean"
        Me.gitClean.Size = New System.Drawing.Size(94, 23)
        Me.gitClean.TabIndex = 4
        Me.gitClean.Text = "Git Clean"
        Me.gitClean.UseVisualStyleBackColor = true
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(112, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(94, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Command Line"
        Me.Button1.UseVisualStyleBackColor = true
        '
        'openGitIgnore
        '
        Me.openGitIgnore.Location = New System.Drawing.Point(12, 148)
        Me.openGitIgnore.Name = "openGitIgnore"
        Me.openGitIgnore.Size = New System.Drawing.Size(94, 23)
        Me.openGitIgnore.TabIndex = 6
        Me.openGitIgnore.Text = ".gitignore"
        Me.openGitIgnore.UseVisualStyleBackColor = true
        '
        'bAddStandardGittgnore
        '
        Me.bAddStandardGittgnore.Location = New System.Drawing.Point(112, 148)
        Me.bAddStandardGittgnore.Name = "bAddStandardGittgnore"
        Me.bAddStandardGittgnore.Size = New System.Drawing.Size(94, 23)
        Me.bAddStandardGittgnore.TabIndex = 7
        Me.bAddStandardGittgnore.Text = "Add Standard"
        Me.bAddStandardGittgnore.UseVisualStyleBackColor = true
        '
        'licenseFile
        '
        Me.licenseFile.Location = New System.Drawing.Point(12, 177)
        Me.licenseFile.Name = "licenseFile"
        Me.licenseFile.Size = New System.Drawing.Size(94, 23)
        Me.licenseFile.TabIndex = 8
        Me.licenseFile.Text = "LICENSE"
        Me.licenseFile.UseVisualStyleBackColor = true
        '
        'readmeFile
        '
        Me.readmeFile.Location = New System.Drawing.Point(112, 177)
        Me.readmeFile.Name = "readmeFile"
        Me.readmeFile.Size = New System.Drawing.Size(94, 23)
        Me.readmeFile.TabIndex = 9
        Me.readmeFile.Text = "README"
        Me.readmeFile.UseVisualStyleBackColor = true
        '
        'renameProject
        '
        Me.renameProject.Location = New System.Drawing.Point(12, 109)
        Me.renameProject.Name = "renameProject"
        Me.renameProject.Size = New System.Drawing.Size(194, 23)
        Me.renameProject.TabIndex = 10
        Me.renameProject.Text = "Rename Project"
        Me.renameProject.UseVisualStyleBackColor = true
        '
        'VsToolsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(221, 212)
        Me.Controls.Add(Me.renameProject)
        Me.Controls.Add(Me.readmeFile)
        Me.Controls.Add(Me.licenseFile)
        Me.Controls.Add(Me.bAddStandardGittgnore)
        Me.Controls.Add(Me.openGitIgnore)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.gitClean)
        Me.Controls.Add(Me.bProjectCheck)
        Me.Controls.Add(Me.explorer)
        Me.Controls.Add(Me.buildRelease)
        Me.Controls.Add(Me.buildDebug)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "VsToolsForm"
        Me.Text = "Bwl VS.Tools"
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents buildDebug As Button
    Friend WithEvents buildRelease As Button
    Friend WithEvents explorer As Button
    Friend WithEvents bProjectCheck As Button
    Friend WithEvents gitClean As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents openGitIgnore As Button
    Friend WithEvents bAddStandardGittgnore As Button
    Friend WithEvents licenseFile As Button
    Friend WithEvents readmeFile As Button
    Friend WithEvents renameProject As Button
End Class
