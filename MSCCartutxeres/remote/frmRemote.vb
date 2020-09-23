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
		 
		'SerialPort1 = New SerialPort()
		AddHandler MySerialPort.DataReceived, AddressOf mySerialPort_DataReceived
		Dim strInfo As String 
		If ConnectSerialPort(strInfo)= False Then MsgBox(strInfo, MsgBoxStyle.Critical, MSG_ATENCIO)
					
	End Sub
				
		
	Dim StrDATA As String
	
	Private Delegate Sub UpdateFormDelegate()
	Private UpdateFormDelegateDisplay As UpdateFormDelegate
	
	Private Sub mySerialPort_DataReceived(sender As Object,e As SerialDataReceivedEventArgs)		
		
		'Handles serial port data received events
		UpdateFormDelegateDisplay = New UpdateFormDelegate(AddressOf UpdateDisplay)
		StrDATA = MySerialPort.ReadLine
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
			Case Else
				Exit Sub
		End Select

        Dim formCart As frmCartutxera = getFormCart(IDCart)
        Try
			If blPlay = True Then
				formCart.SetRemotePlay ()
                lbState.Text = "Play:" & CType(formCart, frmCartutxera).Text
            Else
				formCart.SetRemotePause()
                lbState.Text = "Pause:" & CType(formCart, frmCartutxera).Text
            End If			
		Catch ex As Exception
			'cridem a una cartutxera que no està oberta			
		End Try		
	End Sub
	
	Sub FrmRemoteFormClosing(sender As Object, e As FormClosingEventArgs)
		MySerialPort.Close()		
		lbInfo.Text = "Stoped"
	End Sub
	
	Sub TimerCOMTick(sender As Object, e As EventArgs)
		Static dataConnect As Date
		If MySerialPort.IsOpen = True Then
			lbInfo.Text = "Data receiving "
		Else
			lbInfo.Text = "Connecting at " & dataConnect.ToString ("HH:mm:ss")
			If dataConnect < Now Then
				dataConnect = Now.AddSeconds (10)
				ConnectSerialPort("")				
			End If
		End If
	End Sub
	
	Sub FrmRemoteShown(sender As Object, e As EventArgs)
		Me.Hide ()
	End Sub
End Class
