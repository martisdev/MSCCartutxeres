'
' Creado por SharpDevelop.
' Usuario: nomai_000
' Fecha: 03/02/2016
' Hora: 0:24
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Partial Class frmHistory
    Inherits MetroFramework.Forms.MetroForm

    ''' <summary>
    ''' Designer variable used to keep track of non-visual components.
    ''' </summary>
    Private components As System.ComponentModel.IContainer

    ''' <summary>
    ''' Disposes resources used by the form.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ''' <summary>
    ''' This method is required for Windows Forms designer support.
    ''' Do not change the method contents inside the source code editor. The Forms designer might
    ''' not be able to load this method if it was changed manually.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHistory))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.tabControl1 = New MetroFramework.Controls.MetroTabControl()
        Me.tabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.lbTitol = New MetroFramework.Controls.MetroLabel()
        Me.flowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.panelImage = New System.Windows.Forms.Panel()
        Me.picDetall = New System.Windows.Forms.PictureBox()
        Me.lbDetallInfo = New MetroFramework.Controls.MetroLabel()
        Me.tabPage2 = New MetroFramework.Controls.MetroTabPage()
        Me.lstDispHistory = New MetroFramework.Controls.MetroGrid()
        Me.HtmlToolTip1 = New MetroFramework.Drawing.Html.HtmlToolTip()
        Me.ColumnTitol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnInterp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnDurada = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnRadiacio = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnRitme = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnTipus = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnPath = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.tabControl1.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.flowLayoutPanel1.SuspendLayout()
        Me.panelImage.SuspendLayout()
        CType(Me.picDetall, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPage2.SuspendLayout()
        CType(Me.lstDispHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ImageList
        '
        Me.ImageList.ImageStream = CType(resources.GetObject("ImageList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList.TransparentColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ImageList.Images.SetKeyName(0, "music")
        Me.ImageList.Images.SetKeyName(1, "promo")
        Me.ImageList.Images.SetKeyName(2, "publi")
        Me.ImageList.Images.SetKeyName(3, "jingel")
        Me.ImageList.Images.SetKeyName(4, "program")
        Me.ImageList.Images.SetKeyName(5, "Fx")
        Me.ImageList.Images.SetKeyName(6, "musicOK")
        Me.ImageList.Images.SetKeyName(7, "promoOK")
        Me.ImageList.Images.SetKeyName(8, "publiOK")
        Me.ImageList.Images.SetKeyName(9, "jingelOK")
        Me.ImageList.Images.SetKeyName(10, "programOK")
        Me.ImageList.Images.SetKeyName(11, "FxOk")
        '
        'tabControl1
        '
        Me.tabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Location = New System.Drawing.Point(4, 22)
        Me.tabControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(333, 518)
        Me.tabControl1.TabIndex = 15
        Me.tabControl1.UseSelectable = True
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.lbTitol)
        Me.tabPage1.Controls.Add(Me.flowLayoutPanel1)
        Me.tabPage1.HorizontalScrollbarBarColor = True
        Me.tabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.tabPage1.HorizontalScrollbarSize = 2
        Me.tabPage1.Location = New System.Drawing.Point(4, 38)
        Me.tabPage1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabPage1.Size = New System.Drawing.Size(325, 476)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "Detall"
        Me.tabPage1.UseVisualStyleBackColor = True
        Me.tabPage1.VerticalScrollbarBarColor = True
        Me.tabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.tabPage1.VerticalScrollbarSize = 3
        '
        'lbTitol
        '
        Me.lbTitol.AutoSize = True
        Me.lbTitol.FontSize = MetroFramework.MetroLabelSize.Tall
        Me.lbTitol.Location = New System.Drawing.Point(5, 5)
        Me.lbTitol.Name = "lbTitol"
        Me.lbTitol.Size = New System.Drawing.Size(0, 0)
        Me.lbTitol.TabIndex = 17
        '
        'flowLayoutPanel1
        '
        Me.flowLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flowLayoutPanel1.Controls.Add(Me.panelImage)
        Me.flowLayoutPanel1.Controls.Add(Me.lbDetallInfo)
        Me.flowLayoutPanel1.Location = New System.Drawing.Point(0, 34)
        Me.flowLayoutPanel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.flowLayoutPanel1.Name = "flowLayoutPanel1"
        Me.flowLayoutPanel1.Size = New System.Drawing.Size(319, 442)
        Me.flowLayoutPanel1.TabIndex = 16
        '
        'panelImage
        '
        Me.panelImage.Controls.Add(Me.picDetall)
        Me.panelImage.Location = New System.Drawing.Point(3, 4)
        Me.panelImage.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.panelImage.Name = "panelImage"
        Me.panelImage.Size = New System.Drawing.Size(250, 250)
        Me.panelImage.TabIndex = 0
        '
        'picDetall
        '
        Me.picDetall.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picDetall.Location = New System.Drawing.Point(9, 8)
        Me.picDetall.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picDetall.Name = "picDetall"
        Me.picDetall.Size = New System.Drawing.Size(229, 228)
        Me.picDetall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picDetall.TabIndex = 0
        Me.picDetall.TabStop = False
        '
        'lbDetallInfo
        '
        Me.lbDetallInfo.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lbDetallInfo.AutoSize = True
        Me.lbDetallInfo.Location = New System.Drawing.Point(259, 129)
        Me.lbDetallInfo.Name = "lbDetallInfo"
        Me.lbDetallInfo.Size = New System.Drawing.Size(0, 0)
        Me.lbDetallInfo.TabIndex = 0
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.lstDispHistory)
        Me.tabPage2.HorizontalScrollbarBarColor = True
        Me.tabPage2.HorizontalScrollbarHighlightOnWheel = False
        Me.tabPage2.HorizontalScrollbarSize = 2
        Me.tabPage2.Location = New System.Drawing.Point(4, 38)
        Me.tabPage2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.tabPage2.Size = New System.Drawing.Size(325, 476)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "Historial"
        Me.tabPage2.UseVisualStyleBackColor = True
        Me.tabPage2.VerticalScrollbarBarColor = True
        Me.tabPage2.VerticalScrollbarHighlightOnWheel = False
        Me.tabPage2.VerticalScrollbarSize = 3
        '
        'lstDispHistory
        '
        Me.lstDispHistory.AllowUserToAddRows = False
        Me.lstDispHistory.AllowUserToDeleteRows = False
        Me.lstDispHistory.AllowUserToResizeRows = False
        Me.lstDispHistory.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.lstDispHistory.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lstDispHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.lstDispHistory.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.lstDispHistory.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.lstDispHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.lstDispHistory.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnTitol, Me.ColumnInterp, Me.ColumnDurada, Me.ColumnRadiacio, Me.ColumnRitme, Me.ColumnID, Me.ColumnTipus, Me.ColumnPath})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.lstDispHistory.DefaultCellStyle = DataGridViewCellStyle2
        Me.lstDispHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lstDispHistory.EnableHeadersVisualStyles = False
        Me.lstDispHistory.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.lstDispHistory.GridColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.lstDispHistory.Location = New System.Drawing.Point(3, 4)
        Me.lstDispHistory.Name = "lstDispHistory"
        Me.lstDispHistory.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.lstDispHistory.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.lstDispHistory.RowHeadersVisible = False
        Me.lstDispHistory.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.lstDispHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.lstDispHistory.Size = New System.Drawing.Size(319, 468)
        Me.lstDispHistory.TabIndex = 2
        Me.lstDispHistory.Theme = MetroFramework.MetroThemeStyle.Dark
        '
        'HtmlToolTip1
        '
        Me.HtmlToolTip1.OwnerDraw = True
        '
        'ColumnTitol
        '
        Me.ColumnTitol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColumnTitol.HeaderText = "Titol"
        Me.ColumnTitol.MinimumWidth = 100
        Me.ColumnTitol.Name = "ColumnTitol"
        Me.ColumnTitol.ReadOnly = True
        '
        'ColumnInterp
        '
        Me.ColumnInterp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColumnInterp.HeaderText = "Interp"
        Me.ColumnInterp.MinimumWidth = 100
        Me.ColumnInterp.Name = "ColumnInterp"
        Me.ColumnInterp.ReadOnly = True
        '
        'ColumnDurada
        '
        Me.ColumnDurada.HeaderText = "Durada"
        Me.ColumnDurada.Name = "ColumnDurada"
        Me.ColumnDurada.ReadOnly = True
        Me.ColumnDurada.Width = 70
        '
        'ColumnRadiacio
        '
        Me.ColumnRadiacio.HeaderText = "Hora radiació"
        Me.ColumnRadiacio.Name = "ColumnRadiacio"
        Me.ColumnRadiacio.ReadOnly = True
        Me.ColumnRadiacio.Width = 60
        '
        'ColumnRitme
        '
        Me.ColumnRitme.HeaderText = "Ritme"
        Me.ColumnRitme.Name = "ColumnRitme"
        Me.ColumnRitme.ReadOnly = True
        Me.ColumnRitme.Visible = False
        Me.ColumnRitme.Width = 50
        '
        'ColumnID
        '
        Me.ColumnID.HeaderText = "ID"
        Me.ColumnID.Name = "ColumnID"
        Me.ColumnID.Visible = False
        '
        'ColumnTipus
        '
        Me.ColumnTipus.HeaderText = "Tipus"
        Me.ColumnTipus.Name = "ColumnTipus"
        Me.ColumnTipus.Visible = False
        '
        'ColumnPath
        '
        Me.ColumnPath.HeaderText = "Path"
        Me.ColumnPath.Name = "ColumnPath"
        Me.ColumnPath.Visible = False
        '
        'frmHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(339, 558)
        Me.Controls.Add(Me.tabControl1)
        Me.Font = New System.Drawing.Font("Segoe UI Symbol", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(233, 262)
        Me.Name = "frmHistory"
        Me.Padding = New System.Windows.Forms.Padding(23, 78, 23, 26)
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Historial de reproducció"
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        Me.tabPage1.PerformLayout()
        Me.flowLayoutPanel1.ResumeLayout(False)
        Me.flowLayoutPanel1.PerformLayout()
        Me.panelImage.ResumeLayout(False)
        CType(Me.picDetall, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPage2.ResumeLayout(False)
        CType(Me.lstDispHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Private panelImage As System.Windows.Forms.Panel
    Private flowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Private picDetall As System.Windows.Forms.PictureBox
    Public ImageList As System.Windows.Forms.ImageList
    Private WithEvents tabPage2 As MetroFramework.Controls.MetroTabPage
    Private WithEvents tabPage1 As MetroFramework.Controls.MetroTabPage
    Private WithEvents tabControl1 As MetroFramework.Controls.MetroTabControl
    Private WithEvents lbDetallInfo As MetroFramework.Controls.MetroLabel
    Friend WithEvents lbTitol As MetroFramework.Controls.MetroLabel
    Friend WithEvents HtmlToolTip1 As MetroFramework.Drawing.Html.HtmlToolTip
    Friend WithEvents lstDispHistory As MetroFramework.Controls.MetroGrid
    Friend WithEvents ColumnTitol As DataGridViewTextBoxColumn
    Friend WithEvents ColumnInterp As DataGridViewTextBoxColumn
    Friend WithEvents ColumnDurada As DataGridViewTextBoxColumn
    Friend WithEvents ColumnRadiacio As DataGridViewTextBoxColumn
    Friend WithEvents ColumnRitme As DataGridViewTextBoxColumn
    Friend WithEvents ColumnID As DataGridViewTextBoxColumn
    Friend WithEvents ColumnTipus As DataGridViewTextBoxColumn
    Friend WithEvents ColumnPath As DataGridViewTextBoxColumn
End Class
