<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddMark
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddMark))
        Me.lbComment = New MetroFramework.Controls.MetroLabel()
        Me.txtCommentMark = New MetroFramework.Controls.MetroTextBox()
        Me.cmdCancel = New MetroFramework.Controls.MetroTile()
        Me.cmdOK = New MetroFramework.Controls.MetroTile()
        Me.SuspendLayout()
        '
        'lbComment
        '
        Me.lbComment.AutoSize = True
        Me.lbComment.Location = New System.Drawing.Point(23, 60)
        Me.lbComment.Name = "lbComment"
        Me.lbComment.Size = New System.Drawing.Size(71, 19)
        Me.lbComment.TabIndex = 0
        Me.lbComment.Text = "Comment:"
        '
        'txtCommentMark
        '
        Me.txtCommentMark.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        '
        '
        '
        Me.txtCommentMark.CustomButton.Image = Nothing
        Me.txtCommentMark.CustomButton.Location = New System.Drawing.Point(156, 2)
        Me.txtCommentMark.CustomButton.Name = ""
        Me.txtCommentMark.CustomButton.Size = New System.Drawing.Size(141, 141)
        Me.txtCommentMark.CustomButton.Style = MetroFramework.MetroColorStyle.Blue
        Me.txtCommentMark.CustomButton.TabIndex = 1
        Me.txtCommentMark.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light
        Me.txtCommentMark.CustomButton.UseSelectable = True
        Me.txtCommentMark.CustomButton.Visible = False
        Me.txtCommentMark.Lines = New String(-1) {}
        Me.txtCommentMark.Location = New System.Drawing.Point(23, 87)
        Me.txtCommentMark.MaxLength = 300
        Me.txtCommentMark.Multiline = True
        Me.txtCommentMark.Name = "txtCommentMark"
        Me.txtCommentMark.PasswordChar = Global.Microsoft.VisualBasic.ChrW(0)
        Me.txtCommentMark.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.txtCommentMark.SelectedText = ""
        Me.txtCommentMark.SelectionLength = 0
        Me.txtCommentMark.SelectionStart = 0
        Me.txtCommentMark.ShortcutsEnabled = True
        Me.txtCommentMark.Size = New System.Drawing.Size(300, 146)
        Me.txtCommentMark.TabIndex = 1
        Me.txtCommentMark.UseSelectable = True
        Me.txtCommentMark.WaterMarkColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        Me.txtCommentMark.WaterMarkFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel)
        '
        'cmdCancel
        '
        Me.cmdCancel.ActiveControl = Nothing
        Me.cmdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(98, 240)
        Me.cmdCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.Size = New System.Drawing.Size(83, 31)
        Me.cmdCancel.TabIndex = 5
        Me.cmdCancel.TileImage = CType(resources.GetObject("cmdCancel.TileImage"), System.Drawing.Image)
        Me.cmdCancel.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCancel.UseSelectable = True
        Me.cmdCancel.UseTileImage = True
        '
        'cmdOK
        '
        Me.cmdOK.ActiveControl = Nothing
        Me.cmdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdOK.Location = New System.Drawing.Point(189, 240)
        Me.cmdOK.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(86, 31)
        Me.cmdOK.TabIndex = 4
        Me.cmdOK.TileImage = CType(resources.GetObject("cmdOK.TileImage"), System.Drawing.Image)
        Me.cmdOK.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdOK.UseSelectable = True
        Me.cmdOK.UseTileImage = True
        '
        'frmAddMark
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(350, 295)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.txtCommentMark)
        Me.Controls.Add(Me.lbComment)
        Me.Name = "frmAddMark"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Comment to mark"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lbComment As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtCommentMark As MetroFramework.Controls.MetroTextBox
    Friend WithEvents cmdCancel As MetroFramework.Controls.MetroTile
    Friend WithEvents cmdOK As MetroFramework.Controls.MetroTile
End Class
