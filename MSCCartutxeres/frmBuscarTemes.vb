Option Strict Off
Option Explicit On

Public Class frmBuscarTemes
	Public OptAvanç As Boolean ' Per fer recerca avançada
	Public IsClosed As Boolean


    Private Sub cmdMoreFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdMoreFind.Click
		If OptAvanç = True Then
			OptAvanç = False
            Me.Height = 411
            cmdMoreFind.Text = LABEL_AVANCAT
			Me.fraAvanç.Visible = OptAvanç
			Me.chkAddResult.CheckState = False
		Else
			OptAvanç = True
            Me.Height = 622
            cmdMoreFind.Text = LABEL_SENZILL
			Me.fraAvanç.Visible = OptAvanç
		End If
		Me.txtNomTema.Focus()
	End Sub

    Private Sub frmBuscarTemes_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		setLanguageForm()


        If OptAvanç Then Me.Height = 566 : cmdMoreFind.Text = LABEL_SENZILL
        Me.fraAvanç.Visible = OptAvanç
        setThemeControls()
    End Sub

    Dim LABEL_SENZILL As String
    Dim LABEL_AVANCAT As String

    Public Sub setLanguageForm()
        lang.StrForm = Me.Name
        LABEL_SENZILL = lang.getText("LABEL_SENZILL", True) '"<<Senzill"
        LABEL_AVANCAT = lang.getText("LABEL_AVANCAT", True) '"Avançats>>"
        Me.Label1.Text = lang.getText("Label1.Text") '"Buscar cançons segons un filtre determinat."
        Me.Label2.Text = lang.getText("Label2.Text") '"Nom cançó"
        Me.Label3.Text = lang.getText("HEADERTEXT_INTERP", True) '"Intèrpret"
        Me.Label4.Text = lang.getText("HEADERTEXT_DISC", True) '"Disc"
        Me.Label5.Text = lang.getText("HEADERTEXT_ESTIL", True) '"Estil"
        Me.Label6.Text = lang.getText("HEADERTEXT_IDIOMA", True) '"Idioma"
        Me.Label7.Text = lang.getText("LABEL_CLAS_TEMP", True) '"Clas. temporal"
        Me.Label8.Text = lang.getText("LABEL_CLAS_SUBJ", True) '"Clas. Subjectiva"
        Me.fraPauta.Text = lang.getText("LABEL_DISPONIBILITAT", True) '"Disponibilitat"
        Me.chkNoFiltre.Text = lang.getText("LABEL_INDIFERENT", True) '"Indiferent"
        Me.chkOutP.Text = lang.getText("LABEL_DESACTIVA", True) '"Desectivada"
        Me.chkInP.Text = lang.getText("LABEL_ACTIVA", True) '"Activada"
        Me.fraAvanç.Text = lang.getText("LABEL_RECERCA_ABANCADA", True) '"Recerca avançada"
        Me.grupOrderBy.Text = lang.getText("grupOrderBy.Text") '"Ordenar per"
        Me.chkNumRadia.Text = lang.getText("chkNumRadia.Text") '"Número de radiacions"
        Me.chkDateInDESC.Text = lang.getText("chkDateInDESC.Text") '"Data entrada descendent"
        Me.chkDateInASC.Text = lang.getText("chkDateInASC.Text") '"Data entrada ascendent"

        Me.Label13.Text = lang.getText("LABEL_ANY_PUBLICACIO", True) '"Any publicació"
        Me.Label12.Text = lang.getText("LABEL_BPM", True) '"Ritme (BPM)"
        Me.Label11.Text = lang.getText("LABEL_COMMENT", True) '"Comentari"
        Me.Label10.Text = lang.getText("LABEL_COMPOSITOR", True) '"Compositor"
        Me.chkRestric.Text = lang.getText("LABEL_RETRICCIO", True) '"Restricció"
        Me.chkIntrumental.Text = lang.getText("LABEL_INSTRUMENTAL", True) '"Instrumental"
        Me.chkBPMFort.Text = lang.getText("LABEL_FILTRE_FORT", True) '"Filtratge fort"
        Me.chkAddResult.Text = lang.getText("LABEL_ADD_RESULT", True) '"Afegir a la recerca anterior"
        Me.cmdMoreFind.Text = IIf(OptAvanç = False, LABEL_AVANCAT, LABEL_SENZILL).ToString
        Me.Label9.Text = lang.getText("label_comodi", True) '"% signe comodí"

        Me.Text = lang.getText("LABEL_BUSCAR_CANÇONS", True) '"Buscar cançons ..."		
        ' Omple el combo estils
        Dim Cmb As New combo
        Cmb.OmpleCombo(cbEstil, TAULES_DBS.TAULA_ESTILS, TotsCap.TOTS) : cbEstil.SelectedValue = 0
        ' Omple el combo d'Interp
        Cmb.OmpleCombo(cbInterp, TAULES_DBS.TAULA_INTERPRETS, TotsCap.TOTS) : cbInterp.SelectedValue = 0
        ' Omple el combo Discos
        Cmb.OmpleCombo(cbDisc, TAULES_DBS.TAULA_DISCOS, TotsCap.TOTS) : cbDisc.SelectedValue = 0
        ' Omple el combo subjectiva
        Cmb.OmpleCombo(cbSubject, TAULES_DBS.TAULA_SUBJECTIV, TotsCap.TOTS) : cbSubject.SelectedValue = 0
        ' Omple el combo temporal
        Cmb.OmpleCombo(cbTemp, TAULES_DBS.TAULA_TEMPORALITAT, TotsCap.TOTS) : cbTemp.SelectedValue = 0
        ' Omple el combo Idioma
        Cmb.OmpleCombo(cbIdioma, TAULES_DBS.TAULA_IDIOMES, TotsCap.TOTS, "lang_visible=1") : cbIdioma.SelectedValue = 0
    End Sub

    Private Sub CmdTancar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmdTancar.Click, cmdSortir.Click
		Me.Hide()
		IsClosed = True
	End Sub
	
	Private Sub cmdBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdBuscar.Click
        Static LastQuery As String = ""
        Dim LocalStrSql As String = ""
        Dim VarRitme1, VarRitme2 As Short
		If Len(Me.txtNomTema.Text) > 0 Then LocalStrSql = "tema_titol LIKE '%" & AddSlashes(Me.txtNomTema.Text) & "%' "
		If Me.cbDisc.SelectedValue > 0 Then
			If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
			LocalStrSql = LocalStrSql & " tema_disc = " & cbDisc.SelectedValue
		End If
		If Me.cbEstil.SelectedValue > 0 Then
			If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "						
			Dim sCmd As String = "style_relationships_tipfitxer = " & Tipus_Fitxers.FITXER_MUSICA & " AND `style_relationships_style` = " & cbEstil.SelectedValue
			Dim colec() As DataRow = dsClient.Tables("style_relationships").Select(sCmd)			
			Dim StrSql As String = "("
			For i As Integer = 0 To colec.Length - 1
				If StrSql.Length> 3 Then StrSql += " OR "
				Dim tema_id As Integer = colec(i)("style_relationships_fitxer").ToString
				StrSql += "tema_id = " & tema_id 						
			Next	
			LocalStrSql += StrSql & ")"
		End If
		If Me.cbInterp.SelectedValue > 0 Then
			If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
			LocalStrSql = LocalStrSql & " tema_interp = " & cbInterp.SelectedValue
		End If
		If cbIdioma.SelectedValue > 0 Then
			If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
			LocalStrSql = LocalStrSql & " tema_idioma = " & cbIdioma.SelectedValue
		End If
		If Me.cbTemp.SelectedValue > 0 Then
			If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
			LocalStrSql = LocalStrSql & " tema_temps = " & cbTemp.SelectedValue
		End If
		If Me.cbSubject.SelectedValue > 0 Then
			If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
			LocalStrSql = LocalStrSql & " tema_subj = " & cbSubject.SelectedValue
		End If
		
		If Me.chkInP.Checked Then
			If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
			LocalStrSql = LocalStrSql & " tema_pautain = 1 "
		ElseIf Me.chkOutP.Checked Then
			If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
			LocalStrSql = LocalStrSql & " tema_pautain = 0"
		End If
		
		'Opcions avançades
		If OptAvanç Then
			If Len(Me.txtComent.Text) > 0 Then
				If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
				LocalStrSql = "tema_comentari LIKE '%" & Me.txtComent.Text & "%' "
			End If
			If Len(Me.txtCompositor.Text) > 0 Then
				If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
				LocalStrSql = "tema_compositor LIKE '%" & Me.txtCompositor.Text & "%' "
			End If
			If Len(Me.txtRitme.Text) > 0 Then
				If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
				If Me.chkBPMFort.CheckState Then
					VarRitme1 = CDbl(Me.txtRitme.Text) - 20
					VarRitme2 = CDbl(Me.txtRitme.Text) + 20
				Else
					VarRitme1 = CDbl(Me.txtRitme.Text) - 40
					VarRitme2 = CDbl(Me.txtRitme.Text) + 40
				End If
				LocalStrSql = LocalStrSql & " tema_ritme BETWEEN " & VarRitme1 & " AND " & VarRitme2
			End If
			If Len(Me.txtAny.Text) > 0 Then
				If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
				LocalStrSql = LocalStrSql & " tema_any = " & Me.txtAny.Text
			End If
			If Me.chkIntrumental.Checked Then
				If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
				LocalStrSql = LocalStrSql & " tema_instrument = 1 "
			End If
			If Me.chkRestric.Checked Then
				If Len(LocalStrSql) > 0 Then LocalStrSql = LocalStrSql & " AND "
				LocalStrSql = LocalStrSql & " tema_restric = 1 "
			End If
		End If
        If chkAddResult.Checked = True And LastQuery.Length > 0 Then
            LocalStrSql += " OR " & LastQuery
        End If
        LastQuery = LocalStrSql

        If Len(LocalStrSql) = 0 Then LocalStrSql = " tema_instrument = 1 OR tema_instrument = 0 "
        If Me.chkDateInASC.Checked = True Then LocalStrSql = LocalStrSql & " ORDER BY tema_datain ASC"
        If Me.chkDateInDESC.Checked = True Then LocalStrSql = LocalStrSql & " ORDER BY tema_datain DESC"
        If Me.chkNumRadia.Checked = True Then LocalStrSql = LocalStrSql & " ORDER BY tema_numradiacions DESC"

        My.Forms.frmAudioDBS.getListTemes(LocalStrSql)

        Me.txtNomTema.Focus()
	End Sub
	
	Private Sub KeyPressComboBox(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
		Handles cbEstil.KeyPress, cbDisc.KeyPress, _
		cbIdioma.KeyPress, cbInterp.KeyPress, _
		cbSubject.KeyPress, cbTemp.KeyPress
		e.KeyChar = UCase(e.KeyChar)
	End Sub
	
	Private Sub KeyPress_txtNumeric(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRitme.KeyPress, txtAny.KeyPress
		Dim KeyAscii As Short = Asc(e.KeyChar)
		If KeyAscii < Asc("0") Or KeyAscii > Asc("9") Then If KeyAscii <> 8 Then KeyAscii = 0
		e.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			e.Handled = True
		End If
	End Sub
	
	Private Sub KeyUp_Combo(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles _
		cbEstil.KeyUp, cbDisc.KeyUp _
		, cbIdioma.KeyUp, cbInterp.KeyUp _
		, cbSubject.KeyUp, cbTemp.KeyUp
		Select Case e.KeyCode
			Case Keys.Left, Keys.Right, Keys.Up, Keys.Down
				Return
		End Select
		Dim MyComboBox As ComboBox = CType(sender, ComboBox)
		Dim Origen As TAULES_DBS
		
		Select Case MyComboBox.Name
			Case "cbEstil" : Origen = TAULES_DBS.TAULA_ESTILS
			Case "cbDisc" : Origen = TAULES_DBS.TAULA_DISCOS
			Case "cbIdioma" : Origen = TAULES_DBS.TAULA_IDIOMES
			Case "cbInterp" : Origen = TAULES_DBS.TAULA_INTERPRETS
			Case "cbSubject" : Origen = TAULES_DBS.TAULA_SUBJECTIV
			Case "cbTemp" : Origen = TAULES_DBS.TAULA_TEMPORALITAT
		End Select
		refreshComboBoxKeyUp(MyComboBox, Origen, TotsCap.TOTS)
		
	End Sub

    Friend Sub setThemeControls()
        If MyThemeForm = Formthemes.dark Then
            Me.Theme = MetroFramework.MetroThemeStyle.Dark
            fraPauta.ForeColor = Color.DeepSkyBlue
            fraAvanç.ForeColor = Color.DeepSkyBlue
            grupOrderBy.ForeColor = Color.DeepSkyBlue

        Else
            Me.Theme = MetroFramework.MetroThemeStyle.Light
            fraPauta.ForeColor = Color.Black
            fraAvanç.ForeColor = Color.Black
            grupOrderBy.ForeColor = Color.Black
        End If
        cmdSortir.Theme = Me.Theme
        Label2.Theme = Me.Theme
        Label3.Theme = Me.Theme
        Label4.Theme = Me.Theme
        Label5.Theme = Me.Theme
        Label6.Theme = Me.Theme
        Label7.Theme = Me.Theme
        Label8.Theme = Me.Theme
        Label9.Theme = Me.Theme
        Label10.Theme = Me.Theme
        Label11.Theme = Me.Theme
        Label12.Theme = Me.Theme
        Label13.Theme = Me.Theme
        chkInP.Theme = Me.Theme
        chkOutP.Theme = Me.Theme
        chkNoFiltre.Theme = Me.Theme
        chkBPMFort.Theme = Me.Theme
        chkIntrumental.Theme = Me.Theme
        chkRestric.Theme = Me.Theme
        chkBPMFort.Theme = Me.Theme
        chkDateInASC.Theme = Me.Theme
        chkDateInDESC.Theme = Me.Theme
        chkNumRadia.Theme = Me.Theme
        chkAddResult.Theme = Me.Theme

        Me.Refresh()
    End Sub
End Class