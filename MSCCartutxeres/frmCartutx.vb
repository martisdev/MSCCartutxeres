Option Strict Off
Option Explicit On

Imports BassCd = Un4seen.Bass.AddOn.Cd.BassCd
Imports BassTags = Un4seen.Bass.AddOn.Tags.BassTags
Imports Un4seen.Bass
Imports VB = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Threading
Imports WaveForm = Un4seen.Bass.Misc.WaveForm
Imports System.IO.Ports 

Public Partial Class frmCartut
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	
	#Region "Definició de Variables"
	
	Const MAX_SEC_WAVE As Integer = 3600
	
	
	Dim AutoFader As Boolean
	Dim VolMax As Integer
	Dim threshold As Integer
	Dim Atack As Integer
	Dim VolNormalice As Integer
	Dim VolIni As Integer
	Dim SegActivate As Short
	Dim Inc As Short
	'dim OldIndexID As Integer = -1
	
	'Friend ActualHandle As Integer
	
	friend ActualPlay As ListAudioSelect
	Dim ProxPlay As New ListAudioSelect
	
	Dim IntroSegons As Double= 0.0
	Dim OutSegons As Double= 0.0
	Dim ManualEnd As Boolean = False 
	Dim CuePosition As Double = 0.0
	Dim LoopIn As Long
	Dim LoopOut As Long		
	Dim TotalDuration As Long
	
	Dim FreqDef As Integer = 44100
	
	Dim TimeFadeOut As Integer = 3000
	
	' Botó Cue
	'Dim CuePress As Boolean
	'Dim Position As Long
	Dim InitCue As Boolean
	' Presenta Intro
	Dim blIntro As Boolean
	
	Dim StrNumCart As String = "1"
	Dim NumCart As Integer
	Dim DEV_PLAY As Integer = 1
	
	Dim USB_ON As Integer 
	Dim USB_CUE As Integer 
	Dim USB_FADERSTART As Integer 
	
	Dim DisplayTemps As mdlMscDefines.DisplayTime
	Dim infoPlay As New BASS_SAMPLE
	Dim Invert As Boolean 'display titol
	
	Private DragLVDispNewPos As System.Windows.Forms.ListViewItem 'The item being dragged
	
	Dim InfoMsg As frmTip
	
	Dim bl_AutoSinc As Boolean
	Dim TimeToStart As Date
	Dim SegSH As Long = 0
	
	Dim filInstucServer As Thread
	Dim RI As MSC.InstruccionsRemotes
	
	Dim CloseAppSilenci As Boolean = False
	
	
	Const FORMAT_FITXER As Short = 2
	
	Dim MSG_STOPPING_ALL_CART As String
	Dim MSG_ATENCIO As String 
	Dim MSG_NO_STOP_APP As String 
	Dim MSG_ERROR_PLAYER As String 
	DIM MSG_ERROR_POSITION AS STRING 
	Dim LABEL_SERVER as String 
	Dim LABEL_HORA_SINCRO As String
	Dim LABEL_SENYALS_HORARIS As String 
	Dim LABEL_H_INI As String 
	Dim LABEL_H_END As String 
	Dim MSG_ERROR_DURADA_SINCRO As String
	Dim COMMONDIALOG1_TITLE As String 
	Dim LIST_FILTER_FILES As String 
	Dim LIST_FILTER_CART As String 
	Dim LIST_FILTER_WINAMP As String 
	Dim SAVE_FILE_TITLE As String
	Dim SAVE_FILE_FILTER As String 
	Dim SAVE_FILE_NAME As String
	Dim MSG_OVERWRITE_LIST As String
	Dim MSG_ERROR_BASS_NO_INI As String 
	Dim MES_INFO As String 
	Dim OPENFILEDIALOG1_TITLE As String 
	
	#End Region
	
	#Region "Pantalla frmCartut"
	
	
	Private Sub frmCartut_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = e.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = e.CloseReason
		Select Case UnloadMode
			Case CloseReason.UserClosing
				If Me.cmdTancar.Enabled Or CloseAppSilenci = True Then
					'si hi han altres instàncies de les cartutxeres obertes no es pot parar el programa.
					If mnuIniDirect.Checked = False And Me.chkContinuos.Checked = False Then
						Dim frm As Form
						Dim Opencart As Short
						For Each frm In My.Application.OpenForms
							If frm.Name = "frmCartut" Then
								Opencart = Opencart + 1
							End If														
						Next
						If NumCart = 1 And Opencart > 1 Then
							If CloseAppSilenci = False Then Cancel = MsgBox(MSG_STOPPING_ALL_CART, MsgBoxStyle.OkCancel, MSG_ATENCIO) = MsgBoxResult.Cancel
						End If
					End If
					If Cancel = False Then EndAplic()
				Else
					Cancel = True
					WindowState = System.Windows.Forms.FormWindowState.Minimized
				End If
			Case System.Windows.Forms.CloseReason.TaskManagerClosing
				MsgBox(MSG_NO_STOP_APP, MsgBoxStyle.Critical,MSG_ATENCIO)
				Cancel = True
				WindowState = System.Windows.Forms.FormWindowState.Minimized
			Case System.Windows.Forms.CloseReason.WindowsShutDown
				EndAplic()
			Case Else
				'MsgBox(MSG_DENY_STOP_APP, MsgBoxStyle.Information, MSG_ATENCIO)
				Cancel = True
				WindowState = System.Windows.Forms.FormWindowState.Minimized
		End Select
		e.Cancel = Cancel
	End Sub
	
	Private Sub frmCartut_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
		If e.KeyData = 65652 And e.Shift = True Then
			
		ElseIf e.KeyData = Keys.Add Or e.KeyData = Keys.Subtract Then
			''Realitzem desplaçaments dins la mateixa llista
			Dim liNew As System.Windows.Forms.ListViewItem
			Dim hitItem As System.Windows.Forms.ListViewItem = lstDisp.FocusedItem
			Dim I As Short
			Try
				Dim IndexDesp As Short = hitItem.Index
				If e.KeyData = Keys.Add Then IndexDesp = IndexDesp + 1
				If IndexDesp > Me.lstDisp.Items.Count - 1 Then IndexDesp = lstDisp.Items.Count - 1
				If e.KeyData = Keys.Subtract Then IndexDesp = IndexDesp - 1
				If IndexDesp < 0 Then IndexDesp = 0
				
				lstDisp.Items.RemoveAt(hitItem.Index)
				liNew = lstDisp.Items.Insert(IndexDesp, hitItem.Text)
				liNew.Tag = hitItem.Tag
				liNew.ImageKey = hitItem.ImageKey
				
				Do Until I = hitItem.SubItems.Count
					If liNew.SubItems.Count > I Then
						liNew.SubItems(I).Text = hitItem.SubItems(I).Text
					Else
						liNew.SubItems.Insert(I, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, hitItem.SubItems(I).Text))
					End If
					liNew.SubItems.Item(I).Tag = hitItem.SubItems.Item(I).Tag
					I = I + 1
				Loop
				liNew.Focused = True
				liNew.Selected = True
			Catch ex As Exception
			End Try
		ElseIf e.KeyData = 131140 And e.Control = True Then
			'estableix l'enfocament al llistat de temes disponibles
			lstDisp.Focus()
		End If
	End Sub
	
	Private Sub frmCartut_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		
		RI = New MSC.InstruccionsRemotes(MyAPP.IDSesion_Client)
		filInstucServer = New Thread(AddressOf RI.getRemoteInstruccions)	
		
		LoadParams()
		ListDevice()
		setLanguageForm()
		
		Dim FitxerINI As New IniFile
		
		Me.Left = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Left(StrNumCart, 6) & "_L", CStr(10)))
		Me.Top = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Left(StrNumCart, 6) & "_T", CStr(10)))
		Me.Width = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Left(StrNumCart, 6) & "_W", CStr(405)))
		Me.Height = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Left(StrNumCart, 6) & "_H", CStr(500)))
		SplitContainer1.SplitterDistance = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "SD", CStr(180)))
		
		Dim Descrip As String = MSG_START_APP & " " & My.Application.Info.Title & " (" & StrNumCart & ") V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & " PID:" & MyAPP.IDSesion_Client
		If STANDALONE = False Then addCtlRadi(0, CShort(Tipus_Play.CTL_SISTEMA), 0, "00:00:00", 0, 0, Usuari.UsrID, Descrip, 0)
		If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(Descrip)
		
		Me.mnuAutoFader.Checked = My.Settings.AutoFader
		
		If STANDALONE = False Then mnuExplorerDBS.Checked = CBool(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "FormDBS", 0))
		
		ProgEditAudio = FitxerINI.INIRead(MyAPP.IniFile, "Data Control", "ProgEditAudio", "")
		If ProgEditAudio.Length > 0 AndAlso IO.File.Exists (ProgEditAudio) Then
			Dim versionInfo As FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(ProgEditAudio)
			Me.mnuProgEditAudio.Text = "Editor: " & versionInfo.FileDescription
			mnuEditAudio.Enabled = True
		End If
		
		Me.lbDisplayTitol.Text = My.Application.Info.Title
		
		If STANDALONE = False Then
			EinesToolStripMenuItem.DropDownItems.Add(LABEL_SERVER & ": " & MyAPP.getServerName & " / " & MyAPP.getDBSName)
			If MyAPP.getMasterServerName.Length > 0 Then EinesToolStripMenuItem.DropDownItems.Add("MASTER: " & MyAPP.getMasterServerName & " /" & MyAPP.getMasterDBSName)			
		End If
		
		ListLanguage()
		
		If STANDALONE = True Then
			' inhabilitem els menús
			mnuExplorerDBS.Enabled = False
			mnuExplorerDBS.Checked = False 
			mnuMoveDBS.Enabled = False
			mnuIniDirect.Enabled = False
			mnuForceDirectMusic.Enabled = False
			mnuSetTime.Enabled = False
			mnuLoadVar.Enabled = False
			cmdLoadLoop.Enabled = False
			cmdSaveLoop.Enabled = False
			mnuMesInfo.Enabled = False
			lbInfo.Text =  "Standalone"
			lbInfo.Visible = True
		End If
		
		If params.IsAlternativeDir = True Then
			Me.lbInfo.Visible = True
			Me.lbInfo.Text = MSG_ALTERNATIVE_DIR
		End If
		
		'Control remot
		
		Select NumCart
			Case 1 : USB_FADERSTART = 27 : USB_ON = 28 : USB_CUE = 29								
			Case 2 : USB_FADERSTART = 30 : USB_ON = 31 : USB_CUE = 32	
			Case 3 : USB_FADERSTART = 33 : USB_ON = 34 : USB_CUE = 35
			Case 4 : USB_FADERSTART = 36 : USB_ON = 37 : USB_CUE = 38
			Case Else : USB_FADERSTART = 27	: USB_ON = 28 : USB_CUE = 29				
		End Select		 
		Me.mnuControlRemot.Visible = CBool(IIf( NumCart= 1, True,False))
		If NumCart = 1 Then
			'mnuAirence.Visible = MyAPP.CtlDebug
			LoadCOMPorts()
			LoadAirence()
		End If
		
		For Each item As String In My.Settings.AutoCompleteList
			txtPlayURL.AutoCompleteCustomSource.Add(item)  
			txtPlayURL.Text = item
		Next 
		
		
	End Sub
	
	
	Dim MSG_START_APP As String
	Dim MSG_END_APP As String
	'Dim LABEL_REFRESH As String 
	Dim MSG_ERROR_WAVE_TOO_LONG As String 
	Dim MSG_ERROR_LOAD_URL As String 
	Public Sub setLanguageForm()
		
		lang.StrForm = Me.Name		
		' Missatges
		'LABEL_REFRESH = "Refrescar" 'de moment no es fa servir
		MSG_START_APP = lang.getText("MSG_START_APP",True)'"Inici execució"
		MSG_END_APP = lang.getText("MSG_END_APP",True)'"Final execució"
		
		MSG_STOPPING_ALL_CART  = lang.getText("MSG_STOPPING_ALL_CART") '"Atenció es pararan totes les cartutxeres, vols continuar?"
		MSG_ATENCIO  = lang.getText("MSG_ATENCIO",True)
		MSG_NO_STOP_APP = lang.getText("MSG_NO_STOP_APP") '"Aquest programa no es pot parar d'aquesta manera"
		MSG_ERROR_PLAYER = lang.getText("MSG_ERROR_PLAYER") '"Error del reproductor"
		MSG_ERROR_POSITION = lang.getText("MSG_ERROR_POSITION") '"Error de posicionament"
		MSG_ERROR_WAVE_TOO_LONG = lang.getText("MSG_ERROR_WAVE_TOO_LONG",True)'"Fitxer massa llarg, no es carregarà l'ona"
		
		LABEL_SERVER = lang.getText("LABEL_SERVER",true) '"Servidor"
		LABEL_HORA_SINCRO = lang.getText("LABEL_HORA_SINCRO") '"Hora Sincro."
		LABEL_SENYALS_HORARIS = lang.getText("LABEL_SENYALS_HORARIS",True) '"Senyals Horaris"
		LABEL_H_INI = lang.getText("LABEL_H_INI") '"H. Ini"
		LABEL_H_END = lang.getText("LABEL_H_END") '"H. End"
		MSG_ERROR_DURADA_SINCRO = lang.getText("MSG_ERROR_DURADA_SINCRO") '"La hora final no és correcte segons la durada total ({0})."
		COMMONDIALOG1_TITLE = lang.getText("COMMONDIALOG1_TITLE") '"Carregar fitxer ..."
		LIST_FILTER_FILES = lang.getText("LABEL_FITXERS",True) '"Fitxers"
		LIST_FILTER_CART = lang.getText("HEADERTEXT_LLISTAT",True) '"Llistats Cartutxeres"
		LIST_FILTER_WINAMP = lang.getText("LIST_FILTER_WINAMP") '"Llistats Winamp"
		SAVE_FILE_TITLE = lang.getText("SAVE_FILE_TITLE") '"Salvar llistat"
		SAVE_FILE_FILTER  = lang.getText("SAVE_FILE_FILTER") '"Tots els arxius"
		SAVE_FILE_NAME = lang.getText("SAVE_FILE_NAME") '"Nou llistat"
		MSG_OVERWRITE_LIST = lang.getText("MSG_OVERWRITE_LIST") '"La llista {0} ja està creada, vols sobrescriure-la?"
		MSG_ERROR_BASS_NO_INI = lang.getText("MSG_ERROR_BASS_NO_INI",true) '"No es pot iniciar el sistema d'audio."
		MES_INFO = lang.getText("MSG_MORE_INFO",True)
		OPENFILEDIALOG1_TITLE = lang.getText("LABEL_BUSCAR_PROG",True) '"Buscar Programa ..."
		'Comunes
		Me.mnuExit.Text = lang.getText("LABEL_EXIT",True) '"Sortir"
		Me.mnuArxiu.Text = lang.getText("LABEL_ARXIU",True) '"Arxiu"
		Me.mnuOpenFile.Text = lang.getText("LABEL_OPEN",True) '"Obrir"
		Me.mnuSaveFile.Text = lang.getText("LABEL_SAVE",True) '"Salvar"
		Me.cmdSaveLoop.Text = lang.getText("LABEL_SAVE",True) '"Salvar"
		Me.cmdLoadLoop.Text = lang.getText("LABEL_LOAD",True) '"Carregar"		
		Me.mnuLoad.Text = lang.getText("LABEL_LOAD",True) '"Carregar"
		Me.mnuDel.Text = lang.getText("LABEL_DELETE",True) '"Borrar"
		Me.mnuStop.Text = lang.getText("LABEL_STOP",True) '"Stop"
		Me.ToolStripTextBox1.Text = lang.getText("LABEL_PLAY",True) '"Play"
		
		Me.mnuSpeed.Text = lang.getText("mnuSpeed.Text") ' Velocitat
		Me.mnuSpeedSlow.Text = lang.getText("mnuSpeedSlow.Text")' Lent
		Me.mnuSpeedNormal.Text = lang.getText("mnuSpeedNormal.Text")' Normal
		Me.mnuSpeedFast.Text = lang.getText("mnuSpeedFast.Text")' Ràpid
		
		Me.mnuCalcBPMOnLoad.Text = lang.getText("mnuCalcBPMOnLoad.Text") '"Calcular BPM al carregar"
		Me.mnuCalBPMList.Text = lang.getText("mnuCalBPMList.Text") '"Calcular BPMs del llistat"		
		Me.mnuLang.Text = lang.getText("LABEL_IDIOMA_INTERFICIE",True)' "Idioma Interfície"		
		
		Me.chkContinuos.Text = lang.getText("chkContinuos.Text") '"Continu"
		Me.AutomàticaToolStripMenuItem.Text = chkContinuos.Text  '"Continu"
		Me.chkLoop.Text = lang.getText("chkLoop.Text") '"Loop"
		Me.cmdLoopOut.Text = lang.getText("cmdLoopOut.Text") '"loop Out"
		Me.cmdLoopIn.Text = lang.getText("cmdLoopIn.Text") '"Loop In"		
		Me.Label3.Text = lang.getText("HEADERTEXT_HORA",True) '"Hora"
		Me.Label1.Text = lang.getText("LABEL_INTRO",True) '"Intro"
		'Me.lbFormatTime.Text = "Remain Time"
		'Me.mnuDef.Text = "MenuStrip1"
		Me.ProgramaToolStripMenuItem.Text = lang.getText("LABEL_PROGRAMA", True) '"Programa"
		Me.mnuNewCartut.Text = lang.getText("mnuNewCartut.Text") '"Nova Cartutxera"
		Me.mnuReOrdCart.Text = lang.getText("mnuReOrdCart.Text") '"Re-ordena l'emplaçament"
		Me.mnuReOrdCart.ToolTipText = lang.getText("mnuReOrdCart.ToolTipText") '"Estableix tamany i enplaçament per defecte"
		Me.mnuExplorerPC.Text = lang.getText("mnuExplorerPC.Text") '"Explorador del PC"
		Me.mnuExplorerDBS.Text = lang.getText("mnuExplorerDBS.Text") '"Explorador de la DBS"
		Me.mnuMoveDBS.Text = lang.getText("mnuMoveDBS.Text") '"Emplaçament per defecte expl. DBS"
		
		
		Me.mnuAutoDel.Text = lang.getText("mnuAutoDel.Text") '"Auto eliminar fitxer"
		Me.mnuReproduccio.Text = lang.getText("mnuReproduccio.Text") '"Reproducció"
		Me.GroupBox1.Text = mnuReproduccio.Text '"Reproducció"
		Me.ReproduccióToolStripMenuItem.Text = mnuReproduccio.Text '"Reproducció"
		Me.mnuPlayPause.Text = lang.getText("LABEL_PLAY_PAUSA",True) '"Play/Pausa"
		
		
		'Me.LoopToolStripMenuItem.Text = "Loop"
		Me.mnuAutoFader.Text = lang.getText("mnuAutoFader.Text") '"Auto Mescla"
		Me.mnuBucleList.Text = lang.getText("mnuBucleList.Text") '"Bucle del llistat"
		Me.AutomàticToolStripMenuItem.Text = lang.getText("AutomàticToolStripMenuItem.Text") '"Automàtic"
		Me.mnuIniDirect.Text = lang.getText("mnuIniDirect.Text") '"Iniciar MSC Directe al final de la reproducció"
		Me.mnuForceDirectMusic.Text = lang.getText("mnuForceDirectMusic.Text") '"Forçar MSC Directe a  Música"
		Me.mnuAutoSincroMare.Text = lang.getText("mnuAutoSincroMare.Text") '"Sincronitzar"
		Me.mnuAutoSincroXX.Text = lang.getText("FITXER_ALTRES",True) & "..." '"Altres ..."		
		Me.mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00" '"Hora Sincro.: 00:00:00"
		Me.mnuSH.Text = LABEL_SENYALS_HORARIS 
		Me.PresentacióToolStripMenuItem.Text = lang.getText("PresentacióToolStripMenuItem.Text") '"Presentació"
		Me.mnuBotons.Text = lang.getText("mnuBotons.Text") '"Presentació per botonera"
		Me.EinesToolStripMenuItem.Text = lang.getText("LABEL_EINES",True) '"Eines"
		Me.mnuLoadVar.Text = lang.getText("mnuReload.Text",True) '"Actualitzar variables"
		Me.mnuDevice.Text = lang.getText("mnuDevice.Text") '"Dispositius de so (Play)"
		Me.mnuDevicePreEscolta.Text = lang.getText("mnuDevicePreEscolta.Text") '"Dispositius de so (Pre-escolta)"
		Me.mnuSavedevicePlay.Text = lang.getText("mnuSavedevicePlay.Text")
		Me.mnuSavedevicePlay.ToolTipText = lang.getText("mnuSavedevicePlay.ToolTipText") '"Salva la assignació actual del dispositiu de reproducció"
		Me.mnuDelDevicePlay.Text = lang.getText("mnuDelDevicePlay.Text") '"Eliminar les assignacions personalitzades"
		'--------------
		Me.mnuAbout.Text = lang.getText("mnuRefMare.Text",True) '"&Referent a ..."
		Me.mnuHelp.Text = lang.getText("mnuRefHelp.Text",true) '"Ajuda" "Manual OnLine"
		Me.mnuHelpInternet.Text = lang.getText("mnuRefWeb.Text",true) '"Web""MSC a Internet"
		Me.mnuProgEditAudio.Text = lang.getText("mnuProgEditAudio.Text",True)'"Escollir programa d'edició d'àudio"
		Me.mnuEditAudio.Text = lang.getText("mnuEditAudio.Text",True)'"Editar l'àudio"
		
		Me.ColumnHeader21.Text = lang.getText("HEADERTEXT_TITOL",True)'"Títol"
		
		Me.ColumnHeader22.Text = lang.getText("LABEL_INTERP_CLIENT",True) '"Intèrpret/Client"
		mnuColumnInterp.Text = Me.ColumnHeader22.Text 
		'Me.ColumnHeader17.Text = ""
		Me.ColumnHeader23.Text = lang.getText("HEADERTEXT_DURADA",True)'"Durada"
		mnuColumnDurada.Text = Me.ColumnHeader23.Text 
		Me.ColumnHeader24.Text = lang.getText("LABEL_HORA_RADI",True) '"Hora radi."
		mnuColumnHora.Text = Me.ColumnHeader24.Text 
		Me.columnHeader1.Text = lang.getText("HEADERTEXT_RITME",True)'"Ritme"
		mnuColumnVRitme.Text = Me.columnHeader1.Text 
		Me.mnuMesInfo.Text = lang.getText("MSG_MORE_INFO",True)'"Més Info ..."
		
		Me.mnuPreEscoltaStop.Text = LABEL_PREESCOLTA_STOP '"Pre-escolta STOP"
		
		Me.mnuSetPoxPlay.Text = lang.getText("mnuSetPoxPlay.Text") '"Pròxim a  Play"
		Me.mnuDesmarcar.ToolTipText = lang.getText("mnuDesmarcar.Text") '"Descarca un fitxer com a ja reproduït."
		Me.mnuContextDel.ToolTipText = lang.getText("mnuContextDel.Text") '"Borrar de la llista de reprodució"
		
		'Me.ToolStrip1.Text = "ToolStrip1"
		Me.mnuPreEscolta.Text = lang.getText("mnuPreEscolta.Text") '"Pre-escolta"
		Me.cmdPlayPre.Text = mnuPreEscolta.Text '"Pre-escolta"
		Me.ToolStripLabel1.Text = Me.cmdPlayPre.Text & ":      " '"Pre-escolta" & ":      "
		
		Me.lbTamanyBotons.Text = lang.getText("lbTamanyBotons.Text") '"Mida"
		Me.cmdMesGranBotons.ToolTipText = lang.getText("cmdMesGranBotons.ToolTipText") '"Mida dels botons"		
		Me.lbTamanyBotons.ToolTipText = Me.cmdMesGranBotons.ToolTipText' "Mida dels botons"
		Me.cmdMenyGranBotons.ToolTipText = Me.cmdMesGranBotons.ToolTipText '"Mida dels botons"
		'Me.lbInfo.Text = "Sincro: 12:35:26"
		Me.mnuHistoryPlay.Text = lang.getText("mnuHistoryPlay.Text") '"Historial"
		'Me.ToolTipInfo.SetToolTip(Me.picIn, lang.getText("LABEL_PUNT_INTRO")) '"Punt Intro"
		'Me.ToolTipInfo.SetToolTip(Me.picOut, lang.getText("LABEL_PUNT_OUT"))'"Punt Out"
		Me.ToolTipInfo.SetToolTip(Me.picWave, lang.getText("LABEL_AVANT-RETRO"))'"avançar/retrocedir"    	
		Me.mnuWaveSetIntro.Text = lang.getText("mnuWaveSetIntro.Text")'"Establir aquí punt intro"
		Me.mnuWaveSetPointMix.Text = lang.getText("mnuWaveSetPointMix.Text")'"Establir aquí punt mescla"
		Me.mnuWavePlay.Text = lang.getText("mnuWavePlay.Text")'"Play aquí"
		mnuControlRemot.Text = lang.getText("LABEL_CONTROL_REMOT",True)'"Control remot"
		mnuPanicButton.Text = lang.getText("LABEL_PANIC",True)'"Botó Pànic"
		mnuInsertStop.Text = lang.getText("mnuInsertStop.Text")'"Inserta Punt Stop"
		mnuColVisibles.Text = lang.getText("LABEL_COLUMN_VISIBLES",True)'"Columnes Visibles"
		Me.mnuPlayURL.Text  = lang.getText("mnuPlayURL.Text")'"Reproduir URL"
		MSG_ERROR_LOAD_URL = lang.getText("MSG_ERROR_LOAD_URL")
		Me.Text = lang.getText("Text") '"MCS Cartutxera" o My.Application.Info.Title
		
		
	End Sub
	
	Private Sub frmCartut_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
		
		If mnuExplorerDBS.Checked Then My.Forms.frmAudioDBS.Show()
		'Carrèga fitxers per línea de comandament
		If NumCart = 1 Then
			Dim blFlad As Boolean = False
			For i As Short = 0 To My.Application.CommandLineArgs.Count - 1
				Dim PathCommandLine As String = Replace(My.Application.CommandLineArgs.Item(i), "~", " ")
				If IO.File.Exists(PathCommandLine) = True Then
					LoadToList(PathCommandLine)
					blFlad = True
				End If
			Next i
			
			If blFlad = True Then
				PlayPauseFitxer(0)                
				chkContinuos.Checked = True
				mnuIniDirect.Checked = True
				If STANDALONE = False Then IniDirecte()
			End If
		End If
		'frmHistory.hide()		
	End Sub
	#End Region
	
	
	#Region "Botons"
	
	Private Sub cmdTancar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdTancar.Click
		Me.Close()
	End Sub
	
	Private Sub ClickBotoTack(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Dim IndexLlistat As Integer = Me.flowBotons.Controls.IndexOf(CType(sender, Button))
		PlayPauseFitxer(IndexLlistat, True, True)        
		
		'Me.progressBarBotons.Visible = True		
		'progressBarBotons.Width = CType(sender, Button).Width
		'progressBarBotons.Location =  New Point ( CType(sender, Button).Bounds.X ,  CType(sender, Button).Bounds.Y + flowBotons.Width)
	End Sub
	
	Private Sub MouseDownBotoTack(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
		Try
			If e.Button = System.Windows.Forms.MouseButtons.Right Then
				sender.DoDragDrop(sender, DragDropEffects.Copy)
				Me.cmdBorrar.AllowDrop = True
				Me.cmdPlay.AllowDrop = True
			End If
			Me.lstDisp.Items(Me.flowBotons.Controls.IndexOf(CType(sender, Button))).Selected = True
			DragLVDisp = Me.lstDisp.Items(Me.flowBotons.Controls.IndexOf(CType(sender, Button)))
		Catch
			sender.DoDragDrop(sender, DragDropEffects.None)
			Me.cmdBorrar.AllowDrop = False
			Me.cmdPlay.AllowDrop = False
			DragLVDisp = Nothing
		End Try
	End Sub
	
	Private Sub cmdLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoad.Click
		LoadAllFitxers()
	End Sub
	
	Private Sub cmdBorrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBorrar.Click
		Click_Borrar()
	End Sub
	
	Private Sub Click_Borrar()
		cartOrigenMove = Nothing
		MoveInterPlayers = False
		If lstDisp.SelectedItems.Count = 0 Then BorrarFitxer(lstDisp.Items.Count - 1)
		Dim i As Short = Me.lstDisp.Items.Count - 1
		Do Until i < 0
			Try
				If Me.lstDisp.Items.Item(i).Selected Then BorrarFitxer(i)
				i -= 1
			Catch ex As Exception
				Exit Do
			End Try
		Loop        
	End Sub
	
	Private Sub cmdPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlay.Click
		If lstDisp.Items.Count = 0 Then Exit Sub
		Dim IndexLlistat As Integer = findIndexActualPlay()'GetIndexListOnFocus()
		PlayPauseFitxer(IndexLlistat)
		cartOrigenMove = Nothing
		MoveInterPlayers = False		
	End Sub
	
	Friend Sub SetRemotePlay()
		If lstDisp.Items.Count = 0 Then Exit Sub		
		Dim IndexLlistat As Integer = findIndexActualPlay()
		If ActualPlay.AUDIO_HANDLE = 0 Then
			PlayPauseFitxer(IndexLlistat)
		Else			
			If ActualPlay.isActv  = BASSActive.BASS_ACTIVE_PLAYING Then Exit Sub			
			PlayPauseFitxer(IndexLlistat)
		End If
	End Sub
	
	Friend Sub SetRemotePause()
		If ActualPlay.AUDIO_HANDLE = 0 Then Exit Sub		
		If ActualPlay.isActv <> BASSActive.BASS_ACTIVE_PLAYING Then Exit Sub
		
		If lstDisp.Items.Count = 0 Then Exit Sub
		Dim IndexLlistat As Integer = findIndexActualPlay()	  
		PlayPauseFitxer(IndexLlistat)
	End Sub
	
	Private Function GetIndexListOnFocus() As Integer        				
		If lstDisp.FocusedItem Is Nothing And lstDisp.Items.Count Then
			Return 0
		Else
			Return lstDisp.FocusedItem.Index
		End If					
	End Function
	
	Dim ManualStop As Boolean = False
	Private Sub cmdStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdStop.Click
		ManualStop = True
		StopFitxer()
	End Sub
	
	Private Sub cmdPlay_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles cmdPlay.DragDrop
		Dim IndexLlistat As Integer
		If Not DragLVDisp Is Nothing Then
			'Procedents de lstDisp
			IndexLlistat = DragLVDisp.Index
			PlayPauseFitxer(IndexLlistat, True, True)            
			DragLVDisp = Nothing
		Else
			'Procedents externs            
			IndexLlistat = lstDisp.Items.Count
			Dim path As String
			Dim filenames As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
			For Each file As String In filenames
				path = file
				LoadToList(path)
			Next
			PlayPauseFitxer(IndexLlistat, True, True)            
			TotalDuration = calcTotalDurationList()
		End If
		Me.cmdPlay.AllowDrop = False
	End Sub
	
	Private Sub cmdPlay_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles cmdPlay.DragEnter
		e.Effect = DragDropEffects.Copy
	End Sub
	
	Private Sub cmdBorrar_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles cmdBorrar.DragDrop
		Click_Borrar()
	End Sub
	
	Private Sub cmdBorrar_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles cmdBorrar.DragEnter
		If lstDisp.Items.Count Then e.Effect = DragDropEffects.Copy
	End Sub
	
	Private Sub cmdCue_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmdCue.KeyDown
		If e.KeyCode = Keys.Space Then PlayCue(True)
		If e.KeyCode = Keys.Home Then StopCue()
	End Sub
	
	Private Sub cmdCue_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cmdCue.KeyUp
		'If e.KeyCode = Keys.Space Then StopCue()
	End Sub
	
	Private Sub cmdCue_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdCue.MouseDown
		PlayCue()
	End Sub
	
	Private Sub cmdCue_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmdCue.MouseUp
		StopCue()
	End Sub
	
	Private Sub cmdPrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPrev.Click
		If lstDisp.Items.Count Then
			Dim IndexLlistat As Integer = findIndexActualPlay() - 1
			If IndexLlistat < 0 Then IndexLlistat = 0
			PlayPauseFitxer(IndexLlistat, True, True)
		End If
	End Sub
	
	Private Sub cmdNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNext.Click
		If lstDisp.Items.Count Then
			Dim IndexLlistat As Integer = findIndexActualPlay() + 1
			If IndexLlistat >= lstDisp.Items.Count And mnuBucleList.Checked = True Then
				IndexLlistat = 0
			ElseIf IndexLlistat >= lstDisp.Items.Count Then
				IndexLlistat = lstDisp.Items.Count - 1
				Exit Sub
			End If
			PlayPauseFitxer(IndexLlistat, True, True)
		End If
	End Sub
	
	Private Sub cmdLoadLoop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoadLoop.Click
		Dim idInt As Integer
		Dim idStr As String = ""
		Me.chkLoop.Checked = True
		If lbIDActual.Tag = mdlMscDefines.Tipus_Play.CTL_SISTEMA  Then
			idStr = lbIDActual.Text
		Else
			idInt = lbIDActual.Text
		End If
		Dim db As New MSC.dbs
		Dim sCmd As String
		If idStr.Length > 0 Then
			sCmd = "SELECT loop_in,loop_out FROM loops Where loop_idstr = '" & getMD5Hash(idStr) & "' ;"
		Else
			sCmd = "SELECT loop_in,loop_out FROM loops Where loop_id =" & idInt & " ;"
		End If
		Dim rs As DataTable = db.getTable(sCmd)
		If rs.Rows.Count > 0 Then
			LoopIn = rs.Rows(0)("loop_in")
			LoopOut = rs.Rows(0)("loop_out")
		Else
			LoopIn = 0
			LoopOut = ActualPlay.DurationTime
		End If
		
		refreshLoopLabels()
		
		cmdLoopIn.BackColor = Color.Lime
		cmdLoopOut.BackColor = Color.Lime
		If STANDALONE = False Then cmdSaveLoop.Enabled = False
		db = Nothing
		rs.Dispose()
	End Sub
	
	Private Sub cmdLoopIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoopIn.Click
		Me.chkLoop.Checked = True
		If cmdLoopIn.BackColor = Color.Lime Then
			LoopIn = 0
			cmdLoopIn.BackColor = Color.Transparent
		Else
			LoopIn = ActualPlay.ElapseTime
			cmdLoopIn.BackColor = Color.Lime
		End If
		If STANDALONE = False Then cmdSaveLoop.Enabled = True
		refreshLoopLabels()
	End Sub
	
	Private Sub cmdLoopOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdLoopOut.Click
		Me.chkLoop.Checked = True
		If cmdLoopOut.BackColor = Color.Lime Then
			LoopOut = ActualPlay.DurationTime
			cmdLoopOut.BackColor = Color.Transparent
		Else
			LoopOut = ActualPlay.ElapseTime
			cmdLoopOut.BackColor = Color.Lime
		End If
		If STANDALONE = False Then cmdSaveLoop.Enabled = True
		refreshLoopLabels()
	End Sub
	
	Private Sub cmdLoopInMenys_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles cmdLoopInMenys.LinkClicked
		If cmdLoopIn.BackColor <> Color.Lime Then Exit Sub
		LoopIn = LoopIn - 2000
		If LoopIn < 0 Then LoopIn = 0
		refreshLoopLabels()
		If STANDALONE = False Then cmdSaveLoop.Enabled = True
	End Sub
	
	Private Sub cmdLoopInMes_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles cmdLoopInMes.LinkClicked
		If cmdLoopIn.BackColor <> Color.Lime Then Exit Sub
		LoopIn = LoopIn + 2000
		If LoopIn > ActualPlay.DurationTime Then LoopIn = ActualPlay.DurationTime
		refreshLoopLabels()
		If STANDALONE = False Then cmdSaveLoop.Enabled = True
	End Sub
	
	Private Sub cmdLoopOutMes_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles cmdLoopOutMes.LinkClicked
		If cmdLoopOut.BackColor <> Color.Lime Then Exit Sub
		LoopOut = LoopOut + 2000
		If LoopOut > ActualPlay.DurationTime Then LoopOut = ActualPlay.DurationTime
		refreshLoopLabels()
		If STANDALONE = False Then cmdSaveLoop.Enabled = True
	End Sub
	
	Private Sub cmdLoopOutMenys_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles cmdLoopOutMenys.LinkClicked
		If cmdLoopOut.BackColor <> Color.Lime Then Exit Sub
		LoopOut = LoopOut - 2000
		If LoopOut < 0 Then LoopOut = 0
		refreshLoopLabels()
		If STANDALONE = False Then cmdSaveLoop.Enabled = True
	End Sub
	
	Private Sub cmdSaveLoop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSaveLoop.Click
		Dim idInt As Integer = 0
		Dim idStr As String = ""
		Dim db As New MSC.dbs
		Dim SqlStr As String = ""
		'If IsNumeric (lbIDActual.Text) Then 
		If lbIDActual.Tag = mdlMscDefines.Tipus_Play.CTL_SISTEMA  Then
			idStr = getMD5Hash(lbIDActual.Text)
			SqlStr = "SELECT loop_id FROM loops Where loop_idstr = '" & idStr & "'"
		Else
			idInt = lbIDActual.Text
			SqlStr = "SELECT loop_idstr FROM loops Where loop_id =" & idInt
		End If
		Dim obj As Object = db.ExecuteScalar(SqlStr)
		If obj IsNot Nothing Then
			If idStr.Length > 0 Then
				SqlStr = "DELETE FROM loops WHERE loop_idstr = '" & idStr & "' ;"
			Else
				SqlStr = "DELETE FROM loops WHERE loop_id = " & idInt & " ;"
			End If
			db.Delete_ID(SqlStr)
		End If
		SqlStr = "INSERT INTO loops (loop_id, loop_in, loop_out, loop_idstr, loop_tipfitxer)"
		SqlStr = SqlStr & " VALUES "
		SqlStr = SqlStr & " ( " & idInt & ""
		SqlStr = SqlStr & ", '" & LoopIn & "'"
		SqlStr = SqlStr & ", '" & LoopOut & "'"
		
		SqlStr = SqlStr & ", '" & idStr & "'"
		SqlStr = SqlStr & ", " & lbIDActual.Tag
		SqlStr = SqlStr & ");"
		
		db.New_ID(SqlStr)
		obj = Nothing
		db = Nothing
	End Sub
	
	Private Sub cmdSalvar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSalvar.Click
		salvarLlistat()
	End Sub
	
	#End Region
	
	#Region "Rellotges"
	
	Private Sub tmrRellotge_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrRellotge.Tick
		
		Select Case DisplayTemps
			Case mdlMscDefines.DisplayTime.DISPLAY_ELAPSE : lbFormatTime.Text = "Elapse Time"
			Case mdlMscDefines.DisplayTime.DISPLAY_REMAIN : lbFormatTime.Text = "Remain Time"
			Case mdlMscDefines.DisplayTime.DISPLAY_TOTAL : lbFormatTime.Text = "Total remain"
		End Select
		Static OldActive As Un4seen.Bass.BASSActive
		ActualPlay.isActv = Bass.BASS_ChannelIsActive(ActualPlay.AUDIO_HANDLE)
		
		If chkContinuos.Checked = False AndAlso OldActive = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING AndAlso  ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then
			Dim listID As Integer = ActualPlay.AUDIO_ListID + 1
			If IsNothing(ActualPlay.AUDIO_Path) Then listID = 0			
			'si és un STOP (tag=100) ho descartem
			If lstDisp.Items.Count > 0 _
				AndAlso listID <= (lstDisp.Items.Count - 1) _
				AndAlso lstDisp.Items.Item(listID).Tag <> 100 _ 
				AndAlso ManualStop = False Then
				Bass.BASS_SetDevice(DEV_PLAY)
				Bass.BASS_ChannelStop(ActualPlay.AUDIO_HANDLE)
				PlayPauseFitxer(listID, False,False, True)	 
			End If		
		End If
		
		If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
			ActualPlay.ElapseTime = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
			ActualPlay.RemainTime = ActualPlay.DurationTime - ActualPlay.ElapseTime			
			Select Case DisplayTemps
				Case mdlMscDefines.DisplayTime.DISPLAY_ELAPSE
					lbDisplayTime.Text = Un4seen.Bass.Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.ElapseTime), "HHMMSSFF")
				Case mdlMscDefines.DisplayTime.DISPLAY_REMAIN
					lbDisplayTime.Text = "-" & Un4seen.Bass.Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.RemainTime), "HHMMSSFF")
				Case mdlMscDefines.DisplayTime.DISPLAY_TOTAL
					Dim SegTotal As Single
					If TotalDuration = 0 Then
						SegTotal = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.RemainTime)
					Else
						SegTotal = TotalDuration - Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.ElapseTime)
					End If
					lbDisplayTime.Text = "-" & Un4seen.Bass.Utils.FixTimespan(SegTotal, "HHMMSSFF")					
			End Select			
		ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then
			Select Case DisplayTemps
				Case mdlMscDefines.DisplayTime.DISPLAY_ELAPSE
					lbDisplayTime.Text = "0:00:00.00"
				Case mdlMscDefines.DisplayTime.DISPLAY_REMAIN
					If Me.lstDisp.Items.Count Then
						'Només el fitxer sel·leccionar
						Dim IndLlist As Short
						If lstDisp.FocusedItem Is Nothing Or lstDisp.Items.Count = 0 Then
							IndLlist = 0
						Else
							IndLlist = lstDisp.FocusedItem.Index
						End If
						Try
							lbDisplayTime.Text = "-" & Un4seen.Bass.Utils.FixTimespan(SecondDec(lstDisp.Items(IndLlist).SubItems(3).Text), "HHMMSSFF")	
						Catch ex As Exception							
							lbDisplayTime.Text = "-00:00:00" 
						End Try
						
					Else
						lbDisplayTime.Text = "-0:00:00.00"
					End If
				Case mdlMscDefines.DisplayTime.DISPLAY_TOTAL
					If Me.lstDisp.Items.Count Then
						lbDisplayTime.Text = "-" & Un4seen.Bass.Utils.FixTimespan(TotalDuration, "HHMMSSFF")
					Else
						lbDisplayTime.Text = "-0:00:00.00"
					End If
			End Select					
		End If
		If bl_AutoSinc Then
			Static DisplayCount As Short
			If DisplayCount = 4 Then
				DisplayCount = 0
				If lbAutoSinc.BackColor = Color.Lime Then
					lbAutoSinc.BackColor = Color.Red
				Else
					lbAutoSinc.BackColor = Color.Lime
				End If
			Else
				DisplayCount = DisplayCount + 1
			End If
			If TimeToStart = TimeOfDay Then
				bl_AutoSinc = False
				PlayPauseFitxer(0)
				lbAutoSinc.BackColor = Color.Lime
				mnuAutoSincro59.Checked = False
				mnuAutoSincro29.Checked = False
				mnuAutoSincroXX.Checked = False
				mnuAutoSincro00.Checked = False
				mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00"
				lbInfo.Visible = False
			End If
		End If		
		Me.lbCPU.Text = "CPU: " & String.Format("{0:0.00}%",Bass.BASS_GetCPU())
		
		If STANDALONE = True Then Exit Sub 
		'---------------------------
		'Buscar instruccions remotes (Només controlar-ho a la primera cartutxera)
		'---------------------------		
		If NumCart = 1 Then
			Static TempRefresh As Date
			Static Flag As Boolean
			If TempRefresh < Now And filInstucServer.IsAlive = False Then
				TempRefresh = Now.AddMilliseconds(Params.TimeRefrestInstruc)
				filInstucServer = New Thread(AddressOf RI.getRemoteInstruccions)
				filInstucServer.IsBackground = True
				filInstucServer.Priority = ThreadPriority.Normal
				filInstucServer.Start()
				Flag = True
			ElseIf filInstucServer.IsAlive = False And Flag = True Then
				Flag = False
				'Un cop s'ha establert a procedim segons els casos
				Select Case CType(RI.ServerInstrucValue, ServerInstruc)
					Case ServerInstruc.MSC_NO_INSTRUC ' No fer res
					Case ServerInstruc.MSC_END : CloseAppSilenci = True : Me.Close() ' StopFitxer() : EndAplic()
					Case ServerInstruc.CARTUT_PLAY
						If lstDisp.Items.Count > 0 Then							
							If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PAUSED Then
								Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False)
							ElseIf ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_STOPPED Then
								Dim Index As Integer = 0
								Try
									Index = lstDisp.SelectedIndices(0)
								Catch ex As Exception
									Index = 0
								End Try
								PlayPauseFitxer(Index, True)
							End If
						End If
					Case ServerInstruc.CARTUT_PAUSA
						
						If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Then Bass.BASS_ChannelPause(ActualPlay.AUDIO_HANDLE)
						
					Case ServerInstruc.CARTUT_STOP : StopFitxer()
					Case ServerInstruc.CARTUT_END_INI_DIRECT : mnuIniDirect.Checked = True : IniDirecte()
				End Select
				MyAPP.ClearMyMemory()
			End If
		End If
		OldActive = ActualPlay.isActv
	End Sub
	
	Private Sub tmrVuMeter_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrVuMeter.Tick
		Dim vol As Integer
		Dim RealVol As Integer
		Dim LVol As Integer
		Dim RVol As Integer
		Dim graphWave As Graphics 
		'---------
		
		
		vol = Bass.BASS_ChannelGetLevel(ActualPlay.AUDIO_HANDLE)
		If vol < 0 Then vol = 0
		LVol = Un4seen.Bass.Utils.HighWord(vol)
		RVol = Un4seen.Bass.Utils.LowWord(vol)
		RealVol = (LVol + RVol) / 2 'Volum màxim = 32768		
		If VolMax < RealVol Then
			VolMax = RealVol
		Else
			' s'ha de trobar la fórmula logarítmica (valor màxim 32769 )
			If VolMax > 32000 Then
				VolMax = VolMax - (10 * Atack)
			ElseIf VolMax > 30000 Then
				VolMax = VolMax - (15 * Atack)
			ElseIf VolMax > 25000 Then
				VolMax = VolMax - (25 * Atack)
			ElseIf VolMax > 20000 Then
				VolMax = VolMax - (35 * Atack)
			ElseIf VolMax > 10000 Then
				VolMax = VolMax - (50 * Atack)
			Else
				VolMax = VolMax - (100 * Atack)
			End If
		End If		
		If ActualPlay.ElapseInSecons < 40 And (VolMax < VolNormalice) Then
			If infoPlay.volume < 100 Then
				Bass.BASS_ChannelGetAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_VOL, infoPlay.volume)
				Bass.BASS_ChannelSetAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_VOL, infoPlay.volume + 1)
			End If
			If VolMax + Inc < 32000 Then Inc = Inc + 50
		End If
		If VolMax + Inc > 32000 Then Inc = Inc - 50
		If ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then Inc = 0
		If ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then LVol = 4 : RVol = 4
		
		'---------------------------------------------
		
		Try
			Dim gruix As Integer = picVis.Width/4
			Dim LimePen As New Pen(Color.Lime, gruix)
			Dim PeakPen As New Pen(Color.Red, gruix+2)
			Dim thresholdPen As New Pen(Color.Blue, gruix-2 )		
			Dim bit As Bitmap = New Bitmap(picVis.Width, picVis.Height)
			Dim graph As Graphics = Graphics.FromImage(bit)
			Dim Y As Integer
			graph.SmoothingMode = SmoothingMode.AntiAlias
			Dim VuWidthR As Integer = ((picVis.Width*2)/6)-2
			Dim VuWidthL As Integer = (picVis.Width*2)/3
			
			' Pinta Volum dreta i esquerra
			Y = (RVol / 32768) * picVis.Height		
			If Y > picVis.Height Then Y = picVis.Height
			graph.DrawLine(LimePen, VuWidthR, picVis.Height, VuWidthR, picVis.Height - Y)
			
			Y = (LVol / 32768) * picVis.Height		
			If Y > picVis.Height Then Y = picVis.Height
			graph.DrawLine(LimePen, VuWidthL, picVis.Height, VuWidthL, picVis.Height - Y)
			
			If VolMax > 0 Then
				'Pinta peak
				Y = (VolMax / 32768) * picVis.Height
				If Y > picVis.Height Then Y = picVis.Height
				graph.DrawLine(PeakPen, VuWidthR, picVis.Height - Y, VuWidthR, picVis.Height - Y - 2)
				graph.DrawLine(PeakPen, VuWidthL, picVis.Height - Y, VuWidthL, picVis.Height - Y - 2)
				'Pinta Threshold
				Y = (threshold / 32768) * picVis.Height
				If Y > picVis.Height Then Y = picVis.Height
				graph.DrawLine(thresholdPen, VuWidthR, picVis.Height - Y, VuWidthR, picVis.Height - Y - 2)
				graph.DrawLine(thresholdPen, VuWidthL, picVis.Height - Y, VuWidthL, picVis.Height - Y - 2)
				If threshold > VolMax Then
					'Pinta valors per play
					Y = 2
					graph.DrawLine(thresholdPen, VuWidthR, picVis.Height - Y, VuWidthR, picVis.Height - Y - 2)
					graph.DrawLine(thresholdPen, VuWidthL, picVis.Height - Y, VuWidthL, picVis.Height - Y - 2)
				End If
			End If
			'draw the visual onto the picturebox
			picVis.Image = bit
			'Pinta posició			
			Dim pos As Long = 0
			Dim len As Long = 0	
			Dim bitWave As Bitmap = New Bitmap(picWave.Width, picWave.Height)
			graphWave  = Graphics.FromImage(bitWave)	
			Dim x As Integer
			Dim PositionPen As New Pen(Color.Black, 2)						
			If IsNothing(PlayerPre)= False AndAlso PlayerPre.IsPlaying AndAlso PlayerPre.numCart = NumCart Then 
				pos = Bass.BASS_ChannelGetPosition(PlayerPre.PreEscoltaHandle)
				len = Bass.BASS_ChannelGetLength(PlayerPre.PreEscoltaHandle)
				PlayerPre.PintaPicPreEscolta(picPreEsc, Atack)
			Else
				pos = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
				len = Bass.BASS_ChannelGetLength(ActualPlay.AUDIO_HANDLE)
			End If
			Dim bpp As Double = len / CType(Me.picWave.Width, Double) ' bytes per pixel  
			' position (x) where to draw the line, Integer)
			x = CType(Math.Round(pos / bpp), Double)
			graphWave.DrawLine(PositionPen, x, 0, x, Me.picWave.Height - 1)
			picWave.Image = bitWave			
			'End Posició
			
			LimePen.Dispose()
			PeakPen.Dispose()
			thresholdPen.Dispose()
			graph.Dispose()
			graphWave.Dispose()
			LimePen = Nothing
			PeakPen = Nothing
			thresholdPen = Nothing
			bit = Nothing
			graph = Nothing
			graphWave = Nothing
		Catch ex As Exception			
		End Try
	End Sub
	
	Private Sub tmrTitol_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrTitol.Tick
		If Not Invert Then
			lbDisplayTitol.Left = lbDisplayTitol.Left + 1
		Else
			lbDisplayTitol.Left = lbDisplayTitol.Left - 1
			
		End If
		If lbDisplayTitol.Location.X > 180 Then Invert = True
		If lbDisplayTitol.Location.X < -50 Then Invert = False
	End Sub
	
	Private Sub tmrDisplay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmrDisplay.Tick
		Static ExeDirecte As Boolean = False
		If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING OrElse ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
			Try
				If ActualPlay.AUDIO_HANDLE <> 0 Then ActualPlay.ElapseInSecons = CShort(Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.ElapseTime))
			Catch
				ActualPlay.ElapseInSecons = 0
			End Try
			
			'Display destellant els ùltims 25 seg.
			
			If MyMixer IsNot Nothing AndAlso  MyMixer.myDeviceDetected  = True AndAlso ActualPlay.DurationInSecons - ActualPlay.ElapseInSecons  = 25 Then
				'MyMixer.airenceSetLedBlink( USB_ON,colors_t.RED,colors_t.YELLOW,blink_speed_t.FAST)
			End If
			If (ActualPlay.DurationInSecons - ActualPlay.ElapseInSecons) < 25 And ActualPlay.DurationInSecons > 0 And lbDisplayTime.ForeColor = Color.Lime Then				
				lbDisplayTime.ForeColor = Color.Red
			Else
				lbDisplayTime.ForeColor = Color.Lime
			End If
			If InitCue Then
				If lbCue.BackColor = Color.Lime Then
					lbCue.BackColor = Color.Red
				Else
					lbCue.BackColor = Color.Lime
				End If
			End If
			'Display Introducció
			If blIntro Then
				Dim SegIntroRest As Double = IntroSegons - ActualPlay.ElapseInSecons
				If IntroSegons <= ActualPlay.ElapseInSecons Then
					blIntro = False
					lbRestaIntro.Text = "00:00"
					lbRestaIntro.ForeColor = Color.Lime
					Exit Sub
				Else
					lbRestaIntro.Text = Un4seen.Bass.Utils.FixTimespan(SegIntroRest, "MMSS")
					If lbRestaIntro.ForeColor = Color.Red Then
						lbRestaIntro.ForeColor = Color.Lime
					Else
						lbRestaIntro.ForeColor = Color.Red
					End If
				End If
			Else
				lbRestaIntro.Text = "00:00"
				lbRestaIntro.ForeColor = Color.Lime
			End If
			
			lbDisplayTitol.Text = ActualPlay.AUDIO_Titol & " / " & ActualPlay.AUDIO_SubTitol
			'NO Poder sortir del programa
			Me.cmdTancar.Enabled = False
			Me.mnuExit.Enabled = False
			ExeDirecte = (mnuIniDirect.Checked And Me.chkContinuos.Checked)
		ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then
			If mnuIniDirect.Checked And Me.chkContinuos.Checked And (lstDisp.Items.Count - 1) = ActualPlay.AUDIO_ListID And ExeDirecte = True Then
				
				' Assegura que s'iniciarà el programa MSCDirecte
				' Executa el programa directe al finalitzar la reproducció
				Try
					'TODO:Això peta, ja que al enviar la instrucció encara no s'ha engegat el programa ( si ja està engegat va bé)
					Dim p As System.Diagnostics.Process = New System.Diagnostics.Process()
					If mnuForceDirectMusic.Checked Then p.StartInfo.Arguments = "PLAY"
					'If mnuForceDirectMusic.Checked Then p.StartInfo.Arguments = "MUSIC"
					p.StartInfo.FileName = My.Application.Info.DirectoryPath & "\MSCDirecte.exe"
					p.Start()
					'Executa la instrucció a Play.
					Dim ctlRemot As New MSC.InstruccionsRemotes(getNomAplic(Aplicatius.PRG_DIRECTE))
					ctlRemot.ExecuteInstruc(ctlRemot.Id_Sessio, ServerInstruc.DIRECT_PLAY)
				Catch ex As Exception
				End Try
				Me.cmdTancar.Enabled = True
				Me.Close()
				Exit Sub
			ElseIf Not Me.mnuIniDirect.Checked Then
				'Poder sortir del programa
				ExeDirecte = False
				If NumCart = 1 Then
					Me.cmdTancar.Enabled = Not SomeFormPlay()	
				Else
					Me.cmdTancar.Enabled = True
				End If
				
				Me.mnuExit.Enabled = True				
			End If
			lbDisplayTitol.Text = Params.NomRadio
			cmdPlay.Image = Me.picPlay.Image
			'			'AutoLoadPause
			'			Dim listID As Integer = ActualPlay.AUDIO_ListID + 1
			'			If IsNothing(ActualPlay.AUDIO_Path) Then listID = 0			
			'			'si és un STOP (tag=100) ho descartem	
			'			static PlayInProcess As Boolean = False
			'			If PlayInProcess = False _
			'				AndAlso lstDisp.Items.Count > 0 _
			'				AndAlso listID <= (lstDisp.Items.Count - 1) _
			'				AndAlso lstDisp.Items.Item(listID).Tag <> 100 _ 
			'				AndAlso ManualStop = False Then
			'				'Beep
			'				PlayInProcess = True
			'				PlayPauseFitxer(listID, False,False, True)
			'				PlayInProcess = False 
			'			End If
		ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_STALLED Then
			lbDisplayTitol.Text = MSG_ERROR_PLAYER
			'Error al reproduïr
		End If
		' ensenya diferents INFOS al display
		Me.Text = StrNumCart & IIf(Len(Me.lbDisplayTitol.Text) > 31, VB.Left(Me.lbDisplayTitol.Text, 31) & "... - ", Me.lbDisplayTitol.Text & " - ") & VB.Right(TimeSerial(0, 0,ActualPlay.ElapseInSecons), 5)
		If ActualPlay.ElapseTime < 0 Then lbDisplayTitol.Text = MSG_ERROR_POSITION
		Me.lbHora.Text = Microsoft.VisualBasic.TimeOfDay.ToLongTimeString
		
	End Sub
	
	Private Sub tmr_Play_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tmr_Play.Tick
		
		If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then			
			Dim SegRest As Single = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.RemainTime)
			If Me.chkContinuos.Checked And AutoFader = True Then
				' Play quan està en el bucle automàtic:                                                 
				If lstDisp.Items.Count = 0 Then Exit Sub
				AutoFader = False
				'No inicia el bucle del llistat si hi ha mnuIniDirect.Checked
				If mnuIniDirect.Checked = True _
					And (lstDisp.Items.Count - 1) = ActualPlay.AUDIO_ListID _
					And ProxPlay.AUDIO_ListID = 0 Then Exit Sub
				
				'Si ha arribat al final del llistat i no s'ha de fer bucle: surt.
				If (lstDisp.Items.Count - 1) <= ActualPlay.AUDIO_ListID And mnuBucleList.Checked = False Then Exit Sub
				
				If mnuAutoDel.Checked Then
					Try
						lstDisp.Items.RemoveAt(ActualPlay.AUDIO_ListID )
						Me.flowBotons.Controls.RemoveAt(ActualPlay.AUDIO_ListID )							
						ProxPlay.AUDIO_ListID -= 1						
					Catch ex As Exception
					End Try
				End If
				
				Dim listID As Integer = ProxPlay.AUDIO_ListID
				If listID > (lstDisp.Items.Count - 1) Then listID = lstDisp.Items.Count - 1
				
				PlayPauseFitxer(listID, True,ManualEnd)
				
			ElseIf Me.chkLoop.Checked Then
				'Loop amb temps final modificat, si és tot el trac ja el automàtic: Bass.BASS_ChannelSetFlags(ActualHandle, BASSMusic.BASS_MUSIC_LOOP)
				If (LoopOut <= ActualPlay.ElapseTime) Then Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, LoopIn)
				AutoFader = False
			ElseIf Me.chkContinuos.Checked = False Or Me.chkLoop.Checked Then
				AutoFader = False
				Inc = 0
			ElseIf ActualPlay.ElapseTime < 0 Then
				'Error per durada superior a 3:22:00
				AutoFader = IIf(Bass.BASS_ChannelIsActive(ActualPlay.AUDIO_HANDLE) = BASSActive.BASS_ACTIVE_PLAYING, False, True)
			ElseIf ActualPlay.RemainTime < 123000 And ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING And ActualPlay.AUDIO_TipFitxer <> Tipus_Play.CTL_URL_STREAM Then
				'milisegons del últim segon
				AutoFader = True
				Inc = 0
			ElseIf Me.chkContinuos.Checked And  ActualPlay.ElapseInSecons > OutSegons And ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING And mnuAutoFader.Checked And ActualPlay.AUDIO_TipFitxer <> Tipus_Play.CTL_URL_STREAM Then				 
				AutoFader = True
				Inc = 0
			ElseIf SegRest <= SegActivate And ActualPlay.ElapseTime > 1 And VolMax < threshold And ActualPlay.AUDIO_TipFitxer <> mdlMscDefines.Tipus_Play.CTL_PUBLICITAT And ProxPlay.AUDIO_TipFitxer <> mdlMscDefines.Tipus_Play.CTL_PUBLICITAT And mnuAutoFader.Checked Then
				AutoFader = True
				Inc = 0
			End If
		ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then			
			AutoFader = False
		ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_STALLED Then
			'Error de reproducció
			AutoFader = True 'Això s'ha d'agafar amb pinces
			Inc = 0
		End If
		
		If IsNothing(PlayerPre)= True Then Exit Sub
		If PlayerPre.IsPlaying = True Then
			cmdPlayPre.Image = Me.picStop.Image
			Me.picPreEsc.Visible = True
		ElseIf PlayerPre.IsPlaying = False And PlayerPre.PreEscoltaHandle <> 0 Then
			PlayerPre.StopAudio()
			cmdPlayPre.Image = Me.picPlay.Image
			Me.picPreEsc.Visible = False
		Else
			cmdPlayPre.Image = Me.picPlay.Image
			Me.picPreEsc.Visible = False
		End If
		
	End Sub
	
	#End Region
	
	#Region "Altres"
	
	Private Sub lstDisp_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstDisp.DragDrop, flowBotons.DragDrop
		Dim liNew As System.Windows.Forms.ListViewItem
		Dim hitItem As System.Windows.Forms.ListViewItem
		If DragLVDisp IsNot Nothing And cartOrigenMove Is Me Then
			'Realitzem desplaçaments dins la mateixa llista            
			Dim I As Short
			Dim position As New Point(e.X, e.Y)
			position = lstDisp.PointToClient(position)
			
			hitItem = lstDisp.HitTest(position).Item
			If hitItem Is Nothing Then
				Exit Sub 
				'liNew = lstDisp.Items.Insert(lstDisp.Items.Count, DragLVDisp.Text)
			Else
				liNew = lstDisp.Items.Insert(hitItem.Index, DragLVDisp.Text)
				If (ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING) _
					And ActualPlay.AUDIO_ListID > DragLVDisp.Index _
					And ActualPlay.AUDIO_ListID <= hitItem.Index Then
					ActualPlay.AUDIO_ListID -= 1
					SetProxTrack(ActualPlay.AUDIO_ListID + 1)
				End If
			End If
			liNew.Tag = DragLVDisp.Tag
			liNew.ImageKey = DragLVDisp.ImageKey
			Do Until I = DragLVDisp.SubItems.Count
				If liNew.SubItems.Count > I Then
					liNew.SubItems(I).Text = DragLVDisp.SubItems(I).Text
				Else
					liNew.SubItems.Insert(I, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, DragLVDisp.SubItems(I).Text))
				End If
				liNew.SubItems.Item(I).Tag = DragLVDisp.SubItems.Item(I).Tag
				I = I + 1
			Loop
			lstDisp.Items.RemoveAt(DragLVDisp.Index)
			liNew.Selected = True
			DragLVDisp = Nothing
		ElseIf MoveInterPlayers = True Then
			'Desplacem d'una altre cartutxera
			If cartOrigenMove Is Me Then Exit Sub
			Dim listAudio(cartOrigenMove.lstDisp.SelectedItems.Count - 1) As ListAudioSelect
			For l As Short = 0 To cartOrigenMove.lstDisp.SelectedItems.Count - 1
				listAudio(l).AUDIO_Titol = cartOrigenMove.lstDisp.SelectedItems(l).Text
				listAudio(l).AUDIO_Path = cartOrigenMove.lstDisp.SelectedItems(l).SubItems.Item(1).Tag
				listAudio(l).AUDIO_TipFitxer = cartOrigenMove.lstDisp.SelectedItems(l).Tag
				listAudio(l).AUDIO_Durada = cartOrigenMove.lstDisp.SelectedItems(l).SubItems.Item(3).Text
				listAudio(l).AUDIO_ID = cartOrigenMove.lstDisp.SelectedItems(l).SubItems.Item(2).Tag
				listAudio(l).AUDIO_SubTitol = cartOrigenMove.lstDisp.SelectedItems(l).SubItems.Item(1).Text
				listAudio(l).AUDIO_BPM = cartOrigenMove.lstDisp.SelectedItems(l).SubItems.Item(5).Text
				listAudio(l).AUDIO_Radiat = False
				
			Next
			addElementlist(listAudio)
			'Borrar            
			Dim r As Short = cartOrigenMove.lstDisp.Items.Count - 1
			Do Until r < 0
				Try
					If cartOrigenMove.lstDisp.Items.Item(r).Selected Then cartOrigenMove.BorrarFitxer(r)
					r = r - 1
				Catch ex As Exception
					Exit Do
				End Try
			Loop
			calcTotDurationEnableBotons()
		ElseIf listAudioFromDBS IsNot Nothing Then
			'Desplacem de la base de dades
			addElementlist(listAudioFromDBS)
			listAudioFromDBS = Nothing
			Dim ErrLog As String = CType(e.Data.GetData(DataFormats.StringFormat), String)
			If ErrLog.Length > 1 Then MsgBox(ErrLog, MsgBoxStyle.Critical, MSG_ATENCIO)
			
		ElseIf MoveFromHistory = True Then
			'Desplacem del historial			
			Dim listAudio(frmHistory.lstDispHistory.SelectedItems.Count - 1) As ListAudioSelect
			For l As Short = 0 To frmHistory.lstDispHistory.SelectedItems.Count - 1
				listAudio(l).AUDIO_Titol = frmHistory.lstDispHistory.SelectedItems(l).Text
				listAudio(l).AUDIO_Path = frmHistory.lstDispHistory.SelectedItems(l).SubItems.Item(1).Tag
				listAudio(l).AUDIO_TipFitxer = frmHistory.lstDispHistory.SelectedItems(l).Tag
				listAudio(l).AUDIO_Durada = frmHistory.lstDispHistory.SelectedItems(l).SubItems.Item(3).Text
				listAudio(l).AUDIO_ID = frmHistory.lstDispHistory.SelectedItems(l).SubItems.Item(2).Tag
				listAudio(l).AUDIO_SubTitol = frmHistory.lstDispHistory.SelectedItems(l).SubItems.Item(1).Text
				listAudio(l).AUDIO_BPM = frmHistory.lstDispHistory.SelectedItems(l).SubItems.Item(5).Text
				listAudio(l).AUDIO_Radiat = False				
			Next
			addElementlist(listAudio)
			'Borrar            
			Dim r As Short = frmHistory.lstDispHistory.Items.Count - 1
			Do Until r < 0
				Try
					If frmHistory.lstDispHistory.Items.Item(r).Selected Then 
						Dim listItem As ListViewItem = frmHistory.lstDispHistory.Items(r)
						frmHistory.lstDispHistory.Items .Remove (listItem)
					End If
					r = r - 1
				Catch ex As Exception
					Exit Do
				End Try
			Loop
			
			calcTotDurationEnableBotons()
		ElseIf MoveFromExplorer = True Then
			MoveFromExplorer = False
			For r As Integer = 0 To ActualFrmExplorer.ListViewFitxers.SelectedItems.Count -1
				Dim file As String =  ActualFrmExplorer.ListViewFitxers.SelectedItems(r).Tag.ToString 
				LoadToList(file)
			Next
			
			calcTotDurationEnableBotons()
		Else
			'afegim fitxers exteriors
			Dim filenames As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())			
			For Each file As String In filenames
				LoadToList(file)
			Next
			calcTotDurationEnableBotons()
		End If
		If (ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED) And chkContinuos.Checked = True Then
			SetProxTrack(ActualPlay.AUDIO_ListID + 1)
		End If
		cartOrigenMove = Nothing
		MoveInterPlayers = False
		MoveFromHistory = False
	End Sub
	
	Private Sub lstDisp_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lstDisp.DragEnter, flowBotons.DragEnter
		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			Dim files() As String = e.Data.GetData(DataFormats.FileDrop, True)
			If UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "MP3" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "MP2" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "MP1" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "WAV" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "OGG" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 4)) = "AIFF" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "AIF" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "CDA" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "PTC" _
				Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "M3U" Then
				e.Effect = DragDropEffects.Copy
				DragLVDisp = Nothing
				Me.cmdPlay.AllowDrop = True
			Else
				'és un fitxer no reconegut per el programa    
				e.Effect = DragDropEffects.None
			End If
		Else
			'intern dels llistats
			e.Effect = DragDropEffects.Copy
		End If
	End Sub
	
	Private Sub lstDisp_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lstDisp.ItemDrag
		If e.Button = System.Windows.Forms.MouseButtons.Left Then			
			DoDragDrop(e.Item, DragDropEffects.Copy)
		End If
		
	End Sub
	
	Private Sub lstDisp_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles lstDisp.KeyDown
		If e.KeyData = Keys.Return And e.Control = False Then
			Try
				Dim IndexLlistat As Integer = lstDisp.FocusedItem.Index
				PlayPauseFitxer(IndexLlistat, True, True)
			Catch ex As Exception
				Exit Sub
			End Try
			
		End If
		If e.Control = True Then
			Select Case e.KeyData
				Case Keys.Up Or Keys.Control : Me.MoveListViewItem(Me.lstDisp, True)
				Case Keys.Down Or Keys.Control : Me.MoveListViewItem(Me.lstDisp, False)
			End Select
		End If
		
	End Sub
	
	Private Sub lstDisp_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstDisp.MouseDown
		If e.Button = System.Windows.Forms.MouseButtons.Left And e.Clicks = 1 Then
			DragLVDisp = CType(sender, ListView).GetItemAt(e.X, e.Y)
			Me.cmdBorrar.AllowDrop = True
			Me.cmdPlay.AllowDrop = True
			sender.DoDragDrop(sender, DragDropEffects.Copy)
			MoveInterPlayers = True
			cartOrigenMove = Me
		ElseIf e.Clicks = 2 Then
			'Fer un Play
			Try
				Dim IndexLlistat As Integer = lstDisp.FocusedItem.Index
				PlayPauseFitxer(IndexLlistat, True, True)
			Catch ex As Exception
				Exit Sub
			End Try
		End If
		Try
			If InfoMsg IsNot Nothing Then InfoMsg.Close()
		Catch ex As Exception
		End Try
		'DragLVDisp = Nothing
		'MoveInterPlayers = False
	End Sub
	
	Private Sub lstDisp_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lstDisp.MouseUp
		If e.Button = System.Windows.Forms.MouseButtons.Right Then
			Try
				If InfoMsg IsNot Nothing Then InfoMsg.Close()
			Catch ex As Exception
			End Try
		End If
		DragLVDisp = Nothing
		MoveInterPlayers = False
	End Sub
	
	Private Sub DragEnterListDBS(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
		If e.Data.GetDataPresent(DataFormats.FileDrop) Then
			e.Effect = DragDropEffects.None
		Else
			'intern dels llistats
			e.Effect = DragDropEffects.Copy
		End If
	End Sub
	
	Private Sub lbStrPith_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbStrPith.LinkClicked
		sldPith.Value = FreqDef
		canviPith()
	End Sub
	
	Private Sub sldPith_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sldPith.MouseWheel
		Try
			If e.Delta > 0 Then
				sldPith.Value = sldPith.Value + 1000
			Else
				sldPith.Value = sldPith.Value - 1000
			End If
		Catch ex As Exception
		End Try
		canviPith()
	End Sub
	
	Private Sub sldPith_Scroll(ByVal sender As Object, ByVal e As System.EventArgs) Handles sldPith.Scroll
		canviPith()
	End Sub
	
	Private Sub MicroDesplacament(ByRef Sumar As Boolean)
		If Sumar = True Then
			CuePosition = ActualPlay.ElapseTime + 0.005
		Else
			CuePosition = ActualPlay.ElapseTime - 0.003
		End If
		Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, CuePosition)
	End Sub
	
	Private Sub lbMicroDespMes_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbMicroDespMes.LinkClicked
		MicroDesplacament(True)
	End Sub
	
	Private Sub lbMicroDespMenys_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lbMicroDespMenys.LinkClicked
		MicroDesplacament(False)
	End Sub
	
	Private Sub optPlayLoop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
		If chkLoop.Checked = True Then
			Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_MUSIC_LOOP, BASSFlag.BASS_MUSIC_LOOP)
		Else
			Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_DEFAULT, BASSFlag.BASS_MUSIC_LOOP)
		End If
	End Sub
	
	Private Sub lbDisplayTitol_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbDisplayTitol.Click
		Invert = Not Invert
	End Sub
	
	Private Sub lbDisplayTime_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbDisplayTime.Click
		DisplayTemps += 1
		If DisplayTemps = DisplayTime.DISPLAY_TOTAL + 1 Then DisplayTemps = DisplayTime.DISPLAY_ELAPSE
	End Sub
	
	#End Region
	
	#Region "menus pantalla"
	
	Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
		Me.Close()
	End Sub
	
	Private Sub mnuLoadVar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLoadVar.Click
		LoadParams()
	End Sub
	
	Private Sub mnuIniDirect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIniDirect.Click
		mnuIniDirect.Checked = Not mnuIniDirect.Checked
		mnuForceDirectMusic.Enabled = mnuIniDirect.Checked
		IniDirecte()
	End Sub
	
	Friend Sub IniDirecte()
		picDirecte.Visible = mnuIniDirect.Checked
		If mnuIniDirect.Checked = True Then chkContinuos.Checked = True
		lbInfo.Text = mnuIniDirect.Text
		lbInfo.Visible = mnuIniDirect.Checked
	End Sub
	
	Private Sub mnuAutoDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAutoDel.Click
		
		mnuAutoDel.Checked = Not mnuAutoDel.Checked		
	End Sub
	
	Private Sub mnuAutoFader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAutoFader.Click
		mnuAutoFader.Checked = Not mnuAutoFader.Checked
	End Sub
	
	Private Sub mnuBotons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBotons.Click
		mnuBotons.Checked = Not mnuBotons.Checked
		Me.flowBotons.Visible = mnuBotons.Checked		
		Me.PanelListDisp.Visible = Not flowBotons.Visible
		Me.chkContinuos.Enabled = Not flowBotons.Visible
		AutomàticaToolStripMenuItem.Enabled = Not flowBotons.Visible
		If flowBotons.Visible Then Me.chkContinuos.Checked = False
		If flowBotons.Visible Then InxfrmOnBotonera = NumCart 
		'Me.cmdMenyGranBotons.Visible = flowBotons.Visible
		'Me.cmdMesGranBotons.Visible = flowBotons.Visible
		'Me.lbTamanyBotons.Visible = flowBotons.Visible
		Try
			flowBotons.Controls(0).Focus()
		Catch ex As Exception
		End Try
	End Sub
	
	Private Sub mnuSaveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveFile.Click
		salvarLlistat()
	End Sub
	
	Private Sub mnuHelpInternet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpInternet.Click
		Dim Proces As Process = New Process
		Try
			Process.Start(MSC.Help.WEB_DEF_MSC.ToString)
		Catch ex As Exception
			
		End Try
		
	End Sub
	
	Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
		Try
			frmSplash.Show(Me)
		Catch ex As Exception
		End Try
	End Sub
	
	Private Sub mnuDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDel.Click
		Click_Borrar()
	End Sub
	
	Private Sub mnuHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelp.Click
		Dim Proces As Process = New Process
		Try
			Process.Start(MSC.Help.HELP_CARTUTXERES.ToString)
		Catch ex As Exception
		End Try
	End Sub
	
	Private Sub mnuOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpenFile.Click
		LoadAllFitxers()
	End Sub
	
	Private Sub mnuPlayPause_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPlayPause.Click
		If lstDisp.Items.Count = 0 Then Exit Sub
		Dim IndexLlistat As Integer
		If lstDisp.FocusedItem Is Nothing And lstDisp.Items.Count Then
			IndexLlistat = 0
		Else
			IndexLlistat = lstDisp.FocusedItem.Index
		End If
		
		PlayPauseFitxer(IndexLlistat)
	End Sub
	
	Private Sub mnuStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuStop.Click
		StopFitxer()
	End Sub
	
	Private Sub mnuLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLoad.Click
		LoadAllFitxers()
	End Sub
	
	Private Sub AutomàticaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutomàticaToolStripMenuItem.Click
		chkContinuos.Checked = Not chkContinuos.Checked
		AutomàticaToolStripMenuItem.Checked = chkContinuos.Checked
		If chkContinuos.Checked Then LoopToolStripMenuItem.Checked = False
	End Sub
	
	Private Sub LoopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoopToolStripMenuItem.Click
		LoopToolStripMenuItem.Checked = Not LoopToolStripMenuItem.Checked
		chkLoop.Checked = LoopToolStripMenuItem.Checked
		If chkLoop.Checked = True Then chkContinuos.Checked = False
	End Sub
	
	Private Sub mnuAutoSincro59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAutoSincro59.Click, mnuAutoSincro29.Click, mnuAutoSincroXX.Click, mnuAutoSincro00.Click
		
		bl_AutoSinc = Not  CType(sender, ToolStripMenuItem).Checked		
		mnuAutoSincroXX.Checked = False
		mnuAutoSincro29.Checked = False					
		mnuAutoSincro59.Checked = False
		mnuAutoSincro00.Checked = False
		
		If bl_AutoSinc = True Then			
			Dim HoraEnd As Date
			Dim seg_he As Integer 
			Dim seg_total As Integer 
			TotalDuration = calcTotalDurationList()	
			seg_total = TotalDuration
			seg_he = 60 - SegSH	
			If mnuAutoSincroEndPlay.Checked = True Then											
				Select Case CType(sender, ToolStripMenuItem).Name
					Case "mnuAutoSincro59"
						TimeToStart = TimeSerial(TimeOfDay.Hour, 59, seg_he - seg_total)					
						HoraEnd = TimeSerial(TimeOfDay.Hour, 59,seg_he )
						mnuAutoSincro59.Checked = True						
					Case "mnuAutoSincro00"
						TimeToStart = TimeSerial(TimeOfDay.Hour+1,00,- seg_total)					
						HoraEnd = TimeSerial(TimeOfDay.Hour+1, 00,00 )
						mnuAutoSincro00.Checked = True
					Case "mnuAutoSincro29"
						TimeToStart = TimeSerial(TimeOfDay.Hour, 29, seg_he - seg_total)					
						HoraEnd = TimeSerial(TimeOfDay.Hour, 29, seg_he)
						mnuAutoSincro29.Checked = True
					Case "mnuAutoSincroXX"	
						Dim DataSincroEnd As Long
						Try
							frmSincro.ShowDialog()
							If frmSincro.DialogResult = System.Windows.Forms.DialogResult.OK Then
								HoraEnd = CDate(frmSincro.txtHoraEnd.Value)
								DataSincroEnd = SecondDec(frmSincro.txtHoraEnd.Value)
								TimeToStart = TimeSerial(0, 0, DataSincroEnd - seg_total)	
								mnuAutoSincroXX.Checked = True
							Else
								TimeToStart = TimeOfDay.AddMinutes (-1)							
							End If
						Catch ex As Exception
							TimeToStart = TimeOfDay.AddMinutes (-1)							
						End Try							
				End Select
				If (TimeToStart < TimeOfDay) Then
					'fas tard per sincronitzar				
					MsgBox(String.Format(MSG_ERROR_DURADA_SINCRO,TimeSerial(0, 0, TotalDuration).ToString("HH:mm:ss")), MsgBoxStyle.Critical, MSG_ATENCIO)				
					bl_AutoSinc = False
					lbAutoSinc.BackColor = Color.Lime
					mnuAutoSincro59.Checked = False
					mnuAutoSincro29.Checked = False
					mnuAutoSincroXX.Checked = False
					mnuAutoSincro00.Checked = False
					mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00"
				End If					
			Else				
				Select Case CType(sender, ToolStripMenuItem).Name					
					Case "mnuAutoSincro00"
						TimeToStart = TimeSerial(TimeOfDay.Hour+1,00, 0)					
						HoraEnd = TimeSerial(TimeOfDay.Hour+1, 00,seg_total )
						mnuAutoSincro00.Checked = True
					Case "mnuAutoSincroXX"						
						Dim DataSincroEnd As Long
						Try
							Dim frmSync As New frmSincro
							frmSync.isEnd = False							
							frmSync.ShowDialog()							
							If frmSync.DialogResult = System.Windows.Forms.DialogResult.OK Then
								Dim horafrm As Datetime = CDate(frmSync.txtHoraEnd.Value)
								TimeToStart = TimeSerial(horafrm.Hour, horafrm.Minute,horafrm.Second)
								DataSincroEnd = SecondDec(frmSync.txtHoraEnd.Value)
								HoraEnd = TimeSerial(0, 0, DataSincroEnd + seg_total)	
								mnuAutoSincroXX.Checked = True
							Else
								TimeToStart = TimeOfDay.AddMinutes (-1)							
							End If
							frmSync= Nothing
						Catch ex As Exception
							TimeToStart = TimeOfDay.AddMinutes (-1)							
						End Try													
				End Select
			End If						
			mnuAutoFader.Checked = False
			chkContinuos.Checked = True
			mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": " & TimeToStart.ToString("HH:mm:ss")
			lbInfo.Text = LABEL_H_INI & ": " & TimeToStart.ToString("HH:mm:ss") & "/ " & LABEL_H_END & ": " & HoraEnd.ToString("HH:mm:ss")			
		Else
			mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00"
		End If
		lbInfo.Visible = bl_AutoSinc
		
	End Sub
	
	
	#End Region
	
	#Region "Funcions Intèrnes"
	
	Private Sub SincroPlay(Start As Boolean)
		
	End Sub
	
	'paràmetres Inicials
	Private Sub LoadParams()
		DisplayTemps = DisplayTime.DISPLAY_REMAIN
		
		Dim SenyalHora As String = Params.PathSenyalHora
		Dim LocalHandLe As Integer = Bass.BASS_StreamCreateFile(SenyalHora, 0, 0, 0)
		If LocalHandLe <> 0 Then
			Dim txtHora As Date
			Dim Lens As Long = Bass.BASS_ChannelGetLength(LocalHandLe) 'Stream Length
			SegSH = Bass.BASS_ChannelBytes2Seconds(LocalHandLe, Lens)
			txtHora = TimeSerial(0, 59, 60 - SegSH)
			mnuAutoSincro59.Text = " HH:" & txtHora.ToString("mm:ss")
			txtHora = TimeSerial(0, 29, 60 - SegSH)
			mnuAutoSincro29.Text = " HH:" & txtHora.ToString("mm:ss")
		End If
		
		'Carrèga els valors del autofader -----------------------------------
		threshold = Params.Threshold
		Atack = Params.Attack
		VolNormalice = Params.VolNormalize
		VolIni = Params.VolIni
		SegActivate = Params.SegActivate
		
	End Sub
	
	'Carrèga listats
	Friend Sub LoadToList(ByVal Path As String)
		Dim Title As String = ""
		Dim interp As String = ""
		Dim Duration As Date
		Dim BPM As Single = 0
		Dim Handle As Integer
		Dim tLength As Single
		Dim lenTrack As Long
		Dim TI As New Un4seen.Bass.AddOn.Tags.TAG_INFO
		
		If UCase(Microsoft.VisualBasic.Right(Path, 3)) = "MP3" _
			Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "MP2" _
			Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "MP1" _
			Or UCase(Microsoft.VisualBasic.Right(Path, 4)) = "AIFF" _
			Or UCase(Microsoft.VisualBasic.Right(Path, 4)) = "AIF" _
			Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "WAV" _
			Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "OGG" Then
			Handle = Bass.BASS_StreamCreateFile(Path, 0, 0, BASSFlag.BASS_DEFAULT)
			If (BassTags.BASS_TAG_GetFromFile(Handle, TI)) Then
				interp = TI.artist
				Title = TI.title
			End If
			If Title.Length = 0 Then Title = GetFileName(Path)
			If interp.Length = 0 Then interp = Path
			lenTrack = Bass.BASS_ChannelGetLength(Handle)
			tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
			Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
			If mnuCalcBPMOnLoad.Checked = True Then BPM = getBPMFitxer(Path, Me.Handle) 			
			addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Title, interp, Path, 0, Duration, #12:00:00 AM#,BPM)
			Bass.BASS_StreamFree(Handle)
		ElseIf UCase(Microsoft.VisualBasic.Right(Path, 3)) = "PTC" Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "M3U" Then
			CarregaLListat(Path)
		ElseIf UCase(Microsoft.VisualBasic.Right(Path, 3)) = "CDA"
			Handle = BassCd.BASS_CD_StreamCreateFile(Path, BASSFlag.BASS_STREAM_AUTOFREE )
			Title = GetFileName(Path)
			interp = Path
			lenTrack = Bass.BASS_ChannelGetLength(Handle)
			tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
			Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
			If mnuCalcBPMOnLoad.Checked = True Then BPM = getBPMFitxer(Path, Me.Handle) 			
			addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Title, interp, Path, 0, Duration, #12:00:00 AM#,BPM)
			Bass.BASS_StreamFree(Handle)
		End If
	End Sub
	
	Private Sub LoadAllFitxers()
		Dim i As Integer
		With CommonDialog1
			.FileName = ""
			.Title = COMMONDIALOG1_TITLE
			.Multiselect = True
			.ShowReadOnly = False 'Falgs, allows Multi select, Explorer style and hide the Read only tag            
			.Filter =  LIST_FILTER_FILES & " MP3 (*.mp3) |*.mp3; |" & _
				LIST_FILTER_FILES & " MP2 (*.mp2) |*.mp2; |" & _	
				LIST_FILTER_FILES & " MP1 (*.mp1) |*.mp1; |" & _		
				LIST_FILTER_FILES & " Wave (*.wav) |*.wav; |" & _
				LIST_FILTER_FILES & " CD Audio (*.cda) |*.CDA; |" & _
				LIST_FILTER_FILES & " Ogg (*.ogg)|*.ogg; |" & _
				LIST_FILTER_FILES & " Aiff (*.aiff)|*.aiff; |" & _				
				LIST_FILTER_FILES & " Aif (*.aif)|*.aif; |" & _				
				LIST_FILTER_CART & " (*.ptc)|*.ptc; |" & _
				LIST_FILTER_WINAMP &" (*.m3u) |*.m3u"
			.ShowDialog()
			
			For i = 0 To .FileNames.Length - 1
				CarregaFitxer(.FileNames(i))
			Next
		End With
	End Sub
	
	Friend Sub CarregaFitxer(ByRef path As String)
		Dim TI As New Un4seen.Bass.AddOn.Tags.TAG_INFO
		Dim Handle As Integer
		Dim tLength As Single
		Dim lenTrack As Long
		Dim Title As String = ""
		Dim interp As String = ""
		Dim Duration As Date
		Dim BPM As Single = 0
		
		If UCase(Microsoft.VisualBasic.Right(path, 3)) = "MP3" _
			Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "MP1" _
			Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "MP2" _
			Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "WAV" _
			Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "WMA" _
			Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "OGG" _
			Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "AIF" _
			Or UCase(Microsoft.VisualBasic.Right(path, 4)) = "AIFF" Then
			
			Handle = Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT)
			If (BassTags.BASS_TAG_GetFromFile(Handle, TI)) Then
				interp = TI.artist
				Title = TI.title
			End If			
			If Len(Title) = 0 Then Title = GetFileName(path)
			If Len(interp) = 0 Then interp = GetFileName(path)
			lenTrack = Bass.BASS_ChannelGetLength(Handle)
			tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
			Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
			If mnuCalcBPMOnLoad.Checked = True Then BPM = getBPMFitxer(Path, Me.Handle) 
			addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Title, interp, path, 0, Duration, #12:00:00 AM#,BPM)
			Bass.BASS_StreamFree(Handle)
		ElseIf UCase(Microsoft.VisualBasic.Right(path, 3)) = "PTC" Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "M3U" Then
			CarregaLListat(path)
		ElseIf UCase(Microsoft.VisualBasic.Right(Path, 3)) = "CDA"
			Handle = BassCd.BASS_CD_StreamCreateFile(Path, BASSFlag.BASS_STREAM_AUTOFREE )
			Title = GetFileName(Path)
			interp = Path
			lenTrack = Bass.BASS_ChannelGetLength(Handle)
			tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
			Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
			If mnuCalcBPMOnLoad.Checked = True Then BPM = getBPMFitxer(Path, Me.Handle) 
			addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Title, interp, Path, 0, Duration, #12:00:00 AM#,BPM)
			Bass.BASS_StreamFree(Handle)
		End If
	End Sub
	
	Private Sub CarregaLListat(ByRef sNomFitxer As String, Optional ByRef InitExe As Boolean = False)
		Dim Tipus As Short
		Dim id As Integer = 0
		Dim Titol As String = ""
		Dim SubTitol As String = ""
		Dim Durada As Date
		Dim StrPath As String = ""
		Dim NumFitxer As Short
		Dim NumParts As Short
		Dim ValorsLLista() As ListAudioSelect = Nothing
		Dim MaxVal As Short
		
		NumParts = 1
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
		'Si la llista està invisible (Presentació de botons)
		'Controla la visió de la botonera
		
		Me.mnuBotons.Checked = False
		' Buida la llista
		'Me.lstDisp.Items.Clear()
		
		NumFitxer = FreeFile()
		FileOpen(NumFitxer, sNomFitxer, OpenMode.Input)
		If Microsoft.VisualBasic.Right(sNomFitxer, 3).ToLower = "ptc" Then
			Dim Versio As String = LineInput(NumFitxer)
			If Versio.ToUpper = "V:2" Then
				Do
					'recupera tots els valors del fitxer ptc
					Input(NumFitxer, Titol)
					Input(NumFitxer, SubTitol)
					Input(NumFitxer, Durada)
					Input(NumFitxer, Tipus)
					Input(NumFitxer, id)
					Input(NumFitxer, StrPath)
					If OKFitxerToPlay(StrPath, Durada) Or Tipus = 200 Then
						ReDim Preserve ValorsLLista(MaxVal)
						ValorsLLista(MaxVal).AUDIO_TipFitxer = Tipus
						ValorsLLista(MaxVal).AUDIO_ID = id
						ValorsLLista(MaxVal).AUDIO_Durada = Durada
						ValorsLLista(MaxVal).AUDIO_Titol = Titol
						ValorsLLista(MaxVal).AUDIO_SubTitol = SubTitol
						ValorsLLista(MaxVal).AUDIO_Path = StrPath
						MaxVal = MaxVal + 1
					End If
				Loop Until EOF(NumFitxer)
				addElementlist(ValorsLLista)
			End If
		ElseIf Microsoft.VisualBasic.Right(sNomFitxer, 3).ToLower = "m3u" Then
			Dim TI As New Un4seen.Bass.AddOn.Tags.TAG_INFO
			Do
				Input(NumFitxer, StrPath)
				If OKFitxerToPlay(StrPath, Durada) Then
					Dim AudioHandle As Integer = Bass.BASS_StreamCreateFile(StrPath, 0, 0, BASSFlag.BASS_DEFAULT)
					If (BassTags.BASS_TAG_GetFromFile(AudioHandle, TI)) Then
						SubTitol = TI.artist
						Titol = TI.title
					End If
					If Titol.Length = 0 Then Titol = GetFileName(StrPath)
					If SubTitol.Length = 0 Then SubTitol = StrPath
					
					ReDim Preserve ValorsLLista(MaxVal)
					ValorsLLista(MaxVal).AUDIO_TipFitxer = mdlMscDefines.Tipus_Play.CTL_SISTEMA
					ValorsLLista(MaxVal).AUDIO_ID = 0
					ValorsLLista(MaxVal).AUDIO_Durada = Durada
					ValorsLLista(MaxVal).AUDIO_Titol = Titol
					ValorsLLista(MaxVal).AUDIO_SubTitol = SubTitol
					ValorsLLista(MaxVal).AUDIO_Path = StrPath
					MaxVal = MaxVal + 1
				End If
			Loop Until EOF(NumFitxer)
			addElementlist(ValorsLLista)
		End If
		FileClose(NumFitxer)
		lbInfo.Text = "Llistat: " & sNomFitxer
		lbInfo.Visible = True
		If InitExe Then
			Me.lstDisp.Focus()
			Me.chkContinuos.Checked = True
			PlayPauseFitxer(0)
			mnuIniDirect.Checked = True
			Me.picDirecte.Visible = True
		End If
		TotalDuration = calcTotalDurationList()
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		lstDisp.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
	End Sub
	
	
	'	Private Function LoadM3U(ByVal strFileName As String, ByRef strFilePaths() As String, ByRef strNames() As String) As Boolean
	'		
	'		'Error handler
	'		'On Error GoTo ErrHap
	'		
	'		'Declare variables
	'		Dim lngFileNo As Long
	'		Dim strTemp As String
	'		Dim i As Long
	'		Dim strLines() As String
	'		Dim lngLines As Long
	'		Dim strM3ULoc As String
	'		
	'		'Check if file exists
	'		If Dir(strFileName) <> "" Then
	'			
	'			'Get M3U location
	'			strM3ULoc = Left(strFileName, InStrRev(strFileName, "\"))
	'			
	'			'Get new file number
	'			lngFileNo = FreeFile
	'			
	'			'Open the file
	'			Open strFileName For Input As lngFileNo
	'				
	'				'Get the file
	'				strTemp = Input(LOF(lngFileNo), #lngFileNo)
	'					
	'					Close lngFileNo
	'				
	'				'Split the file into it's lines
	'				strLines = Split(strTemp, vbCrLf)
	'				
	'				'Check that this file has enough lines
	'				If UBound(strLines) > 2 Then
	'					
	'					'Check that it's an M3U file
	'					If strLines(0) = "#EXTM3U" Then
	'						
	'						'Get number of lines
	'						lngLines = UBound(strLines)
	'						
	'						'Attention! If you have any errors over the next 2 lines then you need to make sure
	'						'that you have declared the array variables without specifying their size,
	'						'because here we're changing their sizes to match.  - Thanks
	'						ReDim strFilePaths(0 To (lngLines / 2)) As String
	'						ReDim strNames(0 To (lngLines / 2)) As String
	'						
	'						'Loop through each line
	'						For i = 1 To lngLines
	'							
	'							'Check what kind of data we've got
	'							If Left$(strLines(i), 7) = "#EXTINF" Then
	'								
	'								'File name & length (but we don't return that). Get file name
	'								strNames((i - 1) / 2) = Right$(strLines(i), Len(strLines(i)) - InStr(1, strLines(i), ","))
	'								
	'							Else
	'								'File path. Verify the path
	'								If Dir(strLines(i)) <> "" Then
	'									'Pure path, including drive letter
	'									strFilePaths((i / 2) - 1) = strLines(i)
	'								ElseIf Dir(strM3ULoc & strLines(i)) <> "" Then
	'									'Adding onto the M3U's path (most common)
	'									strFilePaths((i / 2) - 1) = strM3ULoc & strLines(i)
	'								ElseIf Dir(Left$(strM3ULoc, 3) & strLines(i)) <> "" Then
	'									'Adding onto the M3U's drive only
	'									strFilePaths((i / 2) - 1) = Left$(strM3ULoc, 3) & strLines(i)
	'								Else
	'									'Display error message
	'									Call MsgBox("Cannot find file!" & vbCrLf & strLines(i), vbExclamation, "Error while loading a file!")
	'								End If
	'							End If
	'							
	'						Next i
	'						
	'						'Set return value to true
	'						LoadM3U = True
	'						
	'					End If
	'					
	'				Else
	'					'Return error
	'					LoadM3U = False
	'				End If
	'				
	'			Else
	'				'Return error
	'				LoadM3U = False
	'			End If
	'			
	'			Exit Function
	
	Friend Sub addElementlist(ByVal ListAudio() As ListAudioSelect)
		Dim ElementX As System.Windows.Forms.ListViewItem
		Dim subElementoX As System.Windows.Forms.ListViewItem.ListViewSubItem ' subelement        
		
		ManualStop = False
		AutoFader = False
		Dim BotoTrack As Button
		Dim NumAdd As Integer = 0
		Dim BotWidth As Integer = 180
		Dim BotHeight As Integer = 30
		If Me.flowBotons.Controls.Count > 0 Then
			BotWidth = Me.flowBotons.Controls(0).Width
			BotHeight = Me.flowBotons.Controls(0).Height
		End If
		'Si està en play/pause i en continu mirar que no afecte el pròxin track
		For i As Integer = 0 To ListAudio.Length - 1
			NumAdd = NumAdd + 1
			BotoTrack = New Button
			BotoTrack.Parent = Me.flowBotons
			BotoTrack.Width = BotWidth
			BotoTrack.Height = BotHeight
			BotoTrack.ImageAlign = ContentAlignment.MiddleLeft
			BotoTrack.AllowDrop = True
			ElementX = Me.lstDisp.Items.Add("")
			Select Case ListAudio(i).AUDIO_TipFitxer
				Case Tipus_Play.CTL_PROMOS
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(1)
					BotoTrack.Image = Me.ImageList.Images.Item(1)
				Case Tipus_Play.CTL_MUSICA
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(0)
					BotoTrack.Image = Me.ImageList.Images.Item(0)
				Case Tipus_Play.CTL_PUBLICITAT
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(2)
					BotoTrack.Image = Me.ImageList.Images.Item(2)
				Case Tipus_Play.CTL_JINGELS
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(3)
					BotoTrack.Image = Me.ImageList.Images.Item(3)
				Case Tipus_Play.CTL_PROGRAMA
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(4)
					BotoTrack.Image = Me.ImageList.Images.Item(4)
				Case Tipus_Play.CTL_AUDIO_USR
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(5)
					BotoTrack.Image = Me.ImageList.Images.Item(5)
				Case Else
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(5)
					BotoTrack.Image = Me.ImageList.Images.Item(5)
			End Select
			
			ElementX.Text = ListAudio(i).AUDIO_Titol
			ElementX.Tag = ListAudio(i).AUDIO_TipFitxer
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Text = ListAudio(i).AUDIO_SubTitol
			subElementoX.Tag = ListAudio(i).AUDIO_Path
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Tag = ListAudio(i).AUDIO_ID
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Text = ListAudio(i).AUDIO_Durada.ToString("HH:mm:ss")
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Text = ListAudio(i).AUDIO_HoraRadi.ToString("HH:mm:ss")
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Text = ListAudio(i).AUDIO_BPM.ToString
			
			BotoTrack.Text = ListAudio(i).AUDIO_Titol
			Me.ToolTipInfo.SetToolTip(BotoTrack, ListAudio(i).AUDIO_SubTitol)
			'S'ha d'afegir els Events als botons
			AddHandler BotoTrack.MouseDown, AddressOf MouseDownBotoTack
			AddHandler BotoTrack.Click, AddressOf ClickBotoTack
			If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_STOPPED AndAlso lstDisp.Items.Count = 1 Then
				PlayPauseFitxer(0, False,False, True)	
			End If
		Next i
		If (ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED) And Me.chkContinuos.Checked = True Then
			SetProxTrack(ActualPlay.AUDIO_ListID + 1)
		End If
		
		calcTotDurationEnableBotons()
	End Sub
	
	Private Sub calcTotDurationEnableBotons()
		TotalDuration = calcTotalDurationList()
		Me.cmdBorrar.Enabled = CBool(lstDisp.Items.Count > 0)
		Me.cmdSalvar.Enabled = CBool(lstDisp.Items.Count > 0)
		Me.mnuDel.Enabled = CBool(lstDisp.Items.Count > 0)
		Me.mnuSaveFile.Enabled = CBool(lstDisp.Items.Count > 0)
		mnuAutoSincroMare.Enabled = CBool(lstDisp.Items.Count > 0)
		If STANDALONE = False Then Me.cmdSaveLoop.Enabled = CBool(lstDisp.Items.Count > 0)
	End Sub
	
	Friend Function addElementlist(ByRef Tipus As Short, ByRef Titol As String _
		, ByRef SubTitol As String, ByRef PathFitxer As String, ByRef id As Integer _
		, ByRef Durada As Date, Optional ByRef HoraRadi As Date = #12:00:00 AM#,optional BPM As Single	= 0) As Boolean
		Dim ElementX As System.Windows.Forms.ListViewItem
		Dim subElementoX As System.Windows.Forms.ListViewItem.ListViewSubItem ' subelement
		
		ManualStop = False
		
		AutoFader = False
		Dim BotoTrack As Button
		If IO.File.Exists(PathFitxer)= True Or Tipus= 100 Or Tipus= 200 Then
			BotoTrack = New Button
			BotoTrack.Parent = Me.flowBotons
			Dim BotWidth As Integer = 180
			Dim BotHeight As Integer = 30
			If Me.flowBotons.Controls.Count > 0 Then
				BotWidth = Me.flowBotons.Controls(0).Width
				BotHeight = Me.flowBotons.Controls(0).Height
			End If
			BotoTrack.Width = BotWidth
			BotoTrack.Height = BotHeight
			BotoTrack.ImageAlign = ContentAlignment.MiddleLeft
			BotoTrack.AllowDrop = True
			
			
			ElementX = Me.lstDisp.Items.Add("")
			Select Case Tipus
				Case Tipus_Play.CTL_PROMOS
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(1)
					BotoTrack.Image = Me.ImageList.Images.Item(1)
				Case Tipus_Play.CTL_MUSICA
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(0)
					BotoTrack.Image = Me.ImageList.Images.Item(0)
				Case Tipus_Play.CTL_PUBLICITAT
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(2)
					BotoTrack.Image = Me.ImageList.Images.Item(2)
				Case Tipus_Play.CTL_JINGELS
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(3)
					BotoTrack.Image = Me.ImageList.Images.Item(3)
				Case Tipus_Play.CTL_PROGRAMA
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(4)
					BotoTrack.Image = Me.ImageList.Images.Item(4)					
				Case Tipus_Play.CTL_META_STOP ' Stop
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(12)
					BotoTrack.Image = Me.ImageList.Images.Item(12)
				Case Tipus_Play.CTL_URL_STREAM 'URL Streaming
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(5)
					BotoTrack.Image = Me.ImageList.Images.Item(5)					
				Case Else 'mdlMscDefines.Tipus_Play.CTL_AUDIO_USR
					ElementX.ImageKey = Me.ImageList.Images.Keys.Item(5)
					BotoTrack.Image = Me.ImageList.Images.Item(5)
			End Select
			ElementX.Text = Titol
			ElementX.Tag = Tipus
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Text = SubTitol
			subElementoX.Tag = PathFitxer
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Tag = id
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Text = Durada.ToString("HH:mm:ss")
			subElementoX = ElementX.SubItems.Add("")
			subElementoX.Text = HoraRadi.ToString("HH:mm:ss")
			subElementoX = ElementX.SubItems.Add("")			
			subElementoX.Text = CInt(BPM).ToString
			
			BotoTrack.Text = Titol
			Me.ToolTipInfo.SetToolTip(BotoTrack, SubTitol)
			'S'ha d'afegir els Events als botins
			AddHandler BotoTrack.MouseDown, AddressOf MouseDownBotoTack
			AddHandler BotoTrack.Click, AddressOf ClickBotoTack
			Me.cmdBorrar.Enabled = True
			Me.cmdSalvar.Enabled = True
			Me.mnuDel.Enabled = True
			Me.mnuSaveFile.Enabled = True
			mnuAutoSincroMare.Enabled = True
			Me.cmdSaveLoop.Enabled = False
			
			If (ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED) And Me.chkContinuos.Checked = True Then
				SetProxTrack(ActualPlay.AUDIO_ListID + 1)
				calcTotalDurationList()
			End If
			If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_STOPPED AndAlso lstDisp.Items.Count = 1 Then
				PlayPauseFitxer(0, False,False, True)	
			End If
			Return True
		Else
			Return False
		End If
	End Function
	
	Private Sub BorrarFitxer(ByRef idList As Short)
		Try
			'si esborrem el que està carregat s'ha de descarregar.
			If ActualPlay.AUDIO_ListID = idList AndAlso  ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then BorrarWaveForn()
			If ActualPlay.AUDIO_ListID = idList AndAlso  ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then StopFitxer()
			Me.lstDisp.Items.RemoveAt((idList))
			Me.flowBotons.Controls.RemoveAt(idList)
			
			If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
				If idList <= ActualPlay.AUDIO_ListID  Then	ActualPlay.AUDIO_ListID -= 1									
				If chkContinuos.Checked = True Then SetProxTrack(ActualPlay.AUDIO_ListID + 1)	
			End If
			
			TotalDuration = calcTotalDurationList()
		Catch
		Finally
			Me.cmdBorrar.Enabled = IIf(lstDisp.Items.Count > 0, True, False)
			Me.cmdSalvar.Enabled = cmdBorrar.Enabled
			mnuDel.Enabled = cmdBorrar.Enabled
			cmdBorrar.Enabled = cmdBorrar.Enabled
			mnuAutoSincroMare.Enabled = cmdBorrar.Enabled
			Me.mnuSaveFile.Enabled = cmdBorrar.Enabled
		End Try							
	End Sub
	
	'Player
	Friend Sub PlayCue(optional SetNewCue As Boolean= False )
		
		If Me.lstDisp.Items.Count = 0 Then Exit Sub
		Me.cmdCue.Image = Me.picCueOn .Image
		PlayerPre.NumCart =  NumCart
		Bass.BASS_SetDevice(PlayerPre.DeviceID )		
		' SI ESTÀ EN PAUSE S'ESTABLEIX el punt de cue
		'If CuePress = False Then PintaCue()
		If Bass.BASS_ChannelIsActive(ActualPlay.AUDIO_HANDLE)= BASSActive.BASS_ACTIVE_PLAYING Then
			Bass.BASS_ChannelPause(ActualPlay.AUDIO_HANDLE)
		End If
		
		Select Case Bass.BASS_ChannelIsActive(PlayerPre.PreEscoltaHandle)
			Case BASSActive.BASS_ACTIVE_STOPPED
				
				Dim path As String = ""
				Try
					Dim IndexLlistat As Integer = findIndexActualPlay()
					path = Me.lstDisp.Items.Item(IndexLlistat).SubItems.Item(1).Tag
				Catch ex As Exception
					path = Me.lstDisp.Items.Item(0).SubItems.Item(1).Tag
				End Try
				Select Case UCase(IO.Path.GetExtension(path))
					Case ".CDA"
						PlayerPre.PreEscoltaHandle = BassCd.BASS_CD_StreamCreateFile(path, BASSFlag.BASS_STREAM_AUTOFREE )
					Case Else ' .mp3 .wav
						PlayerPre.PreEscoltaHandle = Bass.BASS_StreamCreateFile(path, 0, 0,  BASSFlag.BASS_STREAM_AUTOFREE)
				End Select
				
				If Bass.BASS_ChannelSetPosition(PlayerPre.PreEscoltaHandle, Bass.BASS_ChannelBytes2Seconds(PlayerPre.PreEscoltaHandle, CuePosition)) = False Then
					Dim errBass As BASSError = Bass.BASS_ErrorGetCode  
				End If
				Bass.BASS_ChannelPlay(PlayerPre.PreEscoltaHandle, False)
				cmdPlay.Image = Me.picPause.Image	
				
			Case BASSActive.BASS_ACTIVE_PAUSED
				' si el punt ja està establert es fa un play
				Bass.BASS_ChannelPlay(PlayerPre.PreEscoltaHandle, False)
				Bass.BASS_ChannelSetPosition(PlayerPre.PreEscoltaHandle, Bass.BASS_ChannelBytes2Seconds(PlayerPre.PreEscoltaHandle, CuePosition))				
				
				cmdPlay.Image = Me.picPause.Image
			Case BASSActive.BASS_ACTIVE_PLAYING
				If  SetNewCue = True Then
					lbCue.BackColor = Color.Red
					CuePosition = Bass.BASS_ChannelGetPosition(PlayerPre.PreEscoltaHandle)
					If CuePosition = -1 Then 
						CuePosition = 0
					End If
					Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, CuePosition))
					WF.AddMarker("CUE", Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, CuePosition))
					DrawWave()
				End If
				
		End Select
	End Sub
	
	
	Friend Sub StopCue()
		If Me.lstDisp.Items.Count = 0 Then Exit Sub
		'Bass.BASS_SetDevice(DEV_PLAY)
		
		'Bass.BASS_SetDevice(PlayerPre.DeviceID)
		PlayerPre.StopAudio()
		'PlayPauseFitxer(ActualPlay.AUDIO_ListID,False,False,True)
		Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, CuePosition))
		'If AirenceConnected = True Then airenceSetLed( USB_CUE,colors_t.NONE)
	End Sub
	
	Private Sub canviPith()
		Bass.BASS_SetDevice(DEV_PLAY)
		infoPlay.freq = sldPith.Value
		Bass.BASS_ChannelSetAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_FREQ, infoPlay.freq)
		lbStrPith.Text = Format(sldPith.Value / 1000, "00.0")
		lbStrPith.BackColor = IIf(infoPlay.freq = FreqDef, Color.Lime, Color.Red)
	End Sub
	
	Private Sub StopFitxer()
		Bass.BASS_SetDevice(DEV_PLAY)
		Bass.BASS_ChannelStop(ActualPlay.AUDIO_HANDLE)
		Bass.BASS_StreamFree(ActualPlay.AUDIO_HANDLE)
		cmdPlay.Image = Me.picPlay.Image		
		If Me.lstDisp.Items.Count = 0  Then BorrarWaveForn()
		If ActualPlay.AUDIO_HANDLE = ActualPlay.AUDIO_HANDLE Then BorrarWaveForn()
		ActualPlay= Nothing
		'If AirenceConnected = True Then airenceSetLed( USB_ON,colors_t.NONE)
	End Sub		
	
	'Private _mySync As SYNCPROC
	
	Friend Sub PlayPauseFitxer(ByRef IndexID As Short, Optional ByRef ForceNextPlay As Boolean = False, Optional ByRef MixTracks As Boolean = False,optional LoadOnPause As Boolean = False )						
		ManualStop = False
		Bass.BASS_SetDevice(DEV_PLAY)		
		
		If PlayerPre.IsPlaying = True AndAlso PlayerPre.numCart = NumCart Then StopCue()
		
		Dim AutoDelIndex As Integer = -1 ' és la marca per no fer res
		
		
		If LoadOnPause = True Then GoTo lineLoad						
		If ForceNextPlay = True Then CuePosition = 0 : GoTo lineLoad	
		
		Select Case ActualPlay.isActv 
			Case BASSActive.BASS_ACTIVE_PLAYING
				AutoDelIndex = ActualPlay.AUDIO_ListID
				Bass.BASS_ChannelPause(ActualPlay.AUDIO_HANDLE)
				cmdPlay.Image = Me.picPlay.Image
			Case BASSActive.BASS_ACTIVE_PAUSED
				If Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False) = False Then
					'  Error de Play
				End If
				cmdPlay.Image = Me.picPause.Image				
				If Me.chkContinuos.Checked = True Then SetProxTrack(ActualPlay.AUDIO_ListID + 1)
				If ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_URL_STREAM Then
					Bass.BASS_ChannelSetAttribute( ActualPlay.AUDIO_HANDLE ,BASSAttribute.BASS_ATTRIB_VOL,0)
					Bass.BASS_ChannelSlideAttribute(ActualPlay.AUDIO_HANDLE,  BASSAttribute.BASS_ATTRIB_VOL, 100, 100000)
				End If				
			Case BASSActive.BASS_ACTIVE_STOPPED	
				lineLoad:
				
				Dim Path As String = ""
				Try
					Path = Me.lstDisp.Items.Item(IndexID).SubItems.Item(1).Tag	'TODO: Dim localPath As String = Params.PathAlternative & "\" & IO.Path.GetFileName(Path)					
				Catch ex As Exception
					Dim ItemCount As Integer = Me.lstDisp.Items.Count
					If IndexID >=  ItemCount Then IndexID = ItemCount -1
					If IndexID <  0 And ItemCount > 0 Then IndexID =  0
					if ItemCount = 0 Then  Exit Sub 
					Path = Me.lstDisp.Items.Item(IndexID).SubItems.Item(1).Tag	
					If Path.Length = 0 Then Exit Sub 
				End Try				
				If MixTracks = True then Bass.BASS_ChannelSlideAttribute(ActualPlay.AUDIO_HANDLE,  BASSAttribute.BASS_ATTRIB_VOL, 0, TimeFadeOut)
				
				Select Case UCase(IO.Path.GetExtension(Path))
					Case ".CDA"
						ActualPlay.AUDIO_HANDLE = BassCd.BASS_CD_StreamCreateFile(Path, BASSFlag.BASS_STREAM_AUTOFREE )
					Case ".STOP"
						If AutoDelIndex > -1 Then
							AutoDelete(AutoDelIndex)' aquest és l'actual track 
							AutoDelete(AutoDelIndex)' aquest és el stop	
							SetProxTrack(IndexID + 1)
							'Dim df As Integer = ProxPlay.AUDIO_ListID
							'If ProxPlay.AUDIO_ListID <  lstDisp.Items.Count Then
							PlayPauseFitxer(ProxPlay.AUDIO_ListID, True,False, True)
							'End If														
						Else							
							If IndexID+1 <  lstDisp.Items.Count Then
								PlayPauseFitxer(IndexID+1, True,False, True)
							End If							
						End If	
						lstDisp.Items.Item(IndexID).ForeColor = Color.Black
						Return
					Case ".STREAM"												
						Path = Replace( Path,".STREAM","") 'esborrem ".STREAM"
						ActualPlay.AUDIO_HANDLE = Bass.BASS_StreamCreateURL(Path, 0, BASSFlag.BASS_DEFAULT, Nothing, IntPtr.Zero)
						'Dim tagInfo As New TAG_INFO(Path)
						'If BassTags.BASS_TAG_GetFromURL(stream, tagInfo) Then
						' display the tags...
						'End If
					Case Else '.mp3 .wav .ogg						
						ActualPlay.AUDIO_HANDLE = Bass.BASS_StreamCreateFile(Path, 0, 0,  BASSFlag.BASS_STREAM_AUTOFREE )
				End Select				
				
				If ActualPlay.AUDIO_HANDLE = 0 Then Return '  Error de Càrrega
				
				Bass.BASS_ChannelGetAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_FREQ, infoPlay.freq)
				
				FreqDef = infoPlay.freq	
				
				If Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False) = True Then					
					ActualPlay.AUDIO_TipFitxer = Me.lstDisp.Items.Item(IndexID).Tag					
					
					If LoadOnPause = True Then
						Bass.BASS_ChannelPause(ActualPlay.AUDIO_HANDLE )
					Else
						If ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_URL_STREAM Then 
							Bass.BASS_ChannelSetAttribute( ActualPlay.AUDIO_HANDLE ,BASSAttribute.BASS_ATTRIB_VOL,0)
							Bass.BASS_ChannelSlideAttribute(ActualPlay.AUDIO_HANDLE,  BASSAttribute.BASS_ATTRIB_VOL, 100, 100000)
						End If
					End If
					
					If Me.chkLoop.Checked = True Then Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_MUSIC_LOOP, BASSFlag.BASS_MUSIC_LOOP)
					
					ActualPlay.DurationTime =  Bass.BASS_ChannelGetLength(ActualPlay.AUDIO_HANDLE)
					ActualPlay.ElapseTime = 0
					ActualPlay.RemainTime = ActualPlay.DurationTime 
					ActualPlay.DurationInSecons	= Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.DurationTime)
					ActualPlay.ElapseInSecons = 0
					OutSegons =  ActualPlay.DurationInSecons -1
					IntroSegons = 0
					
					ActualPlay.AUDIO_Titol = lstDisp.Items.Item(IndexID).Text
					ActualPlay.AUDIO_SubTitol = lstDisp.Items.Item(IndexID).SubItems.Item(1).Text					
					ActualPlay.AUDIO_ListID = IndexID
					ActualPlay.AUDIO_HoraRadi = Now.ToString("HH:mm:ss")
					ActualPlay.AUDIO_ID = lstDisp.Items.Item(IndexID).SubItems.Item(2).Tag				
					ActualPlay.AUDIO_Path = Path
					ActualPlay.AUDIO_Radiat = False
					ActualPlay.AUDIO_Durada = lstDisp.Items.Item(IndexID).SubItems.Item(3).Text
					
				End If	
				
				If LoadOnPause = True Then
					cmdPlay.Image = Me.picPlay.Image
				Else
					cmdPlay.Image = Me.picPause.Image
				End If
				
				Me.sldPith.Maximum = FreqDef + 30000
				Me.sldPith.Minimum = FreqDef - 30000
				Me.sldPith.Value = FreqDef
				canviPith()			
				GetWaveForm(Path)
				WF.GetCuePoints(CuePosition, 0, -30.0, Un4seen.Bass.Utils.LevelToDB(threshold, 32768), true)
				WF.AddMarker("CUE", CuePosition)
				If CuePosition > 0 Then Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE,CuePosition)
				
				ArrageDisplayPlaying()				
				If Me.chkContinuos.Checked = True Then SetProxTrack(IndexID + 1)			
				AutoDelete(AutoDelIndex)				
		End Select				
	End Sub	
	
	Private Sub AutoDelete(InxToDel As Integer )
		If mnuAutoDel.Checked AndAlso Me.chkContinuos.Checked AndAlso InxToDel > -1 Then
			lstDisp.Items.RemoveAt(InxToDel )
			Me.flowBotons.Controls.RemoveAt(InxToDel )	
			ProxPlay.AUDIO_ListID -=1 
			ActualPlay.AUDIO_ListID -=1 
		End If	
	End Sub
	
	Private Sub ArrageDisplayPlaying()
		
		On Error Resume Next
		If AutoFader = True Then Exit Sub
		'Busca Loop
		If STANDALONE= False Then
			Dim db As New MSC.dbs
			Dim StrSql As String = "SELECT loop_id FROM loops"
			If ActualPlay.AUDIO_ID > 0 Then
				StrSql = StrSql & " WHERE loop_id = " & ActualPlay.AUDIO_ID & " ;"
			Else
				Dim path As String = ActualPlay.AUDIO_Path
				StrSql = StrSql & " WHERE loop_idstr = '" & getMD5Hash(path) & "' ;"
			End If
			Dim ret As Object = db.ExecuteScalar(StrSql)
			cmdLoadLoop.Enabled = CBool(ret IsNot Nothing )
			db = Nothing
		End If
		
		Me.cmdLoopIn.BackColor = Color.Transparent
		LoopIn = 0
		Me.cmdLoopOut.BackColor = Color.Transparent
		LoopOut = ActualPlay.DurationTime
		If Me.chkLoop.Checked = True then refreshLoopLabels()
		' end loop
		
		If ActualPlay.AUDIO_TipFitxer = mdlMscDefines.Tipus_Play.CTL_SISTEMA  Then
			Me.lbIDActual.Text = ActualPlay.AUDIO_Path
		Else
			Me.lbIDActual.Text = ActualPlay.AUDIO_ID
		End If
		Me.lbIDActual.Tag = ActualPlay.AUDIO_TipFitxer
		
		If STANDALONE= False then RegistrarPlay(ActualPlay.AUDIO_Path, ActualPlay.AUDIO_ID, ActualPlay.AUDIO_TipFitxer)		
		frmHistory.addElementlistHistory(ActualPlay.AUDIO_ListID,Me.lstDisp)
		
		'   Mostra els tracks que fem el play amb l'icona  ----------
		Select Case ActualPlay.AUDIO_TipFitxer
			Case mdlMscDefines.Tipus_Play.CTL_PROMOS
				lstDisp.Items.Item(ActualPlay.AUDIO_ListID).ImageKey = Me.ImageList.Images.Keys.Item(7)
				CType(Me.flowBotons.Controls.Item(ActualPlay.AUDIO_ListID), Button).Image = Me.ImageList.Images.Item(7)
			Case mdlMscDefines.Tipus_Play.CTL_MUSICA
				lstDisp.Items.Item(ActualPlay.AUDIO_ListID).ImageKey = Me.ImageList.Images.Keys.Item(6)
				CType(Me.flowBotons.Controls.Item(ActualPlay.AUDIO_ListID), Button).Image = Me.ImageList.Images.Item(6)
			Case mdlMscDefines.Tipus_Play.CTL_PUBLICITAT
				lstDisp.Items.Item(ActualPlay.AUDIO_ListID).ImageKey = Me.ImageList.Images.Keys.Item(8)
				CType(Me.flowBotons.Controls.Item(ActualPlay.AUDIO_ListID), Button).Image = Me.ImageList.Images.Item(8)
			Case mdlMscDefines.Tipus_Play.CTL_JINGELS
				lstDisp.Items.Item(ActualPlay.AUDIO_ListID).ImageKey = Me.ImageList.Images.Keys.Item(9)
				CType(Me.flowBotons.Controls.Item(ActualPlay.AUDIO_ListID), Button).Image = Me.ImageList.Images.Item(9)
			Case mdlMscDefines.Tipus_Play.CTL_PROGRAMA
				lstDisp.Items.Item(ActualPlay.AUDIO_ListID).ImageKey = Me.ImageList.Images.Keys.Item(10)
				CType(Me.flowBotons.Controls.Item(ActualPlay.AUDIO_ListID), Button).Image = Me.ImageList.Images.Item(10)
			Case Else
				lstDisp.Items.Item(ActualPlay.AUDIO_ListID).ImageKey = Me.ImageList.Images.Keys.Item(11)
				CType(Me.flowBotons.Controls.Item(ActualPlay.AUDIO_ListID), Button).Image = Me.ImageList.Images.Item(11)
		End Select
		lstDisp.Items.Item(ActualPlay.AUDIO_ListID).ForeColor = Color.Blue
		
		
		For i As Integer = 0 To lstDisp.Items.Count - 1
			'Reset Bold
			lstDisp.Items.Item(i).Font = New Font(lstDisp.Font, FontStyle.Regular)
			CType(Me.flowBotons.Controls.Item(i), Button).Font = New Font(lstDisp.Font, FontStyle.Regular)
		Next
		lstDisp.Items.Item(ActualPlay.AUDIO_ListID).Font = New Font(lstDisp.Font, FontStyle.Bold)
		CType(Me.flowBotons.Controls.Item(ActualPlay.AUDIO_ListID), Button).Font = New Font(lstDisp.Font, FontStyle.Bold)
		
		If lstDisp.Items.Item(ActualPlay.AUDIO_ListID).SubItems.Count > 3 Then
			lstDisp.Items.Item(ActualPlay.AUDIO_ListID).SubItems(4).Text = CStr(TimeOfDay)
		Else
			lstDisp.Items.Item(ActualPlay.AUDIO_ListID).SubItems.Insert(3, New System.Windows.Forms.ListViewItem.ListViewSubItem(Nothing, CStr(TimeOfDay)))
		End If
		'--------		
		Me.cmdCue.Image = Me.picCueOFF.Image
		'CuePosition = 0		
		
		CalcPintaWavePunts()
		DrawWave()			
		
		'CuePress = False
		lbCue.BackColor = Color.Lime
		InitCue = False
		TotalDuration = calcTotalDurationList()			
	End Sub
	
	Sub CalcPintaWavePunts()		
		'INTRO
		WF.AddMarker("INTRO", IntroSegons)			
		
		'END
		If ActualPlay.AUDIO_TipFitxer <> mdlMscDefines.Tipus_Play.CTL_PUBLICITAT Then
			WF.GetCuePoints(0, OutSegons, -40.0, Un4seen.Bass.Utils.LevelToDB(threshold, 32768), true)	
		End If
		'si la posició OutSegons és més gran que 70 % estableix OutSegons = sldDuration.Maximum
		Dim MinEND As Integer = CInt (ActualPlay.DurationInSecons * 0.8)
		If OutSegons < MinEND Then 
			OutSegons = ActualPlay.DurationInSecons -1
		End If
		WF.AddMarker("END", OutSegons)
	End Sub
	
	Private Sub SetProxTrack(ByRef IndexPoxID As Short)
		If IndexPoxID < 0 Or lstDisp.Items.Count = 0 Then Exit Sub
		If mnuBucleList.Checked = False And lstDisp.Items.Count = IndexPoxID Then Exit Sub
		If lstDisp.Items.Count = IndexPoxID Then IndexPoxID = 0
		
		For i As Integer = 0 To lstDisp.Items.Count - 1
			If lstDisp.Items.Item(i).ForeColor <> Color.Blue Then
				lstDisp.Items.Item(i).ForeColor = Color.Black
			End If
		Next
		Try
			ProxPlay.AUDIO_TipFitxer = Me.lstDisp.Items.Item(IndexPoxID).Tag
			ProxPlay.AUDIO_ListID = IndexPoxID
			ProxPlay.AUDIO_Durada = lstDisp.Items.Item(IndexPoxID).SubItems.Item(3).Text
			ProxPlay.AUDIO_HoraRadi = "00:00:00"
			ProxPlay.AUDIO_ID = lstDisp.Items.Item(IndexPoxID).SubItems.Item(2).Tag
			ProxPlay.AUDIO_Path = lstDisp.Items.Item(IndexPoxID).SubItems.Item(1).Tag
			ProxPlay.AUDIO_Radiat = False
			ProxPlay.AUDIO_SubTitol = lstDisp.Items.Item(IndexPoxID).SubItems.Item(1).Text
			ProxPlay.AUDIO_Titol = lstDisp.Items.Item(IndexPoxID).Text
			lstDisp.Items.Item(ProxPlay.AUDIO_ListID).ForeColor = Color.Green
		Catch ex As Exception
			
		End Try
		
	End Sub
	
	Private Sub RegistrarPlay(Byval strPathFitxer As String, Byval id As Short, Byval TipFitxer As mdlMscDefines.Tipus_Play)
		If TipFitxer < mdlMscDefines.Tipus_Play.CTL_PROGRAMA Then strPathFitxer = lbDisplayTitol.Text
		addCtlRadi(id, CShort(TipFitxer), 0,  #1:00:00 AM#, 0, 0, Usuari.UsrID, VB.Left(AddSlashes(strPathFitxer), 250), 0)
		IntroSegons = 0
		Dim StrSql As String = ""
		Dim db As New MSC.dbs
		Select Case TipFitxer
			Case mdlMscDefines.Tipus_Play.CTL_PROMOS
				StrSql = "UPDATE promos"
				StrSql = StrSql & " SET "
				StrSql = StrSql & " promo_dataradiacio = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'"
				StrSql = StrSql & " WHERE promo_id = " & id & " ;"
			Case mdlMscDefines.Tipus_Play.CTL_MUSICA
				StrSql = "UPDATE temes "
				StrSql = StrSql & " SET"
				StrSql = StrSql & " tema_dataradiacio = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'"
				StrSql = StrSql & ", tema_numradiacions = tema_numradiacions + 1"
				StrSql = StrSql & " WHERE tema_id = " & id & " ;"
				'Aprofito per saber el temps intro
				IntroSegons = SecondDec(db.ExecuteScalar("SELECT tema_intro FROM temes WHERE tema_id=" & id).ToString)				
				blIntro = IIf(IntroSegons > 0, True, False)
			Case mdlMscDefines.Tipus_Play.CTL_PUBLICITAT
				StrSql = "UPDATE falques SET falc_dataradi ='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "',falc_nradia = falc_nradia+1 WHERE falc_id=" & id
			Case mdlMscDefines.Tipus_Play.CTL_JINGELS
				StrSql = "UPDATE jingels SET jing_dataradiacio ='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "',jing_numradi = jing_numradi+1 WHERE jing_id=" & id
			Case mdlMscDefines.Tipus_Play.CTL_PROGRAMA
				'S'ha de salvar les dades a la sessió 
				StrSql = "UPDATE programes SET ses_dataradiacio ='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "',ses_numradiacions = ses_numradiacions+1 WHERE ses_id=" & id
		End Select		
		db.Update_ID(StrSql)
		db = Nothing
	End Sub
	
	Private Function findIndexActualPlay() As Integer
		If Me.picWave.BackgroundImage IsNot Nothing Then
			Return ActualPlay.AUDIO_ListID			
		ElseIf 	ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Then
			Return ActualPlay.AUDIO_ListID
		Else
			Return GetIndexListOnFocus()
		End If
	End Function
	
	Private Function calcTotalDurationList() As Long
		lstDisp.Refresh()
		If lstDisp.Items.Count = 0 Then Return 0
		
		Dim segTotals As Long
		Dim IndPlay As Integer = findIndexActualPlay()
		If IndPlay < 0 Then IndPlay = 0
		If (ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING) Then IndPlay += 1
		For I As Integer = IndPlay To Me.lstDisp.Items.Count - 1
			segTotals = segTotals + SecondDec(lstDisp.Items(I).SubItems(3).Text)
		Next
		If (ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING) Then segTotals += ActualPlay.DurationInSecons		
		lbTotal.text = Un4seen.Bass.Utils.FixTimespan(segTotals, "HHMMSSFF")	
		Return segTotals
	End Function
	
	Private Sub ListDevice()
		'If DeviceAudioEnable = False Then Exit Sub
		Dim info As New BASS_DEVICEINFO
		Dim ID_Dev As Integer = 1
		While Not (info Is Nothing)
			info = Bass.BASS_GetDeviceInfo(ID_Dev)
			If info Is Nothing Then Exit While 
			Dim mnuNewDev As ToolStripMenuItem
			Dim mnuNewDevPre As ToolStripMenuItem
			mnuNewDev = New ToolStripMenuItem
			mnuNewDevPre = New ToolStripMenuItem
			mnuNewDev.Text = info.name
			mnuNewDevPre.Text = info.name
			mnuNewDev.Tag = ID_Dev
			mnuNewDevPre.Tag = ID_Dev
			mnuNewDev.Visible = True
			mnuNewDevPre.Visible = True
			
			mnuDevice.DropDownItems.Add(mnuNewDev)
			mnuDevicePreEscolta.DropDownItems.Add(mnuNewDevPre)
			
			AddHandler mnuNewDev.Click, AddressOf AssigDevice
			AddHandler mnuNewDevPre.Click, AddressOf AssigDevicePre
			mnuNewDevPre.Checked = (PlayerPre.DeviceID = ID_Dev)
			
			ID_Dev = ID_Dev + 1
		End While		
		ID_Dev -=  1 ' Resta el IdDevice que ha sumat abans de sortir
		
		Dim frm As Form
		For Each frm In My.Application.OpenForms
			If frm.Name = "frmCartut" Then
				NumCart = NumCart + 1
			End If
		Next
		If IsLoadForm("frmAudioDBS") Then
			Dim mnuNewCart As ToolStripMenuItem
			mnuNewCart = New ToolStripMenuItem
			mnuNewCart.Text = "Cartutxera " & NumCart
			Dim KeyShortCut As System.Windows.Forms.Keys = CType(Choose(NumCart, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9), System.Windows.Forms.Keys)
			mnuNewCart.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or KeyShortCut), System.Windows.Forms.Keys)
			mnuNewCart.Tag = NumCart
			mnuNewCart.Visible = True
			frmAudioDBS.mnuAddRepreoduccio.DropDownItems.Add(mnuNewCart)
			AddHandler mnuNewCart.Click, AddressOf frmAudioDBS.AfegirAReproducció_Click
		End If
		
		Dim frmExplore As Form		
		For Each frmExplore In My.Application.OpenForms
			If frmExplore.Name = "frmFileExplorer"  Then
				Dim MyFrmExplore As frmFileExplorer = CType(frmExplore,frmFileExplorer)
				Dim mnuNewCart As ToolStripMenuItem
				mnuNewCart = New ToolStripMenuItem
				mnuNewCart.Text = "Cartutxera " & NumCart
				Dim KeyShortCut As System.Windows.Forms.Keys = CType(Choose(NumCart, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9), System.Windows.Forms.Keys)
				mnuNewCart.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or KeyShortCut), System.Windows.Forms.Keys)
				mnuNewCart.Tag = NumCart
				mnuNewCart.Visible = True
				MyFrmExplore.mnuAddRepreoduccio.DropDownItems.Add(mnuNewCart)
				AddHandler mnuNewCart.Click, AddressOf MyFrmExplore.AfegirAReproducció_Click			
			End If
		Next
		
		Dim FitxerINI As New IniFile
		Dim savedDevice As Integer = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "DevicePlay_" & NumCart.ToString, 999))
		
		If savedDevice <> 999 Then
			ID_Dev = savedDevice
		Else
			If ID_Dev >= NumCart Then ID_Dev = NumCart				
		End If
		'Assigna la targeta de so per defecte segons el nº de cartutxera.
		Try
			CType(mnuDevice.DropDownItems(ID_Dev - 1), ToolStripMenuItem).Checked = True
		Catch ex As Exception			
			CType(mnuDevice.DropDownItems(0), ToolStripMenuItem).Checked = True
			ID_Dev = 1
		End Try
		
		StrNumCart = "Cart " & NumCart & ": "
		DEV_PLAY= CType(mnuDevice.DropDownItems(ID_Dev - 1), ToolStripMenuItem).tag
		DeviceAudioEnable = IniBASS_CTL(DEV_PLAY, Me.Handle ) 
		
	End Sub
	
	'Altres
	Private Sub ListLanguage()
		Dim tbLangs As DataTable = lang.ListLangInterface()
		For i As Integer = 0  To tbLangs.Rows.Count -1
			Dim mnuListLang As ToolStripMenuItem			
			mnuListLang = New ToolStripMenuItem
			mnuListLang.Text = tbLangs.Rows(i)("LangName").ToString 
			mnuListLang.Tag = tbLangs.Rows(i)("langCode").ToString 			  
			mnuListLang.Checked = ( mnuListLang.Tag = lang.Code)
			
			Me.mnuLang.DropDownItems.Add(mnuListLang)
			
			AddHandler mnuListLang.Click, AddressOf AssigLanguage
		Next i
	End Sub
	
	Private Sub AssigLanguage(ByVal sender As System.Object, ByVal e As System.EventArgs)
		
		For i As Integer = 0 To mnuLang.DropDownItems.Count - 1
			CType(mnuLang.DropDownItems(i), ToolStripMenuItem).Checked = False
		Next
		
		CType(sender, ToolStripMenuItem).Checked = True
		
		lang.Code  = CType(sender, ToolStripMenuItem).Tag
		setLanguageGlobal()
		Dim myForms As FormCollection = Application.OpenForms
		For Each frmName As Form In myForms
			If MethodExist(frmName,"setLanguageForm") = True Then
				'ok
			End If			
		Next
		
		My.Settings.Lang = lang.Code 
		Try
			My.Settings.Save()
		Catch ex As Exception
			
		End Try
	End Sub
	
	Private Sub salvarLlistat()
		Dim sNomFitxer As String
		Dim I As Short
		Dim strTXT As String
		Dim NumFichero As Short
		Line1:
		With SaveFileDialog1
			.InitialDirectory = Params.PathDefPauta & "\"
			.Title = SAVE_FILE_TITLE
			.Filter = SAVE_FILE_FILTER & " (*.ptc)|*.ptc"
			.FileName = SAVE_FILE_NAME
			
			If .ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then Exit Sub
			sNomFitxer = .FileName
			If IO.File.Exists(sNomFitxer) Then				   
				If MsgBox(String.Format( MSG_OVERWRITE_LIST ,sNomFitxer) , MsgBoxStyle.YesNo, MSG_ATENCIO) = MsgBoxResult.No Then GoTo Line1
			End If
		End With
		
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
		NumFichero = FreeFile()
		'Error si només és de lectura el dispositiu
		FileOpen(NumFichero, sNomFitxer, OpenMode.Output)
		PrintLine(NumFichero, "V:" & FORMAT_FITXER)
		For I = 0 To lstDisp.Items.Count - 1
			Dim Titol As String = lstDisp.Items.Item(I).Text
			Dim Tipus As Short = lstDisp.Items.Item(I).Tag
			Dim SubTitol As String = lstDisp.Items.Item(I).SubItems.Item(1).Text
			Dim Path As String = lstDisp.Items.Item(I).SubItems.Item(1).Tag
			Dim id As String = lstDisp.Items.Item(I).SubItems.Item(2).Tag
			Dim Durada As String = lstDisp.Items.Item(I).SubItems(3).Text
			
			strTXT = Chr(34) & Titol & Chr(34) & ","
			strTXT = strTXT & Chr(34) & SubTitol & Chr(34) & ","
			strTXT = strTXT & Durada & ","
			strTXT = strTXT & Tipus & ","
			strTXT = strTXT & id & ","
			strTXT = strTXT & Chr(34) & Path & Chr(34)
			'I = I + 1
			PrintLine(NumFichero, strTXT)
		Next
		FileClose()
		
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
	End Sub
	
	Private Sub refreshLoopLabels()
		Me.lbLoopIn.Text = Un4seen.Bass.Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, LoopIn), "MMSSFF")
		Me.lbLoopOut.Text = Un4seen.Bass.Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, LoopOut), "MMSSFF")
		'LoopIn
		'LoopIn 
		WF.AddMarker("Loop In", LoopIn)
		WF.AddMarker("Loop Out", LoopOut)
		DrawWave()
	End Sub
	
	Private Sub AssigDevice(ByVal sender As System.Object, ByVal e As System.EventArgs)
		Dim i As Short
		For i = 0 To mnuDevice.DropDownItems.Count - 1
			CType(mnuDevice.DropDownItems(i), ToolStripMenuItem).Checked = False
		Next
		CType(sender, ToolStripMenuItem).Checked = True
		
		DEV_PLAY = CType(sender, ToolStripMenuItem).Tag
		'Si aquest Dev ja està assignat a una altre Cartutxera no fer res
		Bass.BASS_SetDevice(DEV_PLAY)
		Bass.BASS_Free()
		
		If Bass.BASS_Init(DEV_PLAY, 44100, Un4seen.Bass.BASSInit.BASS_DEVICE_DEFAULT, Me.Handle ) = False Then
			MsgBox(MSG_ERROR_BASS_NO_INI,MsgBoxStyle.Critical ,MSG_ATENCIO)
			Exit Sub
		End If
		'Bass.BASS_SetDevice(IdDevice)
		
	End Sub
	
	Private Sub AssigDevicePre(ByVal sender As System.Object, ByVal e As System.EventArgs)
		
		Dim i As Short
		For i = 0 To mnuDevicePreEscolta.DropDownItems.Count - 1
			CType(mnuDevicePreEscolta.DropDownItems(i), ToolStripMenuItem).Checked = False
		Next
		CType(sender, ToolStripMenuItem).Checked = True
		If PlayerPre.IsPlaying Then PlayerPre.StopAudio
		PlayerPre = Nothing
		PlayerPre = New  PlayerPreEscolta(CType(sender, ToolStripMenuItem).Tag)
		
		My.Settings.DevicePre = PlayerPre.DeviceID
		My.Settings.save()
	End Sub
	
	'Finalitza
	Private Sub EndAplic()
		Try
			frmAudioDBS.mnuAddRepreoduccio.DropDownItems.RemoveAt(NumCart - 1)
		Catch ex As Exception
			
		End Try
		
		If WindowState <> System.Windows.Forms.FormWindowState.Minimized Then
			Dim FitxerINI As New IniFile
			FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Strings.Left(StrNumCart, 6) & "_L", CStr(Me.Left))
			FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Strings.Left(StrNumCart, 6) & "_T", CStr(Me.Top))
			FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Strings.Left(StrNumCart, 6) & "_W", CStr(Me.Width))
			FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Strings.Left(StrNumCart, 6) & "_H", CStr(Me.Height))
			FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "SD", CStr(SplitContainer1.SplitterDistance))						
		End If
		My.Settings.AutoFader = Me.mnuAutoFader.Checked
		'My.Settings.Lang = lang.lang 
		Try
			My.Settings.Save()
		Catch ex As Exception
			
		End Try
		
		
		Me.tmrDisplay.Enabled = False
		Me.tmrRellotge.Enabled = False
		Me.tmr_FadeOunt.Enabled = False
		Me.tmrVuMeter.Enabled = False
		
		Bass.BASS_ChannelStop(ActualPlay.AUDIO_HANDLE)		
		Bass.BASS_StreamFree(ActualPlay.AUDIO_HANDLE)
		Dim Descrip As String = MSG_END_APP & " " & My.Application.Info.Title & " (" & StrNumCart & ") V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & " PID:" & MyAPP.IDSesion_Client
		If STANDALONE = False Then addCtlRadi(0, CShort(Tipus_Play.CTL_SISTEMA), 0, "00:00:00", 0, 0, Usuari.UsrID, Descrip, 0)
		'If AirenceConnected = True Then airenceClose()
		
	End Sub
	
	#End Region
	
	Private Sub mnuExplorerDBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExplorerDBS.Click
		Dim frm As Form
		For Each frm In My.Application.OpenForms
			If frm.Name = "frmCartut" Then
				Dim formCart As Form = Nothing
				formCart = frm
				CType(formCart, frmCartut).mnuExplorerDBS.Checked = Not CType(formCart, frmCartut).mnuExplorerDBS.Checked
			End If
		Next
		
		If mnuExplorerDBS.Checked Then
			My.Forms.frmAudioDBS.Show()
		Else
			If IsLoadForm("frmAudioDBS") Then My.Forms.frmAudioDBS.Close()
		End If
		
		Dim FitxerINI As New IniFile
		FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "FormDBS", IIf(mnuExplorerDBS.Checked= True,1,0))
		FitxerINI = Nothing 
	End Sub
	
	Private Sub mnuExplorerPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExplorerPC.Click
		'		Dim p As System.Diagnostics.Process = New System.Diagnostics.Process()
		'		p.StartInfo.FileName = "explorer.exe"
		'		p.Start()
		Dim frmExplore As New frmFileExplorer
		frmExplore.Show()
	End Sub
	
	Private Sub mnuNewCartut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewCartut.Click
		If STANDALONE = True Then
			Dim cart As New frmCartut
			cart.Show()
		elseIf MyAPP.NovaConnex("MSC Cartutxera", Usuari.UsrNom, True) = True Then
			Dim cart As New frmCartut
			cart.Show()
		End If
	End Sub
	
	Private Sub mnuMesInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMesInfo.Click
		Try
			Dim Tipus As Tipus_Play = lstDisp.FocusedItem.Tag
			Dim id As Integer = lstDisp.FocusedItem.SubItems.Item(2).Tag
			Dim strText As String = strInfoFitxer(Tipus, id)
			InfoMsg = New frmTip
			InfoMsg.ShowMessage(strText, MES_INFO, IconStyles.Information, _
				ContentAlignment.MiddleCenter, 0, 0, True, , , , , Themes.WinXpStyle, MessageBoxButtons.OK)
		Catch ex As Exception
		End Try
	End Sub
	
	Private Sub mnuPreEscolta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreEscolta.Click        
		PlayPreEscolta()
	End Sub
	
	Private Sub cmdPlayPre_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdPlayPre.Click
		If PlayerPre.IsPlaying Then
			PlayerPre.StopAudio()
		Else
			PlayPreEscolta()
		End If
	End Sub
	
	Private Sub ToolStripTextBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripTextBox1.Click
		ForcePlay()
	End Sub
	
	Private Sub PlayPreEscolta()
		Dim Path As String = ""
		Try
			If lstDisp.Focused = True Then
				Path = lstDisp.FocusedItem.SubItems(1).Tag
			ElseIf lstDisp.Items.Count > 0 Then
				Path = lstDisp.SelectedItems(0).SubItems(1).Tag
			End If
		Catch ex As Exception
			Exit Sub
		End Try
		If Len(Path) = 0 Then Exit Sub
		PlayerPre.PlayAudio(Path)
		PlayerPre.NumCart =  NumCart
		cmdPlayPre.Image = Me.picStop.Image
	End Sub
	
	Friend Sub ForcePlay()
		If lstDisp.Items.Count = 0 Then Exit Sub
		Dim IndexLlistat As Integer
		If lstDisp.FocusedItem Is Nothing And lstDisp.Items.Count Then
			IndexLlistat = 0
		Else
			IndexLlistat = lstDisp.FocusedItem.Index
		End If
		PlayPauseFitxer(IndexLlistat, True, True)
	End Sub
	
	Private Sub lbInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbInfo.Click
		lbInfo.Visible = False
	End Sub
	
	
	Private Sub lstDisp_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstDisp.Resize
		lstDisp.Columns(0).Width = lstDisp.Width / 3
		lstDisp.Columns(1).Width = lstDisp.Width / 3		
	End Sub
	
	Private Sub cmdMesGranBotons_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMesGranBotons.Click, cmdMenyGranBotons.Click
		If flowBotons.Visible = True Then
			Select Case CType(sender, ToolStripButton).Text
				Case "+"
					For i As Integer = 0 To Me.flowBotons.Controls.Count - 1
						Try
							Me.flowBotons.Controls(i).Width = Me.flowBotons.Controls(i).Width + 10
							Me.flowBotons.Controls(i).Height = Me.flowBotons.Controls(i).Height + 10
						Catch ex As Exception
							Exit For
						End Try
					Next
					cmdMenyGranBotons.Enabled = True
				Case "-"
					For i As Integer = 0 To Me.flowBotons.Controls.Count - 1
						Try
							Me.flowBotons.Controls(i).Width = Me.flowBotons.Controls(i).Width - 10
							Me.flowBotons.Controls(i).Height = Me.flowBotons.Controls(i).Height - 10
						Catch ex As Exception
							Exit For
						End Try
					Next
					Try
						If Me.flowBotons.Controls(0).Height <= 30 Then
							cmdMenyGranBotons.Enabled = False
						End If
					Catch ex As Exception
					End Try
			End Select
		Else
			Dim MySize As Integer = lstDisp.Font.Size 			
			Select Case CType(sender, ToolStripButton).Text
				Case "+" : MySize += 1 
				Case "-" : MySize -= 1 
			End Select
			cmdMenyGranBotons.Enabled = (MySize > 8)
			cmdMesGranBotons.Enabled = (MySize < 20)
			
			Dim MyFont As Font =  New System.Drawing.Font("Microsoft Sans Serif", MySize)
			Me.lstDisp.Font = MyFont  
		End If
		
	End Sub
	
	Private Sub mnuReOrdCart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReOrdCart.Click
		Dim frm As Form
		Dim _top As Integer = 10
		Dim _left As Integer = 10
		For Each frm In My.Application.OpenForms
			'If frm.Name = "frmCartut"Then
			frm.Left = _left
			frm.Top = _top
			frm.Width = 405
			frm.Height = 500
			frm.Focus()
			_top = _top + 25
			_left = _left + 100
			'End If
		Next
	End Sub
	
	Private Sub chkContinuos_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkContinuos.CheckedChanged
		AutomàticaToolStripMenuItem.Checked = chkContinuos.Checked
		If chkContinuos.Checked = True Then chkLoop.Checked = False
		If chkContinuos.Checked = False Then Me.mnuAutoDel.Checked = False
		Me.mnuAutoDel.Enabled  = chkContinuos.Checked
		If chkContinuos.Checked = True AndAlso Bass.BASS_ChannelIsActive(ActualPlay.AUDIO_HANDLE) = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Then
			Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_DEFAULT, BASSFlag.BASS_MUSIC_LOOP)			
		End If
		SetProxTrack(ActualPlay.AUDIO_ListID + 1)
		calcTotalDurationList()
	End Sub
	
	Private Sub chkLoop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLoop.CheckedChanged
		If chkLoop.Checked = True Then chkContinuos.Checked = False
		If Me.chkLoop.Checked = True AndAlso Bass.BASS_ChannelIsActive(ActualPlay.AUDIO_HANDLE) = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Then
			Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_MUSIC_LOOP ,BASSFlag.BASS_MUSIC_LOOP )
			WF.AddMarker("Loop In", LoopIn)
			WF.AddMarker("Loop Out", LoopOut)
			DrawWave()
		ElseIf Me.chkLoop.Checked = False AndAlso Bass.BASS_ChannelIsActive(ActualPlay.AUDIO_HANDLE) = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Then
			Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_DEFAULT, BASSFlag.BASS_MUSIC_LOOP)
			WF.RemoveMarker( "Loop In" )
			WF.RemoveMarker( "Loop Out")
			DrawWave()
		End If
	End Sub
	
	''' <summary>
	''' Move selected listview item up or down based on moveUp= True/false.
	''' </summary>
	''' <param name = "moveUp"></param>
	Private Sub MoveListViewItem(ByRef lv As ListView, ByVal moveUp As Boolean)
		Dim selIdx As Integer
		Dim ItemListCache As System.Windows.Forms.ListViewItem
		
		With lv
			Try
				selIdx = .SelectedItems.Item(0).Index
			Catch ex As Exception
				Exit Sub
			End Try
			.MultiSelect = False
			If moveUp Then
				' ignore moveup of row(0)
				If selIdx = 0 Then
					Exit Sub
				End If
				' move the subitems for the previous row
				' to cache so we can move the selected row up
				ItemListCache = .Items(selIdx - 1)
				.Items(selIdx - 1) = .Items(selIdx).Clone
				.Items(selIdx) = ItemListCache.Clone
				
				.Items(selIdx - 1).Selected = True
				.Items.Item(selIdx).Focused = True
				.Refresh()
				.Focus()
			Else
				' ignore move down of last row
				If selIdx = .Items.Count - 1 Then
					Exit Sub
				End If
				' move the subitems for the next row
				' to cache so we can move the selected row down
				ItemListCache = .Items(selIdx + 1)
				.Items(selIdx + 1) = .Items(selIdx).Clone
				.Items(selIdx) = ItemListCache.Clone
				
				.Items(selIdx + 1).Selected = True
				.Items.Item(selIdx).Focused = True
				.Refresh()
				.Focus()
			End If
			If .Items.Item(selIdx).ForeColor = Color.Blue And lstDisp.Items.Item(selIdx).Font.Bold = True And moveUp = True Then                
				ActualPlay.AUDIO_ListID += 1
				If chkContinuos.Checked = True Then SetProxTrack(ProxPlay.AUDIO_ListID + 1)
			ElseIf .Items.Item(selIdx).ForeColor = Color.Blue And lstDisp.Items.Item(selIdx).Font.Bold = True And moveUp = False Then
				If chkContinuos.Checked = True Then SetProxTrack(selIdx + 1)
			Else
				If chkContinuos.Checked = True Then SetProxTrack(ProxPlay.AUDIO_ListID)
			End If
			.MultiSelect = True
		End With
	End Sub
	
	Private Sub mnuForceDirectMusic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuForceDirectMusic.Click
		mnuForceDirectMusic.Checked = Not mnuForceDirectMusic.Checked
	End Sub
	
	Private Sub mnuDesmarcar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDesmarcar.Click
		Try
			Dim Tipus As Tipus_Play = lstDisp.FocusedItem.Tag
			Dim IndexID As Integer = lstDisp.FocusedItem.Index
			Select Case Tipus
				Case mdlMscDefines.Tipus_Play.CTL_PROMOS
					lstDisp.Items.Item(IndexID).ImageKey = Me.ImageList.Images.Keys.Item(1)
					CType(Me.flowBotons.Controls.Item(IndexID), Button).Image = Me.ImageList.Images.Item(1)
				Case mdlMscDefines.Tipus_Play.CTL_MUSICA
					lstDisp.Items.Item(IndexID).ImageKey = Me.ImageList.Images.Keys.Item(0)
					CType(Me.flowBotons.Controls.Item(IndexID), Button).Image = Me.ImageList.Images.Item(0)
				Case mdlMscDefines.Tipus_Play.CTL_PUBLICITAT
					lstDisp.Items.Item(IndexID).ImageKey = Me.ImageList.Images.Keys.Item(2)
					CType(Me.flowBotons.Controls.Item(IndexID), Button).Image = Me.ImageList.Images.Item(2)
				Case mdlMscDefines.Tipus_Play.CTL_JINGELS
					lstDisp.Items.Item(IndexID).ImageKey = Me.ImageList.Images.Keys.Item(3)
					CType(Me.flowBotons.Controls.Item(IndexID), Button).Image = Me.ImageList.Images.Item(3)
				Case mdlMscDefines.Tipus_Play.CTL_PROGRAMA
					lstDisp.Items.Item(IndexID).ImageKey = Me.ImageList.Images.Keys.Item(4)
					CType(Me.flowBotons.Controls.Item(IndexID), Button).Image = Me.ImageList.Images.Item(4)
				Case Else
					lstDisp.Items.Item(IndexID).ImageKey = Me.ImageList.Images.Keys.Item(5)
					CType(Me.flowBotons.Controls.Item(IndexID), Button).Image = Me.ImageList.Images.Item(5)
			End Select
			lstDisp.Items.Item(IndexID).ForeColor = Color.Black
		Catch ex As Exception
		End Try
	End Sub
	
	Private Sub mnuContextDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuContextDel.Click
		Click_Borrar()
	End Sub
	
	Private Sub mnuPreEscoltaStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreEscoltaStop.Click
		PlayerPre.StopAudio()
	End Sub
	
	Private Sub mnuSetPoxPlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSetPoxPlay.Click
		Try
			Dim IndexLlistat As Integer = lstDisp.FocusedItem.Index
			lstDisp.Items.Item(ProxPlay.AUDIO_ListID).ForeColor = Color.Black
			SetProxTrack(IndexLlistat)
		Catch ex As Exception
		End Try
	End Sub
	
	Private Sub mnuMoveDBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMoveDBS.Click
		If IsLoadForm("frmAudioDBS") Then
			frmAudioDBS.Width = 814
			frmAudioDBS.Height = 343
			frmAudioDBS.Left = 31
			frmAudioDBS.Top = 583
		End If
	End Sub
	
	Private Sub mnuBucleList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBucleList.Click
		mnuBucleList.Checked = Not mnuBucleList.Checked
		If mnuBucleList.Checked = True Then mnuAutoDel.Checked = False
	End Sub
	
	
	Private Sub mnuProgEditAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProgEditAudio.Click
		With OpenFileDialog1
			.FileName = ""
			.Title = OPENFILEDIALOG1_TITLE
			.Multiselect = False
			.Filter = LIST_FILTER_FILES & " EXE (*.exe) |*.exe"
			If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
				Dim IniFile As New IniFile
				IniFile.INIWrite(MyAPP.IniFile, "Data Control", "ProgEditAudio", .FileName)
				'My.Settings.ProgEditAudio = .FileName
				Dim versionInfo As FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(.FileName)
				Me.mnuProgEditAudio.Text = "Editor: " & versionInfo.FileDescription
				mnuEditAudio.Enabled = True
			End If
		End With
		
	End Sub
	
	Private Sub mnuEditAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditAudio.Click
		
		If ProgEditAudio.Length > 0 Then
			'TODO:Possible problema si ja està sonant (S'hauria de parar per alliberar el fitxer)
			Dim PathFitxer As String = ""
			
			For i As Integer = 0 To lstDisp.SelectedItems.Count - 1
				Dim tempPath As String = Me.lstDisp.SelectedItems(i).SubItems.Item(1).Tag
				If IO.File.Exists(tempPath) Then
					PathFitxer += " " & Chr(34) & tempPath & Chr(34)
				End If
			Next
			If PathFitxer.Length < 3 Then Exit Sub
			Dim p As System.Diagnostics.Process = New System.Diagnostics.Process()
			p.StartInfo.Arguments = PathFitxer.Trim
			p.StartInfo.FileName = ProgEditAudio
			Try
				p.Start()
			Catch ex As Exception
				
				Throw
			End Try
		End If
		
	End Sub
	
	
	Private Sub mnuSavedevicePlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSavedevicePlay.Click
		Dim FitxerINI As New IniFile		
		FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "DevicePlay_" & NumCart.ToString, DEV_PLAY)
	End Sub
	
	Private Sub mnuDelDevicePlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelDevicePlay.Click
		Dim FitxerINI As New IniFile
		For i As Integer = 0 To 10
			FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "DevicePlay_" & i.ToString)
		Next
	End Sub


    Sub CalcularBPMAlCarregarToolStripMenuItemClick(sender As Object, e As EventArgs) Handles mnuCalcBPMOnLoad.Click
        mnuCalcBPMOnLoad.Checked = Not mnuCalcBPMOnLoad.Checked
    End Sub

    Sub MnuCalBPMListClick(sender As Object, e As EventArgs) Handles mnuCalBPMList.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
        For I As Integer = 0 To Me.lstDisp.Items.Count - 1
            Dim Path As String = Me.lstDisp.Items.Item(I).SubItems.Item(1).Tag
            Me.lstDisp.Items.Item(I).SubItems.Item(5).Text = CInt(getBPMFitxer(Path, Me.Handle))
            My.Application.DoEvents()
        Next I
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Sub MnuSHClick(sender As Object, e As EventArgs) Handles mnuSH.Click
        Dim HoraEnd As Date
        Me.mnuSH.Checked = Not Me.mnuSH.Checked
        bl_AutoSinc = Me.mnuSH.Checked

        If bl_AutoSinc = True Then
            Me.lstDisp.Items.Clear()
            addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, LABEL_SENYALS_HORARIS, Params.NomRadio, Params.PathSenyalHora, 0, TimeSerial(0, 0, SegSH), #12:00:00 AM#, 0)
            If TimeOfDay.Minute < 30 Then
                TimeToStart = TimeSerial(TimeOfDay.Hour, 29, (60 - SegSH))
                HoraEnd = TimeSerial(TimeOfDay.Hour, 29, 60)
            Else
                TimeToStart = TimeSerial(TimeOfDay.Hour, 59, (60 - SegSH))
                HoraEnd = TimeSerial(TimeOfDay.Hour, 59, 60)
            End If

            mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": " & TimeToStart.ToString("HH:mm:ss")
            lbInfo.Text = LABEL_H_INI & ": " & TimeToStart.ToString("HH:mm:ss") & "/ " & LABEL_H_END & ": " & HoraEnd.ToString("HH:mm:ss")
            If TimeToStart < TimeOfDay Then
                'fas tard per sincronitzar				
                MsgBox(String.Format(MSG_ERROR_DURADA_SINCRO, TimeSerial(0, 0, TotalDuration).ToString("HH:mm:ss")), MsgBoxStyle.Critical, MSG_ATENCIO)
                GoTo LineError
            End If

        End If
        lbInfo.Visible = bl_AutoSinc
        Exit Sub
