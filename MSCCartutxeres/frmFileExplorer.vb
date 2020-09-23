'
' Creado por SharpDevelop.
' Usuario: Martí
' Fecha: 25/01/2017
' Hora: 17:02
' 
' Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
'
Imports System.IO
Imports BassTags = Un4seen.Bass.AddOn.Tags.BassTags
Imports Un4seen.Bass

Public Partial Class frmFileExplorer
	
	Dim DirIni As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments
	Dim Cancel As Boolean = False
	Public Sub New()
		' The Me.InitializeComponent call is required for Windows Forms designer support.
		Me.InitializeComponent()
		
	End Sub
	
	Dim LABEL_DIR As String 
	Public Sub setLanguageForm()		
		lang.StrForm = Me.Name
		
		LABEL_DIR = "Directory"
		Me.colNom.Text = "Nom"
		Me.colInterp.Text = "Intèrpret"
		Me.cloDurada.Text = "Durada"
		Me.mnuPreEscolta.Text = "Pre-escolta PLAY"
		Me.mnuPreescoltaStop.Text = "Pre-escolta STOP"
		Me.mnuAddRepreoduccio.Text = "Afegir a reproducció"
		Me.mnuEditAudio.Text = "Editar l'àudio"
		Me.Text = "Explorador de fitxers"
		
		
	End Sub
	
	Friend Sub AfegirAReproducció_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
		listAudioFromDBS = Nothing
		MoveFromExplorer = False
		For i As Integer = 0 To ListViewFitxers.SelectedItems.Count - 1
			If Not IsNothing(ListViewFitxers.SelectedItems(i).Tag) Then
				Dim PathFitxer As String = ListViewFitxers.SelectedItems(i).Tag.ToString
				Dim IDCart As String = CType(sender, ToolStripMenuItem).Tag.ToString
                Dim formCart As frmCartutxera = Nothing
                Dim frm As Form
                For Each frm In My.Application.OpenForms
                    If frm.Name = "frmCartutxera" And frm.Text.Contains("Cart " & IDCart) Then
                        Try
                            formCart = CType(frm, frmCartutxera)
                            formCart.CarregaFitxer(PathFitxer)
                            Exit For
                        Catch ex As Exception
                        End Try
                    End If
                Next
            End If
		Next		
	End Sub	
	
	Private Sub PopulateTreeView() 	
		Cancel = False
		Dim info As New DirectoryInfo(Me.comboBoxPath.Text)
		Dim rootNode As TreeNode
		If info.Exists Then
			rootNode = New TreeNode(info.Name)
            rootNode.Tag = info.FullName
            GetDirectories(info.GetDirectories(), rootNode)
			treeView1.Nodes.Add(rootNode)			
		End If
		
	End Sub
	
	Private Sub GetDirectories(ByVal subDirs() As DirectoryInfo, _
		ByVal nodeToAddTo As TreeNode)
		
		Dim aNode As TreeNode
		Dim subSubDirs() As DirectoryInfo
		Dim subDir As DirectoryInfo
		For Each subDir In subDirs
			aNode = New TreeNode(subDir.Name, 0, 0)
			aNode.Tag = subDir
			aNode.ImageKey = "folder"
			Try
				subSubDirs = subDir.GetDirectories()
				If subSubDirs.Length <> 0 Then
					GetDirectories(subSubDirs, aNode)
				End If
				nodeToAddTo.Nodes.Add(aNode)				
			Catch ex As Exception				
				'Accés denegat
			End Try
			
		Next subDir
		
	End Sub
	
	Dim LastNode As TreeNode
	Dim LastNodeParent As TreeNode

    Sub TreeView1NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles treeView1.NodeMouseClick

        Dim newSelected As TreeNode = e.Node
        PintaTreView(newSelected)
    End Sub

    Sub PintaTreView(newSelected As TreeNode)
        Cancel = False
        LastNode = newSelected
        If Not IsNothing(newSelected.Parent) Then LastNodeParent = newSelected.Parent

        listViewFitxers.Items.Clear()
        Dim nodeDirInfo As DirectoryInfo = CType(newSelected.Tag, DirectoryInfo)
        Dim subItems() As ListViewItem.ListViewSubItem
        Dim item As ListViewItem = Nothing
        'per anar al dir parent
        item = New ListViewItem("..", 0)
        subItems = New ListViewItem.ListViewSubItem() {New ListViewItem.ListViewSubItem(item, LABEL_DIR)}
        item.SubItems.AddRange(subItems)
        listViewFitxers.Items.Add(item)

        Dim dir As DirectoryInfo
        For Each dir In nodeDirInfo.GetDirectories()
            item = New ListViewItem(dir.Name, 0)
            subItems = New ListViewItem.ListViewSubItem() {New ListViewItem.ListViewSubItem(item, LABEL_DIR)}

            item.SubItems.AddRange(subItems)
            listViewFitxers.Items.Add(item)
            'My.Application.DoEvents()
            If Cancel = True Then Exit For
        Next dir
        Dim file As FileInfo
        Dim filters As String() = {"*.mp3", "*.mp2", "*.mp1", "*.wav", "*.cda", "*.ogg", "*.aiff", "*.aif"}
        Dim Handle As Integer
        Dim tLength As Single
        Dim lenTrack As Long
        Dim TI As New Un4seen.Bass.AddOn.Tags.TAG_INFO

        For Each file In nodeDirInfo.GetFiles()
            Dim PathFile As String = file.FullName
            Dim Title As String = file.Name
            Dim interp As String = file.Name
            Dim Duration As Date = "00:00:00"
            Dim inxImage As Integer

            Select Case LCase(file.Extension)
                Case ".mp3", ".mp2", ".mp1"
                    inxImage = 1
                Case ".wav"
                    inxImage = 2
                Case ".ogg"
                    inxImage = 3
                Case ".aiff"
                    inxImage = 4
                Case ".aif"
                    inxImage = 5
                Case ".ptc"
                    inxImage = 6
                    'Title = Path.GetFileName(PathFile)
                    interp = "llistat"
                Case ".m3u"
                    inxImage = 7
                    'Title = Path.GetFileName(PathFile)
                    interp = "llistat"
                Case ".cda"
                    inxImage = 8
                Case Else : inxImage = 99
            End Select
            If inxImage <> 99 Then
                If inxImage < 6 Then
                    Handle = Bass.BASS_StreamCreateFile(PathFile, 0, 0, BASSFlag.BASS_DEFAULT)
                    If (BassTags.BASS_TAG_GetFromFile(Handle, TI)) Then
                        interp = TI.artist
                        Title = TI.title
                    End If
                    lenTrack = Bass.BASS_ChannelGetLength(Handle)
                    tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
                    Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
                End If

                If Title.Length = 0 Then Title = Path.GetFileName(PathFile)
                If interp.Length = 0 Then interp = file.Name

                item = New ListViewItem(Title, inxImage)
                item.Tag = PathFile
                subItems = New ListViewItem.ListViewSubItem() {New ListViewItem.ListViewSubItem(item, interp), New ListViewItem.ListViewSubItem(item, Duration)}
                item.SubItems.AddRange(subItems)
                listViewFitxers.Items.Add(item)
            End If
            'My.Application.DoEvents()
            If Cancel = True Then Exit For
        Next file
        listViewFitxers.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize)
    End Sub

    Sub ButtonRefresca_Click(sender As Object, e As EventArgs) Handles buttonRefresca.Click
        refreshDirectories()
    End Sub

    Sub refreshDirectories()
        Me.listViewFitxers.Items.Clear()
        treeView1.Nodes.Clear()
        If IO.Directory.Exists(Me.comboBoxPath.Text) Then PopulateTreeView()
    End Sub

    Sub ButtonCancelClick(sender As Object, e As EventArgs) Handles buttonCancel.Click
        Cancel = True
    End Sub

    Sub ListViewFitxersItemDrag(sender As Object, e As ItemDragEventArgs) Handles listViewFitxers.ItemDrag
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            DoDragDrop(e.Item, DragDropEffects.Copy)
        End If
    End Sub

    Sub ListViewFitxersMouseDown(sender As Object, e As MouseEventArgs) Handles listViewFitxers.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Left And e.Clicks = 1 Then
            DragLVDisp = CType(sender, ListView).GetItemAt(e.X, e.Y)
            ActualFrmExplorer = Me
            MoveFromExplorer = True
        End If
    End Sub

    Sub ComboBoxPathSelectedIndexChanged(sender As Object, e As EventArgs) Handles comboBoxPath.SelectedIndexChanged
        If NoLoad = False Then
            refreshDirectories()
        End If
    End Sub

    Sub ListViewFitxersMouseDoubleClick(sender As Object, e As MouseEventArgs) Handles listViewFitxers.MouseDoubleClick
        If IsNothing(listViewFitxers.HitTest(e.Location).Item.Tag) Then
            Dim lvi As ListViewItem = listViewFitxers.HitTest(e.Location).Item
            Try
                Dim subCarp As TreeNode
                If lvi.Text = ".." Then
                    subCarp = LastNodeParent
                Else
                    subCarp = LastNode.Nodes(listViewFitxers.Items.IndexOf(lvi) - 1)
                End If
                PintaTreView(subCarp)
            Catch ex As Exception
            End Try
        End If
    End Sub

    Dim NoLoad As Boolean
    Sub FrmFileExplorerLoad(sender As Object, e As EventArgs) Handles MyBase.Load
        setLanguageForm()
        NoLoad = True
        Me.comboBoxPath.Items.Add(My.Computer.FileSystem.SpecialDirectories.MyDocuments)
        Me.comboBoxPath.Items.Add(My.Computer.FileSystem.SpecialDirectories.MyMusic)
        Me.comboBoxPath.Items.Add(My.Computer.FileSystem.SpecialDirectories.Desktop)

        Dim allDrives() As DriveInfo = DriveInfo.GetDrives()
        Dim d As DriveInfo
        For Each d In allDrives
            Me.comboBoxPath.Items.Add(d.Name)
        Next
        NoLoad = False
        Me.comboBoxPath.SelectedIndex = 0

        Dim frm As Form
        Dim NumCart As Short = 1
        For Each frm In My.Application.OpenForms
            If frm.Name = "frmCartutxera" Then
                Dim mnuNewCart As ToolStripMenuItem
                mnuNewCart = New ToolStripMenuItem
                mnuNewCart.Text = "Cartutxera " & NumCart
                Dim KeyShortCut As System.Windows.Forms.Keys = CType(Choose(NumCart, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9), System.Windows.Forms.Keys)
                mnuNewCart.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or KeyShortCut), System.Windows.Forms.Keys)
                mnuNewCart.Tag = NumCart
                mnuNewCart.Visible = True
                mnuAddRepreoduccio.DropDownItems.Add(mnuNewCart)
                AddHandler mnuNewCart.Click, AddressOf AfegirAReproducció_Click
                NumCart = NumCart + 1
            End If
        Next
        If ProgEditAudio.Length > 0 Then mnuEditAudio.Enabled = True
        setThemeControls()
        Me.comboBoxPath.Text = DirIni
    End Sub

    Sub MnuPreEscoltaClick(sender As Object, e As EventArgs) Handles mnuPreEscolta.Click
        Dim PathFitxer As String = Me.listViewFitxers.SelectedItems(0).Tag
        PlayerPre.PlayAudio(PathFitxer)
    End Sub

    Sub MnuPreescoltaStopClick(sender As Object, e As EventArgs) Handles mnuPreescoltaStop.Click
        PlayerPre.StopAudio()
    End Sub

    Sub MnuEditAudioClick(sender As Object, e As EventArgs) Handles mnuEditAudio.Click
        Dim PathFitxer As String = ""
        For i As Integer = 0 To listViewFitxers.SelectedItems.Count - 1
            Dim tempPath As String = Me.listViewFitxers.SelectedItems(i).Tag
            If IO.File.Exists(tempPath) Then
                PathFitxer += " " & Chr(34) & tempPath & Chr(34)
            End If
        Next

        If PathFitxer.Length < 3 Then Exit Sub
        Dim p As System.Diagnostics.Process = New System.Diagnostics.Process()
        p.StartInfo.Arguments = PathFitxer.Trim
        p.StartInfo.FileName = ProgEditAudio
        Try
            p.Start()
        Catch ex As Exception
        End Try
    End Sub

    Friend Sub setThemeControls()
        If MyThemeForm = Formthemes.dark Then
            Me.Theme = MetroFramework.MetroThemeStyle.Dark
        Else
            Me.Theme = MetroFramework.MetroThemeStyle.Light
        End If
        Me.Refresh()
    End Sub

    Private Sub comboBoxPath_TextChanged(sender As Object, e As EventArgs) Handles comboBoxPath.TextChanged
        If NoLoad = False Then

        End If
    End Sub
End Class
