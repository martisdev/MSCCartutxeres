Public Class frmCanviDurada

    Friend Sub setLanguageForm()
        lang.StrForm = Me.Name

        lbDurada.Text = lang.getText("HEADERTEXT_DURADA", True)
        lbInfo.Text = lang.getText("lbInfo.Text") 'Durada màxima de la reprodució en streaming
        'Me.Text = lang.getText("Text") '"Hora final"	

    End Sub

    Friend Sub setThemeControls()
        If MyThemeForm = Formthemes.dark Then
            Me.Theme = MetroFramework.MetroThemeStyle.Dark
        Else
            Me.Theme = MetroFramework.MetroThemeStyle.Light
        End If
        lbInfo.Theme = Me.Theme
        lbDurada.Theme = Me.Theme
    End Sub

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub frmCanviDurada_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setLanguageForm()
        setThemeControls()
    End Sub

End Class