LineError:
        bl_AutoSinc = False
        lbAutoSinc.BackColor = Color.Lime
        Me.mnuSH.Checked = False
        mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00"
    End Sub

    Sub MnuHistoryPlayClick(sender As Object, e As EventArgs) Handles mnuHistoryPlay.Click
        frmHistory.Show()
    End Sub

#Region "WaveForm"

    Private WF As Un4seen.Bass.Misc.WaveForm = Nothing

    Private Sub GetWaveForm(FileToPain As String)

        Dim tmpWaveFile As String = IO.Path.ChangeExtension(FileToPain, ".wf")
        WF = Nothing
        WF = New WaveForm(FileToPain, New Un4seen.Bass.Misc.WAVEFORMPROC(AddressOf MyWaveFormCallback), Me)
        WF.FrameResolution = 0.01F ' 10ms are nice
        WF.CallbackFrequency = 500 ' every 10 seconds rendered
        WF.ColorBackground = SystemColors.Control
        WF.ColorLeft = Color.GreenYellow
        WF.ColorLeftEnvelope = Color.Orange
        WF.ColorRight = Color.LightGray
        WF.ColorRightEnvelope = Color.Orange
        WF.DrawWaveForm = WaveForm.WAVEFORMDRAWTYPE.HalfMono
        WF.ColorMarker = Color.Black
        WF.DrawMarker = WaveForm.MARKERDRAWTYPE.Line Or WaveForm.MARKERDRAWTYPE.Name Or WaveForm.MARKERDRAWTYPE.NamePositionAlternate Or WaveForm.MARKERDRAWTYPE.NameBoxFillInverted
        WF.MarkerLength = 0.75
        If IO.File.Exists(tmpWaveFile) = True Then
            If WF.WaveFormLoadFromFile(tmpWaveFile, True) = True Then
                DrawWave()
            Else
                WF.RenderStart(True, BASSFlag.BASS_SAMPLE_FLOAT)
            End If
        Else
            ' it is important to use the same resolution flags as for the playing stream 
            ' e.g. if an already playing stream is 32-bit float, so this should also be 
            ' or use 'SyncPlayback' as shown below	
            WF.RenderStart(True, BASSFlag.BASS_SAMPLE_FLOAT)
        End If

    End Sub

    Private Sub MyWaveFormCallback(framesDone As Integer, framesTotal As Integer,
        elapsedTime As TimeSpan, finished As Boolean)
        ' will be called during rendering...		

        If finished = True Then
            ManualEnd = False
            CalcPintaWavePunts()
            ' if your playback stream uses a different resolution than the WF 
            ' use this to sync them
            WF.SyncPlayback(ActualPlay.AUDIO_HANDLE)
            If ActualPlay.AUDIO_ID > 0 Then
                Dim tmpWaveFile As String = IO.Path.ChangeExtension(ActualPlay.AUDIO_Path, ".wf")
                If IO.File.Exists(tmpWaveFile) = False Then
                    WF.WaveFormSaveToFile(tmpWaveFile, True)
                End If
            End If
        End If
        DrawWave()

    End Sub

    Private Sub DrawWave()
        If ActualPlay.DurationInSecons > MAX_SEC_WAVE Then
            BorrarWaveForn()
            Me.picWave.BackgroundImage = CreateImage(picWave, MSG_ERROR_WAVE_TOO_LONG)
            Exit Sub
        End If

        If Not (WF Is Nothing) Then
            Try
                Me.picWave.BackgroundImage = WF.CreateBitmap(Me.picWave.Width, Me.picWave.Height, -1, -1, False)
                'Dim bm As Bitmap = Me.picWave.BackgroundImage
                'Dim Path As String = "C:\Users\Marti\Documents\SharpDevelop Projects\MSC-Automatitzacio\recursos\Pictures\wave.jpg"
                'bm.Save (Path ,System.Drawing.Imaging.ImageFormat.Jpeg)
                Application.DoEvents()
            Catch ex As Exception
                Me.picWave.BackgroundImage = Nothing
            End Try
        Else
            'Me.picWave.BackgroundImage = Nothing 
        End If
    End Sub


    Sub PicWaveMouseClick(sender As Object, e As MouseEventArgs) Handles picWave.MouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Left And e.Clicks = 1 Then
            If PlayerPre.IsPlaying = True Then
                'Despalcem la preescolta
                Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
                'Posició on s'ha de desplaçar 
                Dim len As Long = Bass.BASS_ChannelGetLength(ActualPlay.AUDIO_HANDLE)
                Dim bpp As Double = len / CType(Me.picWave.Width, Double) ' bytes per pixel  
                Dim PosADespla As Single = Bass.BASS_ChannelBytes2Seconds(PlayerPre.PreEscoltaHandle, CLng(LocalMousePosition.X * bpp))
                'Desplaçar-ho
                Bass.BASS_ChannelSetPosition(PlayerPre.PreEscoltaHandle, PosADespla)
            Else
                PlayFromWave()
            End If
        End If
    End Sub

    Sub PlayFromWave()
        'estableix la posicio del ratolí a sobre el Pig
        '-----			
        If ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED And ActualPlay.AUDIO_HANDLE <> 0 Then
            Dim path As String = ""
            Try
                Dim IndexLlistat As Integer = findIndexActualPlay()
                path = Me.lstDisp.Items.Item(IndexLlistat).SubItems.Item(1).Tag
            Catch ex As Exception
                If Me.lstDisp.Items.Count = 0 Then Exit Sub
                path = Me.lstDisp.Items.Item(0).SubItems.Item(1).Tag
            End Try

            Select Case UCase(IO.Path.GetExtension(path))
                Case ".CDA"
                    ActualPlay.AUDIO_HANDLE = BassCd.BASS_CD_StreamCreateFile(path, BASSFlag.BASS_STREAM_AUTOFREE)
                Case Else ' .mp3 .wav
                    ActualPlay.AUDIO_HANDLE = Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_STREAM_AUTOFREE)
            End Select
            'Bass.BASS_ChannelPlay(ActualHandle,False)
            'Bass.BASS_ChannelPause(ActualHandle)			
        End If

        '-----				
        Dim len As Long = Bass.BASS_ChannelGetLength(ActualPlay.AUDIO_HANDLE)
        Dim bpp As Double = len / CType(Me.picWave.Width, Double) ' bytes per pixel        
        'Quin és el pixel on es troba el ratolí
        'Dim pr As Integer = 0
        Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
        'Posició on s'ha de desplaçar        
        Dim PosADespla As Single = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, CLng(LocalMousePosition.X * bpp))
        'Desplaçar-ho
        Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, PosADespla)
        If ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False)
        If IntroSegons > PosADespla Then blIntro = True

    End Sub

    Sub Pic_MouseDown(sender As Object, e As MouseEventArgs) Handles picWave.MouseUp, picWave.MouseDown
        If IsNothing(WF) Then Exit Sub
        Dim segpix As Double = ActualPlay.DurationInSecons / CType(picWave.Width, Double) ' bytes per pixel        
        'Quin és el pixel on es troba el ratolí

        Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
        'Posició on s'ha de desplaçar        		
        Dim LocationX As Integer = LocalMousePosition.X

        If e.Button = System.Windows.Forms.MouseButtons.Right Then

            If LocationX < (picWave.Width / 2) Then
                If LocationX < 2 Then LocationX = 2
                IntroSegons = LocationX * segpix
                If IntroSegons > 0 Then blIntro = True
                WF.AddMarker("INTRO", IntroSegons)

            Else
                If LocationX > Me.picWave.Width Then LocationX = Me.picWave.Width - 2
                OutSegons = LocationX * segpix
                WF.AddMarker("END", OutSegons)
                ManualEnd = True
            End If

        End If
        'movePig(MyPic,LocationX )
        DrawWave()
    End Sub

    Sub MnuWavePlayClick(sender As Object, e As EventArgs) Handles mnuWavePlay.Click
        PlayFromWave()
    End Sub

    Sub MnuWaveSetIntroClick(sender As Object, e As EventArgs) Handles mnuWaveSetIntro.Click
        If IsNothing(WF) Then Exit Sub
        'Dim MyPic As PictureBox = CType(sender, PictureBox)
        'Dim len As Long = Bass.BASS_ChannelGetLength(ActualHandle)
        Dim segpix As Double = ActualPlay.DurationInSecons / CType(picWave.Width, Double) ' bytes per pixel        
        'Quin és el pixel on es troba el ratolí

        Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
        'Posició on s'ha de desplaçar        		
        Dim LocationX As Integer = LocalMousePosition.X

        If LocationX < 2 Then LocationX = 2
        IntroSegons = LocationX * segpix
        If IntroSegons > 0 Then blIntro = True
        WF.AddMarker("INTRO", IntroSegons)

        DrawWave()
    End Sub

    Sub MnuWaveSetPointMixClick(sender As Object, e As EventArgs) Handles mnuWaveSetPointMix.Click
        If IsNothing(WF) Then Exit Sub

        Dim segpix As Double = ActualPlay.DurationInSecons / CType(picWave.Width, Double) ' bytes per pixel        
        'Quin és el pixel on es troba el ratolí

        Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
        'Posició on s'ha de desplaçar        		
        Dim LocationX As Integer = LocalMousePosition.X


        If LocationX > Me.picWave.Width Then LocationX = Me.picWave.Width - 2
        OutSegons = LocationX * segpix
        WF.AddMarker("END", OutSegons)
        ManualEnd = True

        DrawWave()
    End Sub

    Sub PicWaveResize(sender As Object, e As EventArgs) Handles picWave.Resize
        'DrawWave()
    End Sub

    Sub BorrarWaveForn()
        Me.picWave.BackgroundImage = Nothing
        IntroSegons = 0
        CuePosition = 0
        Dim segpix As Double = ActualPlay.DurationInSecons / CType(picWave.Width, Double)
        OutSegons = ActualPlay.DurationInSecons
    End Sub

