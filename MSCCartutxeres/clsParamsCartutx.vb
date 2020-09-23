Option Strict On
Option Explicit On
Imports System
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Xml 
Imports System.Text

Namespace Parametres
	
	Friend Class clsParams
		Friend blModif As Boolean = False
		Dim param_PathArrelAudios As String
		Dim param_PathAlternativeAudios As String
		Dim IsPathAlternative As Boolean = False
		Dim param_ActvSrvEmergenci As Boolean
		Dim param_ErrEmail As String
        Dim param_SendToDevelop As Boolean

        Dim param_PathProgrames As String
		Dim param_PathSenyalHora As String
		Dim param_CtlUsrCartut As Boolean
		Dim param_NomRadio As String
        Dim param_PathMusica As String
        Dim param_PathPublicitat As String
        Dim param_PathDefPauta As String
        Dim param_NTopHit As Short
		Dim param_NHitOld As Short
		Dim param_PathAudioUser As String
		Dim param_ActvTabProgram As Boolean
		Dim param_Threshold As Integer
		Dim param_Attack As Short
		Dim param_VolNormalize As Integer
		Dim param_VolIni As Short
        Dim param_SegActivate As Short

        Dim param_TimeRefrestInstruc As Short
        Dim param_NomsCarpetaAudiosUser As String

        'Idioma interface
        Dim param_Lang As String
		'cloud
		Dim param_ClientKey As String
        Dim param_ClientID As Integer
        Dim param_OnLine As Boolean = False

        Dim param_LogoEmpresa As Image

        Sub New( Optional StandAlone As Boolean= False )
			If StandAlone = True Then
				IniParams_StAl
			Else
				IniParams()	
			End If
			
		End Sub
		
		Private Sub IniParams_StAl()
			param_ActvSrvEmergenci = False
			param_ErrEmail = ""
            param_SendToDevelop = False

            param_TimeRefrestInstruc = 15000

            param_PathArrelAudios = ""
            param_PathProgrames = ""
			param_PathMusica = ""

            param_PathPublicitat = ""
            Dim PathPautes As String = Environment.GetFolderPath(Environment.SpecialFolder.Personal)

            param_PathDefPauta = PathPautes
            param_PathAudioUser = ""

			param_PathSenyalHora = ""
			param_CtlUsrCartut = False
			param_NomRadio = "MSC Automatització"
            param_LogoEmpresa = Nothing

            param_NTopHit = 0
			param_NHitOld = 0
			param_ActvTabProgram = False
			param_Threshold = 6466
			param_Attack = 55
			param_VolNormalize = 29742
			param_VolIni = 100
            param_SegActivate = 6

            ' idioma interficie
            param_Lang = My.Settings.Lang

            param_OnLine = False
        End Sub

        Private Sub IniParams()
            ' Carreguem totes les variables de les taules paramteres i parametres 2
            Dim db As New MSC.dbs(Cloud)
            Dim dt As DataTable = db.getTable("SELECT params_valor FROM config_params ;")
            'Comuns per totes les aplicacions
            param_ActvSrvEmergenci = CBool(dt.Rows(CONFIG.paramActvSrvEmergenci - 1)("params_valor"))
            param_ErrEmail = StripSlashes(dt.Rows(CONFIG.paramErrEmail - 1)("params_valor").ToString)
            param_SendToDevelop = CBool(dt.Rows(CONFIG.paramSendToDevelop - 1)("params_valor"))

            param_TimeRefrestInstruc = 15000

            Dim FitxerINI As New IniFile
            ON_AIR = CBool(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "ONAIR", "1"))
            If STANDALONE = True Then ON_AIR = False
            param_PathAlternativeAudios = FitxerINI.INIRead(MyAPP.IniFile, "Data Control", "AlternativeDir", "")
            param_PathArrelAudios = StripSlashes(dt.Rows(CONFIG.paramPathArrelAudios - 1)("params_valor").ToString)
            If IO.Directory.Exists(param_PathArrelAudios) = False Then

                If IO.Directory.Exists(param_PathAlternativeAudios) = True Then
                    param_PathArrelAudios = param_PathAlternativeAudios
                    IsPathAlternative = True
                End If
            End If

            Dim separador As String = "\"
            If param_PathArrelAudios.IndexOf("\") = -1 Then separador = "/"

            param_PathProgrames = param_PathArrelAudios & separador & DIR_PROGRAMES & separador
            param_PathMusica = param_PathArrelAudios & separador & DIR_MUSICA & separador
            param_PathPublicitat = param_PathArrelAudios & separador & DIR_PUBLICITAT & separador
            param_PathDefPauta = param_PathArrelAudios & separador & DIR_PAUTES & separador
            param_PathAudioUser = param_PathArrelAudios & separador & DIR_AUDIOUSER & separador

            param_PathSenyalHora = StripSlashes(dt.Rows(CONFIG.paramSenyalHoraria - 1)("params_valor").ToString)
            param_CtlUsrCartut = CBool(dt.Rows(CONFIG.paramCtlUsrCartut - 1)("params_valor"))
            param_NomRadio = StripSlashes(dt.Rows(CONFIG.paramNomClient - 1)("params_valor").ToString)

            param_NomsCarpetaAudiosUser = StripSlashes(dt.Rows(CONFIG.paramNomsCarpetaAudiosUser - 1)("params_valor").ToString)

            'load image Logo
            Try
                Dim bytes() As Byte = Convert.FromBase64String(dt.Rows(CONFIG.paramLogoEmpresa - 1)("params_valor").ToString) ' strData would come from your CSV file
                Dim MS As New System.IO.MemoryStream(bytes)
                param_LogoEmpresa = Image.FromStream(MS)
            Catch ex As Exception
                param_LogoEmpresa = Nothing
            End Try

            param_NTopHit = CShort(dt.Rows(CONFIG.paramNTopHit - 1)("params_valor"))
            param_NHitOld = CShort(dt.Rows(CONFIG.paramNHitOld - 1)("params_valor"))
            param_ActvTabProgram = CBool(dt.Rows(CONFIG.paramActvTabProgram - 1)("params_valor"))
            param_Threshold = CInt(dt.Rows(CONFIG.paramThreshold - 1)("params_valor"))
            param_Attack = CShort(dt.Rows(CONFIG.paramAttack - 1)("params_valor"))
            param_VolNormalize = CInt(dt.Rows(CONFIG.paramVolNormalize - 1)("params_valor"))
            param_VolIni = CShort(dt.Rows(CONFIG.paramVolIni - 1)("params_valor"))


            ' idioma interficie
            param_Lang = dt.Rows(CONFIG.paramLang - 1)("params_valor").ToString

            'cloud
            param_OnLine = CBool(dt.Rows(CONFIG.paramOnLine - 1)("params_valor"))
            param_ClientID = CInt(dt.Rows(CONFIG.paramClientID - 1)("params_valor"))
            Dim StrSql As String = "SELECT AES_DECRYPT(UNHEX(params_valor),'" & my_secret_key_to_encrypt & "') as psw FROM config_params WHERE params_id = " & CONFIG.paramClientKey
            Try
                param_ClientKey = Encoding.ASCII.GetString(CType(db.ExecuteScalar(StrSql), Byte()))
            Catch ex As Exception
                param_ClientKey = ""
            End Try
            If param_OnLine = True AndAlso param_ClientKey.Length > 0 Then
                Try
                    Dim user As String = getMD5Hash(param_NomRadio)
                    Cloud.Register(param_ClientKey, user, param_Lang, GENERAL_VERSION)
                Catch ex As Exception
                    param_OnLine = False
                    param_ClientKey = ""
                    param_ClientID = -1
                End Try
            End If
            blModif = False
            db = Nothing
        End Sub

        Friend ReadOnly Property OnLine() As Boolean
            Get
                Return param_OnLine
            End Get
        End Property

        Friend ReadOnly Property SendToDevelop() As Boolean
            Get
                Return param_SendToDevelop
            End Get
        End Property

        Friend ReadOnly Property LogoEmpresa() As Image
            Get
                Return param_LogoEmpresa
            End Get
        End Property

        Friend ReadOnly Property IsAlternativeDir() As Boolean			
			Get
				Return IsPathAlternative
			End Get
		End Property	
		
		Friend Property Lang() As String
			Get
				Return param_Lang
			End Get
			Set(ByVal Value As String)
				blModif = True
				param_Lang = Value
			End Set
		End Property
		
		Friend ReadOnly Property ActvSrvEmergenci() As Boolean
			Get
				Return param_ActvSrvEmergenci
			End Get
		End Property
		
		Friend ReadOnly Property ErrEmail() As String
			Get
				Return param_ErrEmail
			End Get
		End Property
		
		
		Friend ReadOnly Property PathProgrames() As String
			Get
				Return param_PathProgrames
			End Get
		End Property
		
		Friend ReadOnly Property PathSenyalHora() As String
			Get
				Return param_PathSenyalHora
			End Get
		End Property
		
		Friend ReadOnly Property CtlUsrCartut() As Boolean
			Get
				Return param_CtlUsrCartut
			End Get
		End Property
		
		Friend ReadOnly Property NomRadio() As String
			Get
				Return param_NomRadio
			End Get
		End Property
		
		Friend ReadOnly Property PathMusica() As String
			Get
				Return param_PathMusica
			End Get
		End Property

        Friend ReadOnly Property PathPublicitat() As String
			Get
				Return param_PathPublicitat
			End Get
		End Property
		
		Friend ReadOnly Property PathDefPauta() As String
			Get
				Return param_PathDefPauta
			End Get
		End Property

        Friend ReadOnly Property NTopHit() As Short
			Get
				Return param_NTopHit
			End Get
		End Property
		
		Friend ReadOnly Property NHitOld() As Short
			Get
				Return param_NHitOld
			End Get
		End Property
		
		Friend ReadOnly Property PathAudioUser() As String
			Get
				Return param_PathAudioUser
			End Get
		End Property
		
		Friend ReadOnly Property ActvTabProgram() As Boolean
			Get
				Return param_ActvTabProgram
			End Get
		End Property
		
		Friend ReadOnly Property Threshold() As Integer
			Get
				Return param_Threshold
			End Get
		End Property
		
		Friend ReadOnly Property Attack() As Short
			Get
				Return param_Attack
			End Get
		End Property
		
		Friend ReadOnly Property VolNormalize() As Integer
			Get
				Return param_VolNormalize
			End Get
		End Property
		
		Friend ReadOnly Property VolIni() As Short
			Get
				Return param_VolIni
			End Get
		End Property
		
		Friend ReadOnly Property SegActivate() As Short
			Get
				Return param_SegActivate
			End Get
		End Property

        Friend ReadOnly Property TimeRefrestInstruc() As Short
			Get
				Return param_TimeRefrestInstruc
			End Get
		End Property
		
		Friend ReadOnly Property PathAlternative() As String
			Get
				Return param_PathAlternativeAudios
			End Get
		End Property

        Friend ReadOnly Property NomsCarpetaAudiosUser() As String
            Get
                Return param_NomsCarpetaAudiosUser
            End Get
        End Property

    End Class
	
	
End Namespace

