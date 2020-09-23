'
' Creado por SharpDevelop.
' Usuario: nomai_000
' Fecha: 03/02/2016
' Hora: 0:24
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Public Partial Class frmHistory
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
		'
		' TODO : Add constructor code after InitializeComponents
		'
	End Sub

    Sub FrmHistoryFormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "HISTORY_L", CStr(Me.Left))
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "HISTORY_T", CStr(Me.Top))
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "HISTORY_W", CStr(Me.Width))
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "HISTORY_H", CStr(Me.Height))

        Dim Cancel As Boolean = e.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = e.CloseReason
        Select Case UnloadMode
            Case CloseReason.UserClosing
                Cancel = True
            Case Else
                Cancel = False
        End Select
        e.Cancel = Cancel
        If Cancel = True Then Me.Hide()
    End Sub

    Friend Sub addElementlistHistory(MyPlay As ListAudioSelect)
        Dim newValue(7) As String
        newValue(0) = MyPlay.AUDIO_Titol
        newValue(1) = MyPlay.AUDIO_SubTitol
        newValue(2) = MyPlay.AUDIO_Durada
        newValue(3) = Now.ToShortTimeString
        newValue(4) = MyPlay.AUDIO_BPM
        newValue(5) = MyPlay.AUDIO_ID
        newValue(6) = MyPlay.AUDIO_TipFitxer
        newValue(7) = MyPlay.AUDIO_Path
        lstDispHistory.Rows.Add(newValue)

        ShowDetallTrack(MyPlay.AUDIO_TipFitxer, MyPlay.AUDIO_ID, MyPlay.AUDIO_Titol, MyPlay.AUDIO_SubTitol)
    End Sub

    Private Sub ShowDetallTrack(type As Tipus_Play, Id_track As Integer, strTitol As String, strSubtitol As String)

        Select Case type
            Case Tipus_Play.CTL_MUSICA : Me.picDetall.Image = getImageDiscFromTema(Id_track)
            Case Tipus_Play.CTL_PROGRAMA : Me.picDetall.Image = getImagePrograma(Id_track)
            Case Else : Me.picDetall.Image = Params.LogoEmpresa
        End Select
        panelImage.Visible = Not IsNothing(Me.picDetall.Image)


        lbTitol.Text = strTitol & " - " & strSubtitol
        lbDetallInfo.Text = strInfoFitxer(type, Id_track)
    End Sub

    Sub FrmHistoryLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        setLanguageForm()
        Dim FitxerINI As New IniFile
        Me.Left = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "HISTORY_L", 10))
        Me.Top = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "HISTORY_T", 10))
        Me.Width = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "HISTORY_W", 400))
        Me.Height = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "HISTORY_H", 500))
        setThemeControls()
    End Sub

    Public Sub setLanguageForm()
        lang.StrForm = Me.Name

        Me.ColumnTitol.HeaderText = lang.getText("HEADERTEXT_TITOL", True) '"Títol"
        Me.ColumnInterp.HeaderText = lang.getText("LABEL_INTERP_CLIENT", True) '"Intèrpret/Client"
        Me.ColumnDurada.HeaderText = lang.getText("HEADERTEXT_DURADA", True) '"Durada"
        Me.ColumnRadiacio.HeaderText = lang.getText("LABEL_HORA_RADI", True) '"Hora radi."
        Me.ColumnRitme.HeaderText = lang.getText("HEADERTEXT_RITME", True) '"Ritme"
        Me.tabPage1.Text = lang.getText("tabPage1.Text") '"Detall"
        Me.tabPage2.Text = lang.getText("tabPage2.Text") '"Historal"

        Me.Text = lang.getText("Text") '"Detall/Historial de reproducció"

    End Sub

    Sub LstDispHistoryItemDrag(sender As Object, e As ItemDragEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            DoDragDrop(e.Item, DragDropEffects.Copy)
        End If
    End Sub


    Sub LstDispHistoryMouseDown(sender As Object, e As MouseEventArgs)
        If e.Button = System.Windows.Forms.MouseButtons.Left And e.Clicks = 1 Then
            DragLVDisp = CType(sender, ListView).GetItemAt(e.X, e.Y)
            MoveFromHistory = True
        End If
    End Sub

    Friend Function getImageDiscFromTema(tema_id As Integer )As Image
        Dim db As New MSC.dbs(Cloud)
        Dim StrSql As String = "SELECT img_image FROM image_discos,temes  WHERE tema_disc= img_disc And tema_id=" & tema_id & " ;"
		Try
			Return GetImageFromByteArray(CType(db.ExecuteScalar(StrSql), Byte()))			
		Catch ex As Exception
			Return Nothing			
		End Try
		
		'db = Nothing
	End Function
	
	Friend Function getImagePrograma(Session_id As Integer )As Image
        Dim db As New MSC.dbs(Cloud)
        Dim StrSql As String = "SELECT prg_imatge FROM programes ,prg_sessions WHERE `programes`.`prg_id` = `prg_sessions`.`ses_prg` AND ses_id = " & Session_id & " ;"
		Try
			Return GetImageFromByteArray(CType(db.ExecuteScalar(StrSql), Byte()))			
		Catch ex As Exception			
			Return Nothing
		End Try	
		
		'db = Nothing
	End Function

    Friend Sub setThemeControls()
        If MyThemeForm = Formthemes.dark Then
            Me.Theme = MetroFramework.MetroThemeStyle.Dark
        Else
            Me.Theme = MetroFramework.MetroThemeStyle.Light
        End If
        lstDispHistory.Theme = Me.Theme
        tabControl1.Theme = Me.Theme
        tabPage1.Theme = Me.Theme
        tabPage2.Theme = Me.Theme
        Me.lbDetallInfo.Theme = Me.Theme
        Me.lbTitol.Theme = Me.Theme

        Me.Refresh()
    End Sub

    Private Sub frmHistory_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Dim pw As Integer = CInt(Me.Height / 2)
        If pw > 250 Then pw = 250
        If pw < 175 Then
            pw = 175
            lbDetallInfo.FontSize = MetroFramework.MetroLabelSize.Small
        Else
            lbDetallInfo.FontSize = MetroFramework.MetroLabelSize.Medium
        End If
        panelImage.Width = pw
        panelImage.Height = panelImage.Width

    End Sub

    Private Function createListPlayerToCopy() As ListAudioSelect()
        Dim AudioToPlay() As ListAudioSelect = Nothing

        Try
            Dim NewID As Integer = 0
            For i As Integer = 0 To lstDispHistory.SelectedRows.Count - 1
                Dim Tipus As Tipus_Play = lstDispHistory.SelectedRows(i).Cells(6).Value
                Dim ID As Integer = lstDispHistory.SelectedRows(i).Cells(5).Value
                Dim Titol As String = lstDispHistory.SelectedRows(i).Cells(0).Value
                Dim SubTitol As String = lstDispHistory.SelectedRows(i).Cells(1).Value
                Dim PathFitxer As String = lstDispHistory.SelectedRows(i).Cells(7).Value
                Dim Durada As Date = lstDispHistory.SelectedRows(i).Cells(2).Value
                Dim BPM As Single = lstDispHistory.SelectedRows(i).Cells(4).Value

                ReDim Preserve AudioToPlay(NewID)
                AudioToPlay(NewID).AUDIO_TipFitxer = Tipus
                AudioToPlay(NewID).AUDIO_ID = ID
                AudioToPlay(NewID).AUDIO_Path = PathFitxer
                AudioToPlay(NewID).AUDIO_Titol = Titol
                AudioToPlay(NewID).AUDIO_Durada = Durada
                AudioToPlay(NewID).AUDIO_BPM = BPM
                AudioToPlay(NewID).AUDIO_SubTitol = SubTitol
                NewID = NewID + 1
            Next i
            'MoveInterPlayers = False
            Return AudioToPlay
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Private Sub lstDispHistory_MouseDown(sender As Object, e As MouseEventArgs) Handles lstDispHistory.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            listAudioFromDBS = createListPlayerToCopy()
            If Not IsNothing(listAudioFromDBS) Then Me.lstDispHistory.DoDragDrop(listAudioFromDBS, DragDropEffects.Copy)
        End If
    End Sub
End Class

