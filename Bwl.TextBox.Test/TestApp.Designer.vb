<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TestApp
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TestApp))
        Me.TextBoxEx1 = New Bwl.TextBox.TextBoxEx()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
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
        Me.TextBoxEx1.FontSize = 9
        Me.TextBoxEx1.Location = New System.Drawing.Point(12, 12)
        Me.TextBoxEx1.Name = "TextBoxEx1"
        Me.TextBoxEx1.NewlineSpacesAsPreviousLine = True
        Me.TextBoxEx1.Size = New System.Drawing.Size(515, 492)
        Me.TextBoxEx1.TabIndex = 0
        Me.TextBoxEx1.TabSize = 4
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(546, 370)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 1
        '
        'TestApp
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(624, 516)
        Me.Controls.Add(Me.TextBoxEx1)
        Me.Controls.Add(Me.TextBox1)
        Me.Name = "TestApp"
        Me.Text = "Form1"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TextBoxEx1 As TextBoxEx
    Friend WithEvents TextBox1 As Windows.Forms.TextBox
End Class
