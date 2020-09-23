'
' Creado por SharpDevelop.
' Usuario: Martí
' Fecha: 25/01/2017
' Hora: 17:02
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Partial Class frmFileExplorer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFileExplorer))
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.treeView1 = New System.Windows.Forms.TreeView()
        Me.imageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.listViewFitxers = New System.Windows.Forms.ListView()
        Me.colNom = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colInterp = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cloDurada = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStripBotoDret = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuPreEscolta = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPreescoltaStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAddRepreoduccio = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuEditAudio = New System.Windows.Forms.ToolStripMenuItem()
        Me.comboBoxPath = New System.Windows.Forms.ComboBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.buttonRefresca = New MetroFramework.Controls.MetroTile()
        Me.buttonCancel = New MetroFramework.Controls.MetroTile()
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.ContextMenuStripBotoDret.SuspendLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'splitContainer1
        '
        Me.splitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splitContainer1.Location = New System.Drawing.Point(37, 108)
        Me.splitContainer1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.treeView1)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.listViewFitxers)
        Me.splitContainer1.Size = New System.Drawing.Size(719, 333)
        Me.splitContainer1.SplitterDistance = 237
        Me.splitContainer1.SplitterWidth = 5
        Me.splitContainer1.TabIndex = 0
        '
        'treeView1
        '
        Me.treeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.treeView1.ImageIndex = 0
        Me.treeView1.ImageList = Me.imageList1
        Me.treeView1.Location = New System.Drawing.Point(0, 0)
        Me.treeView1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.treeView1.Name = "treeView1"
        Me.treeView1.SelectedImageIndex = 0
        Me.treeView1.Size = New System.Drawing.Size(237, 333)
        Me.treeView1.TabIndex = 0
        '
        'imageList1
        '
        Me.imageList1.ImageStream = CType(resources.GetObject("imageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.imageList1.Images.SetKeyName(0, "carpeta.ico")
        Me.imageList1.Images.SetKeyName(1, "sound.png")
        Me.imageList1.Images.SetKeyName(2, "sound_blue.png")
        Me.imageList1.Images.SetKeyName(3, "sound_green.png")
        Me.imageList1.Images.SetKeyName(4, "sound_red.png")
        Me.imageList1.Images.SetKeyName(5, "sound_yellow.png")
        Me.imageList1.Images.SetKeyName(6, "blog_post.png")
        Me.imageList1.Images.SetKeyName(7, "blog_post_red.png")
        '
        'listViewFitxers
        '
        Me.listViewFitxers.AllowDrop = True
        Me.listViewFitxers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colNom, Me.colInterp, Me.cloDurada})
        Me.listViewFitxers.ContextMenuStrip = Me.ContextMenuStripBotoDret
        Me.listViewFitxers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.listViewFitxers.FullRowSelect = True
        Me.listViewFitxers.Location = New System.Drawing.Point(0, 0)
        Me.listViewFitxers.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.listViewFitxers.Name = "listViewFitxers"
        Me.listViewFitxers.Size = New System.Drawing.Size(477, 333)
        Me.listViewFitxers.SmallImageList = Me.imageList1
        Me.listViewFitxers.TabIndex = 0
        Me.listViewFitxers.UseCompatibleStateImageBehavior = False
        Me.listViewFitxers.View = System.Windows.Forms.View.Details
        '
        'colNom
        '
        Me.colNom.Text = "Nom"
        Me.colNom.Width = 200
        '
        'colInterp
        '
        Me.colInterp.Text = "Intèrpret"
        Me.colInterp.Width = 150
        '
        'cloDurada
        '
        Me.cloDurada.Text = "Durada"
        '
        'ContextMenuStripBotoDret
        '
        Me.ContextMenuStripBotoDret.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPreEscolta, Me.mnuPreescoltaStop, Me.ToolStripSeparator20, Me.mnuAddRepreoduccio, Me.ToolStripSeparator1, Me.mnuEditAudio})
        Me.ContextMenuStripBotoDret.Name = "ContextMenuStripBotoDret"
        Me.ContextMenuStripBotoDret.Size = New System.Drawing.Size(229, 104)
        '
        'mnuPreEscolta
        '
        Me.mnuPreEscolta.Image = CType(resources.GetObject("mnuPreEscolta.Image"), System.Drawing.Image)
        Me.mnuPreEscolta.Name = "mnuPreEscolta"
        Me.mnuPreEscolta.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F5), System.Windows.Forms.Keys)
        Me.mnuPreEscolta.Size = New System.Drawing.Size(228, 22)
        Me.mnuPreEscolta.Text = "Pre-escolta PLAY"
        '
        'mnuPreescoltaStop
        '
        Me.mnuPreescoltaStop.Image = CType(resources.GetObject("mnuPreescoltaStop.Image"), System.Drawing.Image)
        Me.mnuPreescoltaStop.Name = "mnuPreescoltaStop"
        Me.mnuPreescoltaStop.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7), System.Windows.Forms.Keys)
        Me.mnuPreescoltaStop.Size = New System.Drawing.Size(228, 22)
        Me.mnuPreescoltaStop.Text = "Pre-escolta STOP"
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(225, 6)
        '
        'mnuAddRepreoduccio
        '
        Me.mnuAddRepreoduccio.Name = "mnuAddRepreoduccio"
        Me.mnuAddRepreoduccio.Size = New System.Drawing.Size(228, 22)
        Me.mnuAddRepreoduccio.Text = "Afegir a reproducció"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(225, 6)
        '
        'mnuEditAudio
        '
        Me.mnuEditAudio.Enabled = False
        Me.mnuEditAudio.Name = "mnuEditAudio"
        Me.mnuEditAudio.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.mnuEditAudio.Size = New System.Drawing.Size(228, 22)
        Me.mnuEditAudio.Text = "Editar l'àudio"
        '
        'comboBoxPath
        '
        Me.comboBoxPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.comboBoxPath.FormattingEnabled = True
        Me.comboBoxPath.Location = New System.Drawing.Point(61, 81)
        Me.comboBoxPath.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.comboBoxPath.Name = "comboBoxPath"
        Me.comboBoxPath.Size = New System.Drawing.Size(616, 25)
        Me.comboBoxPath.TabIndex = 1
        '
        'pictureBox1
        '
        Me.pictureBox1.Image = CType(resources.GetObject("pictureBox1.Image"), System.Drawing.Image)
        Me.pictureBox1.Location = New System.Drawing.Point(37, 80)
        Me.pictureBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(22, 27)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBox1.TabIndex = 2
        Me.pictureBox1.TabStop = False
        '
        'buttonRefresca
        '
        Me.buttonRefresca.ActiveControl = Nothing
        Me.buttonRefresca.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonRefresca.Location = New System.Drawing.Point(681, 80)
        Me.buttonRefresca.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.buttonRefresca.Name = "buttonRefresca"
        Me.buttonRefresca.Size = New System.Drawing.Size(35, 27)
        Me.buttonRefresca.TabIndex = 3
        Me.buttonRefresca.TileImage = CType(resources.GetObject("buttonRefresca.TileImage"), System.Drawing.Image)
        Me.buttonRefresca.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.buttonRefresca.UseSelectable = True
        Me.buttonRefresca.UseTileImage = True
        '
        'buttonCancel
        '
        Me.buttonCancel.ActiveControl = Nothing
        Me.buttonCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.buttonCancel.Location = New System.Drawing.Point(721, 80)
        Me.buttonCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.buttonCancel.Name = "buttonCancel"
        Me.buttonCancel.Size = New System.Drawing.Size(35, 27)
        Me.buttonCancel.TabIndex = 4
        Me.buttonCancel.TileImage = CType(resources.GetObject("buttonCancel.TileImage"), System.Drawing.Image)
        Me.buttonCancel.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.buttonCancel.UseSelectable = True
        Me.buttonCancel.UseTileImage = True
        '
        'frmFileExplorer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 462)
        Me.Controls.Add(Me.buttonCancel)
        Me.Controls.Add(Me.buttonRefresca)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.comboBoxPath)
        Me.Controls.Add(Me.splitContainer1)
        Me.Font = New System.Drawing.Font("Segoe UI Symbol", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "frmFileExplorer"
        Me.Padding = New System.Windows.Forms.Padding(23, 78, 23, 26)
        Me.Text = "Explorador de fitxers"
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer1.ResumeLayout(False)
        Me.ContextMenuStripBotoDret.ResumeLayout(False)
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents mnuEditAudio As System.Windows.Forms.ToolStripMenuItem
    Friend ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
	Friend mnuAddRepreoduccio As System.Windows.Forms.ToolStripMenuItem
	Friend ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPreescoltaStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPreEscolta As System.Windows.Forms.ToolStripMenuItem
    Friend ContextMenuStripBotoDret As System.Windows.Forms.ContextMenuStrip
	Private cloDurada As System.Windows.Forms.ColumnHeader
	Private colInterp As System.Windows.Forms.ColumnHeader
    Private pictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents comboBoxPath As System.Windows.Forms.ComboBox
    Private colNom As System.Windows.Forms.ColumnHeader
    Friend WithEvents listViewFitxers As System.Windows.Forms.ListView
    Private imageList1 As System.Windows.Forms.ImageList
    Private WithEvents treeView1 As System.Windows.Forms.TreeView
    Private splitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents buttonRefresca As MetroFramework.Controls.MetroTile
    Private WithEvents buttonCancel As MetroFramework.Controls.MetroTile
End Class
