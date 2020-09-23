'
' Creado por SharpDevelop.
' Usuario: Marti
' Fecha: 17/07/2016
' Hora: 09:45
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Partial Class frmRemote
	Inherits System.Windows.Forms.Form
	
	''' <summary>
	''' Designer variable used to keep track of non-visual components.
	''' </summary>
	Private components As System.ComponentModel.IContainer
	
	''' <summary>
	''' Disposes resources used by the form.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If disposing Then
			If components IsNot Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
	
	''' <summary>
	''' This method is required for Windows Forms designer support.
	''' Do not change the method contents inside the source code editor. The Forms designer might
	''' not be able to load this method if it was changed manually.
	''' </summary>
	Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container()
		Me.lbInfo = New System.Windows.Forms.Label()
		Me.timerCOM = New System.Windows.Forms.Timer(Me.components)
		Me.lbState = New System.Windows.Forms.Label()
		Me.SuspendLayout
		'
		'lbInfo
		'
		Me.lbInfo.Location = New System.Drawing.Point(12, 40)
		Me.lbInfo.Name = "lbInfo"
		Me.lbInfo.Size = New System.Drawing.Size(281, 16)
		Me.lbInfo.TabIndex = 3
		Me.lbInfo.Text = "Connecting"
		Me.lbInfo.TextAlign = System.Drawing.ContentAlignment.TopCenter
		'
		'timerCOM
		'
		Me.timerCOM.Enabled = true
		AddHandler Me.timerCOM.Tick, AddressOf Me.TimerCOMTick
		'
		'lbState
		'
		Me.lbState.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
		Me.lbState.Location = New System.Drawing.Point(6, 13)
		Me.lbState.Name = "lbState"
		Me.lbState.Size = New System.Drawing.Size(296, 23)
		Me.lbState.TabIndex = 4
		'
		'frmRemote
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(305, 69)
		Me.Controls.Add(Me.lbState)
		Me.Controls.Add(Me.lbInfo)
		Me.Name = "frmRemote"
		Me.ShowInTaskbar = false
		Me.Text = "Control via MSC CTL04"
		AddHandler FormClosing, AddressOf Me.FrmRemoteFormClosing
		AddHandler Load, AddressOf Me.FrmRemoteLoad
		AddHandler Shown, AddressOf Me.FrmRemoteShown
		Me.ResumeLayout(false)
	End Sub
	Private lbState As System.Windows.Forms.Label
	Private timerCOM As System.Windows.Forms.Timer
	Private lbInfo As System.Windows.Forms.Label
End Class
