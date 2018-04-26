<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ErrorsList
    Inherits System.Windows.Forms.UserControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
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
        Me.cbErrors = New System.Windows.Forms.CheckBox()
        Me.cbWarnings = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabErrors = New System.Windows.Forms.TabPage()
        Me.cbOther = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.MsgNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MsgPicture = New System.Windows.Forms.DataGridViewImageColumn()
        Me.MsgText = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MsgFile = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabBuildLog = New System.Windows.Forms.TabPage()
        Me.tbLog = New System.Windows.Forms.TextBox()
        Me.TabControl1.SuspendLayout()
        Me.tabErrors.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabBuildLog.SuspendLayout()
        Me.SuspendLayout()
        '
        'cbErrors
        '
        Me.cbErrors.Appearance = System.Windows.Forms.Appearance.Button
        Me.cbErrors.Checked = True
        Me.cbErrors.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbErrors.Location = New System.Drawing.Point(0, 2)
        Me.cbErrors.Name = "cbErrors"
        Me.cbErrors.Size = New System.Drawing.Size(104, 24)
        Me.cbErrors.TabIndex = 1
        Me.cbErrors.Text = "Errors"
        Me.cbErrors.UseVisualStyleBackColor = True
        '
        'cbWarnings
        '
        Me.cbWarnings.Appearance = System.Windows.Forms.Appearance.Button
        Me.cbWarnings.Location = New System.Drawing.Point(106, 2)
        Me.cbWarnings.Name = "cbWarnings"
        Me.cbWarnings.Size = New System.Drawing.Size(104, 24)
        Me.cbWarnings.TabIndex = 2
        Me.cbWarnings.Text = "Warnings"
        Me.cbWarnings.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabErrors)
        Me.TabControl1.Controls.Add(Me.tabBuildLog)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(890, 403)
        Me.TabControl1.TabIndex = 3
        '
        'tabErrors
        '
        Me.tabErrors.BackColor = System.Drawing.Color.White
        Me.tabErrors.Controls.Add(Me.cbOther)
        Me.tabErrors.Controls.Add(Me.DataGridView1)
        Me.tabErrors.Controls.Add(Me.cbWarnings)
        Me.tabErrors.Controls.Add(Me.cbErrors)
        Me.tabErrors.Location = New System.Drawing.Point(4, 22)
        Me.tabErrors.Name = "tabErrors"
        Me.tabErrors.Padding = New System.Windows.Forms.Padding(3)
        Me.tabErrors.Size = New System.Drawing.Size(882, 377)
        Me.tabErrors.TabIndex = 0
        Me.tabErrors.Text = "Errors"
        '
        'cbOther
        '
        Me.cbOther.Appearance = System.Windows.Forms.Appearance.Button
        Me.cbOther.Location = New System.Drawing.Point(212, 2)
        Me.cbOther.Name = "cbOther"
        Me.cbOther.Size = New System.Drawing.Size(104, 24)
        Me.cbOther.TabIndex = 3
        Me.cbOther.Text = "Other"
        Me.cbOther.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.MsgNumber, Me.MsgPicture, Me.MsgText, Me.MsgFile})
        Me.DataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.DataGridView1.Location = New System.Drawing.Point(1, 29)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(881, 348)
        Me.DataGridView1.TabIndex = 0
        '
        'MsgNumber
        '
        Me.MsgNumber.HeaderText = "#"
        Me.MsgNumber.Name = "MsgNumber"
        Me.MsgNumber.Width = 40
        '
        'MsgPicture
        '
        Me.MsgPicture.HeaderText = ""
        Me.MsgPicture.Name = "MsgPicture"
        Me.MsgPicture.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.MsgPicture.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.MsgPicture.Width = 30
        '
        'MsgText
        '
        Me.MsgText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.MsgText.HeaderText = "Text"
        Me.MsgText.Name = "MsgText"
        '
        'MsgFile
        '
        Me.MsgFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.MsgFile.FillWeight = 30.0!
        Me.MsgFile.HeaderText = "File"
        Me.MsgFile.Name = "MsgFile"
        '
        'tabBuildLog
        '
        Me.tabBuildLog.Controls.Add(Me.tbLog)
        Me.tabBuildLog.Location = New System.Drawing.Point(4, 22)
        Me.tabBuildLog.Name = "tabBuildLog"
        Me.tabBuildLog.Padding = New System.Windows.Forms.Padding(3)
        Me.tabBuildLog.Size = New System.Drawing.Size(882, 377)
        Me.tabBuildLog.TabIndex = 1
        Me.tabBuildLog.Text = "Build log"
        Me.tabBuildLog.UseVisualStyleBackColor = True
        '
        'tbLog
        '
        Me.tbLog.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tbLog.Location = New System.Drawing.Point(3, 3)
        Me.tbLog.Multiline = True
        Me.tbLog.Name = "tbLog"
        Me.tbLog.Size = New System.Drawing.Size(876, 371)
        Me.tbLog.TabIndex = 0
        '
        'ErrorsList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "ErrorsList"
        Me.Size = New System.Drawing.Size(890, 403)
        Me.TabControl1.ResumeLayout(False)
        Me.tabErrors.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabBuildLog.ResumeLayout(False)
        Me.tabBuildLog.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cbErrors As CheckBox
    Friend WithEvents cbWarnings As CheckBox
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tabErrors As TabPage
    Friend WithEvents tabBuildLog As TabPage
    Friend WithEvents tbLog As TextBox
    Friend WithEvents cbOther As CheckBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents MsgNumber As DataGridViewTextBoxColumn
    Friend WithEvents MsgPicture As DataGridViewImageColumn
    Friend WithEvents MsgText As DataGridViewTextBoxColumn
    Friend WithEvents MsgFile As DataGridViewTextBoxColumn
End Class
