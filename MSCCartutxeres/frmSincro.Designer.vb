<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSincro
    Inherits MetroFramework.Forms.MetroForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSincro))
        Me.txtHoraEnd = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmdOK = New MetroFramework.Controls.MetroTile()
        Me.cmdCancel = New MetroFramework.Controls.MetroTile()
        Me.SuspendLayout()
        '
        'txtHoraEnd
        '
        Me.txtHoraEnd.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.txtHoraEnd.Location = New System.Drawing.Point(119, 59)
        Me.txtHoraEnd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtHoraEnd.Name = "txtHoraEnd"
        Me.txtHoraEnd.ShowUpDown = True
        Me.txtHoraEnd.Size = New System.Drawing.Size(84, 25)
        Me.txtHoraEnd.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 63)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 17)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Hora final..."
        '
        'cmdOK
        '
        Me.cmdOK.ActiveControl = Nothing
        Me.cmdOK.Location = New System.Drawing.Point(117, 94)
        Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(86, 31)
        Me.cmdOK.TabIndex = 2
        Me.cmdOK.TileImage = CType(resources.GetObject("cmdOK.TileImage"), System.Drawing.Image)
        Me.cmdOK.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdOK.UseSelectable = True
        Me.cmdOK.UseTileImage = True
        '
        'cmdCancel
        '
        Me.cmdCancel.ActiveControl = Nothing
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(26, 94)
        Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(83, 31)
        Me.cmdCancel.TabIndex = 3
        Me.cmdCancel.TileImage = CType(resources.GetObject("cmdCancel.TileImage"), System.Drawing.Image)
        Me.cmdCancel.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCancel.UseSelectable = True
        Me.cmdCancel.UseTileImage = True
        '
        'frmSincro
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(226, 149)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtHoraEnd)
        Me.Font = New System.Drawing.Font("Segoe UI Symbol", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmSincro"
        Me.Padding = New System.Windows.Forms.Padding(23, 78, 23, 26)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Hora final"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtHoraEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmdOK As MetroFramework.Controls.MetroTile
    Friend WithEvents cmdCancel As MetroFramework.Controls.MetroTile
End Class
