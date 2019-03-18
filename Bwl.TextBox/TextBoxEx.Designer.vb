<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TextBoxEx
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
        Me.components = New System.ComponentModel.Container()
        Me.cursorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'cursorTimer
        '
        Me.cursorTimer.Enabled = True
        Me.cursorTimer.Interval = 500
        '
        'TextBoxEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Name = "TextBoxEx"
        Me.Size = New System.Drawing.Size(592, 418)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents cursorTimer As Windows.Forms.Timer
End Class
