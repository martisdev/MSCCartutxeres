'
' Creado por SharpDevelop.
' Usuario: Marti
' Fecha: 22/10/2016
' Hora: 11:31
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Partial Class frmAirence
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAirence))
		Me.groupBox5 = New System.Windows.Forms.GroupBox()
		Me.button32 = New System.Windows.Forms.Button()
		Me.button31 = New System.Windows.Forms.Button()
		Me.button30 = New System.Windows.Forms.Button()
		Me.button29 = New System.Windows.Forms.Button()
		Me.button28 = New System.Windows.Forms.Button()
		Me.button27 = New System.Windows.Forms.Button()
		Me.button26 = New System.Windows.Forms.Button()
		Me.button25 = New System.Windows.Forms.Button()
		Me.button24 = New System.Windows.Forms.Button()
		Me.button23 = New System.Windows.Forms.Button()
		Me.button22 = New System.Windows.Forms.Button()
		Me.button21 = New System.Windows.Forms.Button()
		Me.button20 = New System.Windows.Forms.Button()
		Me.button19 = New System.Windows.Forms.Button()
		Me.button18 = New System.Windows.Forms.Button()
		Me.button17 = New System.Windows.Forms.Button()
		Me.button16 = New System.Windows.Forms.Button()
		Me.button15 = New System.Windows.Forms.Button()
		Me.button14 = New System.Windows.Forms.Button()
		Me.button13 = New System.Windows.Forms.Button()
		Me.button12 = New System.Windows.Forms.Button()
		Me.button11 = New System.Windows.Forms.Button()
		Me.button10 = New System.Windows.Forms.Button()
		Me.button9 = New System.Windows.Forms.Button()
		Me.lbLedEvent = New System.Windows.Forms.Label()
		Me.groupBox5.SuspendLayout
		Me.SuspendLayout
		'
		'groupBox5
		'
		Me.groupBox5.Controls.Add(Me.button32)
		Me.groupBox5.Controls.Add(Me.button31)
		Me.groupBox5.Controls.Add(Me.button30)
		Me.groupBox5.Controls.Add(Me.button29)
		Me.groupBox5.Controls.Add(Me.button28)
		Me.groupBox5.Controls.Add(Me.button27)
		Me.groupBox5.Controls.Add(Me.button26)
		Me.groupBox5.Controls.Add(Me.button25)
		Me.groupBox5.Controls.Add(Me.button24)
		Me.groupBox5.Controls.Add(Me.button23)
		Me.groupBox5.Controls.Add(Me.button22)
		Me.groupBox5.Controls.Add(Me.button21)
		Me.groupBox5.Controls.Add(Me.button20)
		Me.groupBox5.Controls.Add(Me.button19)
		Me.groupBox5.Controls.Add(Me.button18)
		Me.groupBox5.Controls.Add(Me.button17)
		Me.groupBox5.Controls.Add(Me.button16)
		Me.groupBox5.Controls.Add(Me.button15)
		Me.groupBox5.Controls.Add(Me.button14)
		Me.groupBox5.Controls.Add(Me.button13)
		Me.groupBox5.Controls.Add(Me.button12)
		Me.groupBox5.Controls.Add(Me.button11)
		Me.groupBox5.Controls.Add(Me.button10)
		Me.groupBox5.Controls.Add(Me.button9)
		Me.groupBox5.Location = New System.Drawing.Point(23, 32)
		Me.groupBox5.Name = "groupBox5"
		Me.groupBox5.Size = New System.Drawing.Size(149, 365)
		Me.groupBox5.TabIndex = 32
		Me.groupBox5.TabStop = false
		Me.groupBox5.Text = "Control"
		'
		'button32
		'
		Me.button32.BackColor = System.Drawing.Color.Transparent
		Me.button32.Location = New System.Drawing.Point(97, 329)
		Me.button32.Name = "button32"
		Me.button32.Size = New System.Drawing.Size(32, 23)
		Me.button32.TabIndex = 23
		Me.button32.Tag = "24"
		Me.button32.Text = "24"
		Me.button32.UseVisualStyleBackColor = false
		AddHandler Me.button32.Click, AddressOf Me.ButtonPlay_Click
		'
		'button31
		'
		Me.button31.BackColor = System.Drawing.Color.Transparent
		Me.button31.Location = New System.Drawing.Point(58, 329)
		Me.button31.Name = "button31"
		Me.button31.Size = New System.Drawing.Size(32, 23)
		Me.button31.TabIndex = 22
		Me.button31.Tag = "23"
		Me.button31.Text = "23"
		Me.button31.UseVisualStyleBackColor = false
		AddHandler Me.button31.Click, AddressOf Me.ButtonPlay_Click
		'
		'button30
		'
		Me.button30.BackColor = System.Drawing.Color.Transparent
		Me.button30.Location = New System.Drawing.Point(19, 329)
		Me.button30.Name = "button30"
		Me.button30.Size = New System.Drawing.Size(32, 23)
		Me.button30.TabIndex = 21
		Me.button30.Tag = "22"
		Me.button30.Text = "22"
		Me.button30.UseVisualStyleBackColor = false
		AddHandler Me.button30.Click, AddressOf Me.ButtonPlay_Click
		'
		'button29
		'
		Me.button29.BackColor = System.Drawing.Color.Transparent
		Me.button29.Location = New System.Drawing.Point(97, 300)
		Me.button29.Name = "button29"
		Me.button29.Size = New System.Drawing.Size(32, 23)
		Me.button29.TabIndex = 20
		Me.button29.Tag = "21"
		Me.button29.Text = "21"
		Me.button29.UseVisualStyleBackColor = false
		AddHandler Me.button29.Click, AddressOf Me.ButtonPlay_Click
		'
		'button28
		'
		Me.button28.BackColor = System.Drawing.Color.Transparent
		Me.button28.Location = New System.Drawing.Point(58, 300)
		Me.button28.Name = "button28"
		Me.button28.Size = New System.Drawing.Size(32, 23)
		Me.button28.TabIndex = 19
		Me.button28.Tag = "20"
		Me.button28.Text = "20"
		Me.button28.UseVisualStyleBackColor = false
		AddHandler Me.button28.Click, AddressOf Me.ButtonPlay_Click
		'
		'button27
		'
		Me.button27.BackColor = System.Drawing.Color.Transparent
		Me.button27.Location = New System.Drawing.Point(19, 300)
		Me.button27.Name = "button27"
		Me.button27.Size = New System.Drawing.Size(32, 23)
		Me.button27.TabIndex = 18
		Me.button27.Tag = "19"
		Me.button27.Text = "19"
		Me.button27.UseVisualStyleBackColor = false
		AddHandler Me.button27.Click, AddressOf Me.ButtonPlay_Click
		'
		'button26
		'
		Me.button26.BackColor = System.Drawing.Color.Transparent
		Me.button26.Location = New System.Drawing.Point(97, 271)
		Me.button26.Name = "button26"
		Me.button26.Size = New System.Drawing.Size(32, 23)
		Me.button26.TabIndex = 17
		Me.button26.Tag = "18"
		Me.button26.Text = "18"
		Me.button26.UseVisualStyleBackColor = false
		AddHandler Me.button26.Click, AddressOf Me.ButtonPlay_Click
		'
		'button25
		'
		Me.button25.BackColor = System.Drawing.Color.Transparent
		Me.button25.Location = New System.Drawing.Point(19, 271)
		Me.button25.Name = "button25"
		Me.button25.Size = New System.Drawing.Size(32, 23)
		Me.button25.TabIndex = 16
		Me.button25.Tag = "17"
		Me.button25.Text = "17"
		Me.button25.UseVisualStyleBackColor = false
		AddHandler Me.button25.Click, AddressOf Me.ButtonPlay_Click
		'
		'button24
		'
		Me.button24.BackColor = System.Drawing.Color.Transparent
		Me.button24.Location = New System.Drawing.Point(84, 234)
		Me.button24.Name = "button24"
		Me.button24.Size = New System.Drawing.Size(32, 23)
		Me.button24.TabIndex = 15
		Me.button24.Tag = "16"
		Me.button24.Text = "16"
		Me.button24.UseVisualStyleBackColor = false
		AddHandler Me.button24.Click, AddressOf Me.ButtonPlay_Click
		'
		'button23
		'
		Me.button23.BackColor = System.Drawing.Color.Transparent
		Me.button23.Location = New System.Drawing.Point(32, 234)
		Me.button23.Name = "button23"
		Me.button23.Size = New System.Drawing.Size(32, 23)
		Me.button23.TabIndex = 14
		Me.button23.Tag = "15"
		Me.button23.Text = "15"
		Me.button23.UseVisualStyleBackColor = false
		AddHandler Me.button23.Click, AddressOf Me.ButtonPlay_Click
		'
		'button22
		'
		Me.button22.BackColor = System.Drawing.Color.Transparent
		Me.button22.Location = New System.Drawing.Point(84, 205)
		Me.button22.Name = "button22"
		Me.button22.Size = New System.Drawing.Size(32, 23)
		Me.button22.TabIndex = 13
		Me.button22.Tag = "14"
		Me.button22.Text = "14"
		Me.button22.UseVisualStyleBackColor = false
		AddHandler Me.button22.Click, AddressOf Me.ButtonPlay_Click
		'
		'button21
		'
		Me.button21.BackColor = System.Drawing.Color.Transparent
		Me.button21.Location = New System.Drawing.Point(32, 205)
		Me.button21.Name = "button21"
		Me.button21.Size = New System.Drawing.Size(32, 23)
		Me.button21.TabIndex = 12
		Me.button21.Tag = "13"
		Me.button21.Text = "13"
		Me.button21.UseVisualStyleBackColor = false
		AddHandler Me.button21.Click, AddressOf Me.ButtonPlay_Click
		'
		'button20
		'
		Me.button20.BackColor = System.Drawing.Color.Transparent
		Me.button20.Location = New System.Drawing.Point(84, 176)
		Me.button20.Name = "button20"
		Me.button20.Size = New System.Drawing.Size(32, 23)
		Me.button20.TabIndex = 11
		Me.button20.Tag = "12"
		Me.button20.Text = "12"
		Me.button20.UseVisualStyleBackColor = false
		AddHandler Me.button20.Click, AddressOf Me.ButtonPlay_Click
		'
		'button19
		'
		Me.button19.BackColor = System.Drawing.Color.Transparent
		Me.button19.Location = New System.Drawing.Point(32, 176)
		Me.button19.Name = "button19"
		Me.button19.Size = New System.Drawing.Size(32, 23)
		Me.button19.TabIndex = 10
		Me.button19.Tag = "11"
		Me.button19.Text = "11"
		Me.button19.UseVisualStyleBackColor = false
		AddHandler Me.button19.Click, AddressOf Me.ButtonPlay_Click
		'
		'button18
		'
		Me.button18.BackColor = System.Drawing.Color.Transparent
		Me.button18.Location = New System.Drawing.Point(84, 147)
		Me.button18.Name = "button18"
		Me.button18.Size = New System.Drawing.Size(32, 23)
		Me.button18.TabIndex = 9
		Me.button18.Tag = "10"
		Me.button18.Text = "10"
		Me.button18.UseVisualStyleBackColor = false
		AddHandler Me.button18.Click, AddressOf Me.ButtonPlay_Click
		'
		'button17
		'
		Me.button17.BackColor = System.Drawing.Color.Transparent
		Me.button17.Location = New System.Drawing.Point(32, 147)
		Me.button17.Name = "button17"
		Me.button17.Size = New System.Drawing.Size(32, 23)
		Me.button17.TabIndex = 8
		Me.button17.Tag = "9"
		Me.button17.Text = "9"
		Me.button17.UseVisualStyleBackColor = false
		AddHandler Me.button17.Click, AddressOf Me.ButtonPlay_Click
		'
		'button16
		'
		Me.button16.BackColor = System.Drawing.Color.Transparent
		Me.button16.Location = New System.Drawing.Point(84, 118)
		Me.button16.Name = "button16"
		Me.button16.Size = New System.Drawing.Size(32, 23)
		Me.button16.TabIndex = 7
		Me.button16.Tag = "8"
		Me.button16.Text = "8"
		Me.button16.UseVisualStyleBackColor = false
		AddHandler Me.button16.Click, AddressOf Me.ButtonPlay_Click
		'
		'button15
		'
		Me.button15.BackColor = System.Drawing.Color.Transparent
		Me.button15.Location = New System.Drawing.Point(32, 118)
		Me.button15.Name = "button15"
		Me.button15.Size = New System.Drawing.Size(32, 23)
		Me.button15.TabIndex = 6
		Me.button15.Tag = "7"
		Me.button15.Text = "7"
		Me.button15.UseVisualStyleBackColor = false
		AddHandler Me.button15.Click, AddressOf Me.ButtonPlay_Click
		'
		'button14
		'
		Me.button14.BackColor = System.Drawing.Color.Transparent
		Me.button14.Location = New System.Drawing.Point(84, 87)
		Me.button14.Name = "button14"
		Me.button14.Size = New System.Drawing.Size(32, 23)
		Me.button14.TabIndex = 5
		Me.button14.Tag = "6"
		Me.button14.Text = "6"
		Me.button14.UseVisualStyleBackColor = false
		AddHandler Me.button14.Click, AddressOf Me.ButtonPlay_Click
		'
		'button13
		'
		Me.button13.BackColor = System.Drawing.Color.Transparent
		Me.button13.Location = New System.Drawing.Point(32, 87)
		Me.button13.Name = "button13"
		Me.button13.Size = New System.Drawing.Size(32, 23)
		Me.button13.TabIndex = 4
		Me.button13.Tag = "5"
		Me.button13.Text = "5"
		Me.button13.UseVisualStyleBackColor = false
		AddHandler Me.button13.Click, AddressOf Me.ButtonPlay_Click
		'
		'button12
		'
		Me.button12.BackColor = System.Drawing.Color.Transparent
		Me.button12.Location = New System.Drawing.Point(84, 60)
		Me.button12.Name = "button12"
		Me.button12.Size = New System.Drawing.Size(32, 23)
		Me.button12.TabIndex = 3
		Me.button12.Tag = "4"
		Me.button12.Text = "4"
		Me.button12.UseVisualStyleBackColor = false
		AddHandler Me.button12.Click, AddressOf Me.ButtonPlay_Click
		'
		'button11
		'
		Me.button11.BackColor = System.Drawing.Color.Transparent
		Me.button11.Location = New System.Drawing.Point(32, 60)
		Me.button11.Name = "button11"
		Me.button11.Size = New System.Drawing.Size(32, 23)
		Me.button11.TabIndex = 2
		Me.button11.Tag = "3"
		Me.button11.Text = "3"
		Me.button11.UseVisualStyleBackColor = false
		AddHandler Me.button11.Click, AddressOf Me.ButtonPlay_Click
		'
		'button10
		'
		Me.button10.BackColor = System.Drawing.Color.Transparent
		Me.button10.Location = New System.Drawing.Point(84, 31)
		Me.button10.Name = "button10"
		Me.button10.Size = New System.Drawing.Size(32, 23)
		Me.button10.TabIndex = 1
		Me.button10.Tag = "2"
		Me.button10.Text = "2"
		Me.button10.UseVisualStyleBackColor = false
		AddHandler Me.button10.Click, AddressOf Me.ButtonPlay_Click
		'
		'button9
		'
		Me.button9.BackColor = System.Drawing.Color.Transparent
		Me.button9.Location = New System.Drawing.Point(32, 31)
		Me.button9.Name = "button9"
		Me.button9.Size = New System.Drawing.Size(32, 23)
		Me.button9.TabIndex = 0
		Me.button9.Tag = "1"
		Me.button9.Text = "1"
		Me.button9.UseVisualStyleBackColor = false
		AddHandler Me.button9.Click, AddressOf Me.ButtonPlay_Click
		'
		'lbLedEvent
		'
		Me.lbLedEvent.BackColor = System.Drawing.Color.Red
		Me.lbLedEvent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
		Me.lbLedEvent.Location = New System.Drawing.Point(538, 9)
		Me.lbLedEvent.Name = "lbLedEvent"
		Me.lbLedEvent.Size = New System.Drawing.Size(41, 19)
		Me.lbLedEvent.TabIndex = 33
		'
		'frmAirence
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(585, 440)
		Me.Controls.Add(Me.lbLedEvent)
		Me.Controls.Add(Me.groupBox5)
		Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
		Me.Name = "frmAirence"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Test Airence"
		AddHandler FormClosing, AddressOf Me.FrmAirenceFormClosing
		AddHandler Load, AddressOf Me.FrmAirenceLoad
		AddHandler Shown, AddressOf Me.FrmAirenceShown
		Me.groupBox5.ResumeLayout(false)
		Me.ResumeLayout(false)
	End Sub
	Private lbLedEvent As System.Windows.Forms.Label
	Private button9 As System.Windows.Forms.Button
	Private button10 As System.Windows.Forms.Button
	Private button11 As System.Windows.Forms.Button
	Private button12 As System.Windows.Forms.Button
	Private button13 As System.Windows.Forms.Button
	Private button14 As System.Windows.Forms.Button
	Private button15 As System.Windows.Forms.Button
	Private button16 As System.Windows.Forms.Button
	Private button17 As System.Windows.Forms.Button
	Private button18 As System.Windows.Forms.Button
	Private button19 As System.Windows.Forms.Button
	Private button20 As System.Windows.Forms.Button
	Private button21 As System.Windows.Forms.Button
	Private button22 As System.Windows.Forms.Button
	Private button23 As System.Windows.Forms.Button
	Private button24 As System.Windows.Forms.Button
	Private button25 As System.Windows.Forms.Button
	Private button26 As System.Windows.Forms.Button
	Private button27 As System.Windows.Forms.Button
	Private button28 As System.Windows.Forms.Button
	Private button29 As System.Windows.Forms.Button
	Private button30 As System.Windows.Forms.Button
	Private button31 As System.Windows.Forms.Button
	Private button32 As System.Windows.Forms.Button
	Private groupBox5 As System.Windows.Forms.GroupBox
End Class
