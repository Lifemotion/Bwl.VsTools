<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TextBoxEx
    Inherits System.Windows.Forms.UserControl

    'Пользовательский элемент управления (UserControl) переопределяет метод Dispose для очистки списка компонентов.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
                UserDispose
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
        Me.CursorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.VScrollBar1 = New System.Windows.Forms.VScrollBar()
        Me.HScrollBar1 = New System.Windows.Forms.HScrollBar()
        Me.InputTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'CursorTimer
        '
        Me.CursorTimer.Enabled = True
        Me.CursorTimer.Interval = 500
        '
        'VScrollBar1
        '
        Me.VScrollBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.VScrollBar1.Location = New System.Drawing.Point(543, 0)
        Me.VScrollBar1.Name = "VScrollBar1"
        Me.VScrollBar1.Size = New System.Drawing.Size(17, 343)
        Me.VScrollBar1.TabIndex = 0
        '
        'HScrollBar1
        '
        Me.HScrollBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.HScrollBar1.Location = New System.Drawing.Point(0, 343)
        Me.HScrollBar1.Name = "HScrollBar1"
        Me.HScrollBar1.Size = New System.Drawing.Size(543, 19)
        Me.HScrollBar1.TabIndex = 1
        '
        'InputTimer
        '
        Me.InputTimer.Interval = 1000
        '
        'TextBoxEx
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.HScrollBar1)
        Me.Controls.Add(Me.VScrollBar1)
        Me.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.Name = "TextBoxEx"
        Me.Size = New System.Drawing.Size(560, 362)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CursorTimer As Windows.Forms.Timer
    Friend WithEvents VScrollBar1 As Windows.Forms.VScrollBar
    Friend WithEvents HScrollBar1 As Windows.Forms.HScrollBar
    Friend WithEvents InputTimer As Windows.Forms.Timer
End Class
