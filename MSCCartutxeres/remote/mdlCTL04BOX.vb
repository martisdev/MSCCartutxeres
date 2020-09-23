'
' Creado por SharpDevelop.
' Usuario: Marti
' Fecha: 07/12/2016
' Hora: 10:44
' 
'
Imports System.IO.Ports

Public Module mdlCTL04BOX
		
	Friend ComPortCTL04 As String = ""
	Friend MySerialPort As SerialPort	
	
	Friend MSG_CTL04BOX_PORT_NO_EXIST As String = "Port no exist"
	Friend LABEL_VERSION As String 
	
	
	Friend function ConnectSerialPort( byref StrInfo As String ) As Boolean 
		Dim FitxerINI As New IniFile		
		Dim PortExist As Boolean
		For Each sp As String In My.Computer.Ports.SerialPortNames
			PortExist = (sp = ComPortCTL04) 
			If 	PortExist = True Then 
				Exit For
			End If
		Next
		
		If 	PortExist = True Then
			MySerialPort.Close()
			MySerialPort.PortName = ComPortCTL04
			MySerialPort.BaudRate = 9600
			MySerialPort.DataBits = 8
			MySerialPort.Parity = Parity.None
			MySerialPort.StopBits = StopBits.One
			MySerialPort.Handshake = Handshake.None
			MySerialPort.Encoding = System.Text.Encoding.Default 'very important!
			MySerialPort.ReadTimeout = 10000
			Try
				MySerialPort.Open()									
				StrInfo = "OK"	
				Return True
			Catch ex As Exception
				StrInfo = ex.Message	
				Return False
			End Try
		Else
			StrInfo = MSG_CTL04BOX_PORT_NO_EXIST
			Return False
		End If
		
	End Function
	
	Friend Function getVersionBox() As String 
		Return "Version: 1.1"
	End Function
End Module
