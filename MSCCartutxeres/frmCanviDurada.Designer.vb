<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCanviDurada
    Inherits MetroFramework.Forms.MetroForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCanviDurada))
        Me.cmdCancel = New MetroFramework.Controls.MetroTile()
        Me.cmdOK = New MetroFramework.Controls.MetroTile()
        Me.lbDurada = New MetroFramework.Controls.MetroLabel()
        Me.txtDurada = New System.Windows.Forms.DateTimePicker()
        Me.lbInfo = New MetroFramework.Controls.MetroLabel()
        Me.SuspendLayout()
        '
        'cmdCancel
        '
        Me.cmdCancel.ActiveControl = Nothing
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(18, 147)
        Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(83, 31)
        Me.cmdCancel.TabIndex = 7
        Me.cmdCancel.TileImage = CType(resources.GetObject("cmdCancel.TileImage"), System.Drawing.Image)
        Me.cmdCancel.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCancel.UseSelectable = True
        Me.cmdCancel.UseTileImage = True
        '
        'cmdOK
        '
        Me.cmdOK.ActiveControl = Nothing
        Me.cmdOK.Location = New System.Drawing.Point(109, 147)
        Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(86, 31)
        Me.cmdOK.TabIndex = 6
        Me.cmdOK.TileImage = CType(resources.GetObject("cmdOK.TileImage"), System.Drawing.Image)
        Me.cmdOK.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdOK.UseSelectable = True
        Me.cmdOK.UseTileImage = True
        '
        'lbDurada
        '
        Me.lbDurada.AutoSize = True
        Me.lbDurada.Location = New System.Drawing.Point(18, 111)
        Me.lbDurada.Name = "lbDurada"
        Me.lbDurada.Size = New System.Drawing.Size(80, 19)
        Me.lbDurada.TabIndex = 5
        Me.lbDurada.Text = "Max durada"
        '
        'txtDurada
        '
        Me.txtDurada.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.txtDurada.Location = New System.Drawing.Point(111, 110)
        Me.txtDurada.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtDurada.Name = "txtDurada"
        Me.txtDurada.ShowUpDown = True
        Me.txtDurada.Size = New System.Drawing.Size(84, 20)
        Me.txtDurada.TabIndex = 4
        Me.txtDurada.Value = New Date(2019, 1, 21, 0, 0, 0, 0)
        '
        'lbInfo
        '
        Me.lbInfo.Location = New System.Drawing.Point(23, 60)
        Me.lbInfo.Name = "lbInfo"
        Me.lbInfo.Size = New System.Drawing.Size(172, 46)
        Me.lbInfo.TabIndex = 8
        Me.lbInfo.Text = "Durada màxima de la reprodució en streaming"
        Me.lbInfo.WrapToLine = True
        '
        'frmCanviDurada
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(235, 196)
        Me.Controls.Add(Me.lbInfo)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.lbDurada)
        Me.Controls.Add(Me.txtDurada)
        Me.Name = "frmCanviDurada"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "frmCanviDurada"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents cmdCancel As MetroFramework.Controls.MetroTile
    Friend WithEvents cmdOK As MetroFramework.Controls.MetroTile
    Friend WithEvents txtDurada As DateTimePicker
    Friend WithEvents lbDurada As MetroFramework.Controls.MetroLabel
    Friend WithEvents lbInfo As MetroFramework.Controls.MetroLabel
End Class