#End Region

    Sub MnuPanicButtonClick(sender As Object, e As EventArgs) Handles mnuPanicButton.Click
        Dim ActualFitxer As String = ActualPlay.AUDIO_Path
        Dim actualPosition As Long = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
        Call Bass.BASS_Stop()
        Call Bass.BASS_Free()

        If Bass.BASS_Init(DEV_PLAY, 44100, Un4seen.Bass.BASSInit.BASS_DEVICE_DEFAULT, Me.Handle) = False Then
            MsgBox(MSG_ERROR_BASS_NO_INI, MsgBoxStyle.Critical, MSG_ATENCIO)
            Exit Sub
        End If
        ActualPlay.AUDIO_HANDLE = Bass.BASS_StreamCreateFile(ActualFitxer, 0, 0, 0)
        Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, actualPosition)
        Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False)

    End Sub

#Region "BOXCTL04"
    Dim BOXCTL04Port As String = ""

    Sub MnuBOXCTL04Click(sender As Object, e As EventArgs) Handles mnuBOXCTL04.Click
        Me.mnuAirence.Checked = False
        mnuBOXCTL04.Checked = Not mnuBOXCTL04.Checked
        If mnuBOXCTL04.Checked Then
            IniControlCom()
            'OpenFormControlCTL04()
        Else
            frmRemote.Close()
        End If
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "CTL04_ACTV", IIf(mnuBOXCTL04.Checked = True, 1, 0))
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "AIRENCE_ACTV", 0)
    End Sub

    Private Sub OpenFormControlCTL04()
        Dim NoLoad As Boolean = False
        For Each frm As Form In My.Application.OpenForms
            If frm.Name = "frmRemote" Then
                NoLoad = True
                Exit For
            End If
        Next
        If NoLoad = False Then frmRemote.Show()
    End Sub


    Private Sub refreshComPort(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mnuBOXCTL04.DropDownItems.Clear()
        LoadCOMPorts()
    End Sub

    Private Sub LoadCOMPorts()
        Dim FitxerINI As New IniFile
        ComPortCTL04 = FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "PORT_CTL04", "")
        Dim PortActive As Boolean = False
        Dim PortCount As Integer = 0
        For Each sp As String In My.Computer.Ports.SerialPortNames
            PortCount += 1
            Dim mnuNewCOMPortCTL04 As ToolStripMenuItem
            mnuNewCOMPortCTL04 = New ToolStripMenuItem
            mnuNewCOMPortCTL04.Name = "mnuComPort" & sp
            mnuNewCOMPortCTL04.Text = sp
            mnuNewCOMPortCTL04.Visible = True
            mnuNewCOMPortCTL04.Checked = (ComPortCTL04 = sp)
            mnuBOXCTL04.DropDownItems.Add(mnuNewCOMPortCTL04)
            AddHandler mnuNewCOMPortCTL04.Click, AddressOf AssigComPort
            If mnuNewCOMPortCTL04.Checked = True Then PortActive = True
        Next

        mnuBOXCTL04.Checked = CBool(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "CTL04_ACTV", 0))
        If mnuBOXCTL04.Checked AndAlso PortActive = True Then
            'Obre la pantalla de control
            IniControlCom()
        Else
            mnuBOXCTL04.Enabled = PortCount > 0
        End If
    End Sub

    Private Sub getVersionCTL04()
        Dim strInfo As String
        If ConnectSerialPort(strInfo) = True Then
            Dim mnuVersionCTL04 As ToolStripMenuItem
            mnuVersionCTL04 = New ToolStripMenuItem
            mnuVersionCTL04.Name = "mnuVersionCTL04"
            mnuVersionCTL04.Text = getVersionBox()
            mnuVersionCTL04.Visible = True
            mnuBOXCTL04.DropDownItems.Add(mnuVersionCTL04)
        End If
        'MsgBox(strInfo, MsgBoxStyle.Critical, MSG_ATENCIO)

    End Sub

    Private Sub AssigComPort(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyMnu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        For i As Integer = 0 To mnuBOXCTL04.DropDownItems.Count - 1
            CType(mnuBOXCTL04.DropDownItems(i), ToolStripMenuItem).Checked = False
        Next
        MyMnu.Checked = True
        ComPortCTL04 = MyMnu.Text
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "PORT_CTL04", ComPortCTL04)
        IniControlCom()
    End Sub

    Private Sub IniControlCom()
        MySerialPort = New SerialPort()
        'Primer consultem la versió
        getVersionCTL04()
        OpenFormControlCTL04()
    End Sub

