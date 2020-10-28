Imports Un4seen.Bass
Namespace My
	' StartupNextInstance: se desencadena cuando se inicia una aplicación de instancia única y la aplicación ya está activa. 
	' NetworkAvailabilityChanged: se desencadena cuando la conexión de red está conectada o desconectada.
	
	Partial Friend Class MyApplication

        Const MIN_VER_DBS As String = "4.1"
        Const COMPATIBLE_VERSION_DBS As String = "4.1"

        Private Sub MyApplication_Shutdown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shutdown
            CloseMyApp()
        End Sub

        Private Sub MyApplication_Startup(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
			'TODO: Controlar els resultats de la MyAPPiómsc.ini, dbs i usuari            
			MyAPP = New MSC.Control.MSC_Aplic
			MyAPP.AplicActual = Aplicatius.PRG_CARTUTX

            Dim PathAppData As String = Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData) & "\" & My.Application.Info.CompanyName
            If io.Directory.Exists (PathAppData )= False Then
				IO.Directory.CreateDirectory ( PathAppData)
			End If
			MyAPP.IniFile = PathAppData   & "\msc.ini"
			
			TestStandAlone()
            lang = New MSC.UserLanguage(My.Settings.Lang)
            setLanguageGlobal()
			If STANDALONE = False Then
				'Controlem Llicències i establim els valors per la connexió DBS
				Dim result As ErrorsConnect = CType(MyAPP.IniAplic(), ErrorsConnect)
				Select Case result
					Case ErrorsConnect.Err_NO_ERROR 'OK Continuem.                
					Case ErrorsConnect.Err_NO_CONNECT_TO_SERVER
						If MsgBox(MSG_ERROR_CONNECT_DBS_STANTALONE, MsgBoxStyle.OkCancel,MSG_ATENCIO) = MsgBoxResult.Ok Then
							STANDALONE = True	
						Else
							end
						End If
					Case ErrorsConnect.Err_INI_NO_EXIST
						End
					Case ErrorsConnect.Err_STOP
						End
				End Select
			End If	
			'control d'usuaris            
			Usuari = New MSC.User(MyAPP,STANDALONE)
			If Usuari.UsrErrNum > 0 Then CloseMyApp()

            If STANDALONE = False Then If MyAPP.TestVersionDBS(MIN_VER_DBS, COMPATIBLE_VERSION_DBS) = False Then CloseMyApp()

            Params = New Parametres.clsParams(STANDALONE)
			lang = Nothing
            lang = New MSC.UserLanguage(Params.Lang)
            lang.StrApp = My.Application.Info.AssemblyName
			My.Application.ChangeUICulture(lang.Code)		
			setLanguageGlobal()
			
			If STANDALONE = False Then If MyAPP.NovaConnex(getNomAplic(MyAPP.AplicActual), Usuari.UsrNom, True) = False Then CloseMyApp()

            'Iniciem el sistema de registre d'errors
            MyAPP.Error_MSC = New MSC.Control.MSC_Error(Params.ActvSrvEmergenci, Params.ErrEmail, Params.NomRadio, Params.SendToDevelop, BETA_VERSION)

            If STANDALONE = False Then IniDataSet()
			
			RegisterBass()            
			
			PlayerPre = New  PlayerPreEscolta(My.Settings.DevicePre)
			
			For i As Integer = 0 To My.Application.CommandLineArgs.Count - 1
				Dim PathCommandLine As String = My.Application.CommandLineArgs.Item(i)
                If PathCommandLine.ToUpper = "DEBUG" Then MyAPP.CtlDebug = True
                If PathCommandLine.ToUpper = "RESET" Then
                    Dim FitxerINI As New IniFile
                    For d As Integer = 0 To 15
                        FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "Cart " & d & "_L")
                        FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "Cart " & d & "_T")
                        FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "Cart " & d & "_W")
                        FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "Cart " & d & "_H")
                    Next
                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "DBS_L")
                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "DBS_T")
                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "DBS_W")
                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "DBS_H")

                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "HISTORY_L")
                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "HISTORY_T")
                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "HISTORY_W")
                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "HISTORY_H")

                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "frmFind_L")
                    FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "frmFind_T")
                End If

            Next i
		End Sub
				
		Private Sub MyApplication_StartupNextInstance(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
			If MyAPP.NovaConnex(getNomAplic(MyAPP.AplicActual), Usuari.UsrNom, True) = True Then
                Dim cart As New frmCartutxera
                cart.Show()
				Dim PathCommandLine As String
				For Each s As String In e.CommandLine
					PathCommandLine = s.ToString
					If IO.File.Exists(PathCommandLine) = True Then
                        cart.CarregaFitxer(PathCommandLine)
                        cart.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS
                    End If
				Next
			End If
		End Sub		
		
		Private Sub MyApplication_UnhandledException(ByVal sender As Object, ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) Handles Me.UnhandledException
			'Registra les excepcions no controlades.
			
			Dim ex As Exception = e.Exception				
			Dim InfoErr As Boolean = True
			Dim ForceMailInDebug As Boolean = False
			Dim InCrash As Boolean = True
			MyAPP.Error_MSC.SalvarExcepcioNoControlada(ex , , InCrash,ForceMailInDebug,InfoErr )	
			
			CloseMyApp()
			
		End Sub
		
		Private Sub CloseMyApp()
			
			Call Bass.BASS_Stop()
			Call Bass.BASS_Free()
			If Usuari IsNot Nothing AndAlso Usuari.UsrID > 0 Then Usuari.EndUserSession(Usuari.UsrID)
			If STANDALONE = False Then MyAPP.CloseSesionClient(MyAPP.IDSesion_Client)			
			'FINAL de l'aplicació
			End
			
		End Sub
		
	End Class
	
End Namespace

