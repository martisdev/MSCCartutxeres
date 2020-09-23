'
' Creado por SharpDevelop.
' Usuario: Marti
' Fecha: 29/12/2016
' Hora: 18:08
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Public Module mdlAirence
	
	#Region " Constants"
	'command
	'---WRITE----
	Friend Const AIRENCE_LED_WRITE As Integer = 2
	Friend Const AIRENCE_LED_BLINK_WRITE As Integer = 3
	Friend const AIRENCE_LED_ALL_WRITE As Integer = 4
	'---REQUEST----
	Friend Const AIRENCE_FIRMWARE_VERSION_REQUEST As Integer = 65
	Friend Const AIRENCE_SWITCH_CHANGE_REQUEST As Integer = 69
	'---RESPONSE---
	Friend Const AIRENCE_FIRMWARE_VERSION_RESPONSE As Integer = 129
	Friend const AIRENCE_SWITCH_CHANGE_RESPONSE As Integer = 197
	'---EVENTS----
	Friend Const AIRENCE_LED_EVENT As Integer = 194
	Friend Const AIRENCE_LED_BLINK_EVENT As Integer = 195
	Friend Const AIRENCE_LED_ALL_EVENT As Integer = 196
	Friend Const AIRENCE_SWITCH_CHANGE_EVENT As Integer = 197
	Friend Const AIRENCE_ENCODER_INCREMENT_EVENT As Integer = 198
	Friend Const AIRENCE_ENCODER_DECREMENT_EVENT As Integer = 199
	'
	Friend enum AirenceSWITCHES
		LED_ALL  =  0 '0xFF
		SW_LED_1 = 1
		SW_LED_2 = 2
		SW_LED_3 = 3
		SW_LED_4 = 4
		SW_LED_5 = 5
		SW_LED_6 = 6
		SW_LED_7 = 7
		SW_LED_8 = 8
		SW_LED_9 = 9
		SW_LED_10 = 10
		SW_LED_11 = 11
		SW_LED_12 = 12
		SW_LED_13 = 13
		SW_LED_14 = 14
		SW_LED_15 = 15
		SW_LED_16 = 16
		SW_LED_17 = 17
		SW_LED_18 = 18
		SW_LED_19 = 19
		SW_LED_20 = 20
		SW_LED_21 = 21
		SW_LED_22 = 22
		SW_LED_23 = 23
		SW_LED_24 = 24
		SW_ENCODER = 25
		SW_NONSTOP = 26
		SW_USB1_FADERSTART = 27
		SW_USB1_ON = 28
		SW_USB1_CUE = 29
		SW_USB2_FADERSTART = 30
		SW_USB2_ON = 31
		SW_USB2_CUE = 32
		SW_USB3_FADERSTART = 33
		SW_USB3_ON = 34
		SW_USB3_CUE = 35
		SW_USB4_FADERSTART = 36
		SW_USB4_ON = 37
		SW_USB4_CUE = 38
	End Enum
	
	
	Friend Enum colors_t 
		NONE
		RED
		GREEN
		YELLOW
	end enum
	
	Friend Enum blink_speed_t
		SLOW
		NORMAL
		FAST
	End Enum
	
	Friend MyMixer As Airence
	
	Friend Structure AirenceEVENT
		Dim _Command As Integer
		Dim _Event As Integer 
		Dim _Value As Boolean 
	End Structure
	
	
	#End Region
	
	Friend Function ConvertHexToBin(ByVal a As String, len As Integer ) As String  'Converts Hex to Binary
		
		'Some error checking before I do anything
		If a = "" Then 'If the inputted text is blank then it shows a message box saying that you must enter a string first for the function to work.
			'MsgBox("Enter a string value first.")
			Dim res As String = ""
			Return res.Insert(len,"0")
		Else
			
			Dim temp As String = Convert.ToString(Convert.ToInt32(a, 16), 2)
			Dim Mylen As Integer = len-temp.Length 
			For i As Integer = 0 To Mylen -1
				temp = "0" & temp 
			Next
			
			Return temp
		End If
		
	End Function
	
	Friend Function ConvertBinToHex(ByVal a As String) As String  'Converts Binary to Hex
		
		'Some error checking before I do anything
		If a = "" Then 'If the inputted text is blank then it shows a message box saying that you must enter a string first for the function to work.
			'MsgBox("Enter a string value first.")
			Return String.Empty
		Else
			Return Convert.ToString(Convert.ToInt32(a, 2), 16).ToUpper
		End If
		
	End Function
	
	Friend Function splitBinary(bynaryStr As String ) As Boolean ()
		Dim strSplit(bynaryStr.Length-1)As Boolean
		For i As Integer = 0 To bynaryStr.Length-1
			strSplit(i)= IIf(bynaryStr.Substring(i,1)=1,True,False)
		Next
		Return strSplit
	End Function
	
End Module