#End Region

#Region "Airence"

    Sub MnuAirenceClick(sender As Object, e As EventArgs) Handles mnuAirence.Click
        Me.mnuBOXCTL04.Checked = False
        Me.mnuAirence.Checked = Not Me.mnuAirence.Checked
        If Me.mnuAirence.Checked = True Then
            MyMixer = Nothing
            MyMixer = New Airence()
            If MyMixer.myDeviceDetected = True Then frmAirence.Show()
            mnuAirence.Checked = MyMixer.myDeviceDetected
            mnuAirenceMap.Enabled = MyMixer.myDeviceDetected
        Else
            If MyMixer.myDeviceDetected = True Then frmAirence.Close()
            MyMixer = Nothing
        End If
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "AIRENCE_ACTV", IIf(mnuAirence.Checked = True, 1, 0))
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "CTL04_ACTV", 0)
    End Sub

    Sub LoadAirence()
        Dim FitxerINI As New IniFile
        Me.mnuAirence.Checked = CBool(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "AIRENCE_ACTV", 0))
        If Me.mnuAirence.Checked = True Then
            MyMixer = Nothing
            MyMixer = New Airence()

            If MyMixer.myDeviceDetected = True Then
                Me.mnuAirence.Checked = True
                Me.mnuAirence.Enabled = True
                Dim mnuVersioAirence As ToolStripMenuItem
                mnuVersioAirence = New ToolStripMenuItem
                mnuVersioAirence.Name = "mnuVersioAirence"

                mnuVersioAirence.Text = "Version lib: " & MyMixer.airenceGetLibraryVersion(0, 0)
                mnuVersioAirence.Visible = True
                mnuAirence.DropDownItems.Add(mnuVersioAirence)

                Dim mnuFirmwareAirence As ToolStripMenuItem
                mnuFirmwareAirence = New ToolStripMenuItem
                mnuFirmwareAirence.Name = "mnuFirmwareAirence"
                mnuFirmwareAirence.Text = "Firmware mixer: " & MyMixer.airenceGetFirmwareVersion(0, 0)
                mnuFirmwareAirence.Visible = True
                mnuAirence.DropDownItems.Add(mnuFirmwareAirence)
                frmAirence.Show()
            Else
                Me.mnuAirence.Enabled = False
                'If MyAPP.CtlDebug = True Then frmAirence.show()
            End If
        End If
    End Sub


