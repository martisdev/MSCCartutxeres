Option Strict On
Option Explicit On


Imports BassCd = Un4seen.Bass.AddOn.Cd.BassCd
Imports BassTags = Un4seen.Bass.AddOn.Tags.BassTags
Imports Un4seen.Bass
Imports MySql.Data.MySqlClient
Imports System.IO

Module mdlCartutx
	Friend MyAPP As MSC.Control.MSC_Aplic
	Friend Usuari As MSC.User
	Friend Params As Parametres.clsParams    
	Friend PlayerPre As PlayerPreEscolta
    Friend lang As MSC.UserLanguage
    Friend Cloud As msc_cloud.Cloud = New msc_cloud.Cloud()
    Friend STANDALONE As Boolean = False
    Friend Const BETA_VERSION As Boolean = True ' provar CTL04 box i Airence
    Friend ON_AIR As Boolean = True

    Enum TypeShow
        SHOW_LIST = 0
        SHOW_BUTTONS = 1
    End Enum
    Enum TypePlay
        PLAY_STEP = 0
        PLAY_CUNTINUOS = 1
        PLAY_LOOP = 2
    End Enum

    Friend Structure ListAudioSelect
        'Valors de la cançó
        Dim AUDIO_TipFitxer As Integer
        Dim AUDIO_ID As Integer
        Dim AUDIO_Path As String
        Dim AUDIO_Titol As String
        Dim AUDIO_SubTitol As String
        Dim AUDIO_Durada As Date
        Dim DurationInSecons As Double
        Dim ElapseInSecons As Double
        Dim DurationTime As Long
        Dim ElapseTime As Long
        Dim RemainTime As Long
        Dim RemainInSecons As Double
        Dim AUDIO_Freq As Integer
        Dim AUDIO_Radiat As Boolean
        Dim AUDIO_HoraRadi As Date
        Dim AUDIO_ListID As Integer
        Dim AUDIO_BPM As Integer
        Dim AUDIO_HANDLE As Integer

        Dim LoopIn As Long
        Dim LoopOut As Long

        Dim IntroSegons As Double
        Dim OutSegons As Double
        Dim CuePosition As Double
        Dim gain_factor As Single
        Dim peak_db As Single

        Dim isActv As Un4seen.Bass.BASSActive
        Dim Load As Boolean
        Dim ForcePause As Boolean
    End Structure
	
	Friend listAudioFromDBS() As ListAudioSelect = Nothing

    Friend Enum Formthemes
        light = 0
        dark = 1
    End Enum

    Friend MyThemeForm As Formthemes

    'Friend ReOrdCart As Boolean = False

    Friend MoveFromHistory As Boolean
	Friend MoveFromExplorer As Boolean 
	Friend ActualFrmExplorer As frmFileExplorer
    Friend cartOrigenMove As frmCartutxera
    Friend DragLVDisp As System.Windows.Forms.ListViewItem 'The item being dragged
	Friend dsClient As New DataSet
	
	Friend ProgEditAudio As String = ""	
	
	Friend InxfrmOnBotonera As Integer = 0

    Friend ButtonsDataObject As DataObject
    Friend TypeButtonsDataObject As Type = GetType(MetroFramework.Controls.MetroTile)
    Friend MoveInterPlayers As Boolean

    Friend Sub IniDataSet()
		Dim SqlStr As String = ""
        Dim db As New MSC.dbs(Cloud)
        Dim da As MySqlDataAdapter 
		
		'style_relationships
		SqlStr = "SELECT `style_relationships_tipfitxer`,`style_relationships_fitxer`,`style_relationships_style`,`estils`.`estil_id`,`estil_nom` FROM `style_relationships`, `estils` where `style_relationships`.`style_relationships_style`=`estils`.`estil_id`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "style_relationships")		
		'Taula interprets
		SqlStr = "SELECT * FROM `interprets`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "interprets")
		'temes()
		SqlStr = "SELECT * FROM `temes`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "temes")

        'discos
        SqlStr = "SELECT * FROM `discos`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "discos")
		'estil
		SqlStr = "SELECT * FROM `estils`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "estils")
		'idiomes
		SqlStr = "SELECT * FROM `idiomes`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "idiomes")
		'subjectiv
		SqlStr = "SELECT * FROM `subjectiv`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "subjectiv")
		
		'clients
		SqlStr = "SELECT * FROM `clients`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "clients")
		
		'locutor
		SqlStr = "SELECT * FROM `locutor`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "locutor")
		
		'loops
		SqlStr = "SELECT * FROM `loops`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "loops")
		
		'user_temes
		SqlStr = "SELECT * FROM `user_temes`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "user_temes")
		
		'user_audio
		SqlStr = "SELECT * FROM `user_audio`;"
		da = db.getAdapter(SqlStr)
		da.Fill(dsClient, "user_audio")

        'programes
        SqlStr = "SELECT * FROM `programes`;"
        da = db.getAdapter(SqlStr)
        da.Fill(dsClient, "programes")

        'listaudio
        SqlStr = "SELECT * FROM `listaudio`;"
        da = db.getAdapter(SqlStr)
        da.Fill(dsClient, "listaudio")

        da.Dispose()
		db = Nothing
		
	End Sub
	
	Friend Sub RefreshDataSet(ByVal IdTaula As TAULES_DBS)
		Dim SqlStr As String = ""
		Dim NomTaula As String = ""
		Select Case IdTaula
			Case TAULES_DBS.TAULA_USER_AUDIO
				SqlStr = "SELECT * FROM `user_audio`;"
				NomTaula = "user_audio"
		End Select
        Dim db As New MSC.dbs(Cloud)
        Dim da As New MySqlDataAdapter
		
		dsClient.Tables(NomTaula).Clear()
		Try
			da = db.getAdapter(SqlStr)
			da.Fill(dsClient, NomTaula)
		Catch ex As Exception
			MyAPP.Error_MSC.SalvarExcepcioNoControlada(ex, SqlStr, False)
		Finally
			da.Dispose()
			db = Nothing
		End Try
	End Sub
	
	'get file name from file path
	Friend Function GetFileName(ByVal fp As String, Optional ByRef DelTpus As Boolean = False) As String
		GetFileName = Mid(fp, InStrRev(fp, "\") + 1)
		If DelTpus Then GetFileName = Left(GetFileName, Len(GetFileName) - 4)
	End Function
	
	Friend Class PlayerPreEscolta
		
		Friend PreEscoltaHandle As Integer		
		dim IdDevicePre As Short = 1
		
		Public Sub New(DeviceID As Short)
			IdDevicePre = DeviceID
			DeviceAudioEnable = IniBASS_CTL(IdDevicePre,IntPtr.Zero )
			
		End Sub
		
		Friend Sub PlayAudio(ByVal PathFitxer As String)
            Bass.BASS_SetDevice(IdDevicePre)
            Select Case UCase(IO.Path.GetExtension(PathFitxer))
				Case ".CDA"
					PreEscoltaHandle = BassCd.BASS_CD_StreamCreateFile(PathFitxer, BASSFlag.BASS_STREAM_AUTOFREE )
				Case Else ' .mp3 .wav .ogg
					PreEscoltaHandle = Bass.BASS_StreamCreateFile(PathFitxer, 0, 0,  BASSFlag.BASS_DEFAULT)
			End Select	
			
			If Bass.BASS_ChannelPlay(PreEscoltaHandle, False) = False Then
				'beep
				'  Error de Play
			End If
			
		End Sub
		
		Friend Function IsPlaying() As Boolean
			'Call Bass.BASS_SetDevice(IdDevicePre)
			Dim PreActv As Un4seen.Bass.BASSActive = CType(Bass.BASS_ChannelIsActive(PreEscoltaHandle), BASSActive)
			If PreActv = BASSActive.BASS_ACTIVE_PLAYING Then
				Return True
			Else
				Return False
			End If
			
		End Function
		
		Friend Sub PintaPicPreEscolta(ByRef PicBox As PictureBox, ByVal Atack As Integer)
			
			Static VolMax As Integer
			Dim vol As Integer
			Dim RealVol As Integer
			Dim LVol As Integer
			Dim RVol As Integer
			Call Bass.BASS_SetDevice(IdDevicePre)
			vol = Bass.BASS_ChannelGetLevel(PreEscoltaHandle)
			If vol < 0 Then vol = 0
			LVol = Un4seen.Bass.Utils.HighWord(vol)
			RVol = Un4seen.Bass.Utils.LowWord(vol)
			RealVol = CInt((LVol + RVol) / 2) 'Volum màxim = 32768
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
			Dim bit As Bitmap = New Bitmap(PicBox.Width, PicBox.Height)
			Dim graph As Graphics = Graphics.FromImage(bit)
			Dim LimePen As New Pen(Color.Lime, 5)
			Dim PeakPen As New Pen(Color.Red, 5)
			Dim thresholdPen As New Pen(Color.Blue, 5)
			Dim Y As Integer
			graph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias
			
			' Pinta Volum dreta i esquerra
			Y = CInt((RVol / 32768) * PicBox.Height)
			If Y > PicBox.Height Then Y = PicBox.Height
			graph.DrawLine(LimePen, 2, PicBox.Height, 2, PicBox.Height - Y)
			Y = CInt((LVol / 32768) * PicBox.Height)
			If Y > PicBox.Height Then Y = PicBox.Height
			graph.DrawLine(LimePen, 8 + 2, PicBox.Height, 8 + 2, PicBox.Height - Y)
			
			If VolMax > 0 Then
				'Pinta peak
				Y = CInt((VolMax / 32768) * PicBox.Height)
				If Y > PicBox.Height Then Y = PicBox.Height
				graph.DrawLine(PeakPen, 2, PicBox.Height - Y, 2, PicBox.Height - Y - 2)
				graph.DrawLine(PeakPen, 8 + 2, PicBox.Height - Y, 8 + 2, PicBox.Height - Y - 2)
			End If
			'draw the visual onto the picturebox
			PicBox.Image = bit
			Try
				LimePen.Dispose()
				PeakPen.Dispose()
				thresholdPen.Dispose()
				graph.Dispose()
				
			Catch ex As Exception
			Finally
				LimePen = Nothing
				PeakPen = Nothing
				thresholdPen = Nothing
				bit = Nothing
				graph = Nothing
			End Try			
		End Sub
		
		Friend ReadOnly Property DeviceID() As Short
			Get
				Return IdDevicePre
			End Get
		End Property
		
		Dim cart_index As Integer 
		Friend  Property numCart() As Integer 
			Get
				Return cart_index
				
			End Get
			Set(ByVal Value As Integer)
				cart_index = Value
			End Set
		End Property
		
		
		Friend Sub StopAudio()
			Bass.BASS_ChannelStop(PreEscoltaHandle)
			Bass.BASS_StreamFree(PreEscoltaHandle)
			PreEscoltaHandle = 0
		End Sub
		
	End Class
	
	Friend LIST_NO_DEF As String 
	Friend LIST_CAP As String
	friend MSG_ATENCIO As String =  ""' "Atenció"
	Friend MSG_ERROR_CONNECT_DBS_STANTALONE As String 
	Friend MSG_ALTERNATIVE_DIR As String
	Friend LABEL_PREESCOLTA_STOP As String
    Friend MSG_STOP_AND_EXIT As String = ""

    Friend Sub setLanguageGlobal()
        MSG_STOP_AND_EXIT = lang.getText("MSG_STOP_AND_EXIT", True) ' primer para el reproductor
        LABEL_PREESCOLTA_STOP = lang.getText("LABEL_PREESCOLTA_STOP", True)
        MSG_ATENCIO = lang.getText("MSG_ATENCIO",True)' "Atenció"
		LIST_CAP = lang.getText("VALUE_CAP",True) '<CAP>
		LIST_NO_DEF = lang.getText("LIST_NO_DEF",True) 'NO DEF.	
		MSG_ERROR_CONNECT_DBS_STANTALONE = lang.getText("MSG_ERROR_CONNECT_DBS_STANTALONE",True) '"No s'ha pogut connectar amb la base de dades, vols iniciar sense connexió?".	
        MSG_ALTERNATIVE_DIR = lang.getText("MSG_ALTERNATIVE_DIR", True) '"Directori alternatiu"
        mdlFrontEnd.setLanguage()
    End Sub
	
	Friend sub TestStandAlone()
        For i As Integer = 0 To My.Application.CommandLineArgs.Count - 1
            Dim PathCommandLine As String = My.Application.CommandLineArgs.Item(i)
            If PathCommandLine.ToUpper = "STANDALONE" Then
                STANDALONE = True
                Exit Sub
            End If
        Next i
        If Not IO.File.Exists(MyAPP.IniFile) Then
            STANDALONE = True
        End If
    End Sub

    ''' <summary>
    ''' Per saber si hi ha alguna Cartutxera en reprodució
    ''' </summary>
    ''' <returns></returns>
    Friend Function SomeFormPlay() As Boolean
        For Each frm As Form In My.Application.OpenForms
            If frm.Name = "frmCartutxera" Then
                Dim formCart As frmCartutxera = CType(frm, frmCartutxera)
                Select Case formCart.ActualPlay.isActv
                    'Case BASSActive.BASS_ACTIVE_PAUSED : Return True
                    Case BASSActive.BASS_ACTIVE_PLAYING : Return True
                End Select
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' retorna el form de la cartutxera segons l'ID 
    ''' </summary>
    ''' <returns></returns>
    Friend Function getFormCart(inx As Integer) As frmCartutxera
        Dim IDCart As String = inx.ToString
        Dim frm As Form

        For Each frm In My.Application.OpenForms
            If frm.Name = "frmCartutxera" And frm.Text.Contains("Cart " & IDCart) Then
                Return CType(frm, frmCartutxera)
            End If
        Next
    End Function

    Friend Function getOrdreAudioUser(id_user As Integer, id_audio As Integer) As Integer
        Dim colec() As DataRow = dsClient.Tables("user_audio").Select("user_id=" & id_user & " AND audio_id= " & id_audio)
        Try
            Return CInt(colec(0)("order"))
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Friend Function IsAudioForUser(ByVal AudioID As Integer, ByVal IDUsuari As Integer) As Boolean
        If Usuari.UsrID = 0 Then Return True
        If Usuari.UsrID > 0 Then
            Dim StrSql As String = "user_id=" & IDUsuari & " AND audio_id=" & AudioID
            Dim colec() As DataRow = dsClient.Tables("user_audio").Select(StrSql, "user_id")
            If colec.Length = 0 Then
                Return False
            Else
                Return CBool(colec(0)("visible"))
            End If
        End If
        Return True
    End Function

    Friend Function getImageDiscFromTema(tema_id As Integer) As Image
        Dim db As New MSC.dbs(Cloud)
        Dim StrSql As String = "SELECT img_image FROM image_discos,temes  WHERE tema_disc= img_disc And tema_id=" & tema_id & " ;"
        Try
            Return GetImageFromByteArray(CType(db.ExecuteScalar(StrSql), Byte()))
        Catch ex As Exception
            Return Nothing
        End Try

        'db = Nothing
    End Function

    Friend Function getImagePrograma(Session_id As Integer) As Image
        Dim db As New MSC.dbs(Cloud)
        Dim StrSql As String = "SELECT prg_imatge FROM programes ,prg_sessions WHERE `programes`.`prg_id` = `prg_sessions`.`ses_prg` AND ses_id = " & Session_id & " ;"
        Try
            Return GetImageFromByteArray(CType(db.ExecuteScalar(StrSql), Byte()))
        Catch ex As Exception
            Return Nothing
        End Try

        'db = Nothing
    End Function

    Friend Function getLogoAudioUser(id As Integer) As Integer
        Dim colec() As DataRow = dsClient.Tables("listaudio").Select("audio_id=" & id)
        Try
            Return CInt(colec(0)("audio_image"))
        Catch ex As Exception
            Return 2
        End Try
    End Function

End Module
