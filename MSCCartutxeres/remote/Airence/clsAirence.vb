'
' Creado por SharpDevelop.
' Usuario: Marti
' Fecha: 28/06/2016
' Hora: 14:03
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'

Imports MSCCartutxeres.DeviceManagement
Imports MSCCartutxeres.FileIo
Imports MSCCartutxeres.Hid
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports Microsoft.Win32.SafeHandles

Public Class Airence
	
	Private Const VENDOR_ID As String = "03EB"
	Private Const PRODUCT_ID As String = "2402"	
	
	Private MyHid As New Hid()
	Private hidUsage As String
	Private hidHandle As SafeFileHandle
	
	
	Private myDevicePathName As String
	
	Private deviceNotificationHandle As IntPtr
	Private exclusiveAccess As Boolean	
	Private fileStreamdevicedata As FileStream
	Private InputBufferSize As Integer 
	
	Friend ControlTransfersOnly As Boolean = False
	Friend transferInProgress As Boolean = False
	
	Friend myDeviceDetected As Boolean
	
	Friend Response() As Byte = Nothing
	Sub New()
		
		MyHid = New Hid()
		
		FindTheHid(VENDOR_ID,PRODUCT_ID)
		
	End Sub
	
	#Region " Funcions Airence"
	''' <summary>
	''' The function airenceOpen() must be called before any other function of this library may be used. The
	''' function will try to setup the communication with the Airence mixer and initializes the library. After
	''' initialization the initial states of the control signals are read and stored internally in the library. The
	'LED’s behind the control switches are turned off.
	''' </summary>
	''' <returns>On succes the Function returns 0, Or -1 otherwise</returns>	
	Friend Function airenceOpen()As Boolean
		
	End Function
	
	'When done using the library one needs to call this function to free the allocated memory and finalize
	'the library. The LED’s behind the control switches are turned off. On succes the function returns 0, or -
	'1 otherwise.
	Friend Function airenceClose() As Boolean
		
	End Function
	
	
	''' <summary>
	''' Set the specified LED lednr with color. One can choose a value for lednr in the range of 1~24. 
	'''	These LED’s are the LED’s behind the control switches. 
	''' </summary>
	''' <param name="lednr"></param>
	''' <param name="color"></param>
	''' <returns>On succes the Function returns 0, Or -1 otherwise.</returns>
	Friend Function airenceSetLed(lednr As Integer,color As colors_t)As Integer 
		'		'04/02/lednr/color/-/-/-/-/

		'Dim inputReportBuffer() As Byte = Nothing
		Dim outputReportBuffer() As Byte = Nothing
		Dim success As Boolean
		
		Try
			success = False
			
			' Don't attempt to exchange reports if valid handles aren't available
			' (as for a mouse or keyboard under Windows 2000/XP.)
			
			If (Not (hidHandle.IsInvalid)) Then
				
				' Don't attempt to send an Output report if the HID has no Output report.
				
				If (MyHid.Capabilities.OutputReportByteLength > 0) Then
					
					' Set the upper bound of the Output report buffer. 
					' Subtract 1 from OutputReportByteLength because the array begins at index 0.
					
					Array.Resize(outputReportBuffer, MyHid.Capabilities.OutputReportByteLength)
					
					' Store the report ID in the first byte of the buffer:
					
					outputReportBuffer(0) = 0
					
					' Store the report data following the report ID.
					' Use the data in the combo boxes on the form.
					
					outputReportBuffer(1) = hex(4)'SIZE
					outputReportBuffer(2) = Hex(AIRENCE_LED_WRITE)'COMMAND
					outputReportBuffer(3) = hex(lednr)'lednum
					outputReportBuffer(4) = hex(color)'color
					
					
					' Write a report.
					
					If ControlTransfersOnly = True Then
						
						' Use a control transfer to send the report,
						' even if the HID has an interrupt OUT endpoint.
						
						success = MyHid.SendOutputReportViaControlTransfer(hidHandle, outputReportBuffer)
						
					Else
						
						' If the HID has an interrupt OUT endpoint, the host uses an 
						' interrupt transfer to send the report. 
						' If not, the host uses a control transfer.
						
						If (fileStreamdevicedata.CanWrite) Then
							fileStreamdevicedata.Write(outputReportBuffer, 0, outputReportBuffer.Length)
							success = True
						Else
							CloseCommunications()
							'MsgBox("The attempt to read an Input report has failed.")
						End If
					End If
					
					If success Then
						'MsgBox("An Output report has been written.")
						
					Else
						CloseCommunications()
						'MsgBox("The attempt to write an Output report failed.")
					End If
					
					
				Else
					MsgBox("The HID doesn't have an Output report.")
				End If
			Else
				MsgBox("Invalid handle. The device is probably a system mouse or keyboard.")
				MsgBox("No attempt to write an Output report or read an Input report was made.")
				'AccessForm("EnableCmdOnce", "")
			End If
			
		Catch ex As Exception
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)
			'Throw
		End Try
		
	End Function
	
	''' <summary>
	''' Set the specified LED lednr in blink mode. The on/off-color and speed can be given to the function by
	''' the arguments. On succes the function returns 0, or -1 otherwise.
	''' </summary>
	''' <param name="lednr"></param>
	''' <param name="color_on"></param>
	''' <param name="color_off"></param>
	''' <param name="speed"></param>
	''' <returns></returns>
	Friend Function airenceSetLedBlink(lednr As Integer,color_on As colors_t,color_off As colors_t,speed As blink_speed_t)As Integer 
		
		'Dim inputReportBuffer() As Byte = Nothing
		Dim outputReportBuffer() As Byte = Nothing
		Dim success As Boolean
		
		Try
			success = False
			
			' Don't attempt to exchange reports if valid handles aren't available
			' (as for a mouse or keyboard under Windows 2000/XP.)
			
			If (Not (hidHandle.IsInvalid)) Then
				
				' Don't attempt to send an Output report if the HID has no Output report.
				
				If (MyHid.Capabilities.OutputReportByteLength > 0) Then
					
					' Set the upper bound of the Output report buffer. 
					' Subtract 1 from OutputReportByteLength because the array begins at index 0.
					
					Array.Resize(outputReportBuffer, MyHid.Capabilities.OutputReportByteLength)
					
					' Store the report ID in the first byte of the buffer:
					
					outputReportBuffer(0) = 0
					
					' Store the report data following the report ID.
					' Use the data in the combo boxes on the form.
					
					outputReportBuffer(1) = Convert.ToByte(6)'SIZE
					outputReportBuffer(2) = Convert.ToByte(2)'COMMAND
					outputReportBuffer(3) = Convert.ToByte(lednr)'lednum
					outputReportBuffer(4) = Convert.ToByte(color_on)'color_on
					outputReportBuffer(5) = Convert.ToByte(color_off)'color_off
					outputReportBuffer(6) = Convert.ToByte(speed)'speed
					
					' Write a report.
					
					If ControlTransfersOnly = True Then
						
						' Use a control transfer to send the report,
						' even if the HID has an interrupt OUT endpoint.
						
						success = MyHid.SendOutputReportViaControlTransfer(hidHandle, outputReportBuffer)
						
					Else
						
						' If the HID has an interrupt OUT endpoint, the host uses an 
						' interrupt transfer to send the report. 
						' If not, the host uses a control transfer.
						
						If (fileStreamdevicedata.CanWrite) Then
							fileStreamdevicedata.Write(outputReportBuffer, 0, outputReportBuffer.Length)
							success = True
						Else
							CloseCommunications()
							MsgBox("The attempt to read an Input report has failed.")
						End If
					End If
					
					If success Then
						MsgBox("An Output report has been written.")
						
					Else
						CloseCommunications()
						MsgBox("The attempt to write an Output report failed.")
					End If
					
					
				Else
					MsgBox("The HID doesn't have an Output report.")
				End If
			Else
				MsgBox("Invalid handle. The device is probably a system mouse or keyboard.")
				MsgBox("No attempt to write an Output report or read an Input report was made.")
				'AccessForm("EnableCmdOnce", "")
			End If
			
		Catch ex As Exception
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)
			'Throw
		End Try
	End Function
	
	
	'To retrieve the state of a single control signal this function must be called. The controlsignal argument
	'can have a value in the range of 1~38 (see control signal defines in this document). The argument
	'state is a pointer to a boolean variable. The variable itself will be set to TRUE(1) for press state, and
	'False(0) For release state. On succes the Function returns 0, Or -1 otherwise.
	Friend Function airenceGetControlSignal(controlsignal As Integer,state As Boolean)As Integer
		
	End Function
	
	
	'In order to retrieve the state of all the control signals this function must be called. Reserve a buffer of
	'at least 6 bytes to store the data and give the pointer of that buffer as argument to this function. Every
	'bit presents the state of a control signal, ‘1’ for press and ‘0’ for release state. The data will be
	'presented As can be seen In the table below. On succes the Function returns 0, Or -1 otherwise.
	Friend Function airenceGetRawControlData(ByVal data As Char  )As Integer
		
	End Function
	
	'This function returns the version of this library. The major and minor arguments can be used to store
	'the version, otherwise one has to fill in these arguments with the value NULL. Besides the version the
	'build Date Of the library will be returned.
	Friend Function airenceGetLibraryVersion(ByVal major As Integer ,ByVal minor As Integer )As Integer
		major = 1
		minor = 1
		Return 11
	End Function
	
	'This function returns the version of the firmware present in the Airence mixer. The major and minor
	'	arguments can
	Friend Function airenceGetFirmwareVersion(ByVal major As Integer ,ByVal minor As Integer )As Integer
		major = 1
		minor = 2
		Return 12
	End Function
	
	'Sets the callback function for a control signal change event. If there occurs a state change in one of
	'the control signals, this callback will be called. The data parameter when registering the callback will
	'be provided as parameter in the callback function. The function returns void.
	
	'Public Shared Function airenceSetControlSignalChangeCB Lib "airence.dll"(ByVal CallbackAddr As CSC_CB) As Long		
	'End Function
	
	Friend Delegate sub CSC_CB(controlsignal As Integer , state As Boolean , byval data As Integer)
	
	Friend Declare Function airenceSetControlSignalChangeCB Lib "airence.dll" (x As CSC_CB, byval data As Integer)As Integer
	
	'Clears the callback function for a control signal change event which was prior set with the
	'			airenceSetControlSignalChangeCB() Function. The Function returns void.
	Friend Function airenceClearControlSignalChangeCB()As Integer
		
	End Function
	
	
	Friend Function airenceSetEncoderChangeCB (x As EC_CB, ByVal data As Integer)As Integer
		
	End Function
	
	Friend Delegate sub EC_CB( direction As Integer, abs_value As String , data As Integer)
	
	Friend Function airenceClearEncoderChangeCB()As Integer
		
	End Function
	
	'readmode value:
	' -1: Blocking
	'  0: Non-Blocking 
	' >0: Non-Blocking with timeout in ms */
	Friend Function airenceSetReadMode(readmode As Integer ) As Integer
		
	End Function
	
	
	'			This Function returns the last occurred Error In the library.
	Friend Function airenceGetLastError()As String	
		
	End Function
	
	'Logging can be enabled by calling this function with the argument set to true. Calling the function with
	'the argument false disables the logging. A txt-logfile will be created in the same folder whereas the
	'DLL is located and the library will be used by an application. The logfile logs any kind of activity in the
	'library, Like events, Function calls, Or errors.	
	Friend Sub airenceEnableLogging (state As Boolean)
		
	End Sub
	
	#End Region
	
	#Region " Funcions HID"
	
	private Function FindTheHid(ByVal vendorID As String ,ByVal ProductID As String ) As Boolean
		Dim Descrip As String = "" 'for debug
		
		Dim MyDeviceManagement As New DeviceManagement()
		'dim hidHandle As SafeFileHandle
		
		Dim deviceFound As Boolean
		Dim devicePathName(127) As String
		Dim hidGuid As System.Guid
		Dim inputReportBuffer As Byte() = Nothing
		Dim memberIndex As Int32
		Dim myProductID As Int32
		Dim myVendorID As Int32
		Dim outputReportBuffer As Byte() = Nothing
		Dim success As Boolean
		
		Try
			myDeviceDetected = False
			
			' Get the device's Vendor ID and Product ID from the form's text boxes.
			myVendorID = Int32.Parse(vendorID, NumberStyles.AllowHexSpecifier)
			myProductID = Int32.Parse(ProductID, NumberStyles.AllowHexSpecifier)
			
			
			
			' ***
			' API function: 'HidD_GetHidGuid
			
			' Purpose: Retrieves the interface class GUID for the HID class.
			
			' Accepts: 'A System.Guid object for storing the GUID.
			' ***
			
			HidD_GetHidGuid(hidGuid)
			
			'Debug.WriteLine(MyDebugging.ResultOfAPICall("GetHidGuid"))
			'Debug.WriteLine("  GUID for system HIDs: " & hidGuid.ToString)
			
			' Fill an array with the device path names of all attached HIDs.
			
			deviceFound = MyDeviceManagement.FindDeviceFromGuid _
				(hidGuid, _
				devicePathName)
			
			' If there is at least one HID, attempt to read the Vendor ID and Product ID
			' of each device until there is a match or all devices have been examined.
			
			If deviceFound Then
				
				memberIndex = 0
				
				Do
					' ***
					' API function:
					' CreateFile
					
					' Purpose:
					' Retrieves a handle to a device.
					
					' Accepts:
					' A device path name returned by SetupDiGetDeviceInterfaceDetail
					' The type of access requested (read/write).
					' FILE_SHARE attributes to allow other processes to access the device while this handle is open.
					' A Security structure or IntPtr.Zero. 
					' A creation disposition value. Use OPEN_EXISTING for devices.
					' Flags and attributes for files. Not used for devices.
					' Handle to a template file. Not used.
					
					' Returns: a handle without read or write access.
					' This enables obtaining information about all HIDs, even system
					' keyboards and mice. 
					' Separate handles are used for reading and writing.
					' ***
					
					' Open the handle without read/write access to enable getting information about any HID, even system keyboards and mice.
					
					hidHandle = CreateFile (devicePathName(memberIndex), 0,FILE_SHARE_READ Or FILE_SHARE_WRITE, IntPtr.Zero,OPEN_EXISTING, 0, 0)
					
					'Debug.WriteLine(MyDebugging.ResultOfAPICall("CreateFile"))
					'Debug.WriteLine("  Returned handle: " & hidHandle.ToString)
					
					If Not (hidHandle.IsInvalid) Then
						
						' The returned handle is valid, 
						' so find out if this is the device we're looking for.
						
						' Set the Size property of DeviceAttributes to the number of bytes in the structure.
						
						MyHid.DeviceAttributes.Size = Marshal.SizeOf(MyHid.DeviceAttributes)
						
						' ***
						' API function:
						' HidD_GetAttributes
						
						' Purpose:
						' Retrieves a HIDD_ATTRIBUTES structure containing the Vendor ID, 
						' Product ID, and Product Version Number for a device.
						
						' Accepts:
						' A handle returned by CreateFile.
						' A pointer to receive a HIDD_ATTRIBUTES structure.
						
						' Returns:
						' True on success, False on failure.
						' ***
						
						success = HidD_GetAttributes(hidHandle, MyHid.DeviceAttributes)
						
						If success Then
							
							Descrip += "  HIDD_ATTRIBUTES structure filled without error."
							Descrip += "  Structure size: " & MyHid.DeviceAttributes.Size
							Descrip += "  Vendor ID: " & Hex(MyHid.DeviceAttributes.VendorID)
							Descrip += "  Product ID: " & Hex(MyHid.DeviceAttributes.ProductID)
							Descrip += "  Version Number: " & Hex(MyHid.DeviceAttributes.VersionNumber)
							
							If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(Descrip)
							' Find out if the device matches the one we're looking for.
							
							If (MyHid.DeviceAttributes.VendorID = myVendorID) And _
								(MyHid.DeviceAttributes.ProductID = myProductID) Then
								
								Descrip += "  My device detected"
								If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(Descrip)
								' Display the information in form's list box.
								
								'lstResults.Items.Add("Device detected:")
								'lstResults.Items.Add("  Vendor ID= " & Hex(MyHid.DeviceAttributes.VendorID))
								'lstResults.Items.Add("  Product ID = " & Hex(MyHid.DeviceAttributes.ProductID))
								
								'ScrollToBottomOfListBox()
								
								myDeviceDetected = True
								
								' Save the DevicePathName for OnDeviceChange().
								
								myDevicePathName = devicePathName(memberIndex)
								'tmrContinuousDataCollect.Start()
							Else
								
								' It's not a match, so close the handle.
								'lstResults.Items.Add("Device NOT detected:")
								
								myDeviceDetected = False
								
								hidHandle.Close()
								
							End If
							
						Else
							' There was a problem in retrieving the information.
							
							Descrip +="  Error in filling HIDD_ATTRIBUTES structure."
							If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(Descrip)
							myDeviceDetected = False
							hidHandle.Close()
						End If
						
					End If
					
					' Keep looking until we find the device or there are no devices left to examine.
					
					memberIndex = memberIndex + 1
					
				Loop Until (myDeviceDetected Or (memberIndex = devicePathName.Length))
				
			End If
			
			If myDeviceDetected Then
				
				' The device was detected.
				' Register to receive notifications if the device is removed or attached.
				
				'todo: 
				'success = MyDeviceManagement.RegisterForDeviceNotifications _
				'	(myDevicePathName, _
				'	FrmMy.Handle, _
				'	hidGuid, _
				'	deviceNotificationHandle)
				
				'Debug.WriteLine("RegisterForDeviceNotifications = " & success)
				
				' Learn the capabilities of the device.
				
				MyHid.Capabilities = MyHid.GetDeviceCapabilities(hidHandle)
				
				
				If success Then
					
					' Find out if the device is a system mouse or keyboard.
					
					hidUsage = MyHid.GetHidUsage(MyHid.Capabilities)
					
					' Get the Input report buffer size.
					
					InputBufferSize = GetInputReportBufferSize()					
					
					'Close the handle and reopen it with read/write access.
					
					hidHandle.Close()
					
					hidHandle = CreateFile (myDevicePathName, GENERIC_READ Or GENERIC_WRITE, FILE_SHARE_READ Or FILE_SHARE_WRITE, IntPtr.Zero, OPEN_EXISTING, 0, 0)
					
					If hidHandle.IsInvalid Then
						
						exclusiveAccess = True
						'lstResults.Items.Add("The device is a system " + hidUsage + ".")
						'lstResults.Items.Add("Windows 2000 and Windows XP obtain exclusive access to Input and Output reports for this devices.")
						'lstResults.Items.Add("Applications can access Feature reports only.")
						'ScrollToBottomOfListBox()
						
					Else
						
						If (MyHid.Capabilities.InputReportByteLength > 0) Then
							
							' Set the size of the Input report buffer. 
							' Subtract 1 from the value in the Capabilities structure because 
							' the array begins at index 0.
							
							Array.Resize(inputReportBuffer, MyHid.Capabilities.InputReportByteLength)
							
							fileStreamdevicedata = New FileStream(hidHandle, FileAccess.Read Or FileAccess.Write, inputReportBuffer.Length, False)
							
						End If
						
						If (MyHid.Capabilities.OutputReportByteLength > 0) Then
							
							' Set the size of the Output report buffer. 
							' Subtract 1 from the value in the Capabilities structure because 
							' the array begins at index 0.
							
							Array.Resize(outputReportBuffer, MyHid.Capabilities.OutputReportByteLength)
							
						End If
						
						' Flush any waiting reports in the input buffer. (optional)
						
						MyHid.FlushQueue(hidHandle)
						
					End If
				End If
			Else
				' The device wasn't detected.
				
				'lstResults.Items.Add("Device not found.")
				'cmdInputReportBufferSize.Enabled = False
				'cmdOnce.Enabled = True
				MyAPP.Error_MSC.SalvarRegistreDepuracio(" Device not found.")
				'Debug.WriteLine(" Device not found.")
				If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(Descrip)
				'ScrollToBottomOfListBox()
			End If
			
			Return myDeviceDetected
			
		Catch ex As Exception			
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)
			'Throw
		End Try
	End Function
	
	Friend function GetInputReportBufferSize()As Integer 
		
		Dim numberOfInputBuffers As Int32
		
		Try
			' Get the number of input buffers.
			
			MyHid.GetNumberOfInputBuffers _
				(hidHandle, _
				numberOfInputBuffers)
			
			
			Return(numberOfInputBuffers)
			
		Catch ex As Exception
			
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)
			Return 0
			Throw
		End Try
		
	End Function
	
	Friend Sub SetInputReportBufferSize(byref BuferSize As Integer )
		
		Dim numberOfInputBuffers As Int32
		
		Try
			' Get the number of buffers from the text box.
			
			numberOfInputBuffers = Convert.ToInt32(Val(BuferSize))
			
			' Set the number of buffers.
			
			MyHid.SetNumberOfInputBuffers _
				(hidHandle, _
				numberOfInputBuffers)
			
		Catch ex As Exception
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)
			Throw
		End Try
	End Sub	
	
	Friend Sub CloseCommunications()
		
		If (Not (fileStreamdevicedata Is Nothing)) Then
			
			fileStreamdevicedata.Close()
		End If
		
		If ((Not (hidHandle Is Nothing)) And (Not hidHandle.IsInvalid)) Then
			
			hidHandle.Close()
		End If
		
		' The next attempt to communicate will get new handles and FileStreams.
		
		myDeviceDetected = False
		
		
	End Sub
	
	friend Sub ReadAndWriteToDevice()
		
		'Report header for the debug display:
		
		'Debug.WriteLine("")
		'Debug.WriteLine("***** HID Test Report *****")
		'Debug.WriteLine(Today & ": " & TimeOfDay)
		
		Try
			' If the device hasn't been detected, was removed, or timed out on a previous attempt
			' to access it, look for the device.
			
			If (myDeviceDetected = False) Then
				
				myDeviceDetected = FindTheHid(VENDOR_ID,PRODUCT_ID)
				
			End If
			
			If (myDeviceDetected = True) Then
				
				'rebre i enviar els valors
				ExchangeInputAndOutputReports()
			End If
			
		Catch ex As Exception
			'DisplayException(Me.Name, ex)
			Throw
		End Try
		
	End Sub
	
	Friend Sub ReadToDevice()
		Try
			' If the device hasn't been detected, was removed, or timed out on a previous attempt
			' to access it, look for the device.
			
			If (myDeviceDetected = False) Then
				
				myDeviceDetected = FindTheHid(VENDOR_ID,PRODUCT_ID)
				
			End If
			
			If (myDeviceDetected = True) Then
				
				'rebre els valors
				
				ExchangeInputReports()
			End If
			
		Catch ex As Exception
			'DisplayException(Me.Name, ex)
			Throw
		End Try
	End Sub
	
	Private Sub ExchangeInputReports()
		
		Dim byteValue As String
		Dim count As Int32
		Dim inputReportBuffer() As Byte = Nothing
		'Dim outputReportBuffer() As Byte = Nothing
		Dim success As Boolean
		
		Try
			success = False
			
			' Don't attempt to exchange reports if valid handles aren't available
			' (as for a mouse or keyboard under Windows 2000/XP.)
			
			If (Not (hidHandle.IsInvalid)) Then
				
				' Read an Input report.
				
				success = False
				
				' Don't attempt to send an Input report if the HID has no Input report.
				' (The HID spec requires all HIDs to have an interrupt IN endpoint,
				' which suggests that all HIDs must support Input reports.)
				
				If (MyHid.Capabilities.InputReportByteLength > 0) Then
					
					' Set the size of the Input report buffer. 
					' Subtract 1 from the value in the Capabilities structure because 
					' the array begins at index 0.
					
					Array.Resize(inputReportBuffer, MyHid.Capabilities.InputReportByteLength)
					
					If ControlTransfersOnly = True Then
						
						' Read a report using a control transfer.
						
						success = MyHid.GetInputReportViaControlTransfer(hidHandle, inputReportBuffer)
						
						If success Then
							
							
							'lstResults.Items.Add("An Input report has been read.")
							
							' Display the report data received in the form's list box.
							
							'lstResults.Items.Add(" Input Report ID: " & String.Format("{0:X2} ", inputReportBuffer(0)))
							'lstResults.Items.Add(" Input Report Data:")
							
							'txtBytesReceived.Text = ""
							For count = 1 To UBound(inputReportBuffer)
								
								' Display bytes as 2-character Hex strings.
								
								byteValue = String.Format("{0:X2} ", inputReportBuffer(count))
								'Decodifica la instrucció i executala.
								
								'lstResults.Items.Add(" " & byteValue)
								
								' Display the received bytes in the text box.
								
								'txtBytesReceived.SelectionStart = Len(txtBytesReceived.Text)
								'txtBytesReceived.SelectedText = byteValue & vbCrLf
							Next count
							
						Else
							CloseCommunications()
							'lstResults.Items.Add("The attempt to read an Input report has failed.")
							
						End If
						
						'ScrollToBottomOfListBox()
						
						' Enable requesting another transfer.
						
						'AccessForm("EnableCmdOnce", "")
						
					Else
						' Read a report using interrupt transfers.                
						' To enable reading a report without blocking the main thread, this
						' application uses an asynchronous delegate.
						
						Dim ar As IAsyncResult = Nothing
						transferInProgress = True
						
						' Timeout if no report is available.
						
						'tmrReadTimeout.Start()
						
						If (fileStreamdevicedata.CanRead) Then
							
							fileStreamdevicedata.BeginRead(inputReportBuffer, 0, inputReportBuffer.Length, New AsyncCallback(AddressOf GetInputReportData), inputReportBuffer)
							
						Else
							CloseCommunications()
							'lstResults.Items.Add("The attempt to read an Input report has failed.")
						End If
					End If
				Else
					'lstResults.Items.Add("No attempt to read an Input report was made.")
					'lstResults.Items.Add("The HID doesn't have an Input report.")
					'AccessForm("EnableCmdOnce", "")
				End If
			Else
				'lstResults.Items.Add("Invalid handle. The device is probably a system mouse or keyboard.")
				'lstResults.Items.Add("No attempt to write an Output report or read an Input report was made.")
				'AccessForm("EnableCmdOnce", "")
			End If
			
			'ScrollToBottomOfListBox()
			
			
		Catch ex As Exception
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)
			Throw
		End Try
		
	End Sub
	
	Private Sub ExchangeInputAndOutputReports()
		
		Dim byteValue As String
		Dim count As Int32
		Dim inputReportBuffer() As Byte = Nothing
		Dim outputReportBuffer() As Byte = Nothing
		Dim success As Boolean
		
		Try
			success = False
			
			' Don't attempt to exchange reports if valid handles aren't available
			' (as for a mouse or keyboard under Windows 2000/XP.)
			
			If (Not (hidHandle.IsInvalid)) Then
				
				' Don't attempt to send an Output report if the HID has no Output report.
				
				If (MyHid.Capabilities.OutputReportByteLength > 0) Then
					
					' Set the upper bound of the Output report buffer. 
					' Subtract 1 from OutputReportByteLength because the array begins at index 0.
					
					Array.Resize(outputReportBuffer, MyHid.Capabilities.OutputReportByteLength)
					
					' Store the report ID in the first byte of the buffer:
					
					outputReportBuffer(0) = 0
					
					' Store the report data following the report ID.
					' Use the data in the combo boxes on the form.
					
					'outputReportBuffer(1) = Convert.ToByte(cboByte0.SelectedIndex)
					
					If UBound(outputReportBuffer) > 1 Then
						'outputReportBuffer(2) = Convert.ToByte(cboByte1.SelectedIndex)
					End If
					
					' Write a report.
					
					If ControlTransfersOnly = True Then
						
						' Use a control transfer to send the report,
						' even if the HID has an interrupt OUT endpoint.
						
						success = MyHid.SendOutputReportViaControlTransfer(hidHandle, outputReportBuffer)
						
					Else
						
						' If the HID has an interrupt OUT endpoint, the host uses an 
						' interrupt transfer to send the report. 
						' If not, the host uses a control transfer.
						
						If (fileStreamdevicedata.CanWrite) Then
							fileStreamdevicedata.Write(outputReportBuffer, 0, outputReportBuffer.Length)
							success = True
						Else
							CloseCommunications()
							'lstResults.Items.Add("The attempt to read an Input report has failed.")
						End If
					End If
					
					If success Then
						
						'lstResults.Items.Add("An Output report has been written.")
						
						' Display the report data in the form's list box.
						
						'lstResults.Items.Add(" Output Report ID: " & String.Format("{0:X2} ", outputReportBuffer(0)))
						'lstResults.Items.Add(" Output Report Data:")
						
						'txtBytesReceived.Text = ""
						For count = 1 To UBound(outputReportBuffer)
							
							' Display bytes as 2-character hex strings.
							
							byteValue = String.Format("{0:X2} ", outputReportBuffer(count))
							'lstResults.Items.Add(" " & byteValue)
							
						Next count
					Else
						CloseCommunications()
						'lstResults.Items.Add("The attempt to write an Output report failed.")
					End If
					
					
				Else
					'lstResults.Items.Add("The HID doesn't have an Output report.")
				End If
				
				' Read an Input report.
				
				success = False
				
				' Don't attempt to send an Input report if the HID has no Input report.
				' (The HID spec requires all HIDs to have an interrupt IN endpoint,
				' which suggests that all HIDs must support Input reports.)
				
				If (MyHid.Capabilities.InputReportByteLength > 0) Then
					
					' Set the size of the Input report buffer. 
					' Subtract 1 from the value in the Capabilities structure because 
					' the array begins at index 0.
					
					Array.Resize(inputReportBuffer, MyHid.Capabilities.InputReportByteLength)
					
					If ControlTransfersOnly = True Then
						
						' Read a report using a control transfer.
						
						success = MyHid.GetInputReportViaControlTransfer(hidHandle, inputReportBuffer)
						
						If success Then
							'lstResults.Items.Add("An Input report has been read.")
							
							' Display the report data received in the form's list box.
							
							'lstResults.Items.Add(" Input Report ID: " & String.Format("{0:X2} ", inputReportBuffer(0)))
							'lstResults.Items.Add(" Input Report Data:")
							
							'txtBytesReceived.Text = ""
							For count = 1 To UBound(inputReportBuffer)
								
								' Display bytes as 2-character Hex strings.
								
								byteValue = String.Format("{0:X2} ", inputReportBuffer(count))
								
								'lstResults.Items.Add(" " & byteValue)
								
								' Display the received bytes in the text box.
								
								'txtBytesReceived.SelectionStart = Len(txtBytesReceived.Text)
								'txtBytesReceived.SelectedText = byteValue & vbCrLf
							Next count
						Else
							CloseCommunications()
							'lstResults.Items.Add("The attempt to read an Input report has failed.")
							
						End If
						
						'ScrollToBottomOfListBox()
						
						' Enable requesting another transfer.
						
						'AccessForm("EnableCmdOnce", "")
						
					Else
						' Read a report using interrupt transfers.                
						' To enable reading a report without blocking the main thread, this
						' application uses an asynchronous delegate.
						
						Dim ar As IAsyncResult = Nothing
						transferInProgress = True
						
						' Timeout if no report is available.
						
						'tmrReadTimeout.Start()
						
						If (fileStreamdevicedata.CanRead) Then
							
							fileStreamdevicedata.BeginRead(inputReportBuffer, 0, inputReportBuffer.Length, New AsyncCallback(AddressOf GetInputReportData), inputReportBuffer)
							
						Else
							CloseCommunications()
							'lstResults.Items.Add("The attempt to read an Input report has failed.")
						End If
					End If
				Else
					'lstResults.Items.Add("No attempt to read an Input report was made.")
					'lstResults.Items.Add("The HID doesn't have an Input report.")
					'AccessForm("EnableCmdOnce", "")
				End If
			Else
				'lstResults.Items.Add("Invalid handle. The device is probably a system mouse or keyboard.")
				'lstResults.Items.Add("No attempt to write an Output report or read an Input report was made.")
				'AccessForm("EnableCmdOnce", "")
			End If
			
			'ScrollToBottomOfListBox()
			
		Catch ex As Exception
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)
			Throw
		End Try
		
	End Sub
	
	Private Sub GetInputReportData(ByVal ar As IAsyncResult)
		
		Dim byteValue As String
		Dim count As Int32
		Dim inputReportBuffer As Byte() = Nothing
		
		Try
			inputReportBuffer = CType(ar.AsyncState, Byte())
			'Response = inputReportBuffer
			fileStreamdevicedata.EndRead(ar)
			
			'tmrReadTimeout.Stop()
			
			' Display the received report data in the form's list box.
			
			If (ar.IsCompleted) Then
				Response = inputReportBuffer
				
				'MyMarshalToForm("AddItemToListBox", "An Input report has been read.")
				
				'MyMarshalToForm("AddItemToListBox", " Input Report ID: " & String.Format("{0:X2} ", inputReportBuffer(0)))
				
				'MyMarshalToForm("AddItemToListBox", " Input Report Data:")
				
'				For count = 1 To UBound(inputReportBuffer)
'					
'					' Display bytes as 2-character Hex strings.
'					
'					byteValue = String.Format("{0:X2} ", inputReportBuffer(count))
'					
'					'MyMarshalToForm("AddItemToListBox", " " & byteValue)
'					'MyMarshalToForm("TextBoxSelectionStart", txtBytesReceived.Text)
'					'MyMarshalToForm("AddItemToTextBox", byteValue)
'					
'				Next count
				
			Else
				'MyMarshalToForm("AddItemToListBox", "The attempt to read an Input report has failed.")
				'Debug.Write("The attempt to read an Input report has failed")
				MyAPP.Error_MSC.SalvarRegistreDepuracio("The attempt to read an Input report has failed")
			End If
			
			'MyMarshalToForm("ScrollToBottomOfListBox", "")
			
			' Enable requesting another transfer.
			
			'MyMarshalToForm("EnableCmdOnce", "")
			transferInProgress = False
			
		Catch ex As Exception
			If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(ex.Message)
			'Throw
		End Try
		
	End Sub
	
	#End Region
	
End Class
