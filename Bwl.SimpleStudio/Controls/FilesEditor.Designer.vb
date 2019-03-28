<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FilesEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FilesEditor))
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TextBoxEx1 = New TextBoxEx()
        Me.SuspendLayout()
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(589, 1)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(236, 21)
        Me.ComboBox1.TabIndex = 3
        '
        'TextBoxEx1
        '
        Me.TextBoxEx1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBoxEx1.BackColor = System.Drawing.Color.White
        Me.TextBoxEx1.BackgroundImage = CType(resources.GetObject("TextBoxEx1.BackgroundImage"), System.Drawing.Image)
        Me.TextBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxEx1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBoxEx1.Location = New System.Drawing.Point(0, 23)
        Me.TextBoxEx1.Name = "TextBoxEx1"
        Me.TextBoxEx1.NewLineSpacesAsPreviousLine = True
        Me.TextBoxEx1.Size = New System.Drawing.Size(826, 634)
        Me.TextBoxEx1.TabIndex = 4
        Me.TextBoxEx1.TabSize = 4
        '
        'FilesEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TextBoxEx1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Name = "FilesEditor"
        Me.Size = New System.Drawing.Size(826, 657)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents TextBoxEx1 As TextBoxEx
End Class
