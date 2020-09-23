<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBuscarTemes
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBuscarTemes))
        Me.txtNomTema = New System.Windows.Forms.TextBox()
        Me.Label1 = New MetroFramework.Controls.MetroLabel()
        Me.Label2 = New MetroFramework.Controls.MetroLabel()
        Me.cbInterp = New System.Windows.Forms.ComboBox()
        Me.cbDisc = New System.Windows.Forms.ComboBox()
        Me.chkTots = New MetroFramework.Controls.MetroCheckBox()
        Me.cbEstil = New System.Windows.Forms.ComboBox()
        Me.cbIdioma = New System.Windows.Forms.ComboBox()
        Me.cbTemp = New System.Windows.Forms.ComboBox()
        Me.cbSubject = New System.Windows.Forms.ComboBox()
        Me.Label3 = New MetroFramework.Controls.MetroLabel()
        Me.Label4 = New MetroFramework.Controls.MetroLabel()
        Me.Label5 = New MetroFramework.Controls.MetroLabel()
        Me.Label6 = New MetroFramework.Controls.MetroLabel()
        Me.Label7 = New MetroFramework.Controls.MetroLabel()
        Me.Label8 = New MetroFramework.Controls.MetroLabel()
        Me.fraPauta = New System.Windows.Forms.GroupBox()
        Me.chkNoFiltre = New MetroFramework.Controls.MetroRadioButton()
        Me.chkOutP = New MetroFramework.Controls.MetroRadioButton()
        Me.chkInP = New MetroFramework.Controls.MetroRadioButton()
        Me.fraAvanç = New System.Windows.Forms.GroupBox()
        Me.grupOrderBy = New System.Windows.Forms.GroupBox()
        Me.chkNumRadia = New MetroFramework.Controls.MetroRadioButton()
        Me.chkDateInDESC = New MetroFramework.Controls.MetroRadioButton()
        Me.chkDateInASC = New MetroFramework.Controls.MetroRadioButton()
        Me.Label13 = New MetroFramework.Controls.MetroLabel()
        Me.Label12 = New MetroFramework.Controls.MetroLabel()
        Me.Label11 = New MetroFramework.Controls.MetroLabel()
        Me.Label10 = New MetroFramework.Controls.MetroLabel()
        Me.chkRestric = New MetroFramework.Controls.MetroCheckBox()
        Me.chkIntrumental = New MetroFramework.Controls.MetroCheckBox()
        Me.txtAny = New System.Windows.Forms.TextBox()
        Me.chkBPMFort = New MetroFramework.Controls.MetroCheckBox()
        Me.txtRitme = New System.Windows.Forms.TextBox()
        Me.txtComent = New System.Windows.Forms.TextBox()
        Me.txtCompositor = New System.Windows.Forms.TextBox()
        Me.chkAddResult = New MetroFramework.Controls.MetroCheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmdMoreFind = New MetroFramework.Controls.MetroTile()
        Me.cmdBuscar = New MetroFramework.Controls.MetroTile()
        Me.CmdTancar = New MetroFramework.Controls.MetroTile()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.Label9 = New MetroFramework.Controls.MetroLabel()
        Me.cmdSortir = New MetroFramework.Controls.MetroLink()
        Me.fraPauta.SuspendLayout()
        Me.fraAvanç.SuspendLayout()
        Me.grupOrderBy.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtNomTema
        '
        Me.txtNomTema.Location = New System.Drawing.Point(126, 87)
        Me.txtNomTema.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNomTema.Name = "txtNomTema"
        Me.txtNomTema.Size = New System.Drawing.Size(283, 25)
        Me.txtNomTema.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(26, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(260, 19)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Buscar cançons segons un filtre determinat."
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(15, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nom cançó"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cbInterp
        '
        Me.cbInterp.FormattingEnabled = True
        Me.cbInterp.Location = New System.Drawing.Point(126, 115)
        Me.cbInterp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbInterp.Name = "cbInterp"
        Me.cbInterp.Size = New System.Drawing.Size(283, 25)
        Me.cbInterp.TabIndex = 3
        '
        'cbDisc
        '
        Me.cbDisc.FormattingEnabled = True
        Me.cbDisc.Location = New System.Drawing.Point(126, 143)
        Me.cbDisc.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbDisc.Name = "cbDisc"
        Me.cbDisc.Size = New System.Drawing.Size(283, 25)
        Me.cbDisc.TabIndex = 4
        '
        'chkTots
        '
        Me.chkTots.AutoSize = True
        Me.chkTots.Location = New System.Drawing.Point(415, 148)
        Me.chkTots.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkTots.Name = "chkTots"
        Me.chkTots.Size = New System.Drawing.Size(16, 0)
        Me.chkTots.TabIndex = 5
        Me.chkTots.UseSelectable = True
        Me.chkTots.Visible = False
        '
        'cbEstil
        '
        Me.cbEstil.FormattingEnabled = True
        Me.cbEstil.Location = New System.Drawing.Point(126, 171)
        Me.cbEstil.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbEstil.Name = "cbEstil"
        Me.cbEstil.Size = New System.Drawing.Size(283, 25)
        Me.cbEstil.TabIndex = 6
        '
        'cbIdioma
        '
        Me.cbIdioma.FormattingEnabled = True
        Me.cbIdioma.Location = New System.Drawing.Point(126, 199)
        Me.cbIdioma.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbIdioma.Name = "cbIdioma"
        Me.cbIdioma.Size = New System.Drawing.Size(283, 25)
        Me.cbIdioma.TabIndex = 7
        '
        'cbTemp
        '
        Me.cbTemp.FormattingEnabled = True
        Me.cbTemp.Location = New System.Drawing.Point(126, 227)
        Me.cbTemp.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbTemp.Name = "cbTemp"
        Me.cbTemp.Size = New System.Drawing.Size(283, 25)
        Me.cbTemp.TabIndex = 8
        '
        'cbSubject
        '
        Me.cbSubject.FormattingEnabled = True
        Me.cbSubject.Location = New System.Drawing.Point(126, 255)
        Me.cbSubject.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cbSubject.Name = "cbSubject"
        Me.cbSubject.Size = New System.Drawing.Size(283, 25)
        Me.cbSubject.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(15, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 17)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Intèrpret"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(15, 147)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 17)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Disc"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(15, 175)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 17)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Estil"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(15, 203)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(104, 17)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "Idioma"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(15, 231)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(104, 17)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Clas. temporal"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(15, 259)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 17)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Clas. Subjectiva"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'fraPauta
        '
        Me.fraPauta.Controls.Add(Me.chkNoFiltre)
        Me.fraPauta.Controls.Add(Me.chkOutP)
        Me.fraPauta.Controls.Add(Me.chkInP)
        Me.fraPauta.Location = New System.Drawing.Point(126, 283)
        Me.fraPauta.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.fraPauta.Name = "fraPauta"
        Me.fraPauta.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.fraPauta.Size = New System.Drawing.Size(283, 46)
        Me.fraPauta.TabIndex = 16
        Me.fraPauta.TabStop = False
        Me.fraPauta.Text = "Disponibilitat"
        '
        'chkNoFiltre
        '
        Me.chkNoFiltre.AutoSize = True
        Me.chkNoFiltre.Checked = True
        Me.chkNoFiltre.Location = New System.Drawing.Point(196, 17)
        Me.chkNoFiltre.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkNoFiltre.Name = "chkNoFiltre"
        Me.chkNoFiltre.Size = New System.Drawing.Size(74, 15)
        Me.chkNoFiltre.TabIndex = 2
        Me.chkNoFiltre.TabStop = True
        Me.chkNoFiltre.Text = "Indiferent"
        Me.chkNoFiltre.UseSelectable = True
        '
        'chkOutP
        '
        Me.chkOutP.AutoSize = True
        Me.chkOutP.Location = New System.Drawing.Point(94, 17)
        Me.chkOutP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkOutP.Name = "chkOutP"
        Me.chkOutP.Size = New System.Drawing.Size(86, 15)
        Me.chkOutP.TabIndex = 1
        Me.chkOutP.Text = "Desectivada"
        Me.chkOutP.UseSelectable = True
        '
        'chkInP
        '
        Me.chkInP.AutoSize = True
        Me.chkInP.Location = New System.Drawing.Point(9, 17)
        Me.chkInP.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkInP.Name = "chkInP"
        Me.chkInP.Size = New System.Drawing.Size(69, 15)
        Me.chkInP.TabIndex = 0
        Me.chkInP.Text = "Activada"
        Me.chkInP.UseSelectable = True
        '
        'fraAvanç
        '
        Me.fraAvanç.Controls.Add(Me.grupOrderBy)
        Me.fraAvanç.Controls.Add(Me.Label13)
        Me.fraAvanç.Controls.Add(Me.Label12)
        Me.fraAvanç.Controls.Add(Me.Label11)
        Me.fraAvanç.Controls.Add(Me.Label10)
        Me.fraAvanç.Controls.Add(Me.chkRestric)
        Me.fraAvanç.Controls.Add(Me.chkIntrumental)
        Me.fraAvanç.Controls.Add(Me.txtAny)
        Me.fraAvanç.Controls.Add(Me.chkBPMFort)
        Me.fraAvanç.Controls.Add(Me.txtRitme)
        Me.fraAvanç.Controls.Add(Me.txtComent)
        Me.fraAvanç.Controls.Add(Me.txtCompositor)
        Me.fraAvanç.Location = New System.Drawing.Point(17, 331)
        Me.fraAvanç.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.fraAvanç.Name = "fraAvanç"
        Me.fraAvanç.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.fraAvanç.Size = New System.Drawing.Size(407, 182)
        Me.fraAvanç.TabIndex = 18
        Me.fraAvanç.TabStop = False
        Me.fraAvanç.Text = "Recerca avançada"
        Me.fraAvanç.Visible = False
        '
        'grupOrderBy
        '
        Me.grupOrderBy.Controls.Add(Me.chkNumRadia)
        Me.grupOrderBy.Controls.Add(Me.chkDateInDESC)
        Me.grupOrderBy.Controls.Add(Me.chkDateInASC)
        Me.grupOrderBy.Location = New System.Drawing.Point(199, 100)
        Me.grupOrderBy.Name = "grupOrderBy"
        Me.grupOrderBy.Size = New System.Drawing.Size(198, 78)
        Me.grupOrderBy.TabIndex = 12
        Me.grupOrderBy.TabStop = False
        Me.grupOrderBy.Text = "Ordenar per"
        '
        'chkNumRadia
        '
        Me.chkNumRadia.AutoSize = True
        Me.chkNumRadia.Location = New System.Drawing.Point(21, 53)
        Me.chkNumRadia.Name = "chkNumRadia"
        Me.chkNumRadia.Size = New System.Drawing.Size(140, 15)
        Me.chkNumRadia.TabIndex = 2
        Me.chkNumRadia.Text = "Número de radiacions"
        Me.chkNumRadia.UseSelectable = True
        '
        'chkDateInDESC
        '
        Me.chkDateInDESC.AutoSize = True
        Me.chkDateInDESC.Location = New System.Drawing.Point(21, 34)
        Me.chkDateInDESC.Name = "chkDateInDESC"
        Me.chkDateInDESC.Size = New System.Drawing.Size(154, 15)
        Me.chkDateInDESC.TabIndex = 1
        Me.chkDateInDESC.Text = "Data entrada descendent"
        Me.chkDateInDESC.UseSelectable = True
        '
        'chkDateInASC
        '
        Me.chkDateInASC.AutoSize = True
        Me.chkDateInASC.Checked = True
        Me.chkDateInASC.Location = New System.Drawing.Point(21, 15)
        Me.chkDateInASC.Name = "chkDateInASC"
        Me.chkDateInASC.Size = New System.Drawing.Size(147, 15)
        Me.chkDateInASC.TabIndex = 0
        Me.chkDateInASC.TabStop = True
        Me.chkDateInASC.Text = "Data entrada ascendent"
        Me.chkDateInASC.UseSelectable = True
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(14, 113)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(86, 17)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "Any publicació"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(14, 85)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(86, 17)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "Ritme (BPM)"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(14, 57)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(86, 17)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "Comentari"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(14, 29)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(86, 17)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "Compositor"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'chkRestric
        '
        Me.chkRestric.AutoSize = True
        Me.chkRestric.Location = New System.Drawing.Point(107, 158)
        Me.chkRestric.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkRestric.Name = "chkRestric"
        Me.chkRestric.Size = New System.Drawing.Size(74, 15)
        Me.chkRestric.TabIndex = 6
        Me.chkRestric.Text = "Restricció"
        Me.chkRestric.UseSelectable = True
        '
        'chkIntrumental
        '
        Me.chkIntrumental.AutoSize = True
        Me.chkIntrumental.Location = New System.Drawing.Point(107, 137)
        Me.chkIntrumental.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkIntrumental.Name = "chkIntrumental"
        Me.chkIntrumental.Size = New System.Drawing.Size(90, 15)
        Me.chkIntrumental.TabIndex = 5
        Me.chkIntrumental.Text = "Instrumental"
        Me.chkIntrumental.UseSelectable = True
        '
        'txtAny
        '
        Me.txtAny.Location = New System.Drawing.Point(107, 109)
        Me.txtAny.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtAny.Name = "txtAny"
        Me.txtAny.Size = New System.Drawing.Size(84, 25)
        Me.txtAny.TabIndex = 4
        '
        'chkBPMFort
        '
        Me.chkBPMFort.AutoSize = True
        Me.chkBPMFort.Location = New System.Drawing.Point(197, 85)
        Me.chkBPMFort.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkBPMFort.Name = "chkBPMFort"
        Me.chkBPMFort.Size = New System.Drawing.Size(88, 15)
        Me.chkBPMFort.TabIndex = 3
        Me.chkBPMFort.Text = "Filtratge fort"
        Me.chkBPMFort.UseSelectable = True
        '
        'txtRitme
        '
        Me.txtRitme.Location = New System.Drawing.Point(107, 81)
        Me.txtRitme.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtRitme.Name = "txtRitme"
        Me.txtRitme.Size = New System.Drawing.Size(84, 25)
        Me.txtRitme.TabIndex = 2
        '
        'txtComent
        '
        Me.txtComent.Location = New System.Drawing.Point(107, 53)
        Me.txtComent.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtComent.Name = "txtComent"
        Me.txtComent.Size = New System.Drawing.Size(285, 25)
        Me.txtComent.TabIndex = 1
        '
        'txtCompositor
        '
        Me.txtCompositor.Location = New System.Drawing.Point(107, 25)
        Me.txtCompositor.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtCompositor.Name = "txtCompositor"
        Me.txtCompositor.Size = New System.Drawing.Size(285, 25)
        Me.txtCompositor.TabIndex = 0
        '
        'chkAddResult
        '
        Me.chkAddResult.AutoSize = True
        Me.chkAddResult.Location = New System.Drawing.Point(124, 521)
        Me.chkAddResult.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.chkAddResult.Name = "chkAddResult"
        Me.chkAddResult.Size = New System.Drawing.Size(161, 15)
        Me.chkAddResult.TabIndex = 19
        Me.chkAddResult.Text = "Afegir a la recerca anterior"
        Me.chkAddResult.UseSelectable = True
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.cmdMoreFind)
        Me.Panel1.Controls.Add(Me.cmdBuscar)
        Me.Panel1.Location = New System.Drawing.Point(11, 331)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(419, 81)
        Me.Panel1.TabIndex = 20
        '
        'cmdMoreFind
        '
        Me.cmdMoreFind.ActiveControl = Nothing
        Me.cmdMoreFind.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdMoreFind.Location = New System.Drawing.Point(299, 7)
        Me.cmdMoreFind.Name = "cmdMoreFind"
        Me.cmdMoreFind.Size = New System.Drawing.Size(98, 55)
        Me.cmdMoreFind.TabIndex = 35
        Me.cmdMoreFind.Text = "Avançats>>"
        Me.cmdMoreFind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdMoreFind.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdMoreFind.UseSelectable = True
        Me.cmdMoreFind.UseTileImage = True
        '
        'cmdBuscar
        '
        Me.cmdBuscar.ActiveControl = Nothing
        Me.cmdBuscar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdBuscar.Location = New System.Drawing.Point(112, 7)
        Me.cmdBuscar.Name = "cmdBuscar"
        Me.cmdBuscar.Size = New System.Drawing.Size(180, 55)
        Me.cmdBuscar.TabIndex = 34
        Me.cmdBuscar.TileImage = CType(resources.GetObject("cmdBuscar.TileImage"), System.Drawing.Image)
        Me.cmdBuscar.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdBuscar.UseSelectable = True
        Me.cmdBuscar.UseTileImage = True
        '
        'CmdTancar
        '
        Me.CmdTancar.ActiveControl = Nothing
        Me.CmdTancar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CmdTancar.Location = New System.Drawing.Point(392, 43)
        Me.CmdTancar.Name = "CmdTancar"
        Me.CmdTancar.Size = New System.Drawing.Size(12, 15)
        Me.CmdTancar.TabIndex = 33
        Me.CmdTancar.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdTancar.UseSelectable = True
        Me.CmdTancar.UseTileImage = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(12, 305)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(103, 19)
        Me.Label9.TabIndex = 17
        Me.Label9.Text = "% signe comodí"
        Me.Label9.Visible = False
        '
        'cmdSortir
        '
        Me.cmdSortir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSortir.BackColor = System.Drawing.Color.Transparent
        Me.cmdSortir.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdSortir.Image = CType(resources.GetObject("cmdSortir.Image"), System.Drawing.Image)
        Me.cmdSortir.ImageSize = 32
        Me.cmdSortir.Location = New System.Drawing.Point(366, 32)
        Me.cmdSortir.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.cmdSortir.Name = "cmdSortir"
        Me.cmdSortir.NoFocusImage = CType(resources.GetObject("cmdSortir.NoFocusImage"), System.Drawing.Image)
        Me.cmdSortir.Size = New System.Drawing.Size(41, 46)
        Me.cmdSortir.TabIndex = 245
        Me.cmdSortir.UseSelectable = True
        '
        'frmBuscarTemes
        '
        Me.AcceptButton = Me.cmdBuscar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.cmdSortir
        Me.ClientSize = New System.Drawing.Size(450, 411)
        Me.Controls.Add(Me.cmdSortir)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.chkAddResult)
        Me.Controls.Add(Me.CmdTancar)
        Me.Controls.Add(Me.fraAvanç)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.fraPauta)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cbSubject)
        Me.Controls.Add(Me.cbTemp)
        Me.Controls.Add(Me.cbIdioma)
        Me.Controls.Add(Me.cbEstil)
        Me.Controls.Add(Me.chkTots)
        Me.Controls.Add(Me.cbDisc)
        Me.Controls.Add(Me.cbInterp)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNomTema)
        Me.Font = New System.Drawing.Font("Segoe UI Symbol", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 411)
        Me.Name = "frmBuscarTemes"
        Me.Padding = New System.Windows.Forms.Padding(23, 78, 23, 26)
        Me.Resizable = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Buscar cançons ..."
        Me.fraPauta.ResumeLayout(False)
        Me.fraPauta.PerformLayout()
        Me.fraAvanç.ResumeLayout(False)
        Me.fraAvanç.PerformLayout()
        Me.grupOrderBy.ResumeLayout(False)
        Me.grupOrderBy.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNomTema As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label2 As MetroFramework.Controls.MetroLabel
    Friend WithEvents cbInterp As System.Windows.Forms.ComboBox
    Friend WithEvents cbDisc As System.Windows.Forms.ComboBox
    Friend WithEvents cbEstil As System.Windows.Forms.ComboBox
    Friend WithEvents cbIdioma As System.Windows.Forms.ComboBox
    Friend WithEvents cbTemp As System.Windows.Forms.ComboBox
    Friend WithEvents cbSubject As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label4 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label5 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label6 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label7 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label8 As MetroFramework.Controls.MetroLabel
    Friend WithEvents fraPauta As System.Windows.Forms.GroupBox
    Friend WithEvents fraAvanç As System.Windows.Forms.GroupBox
    Friend WithEvents txtCompositor As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label10 As MetroFramework.Controls.MetroLabel
    Friend WithEvents txtAny As System.Windows.Forms.TextBox
    Friend WithEvents txtRitme As System.Windows.Forms.TextBox
    Friend WithEvents txtComent As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Label12 As MetroFramework.Controls.MetroLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents Label9 As MetroFramework.Controls.MetroLabel
    Friend WithEvents cmdMoreFind As MetroFramework.Controls.MetroTile
    Friend WithEvents cmdBuscar As MetroFramework.Controls.MetroTile
    Friend WithEvents CmdTancar As MetroFramework.Controls.MetroTile
    Friend WithEvents chkTots As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents chkRestric As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents chkIntrumental As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents chkBPMFort As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents chkAddResult As MetroFramework.Controls.MetroCheckBox
    Friend WithEvents cmdSortir As MetroFramework.Controls.MetroLink
    Friend WithEvents grupOrderBy As GroupBox
    Friend WithEvents chkNoFiltre As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents chkOutP As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents chkInP As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents chkNumRadia As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents chkDateInDESC As MetroFramework.Controls.MetroRadioButton
    Friend WithEvents chkDateInASC As MetroFramework.Controls.MetroRadioButton
End Class
