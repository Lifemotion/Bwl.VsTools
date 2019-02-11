<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NewSolutionDialog
    Inherits System.Windows.Forms.Form

    'Форма переопределяет dispose для очистки списка компонентов.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NewSolutionDialog))
        Me.tbSolutionName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbLocation = New System.Windows.Forms.TextBox()
        Me.bBrowse = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tbProjectName = New System.Windows.Forms.TextBox()
        Me.tbSolutionPath = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.bOK = New System.Windows.Forms.Button()
        Me.bCancel = New System.Windows.Forms.Button()
        Me.rbWindowsForms = New System.Windows.Forms.RadioButton()
        Me.rbConsole = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'tbSolutionName
        '
        Me.tbSolutionName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSolutionName.Location = New System.Drawing.Point(93, 122)
        Me.tbSolutionName.Name = "tbSolutionName"
        Me.tbSolutionName.ReadOnly = True
        Me.tbSolutionName.Size = New System.Drawing.Size(485, 20)
        Me.tbSolutionName.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 126)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Solution Name:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 153)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Location:"
        '
        'tbLocation
        '
        Me.tbLocation.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbLocation.Location = New System.Drawing.Point(93, 150)
        Me.tbLocation.Name = "tbLocation"
        Me.tbLocation.Size = New System.Drawing.Size(485, 20)
        Me.tbLocation.TabIndex = 3
        '
        'bBrowse
        '
        Me.bBrowse.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bBrowse.Location = New System.Drawing.Point(586, 150)
        Me.bBrowse.Name = "bBrowse"
        Me.bBrowse.Size = New System.Drawing.Size(75, 21)
        Me.bBrowse.TabIndex = 4
        Me.bBrowse.Text = "Browse..."
        Me.bBrowse.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 99)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(74, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Project Name:"
        '
        'tbProjectName
        '
        Me.tbProjectName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbProjectName.Location = New System.Drawing.Point(93, 96)
        Me.tbProjectName.Name = "tbProjectName"
        Me.tbProjectName.Size = New System.Drawing.Size(387, 20)
        Me.tbProjectName.TabIndex = 5
        '
        'tbSolutionPath
        '
        Me.tbSolutionPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbSolutionPath.Location = New System.Drawing.Point(93, 176)
        Me.tbSolutionPath.Name = "tbSolutionPath"
        Me.tbSolutionPath.ReadOnly = True
        Me.tbSolutionPath.Size = New System.Drawing.Size(485, 20)
        Me.tbSolutionPath.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(8, 179)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Solution Path:"
        '
        'bOK
        '
        Me.bOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bOK.Location = New System.Drawing.Point(503, 203)
        Me.bOK.Name = "bOK"
        Me.bOK.Size = New System.Drawing.Size(75, 21)
        Me.bOK.TabIndex = 9
        Me.bOK.Text = "OK"
        Me.bOK.UseVisualStyleBackColor = True
        '
        'bCancel
        '
        Me.bCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bCancel.Location = New System.Drawing.Point(586, 203)
        Me.bCancel.Name = "bCancel"
        Me.bCancel.Size = New System.Drawing.Size(75, 21)
        Me.bCancel.TabIndex = 10
        Me.bCancel.Text = "Cancel"
        Me.bCancel.UseVisualStyleBackColor = True
        '
        'rbWindowsForms
        '
        Me.rbWindowsForms.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbWindowsForms.Enabled = False
        Me.rbWindowsForms.Location = New System.Drawing.Point(217, 12)
        Me.rbWindowsForms.Name = "rbWindowsForms"
        Me.rbWindowsForms.Size = New System.Drawing.Size(118, 70)
        Me.rbWindowsForms.TabIndex = 11
        Me.rbWindowsForms.Text = "VB.NET Windows Forms App"
        Me.rbWindowsForms.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbWindowsForms.UseVisualStyleBackColor = True
        '
        'rbConsole
        '
        Me.rbConsole.Appearance = System.Windows.Forms.Appearance.Button
        Me.rbConsole.Checked = True
        Me.rbConsole.Location = New System.Drawing.Point(93, 12)
        Me.rbConsole.Name = "rbConsole"
        Me.rbConsole.Size = New System.Drawing.Size(118, 70)
        Me.rbConsole.TabIndex = 12
        Me.rbConsole.TabStop = True
        Me.rbConsole.Text = "VB.NET Console App"
        Me.rbConsole.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.rbConsole.UseVisualStyleBackColor = True
        '
        'NewSolutionDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(675, 235)
        Me.Controls.Add(Me.rbConsole)
        Me.Controls.Add(Me.rbWindowsForms)
        Me.Controls.Add(Me.bCancel)
        Me.Controls.Add(Me.bOK)
        Me.Controls.Add(Me.tbSolutionPath)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tbProjectName)
        Me.Controls.Add(Me.bBrowse)
        Me.Controls.Add(Me.tbLocation)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tbSolutionName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NewSolutionDialog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "New Solution..."
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents tbSolutionName As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents tbLocation As TextBox
    Friend WithEvents bBrowse As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents tbProjectName As TextBox
    Friend WithEvents tbSolutionPath As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents bOK As Button
    Friend WithEvents bCancel As Button
    Friend WithEvents rbWindowsForms As RadioButton
    Friend WithEvents rbConsole As RadioButton
End Class
