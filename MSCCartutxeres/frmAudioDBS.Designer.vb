<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAudioDBS
    Inherits MetroFramework.Forms.MetroForm

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAudioDBS))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.tabAudioDBS = New MetroFramework.Controls.MetroTabControl()
        Me.TabPage1 = New MetroFramework.Controls.MetroTabPage()
        Me.comboBuscaTema = New System.Windows.Forms.ComboBox()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.cmbBuscar = New System.Windows.Forms.ToolStripSplitButton()
        Me.findPerTitol = New System.Windows.Forms.ToolStripMenuItem()
        Me.findPerInterp = New System.Windows.Forms.ToolStripMenuItem()
        Me.findPerDisc = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.RecercaAvançadaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtBuscaTema = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.mnuColumnVisibleRadia = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnVisibleAny = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnVisibleRitme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnVisibleSubj = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnVisibleTemp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnVisibleIdioma = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnVisibleEstil = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.LlistatToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLimitRegChec = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLimitRecord = New System.Windows.Forms.ToolStripTextBox()
        Me.gridTemes = New MetroFramework.Controls.MetroGrid()
        Me.ColumnID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnTitol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.comboInterp = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.comboDisc = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnIdioma = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ColumnTemp = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ColumnSub = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ColumnRitme = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnAny = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnRadia = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColumnFav = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ContextMenuStripBotoDret = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMesInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPreEscolta = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPreescoltaStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAddRepreoduccio = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuEditAudio = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumns = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSeparadorFav = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAddDelFav = New System.Windows.Forms.ToolStripMenuItem()
        Me.TabPage4 = New MetroFramework.Controls.MetroTabPage()
        Me.cmbHora = New System.Windows.Forms.ComboBox()
        Me.txtData = New System.Windows.Forms.DateTimePicker()
        Me.ToolStrip4 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.gridPubli = New MetroFramework.Controls.MetroGrid()
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.comboClient = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.comboLocutor = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Column13 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdSortir = New System.Windows.Forms.ToolStripButton()
        Me.mnuPosicioPestanyes = New System.Windows.Forms.ToolStripSplitButton()
        Me.mnuPestanyaSobre = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPestanyaSota = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuShowFolders = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripDropDownButton2 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mnuIniSessio = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEndSessio = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMareCarpetesUser = New System.Windows.Forms.ToolStripDropDownButton()
        Me.lbAtencioPubli = New System.Windows.Forms.ToolStripLabel()
        Me.tmr_publi = New System.Windows.Forms.Timer(Me.components)
        Me.ImageListFavorites = New System.Windows.Forms.ImageList(Me.components)
        Me.tabAudioDBS.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.gridTemes, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStripBotoDret.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.ToolStrip4.SuspendLayout()
        CType(Me.gridPubli, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tabAudioDBS
        '
        Me.tabAudioDBS.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabAudioDBS.Controls.Add(Me.TabPage1)
        Me.tabAudioDBS.Controls.Add(Me.TabPage4)
        Me.tabAudioDBS.Cursor = System.Windows.Forms.Cursors.Default
        Me.tabAudioDBS.Location = New System.Drawing.Point(20, 88)
        Me.tabAudioDBS.Multiline = True
        Me.tabAudioDBS.Name = "tabAudioDBS"
        Me.tabAudioDBS.SelectedIndex = 0
        Me.tabAudioDBS.Size = New System.Drawing.Size(376, 389)
        Me.tabAudioDBS.TabIndex = 3
        Me.tabAudioDBS.Tag = ""
        Me.tabAudioDBS.Theme = MetroFramework.MetroThemeStyle.Dark
        Me.tabAudioDBS.UseSelectable = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.comboBuscaTema)
        Me.TabPage1.Controls.Add(Me.ToolStrip2)
        Me.TabPage1.Controls.Add(Me.gridTemes)
        Me.TabPage1.HorizontalScrollbarBarColor = True
        Me.TabPage1.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage1.HorizontalScrollbarSize = 10
        Me.TabPage1.ImageIndex = 0
        Me.TabPage1.Location = New System.Drawing.Point(4, 38)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(368, 347)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Tag = "NO_DELETE"
        Me.TabPage1.Text = "Música"
        Me.TabPage1.UseVisualStyleBackColor = True
        Me.TabPage1.VerticalScrollbarBarColor = True
        Me.TabPage1.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage1.VerticalScrollbarSize = 10
        '
        'comboBuscaTema
        '
        Me.comboBuscaTema.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.comboBuscaTema.FormattingEnabled = True
        Me.comboBuscaTema.Location = New System.Drawing.Point(47, 322)
        Me.comboBuscaTema.Name = "comboBuscaTema"
        Me.comboBuscaTema.Size = New System.Drawing.Size(150, 21)
        Me.comboBuscaTema.TabIndex = 4
        Me.comboBuscaTema.Visible = False
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmbBuscar, Me.txtBuscaTema, Me.ToolStripDropDownButton1, Me.ToolStripLabel1})
        Me.ToolStrip2.Location = New System.Drawing.Point(3, 319)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(362, 25)
        Me.ToolStrip2.TabIndex = 3
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'cmbBuscar
        '
        Me.cmbBuscar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmbBuscar.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.findPerTitol, Me.findPerInterp, Me.findPerDisc, Me.ToolStripSeparator2, Me.RecercaAvançadaToolStripMenuItem})
        Me.cmbBuscar.Image = CType(resources.GetObject("cmbBuscar.Image"), System.Drawing.Image)
        Me.cmbBuscar.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmbBuscar.Name = "cmbBuscar"
        Me.cmbBuscar.Size = New System.Drawing.Size(32, 22)
        Me.cmbBuscar.Text = "ToolStripSplitButton1"
        '
        'findPerTitol
        '
        Me.findPerTitol.Checked = True
        Me.findPerTitol.CheckState = System.Windows.Forms.CheckState.Checked
        Me.findPerTitol.Name = "findPerTitol"
        Me.findPerTitol.Size = New System.Drawing.Size(170, 22)
        Me.findPerTitol.Text = "Per Títol"
        '
        'findPerInterp
        '
        Me.findPerInterp.Name = "findPerInterp"
        Me.findPerInterp.Size = New System.Drawing.Size(170, 22)
        Me.findPerInterp.Text = "Per Intèrpret"
        '
        'findPerDisc
        '
        Me.findPerDisc.Name = "findPerDisc"
        Me.findPerDisc.Size = New System.Drawing.Size(170, 22)
        Me.findPerDisc.Text = "Per Disc"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(167, 6)
        '
        'RecercaAvançadaToolStripMenuItem
        '
        Me.RecercaAvançadaToolStripMenuItem.Name = "RecercaAvançadaToolStripMenuItem"
        Me.RecercaAvançadaToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.RecercaAvançadaToolStripMenuItem.Text = "Recerca Avançada"
        '
        'txtBuscaTema
        '
        Me.txtBuscaTema.Name = "txtBuscaTema"
        Me.txtBuscaTema.Size = New System.Drawing.Size(150, 25)
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColumnVisibleRadia, Me.mnuColumnVisibleAny, Me.mnuColumnVisibleRitme, Me.mnuColumnVisibleSubj, Me.mnuColumnVisibleTemp, Me.mnuColumnVisibleIdioma, Me.mnuColumnVisibleEstil})
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(131, 22)
        Me.ToolStripDropDownButton1.Text = "Columnes opcionals"
        '
        'mnuColumnVisibleRadia
        '
        Me.mnuColumnVisibleRadia.Name = "mnuColumnVisibleRadia"
        Me.mnuColumnVisibleRadia.Size = New System.Drawing.Size(156, 22)
        Me.mnuColumnVisibleRadia.Tag = "10"
        Me.mnuColumnVisibleRadia.Text = "Nº Radiacions"
        '
        'mnuColumnVisibleAny
        '
        Me.mnuColumnVisibleAny.Name = "mnuColumnVisibleAny"
        Me.mnuColumnVisibleAny.Size = New System.Drawing.Size(156, 22)
        Me.mnuColumnVisibleAny.Tag = "9"
        Me.mnuColumnVisibleAny.Text = "Any"
        '
        'mnuColumnVisibleRitme
        '
        Me.mnuColumnVisibleRitme.Name = "mnuColumnVisibleRitme"
        Me.mnuColumnVisibleRitme.Size = New System.Drawing.Size(156, 22)
        Me.mnuColumnVisibleRitme.Tag = "8"
        Me.mnuColumnVisibleRitme.Text = "Ritme"
        '
        'mnuColumnVisibleSubj
        '
        Me.mnuColumnVisibleSubj.Name = "mnuColumnVisibleSubj"
        Me.mnuColumnVisibleSubj.Size = New System.Drawing.Size(156, 22)
        Me.mnuColumnVisibleSubj.Tag = "7"
        Me.mnuColumnVisibleSubj.Text = "Clas. Subjectiva"
        '
        'mnuColumnVisibleTemp
        '
        Me.mnuColumnVisibleTemp.Name = "mnuColumnVisibleTemp"
        Me.mnuColumnVisibleTemp.Size = New System.Drawing.Size(156, 22)
        Me.mnuColumnVisibleTemp.Tag = "6"
        Me.mnuColumnVisibleTemp.Text = "Clas. Temporal"
        '
        'mnuColumnVisibleIdioma
        '
        Me.mnuColumnVisibleIdioma.Name = "mnuColumnVisibleIdioma"
        Me.mnuColumnVisibleIdioma.Size = New System.Drawing.Size(156, 22)
        Me.mnuColumnVisibleIdioma.Tag = "5"
        Me.mnuColumnVisibleIdioma.Text = "Idioma"
        '
        'mnuColumnVisibleEstil
        '
        Me.mnuColumnVisibleEstil.Name = "mnuColumnVisibleEstil"
        Me.mnuColumnVisibleEstil.Size = New System.Drawing.Size(156, 22)
        Me.mnuColumnVisibleEstil.Tag = "5"
        Me.mnuColumnVisibleEstil.Text = "Estil"
        Me.mnuColumnVisibleEstil.Visible = False
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LlistatToolStripMenuItem})
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(84, 19)
        Me.ToolStripLabel1.Text = "Presentació"
        '
        'LlistatToolStripMenuItem
        '
        Me.LlistatToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuLimitRegChec, Me.mnuLimitRecord})
        Me.LlistatToolStripMenuItem.Name = "LlistatToolStripMenuItem"
        Me.LlistatToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.LlistatToolStripMenuItem.Text = "Llistat Música (Max)"
        '
        'mnuLimitRegChec
        '
        Me.mnuLimitRegChec.Name = "mnuLimitRegChec"
        Me.mnuLimitRegChec.Size = New System.Drawing.Size(214, 22)
        Me.mnuLimitRegChec.Text = "Amb limitador de registres"
        '
        'mnuLimitRecord
        '
        Me.mnuLimitRecord.Name = "mnuLimitRecord"
        Me.mnuLimitRecord.Size = New System.Drawing.Size(100, 23)
        Me.mnuLimitRecord.Text = "100"
        '
        'gridTemes
        '
        Me.gridTemes.AllowDrop = True
        Me.gridTemes.AllowUserToAddRows = False
        Me.gridTemes.AllowUserToDeleteRows = False
        Me.gridTemes.AllowUserToOrderColumns = True
        Me.gridTemes.AllowUserToResizeRows = False
        Me.gridTemes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gridTemes.BackgroundColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.gridTemes.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.gridTemes.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.gridTemes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridTemes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.gridTemes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridTemes.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColumnID, Me.ColumnTitol, Me.comboInterp, Me.comboDisc, Me.Column3, Me.ColumnIdioma, Me.ColumnTemp, Me.ColumnSub, Me.ColumnRitme, Me.ColumnAny, Me.ColumnRadia, Me.ColumnFav})
        Me.gridTemes.ContextMenuStrip = Me.ContextMenuStripBotoDret
        Me.gridTemes.Cursor = System.Windows.Forms.Cursors.Default
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer), CType(CType(109, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridTemes.DefaultCellStyle = DataGridViewCellStyle2
        Me.gridTemes.EnableHeadersVisualStyles = False
        Me.gridTemes.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.gridTemes.GridColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        Me.gridTemes.Location = New System.Drawing.Point(-1, 0)
        Me.gridTemes.Name = "gridTemes"
        Me.gridTemes.ReadOnly = True
        Me.gridTemes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridTemes.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.gridTemes.RowHeadersVisible = False
        Me.gridTemes.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.gridTemes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridTemes.Size = New System.Drawing.Size(370, 316)
        Me.gridTemes.TabIndex = 2
        Me.gridTemes.Tag = "2"
        Me.gridTemes.Theme = MetroFramework.MetroThemeStyle.Dark
        '
        'ColumnID
        '
        Me.ColumnID.DataPropertyName = "tema_id"
        Me.ColumnID.HeaderText = "ID"
        Me.ColumnID.Name = "ColumnID"
        Me.ColumnID.ReadOnly = True
        Me.ColumnID.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColumnID.Visible = False
        '
        'ColumnTitol
        '
        Me.ColumnTitol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ColumnTitol.DataPropertyName = "tema_titol"
        Me.ColumnTitol.HeaderText = "Titol"
        Me.ColumnTitol.Name = "ColumnTitol"
        Me.ColumnTitol.ReadOnly = True
        '
        'comboInterp
        '
        Me.comboInterp.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.comboInterp.DataPropertyName = "tema_interp"
        Me.comboInterp.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.comboInterp.HeaderText = "Intèrpret"
        Me.comboInterp.Name = "comboInterp"
        Me.comboInterp.ReadOnly = True
        Me.comboInterp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'comboDisc
        '
        Me.comboDisc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.comboDisc.DataPropertyName = "tema_disc"
        Me.comboDisc.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.comboDisc.HeaderText = "Disc"
        Me.comboDisc.Name = "comboDisc"
        Me.comboDisc.ReadOnly = True
        Me.comboDisc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Column3
        '
        Me.Column3.DataPropertyName = "tema_durada"
        Me.Column3.HeaderText = "Durada"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 50
        '
        'ColumnIdioma
        '
        Me.ColumnIdioma.DataPropertyName = "tema_idioma"
        Me.ColumnIdioma.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ColumnIdioma.HeaderText = "Idioma"
        Me.ColumnIdioma.Name = "ColumnIdioma"
        Me.ColumnIdioma.ReadOnly = True
        Me.ColumnIdioma.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColumnIdioma.Visible = False
        '
        'ColumnTemp
        '
        Me.ColumnTemp.DataPropertyName = "tema_temps"
        Me.ColumnTemp.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ColumnTemp.HeaderText = "Clas. Temp"
        Me.ColumnTemp.Name = "ColumnTemp"
        Me.ColumnTemp.ReadOnly = True
        Me.ColumnTemp.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColumnTemp.Visible = False
        '
        'ColumnSub
        '
        Me.ColumnSub.DataPropertyName = "tema_subj"
        Me.ColumnSub.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.ColumnSub.HeaderText = "Clas. Subjectiva"
        Me.ColumnSub.Name = "ColumnSub"
        Me.ColumnSub.ReadOnly = True
        Me.ColumnSub.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColumnSub.Visible = False
        '
        'ColumnRitme
        '
        Me.ColumnRitme.DataPropertyName = "tema_ritme"
        Me.ColumnRitme.HeaderText = "Ritme"
        Me.ColumnRitme.Name = "ColumnRitme"
        Me.ColumnRitme.ReadOnly = True
        Me.ColumnRitme.Visible = False
        Me.ColumnRitme.Width = 50
        '
        'ColumnAny
        '
        Me.ColumnAny.DataPropertyName = "tema_any"
        Me.ColumnAny.HeaderText = "Any"
        Me.ColumnAny.Name = "ColumnAny"
        Me.ColumnAny.ReadOnly = True
        Me.ColumnAny.Visible = False
        Me.ColumnAny.Width = 50
        '
        'ColumnRadia
        '
        Me.ColumnRadia.DataPropertyName = "tema_numradiacions"
        Me.ColumnRadia.HeaderText = "Nº radiacions"
        Me.ColumnRadia.Name = "ColumnRadia"
        Me.ColumnRadia.ReadOnly = True
        Me.ColumnRadia.Visible = False
        Me.ColumnRadia.Width = 50
        '
        'ColumnFav
        '
        Me.ColumnFav.DataPropertyName = "temFav"
        Me.ColumnFav.HeaderText = "Favorites"
        Me.ColumnFav.Name = "ColumnFav"
        Me.ColumnFav.ReadOnly = True
        Me.ColumnFav.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.ColumnFav.Visible = False
        Me.ColumnFav.Width = 50
        '
        'ContextMenuStripBotoDret
        '
        Me.ContextMenuStripBotoDret.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMesInfo, Me.mnuPreEscolta, Me.mnuPreescoltaStop, Me.ToolStripSeparator20, Me.mnuAddRepreoduccio, Me.ToolStripSeparator1, Me.mnuEditAudio, Me.mnuColumns, Me.mnuSeparadorFav, Me.mnuAddDelFav})
        Me.ContextMenuStripBotoDret.Name = "ContextMenuStripBotoDret"
        Me.ContextMenuStripBotoDret.Size = New System.Drawing.Size(229, 176)
        '
        'mnuMesInfo
        '
        Me.mnuMesInfo.Image = CType(resources.GetObject("mnuMesInfo.Image"), System.Drawing.Image)
        Me.mnuMesInfo.Name = "mnuMesInfo"
        Me.mnuMesInfo.Size = New System.Drawing.Size(228, 22)
        Me.mnuMesInfo.Text = "Més Info ..."
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
        'mnuColumns
        '
        Me.mnuColumns.Name = "mnuColumns"
        Me.mnuColumns.Size = New System.Drawing.Size(228, 22)
        Me.mnuColumns.Text = "Ordenar columnes"
        '
        'mnuSeparadorFav
        '
        Me.mnuSeparadorFav.Name = "mnuSeparadorFav"
        Me.mnuSeparadorFav.Size = New System.Drawing.Size(225, 6)
        Me.mnuSeparadorFav.Visible = False
        '
        'mnuAddDelFav
        '
        Me.mnuAddDelFav.Name = "mnuAddDelFav"
        Me.mnuAddDelFav.Size = New System.Drawing.Size(228, 22)
        Me.mnuAddDelFav.Text = "afegir a favorites"
        Me.mnuAddDelFav.Visible = False
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.cmbHora)
        Me.TabPage4.Controls.Add(Me.txtData)
        Me.TabPage4.Controls.Add(Me.ToolStrip4)
        Me.TabPage4.Controls.Add(Me.gridPubli)
        Me.TabPage4.Controls.Add(Me.ToolStrip3)
        Me.TabPage4.HorizontalScrollbarBarColor = True
        Me.TabPage4.HorizontalScrollbarHighlightOnWheel = False
        Me.TabPage4.HorizontalScrollbarSize = 10
        Me.TabPage4.ImageIndex = 2
        Me.TabPage4.Location = New System.Drawing.Point(4, 38)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Size = New System.Drawing.Size(368, 347)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Tag = "NO_DELETE"
        Me.TabPage4.Text = "Publicitat"
        Me.TabPage4.UseVisualStyleBackColor = True
        Me.TabPage4.VerticalScrollbarBarColor = True
        Me.TabPage4.VerticalScrollbarHighlightOnWheel = False
        Me.TabPage4.VerticalScrollbarSize = 10
        '
        'cmbHora
        '
        Me.cmbHora.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbHora.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.cmbHora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbHora.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cmbHora.FormattingEnabled = True
        Me.cmbHora.Location = New System.Drawing.Point(55, 325)
        Me.cmbHora.Name = "cmbHora"
        Me.cmbHora.Size = New System.Drawing.Size(103, 21)
        Me.cmbHora.TabIndex = 7
        '
        'txtData
        '
        Me.txtData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtData.Location = New System.Drawing.Point(164, 325)
        Me.txtData.Name = "txtData"
        Me.txtData.Size = New System.Drawing.Size(192, 20)
        Me.txtData.TabIndex = 2
        '
        'ToolStrip4
        '
        Me.ToolStrip4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel2})
        Me.ToolStrip4.Location = New System.Drawing.Point(0, 322)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.Size = New System.Drawing.Size(368, 25)
        Me.ToolStrip4.TabIndex = 6
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(33, 22)
        Me.ToolStripLabel2.Text = "Bloc:"
        '
        'gridPubli
        '
        Me.gridPubli.AllowUserToAddRows = False
        Me.gridPubli.AllowUserToDeleteRows = False
        Me.gridPubli.AllowUserToResizeRows = False
        Me.gridPubli.BackgroundColor = System.Drawing.SystemColors.Control
        Me.gridPubli.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.gridPubli.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        Me.gridPubli.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridPubli.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.gridPubli.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.gridPubli.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column8, Me.Column9, Me.comboClient, Me.comboLocutor, Me.Column13})
        Me.gridPubli.ContextMenuStrip = Me.ContextMenuStripBotoDret
        Me.gridPubli.Cursor = System.Windows.Forms.Cursors.Default
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(136, Byte), Integer))
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.gridPubli.DefaultCellStyle = DataGridViewCellStyle5
        Me.gridPubli.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gridPubli.EnableHeadersVisualStyles = False
        Me.gridPubli.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        Me.gridPubli.GridColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.gridPubli.Location = New System.Drawing.Point(0, 0)
        Me.gridPubli.Name = "gridPubli"
        Me.gridPubli.ReadOnly = True
        Me.gridPubli.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(174, Byte), Integer), CType(CType(219, Byte), Integer))
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Segoe UI", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel)
        DataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(198, Byte), Integer), CType(CType(247, Byte), Integer))
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer), CType(CType(17, Byte), Integer))
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.gridPubli.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.gridPubli.RowHeadersVisible = False
        Me.gridPubli.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.gridPubli.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.gridPubli.Size = New System.Drawing.Size(368, 347)
        Me.gridPubli.TabIndex = 4
        Me.gridPubli.Tag = "3"
        '
        'Column8
        '
        Me.Column8.DataPropertyName = "falc_id"
        Me.Column8.HeaderText = "ID"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        Me.Column8.Visible = False
        '
        'Column9
        '
        Me.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Column9.DataPropertyName = "falc_nom"
        Me.Column9.HeaderText = "Nom"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'comboClient
        '
        Me.comboClient.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.comboClient.DataPropertyName = "falc_client"
        Me.comboClient.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.comboClient.HeaderText = "Client"
        Me.comboClient.Name = "comboClient"
        Me.comboClient.ReadOnly = True
        Me.comboClient.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.comboClient.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'comboLocutor
        '
        Me.comboLocutor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.comboLocutor.DataPropertyName = "falc_locutor"
        Me.comboLocutor.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        Me.comboLocutor.HeaderText = "Locutor"
        Me.comboLocutor.Name = "comboLocutor"
        Me.comboLocutor.ReadOnly = True
        Me.comboLocutor.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.comboLocutor.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'Column13
        '
        Me.Column13.DataPropertyName = "falc_durada"
        Me.Column13.HeaderText = "Durada"
        Me.Column13.Name = "Column13"
        Me.Column13.ReadOnly = True
        Me.Column13.Width = 50
        '
        'ToolStrip3
        '
        Me.ToolStrip3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 238)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Size = New System.Drawing.Size(488, 25)
        Me.ToolStrip3.TabIndex = 5
        Me.ToolStrip3.Text = "ToolStrip3"
        Me.ToolStrip3.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.Color.Transparent
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdSortir, Me.mnuPosicioPestanyes, Me.ToolStripTextBox1, Me.ToolStripDropDownButton2, Me.mnuMareCarpetesUser, Me.lbAtencioPubli})
        Me.ToolStrip1.Location = New System.Drawing.Point(20, 60)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(376, 25)
        Me.ToolStrip1.TabIndex = 4
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdSortir
        '
        Me.cmdSortir.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.cmdSortir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdSortir.Image = CType(resources.GetObject("cmdSortir.Image"), System.Drawing.Image)
        Me.cmdSortir.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdSortir.Name = "cmdSortir"
        Me.cmdSortir.Size = New System.Drawing.Size(23, 22)
        Me.cmdSortir.Text = "ToolStripButton1"
        '
        'mnuPosicioPestanyes
        '
        Me.mnuPosicioPestanyes.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPestanyaSobre, Me.mnuPestanyaSota, Me.ToolStripSeparator3, Me.mnuShowFolders})
        Me.mnuPosicioPestanyes.Image = CType(resources.GetObject("mnuPosicioPestanyes.Image"), System.Drawing.Image)
        Me.mnuPosicioPestanyes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuPosicioPestanyes.Name = "mnuPosicioPestanyes"
        Me.mnuPosicioPestanyes.Size = New System.Drawing.Size(126, 22)
        Me.mnuPosicioPestanyes.Text = "Posició Carpetes"
        '
        'mnuPestanyaSobre
        '
        Me.mnuPestanyaSobre.Checked = True
        Me.mnuPestanyaSobre.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuPestanyaSobre.Name = "mnuPestanyaSobre"
        Me.mnuPestanyaSobre.Size = New System.Drawing.Size(158, 22)
        Me.mnuPestanyaSobre.Text = "Sobre"
        '
        'mnuPestanyaSota
        '
        Me.mnuPestanyaSota.Name = "mnuPestanyaSota"
        Me.mnuPestanyaSota.Size = New System.Drawing.Size(158, 22)
        Me.mnuPestanyaSota.Text = "Sota"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(155, 6)
        '
        'mnuShowFolders
        '
        Me.mnuShowFolders.Checked = True
        Me.mnuShowFolders.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuShowFolders.Name = "mnuShowFolders"
        Me.mnuShowFolders.Size = New System.Drawing.Size(158, 22)
        Me.mnuShowFolders.Text = "Mostra carpetes"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(0, 22)
        '
        'ToolStripDropDownButton2
        '
        Me.ToolStripDropDownButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuIniSessio, Me.mnuEndSessio})
        Me.ToolStripDropDownButton2.Image = CType(resources.GetObject("ToolStripDropDownButton2.Image"), System.Drawing.Image)
        Me.ToolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton2.Name = "ToolStripDropDownButton2"
        Me.ToolStripDropDownButton2.Size = New System.Drawing.Size(73, 22)
        Me.ToolStripDropDownButton2.Text = "usuaris"
        '
        'mnuIniSessio
        '
        Me.mnuIniSessio.Image = CType(resources.GetObject("mnuIniSessio.Image"), System.Drawing.Image)
        Me.mnuIniSessio.Name = "mnuIniSessio"
        Me.mnuIniSessio.Size = New System.Drawing.Size(151, 22)
        Me.mnuIniSessio.Text = "Inicia sessió"
        Me.mnuIniSessio.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'mnuEndSessio
        '
        Me.mnuEndSessio.Enabled = False
        Me.mnuEndSessio.Image = CType(resources.GetObject("mnuEndSessio.Image"), System.Drawing.Image)
        Me.mnuEndSessio.Name = "mnuEndSessio"
        Me.mnuEndSessio.Size = New System.Drawing.Size(151, 22)
        Me.mnuEndSessio.Text = "Finalitza sessió"
        '
        'mnuMareCarpetesUser
        '
        Me.mnuMareCarpetesUser.Image = CType(resources.GetObject("mnuMareCarpetesUser.Image"), System.Drawing.Image)
        Me.mnuMareCarpetesUser.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.mnuMareCarpetesUser.Name = "mnuMareCarpetesUser"
        Me.mnuMareCarpetesUser.Size = New System.Drawing.Size(133, 20)
        Me.mnuMareCarpetesUser.Text = "Visibilitat Carpetes"
        Me.mnuMareCarpetesUser.Visible = False
        '
        'lbAtencioPubli
        '
        Me.lbAtencioPubli.DoubleClickEnabled = True
        Me.lbAtencioPubli.Image = CType(resources.GetObject("lbAtencioPubli.Image"), System.Drawing.Image)
        Me.lbAtencioPubli.Name = "lbAtencioPubli"
        Me.lbAtencioPubli.Size = New System.Drawing.Size(171, 16)
        Me.lbAtencioPubli.Text = "ATENCIÓ Publicitat pendent"
        Me.lbAtencioPubli.ToolTipText = "Fer doble click per ometre avís"
        Me.lbAtencioPubli.Visible = False
        '
        'tmr_publi
        '
        Me.tmr_publi.Enabled = True
        '
        'ImageListFavorites
        '
        Me.ImageListFavorites.ImageStream = CType(resources.GetObject("ImageListFavorites.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageListFavorites.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageListFavorites.Images.SetKeyName(0, "favSI")
        Me.ImageListFavorites.Images.SetKeyName(1, "favNO")
        '
        'frmAudioDBS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(416, 491)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.tabAudioDBS)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.Name = "frmAudioDBS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Style = MetroFramework.MetroColorStyle.[Default]
        Me.Text = "Àudio a la DBS"
        Me.Theme = MetroFramework.MetroThemeStyle.Dark
        Me.tabAudioDBS.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.gridTemes, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStripBotoDret.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ToolStrip4.ResumeLayout(False)
        Me.ToolStrip4.PerformLayout()
        CType(Me.gridPubli, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuPreescoltaStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtData As System.Windows.Forms.DateTimePicker
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmbBuscar As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents txtBuscaTema As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents findPerTitol As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents findPerInterp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents findPerDisc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents comboBuscaTema As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RecercaAvançadaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents ContextMenuStripBotoDret As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuMesInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPreEscolta As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAddRepreoduccio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdSortir As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents mnuColumnVisibleRadia As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColumnVisibleAny As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColumnVisibleRitme As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColumnVisibleSubj As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColumnVisibleTemp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColumnVisibleIdioma As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuColumnVisibleEstil As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip4 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbHora As System.Windows.Forms.ComboBox
    Friend WithEvents mnuPosicioPestanyes As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents mnuPestanyaSobre As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPestanyaSota As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents LlistatToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLimitRegChec As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLimitRecord As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lbAtencioPubli As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tmr_publi As System.Windows.Forms.Timer
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAddDelFav As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMareCarpetesUser As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents mnuSeparadorFav As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEditAudio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ImageListFavorites As System.Windows.Forms.ImageList
    Friend WithEvents ToolStripDropDownButton2 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents mnuIniSessio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEndSessio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents comboClient As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents comboLocutor As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents Column13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuColumns As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gridPubli As MetroFramework.Controls.MetroGrid
    Friend WithEvents tabAudioDBS As MetroFramework.Controls.MetroTabControl
    Friend WithEvents TabPage1 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents TabPage4 As MetroFramework.Controls.MetroTabPage
    Friend WithEvents gridTemes As MetroFramework.Controls.MetroGrid
    Private WithEvents ColumnID As DataGridViewTextBoxColumn
    Private WithEvents ColumnTitol As DataGridViewTextBoxColumn
    Private WithEvents comboInterp As DataGridViewComboBoxColumn
    Private WithEvents comboDisc As DataGridViewComboBoxColumn
    Private WithEvents Column3 As DataGridViewTextBoxColumn
    Private WithEvents ColumnIdioma As DataGridViewComboBoxColumn
    Private WithEvents ColumnTemp As DataGridViewComboBoxColumn
    Private WithEvents ColumnSub As DataGridViewComboBoxColumn
    Private WithEvents ColumnRitme As DataGridViewTextBoxColumn
    Private WithEvents ColumnAny As DataGridViewTextBoxColumn
    Private WithEvents ColumnRadia As DataGridViewTextBoxColumn
    Private WithEvents ColumnFav As DataGridViewCheckBoxColumn
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents mnuShowFolders As ToolStripMenuItem
End Class