#End Region

    Sub MnuInsertStopClick(sender As Object, e As EventArgs) Handles mnuInsertStop.Click
        addElementlist(Tipus_Play.CTL_META_STOP, "STOP", Params.NomRadio, "stop.stop", 0, "00:00:00", #12:00:00 AM#, 0)
    End Sub

    Sub MnuColumnInterpClick(sender As Object, e As EventArgs) Handles mnuColumnInterp.Click
        mnuColumnInterp.Checked = Not mnuColumnInterp.Checked
        If mnuColumnInterp.Checked = True Then
            Me.ColumnHeader22.Width = 119
        Else
            Me.ColumnHeader22.Width = 0
        End If
    End Sub

    Sub MnuColumnDuradaClick(sender As Object, e As EventArgs) Handles mnuColumnDurada.Click
        mnuColumnDurada.Checked = Not mnuColumnDurada.Checked
        If mnuColumnDurada.Checked = True Then
            Me.ColumnHeader23.Width = 60
        Else
            Me.ColumnHeader23.Width = 0
        End If
    End Sub

    Sub MnuColumnHoraClick(sender As Object, e As EventArgs) Handles mnuColumnHora.Click
        mnuColumnHora.Checked = Not mnuColumnHora.Checked
        If mnuColumnHora.Checked = True Then
            Me.ColumnHeader24.Width = 62
        Else
            Me.ColumnHeader24.Width = 0
        End If
    End Sub

    Sub MnuColumnVRitmeClick(sender As Object, e As EventArgs) Handles mnuColumnVRitme.Click
        mnuColumnVRitme.Checked = Not mnuColumnVRitme.Checked
        If mnuColumnVRitme.Checked = True Then
            Me.columnHeader1.Width = 45
        Else
            Me.columnHeader1.Width = 0
        End If
    End Sub

    Sub Panel2SizeChanged(sender As Object, e As EventArgs)
        DrawWave()
    End Sub

    Sub SplitContainer2SizeChanged(sender As Object, e As EventArgs)
        DrawWave()
    End Sub


    Sub PichToolStripMenuItemClick(sender As Object, e As EventArgs) Handles pichToolStripMenuItem.Click
        UP_Pitch()
    End Sub

    Sub PichToolStripMenuItem1Click(sender As Object, e As EventArgs) Handles pichToolStripMenuItem1.Click
        DOWN_Pitch()
    End Sub

    Friend Sub UP_Pitch()
        Dim valPith As Integer = sldPith.Value + 1000
        If valPith > sldPith.Maximum Then valPith = sldPith.Maximum
        sldPith.Value = valPith
        canviPith()
    End Sub

    Friend Sub DOWN_Pitch()
        Dim valPith As Integer = sldPith.Value - 1000
        If valPith < sldPith.Minimum Then valPith = sldPith.Minimum
        sldPith.Value = valPith
        canviPith()
    End Sub

    Friend Sub RemoteSetFFPosition()
        Dim pos As Long = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
        'Desplaçar-ho
        Bass.BASS_ChannelSetPosition(PlayerPre.PreEscoltaHandle, pos + 1000)
    End Sub

    Friend Sub RemoteSetRWPosition()
        Dim pos As Long = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
        'Desplaçar-ho
        Bass.BASS_ChannelSetPosition(PlayerPre.PreEscoltaHandle, pos - 1000)
    End Sub

    '	Private Delegate Sub sincEndFormDelegate()
    '	Private sincEndFormDelegateDisplay As sincEndFormDelegate
    '	
    '	Private Sub EndSync(ByVal handle As Integer, ByVal channel As Integer,ByVal data As Integer, ByVal user As IntPtr)		
    '		If channel = ActualHandle Then
    '			sincEndFormDelegateDisplay = New sincEndFormDelegate(AddressOf UpdateDisplay)
    '			Me.Invoke(sincEndFormDelegateDisplay) 'call the delegate
    '		End If					
    '	End Sub
    '	
    '	Private Sub UpdateDisplay()
    '		If Me.chkLoop.Checked = True Then
    '			' the 'channel' has ended - jump to the beginning
    '			'Bass.BASS_ChannelSetPosition(ActualHandle, 0)
    '			Exit Sub
    '		End If
    '		'AutoLoadPause
    '		Dim listID As Integer = ActualPlay.AUDIO_ListID + 1
    '		If IsNothing(ActualPlay.AUDIO_Path) Then listID = 0			
    '		'si és un STOP (tag=100) ho descartem
    '		If lstDisp.Items.Count > 0 _
    '			AndAlso listID <= (lstDisp.Items.Count - 1) _
    '			AndAlso lstDisp.Items.Item(listID).Tag <> 100 _ 
    '			AndAlso ManualStop = False Then
    '			Bass.BASS_SetDevice(DEV_PLAY)
    '			Bass.BASS_ChannelStop(ActualHandle)
    '			PlayPauseFitxer(listID, False,False, True)	 
    '		End If
    '	End Sub

    Sub PicWaveSizeChanged(sender As Object, e As EventArgs) Handles picWave.SizeChanged
        DrawWave()
    End Sub

    Sub TxtPlayURLKeyDown(sender As Object, e As KeyEventArgs) Handles txtPlayURL.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim SubTitle As String = ""
            Dim URL As String = Me.txtPlayURL.Text
            If UCase(Microsoft.VisualBasic.Right(URL, 3)) = "PLS" Then
                Dim FitxerINI As New IniFile
                URL = FitxerINI.INIRead(URL, "playlist", "File1", "")
                SubTitle = FitxerINI.INIRead(URL, "playlist", "Title1", "")
            End If

            Dim channel As Integer = Bass.BASS_StreamCreateURL(URL, 0, BASSFlag.BASS_DEFAULT, Nothing, IntPtr.Zero)
            If channel <> 0 Then
                Dim tagInfo As New AddOn.Tags.TAG_INFO(URL)
                Dim Title As String = "Play Stream"
                SubTitle = Params.NomRadio
                If BassTags.BASS_TAG_GetFromURL(channel, tagInfo) Then
                    Title = tagInfo.title & " - " & tagInfo.artist
                    SubTitle = tagInfo.album
                End If
                URL = URL & ".STREAM"

                addElementlist(Tipus_Play.CTL_URL_STREAM, Title, SubTitle, URL, 0, "00:00:00", "00:00:00", 0)
                My.Settings.AutoCompleteList.Add(Me.txtPlayURL.Text)
                My.Settings.Save()
                txtPlayURL.AutoCompleteCustomSource.Add(Me.txtPlayURL.Text)
            Else
                'Error URL no valida				
                MsgBox(MSG_ERROR_LOAD_URL, MsgBoxStyle.Critical, MSG_ATENCIO)
            End If
        End If
    End Sub

    Sub AirenceMapToolStripMenuItemClick(sender As Object, e As EventArgs) Handles mnuAirenceMap.Click
        frmAirence.Show()
    End Sub

    Sub MnuSpeedClick(sender As Object, e As EventArgs) Handles mnuSpeedSlow.Click, mnuSpeedNormal.Click, mnuSpeedFast.Click
        'Dim Mymnu As New ToolStripMenuItem = CType (sender,ToolStripMenuItem)
        For i As Integer = 0 To mnuSpeed.DropDownItems.Count - 1
            CType(mnuSpeed.DropDownItems(i), ToolStripMenuItem).Checked = False
        Next

        CType(sender, ToolStripMenuItem).Checked = True
        Select Case CType(sender, ToolStripMenuItem).Tag
            Case 0 : TimeFadeOut = 5000
            Case 1 : TimeFadeOut = 3000
            Case 2 : TimeFadeOut = 1000
        End Select

    End Sub

    Sub MnuAutoSincroStartPlayClick(sender As Object, e As EventArgs) Handles mnuAutoSincroStartPlay.Click, mnuAutoSincroEndPlay.Click

        mnuAutoSincroStartPlay.Checked = False
        mnuAutoSincroEndPlay.Checked = False
        CType(sender, ToolStripMenuItem).Checked = True

        mnuAutoSincro59.Enabled = mnuAutoSincroEndPlay.Checked
        mnuAutoSincro29.Enabled = mnuAutoSincroEndPlay.Checked

    End Sub
End Class
