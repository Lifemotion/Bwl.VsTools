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
        Me.SuspendLayout()
        '
        'buildDebug
        '
        Me.buildDebug.Location = New System.Drawing.Point(12, 80)
        Me.buildDebug.Name = "buildDebug"
        Me.buildDebug.Size = New System.Drawing.Size(94, 23)
        Me.buildDebug.TabIndex = 0
        Me.buildDebug.Text = "Build Debug"
        Me.buildDebug.UseVisualStyleBackColor = True
        '
        'buildRelease
        '
        Me.buildRelease.Location = New System.Drawing.Point(112, 80)
        Me.buildRelease.Name = "buildRelease"
        Me.buildRelease.Size = New System.Drawing.Size(94, 23)
        Me.buildRelease.TabIndex = 1
        Me.buildRelease.Text = "Build Release"
        Me.buildRelease.UseVisualStyleBackColor = True
        '
        'explorer
        '
        Me.explorer.Location = New System.Drawing.Point(12, 12)
        Me.explorer.Name = "explorer"
        Me.explorer.Size = New System.Drawing.Size(94, 23)
        Me.explorer.TabIndex = 2
        Me.explorer.Text = "Explorer"
        Me.explorer.UseVisualStyleBackColor = True
        '
        'bProjectCheck
        '
        Me.bProjectCheck.Location = New System.Drawing.Point(112, 51)
        Me.bProjectCheck.Name = "bProjectCheck"
        Me.bProjectCheck.Size = New System.Drawing.Size(94, 23)
        Me.bProjectCheck.TabIndex = 3
        Me.bProjectCheck.Text = "Project Check"
        Me.bProjectCheck.UseVisualStyleBackColor = True
        '
        'gitClean
        '
        Me.gitClean.Location = New System.Drawing.Point(12, 51)
        Me.gitClean.Name = "gitClean"
        Me.gitClean.Size = New System.Drawing.Size(94, 23)
        Me.gitClean.TabIndex = 4
        Me.gitClean.Text = "Git Clean"
        Me.gitClean.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(112, 12)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(94, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "Command Line"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'openGitIgnore
        '
        Me.openGitIgnore.Location = New System.Drawing.Point(12, 120)
        Me.openGitIgnore.Name = "openGitIgnore"
        Me.openGitIgnore.Size = New System.Drawing.Size(94, 23)
        Me.openGitIgnore.TabIndex = 6
        Me.openGitIgnore.Text = ".gitignore"
        Me.openGitIgnore.UseVisualStyleBackColor = True
        '
        'VsToolsForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(259, 237)
        Me.Controls.Add(Me.openGitIgnore)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.gitClean)
        Me.Controls.Add(Me.bProjectCheck)
        Me.Controls.Add(Me.explorer)
        Me.Controls.Add(Me.buildRelease)
        Me.Controls.Add(Me.buildDebug)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "VsToolsForm"
        Me.Text = "Bwl Visual Studio Tools"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents buildDebug As Button
    Friend WithEvents buildRelease As Button
    Friend WithEvents explorer As Button
    Friend WithEvents bProjectCheck As Button
    Friend WithEvents gitClean As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents openGitIgnore As Button
End Class
