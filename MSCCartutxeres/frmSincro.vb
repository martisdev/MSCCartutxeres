Public Class frmSincro
	friend isEnd As Boolean = True
	
    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmSincro_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        setLanguageForm()
        setThemeControls()
        Me.txtHoraEnd.Value = Now
    End Sub
    
    Friend Sub setLanguageForm()
    	lang.StrForm = Me.Name
    	
    	If isEnd = True Then
    		Me.Text = lang.getText("Text")'"Hora final"	
    	Else
    		Me.Text = lang.getText("LABEL_HORA_INICI",True)'"Hora inici"	
    	End If    
    	Me.Label1.Text = Me.Text & "..." '"Hora final..."
    	
    End Sub

    Friend Sub setThemeControls()
        If MyThemeForm = Formthemes.dark Then
            Me.Theme = MetroFramework.MetroThemeStyle.Dark
        Else
            Me.Theme = MetroFramework.MetroThemeStyle.Light
        End If

    End Sub

End Class