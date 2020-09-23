Public Class frmAddMark

    Private Sub cmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub cmdOK_Click(sender As Object, e As EventArgs) Handles cmdOK.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Public Sub setLanguageForm()

        lang.StrForm = Me.Name
        lbComment.Text = lang.getText("lbComment.Text") '"Comment"
        Me.Text = lang.getText("Text") '"Add Comment to the mark"
    End Sub

    Private Sub frmAddMark_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        setLanguageForm()
        setThemeControls()
    End Sub

    Friend Sub setThemeControls()
        If MyThemeForm = Formthemes.dark Then
            Me.Theme = MetroFramework.MetroThemeStyle.Dark
        Else
            Me.Theme = MetroFramework.MetroThemeStyle.Light
        End If
        Me.txtCommentMark.Theme = Me.Theme
        Me.lbComment.Theme = Me.Theme
        Me.Refresh()
    End Sub

End Class