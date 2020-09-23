'
' Creado por SharpDevelop.
' Usuario: Marti
' Fecha: 22/10/2016
' Hora: 11:31
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Imports System.Timers
Public Partial Class frmAirence
	
	dim OLD_res_sw_1_8 As String   = "00000000" 
	dim OLD_res_sw_9_16 As String  = "00000000"
	dim OLD_res_sw_17_24 As String = "00000000"
	dim OLD_res_usb_2_1 As String = "000000"
	dim OLD_res_usb_3_4 As String = "000000"
	dim OLD_res_sw_enc_non As String = "00"
	
	Private Shared tmrContinuousDataCollect As System.Timers.Timer
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		tmrContinuousDataCollect = New System.Timers.Timer(300)
		AddHandler tmrContinuousDataCollect.Elapsed, AddressOf OnDataCollect
		tmrContinuousDataCollect.Stop()	
		If MyMixer.myDeviceDetected= True Then
			'lstResults.Items.Add("  My device detected.")
			tmrContinuousDataCollect.Start()
		Else
			'lstResults.Items.Add("  My device NOT detected.")
		End If
		
	End Sub
	
	
	Private Delegate Sub UpdateFormDelegate()
	Private UpdateFormDelegateEvent As UpdateFormDelegate
	
	Private Sub OnDataCollect(ByVal source As Object, ByVal e As ElapsedEventArgs)
		
		'Try
		If (MyMixer.transferInProgress = False) Then
			
			MyMixer.ReadToDevice()
			
			UpdateFormDelegateEvent = New UpdateFormDelegate(AddressOf ComposeEvent)
			Me.Invoke(UpdateFormDelegateEvent) 'call the delegate
		End If
		
		'Catch ex As Exception
		'	If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)	
		'End Try
		
	End Sub
	
	Sub ComposeEvent()
		If MyMixer.Response Is Nothing Then Exit Sub		
		
		If MyAPP.CtlDebug = True Then
			Dim byteValue As String = ""
			For count As Integer = 0 To UBound(MyMixer.Response)															
				byteValue = byteValue & ", " & count & " = " & MyMixer.Response(count) & " (" & ConvertHexToBin(MyMixer.Response(count),1) & ")" 
			Next count
			MyAPP.Error_MSC.SalvarRegistreDepuracio( byteValue )
		End If
		
		Dim instruc() As AirenceEVENT = decodeInstruccions(MyMixer.Response)
		If Not IsNothing (instruc) Then
			For i As Integer= 0 To UBound(instruc)
				Select instruc(i)._Command
					Case AIRENCE_LED_EVENT 
					Case AIRENCE_LED_BLINK_EVENT
					Case AIRENCE_LED_ALL_EVENT
					Case AIRENCE_SWITCH_CHANGE_EVENT : MyCSC_CB(instruc(i)._Event,instruc(i)._Value,0)
					Case AIRENCE_ENCODER_INCREMENT_EVENT
					Case AIRENCE_ENCODER_DECREMENT_EVENT
				End Select
			Next	
		End If
		
	End Sub
	
	Private Function decodeInstruccions(MyResponse() As Byte )As AirenceEVENT()
		Dim AirenceInstric() As AirenceEVENT = Nothing 
		Dim NewID As Integer 
		For i As Integer = 0 To MyResponse.Length -1			
			Dim Command As Integer = Convert.ToInt32(Hex(MyResponse(2)),16)
			Select Command
				Case AIRENCE_LED_EVENT 
				Case AIRENCE_LED_BLINK_EVENT
				Case AIRENCE_LED_ALL_EVENT
				Case AIRENCE_SWITCH_CHANGE_EVENT
					'buscar diferències a les instruccions
					Dim res_sw_1_8 As String =  ConvertHexToBin(Hex(MyResponse(3)),8)
					Dim res_sw_9_16 As String =  ConvertHexToBin(Hex(MyResponse(4)),8)
					Dim res_sw_17_24 As String =  ConvertHexToBin(Hex(MyResponse(5)),8)			
					Dim res_sw_enc_non As String =  ConvertHexToBin(Hex(MyResponse(6)),2)			
					Dim res_usb_2_1 As String =  ConvertHexToBin(Hex(MyResponse(7)),6)'Canal 1 i 2
					Dim res_usb_3_4 As String =  ConvertHexToBin(Hex(MyResponse(8)),6)'canal 3 i 4
					'comparar valors amb  el previs
					If OLD_res_usb_2_1 <> res_usb_2_1 Then
						Dim resp()As Boolean  = splitBinary(res_usb_2_1)
						Dim resp_OLD()As Boolean  = splitBinary(OLD_res_usb_2_1)
						'Busquem totes les diferències
						'canal 1
						If resp(5) <> resp_OLD(5) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB1_FADERSTART, resp(5))
						If resp(4) <> resp_OLD(4) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB1_ON, resp(4)) 
						If resp(3) <> resp_OLD(3) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB1_CUE, resp(3)) 
						'Canal 2
						If resp(2) <> resp_OLD(2) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB2_FADERSTART, resp(2))
						If resp(1) <> resp_OLD(1) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB2_ON, resp(1))
						If resp(0) <> resp_OLD(0) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB2_CUE, resp(0))
						OLD_res_usb_2_1= res_usb_2_1
					End If	
					If OLD_res_usb_3_4 <> res_usb_3_4 Then
						Dim resp()As Boolean  = splitBinary(res_usb_3_4)
						Dim resp_OLD()As Boolean  = splitBinary(OLD_res_usb_3_4)
						'Busquem totes les diferències
						'Canal 3
						If resp(5) <> resp_OLD(5) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB3_FADERSTART, resp(5))
						If resp(4) <> resp_OLD(4) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB3_ON, resp(4)) 
						If resp(3) <> resp_OLD(3) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB3_CUE, resp(3))
						'Canal 4
						If resp(2) <> resp_OLD(2) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB4_FADERSTART, resp(2))
						If resp(1) <> resp_OLD(1) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB4_ON, resp(1))
						If resp(0) <> resp_OLD(0) Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_USB4_CUE, resp(0)) 
						OLD_res_usb_3_4 = res_usb_3_4
					End if	
					If OLD_res_sw_1_8 <> res_sw_1_8 Then
						'Botons del 1-8
						Dim resp()As Boolean  = splitBinary(res_sw_1_8)
						Dim resp_OLD()As Boolean  = splitBinary(OLD_res_sw_1_8)
						If resp(0) <> resp_OLD(0) AndAlso  resp(0)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_8, resp(0))
						If resp(1) <> resp_OLD(1) AndAlso  resp(1)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_7, resp(1))
						If resp(2) <> resp_OLD(2) AndAlso  resp(2)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_6, resp(2))
						If resp(3) <> resp_OLD(3) AndAlso  resp(3)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_5, resp(3))
						If resp(4) <> resp_OLD(4) AndAlso  resp(4)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_4, resp(4))
						If resp(5) <> resp_OLD(5) AndAlso  resp(5)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_3, resp(5))
						If resp(6) <> resp_OLD(6) AndAlso  resp(6)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_2, resp(6))
						If resp(7) <> resp_OLD(7) AndAlso  resp(7)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_1, resp(7))
						OLD_res_sw_1_8= res_sw_1_8
					End if	
					If OLD_res_sw_9_16 <> res_sw_9_16 Then
						'Botons del 9-16
						Dim resp()As Boolean  = splitBinary(res_sw_9_16)
						Dim resp_OLD()As Boolean  = splitBinary(OLD_res_sw_9_16)
						If resp(0) <> resp_OLD(0) AndAlso  resp(0)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_16, resp(0))
						If resp(1) <> resp_OLD(1) AndAlso  resp(1)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_15, resp(1))
						If resp(2) <> resp_OLD(2) AndAlso  resp(2)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_14, resp(2))
						If resp(3) <> resp_OLD(3) AndAlso  resp(3)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_13, resp(3))
						If resp(4) <> resp_OLD(4) AndAlso  resp(4)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_12, resp(4))
						If resp(5) <> resp_OLD(5) AndAlso  resp(5)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_11, resp(5))
						If resp(6) <> resp_OLD(6) AndAlso  resp(6)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_10, resp(6))
						If resp(7) <> resp_OLD(7) AndAlso  resp(7)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_9, resp(7))
						OLD_res_sw_9_16= res_sw_9_16
					End if	
					If OLD_res_sw_17_24 <> res_sw_17_24 Then
						'TODO: tecles de funció
						Dim resp()As Boolean  = splitBinary(res_sw_17_24)
						Dim resp_OLD()As Boolean  = splitBinary(OLD_res_sw_17_24)
						If resp(0) <> resp_OLD(0) AndAlso resp(0)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_24, resp(0))
						If resp(1) <> resp_OLD(1) AndAlso resp(1)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_23, resp(1))
						If resp(2) <> resp_OLD(2) AndAlso resp(2)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_22, resp(2))
						If resp(3) <> resp_OLD(3) AndAlso resp(3)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_21, resp(3))
						If resp(4) <> resp_OLD(4) AndAlso resp(4)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_20, resp(4))
						If resp(5) <> resp_OLD(5) AndAlso resp(5)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_19, resp(5))
						If resp(6) <> resp_OLD(6) AndAlso resp(6)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_18, resp(6))
						If resp(7) <> resp_OLD(7) AndAlso resp(7)= True Then addInstuccio(AirenceInstric,NewID,AIRENCE_SWITCH_CHANGE_EVENT, AirenceSWITCHES.SW_LED_17, resp(7))
						OLD_res_sw_17_24= res_sw_17_24
					End If
					If OLD_res_sw_enc_non <> res_sw_enc_non Then
						'TODO:
						OLD_res_sw_enc_non = res_sw_enc_non
					End If
				Case AIRENCE_ENCODER_INCREMENT_EVENT	
					Dim abs_value As Integer  =  MyResponse(3)
					addInstuccio(AirenceInstric,NewID,AIRENCE_ENCODER_INCREMENT_EVENT, abs_value, False)
				Case AIRENCE_ENCODER_DECREMENT_EVENT
					Dim abs_value As Integer  =  MyResponse(3)
					addInstuccio(AirenceInstric,NewID,AIRENCE_ENCODER_DECREMENT_EVENT, abs_value, False)
			End Select
		Next
		Return AirenceInstric
	End function
	
	private Sub addInstuccio(ByRef AirenceInstric() As AirenceEVENT,ByRef  NewID As Integer ,MyCommand As Integer , MyEvent As Integer , MyValue As Boolean )
		NewID += 1
		ReDim Preserve AirenceInstric(NewID-1)
		AirenceInstric(NewID-1)._Command = MyCommand
		AirenceInstric(NewID-1)._Event = MyEvent	
		AirenceInstric(NewID-1)._Value = MyValue
	End Sub
	
	
	Sub MyCSC_CB(controlsignal As Integer , state As Boolean , ByVal data As Integer)
		If Me.lbLedEvent.BackColor = Color.Red Then
			Me.lbLedEvent.BackColor = Color.Green 
		Else
			Me.lbLedEvent.BackColor = Color.Red
		End If
		Static key_ALT As Boolean = False 
		'Static key_Control As Boolean = False 
		
		Select controlsignal
			Case AirenceSWITCHES.LED_ALL
				'repassar totes les cartut per fer pausa.
				Dim frm As Form
				
				For Each frm In My.Application.OpenForms
                    If frm.Name = "frmCartutxera" Then
                        Dim frmDef As frmCartutxera = CType(frm, frmCartutxera)
                        frmDef.SetRemotePause()
                    End If
                Next
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("LED_ALL")
			Case AirenceSWITCHES.SW_LED_1  To AirenceSWITCHES.SW_LED_16
                Dim formCart As frmCartutxera = getFormCart(1) 'getFormCart(InxfrmOnBotonera)
                'formCart.PlayFilePlayer(CShort(controlsignal - 1), True, True)
                'MyMixer.airenceSetLed(0,colors_t.NONE)
                'MyMixer.airenceSetLed(controlsignal,colors_t.YELLOW)
                If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_LED_" & controlsignal )
			Case AirenceSWITCHES.SW_ENCODER
			Case AirenceSWITCHES.SW_NONSTOP
			Case AirenceSWITCHES.SW_USB1_FADERSTART
                Dim formCart As frmCartutxera = getFormCart(1)
                If state = True Then
					formCart.SetRemotePlay()
				Else
					formCart.SetRemotePause()
				End If	
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB1_ON state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB1_ON
                Dim formCart As frmCartutxera = getFormCart(1)
                If state = True Then
					formCart.SetRemotePlay()
				Else
					formCart.SetRemotePause()
				End If	
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB1_ON state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB1_CUE
                Dim formCart As frmCartutxera = getFormCart(1)
                If state = True Then
                    'formCart.PlayCue(True)	
                Else
                    'formCart.StopCue()	
                End If		
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB1_CUE state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB2_FADERSTART
                Dim formCart As frmCartutxera = getFormCart(2)
                If state = True Then
					formCart.SetRemotePlay()
				Else
					formCart.SetRemotePause()
				End If	
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB2_ON state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB2_ON
                Dim formCart As frmCartutxera = getFormCart(2)
                If state = True Then
					formCart.SetRemotePlay()
				Else
					formCart.SetRemotePause()
				End If	
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB2_ON state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB2_CUE
                Dim formCart As frmCartutxera = getFormCart(2)
                If state = True Then
                    'formCart.PlayCue(True)	
                Else
                    'formCart.StopCue()	
                End If				
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB2_CUE state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB3_FADERSTART
                Dim formCart As frmCartutxera = getFormCart(3)
                If state = True Then
					formCart.SetRemotePlay()
				Else
					formCart.SetRemotePause()
				End If			
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB3_ON state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB3_ON
                Dim formCart As frmCartutxera = getFormCart(3)
                If state = True Then
					formCart.SetRemotePlay()
				Else
					formCart.SetRemotePause()
				End If			
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB3_ON state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB3_CUE
                Dim formCart As frmCartutxera = getFormCart(3)
                If state = True Then
                    'formCart.PlayCue(True)	
                Else
                    'formCart.StopCue()	
                End If				
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB3_CUE state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB4_FADERSTART
                Dim formCart As frmCartutxera = getFormCart(4)
                If state = True Then
					formCart.SetRemotePlay()
				Else
					formCart.SetRemotePause()
				End If				
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB4_ON state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB4_ON
                Dim formCart As frmCartutxera = getFormCart(4)
                If state = True Then
					formCart.SetRemotePlay()
				Else
					formCart.SetRemotePause()
				End If				
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB4_ON state:" & IIf(state= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_USB4_CUE
                Dim formCart As frmCartutxera = getFormCart(4)
                If state = True Then
                    'formCart.PlayCue(True)	
                Else
                    'formCart.StopCue()	
                End If	
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_USB4_CUE state:" & IIf(state= True ,1,0).ToString )
				
				
			Case AirenceSWITCHES.SW_LED_17
				'equival al Control
				key_ALT   = Not key_ALT
				
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("key_Control_" &  IIf(key_ALT= True ,1,0).ToString )
			Case AirenceSWITCHES.SW_LED_18
				'Equival al control
				'key_shift = Not key_shift
				
				'If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("key_shift_" &  IIf(key_shift= True ,1,0).ToString )
				
			Case AirenceSWITCHES.SW_LED_24'todo: tecles de funció
			
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio("SW_FUNCIO_" & controlsignal )
			
			Case AIRENCE_ENCODER_INCREMENT_EVENT
                Dim formCart As frmCartutxera = getFormCart(4) 'getFormCart(InxfrmOnBotonera)
                If key_ALT = True Then
					'Controlem el pitch
					formCart.UP_Pitch()
				Else
					'Desplacem la ona
					formCart.RemoteSetFFPosition()
				End If
				
			Case AIRENCE_ENCODER_DECREMENT_EVENT
                Dim formCart As frmCartutxera = getFormCart(4) 'getFormCart(InxfrmOnBotonera)
                If key_ALT = True Then
					'Controlem el pitch
					formCart.DOWN_Pitch()
				Else
					'Desplacem la ona
					formCart.RemoteSetRWPosition()
				End If
		End Select
		
	End Sub
	
	
	Sub MyEC_CB( direction As Integer, abs_value As String , data As Integer)
		
	End Sub
	
	Sub EndAirence()
		MyMixer.airenceClearControlSignalChangeCB()
		MyMixer.airenceClearEncoderChangeCB()
		MyMixer.airenceClose()
	End Sub
	
	Sub FrmAirenceLoad(sender As Object, e As EventArgs)		
		If MyMixer.myDeviceDetected = True Then
			'airenceSetControlSignalChangeCB(AddressOf MyCSC_CB,Nothing)		
			'airenceSetEncoderChangeCB(AddressOf MyEC_CB,Nothing)			
			Dim Mayor As Integer 
			Dim minor As Integer
			Dim ver As Integer = MyMixer.airenceGetLibraryVersion(Mayor,minor)
			'lbLibraryVersion.Text = Mayor & "." & minor & " - " & ver
			
			ver = MyMixer.airenceGetFirmwareVersion(Mayor,minor)
			'lbFirmwareVersion.Text = Mayor & "." & minor & " - " & ver				
		Else
			MsgBox ("Error al connectar amb la taula Airence")			
		End If		
		'MyMixer.airenceEnableLogging(MyAPP.CtlDebug)
	End Sub
	
	Sub ButtonPlay_Click(sender As Object, e As EventArgs)
		
		Dim MyButton As Button =  CType(sender, Button)
		Dim State As Boolean = False
		If MyButton.BackColor = Color.Transparent Then
			MyButton.BackColor = Color.Red
			'MyMixer.airenceSetLed(MyButton.Tag,colors_t.RED)
			State = True
		Else
			MyButton.BackColor = Color.Transparent
			'MyMixer.airenceSetLed(MyButton.Tag,colors_t.NONE)
			State = False
		End If			
		MyCSC_CB(CInt(MyButton.Tag),State,100)
	End Sub	
	
	Sub FrmAirenceShown(sender As Object, e As EventArgs)
		'If  MyAPP.CtlDebug = false Then Me.Hide()		
		Me.Hide()
	End Sub		
	
	Sub FrmAirenceFormClosing(sender As Object, e As FormClosingEventArgs)
		MyMixer.airenceSetLed(0,colors_t.NONE)
	End Sub
End Class
