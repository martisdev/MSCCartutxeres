'
' Creado por SharpDevelop.
' Usuario: Marti
' Fecha: 17/07/2016
' Hora: 09:45
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'

Imports System
Imports System.IO.Ports

Public Partial Class frmRemote
	Dim comPORT As String
	Dim SerialPort1 As SerialPort
	
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub
	
	Private Sub ClickBotoTack(ByVal sender As System.Object, ByVal e As System.EventArgs)
		
	End Sub
	
	Sub FrmRemoteLoad(sender As Object, e As EventArgs)
		'		comPORT = ""
		'		For Each sp As String In My.Computer.Ports.SerialPortNames
		'			comPort_ComboBox.Items.Add(sp)
		'		Next
		 
		SerialPort1 = New SerialPort()
		AddHandler SerialPort1.DataReceived, AddressOf mySerialPort_DataReceived
		ConnectSerialPort()
	End Sub
	
	Private Sub ConnectSerialPort()
		Dim FitxerINI As New IniFile
		comPORT = FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "PORT_CTL04", "")
		Dim PortExist As Boolean
		For Each sp As String In My.Computer.Ports.SerialPortNames
			PortExist = (sp = comPORT) 
			If 	PortExist = True Then 
				Exit For
			End If
		Next
		
		If 	PortExist = True Then
			SerialPort1.Close()
			SerialPort1.PortName = comPORT
			SerialPort1.BaudRate = 9600
			SerialPort1.DataBits = 8
			SerialPort1.Parity = Parity.None
			SerialPort1.StopBits = StopBits.One
			SerialPort1.Handshake = Handshake.None
			SerialPort1.Encoding = System.Text.Encoding.Default 'very important!
			SerialPort1.ReadTimeout = 10000
			Try
				SerialPort1.Open()			
				lbInfo.Text = "Connect"				
			Catch ex As Exception
				lbInfo.Text = "Error to connect"	
			End Try
		Else
			lbInfo.Text = "Port no exist"
		End If
		
	End Sub
		
		
	Dim StrDATA As String
	
	Private Delegate Sub UpdateFormDelegate()
	Private UpdateFormDelegateDisplay As UpdateFormDelegate
	
	Private Sub mySerialPort_DataReceived(sender As Object,e As SerialDataReceivedEventArgs)		
		
		'Handles serial port data received events
		UpdateFormDelegateDisplay = New UpdateFormDelegate(AddressOf UpdateDisplay)
		StrDATA = SerialPort1.ReadLine
		Me.Invoke(UpdateFormDelegateDisplay) 'call the delegate
		
	End Sub
	
	Private Sub UpdateDisplay()
		Dim blPlay As Boolean = True 'si és false és pausa		
		Dim ad As String = Microsoft.VisualBasic.Left(StrDATA,5) 
		Dim IDCart As Integer = 0
		Select ad
			Case "b1-on" : IDCart = 1 : blPlay = True 							
			Case "b1-of" : IDCart = 1 : blPlay = False
			Case "b2-on" : IDCart = 2 : blPlay = True
			Case "b2-of" : IDCart = 2 : blPlay = False
			Case "b3-on" : IDCart = 3 : blPlay = True
			Case "b3-of" : IDCart = 3 : blPlay = False
			Case "b4-on" : IDCart = 4 : blPlay = True
			Case "b4-of" : IDCart = 4 : blPlay = False			
		End Select
		
		Dim formCart As Form = Nothing
		Dim frm As Form
		For Each frm In My.Application.OpenForms
			If frm.Name = "frmCartut" And frm.Text.Contains("Cart " & IDCart.ToString) Then
				formCart = frm
			End If
		Next					
		If IsNothing(formCart) Then Exit Sub 
		Try
			If blPlay = True Then
				CType(formCart, frmCartut).SetRemotePlay()
				lbState.Text = "Play:" & CType(formCart, frmCartut).Text  
			Else
				CType(formCart, frmCartut).SetRemotePause()	
				lbState.Text = "Pause:" & CType(formCart, frmCartut).Text
			End If			
		Catch ex As Exception
			'cridem a una cartutxera que no està oberta
			beep
		End Try		
	End Sub
	
	Sub FrmRemoteFormClosing(sender As Object, e As FormClosingEventArgs)
		SerialPort1.Close()		
		lbInfo.Text = "Stoped"
	End Sub
	
	
	Sub TimerCOMTick(sender As Object, e As EventArgs)
		Static dataConnect As Date
		If SerialPort1.IsOpen = True Then
			lbInfo.Text = "Data receiving "
		Else
			lbInfo.Text = "Connecting at " & dataConnect.ToString ("HH:mm:ss")
			If dataConnect < Now Then
				dataConnect = Now.AddSeconds (10)
				ConnectSerialPort()				
			End If
		End If
	End Sub
	
	Sub FrmRemoteShown(sender As Object, e As EventArgs)
		Me.Hide ()
	End Sub
End Class
