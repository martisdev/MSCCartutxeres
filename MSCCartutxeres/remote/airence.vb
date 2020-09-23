
Friend Module Airence
	
		#Region " Constants"
	Friend Const LED_ALL As Integer =  0 '0xFF
	Friend Const SW_LED_1 As Integer = 1
	Friend Const SW_LED_2 As Integer = 2
	Friend Const SW_LED_3 As Integer = 3
	Friend Const SW_LED_4 As Integer = 4
	Friend Const SW_LED_5 As Integer = 5
	Friend Const SW_LED_6 As Integer = 6
	Friend Const SW_LED_7 As Integer = 7
	Friend Const SW_LED_8 As Integer = 8
	Friend Const SW_LED_9 As Integer = 9
	Friend Const SW_LED_10 As Integer = 10
	Friend Const SW_LED_11 As Integer = 11
	Friend Const SW_LED_12 As Integer = 12
	Friend Const SW_LED_13 As Integer = 13
	Friend Const SW_LED_14 As Integer = 14
	Friend Const SW_LED_15 As Integer = 15
	Friend Const SW_LED_16 As Integer = 16
	Friend Const SW_LED_17 As Integer = 17
	Friend Const SW_LED_18 As Integer = 18
	Friend Const SW_LED_19 As Integer = 19
	Friend Const SW_LED_20 As Integer = 20
	Friend Const SW_LED_21 As Integer = 21
	Friend Const SW_LED_22 As Integer = 22
	Friend Const SW_LED_23 As Integer = 23
	Friend Const SW_LED_24 As Integer = 24
	
	Friend Const SW_ENCODER As Integer = 25
	Friend Const SW_NONSTOP As Integer = 26
	
	Friend Const SW_USB1_FADERSTART As Integer = 27
	Friend Const SW_USB1_ON As Integer = 28
	Friend Const SW_USB1_CUE As Integer = 29
	
	Friend Const SW_USB2_FADERSTART As Integer = 30
	Friend Const SW_USB2_ON As Integer = 31
	Friend Const SW_USB2_CUE As Integer = 32
	
	Friend Const SW_USB3_FADERSTART As Integer = 33
	Friend Const SW_USB3_ON As Integer = 34
	Friend Const SW_USB3_CUE As Integer = 35
	
	Friend Const SW_USB4_FADERSTART As Integer = 36
	Friend Const SW_USB4_ON As Integer = 37
	Friend Const SW_USB4_CUE As Integer = 38
	
	'-Enumerations------
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
	
	#End Region
	
	''' <summary>
	''' The function airenceOpen() must be called before any other function of this library may be used. The
	''' function will try to setup the communication with the Airence mixer and initializes the library. After
	''' initialization the initial states of the control signals are read and stored internally in the library. The
	'LED’s behind the control switches are turned off.
	''' </summary>
	''' <returns>On succes the Function returns 0, Or -1 otherwise</returns>
	Friend Declare Function airenceOpen Lib "airence.dll" () As Integer
	
		'When done using the library one needs to call this function to free the allocated memory and finalize
	'the library. The LED’s behind the control switches are turned off. On succes the function returns 0, or -
	'1 otherwise.
	Friend Declare Function airenceClose Lib "airence.dll" () As Integer
	
		'int airenceSetLed( int lednr, colors_t color );
	'Set the specified LED lednr with color. One can choose a value for lednr in the range of 1~24. These
	'		LED’s are the LED’s behind the control switches. On succes the Function returns 0, Or -1 otherwise.
	Friend Declare Function airenceSetLed Lib "airence.dll" (lednr As Integer,color As Integer)As Integer 

	'int airenceSetLedBlink( int lednr, colors_t on, colors_t off, blink_speed_t speed );
	'Set the specified LED lednr in blink mode. The on/off-color and speed can be given to the function by
	'the arguments. On succes the function returns 0, or -1 otherwise.
	Friend Declare Function airenceSetLedBlink Lib "airence.dll" (lednr As Integer,color_on As Integer,color_off As Integer,speed As Integer)As Integer 
	
	'To retrieve the state of a single control signal this function must be called. The controlsignal argument
	'can have a value in the range of 1~38 (see control signal defines in this document). The argument
	'state is a pointer to a boolean variable. The variable itself will be set to TRUE(1) for press state, and
	'False(0) For release state. On succes the Function returns 0, Or -1 otherwise.
	Friend Declare Function airenceGetControlSignal Lib "airence.dll" (controlsignal As Integer,state As Boolean)As Integer 
	
	
	'In order to retrieve the state of all the control signals this function must be called. Reserve a buffer of
	'at least 6 bytes to store the data and give the pointer of that buffer as argument to this function. Every
	'bit presents the state of a control signal, ‘1’ for press and ‘0’ for release state. The data will be
	'presented As can be seen In the table below. On succes the Function returns 0, Or -1 otherwise.
	Friend Declare Function airenceGetRawControlData Lib "airence.dll" (byval data As Char  )As Integer 
	
	'This function returns the version of this library. The major and minor arguments can be used to store
	'the version, otherwise one has to fill in these arguments with the value NULL. Besides the version the
	'build Date Of the library will be returned.
	Friend Declare Function airenceGetLibraryVersion Lib "airence.dll" (ByVal major As Integer ,ByVal minor As Integer )As Integer
	
		'This function returns the version of the firmware present in the Airence mixer. The major and minor
	'	arguments can
	Friend Declare Function airenceGetFirmwareVersion Lib "airence.dll" (byval major As Integer ,byval minor As Integer )As Integer 
	
		'Sets the callback function for a control signal change event. If there occurs a state change in one of
	'the control signals, this callback will be called. The data parameter when registering the callback will
	'be provided as parameter in the callback function. The function returns void.
	
	'Public Shared Function airenceSetControlSignalChangeCB Lib "airence.dll"(ByVal CallbackAddr As CSC_CB) As Long		
	'End Function
	
	Friend Delegate sub CSC_CB(controlsignal As Integer , state As Boolean , byval data As Integer)
	
	Friend Declare Function airenceSetControlSignalChangeCB Lib "airence.dll" (x As CSC_CB, byval data As Integer)As Integer
		
	'Clears the callback function for a control signal change event which was prior set with the
	'			airenceSetControlSignalChangeCB() Function. The Function returns void.
	Friend Declare Function airenceClearControlSignalChangeCB Lib "airence.dll" ()As Integer 
	
	
	Friend Declare Function airenceSetEncoderChangeCB Lib "airence.dll" (x As EC_CB, byval data As Integer)As Integer
	Friend Delegate sub EC_CB( direction As Integer, abs_value As String , data As Integer)
	Friend Declare Function airenceClearEncoderChangeCB Lib "airence.dll" ()As Integer
	
	'readmode value:
	' -1: Blocking
	'  0: Non-Blocking 
	' >0: Non-Blocking with timeout in ms */
	Friend Declare Function airenceSetReadMode Lib "airence.dll"  (readmode As Integer ) As Integer
	
	
	'			This Function returns the last occurred Error In the library.
	Friend Declare Function airenceGetLastError Lib "airence.dll" ()As String	
			
	'Logging can be enabled by calling this function with the argument set to true. Calling the function with
	'the argument false disables the logging. A txt-logfile will be created in the same folder whereas the
	'DLL is located and the library will be used by an application. The logfile logs any kind of activity in the
	'library, Like events, Function calls, Or errors.	
	Friend Declare Sub airenceEnableLogging Lib "airence.dll" (state As Boolean)
	
End Module
