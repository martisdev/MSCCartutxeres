Option Strict Off
Option Explicit On
Imports Un4seen.Bass

Public Class frmAudioDBS
	Dim InfoMsg As frmTip

    Dim gridTipusFitxer As DataGridView
    'llenguatge
    Dim MSG_ATENCIO As String = ""
	Dim HEADERTEXT_ID As String = ""
	Dim HEADERTEXT_NAME As String = ""
	Dim HEADERTEXT_TEMATICA As String = ""
	Dim HEADERTEXT_DATE_CREA As String = ""
	Dim MSG_NO_ACCES_FILE As String = ""
	Dim HEADERTEXT_TITOL As String = "" 
	Dim NAME_PROGRAMES As String = ""
	Dim HEADERTEXT_DURADA As String = ""
	Dim HEADERTEXT_SESSIO_MARE As String = ""
	Dim MSG_BUSCATEMA_NO_VALID As String =  ""
	Dim MSG_MORE_INFO As String = ""
	Dim MSG_NO_ADD_TO_PLAY As String = ""
	Dim MNU_DEL_FAV As String = ""
	Dim MNU_ADD_FAV As String = ""
	
	Private Sub mnuPestanyaSobre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPestanyaSobre.Click, mnuPestanyaSota.Click
        mnuPestanyaSota.Checked = False
        mnuPestanyaSobre.Checked = False
        CType(sender, ToolStripMenuItem).Checked = True

        If mnuPestanyaSobre.Checked = True Then
            tabAudioDBS.Alignment = TabAlignment.Top
        Else
            tabAudioDBS.Alignment = TabAlignment.Bottom
		End If
		My.Settings.PosCarp = IIf(mnuPestanyaSobre.Checked = True, 1, 0)
	End Sub
	
	Private Sub frmAudioDBS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
		Me.Hide()
        setLanguageForm()

        Dim frm As Form
		Dim NumCart As Short = 1
		For Each frm In My.Application.OpenForms
            If frm.Name = "frmCartutxera" Then
                Dim mnuNewCart As ToolStripMenuItem
                mnuNewCart = New ToolStripMenuItem
                mnuNewCart.Text = "Cartutxera " & NumCart
                Dim KeyShortCut As System.Windows.Forms.Keys = CType(Choose(NumCart, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9), System.Windows.Forms.Keys)
                mnuNewCart.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or KeyShortCut), System.Windows.Forms.Keys)
                mnuNewCart.Tag = NumCart
                mnuNewCart.Visible = True
                mnuAddRepreoduccio.DropDownItems.Add(mnuNewCart)
                AddHandler mnuNewCart.Click, AddressOf AfegirAReproducció_Click
                NumCart = NumCart + 1
            End If
        Next
		
		txtData.Value = Now



        mnuLimitRegChec.Checked = My.Settings.Enab_limit
		mnuLimitRecord.Enabled = mnuLimitRegChec.Checked
		mnuLimitRecord.Text = My.Settings.MaxRS
		
		If CBool(My.Settings.PosCarp) = True Then
			Me.mnuPestanyaSobre.Checked = True
			Me.mnuPestanyaSota.Checked = False
			tabAudioDBS.Alignment = TabAlignment.Top
		Else
			Me.mnuPestanyaSota.Checked = True
			Me.mnuPestanyaSobre.Checked = False
			tabAudioDBS.Alignment = TabAlignment.Bottom
		End If

        If Params.ActvTabProgram = True Then getListProgrames(createTabAndGridPrograma) 'Crear el tab i el grid del seu interior

        Dim FitxerINI As New IniFile
		ProgEditAudio = FitxerINI.INIRead(MyAPP.IniFile, "Data Control", "ProgEditAudio", "")
		If ProgEditAudio.Length > 0 Then mnuEditAudio.Enabled = True		
		
		Me.Left = FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "DBS_L",30)
		Me.Top = FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "DBS_T", 580)
		Me.Width = FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "DBS_W",600)
		Me.Height = FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "DBS_H", 300)
        mnuShowFolders.Checked = CBool(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "SW_CARP", 1))
        SetUserEnable()

        setThemeControls()
        Me.Show()
	End Sub

    Public Sub setLanguageForm()
        lang.StrForm = Me.Name

        'Missatges
        'MSG_ATENCIO =  lang.getText("MSG_ATENCIO",True)' "Atenció"
        MSG_MORE_INFO = lang.getText("MSG_MORE_INFO", True) ' "Més info .."
        MSG_NO_ACCES_FILE = lang.getText("MSG_NO_ACCES_FILE", True) ' "No hi ha accés als fitxers d'àudio."    
        NAME_PROGRAMES = lang.getText("NAME_PROGRAMES", True) '"Programes"
        HEADERTEXT_NAME = lang.getText("HEADERTEXT_NAME", True) '"Nom"
        HEADERTEXT_ID = lang.getText("HEADERTEXT_ID", True) '"ID"
        HEADERTEXT_TEMATICA = lang.getText("HEADERTEXT_TEMATICA", True) '"Temàtica"
        HEADERTEXT_DATE_CREA = lang.getText("HEADERTEXT_DATE_CREA", True) '"Data Creació"
        HEADERTEXT_DURADA = lang.getText("HEADERTEXT_DURADA", True) '"Durada"

        HEADERTEXT_SESSIO_MARE = lang.getText("HEADERTEXT_SESSIO_MARE", True) '"Sessió Mare"
        HEADERTEXT_TITOL = lang.getText("HEADERTEXT_TITOL", True) '"Titol"
        Dim HEADERTEXT_INTERP As String = lang.getText("HEADERTEXT_INTERP", True) '"Intèrpret"
        Dim HEADERTEXT_DISC As String = lang.getText("HEADERTEXT_DISC", True) '"Disc"
        Dim HEADERTEXT_ESTIL As String = lang.getText("HEADERTEXT_ESTIL", True) '"Estil"
        Dim HEADERTEXT_IDIOMA As String = lang.getText("HEADERTEXT_IDIOMA", True) '"Idioma"
        Dim HEADERTEXT_CLAS_TEMP As String = lang.getText("HEADERTEXT_CLAS_TEMP", True) '"Clas. Temp"
        Dim HEADERTEXT_RITME As String = lang.getText("HEADERTEXT_RITME", True) '"Ritme"
        Dim HEADERTEXT_ANY As String = lang.getText("HEADERTEXT_ANY", True) '"Any"
        Dim HEADERTEXT_N_RADIA As String = lang.getText("HEADERTEXT_N_RADIA", True) '"Nº radiacions"
        Dim HEADERTEXT_FAVORITES As String = lang.getText("HEADERTEXT_FAVORITES", True) '"Favorites"
        Dim HEADERTEXT_SUBJECT As String = lang.getText("HEADERTEXT_SUBJECT", True) '"Subject."
        Dim HEADERTEXT_PUBLICITAT As String = lang.getText("HEADERTEXT_PUBLICITAT", True) '"Publicitat"
        Dim HEADERTEXT_BLOC As String = lang.getText("HEADERTEXT_BLOC", True) '"Bloc"
        Dim HEADERTEXT_CLIENT As String = lang.getText("HEADERTEXT_CLIENT", True) '"Client"
        Dim HEADERTEXT_LOCUTOR As String = lang.getText("HEADERTEXT_LOCUTOR", True) '"Locutor"
        Dim HEADERTEXT_USUARI As String = lang.getText("HEADERTEXT_USUARI", True) '"Usuaris"
        Me.mnuMesInfo.Text = lang.getText("MSG_MORE_INFO", True) '"Més Info ..."
        Me.mnuEditAudio.Text = lang.getText("mnuEditAudio.Text", True) '"Editar l'àudio"
        'Dim HEADERTEXT_INI_SESSIO As String = lang.getText("HEADERTEXT_INI_SESSIO",True)'"Inicia sessió"
        'Dim HEADERTEXT_END_SESSIO As String = lang.getText("HEADERTEXT_END_SESSIO",True)'"Finalitza sessió"

        MNU_DEL_FAV = lang.getText("mnuDellFav_text", True) '"Borrar de Favorites"
        MNU_ADD_FAV = lang.getText("LABEL_ADD_FAVORITES", True) '"Afegir a Favorites"
        MSG_BUSCATEMA_NO_VALID = lang.getText("MSG_BUSCATEMA_NO_VALID") ' "'{0}', No és un valor vàlid"
        MSG_NO_ADD_TO_PLAY = lang.getText("MSG_NO_ADD_TO_PLAY") ' "El fitxer '{0}' no existeix, no s'afegirà"
        'Pantalla
        cmbBuscar.ToolTipText = lang.getText("LABEL_BUSCAR", True) ' Buscar
        cmdSortir.ToolTipText = lang.getText("LABEL_EXIT", True) ' sortir
        Me.TabPage1.Text = lang.getText("FITXER_MUSICA", True) ' "Música"
        'Me.ToolStrip2.Text = "ToolStrip2"
        'Me.ToolStrip2.Text = "ToolStrip2"
        'Me.ToolStripSplitButton1.Text = "ToolStripSplitButton1"
        Me.findPerTitol.Text = lang.getText("findPerTitol.Text")
        Me.findPerInterp.Text = lang.getText("findPerInterp.Text") '"Per Intèrpret"
        Me.findPerDisc.Text = lang.getText("findPerDisc.Text") '"Per Disc"
        Me.RecercaAvançadaToolStripMenuItem.Text = lang.getText("LABEL_RECERCA_ABANCADA", True) '"Recerca Avançada"
        Me.ToolStripDropDownButton1.Text = lang.getText("ToolStripDropDownButton1.Text") '"Columnes opcionals"
        Me.mnuColumnVisibleRadia.Text = HEADERTEXT_N_RADIA '"Nº Radiacions"
        Me.mnuColumnVisibleAny.Text = HEADERTEXT_ANY  '"Any"
        Me.mnuColumnVisibleRitme.Text = HEADERTEXT_RITME '"Ritme"
        Me.mnuColumnVisibleSubj.Text = HEADERTEXT_SUBJECT '"Clas. Subjectiva"
        Me.mnuColumnVisibleTemp.Text = HEADERTEXT_CLAS_TEMP '"Clas. Temporal"
        Me.mnuColumnVisibleIdioma.Text = HEADERTEXT_IDIOMA '"Idioma"
        Me.mnuColumnVisibleEstil.Text = HEADERTEXT_ESTIL '"Estil"
        Me.ToolStripLabel1.Text = lang.getText("ToolStripLabel1.Text") '"Presentació"    	
        Me.LlistatToolStripMenuItem.Text = lang.getText("LlistatToolStripMenuItem.Text") '"Llistat Música (Max)"
        Me.mnuLimitRegChec.Text = lang.getText("mnuLimitRegChec.Text") '"Amb limitador de registres"
        'Me.mnuLimitRecord.Text = "100"
        Me.ColumnID.HeaderText = HEADERTEXT_ID
        Me.ColumnTitol.HeaderText = HEADERTEXT_TITOL
        Me.comboInterp.HeaderText = HEADERTEXT_INTERP
        Me.comboDisc.HeaderText = HEADERTEXT_DISC
        Me.Column3.HeaderText = HEADERTEXT_DURADA
        'Me.ColumnEstil.HeaderText = HEADERTEXT_ESTIL
        Me.ColumnIdioma.HeaderText = HEADERTEXT_IDIOMA
        Me.ColumnTemp.HeaderText = HEADERTEXT_CLAS_TEMP
        Me.ColumnSub.HeaderText = HEADERTEXT_SUBJECT
        Me.ColumnRitme.HeaderText = HEADERTEXT_RITME
        Me.ColumnAny.HeaderText = HEADERTEXT_ANY
        Me.ColumnRadia.HeaderText = HEADERTEXT_N_RADIA
        Me.ColumnFav.HeaderText = HEADERTEXT_FAVORITES

        Me.mnuPreEscolta.Text = lang.getText("mnuPreEscolta.Text") '"Pre-escolta PLAY"
        Me.mnuPreescoltaStop.Text = LABEL_PREESCOLTA_STOP '"Pre-escolta STOP"
        Me.mnuAddRepreoduccio.Text = lang.getText("mnuAddRepreoduccio.Text") '"Afegir a reproducció"

        Me.mnuColumns.Text = lang.getText("mnuColumns.Text") '"Ordenar columnes"

        Me.mnuAddDelFav.Text = MNU_ADD_FAV

        Me.TabPage4.Text = HEADERTEXT_PUBLICITAT
        Me.ToolStripLabel2.Text = HEADERTEXT_BLOC & ":"
        Me.Column8.HeaderText = HEADERTEXT_ID
        Me.Column9.HeaderText = HEADERTEXT_NAME
        Me.comboClient.HeaderText = HEADERTEXT_CLIENT
        Me.comboLocutor.HeaderText = HEADERTEXT_LOCUTOR
        Me.Column13.HeaderText = HEADERTEXT_DURADA

        Me.mnuPosicioPestanyes.Text = lang.getText("mnuPosicioPestanyes.Text") '"Posició Carpetes"
        mnuShowFolders.Text = lang.getText("mnuShowFolders.Text") '"Mostra carpetes"
        Me.mnuPestanyaSobre.Text = lang.getText("mnuPestanyaSobre.Text") '"Sobre"
        Me.mnuPestanyaSota.Text = lang.getText("mnuPestanyaSota.Text") '"Sota"
        Me.ToolStripDropDownButton2.Text = HEADERTEXT_USUARI
        Me.mnuIniSessio.Text = lang.getText("HEADERTEXT_INI_SESSIO", True) '"Inicia sessió"
        Me.mnuEndSessio.Text = lang.getText("HEADERTEXT_END_SESSIO", True) '"Finalitza sessió"
        Me.mnuMareCarpetesUser.Text = lang.getText("mnuMareCarpetesUser.Text") '"Visibilitat Carpetes"
        Me.lbAtencioPubli.Text = lang.getText("lbAtencioPubli.Text") '"ATENCIÓ Publicitat pendent"
        Me.lbAtencioPubli.ToolTipText = lang.getText("lbAtencioPubli.Text") '"Fer doble click per ometre avís"
        Me.Text = lang.getText("Text") '"Àudio a la DBS"

        Dim Cmb As New combo
        Cmb.OmpleCombo(Me.comboClient, TAULES_DBS.TAULA_CLIENTS, TotsCap.CAP)
        Cmb.OmpleCombo(Me.comboLocutor, TAULES_DBS.TAULA_LOCUTORS, TotsCap.CAP)
        Cmb.OmpleCombo(cmbHora, TAULES_DBS.TAULA_BlocsPublicitat, TotsCap.CAP) : cmbHora.SelectedValue = 0
        Cmb.OmpleCombo(Me.comboInterp, TAULES_DBS.TAULA_INTERPRETS, TotsCap.CAP)
        Cmb.OmpleCombo(Me.comboDisc, TAULES_DBS.TAULA_DISCOS, TotsCap.CAP)
        Cmb.OmpleCombo(Me.ColumnIdioma, TAULES_DBS.TAULA_IDIOMES, TotsCap.CAP)
        Cmb.OmpleCombo(Me.ColumnSub, TAULES_DBS.TAULA_SUBJECTIV, TotsCap.CAP)
        Cmb.OmpleCombo(Me.ColumnTemp, TAULES_DBS.TAULA_TEMPORALITAT, TotsCap.CAP)

    End Sub


    Private Function getListPublicitat() As Short
		If cmbHora.SelectedIndex = 96 Then Exit Function '"<CAP>"        
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
		
		Dim Bloc As String = cmbHora.SelectedValue.ToString
		
		Dim DataRadi As Date = Me.txtData.Value
		Dim DiaSem As Short = Weekday(DataRadi, FirstDayOfWeek.Monday)
        Dim db As New MSC.dbs(Cloud)
        Dim Origen As String = "SELECT falques.falc_id, falques.falc_nom, falques.falc_client, falques.falc_locutor, falques.falc_durada"
		Origen = Origen & " FROM radiaciofalques" & DiaSem & " INNER JOIN  falques ON radiaciofalques" & DiaSem & ".radiID = falques.falc_id"
		Origen = Origen & " WHERE (((radiaciofalques" & DiaSem & "." & Bloc & ")=1) AND (falques.falc_activa=1) AND ((falques.falc_datain)<= '" & DataRadi.ToString("yyyy/MM/dd") & "') AND ((falques.falc_dataout)>= '" & DataRadi.ToString("yyyy/MM/dd") & "')) ORDER BY radiaciofalques" & DiaSem & ".radiOrdre ;"
		
		gridPubli.DataSource = db.getTable(Origen)
		db = Nothing
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		Return gridPubli.Rows.Count
		
	End Function
	
	Friend Function getListTemes(Optional ByRef StrSql As String = "") As Short
		Static MySqlStr As String
		Static CountPage As Integer
		Dim strLimit As String
		Static TotalPage As Integer
		Static page As Short
        Dim db As New MSC.dbs(Cloud)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
		Dim Origen As String = "SELECT tema_id, tema_titol, tema_interp, tema_disc, tema_durada, tema_idioma, tema_temps, tema_subj, tema_ritme,tema_any,tema_numradiacions, " & _
			"IFNULL(usrtema_tema = tema_id,0) as temFav " & _
			"FROM temes " & _
			"Left Join " & _
			"(SELECT usrtema_tema FROM user_temes WHERE usrtema_user=" & Usuari.UsrID & ")tb " & _
			"on tema_id=usrtema_tema "

        If mnuLimitRecord.Enabled = True Then
            If MySqlStr = StrSql And Not MySqlStr = Nothing Then
                CountPage = CountPage + mnuLimitRecord.Text
                page = page + 1
            Else
                MySqlStr = StrSql
                CountPage = 0
                page = 1
                Dim StrWhere As String
                If Len(StrSql) = 0 Then
                    StrWhere = "tema_pautain = 1"
                Else
                    StrWhere = StrSql
                End If
                Dim rsCout As DataTable = db.getTable("SELECT COUNT(*) FROM temes WHERE " & StrWhere)
                TotalPage = CInt(IIf(CInt(rsCout.Rows(0)(0)) <= mnuLimitRecord.Text, 1, CInt(rsCout.Rows(0)(0)) / mnuLimitRecord.Text))
            End If
            strLimit = "limit " & CountPage & ", " & mnuLimitRecord.Text
            If Len(StrSql) > 0 Then
                Origen = Origen & " WHERE " & StrSql & " " & strLimit & " ;"
            Else
                Origen = Origen & " WHERE tema_pautain = 1 " & strLimit & " ;"
            End If
        Else
            If Len(StrSql) > 0 Then
                Origen = Origen & " WHERE " & StrSql & " ;"
            Else
                Origen = Origen & " WHERE tema_pautain = 1 ;"
            End If
        End If
        gridTemes.DataSource = db.getTable(Origen)
        gridTemes.Columns("ColumnFav").Visible = IIf(Usuari.UsrID > 0, True, False)
        If mnuLimitRecord.Enabled = True Then
            Me.gridTemes.Columns("ColumnTitol").HeaderText = HEADERTEXT_TITOL & " (" & page & " / " & TotalPage & ")"
            If gridTemes.Rows.Count = 0 Then
                MySqlStr = ""
                CountPage = 0
            End If
        Else
            Me.gridTemes.Columns("ColumnTitol").HeaderText = HEADERTEXT_TITOL & " (" & gridTemes.Rows.Count & " )"
        End If
        db = Nothing
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
        Return gridTemes.Rows.Count

    End Function

    Private Function getListProgrames(ByVal grid As DataGridView, Optional ByRef StrSql As String = "") As Short
        'crea el recordset dels programes
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
        Dim db As New MSC.dbs(Cloud)

        Dim Origen As String = "SELECT ses_id, prg_nom, prg_tematica, ses_datacreacio, ses_durada, ses_sessiomare FROM programes, prg_sessions WHERE ses_prg= prg_id"
        If StrSql.Length > 0 Then Origen = Origen & " AND " & StrSql & " ;"
        Try
            grid.DataSource = db.getTable(Origen)

            Dim SesioMare As Integer = 0
            Dim Part As Integer = 1
            Dim BackColorGrid As System.Drawing.Color = Color.White
            For i As Integer = 0 To grid.RowCount - 1
                If grid.Rows(i).Cells("DataGridViewTextBoxColumnSesMare").Value <> SesioMare Or SesioMare = 0 Then
                    Part = 1
                    SesioMare = grid.Rows(i).Cells("DataGridViewTextBoxColumnID").Value
                    If BackColorGrid = Color.Gray Then
                        BackColorGrid = Color.White
                    Else
                        BackColorGrid = Color.Gray
                    End If
                End If
                grid.Rows(i).Cells("DataGridViewTextBoxColumnNOM").Value += " (Part " & Part & ")"
                grid.Rows(i).DefaultCellStyle.BackColor = BackColorGrid
                Part += 1
            Next
            '???!!!
            grid.Columns(0).Visible = False
            db = Nothing
            System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
            Return grid.Rows.Count
        Catch ex As Exception
            Return 0
        End Try


    End Function

    Private Function createTabAndGridPrograma() As DataGridView

        'Crear el tab
        Dim NewTabPagePrograms As New MetroFramework.Controls.MetroTabPage()
        NewTabPagePrograms.Name = 4
        NewTabPagePrograms.Text = NAME_PROGRAMES
        NewTabPagePrograms.Tag = "NO_DELETE"
        NewTabPagePrograms.Visible = True
        NewTabPagePrograms.ImageIndex = 4
        NewTabPagePrograms.Theme = Me.Theme

        NewTabPagePrograms.HorizontalScrollbarBarColor = True
        NewTabPagePrograms.HorizontalScrollbarHighlightOnWheel = False
        NewTabPagePrograms.HorizontalScrollbarSize = 10
        NewTabPagePrograms.Location = New System.Drawing.Point(4, 38)
        NewTabPagePrograms.Padding = New System.Windows.Forms.Padding(3)
        NewTabPagePrograms.Size = New System.Drawing.Size(368, 324)
        NewTabPagePrograms.UseVisualStyleBackColor = True
        NewTabPagePrograms.VerticalScrollbarBarColor = True
        NewTabPagePrograms.VerticalScrollbarHighlightOnWheel = False
        NewTabPagePrograms.VerticalScrollbarSize = 10

        Me.tabAudioDBS.TabPages.Add(NewTabPagePrograms)

        'crear el grid
        Dim gridProgrames As New MetroFramework.Controls.MetroGrid()

        gridProgrames.Parent = NewTabPagePrograms ' tabAudioDBS.TabPages(4)
        'gridProgrames
        '
        gridProgrames.AllowUserToAddRows = False
        gridProgrames.AllowUserToDeleteRows = False
        gridProgrames.AllowUserToResizeRows = False

        'gridProgrames.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
        '            Or System.Windows.Forms.AnchorStyles.Left) _
        '            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        gridProgrames.Dock = DockStyle.Fill
        gridProgrames.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        gridProgrames.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        gridProgrames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        gridProgrames.ContextMenuStrip = Me.ContextMenuStripBotoDret
        'gridProgrames.Cursor = System.Windows.Forms.Cursors.Hand
        'gridProgrames.Location = New System.Drawing.Point(3, 3)
        gridProgrames.MultiSelect = True
        gridProgrames.Name = "gridProgrames"
        gridProgrames.ReadOnly = True
        gridProgrames.RowHeadersVisible = False
        gridProgrames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        'gridProgrames.Size = New System.Drawing.Size(383, 209)
        gridProgrames.TabIndex = 5
        gridProgrames.Tag = mdlMscDefines.Tipus_Play.CTL_PROGRAMA
        gridProgrames.Theme = Me.Theme
        gridProgrames.EnableHeadersVisualStyles = False
        gridProgrames.BorderStyle = Windows.Forms.BorderStyle.None
        ''crear les columnnes...
        'DataGridViewTextBoxColumn1
        '
        Dim DataGridViewTextBoxColumn1 As New System.Windows.Forms.DataGridViewTextBoxColumn
        DataGridViewTextBoxColumn1.DataPropertyName = "ses_id"
        DataGridViewTextBoxColumn1.HeaderText = HEADERTEXT_ID
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumnID"
        DataGridViewTextBoxColumn1.ReadOnly = True
        DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn2.DataPropertyName = "prg_nom"
        DataGridViewTextBoxColumn2.HeaderText = HEADERTEXT_NAME
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumnNOM"
        DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'comboTematica
        '
        Dim comboTematica As New System.Windows.Forms.DataGridViewComboBoxColumn
        comboTematica.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        comboTematica.DataPropertyName = "prg_tematica"
        comboTematica.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.[Nothing]
        comboTematica.HeaderText = HEADERTEXT_TEMATICA
        comboTematica.Name = "comboTematica"
        comboTematica.ReadOnly = True
        comboTematica.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        comboTematica.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn3 
        '
        Dim DataGridViewTextBoxColumn3 As New System.Windows.Forms.DataGridViewTextBoxColumn
        'DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn3.Width = 150
        DataGridViewTextBoxColumn3.DataPropertyName = "ses_datacreacio"
        DataGridViewTextBoxColumn3.HeaderText = HEADERTEXT_DATE_CREA
        DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumnDataIN"
        DataGridViewTextBoxColumn3.ReadOnly = True
        DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'DataGridViewTextBoxColumn3
        '
        Dim DataGridViewTextBoxColumn4 As New System.Windows.Forms.DataGridViewTextBoxColumn
        DataGridViewTextBoxColumn4.DataPropertyName = "ses_durada"
        DataGridViewTextBoxColumn4.HeaderText = HEADERTEXT_DURADA
        DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumnDurada"
        DataGridViewTextBoxColumn4.ReadOnly = True
        DataGridViewTextBoxColumn4.Width = 50

        'DataGridViewTextBoxColumnSesMare
        '
        Dim DataGridViewTextBoxColumnSesMare As New System.Windows.Forms.DataGridViewTextBoxColumn
        DataGridViewTextBoxColumnSesMare.DataPropertyName = "ses_sessiomare"
        DataGridViewTextBoxColumnSesMare.HeaderText = HEADERTEXT_SESSIO_MARE
        DataGridViewTextBoxColumnSesMare.Name = "DataGridViewTextBoxColumnSesMare"
        DataGridViewTextBoxColumnSesMare.ReadOnly = True
        DataGridViewTextBoxColumnSesMare.Visible = False

        gridProgrames.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, comboTematica, DataGridViewTextBoxColumn3, DataGridViewTextBoxColumn4, DataGridViewTextBoxColumnSesMare})

        Dim Cmb As New combo
        Cmb.OmpleCombo(comboTematica, TAULES_DBS.TAULA_TEMATIQUES, TotsCap.CAP)

        'Sha d'afegir l'event corresponent
        AddHandler gridProgrames.MouseDown, AddressOf MouseDownListDBS
        AddHandler gridProgrames.CellMouseDown, AddressOf CellMouseDownListDBS
        AddHandler gridProgrames.MouseMove, AddressOf MouseMoveListDBS
        AddHandler gridProgrames.CellMouseMove, AddressOf CellMouseMoveListDBS

        'AddHandler gridProgrames.ColumnHeaderMouseDoubleClick, AddressOf ColumnHeaderMouseDoubleClickListDBS

        Return gridProgrames
    End Function

    Private Sub LoadAudioUSR()
        mnuMareCarpetesUser.DropDownItems.Clear()

        For Each c As Control In tabAudioDBS.Controls
            If c.Tag.ToString <> "NO_DELETE" Then
                Me.Controls.Remove(c)
                c.Dispose()
            End If
        Next

        For Each c As Control In tabAudioDBS.Controls
            If c.Tag.ToString <> "NO_DELETE" Then
                Me.Controls.Remove(c)
                c.Dispose()
            End If
        Next

        Dim SHOW_CARP As Boolean = mnuShowFolders.Checked

        Dim grid As MetroFramework.Controls.MetroGrid
        Dim mnuCarpetaUser As ToolStripMenuItem
        Dim db As New MSC.dbs(Cloud)
        Dim NomCarp() As String = Split(Params.NomsCarpetaAudiosUser, ",")
        For u As Integer = 0 To NomCarp.Length - 1
            Dim mnuParentCarpetaUser As New ToolStripMenuItem
            Dim NewTabPageMareCarpetes As New MetroFramework.Controls.MetroTabPage
            Dim tpControl As New MetroFramework.Controls.MetroTabControl

            Dim SqlStr As String = "SELECT audio_id,audio_nom, (1) As audio_visible,(0) As audio_order FROM listaudio WHERE audio_visible_cart=1 And audio_carpeta=" & u & " ORDER BY audio_id"

            Dim DataTable As DataTable = db.getTable(SqlStr)

            If DataTable.Rows.Count > 0 Then
                If Usuari.UsrID > 0 Then
                    'mesclem la taula listaudio i user_audio
                    For i As Integer = 0 To DataTable.Rows.Count - 1
                        Dim id As Integer = CInt(DataTable.Rows(i)("audio_id"))
                        DataTable.Rows(i)("audio_visible") = IsAudioForUser(id, Usuari.UsrID)
                        DataTable.Rows(i)("audio_order") = getOrdreAudioUser(Usuari.UsrID, id)
                    Next
                    Dim dataView As New DataView(DataTable)
                    dataView.Sort = " audio_order ASC"
                    DataTable = dataView.ToTable()
                End If
                mnuParentCarpetaUser.Text = NomCarp(u)
                mnuMareCarpetesUser.DropDownItems.Add(mnuParentCarpetaUser)
                If SHOW_CARP = True Then
                    NewTabPageMareCarpetes.Text = NomCarp(u)
                    NewTabPageMareCarpetes.Tag = u
                    NewTabPageMareCarpetes.Theme = Me.Theme
                    NewTabPageMareCarpetes.Size = New System.Drawing.Size(tabAudioDBS.Width - 3, tabAudioDBS.Height - 3)
                    NewTabPageMareCarpetes.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                Or System.Windows.Forms.AnchorStyles.Left) _
                                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                    NewTabPageMareCarpetes.Theme = Me.Theme
                    Me.tabAudioDBS.TabPages.Add(NewTabPageMareCarpetes)

                    tpControl.Name = "tpControl" & u.ToString
                    tpControl.Size = New System.Drawing.Size(NewTabPageMareCarpetes.Width - 3, NewTabPageMareCarpetes.Height - 3)
                    tpControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                                Or System.Windows.Forms.AnchorStyles.Left) _
                                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
                    tpControl.Theme = Me.Theme
                    tpControl.Multiline = True
                    tpControl.AllowDrop = (Usuari.UsrID > 0)

                    AddHandler tpControl.DragDrop, AddressOf TabAudioDBSDragDrop
                    AddHandler tpControl.KeyDown, AddressOf frmAudioDBS_KeyDown
                    AddHandler tpControl.DragOver, AddressOf tabAudioDBS_DragOver
                    AddHandler tpControl.MouseMove, AddressOf tabAudioDBS_MouseMove
                    AddHandler tpControl.MouseDown, AddressOf tabAudioDBS_MouseDown

                    If mnuPestanyaSobre.Checked = True Then
                        tpControl.Alignment = TabAlignment.Top
                    Else
                        tpControl.Alignment = TabAlignment.Bottom
                    End If
                    NewTabPageMareCarpetes.Controls.Add(tpControl)
                End If
            End If
            For i As Integer = 0 To dataTable.Rows.Count - 1

                'Dim ord As Integer = dataTable.Rows(i)("ordre")
                Dim AudioNom As String = dataTable.Rows(i)("audio_nom").ToString
                Dim AudioID As Integer = DataTable.Rows(i)("audio_id")
                Dim Visible As Boolean = IsAudioForUser(AudioID, Usuari.UsrID)
                mnuCarpetaUser = New ToolStripMenuItem
                mnuCarpetaUser.Text = AudioNom
                mnuCarpetaUser.Tag = AudioID
                mnuCarpetaUser.Name = "mnuCarpUserAudio" & AudioID
                mnuCarpetaUser.Checked = Visible
                mnuParentCarpetaUser.DropDownItems.Add(mnuCarpetaUser)

                AddHandler mnuCarpetaUser.Click, AddressOf mnuCarpetesUser_Click
                If Visible = True Then
                    grid = New MetroFramework.Controls.MetroGrid
                    If SHOW_CARP = True Then
                        grid = createTabAndGridAudioUsers(AudioNom, AudioID, tpControl)
                    Else
                        grid = createTabAndGridAudioUsers(AudioNom, AudioID, tabAudioDBS)
                    End If
                    Dim rsListAudioUser As DataTable = db.getTable("SELECT audio_id, audio_nom, audio_durada FROM audios_params WHERE audio_actv=1 AND listAudio_id=" & AudioID.ToString & ";")
                    grid.DataSource = rsListAudioUser
                End If
            Next
        Next
        db = Nothing
    End Sub

    Private Sub mnuCarpetesUser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        CType(sender, ToolStripMenuItem).Checked = Not CType(sender, ToolStripMenuItem).Checked

        Dim ChK As Boolean = CType(sender, ToolStripMenuItem).Checked
        Dim AudioID As Integer = CType(sender, ToolStripMenuItem).Tag
        Dim db As New MSC.dbs(Cloud)
        Dim StrSql As String = "DELETE FROM  user_audio  WHERE `user_id`=" & Usuari.UsrID & "  and  `audio_id` = " & AudioID & ";"
        db.Delete_ID(StrSql)
        StrSql = "INSERT INTO  user_audio (`user_id`,`audio_id`, `visible`) VALUES (" & Usuari.UsrID & ", " & AudioID & ", " & IIf(ChK = True, 1, 0) & " );"
        db.New_ID(StrSql)


        db = Nothing
        RefreshDataSet(TAULES_DBS.TAULA_USER_AUDIO)
        LoadAudioUSR()
    End Sub

    Private Function createTabAndGridAudioUsers(ByVal NomTab As String, ByVal AudioID As Integer, ParentTab As MetroFramework.Controls.MetroTabControl) As MetroFramework.Controls.MetroGrid
        'Crear el tab        
        Dim NewTabPageAudioUser As New MetroFramework.Controls.MetroTabPage()
        NewTabPageAudioUser.Name = "Audio" & AudioID.ToString
        NewTabPageAudioUser.Text = NomTab
        NewTabPageAudioUser.Tag = AudioID
        NewTabPageAudioUser.Visible = True
        NewTabPageAudioUser.ImageIndex = 5
        NewTabPageAudioUser.Theme = Me.Theme

        NewTabPageAudioUser.UseVisualStyleBackColor = True
        NewTabPageAudioUser.HorizontalScrollbarBarColor = True
        NewTabPageAudioUser.HorizontalScrollbarHighlightOnWheel = False
        NewTabPageAudioUser.HorizontalScrollbarSize = 10


        ParentTab.TabPages.Add(NewTabPageAudioUser)

        'crear el grid
        Dim gridAudioUser As New MetroFramework.Controls.MetroGrid()

        gridAudioUser.Parent = NewTabPageAudioUser 'tabAudioDBS.TabPages("Audio" & AudioID.ToString)

        gridAudioUser.AllowUserToAddRows = False
        gridAudioUser.AllowUserToDeleteRows = False
        gridAudioUser.AllowUserToResizeRows = False
        gridAudioUser.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        gridAudioUser.Size = New System.Drawing.Size(ParentTab.Width - 10, ParentTab.Height - 70)
        gridAudioUser.Location = New System.Drawing.Point(3, 3)
        gridAudioUser.BackgroundColor = System.Drawing.SystemColors.Control
        gridAudioUser.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None
        gridAudioUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        gridAudioUser.ContextMenuStrip = Me.ContextMenuStripBotoDret
        'gridAudioUser.Cursor = System.Windows.Forms.Cursors.Hand

        gridAudioUser.MultiSelect = True
        gridAudioUser.Name = "gridAudio" & AudioID.ToString
        gridAudioUser.ReadOnly = True
        gridAudioUser.RowHeadersVisible = False
        gridAudioUser.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        gridAudioUser.TabIndex = 6
        gridAudioUser.Tag = mdlMscDefines.Tipus_Play.CTL_AUDIO_USR
        gridAudioUser.Theme = Me.Theme
        gridAudioUser.EnableHeadersVisualStyles = False
        gridAudioUser.BorderStyle = Windows.Forms.BorderStyle.None
        gridAudioUser.Theme = Me.Theme

        'DataGridViewTextBoxColumn1
        '
        Dim DataGridViewTextBoxColumn1 As New System.Windows.Forms.DataGridViewTextBoxColumn
        DataGridViewTextBoxColumn1.DataPropertyName = "audio_id"
        DataGridViewTextBoxColumn1.HeaderText = HEADERTEXT_ID
        DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumnIDAudio" & AudioID.ToString
        DataGridViewTextBoxColumn1.ReadOnly = True
        DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Dim DataGridViewTextBoxColumn2 As New System.Windows.Forms.DataGridViewTextBoxColumn
        DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        DataGridViewTextBoxColumn2.DataPropertyName = "audio_nom"
        DataGridViewTextBoxColumn2.HeaderText = HEADERTEXT_NAME
        DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumnNomAudio" & AudioID.ToString
        DataGridViewTextBoxColumn2.ReadOnly = True
        'DataGridViewTextBoxColumn3
        '
        Dim DataGridViewTextBoxColumn4 As New System.Windows.Forms.DataGridViewTextBoxColumn
        DataGridViewTextBoxColumn4.DataPropertyName = "audio_durada"
        DataGridViewTextBoxColumn4.HeaderText = HEADERTEXT_DURADA
        DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumnDuradaAudio" & AudioID.ToString
        DataGridViewTextBoxColumn4.ReadOnly = True
        DataGridViewTextBoxColumn4.Width = 50

        gridAudioUser.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {DataGridViewTextBoxColumn1, DataGridViewTextBoxColumn2, DataGridViewTextBoxColumn4})

        'S'han d'afegir els events corresponents
        AddHandler gridAudioUser.MouseDown, AddressOf MouseDownListDBS
        AddHandler gridAudioUser.CellMouseDown, AddressOf CellMouseDownListDBS
        AddHandler gridAudioUser.MouseMove, AddressOf MouseMoveListDBS
        AddHandler gridAudioUser.CellMouseMove, AddressOf CellMouseMoveListDBS
        'AddHandler gridAudioUser.ColumnHeaderMouseDoubleClick, AddressOf ColumnHeaderMouseDoubleClickListDBS

        Return gridAudioUser
    End Function

    Private Sub mnuLimitRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLimitRecord.Click
        My.Settings.MaxRS = CInt(mnuLimitRecord.Text)
    End Sub

    Private Sub mnuLimitRecord_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles mnuLimitRecord.KeyPress
        Dim KeyAscii As Short = Asc(e.KeyChar)
        If KeyAscii < Asc("0") OrElse KeyAscii > Asc("9") Then If KeyAscii <> 8 Then KeyAscii = 0
        e.KeyChar = Chr(KeyAscii)
        If KeyAscii = 0 Then e.Handled = True
    End Sub

    Private Sub CellMouseDownListDBS(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) _
        Handles gridPubli.CellMouseDown, gridTemes.CellMouseDown, gridTemes.CellMouseDown
        'TODO: canviar això a un altre esdeviniment de pantalla per no anular la ordenació de columnes.
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            gridTipusFitxer = CType(sender, DataGridView)
            ContextMenuStripBotoDret.Show(gridTipusFitxer, e.X, e.Y)
        End If
    End Sub

    Private Sub MouseDownListDBS(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles gridPubli.MouseDown, gridTemes.MouseDown
        Try
            InfoMsg.Close()
            gridTipusFitxer = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub MouseMoveListDBS(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
        Handles gridPubli.MouseMove, gridTemes.MouseMove

        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            gridTipusFitxer = CType(sender, DataGridView)
            Dim ErrLog As String = ""
            listAudioFromDBS = createListPlayerToCopy(ErrLog)
            If listAudioFromDBS Is Nothing Then
                If ErrLog.Length > 1 Then MetroFramework.MetroMessageBox.Show(Me, ErrLog, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Warning, 175)
            Else
                Me.gridTipusFitxer.DoDragDrop(ErrLog, DragDropEffects.Copy)
                ButtonsDataObject = Nothing
            End If
            gridTipusFitxer = Nothing
        End If

    End Sub

    Private Sub CellMouseMoveListDBS(sender As Object, e As DataGridViewCellMouseEventArgs) _
        Handles gridTemes.CellMouseMove, gridPubli.CellMouseMove
        ButtonsDataObject = Nothing
    End Sub

    Private Sub ToolStripTextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBuscaTema.KeyDown, comboBuscaTema.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim StrWHERE As String = ""
            If Me.findPerDisc.Checked Then
                If comboBuscaTema.Text.Length = 0 Then
                    StrWHERE = ""
                ElseIf comboBuscaTema.SelectedValue Is Nothing Then
                    Dim strMsg As String = String.Format(MSG_BUSCATEMA_NO_VALID, comboBuscaTema.Text)
                    MetroFramework.MetroMessageBox.Show(Me, strMsg, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Warning, 150)
                    Exit Sub
                Else
                    StrWHERE = "tema_disc = " & comboBuscaTema.SelectedValue
                End If
                If comboBuscaTema.SelectedValue = 0 Then StrWHERE = ""
            ElseIf Me.findPerInterp.Checked Then
                If comboBuscaTema.Text.Length = 0 Then
                    StrWHERE = ""
                ElseIf comboBuscaTema.SelectedValue Is Nothing Then
                    Dim strMsg As String = String.Format(MSG_BUSCATEMA_NO_VALID, comboBuscaTema.Text)
                    MetroFramework.MetroMessageBox.Show(Me, strMsg, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Warning, 150)
                    Exit Sub
                Else
                    StrWHERE = "tema_interp = " & comboBuscaTema.SelectedValue
                End If
                If comboBuscaTema.SelectedValue = 0 Then StrWHERE = ""
            ElseIf Me.findPerTitol.Checked Then
                StrWHERE = "tema_titol LIKE '%" & AddSlashes(Me.txtBuscaTema.Text) & "%' "
            End If
            getListTemes(StrWHERE)
        End If
    End Sub

    Private Sub findPerTitol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles findPerTitol.Click, findPerDisc.Click, findPerInterp.Click
        Me.findPerDisc.Checked = False
        Me.findPerInterp.Checked = False
        Me.findPerTitol.Checked = False
        CType(sender, ToolStripMenuItem).Checked = True
        Dim Cmb As New combo
        Select Case CType(sender, ToolStripMenuItem).Name
            Case "findPerDisc"
                Me.comboBuscaTema.Visible = True
                txtBuscaTema.Visible = False
                comboBuscaTema.DataSource = Nothing
                Cmb.OmpleCombo(comboBuscaTema, TAULES_DBS.TAULA_DISCOS, TotsCap.TOTS)
                comboBuscaTema.Focus()
            Case "findPerInterp"
                Me.comboBuscaTema.Visible = True
                txtBuscaTema.Visible = False
                comboBuscaTema.DataSource = Nothing
                Cmb.OmpleCombo(comboBuscaTema, TAULES_DBS.TAULA_INTERPRETS, TotsCap.TOTS)
                comboBuscaTema.Focus()
            Case "findPerTitol"
                Me.comboBuscaTema.Visible = False
                txtBuscaTema.Visible = True
                txtBuscaTema.Focus()
        End Select

    End Sub

    Private Sub RecercaAvançadaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RecercaAvançadaToolStripMenuItem.Click
        frmBuscarTemes.IsClosed = False
        frmBuscarTemes.Show()
        'getListTemes(frmBuscarTemes.StrSql)
    End Sub

    Private Sub cmbHora_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbHora.SelectedIndexChanged
        getListPublicitat()
    End Sub

    Private Sub mnuMesInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMesInfo.Click
        'gridTipusFitxer = CType(sender, DataGridView) 
        gridTipusFitxer = CType(ContextMenuStripBotoDret.SourceControl, DataGridView)
        Dim Tipus As Tipus_Play = CType(gridTipusFitxer.Tag, Tipus_Play)
        Dim id As Integer = gridTipusFitxer.CurrentRow.Cells(0).Value
        Dim strText As String = strInfoFitxer(Tipus, id)
        InfoMsg = New frmTip

        InfoMsg.ShowMessage(strText, MSG_MORE_INFO, IconStyles.Information,
            ContentAlignment.MiddleCenter, 0, 0, True, , , , , Themes.WinXpStyle, MessageBoxButtons.OK)
        gridTipusFitxer = Nothing
    End Sub

    Friend Sub AfegirAReproducció_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim ErrLog As String = ""
        listAudioFromDBS = Nothing
        Dim AudioToPlay() As ListAudioSelect = createListPlayerToCopy(ErrLog)

        Dim IDCart As String = CType(sender, ToolStripMenuItem).Tag.ToString
        Dim formCart As Form = Nothing
        Dim frm As Form
        For Each frm In My.Application.OpenForms
            If frm.Name = "frmCartutxera" And frm.Text.Contains("Cart " & IDCart) Then
                formCart = frm
            End If
        Next
        Try
            If AudioToPlay IsNot Nothing AndAlso AudioToPlay.Length > 0 Then CType(formCart, frmCartutxera).addElementlist(AudioToPlay)
            If ErrLog.Length > 1 Then MetroFramework.MetroMessageBox.Show(Me, ErrLog, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Warning, 150)
        Catch ex As Exception

        End Try

    End Sub

    Private Function createListPlayerToCopy(ByRef ErrLog As String) As ListAudioSelect()
        Dim AudioToPlay() As ListAudioSelect = Nothing
        If gridTipusFitxer Is Nothing Then gridTipusFitxer = CType(Form.ActiveForm.ActiveControl, DataGridView)
        Try
            Dim Tipus As Tipus_Play = CType(gridTipusFitxer.Tag, Tipus_Play)
            Dim NewID As Integer = 0
            'Dim ErrLog As String = ""
            For i As Integer = 0 To gridTipusFitxer.SelectedRows.Count - 1
                Dim PathFitxer As String = ""
                Dim Durada As Date
                Dim BPM As Single = 0
                Dim SubTitol As String = ""
                'TODO: ERROR conficte amb la nova columna
                Dim ID As Integer = gridTipusFitxer.SelectedRows(i).Cells(0).Value
                Select Case Tipus
                    Case Tipus_Play.CTL_MUSICA : PathFitxer = Params.PathMusica & ID & ".mp3" : SubTitol = getNomInterp(gridTipusFitxer.SelectedRows(i).Cells(2).Value) : BPM = getBPMFromMusic(ID)
                    Case Tipus_Play.CTL_PUBLICITAT : PathFitxer = Params.PathPublicitat & ID & ".mp3" : SubTitol = getNomClient(gridTipusFitxer.SelectedRows(i).Cells(2).Value)
                    Case Tipus_Play.CTL_PROGRAMA : PathFitxer = Params.PathProgrames & ID & ".mp3" : SubTitol = getNomTematica(gridTipusFitxer.SelectedRows(i).Cells(2).Value)
                    Case Tipus_Play.CTL_AUDIO_USR
                        PathFitxer = Params.PathAudioUser & ID & ".mp3"
                        SubTitol = Params.NomRadio
                        Dim id_tipus As Integer = CInt(gridTipusFitxer.Parent.Tag)
                        Tipus = 600 + id_tipus
                End Select
                If OKFitxerToPlay(PathFitxer, Durada) Then
                    ReDim Preserve AudioToPlay(NewID)
                    AudioToPlay(NewID).AUDIO_TipFitxer = Tipus
                    AudioToPlay(NewID).AUDIO_ID = gridTipusFitxer.SelectedRows(i).Cells(0).Value
                    AudioToPlay(NewID).AUDIO_Path = PathFitxer
                    AudioToPlay(NewID).AUDIO_Titol = gridTipusFitxer.SelectedRows(i).Cells(1).Value
                    AudioToPlay(NewID).AUDIO_Durada = Durada
                    AudioToPlay(NewID).AUDIO_BPM = BPM
                    AudioToPlay(NewID).AUDIO_SubTitol = SubTitol

                    If Tipus = Tipus_Play.CTL_PROGRAMA Then
                        For f As Short = 2 To 4
                            PathFitxer = Params.PathProgrames & AudioToPlay(i).AUDIO_ID & "_" & f & ".mp3"
                            If OKFitxerToPlay(PathFitxer, Durada) Then
                                NewID = NewID + 1
                                ReDim Preserve AudioToPlay(NewID)
                                AudioToPlay(NewID).AUDIO_ID = gridTipusFitxer.SelectedRows(i).Cells(0).Value
                                AudioToPlay(NewID).AUDIO_TipFitxer = Tipus
                                AudioToPlay(NewID).AUDIO_Path = PathFitxer
                                AudioToPlay(NewID).AUDIO_Titol = gridTipusFitxer.SelectedRows(i).Cells(1).Value
                                AudioToPlay(NewID).AUDIO_SubTitol = SubTitol & " (Part:" & f & ")"
                                AudioToPlay(NewID).AUDIO_Durada = Durada
                            End If
                        Next f
                    End If
                    NewID = NewID + 1
                Else
                    Dim strMsg As String = String.Format(MSG_NO_ADD_TO_PLAY, gridTipusFitxer.SelectedRows(i).Cells(1).Value)
                    ErrLog = ErrLog & strMsg & vbCrLf
                End If
            Next i
            'MoveInterPlayers = False
            Return AudioToPlay
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub cmdSortir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSortir.Click
        Dim frm As Form
        For Each frm In My.Application.OpenForms
            If frm.Name = "frmCartutxera" Then
                Dim formCart As Form = Nothing
                formCart = frm
                CType(formCart, frmCartutxera).mnuExplorerDBS.Checked = False
            End If
        Next
        Me.Close()
    End Sub

    Private Sub mnuLimitRegChec_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLimitRegChec.Click
        mnuLimitRegChec.Checked = Not mnuLimitRegChec.Checked
        mnuLimitRecord.Enabled = mnuLimitRegChec.Checked
        My.Settings.Enab_limit = mnuLimitRegChec.Checked
    End Sub

    Private Sub PlayCue()
        Try
            If gridTipusFitxer Is Nothing Then gridTipusFitxer = CType(Form.ActiveForm.ActiveControl, DataGridView)
        Catch ex As Exception
            Exit Sub
        End Try
        If Not IsNothing(gridTipusFitxer) Then
            Dim PathFitxer As String = ""
            Dim Tipus As Tipus_Play = CType(gridTipusFitxer.Tag, Tipus_Play)
            Dim ID As Integer = gridTipusFitxer.SelectedRows(0).Cells(0).Value
            Select Case Tipus
                Case Tipus_Play.CTL_MUSICA : PathFitxer = Params.PathMusica & "\" & ID & ".mp3"
                Case Tipus_Play.CTL_PUBLICITAT : PathFitxer = Params.PathPublicitat & "\" & ID & ".mp3"
                Case Tipus_Play.CTL_PROGRAMA : PathFitxer = Params.PathProgrames & "\" & ID & "_1.mp3"
                Case Tipus_Play.CTL_AUDIO_USR
                    Dim NomAudio As String = gridTipusFitxer.Parent.Name.ToLower
                    PathFitxer = Params.PathAudioUser & "\" & NomAudio & "\" & ID & ".mp3"
            End Select
            If PlayerPre.numCart = 0 Then PlayerPre.numCart = 1
            If PlayerPre.IsPlaying Then PlayerPre.StopAudio()
            PlayerPre.PlayAudio(PathFitxer)
        End If
        gridTipusFitxer = Nothing
    End Sub

    Private Sub mnuPreEscolta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreEscolta.Click
        PlayCue()
    End Sub

    Private Sub mnuColumnVisible_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuColumnVisibleEstil.Click, mnuColumnVisibleAny.Click, mnuColumnVisibleIdioma.Click, mnuColumnVisibleRadia.Click, mnuColumnVisibleRitme.Click, mnuColumnVisibleSubj.Click, mnuColumnVisibleTemp.Click
        Dim Check As Boolean = Not CType(sender, ToolStripMenuItem).Checked
        CType(sender, ToolStripMenuItem).Checked = Check
        Dim IdColumn As Integer = CType(sender, ToolStripMenuItem).Tag
        Me.gridTemes.Columns(IdColumn).Visible = Check

    End Sub

    Private Sub comboBuscaTema_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles comboBuscaTema.KeyUp
        Select Case e.KeyCode
            Case Keys.Left, Keys.Right, Keys.Up, Keys.Down
                Return
        End Select
        Dim MyComboBox As ComboBox = CType(sender, ComboBox)
        Dim Origen As TAULES_DBS
        If Me.findPerDisc.Checked Then
            Origen = TAULES_DBS.TAULA_DISCOS
        ElseIf Me.findPerInterp.Checked Then
            Origen = TAULES_DBS.TAULA_INTERPRETS
        Else
            Exit Sub
        End If
        refreshComboBoxKeyUp(MyComboBox, Origen, TotsCap.CAP)
    End Sub

    Private Sub frmAudioDBS_Move(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Move
        saveSettings()
    End Sub

    Private Sub frmAudioDBS_ResizeEnd(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ResizeEnd
        saveSettings()
    End Sub

    Private Sub saveSettings()
        If Me.Visible = True Then
            Dim FitxerINI As New IniFile
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "DBS_L", CStr(Me.Left))
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "DBS_T", CStr(Me.Top))
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "DBS_W", CStr(Me.Width))
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "DBS_H", CStr(Me.Height))
        End If

    End Sub

    Private Sub gridTemes_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles gridTemes.MouseUp
        listAudioFromDBS = Nothing
    End Sub

    Sub MnuPreescoltaStopClick(ByVal sender As Object, ByVal e As EventArgs) Handles mnuPreescoltaStop.Click
        PlayerPre.StopAudio()
    End Sub

    Private Sub tmr_publi_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr_publi.Tick
        Static BlocPubliRealitzat(3) As Boolean
        Select Case TimeOfDay.Minute
            Case 0 To 5
                If BlocPubliRealitzat(0) = False Then
                    cmbHora.Text = TimeOfDay.Hour.ToString("00") & ":00"
                    BlocPubliRealitzat(0) = True
                    Me.lbAtencioPubli.Visible = (gridPubli.Rows.Count > 0)
                End If
                BlocPubliRealitzat(3) = False
            Case 10 To 15
                If BlocPubliRealitzat(1) = False Then
                    cmbHora.Text = TimeOfDay.Hour.ToString("00") & ":15"
                    BlocPubliRealitzat(1) = True
                    Me.lbAtencioPubli.Visible = (gridPubli.Rows.Count > 0)
                End If

                BlocPubliRealitzat(0) = False
            Case 25 To 30
                If BlocPubliRealitzat(2) = False Then
                    cmbHora.Text = TimeOfDay.Hour.ToString("00") & ":30"
                    BlocPubliRealitzat(2) = True
                    Me.lbAtencioPubli.Visible = (gridPubli.Rows.Count > 0)
                End If
                BlocPubliRealitzat(1) = False
            Case 40 To 45
                If BlocPubliRealitzat(3) = False Then
                    cmbHora.Text = TimeOfDay.Hour.ToString("00") & ":45"
                    BlocPubliRealitzat(3) = True
                    Me.lbAtencioPubli.Visible = (gridPubli.Rows.Count > 0)
                End If
                BlocPubliRealitzat(2) = False
        End Select

    End Sub

    Private Sub lbAtencioPubli_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles lbAtencioPubli.DoubleClick
        lbAtencioPubli.Visible = False
    End Sub

    Private Sub ContextMenuStripBotoDret_Opening(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStripBotoDret.Opening
        If gridTipusFitxer Is Nothing Then Exit Sub
        mnuAddDelFav.Visible = False
        mnuSeparadorFav.Visible = False
        If gridTipusFitxer.Name = "gridTemes" AndAlso gridTemes.Columns("ColumnFav").Visible = True Then
            mnuAddDelFav.Visible = True
            mnuSeparadorFav.Visible = True
            If gridTemes.SelectedRows(0).Cells("ColumnFav").Value = 1 Then
                mnuAddDelFav.Text = MNU_DEL_FAV
                mnuAddDelFav.Image = Me.ImageListFavorites.Images.Item("favNO")
            Else
                mnuAddDelFav.Text = MNU_ADD_FAV
                mnuAddDelFav.Image = Me.ImageListFavorites.Images.Item("favSi")
            End If
        End If
        mnuColumns.DropDownItems.Clear()
        For i As Integer = 0 To gridTipusFitxer.ColumnCount - 1
            If gridTipusFitxer.Columns(i).Visible = True Then
                Dim mnuOrderColumn As ToolStripMenuItem
                mnuOrderColumn = New ToolStripMenuItem
                mnuOrderColumn.Text = gridTipusFitxer.Columns(i).HeaderText.ToString
                mnuOrderColumn.Tag = i
                AddHandler mnuOrderColumn.Click, AddressOf ColumnHeaderClickListDBS
                Me.mnuColumns.DropDownItems.Add(mnuOrderColumn)
            End If
        Next
    End Sub

    Private Sub ColumnHeaderClickListDBS(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'gridTipusFitxer = CType(sender, DataGridView)
        Try
            Dim newColumn As DataGridViewColumn = gridTipusFitxer.Columns(CType(sender, ToolStripMenuItem).Tag)
            Dim oldColumn As DataGridViewColumn = gridTipusFitxer.SortedColumn
            Dim direction As System.ComponentModel.ListSortDirection

            ' If oldColumn is null, then the DataGridView is not currently sorted. 
            If oldColumn IsNot Nothing Then
                ' Sort the same column again, reversing the SortOrder. 
                If oldColumn Is newColumn AndAlso gridTipusFitxer.SortOrder =
                    SortOrder.Ascending Then
                    direction = System.ComponentModel.ListSortDirection.Descending
                    ' Msgbox HERE
                Else

                    ' Sort a new column and remove the old SortGlyph.
                    direction = System.ComponentModel.ListSortDirection.Ascending
                    oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None
                    ' Msgbox HERE
                End If
            Else
                direction = System.ComponentModel.ListSortDirection.Ascending
                ' Msgbox HERE
            End If

            ' Sort the selected column.
            gridTipusFitxer.Sort(newColumn, direction)
            If direction = System.ComponentModel.ListSortDirection.Ascending Then
                newColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
            Else
                newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
            End If
        Catch ex As Exception
        End Try


    End Sub

    Private Sub mnuAddDelFav_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddDelFav.Click
        Dim db As New MSC.dbs(Cloud)
        Dim SqlStr As String = ""
        Dim TemID As Integer = gridTemes.SelectedRows(0).Cells("ColumnID").Value
        If gridTemes.SelectedRows(0).Cells("ColumnFav").Value = 1 Then
            gridTemes.SelectedRows(0).Cells("ColumnFav").Value = 0
            SqlStr = "DELETE FROM user_temes WHERE usrtema_tema=" & TemID & " AND usrtema_user=" & Usuari.UsrID & " ;"
            db.Delete_ID(SqlStr)
        Else
            gridTemes.SelectedRows(0).Cells("ColumnFav").Value = 1
            SqlStr = "INSERT into user_temes (usrtema_user,usrtema_tema, usrtema_datain) VALUES (" & Usuari.UsrID & "," & TemID & ",now());"
            db.New_ID(SqlStr)
        End If
        db = Nothing
    End Sub

    Private Sub EditAudioWave()
        If ProgEditAudio.Length > 0 Then
            Try
                PlayerPre.StopAudio()
                'TODO:Possible problema si ja està sonant
                Dim PathFitxer As String = ""
                Dim PathArrel As String = ""
                Dim Tipus As Tipus_Play = CType(gridTipusFitxer.Tag, Tipus_Play)

                Select Case Tipus
                    Case Tipus_Play.CTL_MUSICA : PathArrel = Params.PathMusica
                    Case Tipus_Play.CTL_PUBLICITAT : PathArrel = Params.PathPublicitat
                    Case Tipus_Play.CTL_PROGRAMA : PathArrel = Params.PathProgrames
                    Case Tipus_Play.CTL_AUDIO_USR
                        Dim NomAudio As String = gridTipusFitxer.Parent.Name.ToLower
                        PathArrel = Params.PathAudioUser & "\" & NomAudio
                End Select

                For i As Integer = 0 To gridTipusFitxer.SelectedRows.Count - 1
                    Dim ID As Integer = gridTipusFitxer.SelectedRows(i).Cells(0).Value
                    Dim tempPath As String = PathArrel & "\" & ID & ".mp3"
                    If IO.File.Exists(tempPath) Then
                        PathFitxer += " " & Chr(34) & tempPath & Chr(34)
                    End If
                Next
                If PathFitxer.Length < 3 Then Exit Sub
                Dim p As System.Diagnostics.Process = New System.Diagnostics.Process()
                p.StartInfo.Arguments = PathFitxer.Trim
                p.StartInfo.FileName = ProgEditAudio

                p.Start()
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub mnuEditAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditAudio.Click
        EditAudioWave()
    End Sub

    Private Sub mnuIniSessio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIniSessio.Click

        Usuari.ShowPassWordForm(lang.Code)
        SetUserEnable()

    End Sub

    Sub SetUserEnable()
        If Usuari.UsrID > 0 Then
            mnuMareCarpetesUser.Visible = True
            mnuEndSessio.Enabled = True
            mnuIniSessio.Enabled = False
            tabAudioDBS.AllowDrop = (mnuShowFolders.Checked = False)
        Else
            mnuMareCarpetesUser.Visible = False
            mnuEndSessio.Enabled = False
            mnuIniSessio.Enabled = True
            tabAudioDBS.AllowDrop = False
        End If
        LoadAudioUSR()
        getListTemes()
        MyAPP.CloseSesionClient(MyAPP.IDSesion_Client, getNomAplic(MyAPP.AplicActual))
        MyAPP.NovaConnex(getNomAplic(MyAPP.AplicActual), Usuari.UsrNom, True)
    End Sub

    Private Sub mnuEndSessio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEndSessio.Click
        Usuari.EndUserSession(Usuari.UsrID)
        SetUserEnable()
    End Sub

    '----
    Private DragStartPosition As Point = Point.Empty

    Private Sub tabAudioDBS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tabAudioDBS.MouseDown
        DragStartPosition = New Point(e.X, e.Y)
    End Sub

    Private Sub tabAudioDBS_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles tabAudioDBS.MouseMove
        Dim my_tabControl As MetroFramework.Controls.MetroTabControl = CType(sender, MetroFramework.Controls.MetroTabControl)
        If e.Button <> MouseButtons.Left Then Return

        Dim r As Rectangle = New Rectangle(DragStartPosition, Size.Empty)
        r.Inflate(SystemInformation.DragSize)

        Dim tp As TabPage = HoverTab(my_tabControl)

        If Not tp Is Nothing Then
            If Not r.Contains(e.X, e.Y) Then
                my_tabControl.DoDragDrop(tp, DragDropEffects.All)
            End If
        End If

        DragStartPosition = Point.Empty

    End Sub

    Private Sub tabAudioDBS_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles tabAudioDBS.DragOver
        Dim my_tabControl As MetroFramework.Controls.MetroTabControl = CType(sender, MetroFramework.Controls.MetroTabControl)
        Dim hover_Tab As MetroFramework.Controls.MetroTabPage = HoverTab(my_tabControl)

        If hover_Tab Is Nothing Then
            e.Effect = DragDropEffects.None
        ElseIf hover_Tab.Tag.ToString = "NO_DELETE" Then
            e.Effect = DragDropEffects.None
        Else
            If e.Data.GetDataPresent(GetType(MetroFramework.Controls.MetroTabPage)) Then

                e.Effect = DragDropEffects.Move
                Dim drag_tab As MetroFramework.Controls.MetroTabPage = DirectCast(e.Data.GetData(GetType(MetroFramework.Controls.MetroTabPage)), MetroFramework.Controls.MetroTabPage)
                If hover_Tab Is drag_tab Then Return

                Dim TabRect As Rectangle = my_tabControl.GetTabRect(my_tabControl.TabPages.IndexOf(hover_Tab))
                TabRect.Inflate(-3, -3)
                If TabRect.Contains(my_tabControl.PointToClient(New Point(e.X, e.Y))) Then
                    SwapTabPages(my_tabControl, drag_tab, hover_Tab)
                    my_tabControl.SelectedTab = drag_tab
                End If
            End If
        End If

    End Sub

    Private Function HoverTab(tabCTL As MetroFramework.Controls.MetroTabControl) As MetroFramework.Controls.MetroTabPage
        For index As Int32 = 0 To tabCTL.TabCount - 1
            If tabCTL.GetTabRect(index).Contains(tabCTL.PointToClient(Cursor.Position)) Then
                Return tabCTL.TabPages(index)
            End If
        Next
    End Function

    Private Sub SwapTabPages(tabCTL As MetroFramework.Controls.MetroTabControl, ByVal tp1 As MetroFramework.Controls.MetroTabPage, ByVal tp2 As MetroFramework.Controls.MetroTabPage)
        Try
            Dim Index1 As Integer = tabCTL.TabPages.IndexOf(tp1)
            Dim Index2 As Integer = tabCTL.TabPages.IndexOf(tp2)
            tabCTL.TabPages(Index1) = tp2
            tabCTL.TabPages(Index2) = tp1
        Catch ex As Exception
        End Try
    End Sub

    Sub TabAudioDBSDragDrop(sender As Object, e As DragEventArgs) Handles tabAudioDBS.DragDrop
        If Usuari.UsrID > 0 Then
            SaveOrderAudioUser()
        End If
    End Sub

    Friend Sub setThemeControls()
        If MyThemeForm = Formthemes.dark Then
            Me.Theme = MetroFramework.MetroThemeStyle.Dark
            'tabAudioDBS.ForeColor = Color.DeepSkyBlue

            mnuPosicioPestanyes.ForeColor = Color.DeepSkyBlue
            mnuPestanyaSobre.ForeColor = Color.DeepSkyBlue
            mnuPestanyaSota.ForeColor = Color.DeepSkyBlue
            ToolStripDropDownButton2.ForeColor = Color.DeepSkyBlue
            mnuIniSessio.ForeColor = Color.DeepSkyBlue
            mnuEndSessio.ForeColor = Color.DeepSkyBlue
            ToolStripDropDownButton1.ForeColor = Color.DeepSkyBlue
            ToolStripLabel1.ForeColor = Color.DeepSkyBlue
            ToolStripLabel2.ForeColor = Color.DeepSkyBlue
            cmbHora.ForeColor = Color.DeepSkyBlue
            txtData.ForeColor = Color.DeepSkyBlue
            mnuMareCarpetesUser.ForeColor = Color.DeepSkyBlue
        Else
            Me.Theme = MetroFramework.MetroThemeStyle.Light
            'tabAudioDBS.ForeColor = Color.Black

            mnuPosicioPestanyes.ForeColor = Color.Black
            mnuPestanyaSobre.ForeColor = Color.Black
            mnuPestanyaSota.ForeColor = Color.Black
            ToolStripDropDownButton2.ForeColor = Color.Black
            mnuIniSessio.ForeColor = Color.Black
            mnuEndSessio.ForeColor = Color.Black
            ToolStripDropDownButton1.ForeColor = Color.Black
            ToolStripLabel1.ForeColor = Color.Black
            ToolStripLabel2.ForeColor = Color.Black
            cmbHora.ForeColor = Color.Black
            txtData.ForeColor = Color.Black
            mnuMareCarpetesUser.ForeColor = Color.Black
        End If
        txtData.BackColor = Me.BackColor
        cmbHora.BackColor = Me.BackColor
        ToolStrip4.BackColor = Me.BackColor
        tabAudioDBS.BackColor = Me.BackColor
        ToolStrip2.BackColor = Me.BackColor
        Me.gridTemes.Theme = Me.Theme
        Me.gridPubli.Theme = Me.Theme
        Me.tabAudioDBS.Theme = Me.Theme
        TabPage1.Theme = Me.Theme
        TabPage4.Theme = Me.Theme

        For Each ctl As Control In tabAudioDBS.Controls
            If TypeOf ctl Is MetroFramework.Controls.MetroTabPage Then
                CType(ctl, MetroFramework.Controls.MetroTabPage).Theme = Me.Theme
                For Each tabCtl As Control In ctl.Controls
                    If TypeOf tabCtl Is MetroFramework.Controls.MetroTabControl Then
                        CType(tabCtl, MetroFramework.Controls.MetroTabControl).Theme = Me.Theme
                        For Each mtp As Control In tabCtl.Controls
                            CType(mtp, MetroFramework.Controls.MetroTabPage).Theme = Me.Theme
                            For Each grid As Control In mtp.Controls
                                If TypeOf grid Is MetroFramework.Controls.MetroGrid Then
                                    CType(grid, MetroFramework.Controls.MetroGrid).Theme = Me.Theme
                                End If
                            Next
                        Next
                    ElseIf TypeOf tabCtl Is MetroFramework.Controls.MetroGrid Then
                        CType(tabCtl, MetroFramework.Controls.MetroGrid).Theme = Me.Theme
                    End If
                Next
            End If
        Next
        Me.Refresh()
    End Sub

    Private Sub frmAudioDBS_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.Modifiers
            Case Keys.Control
                Select Case e.KeyCode
                    Case Keys.E : EditAudioWave()
                End Select
            Case Keys.Shift
                Select Case e.KeyCode
                    Case Keys.F5 : PlayCue()
                    Case Keys.F7 : PlayerPre.StopAudio()
                End Select
        End Select
    End Sub

    Private Sub frmAudioDBS_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Conprovem un Path
        Dim oDir As New IO.DirectoryInfo(Params.PathMusica)
        If oDir.Exists = False Then
            MetroFramework.MetroMessageBox.Show(Me, MSG_NO_ACCES_FILE & " " & oDir.ToString, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Error, 175)
            'Exit Sub
        End If
    End Sub

    Private Sub mnuShowFolders_Click(sender As Object, e As EventArgs) Handles mnuShowFolders.Click
        mnuShowFolders.Checked = Not mnuShowFolders.Checked
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "SW_CARP", IIf(mnuShowFolders.Checked = True, 1, 0))
        LoadAudioUSR()
    End Sub

    Private Sub SaveOrderAudioUser()
        If Usuari.UsrID > 0 Then
            Dim dbs As New MSC.dbs(Cloud)
            Dim StrSql As String = "DELETE FROM user_audio WHERE user_id=" & Usuari.UsrID & " ;"
            dbs.Delete_ID(StrSql)
            Dim order As Integer = 0
            For i As Integer = 0 To tabAudioDBS.TabPages.Count - 1
                If mnuShowFolders.Checked = True Then
                    If IsNumeric(tabAudioDBS.TabPages(i).Tag) Then
                        Dim audio_id As Integer = tabAudioDBS.TabPages(i).Tag
                        Dim nom As String = tabAudioDBS.TabPages(i).Text
                        StrSql = "INSERT INTO user_audio SET `order`=" & order & ", `user_id`=" & Usuari.UsrID & ", `audio_id`=" & audio_id & ", visible=1 ;"
                        dbs.New_ID(StrSql)
                        order += 1
                    End If
                Else
                    For Each ctl As Control In tabAudioDBS.TabPages(i).Controls
                            If ctl.GetType.FullName = "MetroFramework.Controls.MetroTabControl" Then
                                Dim my_tabCtl As MetroFramework.Controls.MetroTabControl = CType(ctl, MetroFramework.Controls.MetroTabControl)
                                For u As Integer = 0 To my_tabCtl.TabPages.Count - 1
                                    If IsNumeric(my_tabCtl.TabPages(u).Tag) Then
                                        Dim audio_id As Integer = my_tabCtl.TabPages(u).Tag
                                        Dim nom As String = my_tabCtl.TabPages(u).Text
                                        StrSql = "INSERT INTO user_audio SET `order`=" & order & ", `user_id`=" & Usuari.UsrID & ", `audio_id`=" & audio_id & ", visible=1 ;"
                                        dbs.New_ID(StrSql)
                                        order += 1
                                    End If
                                Next
                            End If
                        Next
                    End If
            Next i
            RefreshDataSet(TAULES_DBS.TAULA_USER_AUDIO)

        End If
    End Sub

    Private Sub frmAudioDBS_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Usuari.EndUserSession(Usuari.UsrID)
    End Sub
End Class