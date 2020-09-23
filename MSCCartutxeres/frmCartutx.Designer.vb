'
' Creado por SharpDevelop.
' Usuario: Marti
' Fecha: 26/08/2016
' Hora: 09:34
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Partial Class frmCartut
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCartut))
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.PanelListDisp = New System.Windows.Forms.Panel()
        Me.toolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.lbTotal = New System.Windows.Forms.ToolStripLabel()
        Me.toolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.lbCPU = New System.Windows.Forms.ToolStripLabel()
        Me.lstDisp = New System.Windows.Forms.ListView()
        Me.ColumnHeader21 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader22 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader17 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader23 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader24 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.columnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStripBotoDret = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuMesInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator20 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPreEscolta = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPreEscoltaStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuSetPoxPlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDesmarcar = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuContextDel = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuEditAudio = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImageList = New System.Windows.Forms.ImageList(Me.components)
        Me.panel3 = New System.Windows.Forms.Panel()
        Me.panel4 = New System.Windows.Forms.Panel()
        Me.lbAutoSinc = New System.Windows.Forms.Label()
        Me.lbDisplayTime = New System.Windows.Forms.Label()
        Me.lbDisplayTitol = New System.Windows.Forms.Label()
        Me.lbRestaIntro = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbHora = New System.Windows.Forms.Label()
        Me.lbCue = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lbStrPith = New System.Windows.Forms.LinkLabel()
        Me.lbFormatTime = New System.Windows.Forms.Label()
        Me.cmdCue = New System.Windows.Forms.Button()
        Me.cmdNext = New System.Windows.Forms.Button()
        Me.cmdPlay = New System.Windows.Forms.Button()
        Me.cmdPrev = New System.Windows.Forms.Button()
        Me.cmdStop = New System.Windows.Forms.Button()
        Me.sldPith = New System.Windows.Forms.TrackBar()
        Me.lbMicroDespMes = New System.Windows.Forms.LinkLabel()
        Me.lbMicroDespMenys = New System.Windows.Forms.LinkLabel()
        Me.picVis = New System.Windows.Forms.PictureBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkContinuos = New System.Windows.Forms.CheckBox()
        Me.farLoop = New System.Windows.Forms.GroupBox()
        Me.chkLoop = New System.Windows.Forms.CheckBox()
        Me.lbIDActual = New System.Windows.Forms.Label()
        Me.cmdSaveLoop = New System.Windows.Forms.Button()
        Me.cmdLoadLoop = New System.Windows.Forms.Button()
        Me.cmdLoopOut = New System.Windows.Forms.Button()
        Me.cmdLoopOutMes = New System.Windows.Forms.LinkLabel()
        Me.lbLoopOut = New System.Windows.Forms.Label()
        Me.cmdLoopIn = New System.Windows.Forms.Button()
        Me.cmdLoopInMes = New System.Windows.Forms.LinkLabel()
        Me.lbLoopIn = New System.Windows.Forms.Label()
        Me.cmdLoopOutMenys = New System.Windows.Forms.LinkLabel()
        Me.cmdLoopInMenys = New System.Windows.Forms.LinkLabel()
        Me.tmrTitol = New System.Windows.Forms.Timer(Me.components)
        Me.tmr_FadeOunt = New System.Windows.Forms.Timer(Me.components)
        Me.CommonDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.tmrVuMeter = New System.Windows.Forms.Timer(Me.components)
        Me.mnuDef = New System.Windows.Forms.MenuStrip()
        Me.ProgramaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuNewCartut = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReOrdCart = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExplorerPC = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExplorerDBS = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMoveDBS = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuArxiu = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpenFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaveFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator16 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPlayURL = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtPlayURL = New System.Windows.Forms.ToolStripTextBox()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCalcBPMOnLoad = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuCalBPMList = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReproduccióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPlayPause = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReproduccio = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutomàticaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pichToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.pichToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHistoryPlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLoad = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDel = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAutoDel = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoFader = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSpeed = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSpeedSlow = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSpeedNormal = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSpeedFast = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBucleList = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator17 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuInsertStop = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutomàticToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuIniDirect = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuForceDirectMusic = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoSincroMare = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoSincroStartPlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoSincroEndPlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator18 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAutoSincro59 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoSincro00 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoSincro29 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAutoSincroXX = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAutoSincH_PRG = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSH = New System.Windows.Forms.ToolStripMenuItem()
        Me.PresentacióToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBotons = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuColVisibles = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnVRitme = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnHora = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnDurada = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuColumnInterp = New System.Windows.Forms.ToolStripMenuItem()
        Me.EinesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSetTime = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadVar = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDevice = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDevicePreEscolta = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSavedevicePlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDelDevicePlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuControlRemot = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuBOXCTL04 = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAirence = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuAirenceMap = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuPanicButton = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelpInternet = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuLang = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuProgEditAudio = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmrDisplay = New System.Windows.Forms.Timer(Me.components)
        Me.tmrRellotge = New System.Windows.Forms.Timer(Me.components)
        Me.cmdTancar = New System.Windows.Forms.Button()
        Me.cmdLoad = New System.Windows.Forms.Button()
        Me.cmdBorrar = New System.Windows.Forms.Button()
        Me.pnlControlsEdit = New System.Windows.Forms.Panel()
        Me.cmdSalvar = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.cmdPlayPre = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.picDirecte = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmdMesGranBotons = New System.Windows.Forms.ToolStripButton()
        Me.lbTamanyBotons = New System.Windows.Forms.ToolStripLabel()
        Me.cmdMenyGranBotons = New System.Windows.Forms.ToolStripButton()
        Me.lbInfo = New System.Windows.Forms.ToolStripLabel()
        Me.picPlay = New System.Windows.Forms.PictureBox()
        Me.picStop = New System.Windows.Forms.PictureBox()
        Me.picPreEsc = New System.Windows.Forms.PictureBox()
        Me.ToolTipInfo = New System.Windows.Forms.ToolTip(Me.components)
        Me.picWave = New System.Windows.Forms.PictureBox()
        Me.mnu_wave = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuWaveSetIntro = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuWaveSetPointMix = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuWavePlay = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmr_Play = New System.Windows.Forms.Timer(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.flowBotons = New System.Windows.Forms.FlowLayoutPanel()
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.progressBarBotons = New System.Windows.Forms.ProgressBar()
        Me.picPause = New System.Windows.Forms.PictureBox()
        Me.picCueOn = New System.Windows.Forms.PictureBox()
        Me.picCueOFF = New System.Windows.Forms.PictureBox()
        Me.PanelListDisp.SuspendLayout()
        Me.toolStrip2.SuspendLayout()
        Me.ContextMenuStripBotoDret.SuspendLayout()
        Me.panel3.SuspendLayout()
        Me.panel4.SuspendLayout()
        CType(Me.sldPith, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picVis, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.farLoop.SuspendLayout()
        Me.mnuDef.SuspendLayout()
        Me.pnlControlsEdit.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.picPlay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picStop, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picPreEsc, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picWave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnu_wave.SuspendLayout()
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        CType(Me.picPause, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCueOn, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picCueOFF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.CreatePrompt = True
        '
        'PanelListDisp
        '
        Me.PanelListDisp.Controls.Add(Me.toolStrip2)
        Me.PanelListDisp.Controls.Add(Me.lstDisp)
        Me.PanelListDisp.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelListDisp.Location = New System.Drawing.Point(0, 0)
        Me.PanelListDisp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.PanelListDisp.Name = "PanelListDisp"
        Me.PanelListDisp.Size = New System.Drawing.Size(556, 225)
        Me.PanelListDisp.TabIndex = 4
        '
        'toolStrip2
        '
        Me.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.toolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lbTotal, Me.toolStripLabel2, Me.lbCPU})
        Me.toolStrip2.Location = New System.Drawing.Point(0, 200)
        Me.toolStrip2.Name = "toolStrip2"
        Me.toolStrip2.Size = New System.Drawing.Size(556, 25)
        Me.toolStrip2.TabIndex = 0
        Me.toolStrip2.Text = "toolStrip2"
        '
        'lbTotal
        '
        Me.lbTotal.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lbTotal.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.lbTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbTotal.Name = "lbTotal"
        Me.lbTotal.Size = New System.Drawing.Size(55, 22)
        Me.lbTotal.Text = "00:00:00"
        '
        'toolStripLabel2
        '
        Me.toolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.toolStripLabel2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.toolStripLabel2.Name = "toolStripLabel2"
        Me.toolStripLabel2.Size = New System.Drawing.Size(36, 22)
        Me.toolStripLabel2.Text = "Total:"
        '
        'lbCPU
        '
        Me.lbCPU.Name = "lbCPU"
        Me.lbCPU.Size = New System.Drawing.Size(23, 22)
        Me.lbCPU.Text = "0%"
        '
        'lstDisp
        '
        Me.lstDisp.AllowDrop = True
        Me.lstDisp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstDisp.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader21, Me.ColumnHeader22, Me.ColumnHeader17, Me.ColumnHeader23, Me.ColumnHeader24, Me.columnHeader1})
        Me.lstDisp.ContextMenuStrip = Me.ContextMenuStripBotoDret
        Me.lstDisp.FullRowSelect = True
        Me.lstDisp.HoverSelection = True
        Me.lstDisp.LargeImageList = Me.ImageList
        Me.lstDisp.Location = New System.Drawing.Point(0, 0)
        Me.lstDisp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstDisp.Name = "lstDisp"
        Me.lstDisp.Size = New System.Drawing.Size(556, 190)
        Me.lstDisp.SmallImageList = Me.ImageList
        Me.lstDisp.TabIndex = 7
        Me.lstDisp.UseCompatibleStateImageBehavior = False
        Me.lstDisp.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader21
        '
        Me.ColumnHeader21.Text = "Títol"
        Me.ColumnHeader21.Width = 120
        '
        'ColumnHeader22
        '
        Me.ColumnHeader22.Text = "Intèrpret/Client"
        Me.ColumnHeader22.Width = 119
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = ""
        Me.ColumnHeader17.Width = 0
        '
        'ColumnHeader23
        '
        Me.ColumnHeader23.Text = "Durada"
        '
        'ColumnHeader24
        '
        Me.ColumnHeader24.Text = "Hora radi."
        Me.ColumnHeader24.Width = 62
        '
        'columnHeader1
        '
        Me.columnHeader1.Text = "Ritme"
        Me.columnHeader1.Width = 45
        '
        'ContextMenuStripBotoDret
        '
        Me.ContextMenuStripBotoDret.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuMesInfo, Me.ToolStripSeparator20, Me.mnuPreEscolta, Me.mnuPreEscoltaStop, Me.ToolStripTextBox1, Me.ToolStripSeparator11, Me.mnuSetPoxPlay, Me.mnuDesmarcar, Me.mnuContextDel, Me.ToolStripSeparator13, Me.mnuEditAudio})
        Me.ContextMenuStripBotoDret.Name = "ContextMenuStripBotoDret"
        Me.ContextMenuStripBotoDret.Size = New System.Drawing.Size(230, 198)
        '
        'mnuMesInfo
        '
        Me.mnuMesInfo.Image = CType(resources.GetObject("mnuMesInfo.Image"), System.Drawing.Image)
        Me.mnuMesInfo.Name = "mnuMesInfo"
        Me.mnuMesInfo.Size = New System.Drawing.Size(229, 22)
        Me.mnuMesInfo.Text = "Més Info ..."
        '
        'ToolStripSeparator20
        '
        Me.ToolStripSeparator20.Name = "ToolStripSeparator20"
        Me.ToolStripSeparator20.Size = New System.Drawing.Size(226, 6)
        '
        'mnuPreEscolta
        '
        Me.mnuPreEscolta.Name = "mnuPreEscolta"
        Me.mnuPreEscolta.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F5), System.Windows.Forms.Keys)
        Me.mnuPreEscolta.Size = New System.Drawing.Size(229, 22)
        Me.mnuPreEscolta.Text = "Pre-escolta"
        '
        'mnuPreEscoltaStop
        '
        Me.mnuPreEscoltaStop.Name = "mnuPreEscoltaStop"
        Me.mnuPreEscoltaStop.ShortcutKeys = CType((System.Windows.Forms.Keys.Shift Or System.Windows.Forms.Keys.F7), System.Windows.Forms.Keys)
        Me.mnuPreEscoltaStop.Size = New System.Drawing.Size(229, 22)
        Me.mnuPreEscoltaStop.Text = "Pre-escolta STOP"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(229, 22)
        Me.ToolStripTextBox1.Text = "Play"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(226, 6)
        '
        'mnuSetPoxPlay
        '
        Me.mnuSetPoxPlay.Name = "mnuSetPoxPlay"
        Me.mnuSetPoxPlay.Size = New System.Drawing.Size(229, 22)
        Me.mnuSetPoxPlay.Text = "Pròxim a  Play"
        '
        'mnuDesmarcar
        '
        Me.mnuDesmarcar.Name = "mnuDesmarcar"
        Me.mnuDesmarcar.Size = New System.Drawing.Size(229, 22)
        Me.mnuDesmarcar.Text = "Desmarcar"
        Me.mnuDesmarcar.ToolTipText = "Descarca un fitxer com a ja reproduït."
        '
        'mnuContextDel
        '
        Me.mnuContextDel.Name = "mnuContextDel"
        Me.mnuContextDel.Size = New System.Drawing.Size(229, 22)
        Me.mnuContextDel.Text = "Borrar"
        Me.mnuContextDel.ToolTipText = "Borrar de la llista de reprodució"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(226, 6)
        '
        'mnuEditAudio
        '
        Me.mnuEditAudio.Enabled = False
        Me.mnuEditAudio.Name = "mnuEditAudio"
        Me.mnuEditAudio.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.E), System.Windows.Forms.Keys)
        Me.mnuEditAudio.Size = New System.Drawing.Size(229, 22)
        Me.mnuEditAudio.Text = "Editar l'àudio"
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
        Me.ImageList.Images.SetKeyName(12, "sortir.gif")
        '
        'panel3
        '
        Me.panel3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panel3.Controls.Add(Me.panel4)
        Me.panel3.Controls.Add(Me.cmdCue)
        Me.panel3.Controls.Add(Me.cmdNext)
        Me.panel3.Controls.Add(Me.cmdPlay)
        Me.panel3.Controls.Add(Me.cmdPrev)
        Me.panel3.Controls.Add(Me.cmdStop)
        Me.panel3.Controls.Add(Me.sldPith)
        Me.panel3.Location = New System.Drawing.Point(122, 187)
        Me.panel3.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.panel3.Name = "panel3"
        Me.panel3.Size = New System.Drawing.Size(275, 204)
        Me.panel3.TabIndex = 12
        '
        'panel4
        '
        Me.panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.panel4.BackgroundImage = CType(resources.GetObject("panel4.BackgroundImage"), System.Drawing.Image)
        Me.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.panel4.Controls.Add(Me.lbAutoSinc)
        Me.panel4.Controls.Add(Me.lbDisplayTime)
        Me.panel4.Controls.Add(Me.lbDisplayTitol)
        Me.panel4.Controls.Add(Me.lbRestaIntro)
        Me.panel4.Controls.Add(Me.Label3)
        Me.panel4.Controls.Add(Me.lbHora)
        Me.panel4.Controls.Add(Me.lbCue)
        Me.panel4.Controls.Add(Me.Label1)
        Me.panel4.Controls.Add(Me.lbStrPith)
        Me.panel4.Controls.Add(Me.lbFormatTime)
        Me.panel4.Location = New System.Drawing.Point(0, 7)
        Me.panel4.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.panel4.Name = "panel4"
        Me.panel4.Size = New System.Drawing.Size(255, 128)
        Me.panel4.TabIndex = 12
        '
        'lbAutoSinc
        '
        Me.lbAutoSinc.BackColor = System.Drawing.Color.Lime
        Me.lbAutoSinc.Font = New System.Drawing.Font("Microsoft Sans Serif", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbAutoSinc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lbAutoSinc.Location = New System.Drawing.Point(217, 10)
        Me.lbAutoSinc.Name = "lbAutoSinc"
        Me.lbAutoSinc.Size = New System.Drawing.Size(31, 17)
        Me.lbAutoSinc.TabIndex = 11
        Me.lbAutoSinc.Text = "SYNC"
        Me.lbAutoSinc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lbDisplayTime
        '
        Me.lbDisplayTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.lbDisplayTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbDisplayTime.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lbDisplayTime.ForeColor = System.Drawing.Color.Lime
        Me.lbDisplayTime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lbDisplayTime.Location = New System.Drawing.Point(10, 8)
        Me.lbDisplayTime.Name = "lbDisplayTime"
        Me.lbDisplayTime.Size = New System.Drawing.Size(157, 30)
        Me.lbDisplayTime.TabIndex = 10
        Me.lbDisplayTime.Text = "-00:00:00.00"
        Me.lbDisplayTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbDisplayTitol
        '
        Me.lbDisplayTitol.AutoSize = True
        Me.lbDisplayTitol.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.lbDisplayTitol.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lbDisplayTitol.ForeColor = System.Drawing.Color.Lime
        Me.lbDisplayTitol.Location = New System.Drawing.Point(21, 101)
        Me.lbDisplayTitol.Name = "lbDisplayTitol"
        Me.lbDisplayTitol.Size = New System.Drawing.Size(105, 17)
        Me.lbDisplayTitol.TabIndex = 9
        Me.lbDisplayTitol.Text = "MSC Cartutxeres"
        '
        'lbRestaIntro
        '
        Me.lbRestaIntro.AutoSize = True
        Me.lbRestaIntro.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.lbRestaIntro.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbRestaIntro.ForeColor = System.Drawing.Color.Lime
        Me.lbRestaIntro.Location = New System.Drawing.Point(91, 38)
        Me.lbRestaIntro.Name = "lbRestaIntro"
        Me.lbRestaIntro.Size = New System.Drawing.Size(65, 23)
        Me.lbRestaIntro.TabIndex = 1
        Me.lbRestaIntro.Text = "00:00"
        Me.lbRestaIntro.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Lime
        Me.Label3.Location = New System.Drawing.Point(167, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(25, 11)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Hora"
        '
        'lbHora
        '
        Me.lbHora.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.lbHora.Font = New System.Drawing.Font("Tahoma", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbHora.ForeColor = System.Drawing.Color.Lime
        Me.lbHora.Location = New System.Drawing.Point(42, 69)
        Me.lbHora.Name = "lbHora"
        Me.lbHora.Size = New System.Drawing.Size(124, 33)
        Me.lbHora.TabIndex = 7
        Me.lbHora.Text = "12:55:59"
        Me.lbHora.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbCue
        '
        Me.lbCue.BackColor = System.Drawing.Color.Lime
        Me.lbCue.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbCue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lbCue.Location = New System.Drawing.Point(217, 35)
        Me.lbCue.Name = "lbCue"
        Me.lbCue.Size = New System.Drawing.Size(31, 17)
        Me.lbCue.TabIndex = 4
        Me.lbCue.Text = "CUE"
        Me.lbCue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Lime
        Me.Label1.Location = New System.Drawing.Point(167, 47)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 11)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Intro"
        '
        'lbStrPith
        '
        Me.lbStrPith.BackColor = System.Drawing.Color.Lime
        Me.lbStrPith.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbStrPith.ForeColor = System.Drawing.Color.Lime
        Me.lbStrPith.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lbStrPith.LinkColor = System.Drawing.Color.Lime
        Me.lbStrPith.Location = New System.Drawing.Point(217, 60)
        Me.lbStrPith.Name = "lbStrPith"
        Me.lbStrPith.Size = New System.Drawing.Size(31, 17)
        Me.lbStrPith.TabIndex = 5
        Me.lbStrPith.TabStop = True
        Me.lbStrPith.Text = "44.1"
        Me.lbStrPith.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lbStrPith.VisitedLinkColor = System.Drawing.Color.Red
        '
        'lbFormatTime
        '
        Me.lbFormatTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer), CType(CType(127, Byte), Integer))
        Me.lbFormatTime.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbFormatTime.ForeColor = System.Drawing.Color.Lime
        Me.lbFormatTime.Location = New System.Drawing.Point(167, 8)
        Me.lbFormatTime.Name = "lbFormatTime"
        Me.lbFormatTime.Size = New System.Drawing.Size(51, 37)
        Me.lbFormatTime.TabIndex = 2
        Me.lbFormatTime.Text = "Remain Time"
        '
        'cmdCue
        '
        Me.cmdCue.Image = CType(resources.GetObject("cmdCue.Image"), System.Drawing.Image)
        Me.cmdCue.Location = New System.Drawing.Point(1, 139)
        Me.cmdCue.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdCue.Name = "cmdCue"
        Me.cmdCue.Size = New System.Drawing.Size(52, 59)
        Me.cmdCue.TabIndex = 3
        Me.cmdCue.UseVisualStyleBackColor = True
        '
        'cmdNext
        '
        Me.cmdNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdNext.Image = CType(resources.GetObject("cmdNext.Image"), System.Drawing.Image)
        Me.cmdNext.Location = New System.Drawing.Point(215, 139)
        Me.cmdNext.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdNext.Name = "cmdNext"
        Me.cmdNext.Size = New System.Drawing.Size(52, 59)
        Me.cmdNext.TabIndex = 7
        Me.cmdNext.UseVisualStyleBackColor = True
        '
        'cmdPlay
        '
        Me.cmdPlay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdPlay.Image = CType(resources.GetObject("cmdPlay.Image"), System.Drawing.Image)
        Me.cmdPlay.Location = New System.Drawing.Point(54, 139)
        Me.cmdPlay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdPlay.Name = "cmdPlay"
        Me.cmdPlay.Size = New System.Drawing.Size(52, 59)
        Me.cmdPlay.TabIndex = 4
        Me.cmdPlay.UseVisualStyleBackColor = True
        '
        'cmdPrev
        '
        Me.cmdPrev.Image = CType(resources.GetObject("cmdPrev.Image"), System.Drawing.Image)
        Me.cmdPrev.Location = New System.Drawing.Point(161, 139)
        Me.cmdPrev.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdPrev.Name = "cmdPrev"
        Me.cmdPrev.Size = New System.Drawing.Size(52, 59)
        Me.cmdPrev.TabIndex = 6
        Me.cmdPrev.UseVisualStyleBackColor = True
        '
        'cmdStop
        '
        Me.cmdStop.Image = CType(resources.GetObject("cmdStop.Image"), System.Drawing.Image)
        Me.cmdStop.Location = New System.Drawing.Point(107, 139)
        Me.cmdStop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdStop.Name = "cmdStop"
        Me.cmdStop.Size = New System.Drawing.Size(52, 59)
        Me.cmdStop.TabIndex = 5
        Me.cmdStop.UseVisualStyleBackColor = True
        '
        'sldPith
        '
        Me.sldPith.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.sldPith.AutoSize = False
        Me.sldPith.LargeChange = 5000
        Me.sldPith.Location = New System.Drawing.Point(255, -3)
        Me.sldPith.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.sldPith.Maximum = 80000
        Me.sldPith.Minimum = 11000
        Me.sldPith.Name = "sldPith"
        Me.sldPith.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.sldPith.Size = New System.Drawing.Size(22, 144)
        Me.sldPith.SmallChange = 100
        Me.sldPith.TabIndex = 8
        Me.sldPith.TabStop = False
        Me.sldPith.TickFrequency = 1000
        Me.sldPith.TickStyle = System.Windows.Forms.TickStyle.None
        Me.sldPith.Value = 44100
        '
        'lbMicroDespMes
        '
        Me.lbMicroDespMes.AutoSize = True
        Me.lbMicroDespMes.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lbMicroDespMes.Location = New System.Drawing.Point(812, 526)
        Me.lbMicroDespMes.Name = "lbMicroDespMes"
        Me.lbMicroDespMes.Size = New System.Drawing.Size(17, 17)
        Me.lbMicroDespMes.TabIndex = 9
        Me.lbMicroDespMes.TabStop = True
        Me.lbMicroDespMes.Text = "+"
        Me.lbMicroDespMes.Visible = False
        '
        'lbMicroDespMenys
        '
        Me.lbMicroDespMenys.AutoSize = True
        Me.lbMicroDespMenys.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lbMicroDespMenys.Location = New System.Drawing.Point(531, 526)
        Me.lbMicroDespMenys.Name = "lbMicroDespMenys"
        Me.lbMicroDespMenys.Size = New System.Drawing.Size(13, 17)
        Me.lbMicroDespMenys.TabIndex = 10
        Me.lbMicroDespMenys.TabStop = True
        Me.lbMicroDespMenys.Text = "-"
        Me.lbMicroDespMenys.Visible = False
        '
        'picVis
        '
        Me.picVis.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picVis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picVis.Location = New System.Drawing.Point(0, 187)
        Me.picVis.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picVis.Name = "picVis"
        Me.picVis.Size = New System.Drawing.Size(119, 199)
        Me.picVis.TabIndex = 12
        Me.picVis.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.chkContinuos)
        Me.GroupBox1.Controls.Add(Me.farLoop)
        Me.GroupBox1.Location = New System.Drawing.Point(398, 197)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(155, 183)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Reproducció"
        '
        'chkContinuos
        '
        Me.chkContinuos.AutoSize = True
        Me.chkContinuos.Location = New System.Drawing.Point(15, 21)
        Me.chkContinuos.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkContinuos.Name = "chkContinuos"
        Me.chkContinuos.Size = New System.Drawing.Size(71, 21)
        Me.chkContinuos.TabIndex = 7
        Me.chkContinuos.Text = "Continu"
        Me.chkContinuos.UseVisualStyleBackColor = True
        '
        'farLoop
        '
        Me.farLoop.Controls.Add(Me.chkLoop)
        Me.farLoop.Controls.Add(Me.lbIDActual)
        Me.farLoop.Controls.Add(Me.cmdSaveLoop)
        Me.farLoop.Controls.Add(Me.cmdLoadLoop)
        Me.farLoop.Controls.Add(Me.cmdLoopOut)
        Me.farLoop.Controls.Add(Me.cmdLoopOutMes)
        Me.farLoop.Controls.Add(Me.lbLoopOut)
        Me.farLoop.Controls.Add(Me.cmdLoopIn)
        Me.farLoop.Controls.Add(Me.cmdLoopInMes)
        Me.farLoop.Controls.Add(Me.lbLoopIn)
        Me.farLoop.Controls.Add(Me.cmdLoopOutMenys)
        Me.farLoop.Controls.Add(Me.cmdLoopInMenys)
        Me.farLoop.Location = New System.Drawing.Point(7, 47)
        Me.farLoop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.farLoop.Name = "farLoop"
        Me.farLoop.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.farLoop.Size = New System.Drawing.Size(143, 129)
        Me.farLoop.TabIndex = 3
        Me.farLoop.TabStop = False
        '
        'chkLoop
        '
        Me.chkLoop.AutoSize = True
        Me.chkLoop.Location = New System.Drawing.Point(8, 0)
        Me.chkLoop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkLoop.Name = "chkLoop"
        Me.chkLoop.Size = New System.Drawing.Size(57, 21)
        Me.chkLoop.TabIndex = 10
        Me.chkLoop.Text = "Loop"
        Me.chkLoop.UseVisualStyleBackColor = True
        '
        'lbIDActual
        '
        Me.lbIDActual.AutoSize = True
        Me.lbIDActual.BackColor = System.Drawing.Color.Lime
        Me.lbIDActual.Location = New System.Drawing.Point(93, 21)
        Me.lbIDActual.Name = "lbIDActual"
        Me.lbIDActual.Size = New System.Drawing.Size(15, 17)
        Me.lbIDActual.TabIndex = 6
        Me.lbIDActual.Text = "0"
        Me.lbIDActual.Visible = False
        '
        'cmdSaveLoop
        '
        Me.cmdSaveLoop.Enabled = False
        Me.cmdSaveLoop.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSaveLoop.Location = New System.Drawing.Point(8, 98)
        Me.cmdSaveLoop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdSaveLoop.Name = "cmdSaveLoop"
        Me.cmdSaveLoop.Size = New System.Drawing.Size(129, 24)
        Me.cmdSaveLoop.TabIndex = 9
        Me.cmdSaveLoop.TabStop = False
        Me.cmdSaveLoop.Text = "Salvar"
        Me.cmdSaveLoop.UseVisualStyleBackColor = True
        '
        'cmdLoadLoop
        '
        Me.cmdLoadLoop.Enabled = False
        Me.cmdLoadLoop.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoadLoop.Location = New System.Drawing.Point(8, 69)
        Me.cmdLoadLoop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdLoadLoop.Name = "cmdLoadLoop"
        Me.cmdLoadLoop.Size = New System.Drawing.Size(129, 24)
        Me.cmdLoadLoop.TabIndex = 8
        Me.cmdLoadLoop.TabStop = False
        Me.cmdLoadLoop.Text = "Carregar"
        Me.cmdLoadLoop.UseVisualStyleBackColor = True
        '
        'cmdLoopOut
        '
        Me.cmdLoopOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoopOut.Location = New System.Drawing.Point(84, 46)
        Me.cmdLoopOut.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdLoopOut.Name = "cmdLoopOut"
        Me.cmdLoopOut.Size = New System.Drawing.Size(54, 21)
        Me.cmdLoopOut.TabIndex = 7
        Me.cmdLoopOut.TabStop = False
        Me.cmdLoopOut.Text = "loop Out"
        Me.cmdLoopOut.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdLoopOut.UseVisualStyleBackColor = True
        '
        'cmdLoopOutMes
        '
        Me.cmdLoopOutMes.AutoSize = True
        Me.cmdLoopOutMes.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.cmdLoopOutMes.Location = New System.Drawing.Point(71, 48)
        Me.cmdLoopOutMes.Name = "cmdLoopOutMes"
        Me.cmdLoopOutMes.Size = New System.Drawing.Size(17, 17)
        Me.cmdLoopOutMes.TabIndex = 6
        Me.cmdLoopOutMes.TabStop = True
        Me.cmdLoopOutMes.Text = "+"
        '
        'lbLoopOut
        '
        Me.lbLoopOut.AutoSize = True
        Me.lbLoopOut.Location = New System.Drawing.Point(15, 48)
        Me.lbLoopOut.Name = "lbLoopOut"
        Me.lbLoopOut.Size = New System.Drawing.Size(56, 17)
        Me.lbLoopOut.TabIndex = 4
        Me.lbLoopOut.Text = "00:00:00"
        '
        'cmdLoopIn
        '
        Me.cmdLoopIn.BackColor = System.Drawing.Color.Transparent
        Me.cmdLoopIn.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoopIn.Location = New System.Drawing.Point(84, 16)
        Me.cmdLoopIn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdLoopIn.Name = "cmdLoopIn"
        Me.cmdLoopIn.Size = New System.Drawing.Size(54, 24)
        Me.cmdLoopIn.TabIndex = 3
        Me.cmdLoopIn.TabStop = False
        Me.cmdLoopIn.Text = "Loop In"
        Me.cmdLoopIn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdLoopIn.UseVisualStyleBackColor = False
        '
        'cmdLoopInMes
        '
        Me.cmdLoopInMes.AutoSize = True
        Me.cmdLoopInMes.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.cmdLoopInMes.Location = New System.Drawing.Point(71, 21)
        Me.cmdLoopInMes.Name = "cmdLoopInMes"
        Me.cmdLoopInMes.Size = New System.Drawing.Size(17, 17)
        Me.cmdLoopInMes.TabIndex = 2
        Me.cmdLoopInMes.TabStop = True
        Me.cmdLoopInMes.Text = "+"
        '
        'lbLoopIn
        '
        Me.lbLoopIn.AutoSize = True
        Me.lbLoopIn.Location = New System.Drawing.Point(16, 21)
        Me.lbLoopIn.Name = "lbLoopIn"
        Me.lbLoopIn.Size = New System.Drawing.Size(56, 17)
        Me.lbLoopIn.TabIndex = 0
        Me.lbLoopIn.Text = "00:00:00"
        '
        'cmdLoopOutMenys
        '
        Me.cmdLoopOutMenys.AutoSize = True
        Me.cmdLoopOutMenys.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoopOutMenys.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.cmdLoopOutMenys.Location = New System.Drawing.Point(3, 48)
        Me.cmdLoopOutMenys.Name = "cmdLoopOutMenys"
        Me.cmdLoopOutMenys.Size = New System.Drawing.Size(11, 13)
        Me.cmdLoopOutMenys.TabIndex = 5
        Me.cmdLoopOutMenys.TabStop = True
        Me.cmdLoopOutMenys.Text = "-"
        '
        'cmdLoopInMenys
        '
        Me.cmdLoopInMenys.AutoSize = True
        Me.cmdLoopInMenys.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdLoopInMenys.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.cmdLoopInMenys.Location = New System.Drawing.Point(5, 21)
        Me.cmdLoopInMenys.Name = "cmdLoopInMenys"
        Me.cmdLoopInMenys.Size = New System.Drawing.Size(11, 13)
        Me.cmdLoopInMenys.TabIndex = 1
        Me.cmdLoopInMenys.TabStop = True
        Me.cmdLoopInMenys.Text = "-"
        '
        'tmrTitol
        '
        Me.tmrTitol.Enabled = True
        Me.tmrTitol.Interval = 75
        '
        'tmr_FadeOunt
        '
        Me.tmr_FadeOunt.Interval = 40
        '
        'CommonDialog1
        '
        Me.CommonDialog1.FileName = "OpenFileDialog1"
        '
        'tmrVuMeter
        '
        Me.tmrVuMeter.Enabled = True
        Me.tmrVuMeter.Interval = 20
        '
        'mnuDef
        '
        Me.mnuDef.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.mnuDef.Dock = System.Windows.Forms.DockStyle.None
        Me.mnuDef.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProgramaToolStripMenuItem, Me.mnuArxiu, Me.ReproduccióToolStripMenuItem, Me.AutomàticToolStripMenuItem, Me.PresentacióToolStripMenuItem, Me.EinesToolStripMenuItem})
        Me.mnuDef.Location = New System.Drawing.Point(25, 21)
        Me.mnuDef.Name = "mnuDef"
        Me.mnuDef.Padding = New System.Windows.Forms.Padding(7, 3, 0, 3)
        Me.mnuDef.Size = New System.Drawing.Size(533, 25)
        Me.mnuDef.TabIndex = 6
        Me.mnuDef.Text = "MenuStrip1"
        '
        'ProgramaToolStripMenuItem
        '
        Me.ProgramaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNewCartut, Me.mnuReOrdCart, Me.ToolStripSeparator7, Me.mnuExplorerPC, Me.mnuExplorerDBS, Me.mnuMoveDBS, Me.ToolStripSeparator9, Me.mnuExit})
        Me.ProgramaToolStripMenuItem.Name = "ProgramaToolStripMenuItem"
        Me.ProgramaToolStripMenuItem.Size = New System.Drawing.Size(71, 19)
        Me.ProgramaToolStripMenuItem.Text = "Programa"
        '
        'mnuNewCartut
        '
        Me.mnuNewCartut.Name = "mnuNewCartut"
        Me.mnuNewCartut.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.mnuNewCartut.Size = New System.Drawing.Size(260, 22)
        Me.mnuNewCartut.Text = "Nova Cartutxera"
        '
        'mnuReOrdCart
        '
        Me.mnuReOrdCart.Name = "mnuReOrdCart"
        Me.mnuReOrdCart.Size = New System.Drawing.Size(260, 22)
        Me.mnuReOrdCart.Text = "Re-ordena l'emplaçament"
        Me.mnuReOrdCart.ToolTipText = "Estableix tamany i enplaçament per defecte"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(257, 6)
        '
        'mnuExplorerPC
        '
        Me.mnuExplorerPC.Name = "mnuExplorerPC"
        Me.mnuExplorerPC.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.mnuExplorerPC.Size = New System.Drawing.Size(260, 22)
        Me.mnuExplorerPC.Text = "Explorador del PC"
        '
        'mnuExplorerDBS
        '
        Me.mnuExplorerDBS.Name = "mnuExplorerDBS"
        Me.mnuExplorerDBS.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.mnuExplorerDBS.Size = New System.Drawing.Size(260, 22)
        Me.mnuExplorerDBS.Text = "Explorador de la DBS"
        '
        'mnuMoveDBS
        '
        Me.mnuMoveDBS.Name = "mnuMoveDBS"
        Me.mnuMoveDBS.Size = New System.Drawing.Size(260, 22)
        Me.mnuMoveDBS.Text = "Emplaçament per defecte expl. DBS"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(257, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Image = CType(resources.GetObject("mnuExit.Image"), System.Drawing.Image)
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(260, 22)
        Me.mnuExit.Text = "Sortir"
        '
        'mnuArxiu
        '
        Me.mnuArxiu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpenFile, Me.mnuSaveFile, Me.toolStripSeparator16, Me.mnuPlayURL, Me.ToolStripSeparator1, Me.mnuCalcBPMOnLoad, Me.mnuCalBPMList})
        Me.mnuArxiu.Name = "mnuArxiu"
        Me.mnuArxiu.Size = New System.Drawing.Size(46, 19)
        Me.mnuArxiu.Text = "Arxiu"
        '
        'mnuOpenFile
        '
        Me.mnuOpenFile.Name = "mnuOpenFile"
        Me.mnuOpenFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mnuOpenFile.Size = New System.Drawing.Size(203, 22)
        Me.mnuOpenFile.Text = "Obrir"
        '
        'mnuSaveFile
        '
        Me.mnuSaveFile.Name = "mnuSaveFile"
        Me.mnuSaveFile.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuSaveFile.Size = New System.Drawing.Size(203, 22)
        Me.mnuSaveFile.Text = "Salvar"
        '
        'toolStripSeparator16
        '
        Me.toolStripSeparator16.Name = "toolStripSeparator16"
        Me.toolStripSeparator16.Size = New System.Drawing.Size(200, 6)
        '
        'mnuPlayURL
        '
        Me.mnuPlayURL.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.txtPlayURL})
        Me.mnuPlayURL.Image = CType(resources.GetObject("mnuPlayURL.Image"), System.Drawing.Image)
        Me.mnuPlayURL.Name = "mnuPlayURL"
        Me.mnuPlayURL.Size = New System.Drawing.Size(203, 22)
        Me.mnuPlayURL.Text = "Reproduir URL"
        '
        'txtPlayURL
        '
        Me.txtPlayURL.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtPlayURL.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtPlayURL.Name = "txtPlayURL"
        Me.txtPlayURL.Size = New System.Drawing.Size(150, 23)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(200, 6)
        '
        'mnuCalcBPMOnLoad
        '
        Me.mnuCalcBPMOnLoad.Name = "mnuCalcBPMOnLoad"
        Me.mnuCalcBPMOnLoad.Size = New System.Drawing.Size(203, 22)
        Me.mnuCalcBPMOnLoad.Text = "Calcular BPM al carregar"
        '
        'mnuCalBPMList
        '
        Me.mnuCalBPMList.Name = "mnuCalBPMList"
        Me.mnuCalBPMList.Size = New System.Drawing.Size(203, 22)
        Me.mnuCalBPMList.Text = "Calcular BPM del llistat"
        '
        'ReproduccióToolStripMenuItem
        '
        Me.ReproduccióToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPlayPause, Me.mnuStop, Me.mnuReproduccio, Me.mnuHistoryPlay, Me.ToolStripSeparator2, Me.mnuLoad, Me.mnuDel, Me.ToolStripSeparator3, Me.mnuAutoDel, Me.mnuAutoFader, Me.mnuBucleList, Me.toolStripSeparator17, Me.mnuInsertStop})
        Me.ReproduccióToolStripMenuItem.Name = "ReproduccióToolStripMenuItem"
        Me.ReproduccióToolStripMenuItem.Size = New System.Drawing.Size(86, 19)
        Me.ReproduccióToolStripMenuItem.Text = "Reproducció"
        '
        'mnuPlayPause
        '
        Me.mnuPlayPause.Name = "mnuPlayPause"
        Me.mnuPlayPause.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.mnuPlayPause.Size = New System.Drawing.Size(175, 22)
        Me.mnuPlayPause.Text = "Play/Pausa"
        '
        'mnuStop
        '
        Me.mnuStop.Name = "mnuStop"
        Me.mnuStop.ShortcutKeys = System.Windows.Forms.Keys.F7
        Me.mnuStop.Size = New System.Drawing.Size(175, 22)
        Me.mnuStop.Text = "Stop"
        '
        'mnuReproduccio
        '
        Me.mnuReproduccio.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AutomàticaToolStripMenuItem, Me.LoopToolStripMenuItem, Me.pichToolStripMenuItem, Me.pichToolStripMenuItem1})
        Me.mnuReproduccio.Name = "mnuReproduccio"
        Me.mnuReproduccio.Size = New System.Drawing.Size(175, 22)
        Me.mnuReproduccio.Text = "Reproducció"
        '
        'AutomàticaToolStripMenuItem
        '
        Me.AutomàticaToolStripMenuItem.Name = "AutomàticaToolStripMenuItem"
        Me.AutomàticaToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.AutomàticaToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.AutomàticaToolStripMenuItem.Text = "Continu"
        '
        'LoopToolStripMenuItem
        '
        Me.LoopToolStripMenuItem.Name = "LoopToolStripMenuItem"
        Me.LoopToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.L), System.Windows.Forms.Keys)
        Me.LoopToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.LoopToolStripMenuItem.Text = "Loop"
        '
        'pichToolStripMenuItem
        '
        Me.pichToolStripMenuItem.Name = "pichToolStripMenuItem"
        Me.pichToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Up), System.Windows.Forms.Keys)
        Me.pichToolStripMenuItem.Size = New System.Drawing.Size(170, 22)
        Me.pichToolStripMenuItem.Text = "Pitch +"
        '
        'pichToolStripMenuItem1
        '
        Me.pichToolStripMenuItem1.Name = "pichToolStripMenuItem1"
        Me.pichToolStripMenuItem1.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Down), System.Windows.Forms.Keys)
        Me.pichToolStripMenuItem1.Size = New System.Drawing.Size(170, 22)
        Me.pichToolStripMenuItem1.Text = "Pitch -"
        '
        'mnuHistoryPlay
        '
        Me.mnuHistoryPlay.Name = "mnuHistoryPlay"
        Me.mnuHistoryPlay.Size = New System.Drawing.Size(175, 22)
        Me.mnuHistoryPlay.Text = "Historial"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(172, 6)
        '
        'mnuLoad
        '
        Me.mnuLoad.Name = "mnuLoad"
        Me.mnuLoad.ShortcutKeys = System.Windows.Forms.Keys.F8
        Me.mnuLoad.Size = New System.Drawing.Size(175, 22)
        Me.mnuLoad.Text = "Carregar"
        Me.mnuLoad.Visible = False
        '
        'mnuDel
        '
        Me.mnuDel.Enabled = False
        Me.mnuDel.Name = "mnuDel"
        Me.mnuDel.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.mnuDel.Size = New System.Drawing.Size(175, 22)
        Me.mnuDel.Text = "Borrar"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(172, 6)
        '
        'mnuAutoDel
        '
        Me.mnuAutoDel.Enabled = False
        Me.mnuAutoDel.Name = "mnuAutoDel"
        Me.mnuAutoDel.Size = New System.Drawing.Size(175, 22)
        Me.mnuAutoDel.Text = "Auto eliminar fitxer"
        '
        'mnuAutoFader
        '
        Me.mnuAutoFader.Checked = True
        Me.mnuAutoFader.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuAutoFader.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSpeed})
        Me.mnuAutoFader.Name = "mnuAutoFader"
        Me.mnuAutoFader.Size = New System.Drawing.Size(175, 22)
        Me.mnuAutoFader.Text = "Auto Mescla"
        '
        'mnuSpeed
        '
        Me.mnuSpeed.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSpeedSlow, Me.mnuSpeedNormal, Me.mnuSpeedFast})
        Me.mnuSpeed.Name = "mnuSpeed"
        Me.mnuSpeed.Size = New System.Drawing.Size(106, 22)
        Me.mnuSpeed.Text = "Speed"
        '
        'mnuSpeedSlow
        '
        Me.mnuSpeedSlow.Name = "mnuSpeedSlow"
        Me.mnuSpeedSlow.Size = New System.Drawing.Size(114, 22)
        Me.mnuSpeedSlow.Tag = "0"
        Me.mnuSpeedSlow.Text = "Slow"
        '
        'mnuSpeedNormal
        '
        Me.mnuSpeedNormal.Checked = True
        Me.mnuSpeedNormal.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuSpeedNormal.Name = "mnuSpeedNormal"
        Me.mnuSpeedNormal.Size = New System.Drawing.Size(114, 22)
        Me.mnuSpeedNormal.Tag = "1"
        Me.mnuSpeedNormal.Text = "Normal"
        '
        'mnuSpeedFast
        '
        Me.mnuSpeedFast.Name = "mnuSpeedFast"
        Me.mnuSpeedFast.Size = New System.Drawing.Size(114, 22)
        Me.mnuSpeedFast.Tag = "2"
        Me.mnuSpeedFast.Text = "Fast"
        '
        'mnuBucleList
        '
        Me.mnuBucleList.Name = "mnuBucleList"
        Me.mnuBucleList.Size = New System.Drawing.Size(175, 22)
        Me.mnuBucleList.Text = "Bucle del llistat"
        '
        'toolStripSeparator17
        '
        Me.toolStripSeparator17.Name = "toolStripSeparator17"
        Me.toolStripSeparator17.Size = New System.Drawing.Size(172, 6)
        '
        'mnuInsertStop
        '
        Me.mnuInsertStop.Image = CType(resources.GetObject("mnuInsertStop.Image"), System.Drawing.Image)
        Me.mnuInsertStop.Name = "mnuInsertStop"
        Me.mnuInsertStop.Size = New System.Drawing.Size(175, 22)
        Me.mnuInsertStop.Text = "Inserta Punt Stop"
        '
        'AutomàticToolStripMenuItem
        '
        Me.AutomàticToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuIniDirect, Me.mnuForceDirectMusic, Me.mnuAutoSincroMare, Me.mnuSH})
        Me.AutomàticToolStripMenuItem.Name = "AutomàticToolStripMenuItem"
        Me.AutomàticToolStripMenuItem.Size = New System.Drawing.Size(75, 19)
        Me.AutomàticToolStripMenuItem.Text = "Automàtic"
        '
        'mnuIniDirect
        '
        Me.mnuIniDirect.Name = "mnuIniDirect"
        Me.mnuIniDirect.Size = New System.Drawing.Size(307, 22)
        Me.mnuIniDirect.Text = "Iniciar MSC Directe al final de la reproducció"
        '
        'mnuForceDirectMusic
        '
        Me.mnuForceDirectMusic.Checked = True
        Me.mnuForceDirectMusic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuForceDirectMusic.Enabled = False
        Me.mnuForceDirectMusic.Name = "mnuForceDirectMusic"
        Me.mnuForceDirectMusic.Size = New System.Drawing.Size(307, 22)
        Me.mnuForceDirectMusic.Text = "Forçar MSC Directe a  Música"
        '
        'mnuAutoSincroMare
        '
        Me.mnuAutoSincroMare.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAutoSincroStartPlay, Me.mnuAutoSincroEndPlay, Me.toolStripSeparator18, Me.mnuAutoSincro59, Me.mnuAutoSincro00, Me.mnuAutoSincro29, Me.mnuAutoSincroXX, Me.ToolStripSeparator6, Me.mnuAutoSincH_PRG})
        Me.mnuAutoSincroMare.Enabled = False
        Me.mnuAutoSincroMare.Name = "mnuAutoSincroMare"
        Me.mnuAutoSincroMare.Size = New System.Drawing.Size(307, 22)
        Me.mnuAutoSincroMare.Text = "Sincronitzar"
        '
        'mnuAutoSincroStartPlay
        '
        Me.mnuAutoSincroStartPlay.Name = "mnuAutoSincroStartPlay"
        Me.mnuAutoSincroStartPlay.Size = New System.Drawing.Size(187, 22)
        Me.mnuAutoSincroStartPlay.Tag = ""
        Me.mnuAutoSincroStartPlay.Text = "Start Play"
        '
        'mnuAutoSincroEndPlay
        '
        Me.mnuAutoSincroEndPlay.Checked = True
        Me.mnuAutoSincroEndPlay.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuAutoSincroEndPlay.Name = "mnuAutoSincroEndPlay"
        Me.mnuAutoSincroEndPlay.Size = New System.Drawing.Size(187, 22)
        Me.mnuAutoSincroEndPlay.Tag = ""
        Me.mnuAutoSincroEndPlay.Text = "End Play"
        '
        'toolStripSeparator18
        '
        Me.toolStripSeparator18.Name = "toolStripSeparator18"
        Me.toolStripSeparator18.Size = New System.Drawing.Size(184, 6)
        '
        'mnuAutoSincro59
        '
        Me.mnuAutoSincro59.Name = "mnuAutoSincro59"
        Me.mnuAutoSincro59.Size = New System.Drawing.Size(187, 22)
        Me.mnuAutoSincro59.Text = "HH:59:59"
        '
        'mnuAutoSincro00
        '
        Me.mnuAutoSincro00.Name = "mnuAutoSincro00"
        Me.mnuAutoSincro00.Size = New System.Drawing.Size(187, 22)
        Me.mnuAutoSincro00.Text = "HH:00:00"
        '
        'mnuAutoSincro29
        '
        Me.mnuAutoSincro29.Name = "mnuAutoSincro29"
        Me.mnuAutoSincro29.Size = New System.Drawing.Size(187, 22)
        Me.mnuAutoSincro29.Text = "HH:29:59"
        '
        'mnuAutoSincroXX
        '
        Me.mnuAutoSincroXX.Name = "mnuAutoSincroXX"
        Me.mnuAutoSincroXX.Size = New System.Drawing.Size(187, 22)
        Me.mnuAutoSincroXX.Text = "Altres ..."
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(184, 6)
        '
        'mnuAutoSincH_PRG
        '
        Me.mnuAutoSincH_PRG.Name = "mnuAutoSincH_PRG"
        Me.mnuAutoSincH_PRG.Size = New System.Drawing.Size(187, 22)
        Me.mnuAutoSincH_PRG.Text = "Hora Sincro.: 00:00:00"
        '
        'mnuSH
        '
        Me.mnuSH.Name = "mnuSH"
        Me.mnuSH.Size = New System.Drawing.Size(307, 22)
        Me.mnuSH.Text = "Senyals Horaris"
        '
        'PresentacióToolStripMenuItem
        '
        Me.PresentacióToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBotons, Me.ToolStripSeparator4, Me.mnuColVisibles})
        Me.PresentacióToolStripMenuItem.Name = "PresentacióToolStripMenuItem"
        Me.PresentacióToolStripMenuItem.Size = New System.Drawing.Size(80, 19)
        Me.PresentacióToolStripMenuItem.Text = "Presentació"
        '
        'mnuBotons
        '
        Me.mnuBotons.Name = "mnuBotons"
        Me.mnuBotons.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.mnuBotons.Size = New System.Drawing.Size(247, 22)
        Me.mnuBotons.Text = "Presentació per botonera"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(244, 6)
        '
        'mnuColVisibles
        '
        Me.mnuColVisibles.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuColumnVRitme, Me.mnuColumnHora, Me.mnuColumnDurada, Me.mnuColumnInterp})
        Me.mnuColVisibles.Name = "mnuColVisibles"
        Me.mnuColVisibles.Size = New System.Drawing.Size(247, 22)
        Me.mnuColVisibles.Text = "Columnes Visibles"
        '
        'mnuColumnVRitme
        '
        Me.mnuColumnVRitme.Checked = True
        Me.mnuColumnVRitme.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuColumnVRitme.Name = "mnuColumnVRitme"
        Me.mnuColumnVRitme.Size = New System.Drawing.Size(148, 22)
        Me.mnuColumnVRitme.Text = "Ritme"
        '
        'mnuColumnHora
        '
        Me.mnuColumnHora.Checked = True
        Me.mnuColumnHora.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuColumnHora.Name = "mnuColumnHora"
        Me.mnuColumnHora.Size = New System.Drawing.Size(148, 22)
        Me.mnuColumnHora.Text = "Hora Radiació"
        '
        'mnuColumnDurada
        '
        Me.mnuColumnDurada.Checked = True
        Me.mnuColumnDurada.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuColumnDurada.Name = "mnuColumnDurada"
        Me.mnuColumnDurada.Size = New System.Drawing.Size(148, 22)
        Me.mnuColumnDurada.Text = "Durada"
        '
        'mnuColumnInterp
        '
        Me.mnuColumnInterp.Checked = True
        Me.mnuColumnInterp.CheckState = System.Windows.Forms.CheckState.Checked
        Me.mnuColumnInterp.Name = "mnuColumnInterp"
        Me.mnuColumnInterp.Size = New System.Drawing.Size(148, 22)
        Me.mnuColumnInterp.Text = "Intèrpret"
        '
        'EinesToolStripMenuItem
        '
        Me.EinesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSetTime, Me.mnuLoadVar, Me.ToolStripSeparator8, Me.mnuDevice, Me.mnuDevicePreEscolta, Me.mnuSavedevicePlay, Me.mnuDelDevicePlay, Me.toolStripSeparator15, Me.mnuControlRemot, Me.mnuPanicButton, Me.ToolStripSeparator5, Me.mnuAbout, Me.mnuHelp, Me.mnuHelpInternet, Me.ToolStripSeparator12, Me.mnuLang, Me.mnuProgEditAudio})
        Me.EinesToolStripMenuItem.Name = "EinesToolStripMenuItem"
        Me.EinesToolStripMenuItem.Size = New System.Drawing.Size(46, 19)
        Me.EinesToolStripMenuItem.Text = "Eines"
        '
        'mnuSetTime
        '
        Me.mnuSetTime.Name = "mnuSetTime"
        Me.mnuSetTime.Size = New System.Drawing.Size(288, 22)
        Me.mnuSetTime.Text = "Establir hora del servidor"
        Me.mnuSetTime.Visible = False
        '
        'mnuLoadVar
        '
        Me.mnuLoadVar.Name = "mnuLoadVar"
        Me.mnuLoadVar.Size = New System.Drawing.Size(288, 22)
        Me.mnuLoadVar.Text = "Actualitzar variables"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(285, 6)
        '
        'mnuDevice
        '
        Me.mnuDevice.Name = "mnuDevice"
        Me.mnuDevice.Size = New System.Drawing.Size(288, 22)
        Me.mnuDevice.Text = "Dispositius de so (Play)"
        '
        'mnuDevicePreEscolta
        '
        Me.mnuDevicePreEscolta.Name = "mnuDevicePreEscolta"
        Me.mnuDevicePreEscolta.Size = New System.Drawing.Size(288, 22)
        Me.mnuDevicePreEscolta.Text = "Dispositius de so (Pre-escolta)"
        '
        'mnuSavedevicePlay
        '
        Me.mnuSavedevicePlay.Name = "mnuSavedevicePlay"
        Me.mnuSavedevicePlay.Size = New System.Drawing.Size(288, 22)
        Me.mnuSavedevicePlay.Text = "Salvar la assignació actual (Play)"
        Me.mnuSavedevicePlay.ToolTipText = "Salva la assignació actual del dispositiu de reproducció"
        '
        'mnuDelDevicePlay
        '
        Me.mnuDelDevicePlay.Name = "mnuDelDevicePlay"
        Me.mnuDelDevicePlay.Size = New System.Drawing.Size(288, 22)
        Me.mnuDelDevicePlay.Text = "Eliminar les assignacions personalitzades"
        '
        'toolStripSeparator15
        '
        Me.toolStripSeparator15.Name = "toolStripSeparator15"
        Me.toolStripSeparator15.Size = New System.Drawing.Size(285, 6)
        '
        'mnuControlRemot
        '
        Me.mnuControlRemot.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuBOXCTL04, Me.mnuAirence})
        Me.mnuControlRemot.Name = "mnuControlRemot"
        Me.mnuControlRemot.Size = New System.Drawing.Size(288, 22)
        Me.mnuControlRemot.Text = "Control remot"
        '
        'mnuBOXCTL04
        '
        Me.mnuBOXCTL04.Name = "mnuBOXCTL04"
        Me.mnuBOXCTL04.Size = New System.Drawing.Size(157, 22)
        Me.mnuBOXCTL04.Text = "MSC Box CTL04"
        '
        'mnuAirence
        '
        Me.mnuAirence.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAirenceMap})
        Me.mnuAirence.Name = "mnuAirence"
        Me.mnuAirence.Size = New System.Drawing.Size(157, 22)
        Me.mnuAirence.Text = "Airence"
        '
        'mnuAirenceMap
        '
        Me.mnuAirenceMap.Enabled = False
        Me.mnuAirenceMap.Image = CType(resources.GetObject("mnuAirenceMap.Image"), System.Drawing.Image)
        Me.mnuAirenceMap.Name = "mnuAirenceMap"
        Me.mnuAirenceMap.Size = New System.Drawing.Size(141, 22)
        Me.mnuAirenceMap.Text = "Airence Map"
        Me.mnuAirenceMap.Visible = False
        '
        'mnuPanicButton
        '
        Me.mnuPanicButton.Image = CType(resources.GetObject("mnuPanicButton.Image"), System.Drawing.Image)
        Me.mnuPanicButton.Name = "mnuPanicButton"
        Me.mnuPanicButton.Size = New System.Drawing.Size(288, 22)
        Me.mnuPanicButton.Text = "Botó Pànic "
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(285, 6)
        '
        'mnuAbout
        '
        Me.mnuAbout.Image = CType(resources.GetObject("mnuAbout.Image"), System.Drawing.Image)
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(288, 22)
        Me.mnuAbout.Text = "Referent a MSC"
        '
        'mnuHelp
        '
        Me.mnuHelp.Image = CType(resources.GetObject("mnuHelp.Image"), System.Drawing.Image)
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.mnuHelp.Size = New System.Drawing.Size(288, 22)
        Me.mnuHelp.Text = "Manual OnLine"
        '
        'mnuHelpInternet
        '
        Me.mnuHelpInternet.Name = "mnuHelpInternet"
        Me.mnuHelpInternet.Size = New System.Drawing.Size(288, 22)
        Me.mnuHelpInternet.Text = "MSC a Internet"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(285, 6)
        '
        'mnuLang
        '
        Me.mnuLang.Name = "mnuLang"
        Me.mnuLang.Size = New System.Drawing.Size(288, 22)
        Me.mnuLang.Text = "Idioma Interfície"
        '
        'mnuProgEditAudio
        '
        Me.mnuProgEditAudio.Name = "mnuProgEditAudio"
        Me.mnuProgEditAudio.Size = New System.Drawing.Size(288, 22)
        Me.mnuProgEditAudio.Text = "Escollir programa d'edició d'àudio"
        '
        'tmrDisplay
        '
        Me.tmrDisplay.Enabled = True
        Me.tmrDisplay.Interval = 500
        '
        'tmrRellotge
        '
        Me.tmrRellotge.Enabled = True
        '
        'cmdTancar
        '
        Me.cmdTancar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdTancar.Image = CType(resources.GetObject("cmdTancar.Image"), System.Drawing.Image)
        Me.cmdTancar.Location = New System.Drawing.Point(274, 0)
        Me.cmdTancar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdTancar.Name = "cmdTancar"
        Me.cmdTancar.Size = New System.Drawing.Size(80, 58)
        Me.cmdTancar.TabIndex = 5
        Me.cmdTancar.UseVisualStyleBackColor = True
        '
        'cmdLoad
        '
        Me.cmdLoad.Image = CType(resources.GetObject("cmdLoad.Image"), System.Drawing.Image)
        Me.cmdLoad.Location = New System.Drawing.Point(187, 0)
        Me.cmdLoad.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdLoad.Name = "cmdLoad"
        Me.cmdLoad.Size = New System.Drawing.Size(80, 58)
        Me.cmdLoad.TabIndex = 4
        Me.cmdLoad.UseVisualStyleBackColor = True
        '
        'cmdBorrar
        '
        Me.cmdBorrar.Enabled = False
        Me.cmdBorrar.Image = CType(resources.GetObject("cmdBorrar.Image"), System.Drawing.Image)
        Me.cmdBorrar.Location = New System.Drawing.Point(9, 0)
        Me.cmdBorrar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdBorrar.Name = "cmdBorrar"
        Me.cmdBorrar.Size = New System.Drawing.Size(85, 58)
        Me.cmdBorrar.TabIndex = 1
        Me.cmdBorrar.UseVisualStyleBackColor = True
        '
        'pnlControlsEdit
        '
        Me.pnlControlsEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pnlControlsEdit.Controls.Add(Me.cmdTancar)
        Me.pnlControlsEdit.Controls.Add(Me.cmdLoad)
        Me.pnlControlsEdit.Controls.Add(Me.cmdSalvar)
        Me.pnlControlsEdit.Controls.Add(Me.cmdBorrar)
        Me.pnlControlsEdit.Location = New System.Drawing.Point(7, 694)
        Me.pnlControlsEdit.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pnlControlsEdit.Name = "pnlControlsEdit"
        Me.pnlControlsEdit.Size = New System.Drawing.Size(364, 59)
        Me.pnlControlsEdit.TabIndex = 0
        '
        'cmdSalvar
        '
        Me.cmdSalvar.Enabled = False
        Me.cmdSalvar.Image = CType(resources.GetObject("cmdSalvar.Image"), System.Drawing.Image)
        Me.cmdSalvar.Location = New System.Drawing.Point(101, 0)
        Me.cmdSalvar.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdSalvar.Name = "cmdSalvar"
        Me.cmdSalvar.Size = New System.Drawing.Size(78, 58)
        Me.cmdSalvar.TabIndex = 2
        Me.cmdSalvar.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cmdPlayPre, Me.ToolStripLabel1, Me.picDirecte, Me.ToolStripSeparator10, Me.cmdMesGranBotons, Me.lbTamanyBotons, Me.cmdMenyGranBotons, Me.lbInfo})
        Me.ToolStrip1.Location = New System.Drawing.Point(23, 78)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(535, 25)
        Me.ToolStrip1.TabIndex = 8
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'cmdPlayPre
        '
        Me.cmdPlayPre.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.cmdPlayPre.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.cmdPlayPre.Image = CType(resources.GetObject("cmdPlayPre.Image"), System.Drawing.Image)
        Me.cmdPlayPre.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdPlayPre.Name = "cmdPlayPre"
        Me.cmdPlayPre.Size = New System.Drawing.Size(23, 22)
        Me.cmdPlayPre.Text = "Pre-escolta"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(87, 22)
        Me.ToolStripLabel1.Text = "Pre-escolta:      "
        '
        'picDirecte
        '
        Me.picDirecte.Image = CType(resources.GetObject("picDirecte.Image"), System.Drawing.Image)
        Me.picDirecte.Name = "picDirecte"
        Me.picDirecte.Size = New System.Drawing.Size(16, 22)
        Me.picDirecte.Visible = False
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(6, 25)
        '
        'cmdMesGranBotons
        '
        Me.cmdMesGranBotons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.cmdMesGranBotons.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdMesGranBotons.Name = "cmdMesGranBotons"
        Me.cmdMesGranBotons.Size = New System.Drawing.Size(23, 22)
        Me.cmdMesGranBotons.Text = "+"
        Me.cmdMesGranBotons.ToolTipText = "Tamany dels botons"
        '
        'lbTamanyBotons
        '
        Me.lbTamanyBotons.AutoSize = False
        Me.lbTamanyBotons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.lbTamanyBotons.Image = CType(resources.GetObject("lbTamanyBotons.Image"), System.Drawing.Image)
        Me.lbTamanyBotons.Name = "lbTamanyBotons"
        Me.lbTamanyBotons.Size = New System.Drawing.Size(22, 22)
        Me.lbTamanyBotons.ToolTipText = "Tamany dels botons"
        '
        'cmdMenyGranBotons
        '
        Me.cmdMenyGranBotons.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.cmdMenyGranBotons.Enabled = False
        Me.cmdMenyGranBotons.Image = CType(resources.GetObject("cmdMenyGranBotons.Image"), System.Drawing.Image)
        Me.cmdMenyGranBotons.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.cmdMenyGranBotons.Name = "cmdMenyGranBotons"
        Me.cmdMenyGranBotons.Size = New System.Drawing.Size(23, 22)
        Me.cmdMenyGranBotons.Text = "-"
        Me.cmdMenyGranBotons.ToolTipText = "Tamany dels botons"
        '
        'lbInfo
        '
        Me.lbInfo.Image = CType(resources.GetObject("lbInfo.Image"), System.Drawing.Image)
        Me.lbInfo.Name = "lbInfo"
        Me.lbInfo.Size = New System.Drawing.Size(104, 22)
        Me.lbInfo.Text = "Sincro: 12:35:26"
        Me.lbInfo.Visible = False
        '
        'picPlay
        '
        Me.picPlay.ErrorImage = CType(resources.GetObject("picPlay.ErrorImage"), System.Drawing.Image)
        Me.picPlay.Image = CType(resources.GetObject("picPlay.Image"), System.Drawing.Image)
        Me.picPlay.Location = New System.Drawing.Point(372, 569)
        Me.picPlay.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picPlay.Name = "picPlay"
        Me.picPlay.Size = New System.Drawing.Size(19, 20)
        Me.picPlay.TabIndex = 9
        Me.picPlay.TabStop = False
        Me.picPlay.Visible = False
        '
        'picStop
        '
        Me.picStop.ErrorImage = CType(resources.GetObject("picStop.ErrorImage"), System.Drawing.Image)
        Me.picStop.Image = CType(resources.GetObject("picStop.Image"), System.Drawing.Image)
        Me.picStop.Location = New System.Drawing.Point(398, 569)
        Me.picStop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picStop.Name = "picStop"
        Me.picStop.Size = New System.Drawing.Size(19, 18)
        Me.picStop.TabIndex = 10
        Me.picStop.TabStop = False
        Me.picStop.Visible = False
        '
        'picPreEsc
        '
        Me.picPreEsc.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picPreEsc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picPreEsc.Location = New System.Drawing.Point(516, 35)
        Me.picPreEsc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picPreEsc.Name = "picPreEsc"
        Me.picPreEsc.Size = New System.Drawing.Size(17, 24)
        Me.picPreEsc.TabIndex = 11
        Me.picPreEsc.TabStop = False
        Me.picPreEsc.Visible = False
        '
        'ToolTipInfo
        '
        Me.ToolTipInfo.IsBalloon = True
        '
        'picWave
        '
        Me.picWave.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picWave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picWave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.picWave.Location = New System.Drawing.Point(0, 73)
        Me.picWave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picWave.Name = "picWave"
        Me.picWave.Size = New System.Drawing.Size(556, 112)
        Me.picWave.TabIndex = 0
        Me.picWave.TabStop = False
        Me.ToolTipInfo.SetToolTip(Me.picWave, "avançar/retrocedir")
        '
        'mnu_wave
        '
        Me.mnu_wave.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuWaveSetIntro, Me.mnuWaveSetPointMix, Me.toolStripSeparator14, Me.mnuWavePlay})
        Me.mnu_wave.Name = "mnu_wave"
        Me.mnu_wave.Size = New System.Drawing.Size(207, 76)
        '
        'mnuWaveSetIntro
        '
        Me.mnuWaveSetIntro.Name = "mnuWaveSetIntro"
        Me.mnuWaveSetIntro.Size = New System.Drawing.Size(206, 22)
        Me.mnuWaveSetIntro.Text = "Establir aquí punt intro"
        '
        'mnuWaveSetPointMix
        '
        Me.mnuWaveSetPointMix.Name = "mnuWaveSetPointMix"
        Me.mnuWaveSetPointMix.Size = New System.Drawing.Size(206, 22)
        Me.mnuWaveSetPointMix.Text = "Establir aquí punt mescla"
        '
        'toolStripSeparator14
        '
        Me.toolStripSeparator14.Name = "toolStripSeparator14"
        Me.toolStripSeparator14.Size = New System.Drawing.Size(203, 6)
        '
        'mnuWavePlay
        '
        Me.mnuWavePlay.Name = "mnuWavePlay"
        Me.mnuWavePlay.Size = New System.Drawing.Size(206, 22)
        Me.mnuWavePlay.Text = "Play aquí"
        '
        'tmr_Play
        '
        Me.tmr_Play.Enabled = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'flowBotons
        '
        Me.flowBotons.AllowDrop = True
        Me.flowBotons.AutoScroll = True
        Me.flowBotons.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flowBotons.Location = New System.Drawing.Point(0, 0)
        Me.flowBotons.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.flowBotons.Name = "flowBotons"
        Me.flowBotons.Size = New System.Drawing.Size(556, 225)
        Me.flowBotons.TabIndex = 2
        Me.flowBotons.Visible = False
        '
        'splitContainer1
        '
        Me.splitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.splitContainer1.Location = New System.Drawing.Point(2, 68)
        Me.splitContainer1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.splitContainer1.Name = "splitContainer1"
        Me.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.picVis)
        Me.splitContainer1.Panel1.Controls.Add(Me.panel3)
        Me.splitContainer1.Panel1.Controls.Add(Me.picWave)
        Me.splitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.splitContainer1.Panel1MinSize = 180
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.PanelListDisp)
        Me.splitContainer1.Panel2.Controls.Add(Me.flowBotons)
        Me.splitContainer1.Size = New System.Drawing.Size(556, 619)
        Me.splitContainer1.SplitterDistance = 391
        Me.splitContainer1.SplitterWidth = 3
        Me.splitContainer1.TabIndex = 12
        '
        'progressBarBotons
        '
        Me.progressBarBotons.Location = New System.Drawing.Point(194, 50)
        Me.progressBarBotons.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.progressBarBotons.Name = "progressBarBotons"
        Me.progressBarBotons.Size = New System.Drawing.Size(117, 13)
        Me.progressBarBotons.TabIndex = 13
        Me.progressBarBotons.Visible = False
        '
        'picPause
        '
        Me.picPause.Image = CType(resources.GetObject("picPause.Image"), System.Drawing.Image)
        Me.picPause.Location = New System.Drawing.Point(423, 573)
        Me.picPause.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picPause.Name = "picPause"
        Me.picPause.Size = New System.Drawing.Size(13, 22)
        Me.picPause.TabIndex = 14
        Me.picPause.TabStop = False
        Me.picPause.Visible = False
        '
        'picCueOn
        '
        Me.picCueOn.Image = CType(resources.GetObject("picCueOn.Image"), System.Drawing.Image)
        Me.picCueOn.Location = New System.Drawing.Point(372, 595)
        Me.picCueOn.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picCueOn.Name = "picCueOn"
        Me.picCueOn.Size = New System.Drawing.Size(22, 22)
        Me.picCueOn.TabIndex = 15
        Me.picCueOn.TabStop = False
        Me.picCueOn.Visible = False
        '
        'picCueOFF
        '
        Me.picCueOFF.Image = CType(resources.GetObject("picCueOFF.Image"), System.Drawing.Image)
        Me.picCueOFF.Location = New System.Drawing.Point(411, 605)
        Me.picCueOFF.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.picCueOFF.Name = "picCueOFF"
        Me.picCueOFF.Size = New System.Drawing.Size(21, 10)
        Me.picCueOFF.TabIndex = 16
        Me.picCueOFF.TabStop = False
        Me.picCueOFF.Visible = False
        '
        'frmCartut
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdTancar
        Me.ClientSize = New System.Drawing.Size(581, 755)
        Me.Controls.Add(Me.picCueOFF)
        Me.Controls.Add(Me.picCueOn)
        Me.Controls.Add(Me.picPause)
        Me.Controls.Add(Me.progressBarBotons)
        Me.Controls.Add(Me.pnlControlsEdit)
        Me.Controls.Add(Me.lbMicroDespMes)
        Me.Controls.Add(Me.lbMicroDespMenys)
        Me.Controls.Add(Me.picPreEsc)
        Me.Controls.Add(Me.picStop)
        Me.Controls.Add(Me.picPlay)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.mnuDef)
        Me.Controls.Add(Me.splitContainer1)
        Me.Font = New System.Drawing.Font("Segoe UI Symbol", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MinimumSize = New System.Drawing.Size(497, 411)
        Me.Name = "frmCartut"
        Me.Padding = New System.Windows.Forms.Padding(23, 78, 23, 26)
        Me.Text = "MCS Cartutxera"
        Me.PanelListDisp.ResumeLayout(False)
        Me.PanelListDisp.PerformLayout()
        Me.toolStrip2.ResumeLayout(False)
        Me.toolStrip2.PerformLayout()
        Me.ContextMenuStripBotoDret.ResumeLayout(False)
        Me.panel3.ResumeLayout(False)
        Me.panel4.ResumeLayout(False)
        Me.panel4.PerformLayout()
        CType(Me.sldPith, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picVis, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.farLoop.ResumeLayout(False)
        Me.farLoop.PerformLayout()
        Me.mnuDef.ResumeLayout(False)
        Me.mnuDef.PerformLayout()
        Me.pnlControlsEdit.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.picPlay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picStop, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picPreEsc, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picWave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnu_wave.ResumeLayout(False)
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        CType(Me.splitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitContainer1.ResumeLayout(False)
        CType(Me.picPause, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCueOn, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picCueOFF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents mnuAutoSincro00 As System.Windows.Forms.ToolStripMenuItem
	
	Private toolStripSeparator18 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents mnuAutoSincroEndPlay As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuAutoSincroStartPlay As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuSpeedFast As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuSpeedNormal As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuSpeedSlow As System.Windows.Forms.ToolStripMenuItem
    Private mnuSpeed As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuAirenceMap As System.Windows.Forms.ToolStripMenuItem
    Private toolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents txtPlayURL As System.Windows.Forms.ToolStripTextBox
    Private WithEvents mnuPlayURL As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents pichToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents pichToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private picCueOFF As System.Windows.Forms.PictureBox
	Private picCueOn As System.Windows.Forms.PictureBox
	Private picPause As System.Windows.Forms.PictureBox
    Private WithEvents mnuColumnInterp As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuColumnDurada As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuColumnHora As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuColumnVRitme As System.Windows.Forms.ToolStripMenuItem
    Private mnuColVisibles As System.Windows.Forms.ToolStripMenuItem
	Private toolStripSeparator17 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents mnuInsertStop As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuAirence As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuPanicButton As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuBOXCTL04 As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuControlRemot As System.Windows.Forms.ToolStripMenuItem
    Private toolStripSeparator15 As System.Windows.Forms.ToolStripSeparator

    '	Private progressBarBotons As System.Windows.Forms.ProgressBar
    '	Friend sldDuration As System.Windows.Forms.TrackBar
    '	Friend lbMicroDespMes As System.Windows.Forms.LinkLabel
    '	Friend lbMicroDespMenys As System.Windows.Forms.LinkLabel
    '	Friend ImageListPlayer As System.Windows.Forms.ImageList
    '	Friend tmrTitol As System.Windows.Forms.Timer
    '	Friend CommonDialog1 As System.Windows.Forms.OpenFileDialog
    '	Friend tmrVuMeter As System.Windows.Forms.Timer
    '	Friend tmr_FadeOunt As System.Windows.Forms.Timer
    '	Friend mnuExit As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuMoveDBS As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuExplorerDBS As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuExplorerPC As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuReOrdCart As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuNewCartut As System.Windows.Forms.ToolStripMenuItem
    '	Friend ProgramaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuDef As System.Windows.Forms.MenuStrip
    '	Friend mnuArxiu As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuOpenFile As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuSaveFile As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStrip1 As System.Windows.Forms.ToolStrip
    '	Friend cmdSalvar As System.Windows.Forms.Button
    '	Friend pnlControlsEdit As System.Windows.Forms.Panel
    '	Friend cmdPlayPre As System.Windows.Forms.ToolStripButton
    '	Friend ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    '	Friend ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    '	Friend cmdBorrar As System.Windows.Forms.Button
    '	Friend picDirecte As System.Windows.Forms.ToolStripLabel
    '	Friend cmdLoad As System.Windows.Forms.Button
    '	Friend cmdTancar As System.Windows.Forms.Button
    '	Friend tmrRellotge As System.Windows.Forms.Timer
    '	Friend cmdMesGranBotons As System.Windows.Forms.ToolStripButton
    '	Friend tmrDisplay As System.Windows.Forms.Timer
    '	Friend lbTamanyBotons As System.Windows.Forms.ToolStripLabel
    '	Friend cmdMenyGranBotons As System.Windows.Forms.ToolStripButton
    '	Friend tmr_Play As System.Windows.Forms.Timer
    '	Friend ImageList As System.Windows.Forms.ImageList
    '	Friend mnuEditAudio As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuContextDel As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuDesmarcar As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuSetPoxPlay As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    '	Friend ToolStripTextBox1 As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuPreEscoltaStop As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuPreEscolta As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuMesInfo As System.Windows.Forms.ToolStripMenuItem
    '	Friend ContextMenuStripBotoDret As System.Windows.Forms.ContextMenuStrip
    '	Private columnHeader1 As System.Windows.Forms.ColumnHeader
    '	Friend ColumnHeader24 As System.Windows.Forms.ColumnHeader
    '	Friend ColumnHeader23 As System.Windows.Forms.ColumnHeader
    '	Friend ColumnHeader17 As System.Windows.Forms.ColumnHeader
    '	Friend ColumnHeader22 As System.Windows.Forms.ColumnHeader
    '	Friend ColumnHeader21 As System.Windows.Forms.ColumnHeader
    '	Friend lstDisp As System.Windows.Forms.ListView
    '	Private lbCPU As System.Windows.Forms.ToolStripLabel
    '	Private toolStripLabel2 As System.Windows.Forms.ToolStripLabel
    '	Private lbTotal As System.Windows.Forms.ToolStripLabel
    '	Private toolStrip2 As System.Windows.Forms.ToolStrip
    '	Friend PanelListDisp As System.Windows.Forms.Panel
    '	Friend picVis As System.Windows.Forms.PictureBox
    '	Friend sldPith As System.Windows.Forms.TrackBar
    '	Friend cmdStop As System.Windows.Forms.Button
    '	Friend cmdPrev As System.Windows.Forms.Button
    '	Friend cmdPlay As System.Windows.Forms.Button
    '	Friend cmdNext As System.Windows.Forms.Button
    '	Friend cmdCue As System.Windows.Forms.Button
    '	Friend lbFormatTime As System.Windows.Forms.Label
    '	Friend lbStrPith As System.Windows.Forms.LinkLabel
    '	Friend Label1 As System.Windows.Forms.Label
    '	Friend lbCue As System.Windows.Forms.Label
    '	Friend lbHora As System.Windows.Forms.Label
    '	Friend Label3 As System.Windows.Forms.Label
    '	Friend lbRestaIntro As System.Windows.Forms.Label
    '	Friend lbDisplayTitol As System.Windows.Forms.Label
    '	Friend lbDisplayTime As System.Windows.Forms.Label
    '	Friend lbAutoSinc As System.Windows.Forms.Label
    '	Private panel4 As System.Windows.Forms.Panel
    '	Private panel3 As System.Windows.Forms.Panel
    '	Friend cmdLoopInMenys As System.Windows.Forms.LinkLabel
    '	Friend cmdLoopOutMenys As System.Windows.Forms.LinkLabel
    '	Friend lbLoopIn As System.Windows.Forms.Label
    '	Friend cmdLoopInMes As System.Windows.Forms.LinkLabel
    '	Friend cmdLoopIn As System.Windows.Forms.Button
    '	Friend lbLoopOut As System.Windows.Forms.Label
    '	Friend cmdLoopOutMes As System.Windows.Forms.LinkLabel
    '	Friend cmdLoopOut As System.Windows.Forms.Button
    '	Friend cmdLoadLoop As System.Windows.Forms.Button
    '	Friend cmdSaveLoop As System.Windows.Forms.Button
    '	Friend lbIDActual As System.Windows.Forms.Label
    '	Friend chkLoop As System.Windows.Forms.CheckBox
    '	Friend farLoop As System.Windows.Forms.GroupBox
    '	Friend chkContinuos As System.Windows.Forms.CheckBox
    '	Friend GroupBox1 As System.Windows.Forms.GroupBox
    '	Private splitContainer1 As System.Windows.Forms.SplitContainer
    '	Friend OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    '	Friend flowBotons As System.Windows.Forms.FlowLayoutPanel
    '	Friend picPlay As System.Windows.Forms.PictureBox
    '	Friend picStop As System.Windows.Forms.PictureBox
    '	Friend lbInfo As System.Windows.Forms.ToolStripLabel
    '	Friend picPreEsc As System.Windows.Forms.PictureBox
    '	Private mnuWavePlay As System.Windows.Forms.ToolStripMenuItem
    '	Private toolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    '	Private mnuWaveSetPointMix As System.Windows.Forms.ToolStripMenuItem
    '	Private mnuWaveSetIntro As System.Windows.Forms.ToolStripMenuItem
    '	Private mnu_wave As System.Windows.Forms.ContextMenuStrip
    '	Private picWave As System.Windows.Forms.PictureBox
    '	Private picIn As System.Windows.Forms.PictureBox
    '	Friend ToolTipInfo As System.Windows.Forms.ToolTip
    '	Private picOut As System.Windows.Forms.PictureBox
    '	Private mnuCalcBPMOnLoad As System.Windows.Forms.ToolStripMenuItem
    '	Private mnuCalBPMList As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuBucleList As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuAutoFader As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuAutoDel As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuDel As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuLoad As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    '	Private mnuHistoryPlay As System.Windows.Forms.ToolStripMenuItem
    '	Friend ReproduccióToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    '	Friend LoopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    '	Friend AutomàticaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuReproduccio As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuPlayPause As System.Windows.Forms.ToolStripMenuItem
    '	Friend AutomàticToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuStop As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuIniDirect As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuForceDirectMusic As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuProgEditAudio As System.Windows.Forms.ToolStripMenuItem
    '	Private mnuLang As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuHelpInternet As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuHelp As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuAbout As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuDelDevicePlay As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuLoadVar As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuSetTime As System.Windows.Forms.ToolStripMenuItem
    '	Friend EinesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuSavedevicePlay As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuDevice As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuDevicePreEscolta As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    '	Friend mnuBotons As System.Windows.Forms.ToolStripMenuItem
    '	Friend PresentacióToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuAutoSincro29 As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuAutoSincro59 As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuAutoSincroMare As System.Windows.Forms.ToolStripMenuItem
    '	Friend mnuAutoSincroXX As System.Windows.Forms.ToolStripMenuItem
    '	Private mnuSH As System.Windows.Forms.ToolStripMenuItem
    '	Friend ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    '	Friend SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    '	Friend mnuAutoSincH_PRG As System.Windows.Forms.ToolStripMenuItem
    Private progressBarBotons As System.Windows.Forms.ProgressBar
    Private lbCPU As System.Windows.Forms.ToolStripLabel
    Private toolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents mnuWavePlay As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuWaveSetPointMix As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuWaveSetIntro As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnu_wave As System.Windows.Forms.ContextMenuStrip
    Private WithEvents picWave As System.Windows.Forms.PictureBox
    Private splitContainer1 As System.Windows.Forms.SplitContainer
    Private toolStripLabel2 As System.Windows.Forms.ToolStripLabel
    Private lbTotal As System.Windows.Forms.ToolStripLabel
    Private toolStrip2 As System.Windows.Forms.ToolStrip
    Private WithEvents mnuHistoryPlay As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuSH As System.Windows.Forms.ToolStripMenuItem
    Private panel4 As System.Windows.Forms.Panel
    Private WithEvents mnuLang As System.Windows.Forms.ToolStripMenuItem
    Private panel3 As System.Windows.Forms.Panel
    Private WithEvents mnuCalBPMList As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents mnuCalcBPMOnLoad As System.Windows.Forms.ToolStripMenuItem
    Private columnHeader1 As System.Windows.Forms.ColumnHeader

    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lbMicroDespMes As System.Windows.Forms.LinkLabel
    Friend WithEvents picVis As System.Windows.Forms.PictureBox
    Friend WithEvents cmdNext As System.Windows.Forms.Button
    Friend WithEvents cmdPrev As System.Windows.Forms.Button
    Friend WithEvents cmdStop As System.Windows.Forms.Button
    Friend WithEvents cmdPlay As System.Windows.Forms.Button
    Friend WithEvents cmdCue As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lbIDActual As System.Windows.Forms.Label
    Friend WithEvents farLoop As System.Windows.Forms.GroupBox
    Friend WithEvents cmdSaveLoop As System.Windows.Forms.Button
    Friend WithEvents cmdLoadLoop As System.Windows.Forms.Button
    Friend WithEvents cmdLoopOut As System.Windows.Forms.Button
    Friend WithEvents cmdLoopOutMes As System.Windows.Forms.LinkLabel
    Friend WithEvents lbLoopOut As System.Windows.Forms.Label
    Friend WithEvents cmdLoopIn As System.Windows.Forms.Button
    Friend WithEvents cmdLoopInMes As System.Windows.Forms.LinkLabel
    Friend WithEvents lbLoopIn As System.Windows.Forms.Label
    Friend WithEvents cmdLoopOutMenys As System.Windows.Forms.LinkLabel
    Friend WithEvents cmdLoopInMenys As System.Windows.Forms.LinkLabel
    Friend WithEvents lbMicroDespMenys As System.Windows.Forms.LinkLabel
    Friend WithEvents sldPith As System.Windows.Forms.TrackBar
    Friend WithEvents lbAutoSinc As System.Windows.Forms.Label
    Friend WithEvents lbDisplayTime As System.Windows.Forms.Label
    Friend WithEvents lbDisplayTitol As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbRestaIntro As System.Windows.Forms.Label
    Friend WithEvents lbHora As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lbStrPith As System.Windows.Forms.LinkLabel
    Friend WithEvents lbCue As System.Windows.Forms.Label
    Friend WithEvents lbFormatTime As System.Windows.Forms.Label
    Friend WithEvents tmrTitol As System.Windows.Forms.Timer
    Friend WithEvents CommonDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents tmrVuMeter As System.Windows.Forms.Timer
    Friend WithEvents mnuDef As System.Windows.Forms.MenuStrip
    Friend WithEvents ProgramaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuArxiu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpenFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSaveFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAutoDel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReproduccióToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPlayPause As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReproduccio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutomàticaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuLoad As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAutoFader As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AutomàticToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIniDirect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAutoSincroMare As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAutoSincro59 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAutoSincro29 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuAutoSincroXX As System.Windows.Forms.ToolStripMenuItem    
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAutoSincH_PRG As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PresentacióToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBotons As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EinesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSetTime As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuLoadVar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDevice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelpInternet As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrDisplay As System.Windows.Forms.Timer
    Friend WithEvents ImageList As System.Windows.Forms.ImageList
    Friend WithEvents cmdTancar As System.Windows.Forms.Button
    Friend WithEvents cmdLoad As System.Windows.Forms.Button
    Friend WithEvents flowBotons As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents cmdBorrar As System.Windows.Forms.Button
    Friend WithEvents pnlControlsEdit As System.Windows.Forms.Panel
    Friend WithEvents cmdSalvar As System.Windows.Forms.Button
    Friend WithEvents PanelListDisp As System.Windows.Forms.Panel
    Friend WithEvents lstDisp As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader21 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader22 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader23 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader24 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuExplorerDBS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExplorerPC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNewCartut As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDevicePreEscolta As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents cmdPlayPre As System.Windows.Forms.ToolStripButton
    Friend WithEvents ContextMenuStripBotoDret As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuMesInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPreEscolta As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripLabel1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents picPlay As System.Windows.Forms.PictureBox
    Friend WithEvents picStop As System.Windows.Forms.PictureBox
    Friend WithEvents picPreEsc As System.Windows.Forms.PictureBox
    Friend WithEvents lbInfo As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator20 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripTextBox1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmdMesGranBotons As System.Windows.Forms.ToolStripButton
    Friend WithEvents lbTamanyBotons As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmdMenyGranBotons As System.Windows.Forms.ToolStripButton
    Friend WithEvents mnuReOrdCart As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents chkContinuos As System.Windows.Forms.CheckBox
    Friend WithEvents chkLoop As System.Windows.Forms.CheckBox
    Friend WithEvents picDirecte As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolTipInfo As System.Windows.Forms.ToolTip
    Friend WithEvents mnuForceDirectMusic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuDesmarcar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuContextDel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSetPoxPlay As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuPreEscoltaStop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmrRellotge As System.Windows.Forms.Timer
    Friend WithEvents tmr_Play As System.Windows.Forms.Timer
    Friend WithEvents mnuMoveDBS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuBucleList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuProgEditAudio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEditAudio As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tmr_FadeOunt As System.Windows.Forms.Timer
    Friend WithEvents mnuSavedevicePlay As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuDelDevicePlay As System.Windows.Forms.ToolStripMenuItem
End Class
