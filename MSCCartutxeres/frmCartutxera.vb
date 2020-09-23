Option Strict Off
Option Explicit On

Imports BassCd = Un4seen.Bass.AddOn.Cd.BassCd
Imports BassTags = Un4seen.Bass.AddOn.Tags.BassTags
Imports BassHLS = Un4seen.Bass.AddOn.Hls
Imports Un4seen.Bass
Imports VB = Microsoft.VisualBasic
Imports System.Drawing.Drawing2D
Imports System.Threading
Imports WaveForm = Un4seen.Bass.Misc.WaveForm
Imports System.IO.Ports
Imports System.ComponentModel
Imports System.Runtime.InteropServices

Public Class frmCartutxera

#Region "Definició de Variables"


    Enum ControlsInButton
        _ProgressBar = 0
        _LabelDuration = 1
        _PictureBoxCover = 2
        _LabelBPM = 3
        _LabelTRadiacio = 4
        _LabelTitol = 5
        _LabelSubTitol = 6
        _LabelTipus = 7
        _LabelPath = 8
        _LabelID = 9
        _chkSelect = 10
        _lbText = 11
        _LabelHandle = 12
    End Enum


    Const MAX_SEC_WAVE As Integer = 3800 '11000 '(una mica més de 3 hores)


    Dim threshold As Integer
    Dim Atack As Integer
    Dim VolNormalice As Integer
    Dim VolIni As Integer
    Dim SegActivate As Short
    Dim Inc As Short

    Friend ActualPlay As ListAudioSelect
    Dim ProxPlay As New ListAudioSelect

    Private Structure ManageHandle
        Dim HANDLE_PLAY As Integer
        Dim HANDLE_DSP As Integer
        'Dim HANDLE_STOP As Integer
    End Structure

    Dim PlayedHandle() As ManageHandle

    Dim ManualEnd As Boolean = False

    Dim TotalDuration As Long

    Dim TimeFadeOut As Integer = 3000

    Dim InitCue As Boolean



    Dim StrNumCart As String = "1"
    Dim NumCart As Integer
    Dim DEV_PLAY As Integer = 1

    Dim USB_ON As Integer
    Dim USB_CUE As Integer
    Dim USB_FADERSTART As Integer

    Dim DisplayTemps As mdlMscDefines.DisplayTime
    Dim infoPlay As New BASS_SAMPLE
    Dim Invert As Boolean 'display titol

    Private DragLVDispNewPos As System.Windows.Forms.ListViewItem 'The item being dragged

    Dim InfoMsg As frmTip

    Dim bl_AutoSinc As Boolean
    Dim TimeToStart As Date
    Dim SegSH As Long = 0

    Dim filInstucServer As Thread
    Dim filRegistraPlay As Thread = New Thread(AddressOf ThreadRecPlay)
    Dim RI As MSC.InstruccionsRemotes


    Dim CloseAppSilenci As Boolean = False


    Const FORMAT_FITXER As Short = 2

    Dim MSG_STOPPING_ALL_CART As String
    Dim MSG_ATENCIO As String
    Dim MSG_NO_STOP_APP As String
    Dim MSG_ERROR_PLAYER As String
    Dim MSG_ERROR_POSITION As String
    Dim LABEL_SERVER As String
    Dim LABEL_HORA_SINCRO As String
    Dim LABEL_SENYALS_HORARIS As String
    Dim LABEL_H_INI As String
    Dim LABEL_H_END As String
    Dim MSG_ERROR_DURADA_SINCRO As String
    Dim COMMONDIALOG1_TITLE As String
    Dim LIST_FILTER_FILES As String
    Dim LIST_FILTER_CART As String
    Dim LIST_FILTER_WINAMP As String
    Dim SAVE_FILE_TITLE As String
    Dim SAVE_FILE_FILTER As String
    Dim SAVE_FILE_NAME As String
    Dim MSG_OVERWRITE_LIST As String
    Dim MSG_ERROR_BASS_NO_INI As String
    Dim MES_INFO As String
    Dim OPENFILEDIALOG1_TITLE As String
    Dim LABEL_PLAY_SIMPLE As String
    Dim LABEL_PLAY_CONTINU As String
    Dim LABEL_PLAY_LOOP As String
    Dim LABEL_SHOW_BUTTONS As String
    Dim LABEL_SHOW_LIST As String

#End Region

#Region "ButtonsTrack"

    Friend Function addElementlist(ByVal ListAudio() As ListAudioSelect) As Integer
        Dim FirstToAdd As Integer = -1
        For i As Integer = 0 To ListAudio.Length - 1
            If IO.File.Exists(ListAudio(i).AUDIO_Path) = True Or ListAudio(i).AUDIO_TipFitxer = 100 Or ListAudio(i).AUDIO_TipFitxer = 200 Then
                If i = 0 Then
                    FirstToAdd = CreateButtonTrack(ListAudio(i).AUDIO_TipFitxer, ListAudio(i).AUDIO_Titol, ListAudio(i).AUDIO_SubTitol, ListAudio(i).AUDIO_Path, ListAudio(i).AUDIO_ID, ListAudio(i).AUDIO_Durada, ListAudio(i).AUDIO_HoraRadi, ListAudio(i).AUDIO_BPM)
                Else
                    Dim new_id As Integer = CreateButtonTrack(ListAudio(i).AUDIO_TipFitxer, ListAudio(i).AUDIO_Titol, ListAudio(i).AUDIO_SubTitol, ListAudio(i).AUDIO_Path, ListAudio(i).AUDIO_ID, ListAudio(i).AUDIO_Durada, ListAudio(i).AUDIO_HoraRadi, ListAudio(i).AUDIO_BPM)
                    If FirstToAdd = -1 Then FirstToAdd = new_id
                End If
            End If
        Next
        Return FirstToAdd
    End Function

    Friend Function addElementlist(ByRef Tipus As Short, ByRef Titol As String _
        , ByRef SubTitol As String, ByRef PathFitxer As String, ByRef id As Integer _
        , ByRef Durada As Date, Optional ByRef HoraRadi As Date = #12:00:00 AM#, Optional BPM As Single = 0) As Integer

        If IO.File.Exists(PathFitxer) = True Or Tipus = 100 Or Tipus = 101 Or Tipus = 200 Then
            'Si és un stream canvia el titol i subtitol a l'actual.
            If Tipus = 200 Then
                Dim channel As Integer = 0
                Dim URL As String = Replace(PathFitxer.ToLower, ".stream", "") 'esborrem ".STREAM" 
                If UCase(Microsoft.VisualBasic.Right(URL, 4)) = "M3U8" Then
                    channel = BassHLS.BassHls.BASS_HLS_StreamCreateURL(URL, BASSFlag.BASS_DEFAULT, Nothing, IntPtr.Zero)
                Else
                    channel = Bass.BASS_StreamCreateURL(URL, 0, BASSFlag.BASS_DEFAULT, Nothing, IntPtr.Zero)
                End If
                If channel <> 0 Then
                    Dim tagInfo As New AddOn.Tags.TAG_INFO(URL)
                    SubTitol = Params.NomRadio
                    If BassTags.BASS_TAG_GetFromURL(channel, tagInfo) Then
                        Titol = tagInfo.title & " - " & tagInfo.artist
                        SubTitol = tagInfo.album
                    End If
                End If
            End If

            Return CreateButtonTrack(Tipus, Titol, SubTitol, PathFitxer, id, Durada, HoraRadi, BPM)
        Else
            Return -1
        End If
    End Function

    Private Function CreateButtonTrack(ByRef Tipus As Short, ByRef Titol As String _
                                , ByRef SubTitol As String, ByRef PathFitxer As String, ByRef id As Integer _
                                , ByRef Durada As Date, Optional ByRef HoraRadi As Date = #12:00:00 AM#, Optional BPM As Single = 0, Optional IntoTime As Double = 0.0) As Integer

        Dim MetroTileTrack As MetroFramework.Controls.MetroTile = New MetroFramework.Controls.MetroTile()
        Me.flowBotons.Controls.Add(MetroTileTrack)
        MetroTileTrack.AccessibleRole = AccessibleRole.Animation
        'MetroTileTrack.UseCustomBackColor = True
        MetroTileTrack.Text = "" 'Titol & " - " & SubTitol & "   "
        MetroTileTrack.Tag = getMD5Hash(PathFitxer & "-" & flowBotons.Controls.Count) 'un hash 
        MetroTileTrack.Name = "MetroTileTrack" & MetroTileTrack.Tag
        MetroTileTrack.TextAlign = ContentAlignment.TopRight
        MetroTileTrack.AllowDrop = True

        If cmbTypeShow.SelectedIndex = TypeShow.SHOW_LIST Then
            MetroTileTrack.Size = New System.Drawing.Size(flowBotons.Width - Magin, TrackBarBigButtons.Value)
        Else
            MetroTileTrack.Size = New System.Drawing.Size(TrackBarBigButtons.Value * 3.7, TrackBarBigButtons.Value)
        End If

        Select Case Tipus
            Case Tipus_Play.CTL_MUSICA
                MetroTileTrack.Style = MetroFramework.MetroColorStyle.Orange
                MetroTileTrack.TileImage = Me.ImageList.Images.Item(4)
            Case Tipus_Play.CTL_PUBLICITAT
                MetroTileTrack.Style = MetroFramework.MetroColorStyle.Yellow
                MetroTileTrack.TileImage = Me.ImageList.Images.Item(7)
            Case Tipus_Play.CTL_PROGRAMA
                MetroTileTrack.Style = MetroFramework.MetroColorStyle.Teal
                MetroTileTrack.TileImage = Me.ImageList.Images.Item(5)
            Case Tipus_Play.CTL_URL_STREAM
                MetroTileTrack.Style = MetroFramework.MetroColorStyle.Purple
                MetroTileTrack.TileImage = Me.ImageList.Images.Item(16)
            Case Tipus_Play.CTL_META_STOP
                MetroTileTrack.Style = MetroFramework.MetroColorStyle.Red
                MetroTileTrack.TileImage = Me.ImageList.Images.Item(33)
            Case Tipus_Play.CTL_META_MARK
                MetroTileTrack.Style = MetroFramework.MetroColorStyle.Red
                MetroTileTrack.TileImage = Me.ImageList.Images.Item(32)
            Case > 600
                'Tipus_Play.CTL_AUDIO_USR
                Dim id_image As Integer = getLogoAudioUser(Tipus - 600)
                MetroTileTrack.Style = MetroFramework.MetroColorStyle.Lime
                MetroTileTrack.TileImage = Me.ImageList.Images.Item(id_image)
            Case Else
                MetroTileTrack.Style = MetroFramework.MetroColorStyle.Magenta
                'MetroTileTrack.Style = MetroFramework.MetroColorStyle.
                MetroTileTrack.TileImage = Me.ImageList.Images.Item(4)
        End Select
        MetroTileTrack.TileImageAlign = ContentAlignment.BottomRight
        MetroTileTrack.UseTileImage = True
        MetroTileTrack.UseSelectable = True
        MetroTileTrack.UseCustomForeColor = True

        '0 crea ProgressBar
        Dim MetroProgressBarTrack As MetroFramework.Controls.MetroProgressBar = New MetroFramework.Controls.MetroProgressBar() With {
            .Name = "MetroProgressBarTrack" & MetroTileTrack.Tag,
            .Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles),
            .FontSize = MetroFramework.MetroProgressBarSize.Small,
            .HideProgressText = True,
            .Location = New System.Drawing.Point(60, TrackBarBigButtons.Value - 12),
            .Size = New System.Drawing.Size(MetroTileTrack.Width - 100, 10),
            .TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            .Visible = False '(TrackBarBigButtons.Value > 30)            
        }
        MetroTileTrack.Controls.Add(MetroProgressBarTrack)
        Dim StrDurada As String
        If Tipus = Tipus_Play.CTL_URL_STREAM Then
            If Durada = "00:00:00" Then '#1/1/0001 12:00:00 AM#
                StrDurada = Char.ConvertFromUtf32(8734).ToString & " " & LB_DURADA
            Else
                StrDurada = Durada & " " & LB_DURADA
            End If
        Else
            StrDurada = Durada & " " & LB_DURADA
        End If
        '1 crea LabelDuration        
        Dim MetroLabelDuration As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel() With {
            .Name = "MetroLabelDuration" & MetroTileTrack.Tag,
            .Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles),
            .AutoSize = True,
            .FontSize = MetroFramework.MetroLabelSize.Small,
            .TextAlign = System.Drawing.ContentAlignment.TopRight,
            .Location = New System.Drawing.Point(110, TrackBarBigButtons.Value - 27),
            .UseCustomBackColor = True,
            .Text = StrDurada,
            .Tag = Durada
        }
        MetroTileTrack.Controls.Add(MetroLabelDuration)

        '2 crea PictureBoxCover
        Dim PictureBoxCover As System.Windows.Forms.PictureBox = New System.Windows.Forms.PictureBox()
        MetroTileTrack.Controls.Add(PictureBoxCover)
        PictureBoxCover.Name = "PictureBoxCover" & MetroTileTrack.Tag
        Dim MyPicSize As Integer
        Dim Mypoint As Integer
        If cmbTypeShow.SelectedIndex = TypeShow.SHOW_LIST Then
            MyPicSize = MetroTileTrack.Height
            PictureBoxCover.Size = New System.Drawing.Size(MyPicSize - 7, MyPicSize - 7)
            Mypoint = CInt((MyPicSize - PictureBoxCover.Height) / 2)
            PictureBoxCover.Location = New System.Drawing.Point(MyPicSize - PictureBoxCover.Height - 5, Mypoint)
        Else
            MyPicSize = 28
            PictureBoxCover.Size = New System.Drawing.Size(MyPicSize, MyPicSize)
            Mypoint = MetroTileTrack.Height - MyPicSize - 5
            PictureBoxCover.Location = New System.Drawing.Point(MyPicSize - PictureBoxCover.Height + 2, Mypoint)
        End If
        PictureBoxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Select Case Tipus
            Case Tipus_Play.CTL_MUSICA
                PictureBoxCover.Image = getImageDiscFromTema(id)
            Case Tipus_Play.CTL_PUBLICITAT
                PictureBoxCover.Image = Params.LogoEmpresa
            Case Tipus_Play.CTL_PROGRAMA
                PictureBoxCover.Image = getImagePrograma(id)
            Case Tipus_Play.CTL_AUDIO_USR
                PictureBoxCover.Image = Params.LogoEmpresa
            Case Else
                PictureBoxCover.Image = Nothing
        End Select

        '---------
        '3 crea LabelBPM
        Dim MetroLabelBPM As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel() With {
            .Name = "MetroLabelBPM" & MetroTileTrack.Tag,
            .Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles),
            .AutoSize = True,
            .FontSize = MetroFramework.MetroLabelSize.Small,
            .TextAlign = System.Drawing.ContentAlignment.TopRight,
            .Location = New System.Drawing.Point(60, TrackBarBigButtons.Value - 27),
            .UseCustomBackColor = True,
            .Text = BPM & " BPM /",
            .Tag = BPM
        }
        If cmbTypeShow.SelectedIndex = TypeShow.SHOW_LIST Then
            MetroLabelBPM.Visible = True
        Else
            MetroLabelBPM.Visible = False
        End If

        MetroTileTrack.Controls.Add(MetroLabelBPM)
        '-----------

        '4 crea LabelTRadiacio
        Dim LabelTradiacio As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel() With {
            .Name = "LabelTradiacio" & MetroTileTrack.Tag,
            .Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles),
            .AutoSize = True,
            .FontSize = MetroFramework.MetroLabelSize.Small,
            .TextAlign = System.Drawing.ContentAlignment.TopRight,
            .Location = New System.Drawing.Point(210, TrackBarBigButtons.Value - 27),
            .UseCustomBackColor = True,
            .Text = ""
        }
        MetroTileTrack.Controls.Add(LabelTradiacio)

        '5 crea LabelTitol
        Dim LabelTitol As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel() With {
            .Name = "LabelTitol" & MetroTileTrack.Tag,
            .Visible = False,
            .Text = Titol
        }
        MetroTileTrack.Controls.Add(LabelTitol)


        '6 crea LabelSubTitol
        Dim LabelSubTitol As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel With {
            .Name = "LabelSubTitol" & MetroTileTrack.Tag,
            .Visible = False,
            .Text = SubTitol
        }
        MetroTileTrack.Controls.Add(LabelSubTitol)


        '7 crea LabelTipus
        Dim LabelTipus As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel With {
            .Name = "LabelTipus" & MetroTileTrack.Tag,
            .Visible = False,
            .Text = Tipus
        }
        MetroTileTrack.Controls.Add(LabelTipus)


        '8 crea LabelPath        
        Dim LabelPath As New MetroFramework.Controls.MetroLabel With {
            .Name = "LabelPath" & MetroTileTrack.Tag,
            .Visible = False,
            .Text = PathFitxer
        }
        MetroTileTrack.Controls.Add(LabelPath)


        '9 crea LabelID
        Dim LabelID As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel() With {
            .Name = "LabelID" & MetroTileTrack.Tag,
            .Visible = False,
            .Text = id
        }
        MetroTileTrack.Controls.Add(LabelID)


        '10 crea chkSelect
        '.Location = New System.Drawing.Point(MetroTileTrack.Width - 50, TrackBarBigButtons.Value - 21),
        Dim chkSelect As MetroFramework.Controls.MetroCheckBox = New MetroFramework.Controls.MetroCheckBox With {
            .Name = "chkSelect" & MetroTileTrack.Tag,
            .Text = "",
            .Visible = True,
            .UseCustomBackColor = True,
            .Location = New System.Drawing.Point(MetroTileTrack.Width - 16, 0),
            .Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        }
        MetroTileTrack.Controls.Add(chkSelect)

        '11 crea lbText        
        Dim lbText As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel() With {
            .Name = "lbTitol" & MetroTileTrack.Tag,
            .Text = Titol & " - " & SubTitol,
            .Visible = True,
            .UseCustomBackColor = True,
            .UseCustomForeColor = True,
            .ForeColor = Color.Black,
            .Location = New System.Drawing.Point(60, 2),
            .Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles),
            .TextAlign = System.Drawing.ContentAlignment.TopRight,
            .Size = New System.Drawing.Size(MetroTileTrack.Width - 80, 25),
            .AutoSize = False
        }

        Select Case TrackBarBigButtons.Value
            Case 30 To 45 : lbText.FontSize = MetroFramework.MetroLabelSize.Small
            Case 46 To 55 : lbText.FontSize = MetroFramework.MetroLabelSize.Medium
            Case Else : lbText.FontSize = MetroFramework.MetroLabelSize.Tall
        End Select

        MetroTileTrack.Controls.Add(lbText)

        '12 crea LabelHandle
        Dim LabelHandle As MetroFramework.Controls.MetroLabel = New MetroFramework.Controls.MetroLabel() With {
            .Name = "LabelHandle",
            .Visible = False,
            .Text = 0
        }
        MetroTileTrack.Controls.Add(LabelHandle)


        AddHandler MetroLabelBPM.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler PictureBoxCover.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler MetroLabelBPM.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler MetroProgressBarTrack.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler MetroLabelDuration.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler PictureBoxCover.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler MetroLabelBPM.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler LabelTradiacio.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler lbText.MouseDown, AddressOf buttonsTracks_MouseDown

        AddHandler MetroTileTrack.MouseDown, AddressOf buttonsTracks_MouseDown
        AddHandler MetroTileTrack.DragEnter, AddressOf buttonsTracks_DragEnter
        AddHandler MetroTileTrack.DragDrop, AddressOf buttonsTracks_DragDrop
        AddHandler MetroTileTrack.Enter, AddressOf buttonsTracks_Enter

        If Tipus <> Tipus_Play.CTL_META_MARK AndAlso Tipus <> Tipus_Play.CTL_META_STOP Then
            AddHandler MetroTileTrack.PreviewKeyDown, AddressOf buttonsTracks_PreviewKeyDown
        End If
        Me.MetroToolTipTrackMoreInfo.SetToolTip(MetroTileTrack, lbText.Text)

        Return flowBotons.Controls.GetChildIndex(MetroTileTrack)

        'If ActualPlay.AUDIO_ListID = -1 Then Return MyInx

        'If Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS And MyInx - 1 = ActualPlay.AUDIO_ListID Then
        '    SetPoxPlay(MyInx)
        'End If
        'Return MyInx

        'If (ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED) And Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS Then
        '    SetProxTrack(ActualPlay.AUDIO_ListID + 1)
        '    CalcTotalDurationList()
        'End If

    End Function

    Dim ButtonSelectedIndex As Integer = -1

    Private Sub buttonsTracks_Enter(sender As Object, e As EventArgs)
        ButtonSelectedIndex = flowBotons.Controls.GetChildIndex(sender)
    End Sub

    Private Sub buttonsTracks_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs)
        If e.KeyCode = Keys.Enter Then
            Dim MyTile As MetroFramework.Controls.MetroTile
            Try
                MyTile = CType(sender, MetroFramework.Controls.MetroTile)
            Catch ex As Exception
                Dim parent_control As Control = TryCast(sender, Control)
                MyTile = CType(parent_control.Parent, MetroFramework.Controls.MetroTile)
            End Try
            MyTile.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall
            MyTile.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold
            PlayFilePlayer(flowBotons.Controls.GetChildIndex(MyTile), True)
        ElseIf e.KeyCode = Keys.Space Then
            Dim MyTile As MetroFramework.Controls.MetroTile
            Try
                MyTile = CType(sender, MetroFramework.Controls.MetroTile)
            Catch ex As Exception
                Dim parent_control As Control = TryCast(sender, Control)
                MyTile = CType(parent_control.Parent, MetroFramework.Controls.MetroTile)
            End Try
            CType(MyTile.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = Not CType(MyTile.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked
        End If
    End Sub

    Private Sub buttonsTracks_DragEnter(sender As Object, e As DragEventArgs)
        If IsNothing(ButtonsDataObject) Then Exit Sub
        If ButtonsDataObject.GetDataPresent(TypeButtonsDataObject) Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub buttonsTracks_DragDrop(sender As Object, e As DragEventArgs)
        If ButtonsDataObject.GetDataPresent(TypeButtonsDataObject) Then
            Try
                Dim MyButton As MetroFramework.Controls.MetroTile = CType(ButtonsDataObject.GetData(TypeButtonsDataObject), MetroFramework.Controls.MetroTile)
                Dim MyTileOrigen As MetroFramework.Controls.MetroTile = CType(sender, MetroFramework.Controls.MetroTile)
                If MyTileOrigen Is MyButton Then Exit Sub
                Dim ButtonPosition As MetroFramework.Controls.MetroTile = CType(sender, MetroFramework.Controls.MetroTile)
                Dim NewIndex As Integer = flowBotons.Controls.GetChildIndex(ButtonPosition)
                flowBotons.Controls.SetChildIndex(MyButton, NewIndex)


                For I As Integer = 0 To flowBotons.Controls.Count - 1
                    flowBotons.Controls(I).TabIndex = I + 999
                    'MyTile.Controls(ControlsInButton._LabelHandle).Text = ActualPlay.AUDIO_HANDLE
                    If flowBotons.Controls(I).Controls(ControlsInButton._LabelHandle).Text = ActualPlay.AUDIO_HANDLE Then
                        ActualPlay.AUDIO_ListID = I
                    End If
                Next
                If ActualPlay.isActv <> BASSActive.BASS_ACTIVE_PLAYING Then
                    LoadFilePlayer(ActualPlay.AUDIO_ListID)
                End If
                If cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS And (flowBotons.Controls.Count) > (ActualPlay.AUDIO_ListID + 1) Then
                    ' estableix el pròxim                    
                    SetPoxPlay(ActualPlay.AUDIO_ListID + 1)
                End If

            Catch ex As Exception
            End Try
        Else

        End If
        ButtonsDataObject = Nothing
    End Sub

    Private Sub buttonsTracks_MouseDown(sender As Object, e As MouseEventArgs)

        Dim MyTile As MetroFramework.Controls.MetroTile
        Try
            MyTile = CType(sender, MetroFramework.Controls.MetroTile)
        Catch ex As Exception
            Dim parent_control As Control = TryCast(sender, Control)
            MyTile = CType(parent_control.Parent, MetroFramework.Controls.MetroTile)
        End Try
        Try
            If e.Button = MouseButtons.Left And e.Clicks = 2 Then
                'Dim Tipus As Tipus_Play = CType(MyTile.Controls(ControlsInButton._LabelTipus).Text, Tipus_Play)
                'If Tipus <> Tipus_Play.CTL_META_MARK AndAlso Tipus <> Tipus_Play.CTL_META_STOP Then
                MyTile.TileTextFontSize = MetroFramework.MetroTileTextSize.Tall
                MyTile.TileTextFontWeight = MetroFramework.MetroTileTextWeight.Bold
                PlayFilePlayer(flowBotons.Controls.GetChildIndex(MyTile), True)
                Exit Sub
                'End If
            ElseIf e.Button = MouseButtons.Right And e.Clicks = 1 Then
                Dim LocalMousePosition As Point = MyTile.PointToClient(System.Windows.Forms.Cursor.Position)
                ContextMenuStripBotoDret.Show(MyTile, LocalMousePosition)
                Exit Sub
            End If

            CType(MyTile.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = True
            ButtonsDataObject = New DataObject
            ButtonsDataObject.SetData(TypeButtonsDataObject, MyTile)
            MyTile.DoDragDrop(ButtonsDataObject, DragDropEffects.Move)
        Catch ex As Exception
        End Try

    End Sub
    Dim Magin As Integer = 25
    Private Sub flowBotons_Resize(sender As Object, e As EventArgs) Handles flowBotons.Resize

        Dim MyControl As Control
        For Each MyControl In flowBotons.Controls
            MyControl.Width = flowBotons.Width - Magin
            If Me.cmbTypeShow.SelectedIndex = TypeShow.SHOW_BUTTONS Then MyControl.Width = TrackBarBigButtons.Value * 3.7
            MyControl.Refresh()
        Next MyControl
        flowBotons.Refresh()
        Me.cmdDragDrop.Visible = flowBotons.VerticalScroll.Visible
    End Sub

    Private Sub FlowBotons_DragDrop(sender As Object, e As DragEventArgs) Handles flowBotons.DragDrop, cmdDragDrop.DragDrop
        Dim FirstToAdd As Integer = -1
        If MoveInterPlayers = True AndAlso IsNothing(ButtonsDataObject) = False AndAlso ButtonsDataObject.GetDataPresent(TypeButtonsDataObject) Then
            'from another player
            Dim MyButton As MetroFramework.Controls.MetroTile = CType(ButtonsDataObject.GetData(TypeButtonsDataObject), MetroFramework.Controls.MetroTile)
            Dim MyList As FlowLayoutPanel = CType(MyButton.Parent, FlowLayoutPanel)
            'Si és del pròpi llistat surt
            If MyList Is Me.flowBotons Then Exit Sub
            FirstToAdd = Me.flowBotons.Controls.Count
            For Each chilButton As MetroFramework.Controls.MetroTile In MyList.Controls
                Dim chk As MetroFramework.Controls.MetroCheckBox = CType(chilButton.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox)
                If chk.Checked = True Then
                    addElementlist(chilButton.Controls(ControlsInButton._LabelTipus).Text, chilButton.Controls(ControlsInButton._LabelTitol).Text, chilButton.Controls(ControlsInButton._LabelSubTitol).Text,
                                chilButton.Controls(ControlsInButton._LabelPath).Text, chilButton.Controls(ControlsInButton._LabelID).Text, CDate(chilButton.Controls(ControlsInButton._LabelDuration).Tag))
                End If
            Next

            Dim CountList As Integer = MyList.Controls.Count - 1
            Do Until CountList = -1
                Try
                    Dim chilButton As MetroFramework.Controls.MetroTile = CType(MyList.Controls(CountList), MetroFramework.Controls.MetroTile)
                    Dim chk As MetroFramework.Controls.MetroCheckBox = CType(chilButton.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox)
                    If chk.Checked = True Then
                        Dim MyCart As frmCartutxera = CType(chilButton.FindForm, frmCartutxera)
                        MyCart.DeleteButtton(chilButton)
                    End If
                Catch ex As Exception
                End Try
                CountList -= 1
            Loop
            MoveInterPlayers = False
        ElseIf e.Data.GetDataPresent(DataFormats.FileDrop) Then
            'afegim fitxers exteriors
            FirstToAdd = Me.flowBotons.Controls.Count
            Dim filenames As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())
            For Each file As String In filenames
                LoadToList(file)
            Next
        ElseIf IsNothing(listAudioFromDBS) = False Then
            FirstToAdd = addElementlist(listAudioFromDBS)
            listAudioFromDBS = Nothing
        End If
        EnableButtons(FirstToAdd)
    End Sub

    Private Sub EnableButtons(NewId As Integer)
        If NewId <> -1 Then
            Me.cmdBorrar.Enabled = True
            Me.cmdSalvar.Enabled = True
            Me.mnuDel.Enabled = True
            Me.mnuSaveFile.Enabled = True
            mnuAutoSincroMare.Enabled = True
            Me.cmdSaveLoop.Enabled = False

            If mnuLoadAndPlay.Checked Then
                PlayFilePlayer(NewId, True)
            ElseIf ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_STOPPED AndAlso flowBotons.Controls.Count = 1 Then
                LoadFilePlayer(0)
            ElseIf Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS And NewId - 1 = ActualPlay.AUDIO_ListID Then
                SetPoxPlay(NewId)
            End If
        End If
        Me.cmdDragDrop.Visible = flowBotons.VerticalScroll.Visible
        CalcTotalDurationList()
    End Sub

    Private Sub FlowBotons_DragEnter(sender As Object, e As DragEventArgs) Handles flowBotons.DragEnter, cmdDragDrop.DragEnter

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files() As String = e.Data.GetData(DataFormats.FileDrop, True)
            If UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "MP3" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "MP2" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "MP1" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "WAV" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "OGG" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 4)) = "AIFF" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "AIF" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "CDA" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "PTC" _
                Or UCase(Microsoft.VisualBasic.Right(files(0), 3)) = "M3U" Then
                e.Effect = DragDropEffects.Copy
                DragLVDisp = Nothing
                Me.cmdPlayPause.AllowDrop = True
            Else
                'és un fitxer no reconegut per el programa    
                e.Effect = DragDropEffects.None
            End If
        ElseIf MoveInterPlayers = True AndAlso Not IsNothing(ButtonsDataObject) AndAlso ButtonsDataObject.GetDataPresent(TypeButtonsDataObject) Then
            'intern, Botons d'una altre instància            
            e.Effect = DragDropEffects.Move
            'Static a As Integer = 0
            'a += 1
            'Me.lbDisplayTitol.Text = a.ToString
        ElseIf IsNothing(listAudioFromDBS) = False Then
            'intern dels llistats (DBS)          
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub flowBotons_DragLeave(sender As Object, e As EventArgs) Handles flowBotons.DragLeave, cmdDragDrop.DragLeave
        MoveInterPlayers = True
    End Sub

    Function FindFocusedControl() As Integer
        Dim ctr As Control = flowBotons
        Dim container As ContainerControl = TryCast(ctr, ContainerControl)
        Try
            Return flowBotons.Controls.IndexOf(container.ActiveControl)
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region

#Region "Timers"

    Private Sub tmrVuMeter_Tick(sender As Object, e As EventArgs) Handles tmrVuMeter.Tick
        Dim vol As Integer
        Dim RealVol As Integer
        Dim LVol As Integer
        Dim RVol As Integer
        Dim graphWave As Graphics
        Static VolMax As Integer
        '---------

        vol = Bass.BASS_ChannelGetLevel(ActualPlay.AUDIO_HANDLE)
        If SetFxVumeter = True Then FxVumeter(vol)
        If vol < 0 Then vol = 0
        LVol = Un4seen.Bass.Utils.HighWord(vol)
        RVol = Un4seen.Bass.Utils.LowWord(vol)
        RealVol = (LVol + RVol) / 2 'Volum màxim = 32768		
        If VolMax < RealVol Then
            VolMax = RealVol
        Else
            ' s'ha de trobar la fórmula logarítmica (valor màxim 32769 )
            If VolMax > 32000 Then
                VolMax = VolMax - (10 * Atack)
            ElseIf VolMax > 30000 Then
                VolMax = VolMax - (15 * Atack)
            ElseIf VolMax > 25000 Then
                VolMax = VolMax - (25 * Atack)
            ElseIf VolMax > 20000 Then
                VolMax = VolMax - (35 * Atack)
            ElseIf VolMax > 10000 Then
                VolMax = VolMax - (50 * Atack)
            Else
                VolMax = VolMax - (100 * Atack)
            End If
        End If

        If VolMax + Inc > 32000 Then Inc = Inc - 50
        If ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then Inc = 0
        If (ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED) And SetFxVumeter = False Then LVol = 4 : RVol = 4

        '---------------------------------------------

        Try
            Dim gruix As Integer = picVis.Width / 4
            Dim LimePen As New Pen(Color.Lime, gruix)
            Dim PeakPen As New Pen(Color.Red, gruix + 2)
            Dim thresholdPen As New Pen(Color.Blue, gruix - 2)
            Dim bit As Bitmap = New Bitmap(picVis.Width, picVis.Height)
            Dim graph As Graphics = Graphics.FromImage(bit)
            Dim Y As Integer
            graph.SmoothingMode = SmoothingMode.AntiAlias
            Dim VuWidthR As Integer = ((picVis.Width * 2) / 6) - 2
            Dim VuWidthL As Integer = (picVis.Width * 2) / 3

            ' Pinta Volum dreta i esquerra
            Y = (RVol / 32768) * picVis.Height
            If Y > picVis.Height Then Y = picVis.Height
            graph.DrawLine(LimePen, VuWidthR, picVis.Height, VuWidthR, picVis.Height - Y)

            Y = (LVol / 32768) * picVis.Height
            If Y > picVis.Height Then Y = picVis.Height
            graph.DrawLine(LimePen, VuWidthL, picVis.Height, VuWidthL, picVis.Height - Y)


            If VolMax > 0 Then
                'Pinta peak
                Y = (VolMax / 32768) * picVis.Height
                If Y > picVis.Height Then Y = picVis.Height

                graph.DrawLine(PeakPen, VuWidthR, picVis.Height - Y, VuWidthR, picVis.Height - Y - 2)
                graph.DrawLine(PeakPen, VuWidthL, picVis.Height - Y, VuWidthL, picVis.Height - Y - 2)

                'Pinta Threshold
                Y = (threshold / 32768) * picVis.Height
                If Y > picVis.Height Then Y = picVis.Height
                graph.DrawLine(thresholdPen, VuWidthR, picVis.Height - Y, VuWidthR, picVis.Height - Y - 2)
                graph.DrawLine(thresholdPen, VuWidthL, picVis.Height - Y, VuWidthL, picVis.Height - Y - 2)
                If threshold > VolMax Then
                    'Pinta valors per play
                    Y = 2
                    graph.DrawLine(thresholdPen, VuWidthR, picVis.Height - Y, VuWidthR, picVis.Height - Y - 2)
                    graph.DrawLine(thresholdPen, VuWidthL, picVis.Height - Y, VuWidthL, picVis.Height - Y - 2)
                End If
            End If
            'draw the visual onto the picturebox
            picVis.Image = bit
            If ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED AndAlso Y <= 0 Then Me.tmrVuMeter.Enabled = False

            'Pinta posició	
            If PlayerPre.IsPlaying = False Then
                Dim pos As Long = 0
                Dim len As Long = 0
                Dim bitWave As Bitmap = New Bitmap(picWave.Width, picWave.Height)
                graphWave = Graphics.FromImage(bitWave)

                Dim ColorPosition As Color
                If Me.Theme = MetroFramework.MetroThemeStyle.Dark Then
                    ColorPosition = Color.Red
                Else
                    ColorPosition = Color.Black
                End If
                Dim PositionPen As New Pen(ColorPosition, 2)
                pos = ActualPlay.ElapseTime 'Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
                len = ActualPlay.DurationTime 'Bass.BASS_ChannelGetLength(ActualPlay.AUDIO_HANDLE)            
                Dim bpp As Double = len / CType(Me.picWave.Width, Double) ' bytes per pixel  
                ' position (x) where to draw the line, Integer)
                Dim x As Integer
                Try
                    x = CType(Math.Round(pos / bpp), Double)
                Catch ex As Exception
                    x = 1
                End Try
                graphWave.DrawLine(PositionPen, x, 0, x, Me.picWave.Height - 1)
                picWave.Image = bitWave
            End If
            'End Posició

            LimePen.Dispose()
            PeakPen.Dispose()
            thresholdPen.Dispose()
            graph.Dispose()
            graphWave.Dispose()
            LimePen = Nothing
            PeakPen = Nothing
            thresholdPen = Nothing
            bit = Nothing
            graph = Nothing
            graphWave = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Dim SetFxVumeter As Boolean = False
    Private Sub FxVumeter(ByRef vol)
        Static Ini As Boolean = True
        If Ini = True Then vol = 32768 : Ini = False
        vol -= 1

        If vol <= 0 Then
            Ini = True
            SetFxVumeter = False
        End If
    End Sub


    Private Sub tmrDisplay_Tick(sender As Object, e As EventArgs) Handles tmrDisplay.Tick
        Static ExeDirecte As Boolean = False
        'si està reproduint no podem carregar un altre.
        mnuSetLoad.Enabled = ActualPlay.isActv <> BASSActive.BASS_ACTIVE_PLAYING
        'Pre-escolta
        Static timeOut As Integer = 10
        If PlayerPre.IsPlaying = True Then
            cmdPlayPre.Image = Me.picStop.Image
            timeOut = 10
        ElseIf timeOut > 0 Then
            cmdPlayPre.Image = Me.picPlay.Image
            PlayerPre.PintaPicPreEscolta(picPreEsc, Atack)
            timeOut -= 1
        End If

        Me.lbHora.Text = Microsoft.VisualBasic.TimeOfDay.ToLongTimeString
        Static blFlat As Boolean = False
        If blFlat = True Then
            blFlat = False
        Else
            ManagePlayedHandle()
            blFlat = True
        End If

        If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING OrElse ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
            'Display destellant els ùltims 25 seg.
            If MyMixer IsNot Nothing AndAlso MyMixer.myDeviceDetected = True AndAlso ActualPlay.DurationInSecons - ActualPlay.ElapseInSecons = 25 Then
                'MyMixer.airenceSetLedBlink( USB_ON,colors_t.RED,colors_t.YELLOW,blink_speed_t.FAST)
            End If
            If (ActualPlay.DurationInSecons - ActualPlay.ElapseInSecons) < 25 And ActualPlay.DurationInSecons > 0 And lbDisplayTime.ForeColor = Color.Lime Then
                lbDisplayTime.ForeColor = Color.Red
            Else
                lbDisplayTime.ForeColor = Color.Lime
            End If
            If InitCue Then
                If lbCue.BackColor = Color.Lime Then
                    lbCue.BackColor = Color.Red
                Else
                    lbCue.BackColor = Color.Lime
                End If
            End If
            'Refesca titol procedent del streaming
            Static dateTagRefresh As Date = Now

            If ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_URL_STREAM AndAlso ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING AndAlso dateTagRefresh < Now Then
                dateTagRefresh = dateTagRefresh.AddSeconds(30)
                ChangeTitolStream()
            End If
            'Display Introducció
            If ActualPlay.IntroSegons <= ActualPlay.ElapseInSecons Then
                lbRestaIntro.Text = "00:00"
                lbRestaIntro.ForeColor = Color.Lime
            Else
                Dim SegIntroRest As Double = ActualPlay.IntroSegons - ActualPlay.ElapseInSecons
                lbRestaIntro.Text = Un4seen.Bass.Utils.FixTimespan(SegIntroRest, "MMSS")
                If lbRestaIntro.ForeColor = Color.Red Then
                    lbRestaIntro.ForeColor = Color.Lime
                Else
                    lbRestaIntro.ForeColor = Color.Red
                End If
            End If
            ExeDirecte = (mnuIniDirect.Checked And cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS)
        ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then
            If mnuIniDirect.Checked And cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS And (flowBotons.Controls.Count - 1) = ActualPlay.AUDIO_ListID And ExeDirecte = True Then

                ' Assegura que s'iniciarà el programa MSCDirecte
                ' Executa el programa directe al finalitzar la reproducció
                Try
                    'TODO:Això peta, ja que al enviar la instrucció encara no s'ha engegat el programa ( si ja està engegat va bé)
                    Dim p As System.Diagnostics.Process = New System.Diagnostics.Process()
                    If mnuForceDirectMusic.Checked Then p.StartInfo.Arguments = "PLAY"
                    'If mnuForceDirectMusic.Checked Then p.StartInfo.Arguments = "MUSIC"
                    p.StartInfo.FileName = My.Application.Info.DirectoryPath & "\MSCContinuity.exe"
                    p.Start()
                    'Executa la instrucció a Play.
                    Dim ctlRemot As New MSC.InstruccionsRemotes(getNomAplic(Aplicatius.PRG_DIRECTE))
                    ctlRemot.ExecuteInstruc(ctlRemot.Id_Sessio, ServerInstruc.DIRECT_PLAY)
                Catch ex As Exception
                End Try
                Me.Close()
                Exit Sub
            ElseIf Not Me.mnuIniDirect.Checked Then
                'Poder sortir del programa
                ExeDirecte = False
                Me.mnuExit.Enabled = True
            End If
        ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_STALLED Then
            Me.lbInfoTrack.Text = MSG_ERROR_PLAYER
            'Error al reproduïr
        End If

        'Auto Save List
        Static AutoSaveTime As Date = Now.AddMinutes(1)
        If AutoSaveTime <= Now And flowBotons.Controls.Count > 0 And NumCart = 1 Then
            Dim Path As String = IO.Path.GetTempPath & "tracklist.ptc"
            WriteTracklist(Path)
        End If

        If STANDALONE = True Then Exit Sub
        '---------------------------
        'Buscar instruccions remotes (Només controlar-ho a la primera cartutxera)
        '---------------------------		
        If NumCart = 1 Then
            Static TempRefresh As Date
            Static Flag As Boolean
            If TempRefresh < Now And filInstucServer.IsAlive = False Then
                TempRefresh = Now.AddMilliseconds(Params.TimeRefrestInstruc)
                filInstucServer = New Thread(AddressOf RI.getRemoteInstruccions)
                filInstucServer.IsBackground = True
                filInstucServer.Priority = ThreadPriority.Normal
                filInstucServer.Start()
                Flag = True
            ElseIf filInstucServer.IsAlive = False And Flag = True Then
                Flag = False
                'Un cop s'ha establert a procedim segons els casos
                Select Case CType(RI.ServerInstrucValue, ServerInstruc)
                    Case ServerInstruc.MSC_NO_INSTRUC ' No fer res
                    Case ServerInstruc.MSC_END : CloseAppSilenci = True : Me.Close() ' StopFitxer() : EndAplic()
                    Case ServerInstruc.CARTUT_PLAY
                        If flowBotons.Controls.Count > 0 Then
                            If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PAUSED Then
                                PlayFilePlayer()
                                'Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False)
                            ElseIf ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_STOPPED Then

                                PlayFilePlayer(FindFocusedButton())
                            End If
                        End If
                    Case ServerInstruc.CARTUT_PAUSA

                        If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Then PauseFilePlayer()
                    Case ServerInstruc.CARTUT_STOP : StopFilePlayer()
                    Case ServerInstruc.CARTUT_END_INI_DIRECT : mnuIniDirect.Checked = True : IniDirecte()
                End Select
            End If
        End If
        'OldActive = ActualPlay.isActv


    End Sub

    Private Sub tmrRellotge_Tick(sender As Object, e As EventArgs) Handles tmrRellotge.Tick

        'Static OldActive As Un4seen.Bass.BASSActive
        If ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_META_MARK Then
            ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED
        Else
            ActualPlay.isActv = Bass.BASS_ChannelIsActive(ActualPlay.AUDIO_HANDLE)
        End If

        ActualPlay.ElapseTime = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
        ActualPlay.RemainTime = ActualPlay.DurationTime - ActualPlay.ElapseTime
        ActualPlay.ElapseInSecons = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.ElapseTime)
        ActualPlay.RemainInSecons = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.RemainTime)

        If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
            Select Case DisplayTemps
                Case mdlMscDefines.DisplayTime.DISPLAY_ELAPSE
                    lbDisplayTime.Text = Un4seen.Bass.Utils.FixTimespan(ActualPlay.ElapseInSecons, "HHMMSSFF")
                Case mdlMscDefines.DisplayTime.DISPLAY_REMAIN
                    lbDisplayTime.Text = "-" & Un4seen.Bass.Utils.FixTimespan(ActualPlay.RemainInSecons, "HHMMSSFF")
                Case mdlMscDefines.DisplayTime.DISPLAY_TOTAL
                    Dim SegTotal As Single
                    If cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS Then
                        SegTotal = TotalDuration - ActualPlay.ElapseInSecons
                    Else
                        SegTotal = ActualPlay.RemainInSecons
                    End If
                    lbDisplayTime.Text = "-" & Un4seen.Bass.Utils.FixTimespan(SegTotal, "HHMMSSFF")
            End Select
        ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then
            Select Case DisplayTemps
                Case mdlMscDefines.DisplayTime.DISPLAY_ELAPSE Or mdlMscDefines.DisplayTime.DISPLAY_REMAIN
                    lbDisplayTime.Text = "0:00:00.00"
                Case mdlMscDefines.DisplayTime.DISPLAY_TOTAL
                    If Me.flowBotons.Controls.Count Then
                        lbDisplayTime.Text = "-" & Un4seen.Bass.Utils.FixTimespan(TotalDuration, "HHMMSSFF")
                    Else
                        lbDisplayTime.Text = "-0:00:00.00"
                    End If
            End Select
        End If
        If bl_AutoSinc Then
            Static DisplayCount As Short
            If DisplayCount = 4 Then
                DisplayCount = 0
                If lbAutoSinc.BackColor = Color.Lime Then
                    lbAutoSinc.BackColor = Color.Red
                Else
                    lbAutoSinc.BackColor = Color.Lime
                End If
            Else
                DisplayCount = DisplayCount + 1
            End If
            If TimeToStart = TimeOfDay Then
                bl_AutoSinc = False
                PlayFilePlayer(0)
                lbAutoSinc.BackColor = Color.Lime
                mnuAutoSincro59.Checked = False
                mnuAutoSincro29.Checked = False
                mnuAutoSincroXX.Checked = False
                mnuAutoSincro00.Checked = False
                mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00"
                lbInfo.Visible = False
            End If
        End If
        Me.lbCPU.Text = "CPU: " & String.Format("{0:0.00}%", Bass.BASS_GetCPU())
        If PlayerPre.IsPlaying = True Then
            Dim bitWave As Bitmap = New Bitmap(picWave.Width, picWave.Height)
            Dim graphWave As Graphics = Graphics.FromImage(bitWave)
            Dim ColorPosition As Color = Color.Blue
            Dim PositionPen As New Pen(ColorPosition, 2)
            Dim pos As Long = Bass.BASS_ChannelGetPosition(PlayerPre.PreEscoltaHandle)
            Dim len As Long = Bass.BASS_ChannelGetLength(PlayerPre.PreEscoltaHandle)
            Dim bpp As Double = len / CType(Me.picWave.Width, Double) ' bytes per pixel  
            Dim x As Integer
            Try
                x = CType(Math.Round(pos / bpp), Double)
            Catch ex As Exception
                x = 1
            End Try
            graphWave.DrawLine(PositionPen, x, 0, x, picWave.Height - 1)
            picWave.Image = bitWave
            PlayerPre.PintaPicPreEscolta(picPreEsc, Atack)
        End If
    End Sub

    Private Sub tmr_Play_Tick(sender As Object, e As EventArgs) Handles tmr_Play.Tick
        If ActualPlay.Load = False Then Exit Sub
        Select Case ActualPlay.isActv
            Case Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING
                If ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_URL_STREAM And ActualPlay.ElapseInSecons >= ActualPlay.OutSegons Then
                    'fade out
                    Bass.BASS_ChannelSlideAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_VOL, 0, TimeFadeOut)
                End If
                Select Case Me.cmbTypePlayer.SelectedIndex
                    Case TypePlay.PLAY_CUNTINUOS
                        If IsNothing(ProxPlay.AUDIO_Path) Then Exit Sub
                        If mnuAutoFader.Checked = False Or ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_PUBLICITAT Or ProxPlay.AUDIO_TipFitxer = Tipus_Play.CTL_PUBLICITAT Then
                            If ActualPlay.ElapseTime >= ActualPlay.DurationTime - 123000 Then PlayFilePlayer(ProxPlay.AUDIO_ListID)
                        Else
                            If ActualPlay.ElapseInSecons >= ActualPlay.OutSegons Then PlayFilePlayer(ProxPlay.AUDIO_ListID)
                        End If
                    Case TypePlay.PLAY_LOOP
                        If (ActualPlay.ElapseTime >= ActualPlay.LoopOut) Then Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, ActualPlay.LoopIn)
                End Select
            Case BASSActive.BASS_ACTIVE_STOPPED
                'reload the track
                If cmbTypePlayer.SelectedIndex = TypePlay.PLAY_STEP Then
                    Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, ActualPlay.CuePosition)
                ElseIf cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS And ActualPlay.AUDIO_ListID = (flowBotons.Controls.Count - 1) Then
                    If mnuIniDirect.Checked = False Then
                        If ActualPlay.AUDIO_ListID = 0 Then
                            Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, ActualPlay.CuePosition)
                        Else
                            LoadFilePlayer(0)
                        End If
                    End If
                End If
                PlayAfterPause = False
                tmr_Play.Enabled = False
            Case BASSActive.BASS_ACTIVE_PAUSED
            Case BASSActive.BASS_ACTIVE_STALLED
        End Select
    End Sub

#End Region

#Region "General form"

    Dim MSG_START_APP As String
    Dim MSG_END_APP As String
    'Dim LABEL_REFRESH As String 
    Dim MSG_ERROR_WAVE_TOO_LONG As String
    Dim MSG_ERROR_LOAD_URL As String
    Dim LB_DURADA As String
    Dim LB_HORARADI As String
    Dim MSG_SURE_DELETE_PLAYING As String
    Dim LB_LIST As String
    Dim MSG_SYNC_CLOUD As String

    Public Sub setLanguageForm()

        lang.StrForm = Me.Name
        ' Missatges
        'LABEL_REFRESH = "Refrescar" 'de moment no es fa servir
        buttonDonate.Text = lang.getText("LABEL_DONATE", True)
        LB_LIST = lang.getText("LB_LIST") ' Llistat 
        MSG_SURE_DELETE_PLAYING = lang.getText("MSG_SURE_DELETE_PLAYING") ' "SEGUR QUE VOLS PARAR LA ACTUAL REPRODUCCIÓ?"
        MSG_START_APP = lang.getText("MSG_START_APP", True) '"Inici execució"
        MSG_END_APP = lang.getText("MSG_END_APP", True) '"Final execució"
        MSG_SYNC_CLOUD = lang.getText("LABEL_SYNC_CLOUD", True) '"Sincronitzat amb el núvol"

        MSG_STOPPING_ALL_CART = lang.getText("MSG_STOPPING_ALL_CART") '"Atenció es pararan totes les cartutxeres, vols continuar?"
        MSG_ATENCIO = lang.getText("MSG_ATENCIO", True)
        MSG_NO_STOP_APP = lang.getText("MSG_NO_STOP_APP", True) '"Aquest programa no es pot parar d'aquesta manera"
        MSG_ERROR_PLAYER = lang.getText("MSG_ERROR_PLAYER") '"Error del reproductor"
        MSG_ERROR_POSITION = lang.getText("MSG_ERROR_POSITION") '"Error de posicionament"
        MSG_ERROR_WAVE_TOO_LONG = lang.getText("MSG_ERROR_WAVE_TOO_LONG", True) '"Fitxer massa llarg, no es carregarà l'ona"

        LABEL_SERVER = lang.getText("LABEL_SERVER", True) '"Servidor"
        LABEL_HORA_SINCRO = lang.getText("LABEL_HORA_SINCRO") '"Hora Sincro."
        LABEL_SENYALS_HORARIS = lang.getText("LABEL_SENYALS_HORARIS", True) '"Senyals Horaris"
        LABEL_H_INI = lang.getText("LABEL_H_INI") '"H. Ini"
        LABEL_H_END = lang.getText("LABEL_H_END") '"H. End"
        MSG_ERROR_DURADA_SINCRO = lang.getText("MSG_ERROR_DURADA_SINCRO") '"La hora final no és correcte segons la durada total ({0})."
        COMMONDIALOG1_TITLE = lang.getText("COMMONDIALOG1_TITLE") '"Carregar fitxer ..."
        LIST_FILTER_FILES = lang.getText("LABEL_FITXERS", True) '"Fitxers"
        LIST_FILTER_CART = lang.getText("HEADERTEXT_LLISTAT", True) '"Llistats Cartutxeres"
        LIST_FILTER_WINAMP = lang.getText("LIST_FILTER_WINAMP") '"Llistats Winamp"
        SAVE_FILE_TITLE = lang.getText("SAVE_FILE_TITLE") '"Salvar llistat"
        SAVE_FILE_FILTER = lang.getText("SAVE_FILE_FILTER") '"Tots els arxius"
        SAVE_FILE_NAME = lang.getText("SAVE_FILE_NAME") '"Nou llistat"
        MSG_OVERWRITE_LIST = lang.getText("MSG_OVERWRITE_LIST") '"La llista {0} ja està creada, vols sobrescriure-la?"
        MSG_ERROR_BASS_NO_INI = lang.getText("MSG_ERROR_BASS_NO_INI", True) '"No es pot iniciar el sistema d'audio."
        MES_INFO = lang.getText("MSG_MORE_INFO", True)
        OPENFILEDIALOG1_TITLE = lang.getText("LABEL_BUSCAR_PROG", True) '"Buscar Programa ..."
        'Comunes
        Me.mnuExit.Text = lang.getText("LABEL_EXIT", True) '"Sortir"
        Me.mnuArxiu.Text = lang.getText("LABEL_ARXIU", True) '"Arxiu"        
        Me.mnuSaveFile.Text = lang.getText("LABEL_SAVE", True) '"Salvar"
        Me.cmdSaveLoop.Text = lang.getText("LABEL_SAVE", True) '"Salvar"
        Me.cmdLoadLoop.Text = lang.getText("LABEL_LOAD", True) '"Carregar"		

        Me.mnuLoad.Text = lang.getText("LABEL_LOAD", True) '"Carregar"
        Me.mnuDel.Text = lang.getText("LABEL_DELETE", True) '"Borrar"
        Me.mnuStop.Text = lang.getText("LABEL_STOP", True) '"Stop"
        Me.ToolStripTextBox1.Text = lang.getText("LABEL_PLAY", True) '"Play"

        Me.mnuSpeed.Text = lang.getText("mnuSpeed.Text") ' Velocitat
        Me.mnuSpeedSlow.Text = lang.getText("mnuSpeedSlow.Text") ' Lent
        Me.mnuSpeedNormal.Text = lang.getText("mnuSpeedNormal.Text") ' Normal
        Me.mnuSpeedFast.Text = lang.getText("mnuSpeedFast.Text") ' Ràpid

        Me.mnuCalcBPMOnLoad.Text = lang.getText("mnuCalcBPMOnLoad.Text") '"Calcular BPM al carregar"
        Me.mnuCalBPMList.Text = lang.getText("mnuCalBPMList.Text") '"Calcular BPMs del llistat"		
        Me.mnuLang.Text = lang.getText("LABEL_IDIOMA_INTERFICIE", True) ' "Idioma Interfície"		

        LABEL_PLAY_SIMPLE = lang.getText("LABEL_PLAY_SIMPLE")
        LABEL_PLAY_CONTINU = lang.getText("LABEL_PLAY_CONTINU")
        LABEL_PLAY_LOOP = lang.getText("LABEL_PLAY_LOOP")

        LABEL_SHOW_BUTTONS = lang.getText("LABEL_SHOW_BUTTONS")
        LABEL_SHOW_LIST = lang.getText("LABEL_SHOW_LIST")

        Me.AutomàticaToolStripMenuItem.Text = lang.getText("LABEL_PLAY_CONTINU")  '"Continu"

        Me.cmdLoopOut.Text = lang.getText("cmdLoopOut.Text") '"loop Out"
        Me.cmdLoopIn.Text = lang.getText("cmdLoopIn.Text") '"Loop In"		
        Me.Label3.Text = lang.getText("HEADERTEXT_HORA", True) '"Hora"
        Me.Label1.Text = lang.getText("LABEL_INTRO", True) '"Intro"
        'Me.lbFormatTime.Text = "Remain Time"
        'Me.mnuDef.Text = "MenuStrip1"
        Me.ProgramaToolStripMenuItem.Text = lang.getText("LABEL_PROGRAMA", True) '"Programa"
        Me.mnuNewCartut.Text = lang.getText("mnuNewCartut.Text") '"Nova Cartutxera"
        Me.mnuReOrdCart.Text = lang.getText("mnuReOrdCart.Text") '"Re-ordena l'emplaçament"
        Me.mnuReOrdCart.ToolTipText = lang.getText("mnuReOrdCart.ToolTipText") '"Estableix tamany i enplaçament per defecte"
        Me.mnuExplorerPC.Text = lang.getText("mnuExplorerPC.Text") '"Explorador del PC"
        Me.mnuExplorerDBS.Text = lang.getText("mnuExplorerDBS.Text") '"Explorador de la DBS"
        Me.mnuMoveDBS.Text = lang.getText("mnuMoveDBS.Text") '"Emplaçament per defecte expl. DBS"


        Me.mnuAutoDel.Text = lang.getText("mnuAutoDel.Text") '"Auto eliminar fitxer"
        Me.mnuReproduccio.Text = lang.getText("mnuReproduccio.Text") '"Reproducció"
        Me.ToolStripPlayer.Text = mnuReproduccio.Text & ":" '"Reproducció"
        Me.ReproduccióToolStripMenuItem.Text = mnuReproduccio.Text '"Reproducció"
        Me.mnuPlayPause.Text = lang.getText("LABEL_PLAY_PAUSA", True) '"Play/Pausa"

        ToolStripLabel1.Text = lang.getText("ToolStripLabel1.Text") '"Mostra"
        'Me.LoopToolStripMenuItem.Text = "Loop"
        Me.mnuAutoFader.Text = lang.getText("mnuAutoFader.Text") '"Auto Mescla"
        Me.mnuBucleList.Text = lang.getText("mnuBucleList.Text") '"Bucle del llistat"
        Me.AutomàticToolStripMenuItem.Text = lang.getText("AutomàticToolStripMenuItem.Text") '"Automàtic"
        Me.mnuIniDirect.Text = lang.getText("mnuIniDirect.Text") '"Iniciar MSC Directe al final de la reproducció"
        Me.mnuForceDirectMusic.Text = lang.getText("mnuForceDirectMusic.Text") '"Forçar MSC Directe a  Música"
        Me.mnuAutoSincroMare.Text = lang.getText("mnuAutoSincroMare.Text") '"Sincronitzar"
        Me.mnuAutoSincroXX.Text = lang.getText("FITXER_ALTRES", True) & "..." '"Altres ..."		
        Me.mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00" '"Hora Sincro.: 00:00:00"
        Me.mnuSH.Text = LABEL_SENYALS_HORARIS

        Me.EinesToolStripMenuItem.Text = lang.getText("LABEL_EINES", True) '"Eines"
        Me.mnuLoadVar.Text = lang.getText("mnuReload.Text", True) '"Actualitzar variables"
        Me.mnuDevice.Text = lang.getText("mnuDevice.Text") '"Dispositius de so (Play)"
        Me.mnuDevicePreEscolta.Text = lang.getText("mnuDevicePreEscolta.Text") '"Dispositius de so (Pre-escolta)"
        Me.mnuSavedevicePlay.Text = lang.getText("mnuSavedevicePlay.Text")
        Me.mnuSavedevicePlay.ToolTipText = lang.getText("mnuSavedevicePlay.ToolTipText") '"Salva la assignació actual del dispositiu de reproducció"
        Me.mnuDelDevicePlay.Text = lang.getText("mnuDelDevicePlay.Text") '"Eliminar les assignacions personalitzades"
        '--------------
        Me.mnuRefMare.Text = lang.getText("mnu_Help", True) '"Ajuda"
        Me.mnuHelpManual.Text = lang.getText("mnuRefHelp.Text", True) '"Ajuda" "Manual OnLine"
        Me.mnuHelpWeb.Text = lang.getText("mnuRefWeb.Text", True) '"Web""MSC a Internet"
        Me.mnuHelpAbout.Text = lang.getText("mnuRefMSC.Text", True) '"Sobre MSC"

        Me.mnuProgEditAudio.Text = lang.getText("mnuProgEditAudio.Text", True) '"Escollir programa d'edició d'àudio"
        Me.mnuEditAudio.Text = lang.getText("mnuEditAudio.Text", True) '"Editar l'àudio"

        'Me.ColumnHeader21.Text = lang.getText("HEADERTEXT_TITOL", True) '"Títol"
        'Me.ColumnHeader22.Text = lang.getText("LABEL_INTERP_CLIENT", True) '"Intèrpret/Client"
        'mnuColumnInterp.Text = Me.ColumnHeader22.Text
        'Me.ColumnHeader17.Text = ""
        LB_DURADA = lang.getText("HEADERTEXT_DURADA", True) '"Durada"
        'mnuColumnDurada.Text = Me.ColumnHeader23.Text
        LB_HORARADI = lang.getText("LABEL_HORA_RADI", True) '"Hora radi."
        'mnuColumnHora.Text = Me.ColumnHeader24.Text
        'Me.columnHeader1.Text = lang.getText("HEADERTEXT_RITME", True) '"Ritme"
        'mnuColumnVRitme.Text = Me.columnHeader1.Text

        Me.mnuMesInfo.Text = lang.getText("MSG_MORE_INFO", True) '"Més Info ..."
        Me.mnuPreEscoltaStop.Text = LABEL_PREESCOLTA_STOP '"Pre-escolta STOP"

        mnuSetLoad.Text = lang.getText("LABEL_LOAD", True) '"Carregar"
        Me.mnuSetPoxPlay.Text = lang.getText("mnuSetPoxPlay.Text") '"Pròxim a  Play"

        Me.mnuSelectUnselect.Text = lang.getText("mnuDesmarcar.Text") '"Descarcar"
        mnuContextDel.Text = lang.getText("LABEL_DELETE", True)
        Me.mnuContextDel.ToolTipText = lang.getText("mnuContextDel.Text") '"Borrar de la llista de reprodució"

        'Me.ToolStrip1.Text = "ToolStrip1"
        Me.mnuPreEscolta.Text = lang.getText("mnuPreEscolta.Text") '"Pre-escolta"
        Me.cmdPlayPre.Text = mnuPreEscolta.Text '"Pre-escolta"
        Me.ToolStripLabel3.Text = Me.cmdPlayPre.Text & ":" '"Pre-escolta" & ":      "

        'Me.lbTamanyBotons.Text = lang.getText("lbTamanyBotons.Text") '"Mida"
        'Me.cmdMesGranBotons.ToolTipText = lang.getText("cmdMesGranBotons.ToolTipText") '"Mida dels botons"		
        'Me.lbTamanyBotons.ToolTipText = Me.cmdMesGranBotons.ToolTipText ' "Mida dels botons"
        'Me.cmdMenyGranBotons.ToolTipText = Me.cmdMesGranBotons.ToolTipText '"Mida dels botons"

        'Me.lbInfo.Text = "Sincro: 12:35:26"
        Me.mnuHistoryPlay.Text = lang.getText("mnuHistoryPlay.Text") '"Historial"
        'Me.MetroToolTipTrackMoreInfo.SetToolTip(Me.picIn, lang.getText("LABEL_PUNT_INTRO")) '"Punt Intro"
        'Me.MetroToolTipTrackMoreInfo.SetToolTip(Me.picOut, lang.getText("LABEL_PUNT_OUT"))'"Punt Out"
        Me.MetroToolTipTrackMoreInfo.SetToolTip(Me.picWave, lang.getText("LABEL_AVANT-RETRO")) '"avançar/retrocedir"    	
        Me.mnuWaveSetIntro.Text = lang.getText("mnuWaveSetIntro.Text") '"Establir aquí punt intro"
        Me.mnuWaveSetPointMix.Text = lang.getText("mnuWaveSetPointMix.Text") '"Establir aquí punt mescla"
        Me.mnuWavePlay.Text = lang.getText("mnuWavePlay.Text") '"Play aquí"
        mnuChangeDuration.Text = lang.getText("mnuChangeDuration.Text") 'canvia la durada
        mnuControlRemot.Text = lang.getText("LABEL_CONTROL_REMOT", True) '"Control remot"
        mnuPanicButton.Text = lang.getText("LABEL_PANIC", True) '"Botó Pànic"
        mnuInsertStop.Text = lang.getText("mnuInsertStop.Text") '"Inserta Punt Stop"        
        Me.mnuPlayURL.Text = lang.getText("mnuPlayURL.Text") '"Reproduir URL"
        MSG_ERROR_LOAD_URL = lang.getText("MSG_ERROR_LOAD_URL")

        MSG_MARKS_PRG = lang.getText("MSG_MARKS_PRG") '"Creació de marques pel programa: {0} (sessió: {1})"
        MSG_MARKS_NO_SESSION = lang.getText("MSG_MARKS_NO_SESSION") '"Actualment no hi ha cap sessió enregistrant-se"
        LB_SESSION = lang.getText("LB_SESSION") '"Load session"
        LB_MARK = lang.getText("LB_MARK") '"Mark"
        Me.mnuSession.Text = LB_SESSION
        mnuInsertMark.Text = lang.getText("mnuInsertMark.Text") '"Insert mark"
        mnuMoveUp.Text = lang.getText("mnuMoveUp.Text")
        mnuMoveDown.Text = lang.getText("mnuMoveDown.Text")
        mnuCheckAll.Text = lang.getText("checkAll_Nothing", True)
        cmdOnAir.ToolTipText = lang.getText("LABEL_ON_AIR", True)

        Me.Text = lang.getText("Text") '"MCS Cartutxera" o My.Application.Info.Title

        cmbTypePlayer.Items.Clear()
        cmbTypePlayer.Items.Add(LABEL_PLAY_SIMPLE)
        cmbTypePlayer.Items.Add(LABEL_PLAY_CONTINU)
        cmbTypePlayer.Items.Add(LABEL_PLAY_LOOP)

        cmbTypeShow.Items.Clear()
        cmbTypeShow.Items.Add(LABEL_SHOW_LIST)
        cmbTypeShow.Items.Add(LABEL_SHOW_BUTTONS)

        cmbTypePlayer.SelectedIndex = TypePlay.PLAY_STEP
        cmbTypeShow.SelectedIndex = TypeShow.SHOW_LIST

    End Sub

    Dim MSG_MARKS_PRG As String
    Dim MSG_MARKS_NO_SESSION As String
    Dim LB_SESSION As String
    Dim LB_MARK As String

    Private Sub ListLanguage()
        Dim tbLangs As DataTable = lang.ListLangInterface()
        For i As Integer = 0 To tbLangs.Rows.Count - 1
            Dim mnuListLang As ToolStripMenuItem
            mnuListLang = New ToolStripMenuItem
            mnuListLang.Text = tbLangs.Rows(i)("LangName").ToString
            mnuListLang.Tag = tbLangs.Rows(i)("langCode").ToString
            mnuListLang.Checked = (mnuListLang.Tag = lang.Code)

            Me.mnuLang.DropDownItems.Add(mnuListLang)

            AddHandler mnuListLang.Click, AddressOf AssigLanguage
        Next i
    End Sub

    Private Sub SetDisplaiFonts()
        lbDisplayTime.Font = CustomFont.GetInstance(16, FontStyle.Bold)
        lbRestaIntro.Font = CustomFont.GetInstance(16, FontStyle.Bold)
        lbBPM.Font = CustomFont.GetInstance(16, FontStyle.Bold)
        Me.lbHora.Font = CustomFont.GetInstance(12, FontStyle.Bold)
        lbGainDB.Font = CustomFont.GetInstance(12, FontStyle.Bold)

    End Sub

    Private Sub AssigLanguage(ByVal sender As System.Object, ByVal e As System.EventArgs)

        For i As Integer = 0 To mnuLang.DropDownItems.Count - 1
            CType(mnuLang.DropDownItems(i), ToolStripMenuItem).Checked = False
        Next

        CType(sender, ToolStripMenuItem).Checked = True

        lang.Code = CType(sender, ToolStripMenuItem).Tag
        setLanguageGlobal()
        Dim myForms As FormCollection = Application.OpenForms
        For Each frmName As Form In myForms
            If MethodExist(frmName, "setLanguageForm") = True Then
                'ok
            End If
        Next

        My.Settings.Lang = lang.Code
        Try
            My.Settings.Save()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EndAplic()
        Try
            frmAudioDBS.mnuAddRepreoduccio.DropDownItems.RemoveAt(NumCart - 1)
        Catch ex As Exception

        End Try

        If WindowState <> System.Windows.Forms.FormWindowState.Minimized Then
            Dim FitxerINI As New IniFile
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Strings.Left(StrNumCart, 6) & "_L", CStr(Me.Left))
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Strings.Left(StrNumCart, 6) & "_T", CStr(Me.Top))
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Strings.Left(StrNumCart, 6) & "_W", CStr(Me.Width))
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Strings.Left(StrNumCart, 6) & "_H", CStr(Me.Height))
            FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "SD_" & NumCart.ToString, CStr(splitContainer1.SplitterDistance))

            If STANDALONE = False Then FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "ONAIR", IIf(ON_AIR, 1, 0))

        End If
        My.Settings.AutoFader = Me.mnuAutoFader.Checked
        'My.Settings.Lang = lang.lang 
        Try
            My.Settings.Save()
        Catch ex As Exception

        End Try


        Me.tmrDisplay.Enabled = False
        Me.tmrRellotge.Enabled = False
        Me.tmrVuMeter.Enabled = False
        Dim Path As String = IO.Path.GetTempPath & "tracklist.ptc"

        If IO.File.Exists(Path) = True Then IO.File.Delete(Path)

        Bass.BASS_ChannelStop(ActualPlay.AUDIO_HANDLE)
        Bass.BASS_StreamFree(ActualPlay.AUDIO_HANDLE)
        Dim Descrip As String = MSG_END_APP & " " & My.Application.Info.Title & " (" & StrNumCart & ") V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & " PID:" & MyAPP.IDSesion_Client
        If STANDALONE = False Then addCtlRadi(0, CShort(Tipus_Play.CTL_SISTEMA), 0, "00:00:00", 0, 0, Usuari.UsrID, Descrip, 0, ON_AIR)
        'If AirenceConnected = True Then airenceClose()

    End Sub

    Private Sub LoadParams()
        DisplayTemps = DisplayTime.DISPLAY_REMAIN

        Dim SenyalHora As String = Params.PathSenyalHora
        Dim LocalHandLe As Integer = Bass.BASS_StreamCreateFile(SenyalHora, 0, 0, 0)
        If LocalHandLe <> 0 Then
            Dim txtHora As Date
            Dim Lens As Long = Bass.BASS_ChannelGetLength(LocalHandLe) 'Stream Length
            SegSH = Bass.BASS_ChannelBytes2Seconds(LocalHandLe, Lens)
            txtHora = TimeSerial(0, 59, 60 - SegSH)
            mnuAutoSincro59.Text = " HH:" & txtHora.ToString("mm:ss")
            txtHora = TimeSerial(0, 29, 60 - SegSH)
            mnuAutoSincro29.Text = " HH:" & txtHora.ToString("mm:ss")
        End If

        'Carrèga els valors del autofader -----------------------------------
        threshold = Params.Threshold
        Atack = Params.Attack
        VolNormalice = Params.VolNormalize
        VolIni = Params.VolIni
        SegActivate = Params.SegActivate

    End Sub

    Private Sub frmCartutxera_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.flowBotons.Controls.Clear()
        SetDisplaiFonts()

        RI = New MSC.InstruccionsRemotes(MyAPP.IDSesion_Client)
        filInstucServer = New Thread(AddressOf RI.getRemoteInstruccions)

        LoadParams()
        ListDevice()
        setLanguageForm()

        picCover.MaximumSize = New Size(panel3.Height, panel3.Height)
        Dim FitxerINI As New IniFile
        Me.Left = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Left(StrNumCart, 6) & "_L", CStr(10)))
        Me.Top = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Left(StrNumCart, 6) & "_T", CStr(10)))
        Me.Width = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Left(StrNumCart, 6) & "_W", CStr(405)))
        Me.Height = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", Microsoft.VisualBasic.Left(StrNumCart, 6) & "_H", CStr(600)))

        splitContainer1.SplitterDistance = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "SD_" & NumCart.ToString, CStr(270)))


        Dim Descrip As String = MSG_START_APP & " " & My.Application.Info.Title & " (" & StrNumCart & ") V" & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Build & " PID:" & MyAPP.IDSesion_Client
        If STANDALONE = False Then addCtlRadi(0, CShort(Tipus_Play.CTL_SISTEMA), 0, "00:00:00", 0, 0, Usuari.UsrID, Descrip, 0, ON_AIR)
        If MyAPP.CtlDebug = True Then MyAPP.Error_MSC.SalvarRegistreDepuracio(Descrip)

        Me.mnuAutoFader.Checked = My.Settings.AutoFader

        If STANDALONE = False Then mnuExplorerDBS.Checked = CBool(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "FormDBS", 0))

        ProgEditAudio = FitxerINI.INIRead(MyAPP.IniFile, "Data Control", "ProgEditAudio", "")
        If ProgEditAudio.Length > 0 AndAlso IO.File.Exists(ProgEditAudio) Then
            Dim versionInfo As FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(ProgEditAudio)
            Me.mnuProgEditAudio.Text = "Editor: " & versionInfo.FileDescription
            mnuEditAudio.Enabled = True
        End If

        MyThemeForm = CInt(FitxerINI.INIRead(MyAPP.IniFile, "Data Cartutx", "theme", "0"))
        CType(ThemeToolStripMenuItem.DropDownItems(MyThemeForm), ToolStripMenuItem).Checked = True

        ThemeToolStripMenuItem.Visible = CBool(NumCart = 1) 'only show in the first instance.
        setThemeControls()


        If ON_AIR = True Then
            cmdOnAir.Image = PicOnAir.InitialImage
        Else
            cmdOnAir.Image = PicOnAir.ErrorImage
        End If

        ListLanguage()



        If STANDALONE = True Then
            ' inhabilitem els menús
            mnuExplorerDBS.Enabled = False
            mnuExplorerDBS.Checked = False
            mnuMoveDBS.Enabled = False
            mnuIniDirect.Enabled = False
            mnuForceDirectMusic.Enabled = False
            mnuLoadVar.Enabled = False
            cmdLoadLoop.Enabled = False
            cmdSaveLoop.Enabled = False
            mnuMesInfo.Enabled = False
            lbInfo.Text = "Standalone"
            lbInfo.Visible = True
        End If

        If Params.IsAlternativeDir = True Then
            Me.lbInfo.Visible = True
            Me.lbInfo.Text = MSG_ALTERNATIVE_DIR
        End If

        'Control remot

        Select Case NumCart
            Case 1 : USB_FADERSTART = 27 : USB_ON = 28 : USB_CUE = 29
            Case 2 : USB_FADERSTART = 30 : USB_ON = 31 : USB_CUE = 32
            Case 3 : USB_FADERSTART = 33 : USB_ON = 34 : USB_CUE = 35
            Case 4 : USB_FADERSTART = 36 : USB_ON = 37 : USB_CUE = 38
            Case Else : USB_FADERSTART = 27 : USB_ON = 28 : USB_CUE = 29
        End Select
        Me.mnuControlRemot.Visible = CBool(IIf(NumCart = 1, True, False))
        If NumCart = 1 Then
            'mnuAirence.Visible = MyAPP.CtlDebug
            LoadCOMPorts()
            LoadAirence()
        End If

        'For Each item As String In My.Settings.AutoCompleteList
        '    txtPlayURL.AutoCompleteCustomSource.Add(item)
        '    txtPlayURL.Text = item
        'Next

        mnuSession.Enabled = Cloud.ClientType > Cloud.client_type.RADIO_COMMUNITY
        mnuInsertMark.Enabled = mnuSession.Enabled


        If STANDALONE = False Then
            mnuRefMare.DropDownItems.Add(LABEL_SERVER & ": " & MyAPP.getServerName & " / " & MyAPP.getDBSName)
            If Cloud.IsActive = True Then mnuRefMare.DropDownItems.Add(MSG_SYNC_CLOUD)
        Else
            mnuRefMare.DropDownItems.Add(LABEL_SERVER & ": Standalone")
        End If

        Me.buttonDonate.Visible = Cloud.ClientType < Cloud.client_type.RADIO_WITH_SERVICES

        Me.Text = StrNumCart & " " & My.Application.Info.Title
    End Sub

    Private Sub frmCartutxera_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If mnuExplorerDBS.Checked Then My.Forms.frmAudioDBS.Show()
        'Carrèga fitxers per línea de comandament
        If NumCart = 1 Then
            For i As Short = 0 To My.Application.CommandLineArgs.Count - 1
                Dim PathCommandLine As String = Replace(My.Application.CommandLineArgs.Item(i), "~", " ")
                If IO.File.Exists(PathCommandLine) = True Then
                    LoadToList(PathCommandLine)
                    Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS
                    PlayFilePlayer(0)
                End If
            Next i
        End If
        If My.Application.CommandLineArgs.Count = 0 And NumCart = 1 Then
            Dim tempFile As String = IO.Path.GetTempPath & "tracklist.ptc"
            If IO.File.Exists(tempFile) = True Then
                LoadToList(tempFile)
                EnableButtons(0)
            End If
        End If

    End Sub

    Private Sub frmCartutxera_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim Cancel As Boolean = e.Cancel
        Dim UnloadMode As System.Windows.Forms.CloseReason = e.CloseReason
        Select Case UnloadMode
            Case CloseReason.UserClosing
                If NumCart = 1 And SomeFormPlay() = True Then
                    MetroFramework.MetroMessageBox.Show(Me, MSG_STOP_AND_EXIT, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Warning, 100)
                    Cancel = True
                End If
                If CloseAppSilenci = False And Cancel = False Then
                    'si hi han altres instàncies de les cartutxeres obertes no es pot parar el programa.
                    If mnuIniDirect.Checked = False Then
                        Dim frm As Form
                        Dim Opencart As Short
                        For Each frm In My.Application.OpenForms
                            If frm.Name = "frmCartutxera" Then
                                Opencart = Opencart + 1
                            End If
                        Next
                        If NumCart = 1 And Opencart > 1 Then
                            If CloseAppSilenci = False Then Cancel = (MetroFramework.MetroMessageBox.Show(Me, MSG_STOPPING_ALL_CART, MSG_ATENCIO, MessageBoxButtons.YesNo, MessageBoxIcon.Stop, 125) = DialogResult.No)
                        End If
                    End If
                    If Cancel = False Then EndAplic()
                Else
                    WindowState = System.Windows.Forms.FormWindowState.Normal
                End If
            Case System.Windows.Forms.CloseReason.TaskManagerClosing
                MetroFramework.MetroMessageBox.Show(Me, MSG_NO_STOP_APP, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Stop, 100)
                Cancel = True
                WindowState = System.Windows.Forms.FormWindowState.Minimized
            Case System.Windows.Forms.CloseReason.WindowsShutDown
                EndAplic()
            Case Else
                'MsgBox(MSG_DENY_STOP_APP, MsgBoxStyle.Information, MSG_ATENCIO)
                Cancel = True
                WindowState = System.Windows.Forms.FormWindowState.Minimized
        End Select
        Me.Refresh()
        e.Cancel = Cancel
    End Sub

    Friend Sub LoadToList(ByVal Path As String)
        Dim Title As String = ""
        Dim interp As String = ""
        Dim Duration As Date
        Dim BPM As Single = 0
        Dim Handle As Integer
        Dim tLength As Single
        Dim lenTrack As Long
        Dim TI As New Un4seen.Bass.AddOn.Tags.TAG_INFO

        If UCase(Microsoft.VisualBasic.Right(Path, 3)) = "MP3" _
            Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "MP2" _
            Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "MP1" _
            Or UCase(Microsoft.VisualBasic.Right(Path, 4)) = "AIFF" _
            Or UCase(Microsoft.VisualBasic.Right(Path, 4)) = "AIF" _
            Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "WAV" _
            Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "OGG" Then
            Handle = Bass.BASS_StreamCreateFile(Path, 0, 0, BASSFlag.BASS_DEFAULT)
            If (BassTags.BASS_TAG_GetFromFile(Handle, TI)) Then
                interp = TI.artist
                Title = TI.title
            End If
            If Title.Length = 0 Then Title = GetFileName(Path)
            If interp.Length = 0 Then interp = IO.Directory.GetParent(Path).Name
            lenTrack = Bass.BASS_ChannelGetLength(Handle)
            tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
            Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
            If mnuCalcBPMOnLoad.Checked = True Then BPM = getBPMFitxer(Path, Me.Handle)
            addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Title, interp, Path, 0, Duration, #12:00:00 AM#, BPM)
            Bass.BASS_StreamFree(Handle)
        ElseIf UCase(Microsoft.VisualBasic.Right(Path, 3)) = "PTC" Or UCase(Microsoft.VisualBasic.Right(Path, 3)) = "M3U" Then
            CarregaLListat(Path)
        ElseIf UCase(Microsoft.VisualBasic.Right(Path, 3)) = "CDA" Then
            Handle = BassCd.BASS_CD_StreamCreateFile(Path, BASSFlag.BASS_STREAM_AUTOFREE)
            Title = GetFileName(Path)
            interp = Path
            lenTrack = Bass.BASS_ChannelGetLength(Handle)
            tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
            Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
            If mnuCalcBPMOnLoad.Checked = True Then BPM = getBPMFitxer(Path, Me.Handle)
            addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Title, interp, Path, 0, Duration, #12:00:00 AM#, BPM)
            Bass.BASS_StreamFree(Handle)
        End If
    End Sub

    Private Sub CarregaLListat(ByRef sNomFitxer As String, Optional ByRef InitExe As Boolean = False)
        Dim Tipus As Short
        Dim id As Integer = 0
        Dim Titol As String = ""
        Dim SubTitol As String = ""
        Dim Durada As Date
        Dim strDate As String = ""
        Dim StrPath As String = ""
        Dim NumFitxer As Short
        Dim NumParts As Short

        NumParts = 1
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
        'Si la llista està invisible (Presentació de botons)
        'Controla la visió de la botonera
        'cmbTypeShow.SelectedIndex = TypeShow.SHOW_LIST

        NumFitxer = FreeFile()
        FileOpen(NumFitxer, sNomFitxer, OpenMode.Input, OpenAccess.Read, OpenShare.Shared)
        If Microsoft.VisualBasic.Right(sNomFitxer, 3).ToLower = "ptc" Then
            Dim Versio As String = LineInput(NumFitxer)
            If Versio.ToUpper = "V:2" Then
                Do
                    Try
                        'recupera tots els valors del fitxer ptc
                        Input(NumFitxer, Titol)
                        Input(NumFitxer, SubTitol)
                        Input(NumFitxer, strDate)
                        Input(NumFitxer, Tipus)
                        Input(NumFitxer, id)
                        Input(NumFitxer, StrPath)
                        Durada = CDate(strDate)
                        If OKFitxerToPlay(StrPath, Durada) Or Tipus = 200 Or Tipus = 100 Or Tipus = 101 Then
                            addElementlist(Tipus, Titol, SubTitol, StrPath, id, Durada)
                        End If
                    Catch ex As Exception
                    End Try
                Loop Until EOF(NumFitxer)
            End If
        ElseIf Microsoft.VisualBasic.Right(sNomFitxer, 3).ToLower = "m3u" Then
            Dim TI As New Un4seen.Bass.AddOn.Tags.TAG_INFO
            Do
                Input(NumFitxer, StrPath)
                If OKFitxerToPlay(StrPath, Durada) Then
                    Dim AudioHandle As Integer = Bass.BASS_StreamCreateFile(StrPath, 0, 0, BASSFlag.BASS_DEFAULT)
                    If (BassTags.BASS_TAG_GetFromFile(AudioHandle, TI)) Then
                        SubTitol = TI.artist
                        Titol = TI.title
                    End If
                    If Titol.Length = 0 Then Titol = GetFileName(StrPath)
                    If SubTitol.Length = 0 Then SubTitol = StrPath
                    addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Titol, SubTitol, StrPath, 0, Durada)
                End If
            Loop Until EOF(NumFitxer)
        End If
        FileClose(NumFitxer)
        lbInfo.Text = LB_LIST & ": " & sNomFitxer
        lbInfo.Visible = True
        If InitExe Then
            flowBotons.Focus()
            Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS
            PlayFilePlayer(0)
            mnuIniDirect.Checked = True
            Me.picDirecte.Visible = True
        End If
        CalcTotalDurationList()
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub ListDevice()
        'If DeviceAudioEnable = False Then Exit Sub
        Dim info As New BASS_DEVICEINFO
        Dim ID_Dev As Integer = 1
        While Not (info Is Nothing)
            info = Bass.BASS_GetDeviceInfo(ID_Dev)
            If info Is Nothing Then Exit While
            Dim mnuNewDev As ToolStripMenuItem
            Dim mnuNewDevPre As ToolStripMenuItem
            mnuNewDev = New ToolStripMenuItem
            mnuNewDevPre = New ToolStripMenuItem
            mnuNewDev.Text = info.name
            mnuNewDevPre.Text = info.name
            mnuNewDev.Tag = ID_Dev
            mnuNewDevPre.Tag = ID_Dev
            mnuNewDev.Visible = True
            mnuNewDevPre.Visible = True

            mnuDevice.DropDownItems.Add(mnuNewDev)
            mnuDevicePreEscolta.DropDownItems.Add(mnuNewDevPre)

            AddHandler mnuNewDev.Click, AddressOf AssigDevice
            AddHandler mnuNewDevPre.Click, AddressOf AssigDevicePre
            mnuNewDevPre.Checked = (PlayerPre.DeviceID = ID_Dev)

            ID_Dev = ID_Dev + 1
        End While
        ID_Dev -= 1 ' Resta el IdDevice que ha sumat abans de sortir

        Dim frm As Form
        For Each frm In My.Application.OpenForms
            If frm.Name = "frmCartutxera" Then
                NumCart = NumCart + 1
            End If
        Next
        If IsLoadForm("frmAudioDBS") Then
            Dim mnuNewCart As ToolStripMenuItem
            mnuNewCart = New ToolStripMenuItem
            mnuNewCart.Text = "Cartutxera " & NumCart
            Dim KeyShortCut As System.Windows.Forms.Keys = CType(Choose(NumCart, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9), System.Windows.Forms.Keys)
            mnuNewCart.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or KeyShortCut), System.Windows.Forms.Keys)
            mnuNewCart.Tag = NumCart
            mnuNewCart.Visible = True
            frmAudioDBS.mnuAddRepreoduccio.DropDownItems.Add(mnuNewCart)
            AddHandler mnuNewCart.Click, AddressOf frmAudioDBS.AfegirAReproducció_Click
        End If

        Dim frmExplore As Form
        For Each frmExplore In My.Application.OpenForms
            If frmExplore.Name = "frmFileExplorer" Then
                Dim MyFrmExplore As frmFileExplorer = CType(frmExplore, frmFileExplorer)
                Dim mnuNewCart As ToolStripMenuItem
                mnuNewCart = New ToolStripMenuItem
                mnuNewCart.Text = "Cartutxera " & NumCart
                Dim KeyShortCut As System.Windows.Forms.Keys = CType(Choose(NumCart, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9), System.Windows.Forms.Keys)
                mnuNewCart.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or KeyShortCut), System.Windows.Forms.Keys)
                mnuNewCart.Tag = NumCart
                mnuNewCart.Visible = True
                MyFrmExplore.mnuAddRepreoduccio.DropDownItems.Add(mnuNewCart)
                AddHandler mnuNewCart.Click, AddressOf MyFrmExplore.AfegirAReproducció_Click
            End If
        Next

        Dim FitxerINI As New IniFile
        Dim savedDevice As Integer = CInt(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "DevicePlay_" & NumCart.ToString, 999))

        If savedDevice <> 999 Then
            ID_Dev = savedDevice
        Else
            If ID_Dev >= NumCart Then ID_Dev = NumCart
        End If
        'Assigna la targeta de so per defecte segons el nº de cartutxera.
        Try
            CType(mnuDevice.DropDownItems(ID_Dev - 1), ToolStripMenuItem).Checked = True
        Catch ex As Exception
            CType(mnuDevice.DropDownItems(0), ToolStripMenuItem).Checked = True
            ID_Dev = 1
        End Try

        StrNumCart = "Cart " & NumCart & ": "
        DEV_PLAY = CType(mnuDevice.DropDownItems(ID_Dev - 1), ToolStripMenuItem).Tag
        DeviceAudioEnable = IniBASS_CTL(DEV_PLAY, Me.Handle)

    End Sub

    Private Sub AssigDevice(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim i As Short
        For i = 0 To mnuDevice.DropDownItems.Count - 1
            CType(mnuDevice.DropDownItems(i), ToolStripMenuItem).Checked = False
        Next
        CType(sender, ToolStripMenuItem).Checked = True

        DEV_PLAY = CType(sender, ToolStripMenuItem).Tag
        'Si aquest Dev ja està assignat a una altre Cartutxera no fer res
        Bass.BASS_SetDevice(DEV_PLAY)
        'Bass.BASS_Free()

        If Bass.BASS_Init(DEV_PLAY, 44100, Un4seen.Bass.BASSInit.BASS_DEVICE_DEFAULT, Me.Handle) = False Then
            MetroFramework.MetroMessageBox.Show(Me, MSG_ERROR_BASS_NO_INI, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Error, 100)
            Exit Sub
        End If
        'Bass.BASS_SetDevice(IdDevice)
        PanicButton()

    End Sub

    Private Sub AssigDevicePre(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim i As Short
        For i = 0 To mnuDevicePreEscolta.DropDownItems.Count - 1
            CType(mnuDevicePreEscolta.DropDownItems(i), ToolStripMenuItem).Checked = False
        Next
        CType(sender, ToolStripMenuItem).Checked = True
        If PlayerPre.IsPlaying Then PlayerPre.StopAudio()
        PlayerPre = Nothing
        PlayerPre = New PlayerPreEscolta(CType(sender, ToolStripMenuItem).Tag)

        My.Settings.DevicePre = PlayerPre.DeviceID
        My.Settings.Save()

    End Sub

    Private Sub buttonMenu_Click(sender As Object, e As EventArgs) Handles buttonMenu.Click
        mnuDef.Show(MousePosition)
    End Sub

    Private Sub cmbTypePlayer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTypePlayer.SelectedIndexChanged
        Dim refreshWave As Boolean = (ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING)

        Select Case Me.cmbTypePlayer.SelectedIndex
            Case TypePlay.PLAY_STEP
                Me.ToolStripLoop.Visible = False
                toolStripOthers.Visible = True
                ResetNextPlay()
                If refreshWave Then DeleteLoopLabels()
                DisableLoop()
            Case TypePlay.PLAY_CUNTINUOS
                Me.ToolStripLoop.Visible = False
                toolStripOthers.Visible = True
                If ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
                    SetPoxPlay(ActualPlay.AUDIO_ListID + 1)
                End If
                If refreshWave Then DeleteLoopLabels()
                DisableLoop()
            Case TypePlay.PLAY_LOOP
                Me.ToolStripLoop.Visible = True
                toolStripOthers.Visible = False
                ResetNextPlay()
                If refreshWave Then refreshLoopLabels()
                EnableLoop()
        End Select
        Me.mnuAutoDel.Enabled = (cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS)
        CalcTotalDurationList()
    End Sub

    Private Sub DisableLoop()
        Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_DEFAULT, BASSFlag.BASS_SAMPLE_LOOP)
    End Sub

    Private Sub EnableLoop()
        Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_SAMPLE_LOOP, BASSFlag.BASS_SAMPLE_LOOP)
    End Sub

    Private Sub cmbTypeShow_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTypeShow.TextChanged

        For i As Integer = 0 To flowBotons.Controls.Count - 1
            Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(i), MetroFramework.Controls.MetroTile)
            If cmbTypeShow.SelectedIndex = TypeShow.SHOW_LIST Then
                MyTile.Size = New System.Drawing.Size(flowBotons.Width - Magin, TrackBarBigButtons.Value)
                MyTile.Controls(ControlsInButton._LabelBPM).Visible = True
            Else
                MyTile.Size = New System.Drawing.Size(TrackBarBigButtons.Value * 3.7, TrackBarBigButtons.Value)
                MyTile.Controls(ControlsInButton._LabelBPM).Visible = False
            End If
        Next




        'If cmbTypeShow.SelectedIndex = TypeShow.SHOW_LIST Then
        '    'TrackBarBigButtons.Enabled = True
        '    TrackBarBigButtons.Value = 50
        '    For i As Integer = 0 To flowBotons.Controls.Count - 1
        '        Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(i), MetroFramework.Controls.MetroTile)
        '        'MyTile.Width = flowBotons.Width - Magin


        '    Next

        'Else
        '    'TrackBarBigButtons.Enabled = False
        '    For i As Integer = 0 To flowBotons.Controls.Count - 1
        '        'Dim MyType As Type = flowBotons.Controls(i).GetType
        '        If flowBotons.Controls(i).GetType.FullName = "MetroFramework.Controls.MetroTile" Then
        '            Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(i), MetroFramework.Controls.MetroTile)
        '            'MyTile.Width = 120
        '            'MyTile.Height = 60
        '            MyTile.Width = (flowBotons.Width / (TrackBarBigButtons.Value / 10)) - Magin
        '                MyTile.Height = TrackBarBigButtons.Value * 1.5
        '                MyControl.Width = TrackBarBigButtons.Value * 3.7

        '                Dim MyPicSize As Integer = 28
        '            Try
        '                Dim MyPicture As PictureBox = CType(MyTile.Controls(ControlsInButton._PictureBoxCover), PictureBox)
        '                MyPicture.Width = MyPicSize
        '                MyPicture.Height = MyPicSize
        '                MyPicture.Top = MyTile.Height - MyPicSize - 5

        '                'MyTile.Controls(ControlsInButton._PictureBoxCover).Width = MyPicSize
        '                'MyTile.Controls(ControlsInButton._PictureBoxCover).Height = MyPicSize
        '                'MyTile.Controls(ControlsInButton._PictureBoxCover).Top = MyTile.Height - MyPicSize - 5
        '            Catch ex As Exception
        '            End Try
        '            MyTile.Controls(ControlsInButton._LabelBPM).Visible = False
        '        End If
        '    Next
        'End If
    End Sub

    Private Sub buttonExit_Click(sender As Object, e As EventArgs) Handles mnuExit.Click, buttonExit.Click
        Me.Close()
    End Sub

    Private Sub lbDisplayTime_Click(sender As Object, e As EventArgs) Handles lbDisplayTime.Click
        DisplayTemps += 1
        If DisplayTemps = DisplayTime.DISPLAY_TOTAL + 1 Then DisplayTemps = DisplayTime.DISPLAY_ELAPSE
        Select Case DisplayTemps
            Case mdlMscDefines.DisplayTime.DISPLAY_ELAPSE : lbFormatTime.Text = "Elapse Time"
            Case mdlMscDefines.DisplayTime.DISPLAY_REMAIN : lbFormatTime.Text = "Remain Time"
            Case mdlMscDefines.DisplayTime.DISPLAY_TOTAL : lbFormatTime.Text = "Total remain"
        End Select
    End Sub

    Private Sub picCover_Resize(sender As Object, e As EventArgs) Handles picCover.Resize
        picCover.Height = picCover.Width
    End Sub


#End Region

#Region "BOXCTL04"
    Dim BOXCTL04Port As String = ""

    Sub MnuBOXCTL04Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub OpenFormControlCTL04()
        Dim NoLoad As Boolean = False
        For Each frm As Form In My.Application.OpenForms
            If frm.Name = "frmRemote" Then
                NoLoad = True
                Exit For
            End If
        Next
        If NoLoad = False Then frmRemote.Show()
    End Sub


    Private Sub refreshComPort(ByVal sender As System.Object, ByVal e As System.EventArgs)
        mnuBOXCTL04.DropDownItems.Clear()
        LoadCOMPorts()
    End Sub

    Private Sub LoadCOMPorts()
        Dim FitxerINI As New IniFile
        ComPortCTL04 = FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "PORT_CTL04", "")
        Dim PortActive As Boolean = False
        Dim PortCount As Integer = 0
        For Each sp As String In My.Computer.Ports.SerialPortNames
            PortCount += 1
            Dim mnuNewCOMPortCTL04 As ToolStripMenuItem
            mnuNewCOMPortCTL04 = New ToolStripMenuItem
            mnuNewCOMPortCTL04.Name = "mnuComPort" & sp
            mnuNewCOMPortCTL04.Text = sp
            mnuNewCOMPortCTL04.Visible = True
            mnuNewCOMPortCTL04.Checked = (ComPortCTL04 = sp)
            mnuBOXCTL04.DropDownItems.Add(mnuNewCOMPortCTL04)
            AddHandler mnuNewCOMPortCTL04.Click, AddressOf AssigComPort
            If mnuNewCOMPortCTL04.Checked = True Then PortActive = True
        Next

        mnuBOXCTL04.Checked = CBool(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "CTL04_ACTV", 0))
        If mnuBOXCTL04.Checked AndAlso PortActive = True Then
            'Obre la pantalla de control
            IniControlCom()
        Else
            mnuBOXCTL04.Enabled = PortCount > 0
        End If
    End Sub

    Private Sub getVersionCTL04()
        Dim strInfo As String
        If ConnectSerialPort(strInfo) = True Then
            Dim mnuVersionCTL04 As ToolStripMenuItem
            mnuVersionCTL04 = New ToolStripMenuItem
            mnuVersionCTL04.Name = "mnuVersionCTL04"
            mnuVersionCTL04.Text = getVersionBox()
            mnuVersionCTL04.Visible = True
            mnuBOXCTL04.DropDownItems.Add(mnuVersionCTL04)
        End If
        'MsgBox(strInfo, MsgBoxStyle.Critical, MSG_ATENCIO)

    End Sub

    Private Sub AssigComPort(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim MyMnu As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        For i As Integer = 0 To mnuBOXCTL04.DropDownItems.Count - 1
            CType(mnuBOXCTL04.DropDownItems(i), ToolStripMenuItem).Checked = False
        Next
        MyMnu.Checked = True
        ComPortCTL04 = MyMnu.Text
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "PORT_CTL04", ComPortCTL04)
        IniControlCom()
    End Sub

    Private Sub IniControlCom()
        MySerialPort = New SerialPort()
        'Primer consultem la versió
        getVersionCTL04()
        OpenFormControlCTL04()
    End Sub

#End Region

#Region "Airence"

    Sub LoadAirence()
        Dim FitxerINI As New IniFile
        Me.mnuAirence.Checked = CBool(FitxerINI.INIRead(MyAPP.IniFile, "DATA Cartutx", "AIRENCE_ACTV", 0))
        If Me.mnuAirence.Checked = True Then
            MyMixer = Nothing
            MyMixer = New Airence()

            If MyMixer.myDeviceDetected = True Then
                Me.mnuAirence.Checked = True
                Me.mnuAirence.Enabled = True
                Dim mnuVersioAirence As ToolStripMenuItem
                mnuVersioAirence = New ToolStripMenuItem
                mnuVersioAirence.Name = "mnuVersioAirence"

                mnuVersioAirence.Text = "Version lib: " & MyMixer.airenceGetLibraryVersion(0, 0)
                mnuVersioAirence.Visible = True
                mnuAirence.DropDownItems.Add(mnuVersioAirence)

                Dim mnuFirmwareAirence As ToolStripMenuItem
                mnuFirmwareAirence = New ToolStripMenuItem
                mnuFirmwareAirence.Name = "mnuFirmwareAirence"
                mnuFirmwareAirence.Text = "Firmware mixer: " & MyMixer.airenceGetFirmwareVersion(0, 0)
                mnuFirmwareAirence.Visible = True
                mnuAirence.DropDownItems.Add(mnuFirmwareAirence)
                frmAirence.Show()
            Else
                Me.mnuAirence.Enabled = False
                'If MyAPP.CtlDebug = True Then frmAirence.show()
            End If
        End If
    End Sub

#End Region

#Region "Player"
    Private _myDSPProc As DSPPROC ' make it global, so that the GC can not remove it

    Friend Sub LoadFilePlayer(Optional IndexID As Integer = 0, Optional ForcePlay As Boolean = False)

        If flowBotons.Controls.Count = 0 Then Exit Sub
        If (flowBotons.Controls.Count - 1) < IndexID Then Exit Sub
        Bass.BASS_SetDevice(DEV_PLAY)
        Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(IndexID), MetroFramework.Controls.MetroTile)
        Dim Path As String = MyTile.Controls(ControlsInButton._LabelPath).Text
        If ActualPlay.AUDIO_Path = Path And ActualPlay.Load = True And ActualPlay.AUDIO_ListID = IndexID Then
            'ja està carregat    
            If ForcePlay = True Then Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False)
            Exit Sub
        End If

        ActualPlay.AUDIO_TipFitxer = MyTile.Controls(ControlsInButton._LabelTipus).Text
        ActualPlay.AUDIO_Durada = MyTile.Controls(ControlsInButton._LabelDuration).Tag ' Un4seen.Bass.Utils.FixTimespan(ActualPlay.DurationInSecons, "HHMMSS")
        ActualPlay.ElapseTime = 0
        ActualPlay.ElapseInSecons = 0
        ActualPlay.IntroSegons = 0
        ActualPlay.AUDIO_BPM = MyTile.Controls(ControlsInButton._LabelBPM).Tag
        ActualPlay.AUDIO_Titol = MyTile.Controls(ControlsInButton._LabelTitol).Text
        ActualPlay.AUDIO_SubTitol = MyTile.Controls(ControlsInButton._LabelSubTitol).Text
        ActualPlay.AUDIO_ListID = IndexID
        ActualPlay.AUDIO_ID = MyTile.Controls(ControlsInButton._LabelID).Text

        ActualPlay.AUDIO_Path = Path
        ActualPlay.AUDIO_Radiat = False
        PlayAfterPause = False
        Me.picCover.Image = CType(MyTile.Controls(ControlsInButton._PictureBoxCover), PictureBox).Image

        Select Case UCase(IO.Path.GetExtension(Path))
            Case ".CDA"
                ActualPlay.AUDIO_HANDLE = BassCd.BASS_CD_StreamCreateFile(Path, BASSFlag.BASS_STREAM_AUTOFREE Or BASSFlag.BASS_SAMPLE_FLOAT)
            Case ".STOP"
                CType(MyTile.Controls(ControlsInButton._LabelHandle), MetroFramework.Controls.MetroLabel).Text = "99999"
                CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).ForeColor = Color.Blue
                ActualPlay.AUDIO_HANDLE = 0
                Me.picWave.BackgroundImage = CreateImage(picWave, CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).Text)
            Case ".MARK"
                CType(MyTile.Controls(ControlsInButton._LabelHandle), MetroFramework.Controls.MetroLabel).Text = "99999"
                CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).ForeColor = Color.Blue
                ActualPlay.AUDIO_HANDLE = 0
                Dim txt As String = CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).Text
                Me.picWave.BackgroundImage = CreateImage(picWave, txt)
            Case ".STREAM"
                Path = Replace(Path, ".STREAM", "") 'esborrem ".STREAM"
                'detectar el format
                If UCase(Microsoft.VisualBasic.Right(Path, 4)) = "M3U8" Then
                    ActualPlay.AUDIO_HANDLE = BassHLS.BassHls.BASS_HLS_StreamCreateURL(Path, BASSFlag.BASS_STREAM_AUTOFREE Or BASSFlag.BASS_SAMPLE_FLOAT, Nothing, IntPtr.Zero)
                Else
                    ActualPlay.AUDIO_HANDLE = Bass.BASS_StreamCreateURL(Path, 0, BASSFlag.BASS_DEFAULT, Nothing, IntPtr.Zero)
                End If
            Case Else '.mp3 .wav .ogg						
                ActualPlay.AUDIO_HANDLE = Bass.BASS_StreamCreateFile(Path, 0, 0, BASSFlag.BASS_STREAM_AUTOFREE Or BASSFlag.BASS_SAMPLE_FLOAT)
        End Select
        If ActualPlay.AUDIO_HANDLE <> 0 Then
            Bass.BASS_ChannelGetAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_FREQ, ActualPlay.AUDIO_Freq)
            If Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False) = True Then
                If ForcePlay = False Then Bass.BASS_ChannelPause(ActualPlay.AUDIO_HANDLE)
                If cmbTypePlayer.SelectedIndex = TypePlay.PLAY_LOOP Then Bass.BASS_ChannelFlags(ActualPlay.AUDIO_HANDLE, BASSFlag.BASS_MUSIC_LOOP, BASSFlag.BASS_MUSIC_LOOP)
                If ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_URL_STREAM Then
                    ActualPlay.DurationInSecons = SecondDec(MyTile.Controls(ControlsInButton._LabelDuration).Tag)
                    ActualPlay.DurationTime = Bass.BASS_ChannelSeconds2Bytes(ActualPlay.AUDIO_HANDLE, ActualPlay.DurationInSecons)
                    If ActualPlay.DurationTime = 0 Then
                        ActualPlay.DurationTime = 9999999999
                        ActualPlay.DurationInSecons = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.DurationTime)
                        CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).ProgressBarStyle = ProgressBarStyle.Marquee
                        CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Maximum = 99999 'Bass.BASS_ChannelSeconds2Bytes(ActualPlay.AUDIO_HANDLE, 3600000) '1000 hours
                    Else
                        CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).ProgressBarStyle = ProgressBarStyle.Continuous
                        CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Maximum = ActualPlay.DurationInSecons
                    End If
                Else
                    'Busca el punt d'entrada.
                    WF.GetCuePoints(ActualPlay.CuePosition, 0, -24.0, Un4seen.Bass.Utils.LevelToDB(threshold, 32768), True)
                    Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, ActualPlay.CuePosition)

                    ActualPlay.DurationTime = Bass.BASS_ChannelGetLength(ActualPlay.AUDIO_HANDLE)
                    ActualPlay.DurationInSecons = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.DurationTime)
                    CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).ProgressBarStyle = ProgressBarStyle.Continuous
                    CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Maximum = ActualPlay.DurationInSecons
                End If
                ActualPlay.RemainTime = ActualPlay.DurationTime
                ActualPlay.OutSegons = ActualPlay.DurationInSecons - 1

                'Normatiza el volum(aquí busquem el factor
                ActualPlay.gain_factor = 1
                If ActualPlay.DurationInSecons < MAX_SEC_WAVE Then
                    ActualPlay.gain_factor = Utils.GetNormalizationGain(ActualPlay.AUDIO_Path, 1, -1, -1, ActualPlay.peak_db)
                Else
                    ActualPlay.gain_factor = Utils.GetNormalizationGain(ActualPlay.AUDIO_Path, 1, 50, 200, ActualPlay.peak_db)
                End If

                If ActualPlay.gain_factor = -1 Then ActualPlay.gain_factor = 1
                lbGainDB.Text = String.Format("{0:F1}", Utils.LevelToDB(ActualPlay.gain_factor, 1.0))

                ' Create a new callback for when the current track in the mixer has ended
                'm_MixerSynchProc = New Un4seen.Bass.SYNCPROC(AddressOf CurrentTrackEnded)
                'm_MixerSyncHandle = Bass.BASS_ChannelSetSync(ActualPlay.AUDIO_HANDLE, Un4seen.Bass.BASSSync.BASS_SYNC_END Or Un4seen.Bass.BASSSync.BASS_SYNC_MIXTIME, 0, m_MixerSynchProc, 0)

                Me.sldPith.Maximum = ActualPlay.AUDIO_Freq + 30000
                Me.sldPith.Minimum = ActualPlay.AUDIO_Freq - 30000
                Me.sldPith.Value = ActualPlay.AUDIO_Freq

                If ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_URL_STREAM Then
                    BorrarWaveForn()
                Else
                    GetWaveForm(ActualPlay.AUDIO_Path, ActualPlay.gain_factor)
                    'CalcPintaWavePunts()
                End If
            End If
            ArrageDisplayLoad()
            If cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS Then SetPoxPlay(IndexID + 1)
            ActualPlay.Load = True
        End If
    End Sub

    Private Sub MyDSPGain(handle As Integer, channel As Integer,
                      buffer As IntPtr, length As Integer, user As IntPtr)
        ' number of bytes in 32-bit floats, since length is in bytes
        Dim l4 As Integer = length / 4
        Dim data(l4 - 1) As Single
        ' copy from managed to unmanaged memory
        Marshal.Copy(buffer, data, 0, l4)
        ' apply gain, assumeing using 32-bit floats (no clipping here ;-)
        Dim a As Integer
        For a = 0 To l4 - 1
            'data(a) = data(a) * _gainAmplification
            data(a) = data(a) * ActualPlay.gain_factor
        Next a
        ' copy back from unmanaged to managed memory
        Marshal.Copy(data, 0, buffer, l4)
    End Sub

    Dim PlayAfterPause As Boolean = False

    Friend Sub PlayFilePlayer(Optional IndexID As Integer = 0, Optional ForceMix As Boolean = False)

        Me.tmrVuMeter.Enabled = True
        If flowBotons.Controls.Count = 0 Then Exit Sub
        If IndexID = -1 Then Exit Sub
        Static RunInside As Boolean = False
        If RunInside = True Then Exit Sub
        RunInside = True

        Bass.BASS_SetDevice(DEV_PLAY)

        If ForceMix = True And ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING And ActualPlay.AUDIO_ListID <> IndexID Then
            Bass.BASS_ChannelSlideAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_VOL, 0, TimeFadeOut)
        End If

        'Comprovar si està carregat un fitxer
        If ActualPlay.Load = False Or ActualPlay.AUDIO_ListID <> IndexID Then
            LoadFilePlayer(IndexID, True)
        End If

        If PlayAfterPause = False Then
            ' Pantalla        
            Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(ActualPlay.AUDIO_ListID), MetroFramework.Controls.MetroTile)
            MyTile.Controls(ControlsInButton._LabelTRadiacio).Text = "/ " & CStr(TimeOfDay) & " " & LB_HORARADI
            CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).ForeColor = Color.Blue
            MyTile.Controls(ControlsInButton._LabelHandle).Text = ActualPlay.AUDIO_HANDLE

            Select Case ActualPlay.AUDIO_TipFitxer
                Case Tipus_Play.CTL_URL_STREAM
                    Bass.BASS_ChannelSetAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_VOL, 0)
                    Bass.BASS_ChannelSlideAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_VOL, 100, 100000)
                Case Tipus_Play.CTL_META_STOP
                    SetFxVumeter = True
                    RunInside = False
                    LoadFilePlayer(IndexID + 1)
                    'ActualPlay.ForcePause = True
                    Return
                Case Tipus_Play.CTL_META_MARK
                    SetFxVumeter = True
                    'Executo la creació de la marca.
                    Dim MyCommment As String = CType(MyTile.Controls(ControlsInButton._LabelSubTitol), MetroFramework.Controls.MetroLabel).Text
                    CreateMark(MyCommment)
                    If Actual_SessionMarks.SESSION_ID > 0 Then
                        ActualPlay.AUDIO_Path = LB_MARK & " - " & MyCommment
                        ThreadRecPlay()
                        'CallRegistraPlay()
                    End If
                    If (IndexID + 1) < flowBotons.Controls.Count Then
                        RunInside = False
                        LoadFilePlayer(IndexID + 1, True)
                        'PlayFilePlayer(ActualPlay.AUDIO_ListID)
                    End If

                    Return
            End Select
            CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Visible = True

        End If
        If ActualPlay.ForcePause = True Then
            PauseFilePlayer()
            ActualPlay.ForcePause = False
            RunInside = False
            Exit Sub
        ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then
            If Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False) = False Then
                '  Error de Play
                RunInside = False
                Exit Sub
            End If
        End If
        ActualPlay.isActv = Bass.BASS_ChannelIsActive(ActualPlay.AUDIO_HANDLE)
        tmr_Play.Enabled = True
        If PlayAfterPause = False Then
            'Normalitza el volum       
            Dim MyHandleDSP As Integer = 0
            If mnuGainVolum.Checked = True AndAlso ActualPlay.gain_factor > 1 Then
                ' set a DSP user callback method            
                _myDSPProc = New DSPPROC(AddressOf MyDSPGain)
                MyHandleDSP = Bass.BASS_ChannelSetDSP(ActualPlay.AUDIO_HANDLE, _myDSPProc, IntPtr.Zero, 0)
            End If
            'registra reproducció
            CallRegistraPlay()

            frmHistory.addElementlistHistory(ActualPlay)

            ' gestió dels Handles
            ProxPlay = Nothing
            If Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS Then
                Dim NextIndPlay As Integer = ActualPlay.AUDIO_ListID + 1
                If NextIndPlay <= flowBotons.Controls.Count - 1 Then
                    SetPoxPlay(NextIndPlay)
                Else
                    'loop del llistat
                    If mnuBucleList.Checked = True Then
                        SetPoxPlay(0)
                    End If
                End If
            End If

            Dim Indexhandle As Integer = 0
            Try
                Indexhandle = PlayedHandle.Length
            Catch ex As Exception
                Indexhandle = 0
            End Try
            ReDim Preserve PlayedHandle(Indexhandle)
            PlayedHandle(Indexhandle).HANDLE_PLAY = ActualPlay.AUDIO_HANDLE
            PlayedHandle(Indexhandle).HANDLE_DSP = MyHandleDSP
        End If
        PlayAfterPause = True
        cmdPlayPause.Image = Me.picPause.Image
        RunInside = False
        Me.Refresh()
    End Sub

    Private Sub CallRegistraPlay()
        If STANDALONE = False AndAlso filRegistraPlay.IsAlive = False Then
            filRegistraPlay = Nothing
            filRegistraPlay = New Thread(AddressOf ThreadRecPlay)
            filRegistraPlay.IsBackground = True
            filRegistraPlay.SetApartmentState(ApartmentState.STA)
            filRegistraPlay.Priority = ThreadPriority.Lowest
            filRegistraPlay.Start()
        End If
    End Sub

    Private Sub ThreadRecPlay()
        RegistrarPlay(ActualPlay.AUDIO_Path, ActualPlay.AUDIO_ID, ActualPlay.AUDIO_TipFitxer)
    End Sub

    Private Sub SetPoxPlay(IDlist As Integer)
        If IDlist > flowBotons.Controls.Count - 1 Then Exit Sub
        ResetNextPlay()
        Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(IDlist), MetroFramework.Controls.MetroTile)
        CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).ForeColor = Color.Gray
        Dim Path As String = MyTile.Controls(ControlsInButton._LabelPath).Text
        ProxPlay.AUDIO_TipFitxer = MyTile.Controls(ControlsInButton._LabelTipus).Text
        ProxPlay.DurationTime = Bass.BASS_ChannelGetLength(ActualPlay.AUDIO_HANDLE)
        ProxPlay.RemainTime = ActualPlay.DurationTime
        ProxPlay.DurationInSecons = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.DurationTime)
        ProxPlay.AUDIO_Durada = MyTile.Controls(ControlsInButton._LabelDuration).Tag ' Un4seen.Bass.Utils.FixTimespan(ActualPlay.DurationInSecons, "HHMMSS")
        ProxPlay.ElapseTime = 0
        ProxPlay.ElapseInSecons = 0
        ProxPlay.IntroSegons = 0
        ProxPlay.OutSegons = ActualPlay.DurationInSecons - 1
        ProxPlay.AUDIO_BPM = MyTile.Controls(ControlsInButton._LabelBPM).Tag
        Try
            CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Maximum = ProxPlay.DurationInSecons
        Catch ex As Exception
            ' error quan és streaming
            CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Maximum = 1000
        End Try
        ProxPlay.AUDIO_Titol = MyTile.Controls(ControlsInButton._LabelTitol).Text
        ProxPlay.AUDIO_SubTitol = MyTile.Controls(ControlsInButton._LabelSubTitol).Text
        ProxPlay.AUDIO_ListID = IDlist
        'ProxPlay.AUDIO_HoraRadi = Now.ToString("HH:mm:ss")
        ProxPlay.AUDIO_ID = MyTile.Controls(ControlsInButton._LabelID).Text
        ProxPlay.AUDIO_Path = Path
        ProxPlay.AUDIO_Radiat = False

    End Sub

    Private Sub ResetNextPlay()
        For i As Integer = 0 To flowBotons.Controls.Count - 1
            Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(i), MetroFramework.Controls.MetroTile)
            If CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).ForeColor = Color.Gray Then
                CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).ForeColor = Color.Black
            End If
        Next
    End Sub

    Private Sub ManagePlayedHandle()
        Static blflat As Boolean = False
        If blflat = True Then Exit Sub
        If IsNothing(PlayedHandle) Then Exit Sub
        blflat = True
        Dim TempPlayedHandle() As ManageHandle
        For i As Integer = 0 To flowBotons.Controls.Count - 1
            Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(i), MetroFramework.Controls.MetroTile)
            Dim MyHandle As Integer = MyTile.Controls(ControlsInButton._LabelHandle).Text
            If (Bass.BASS_ChannelIsActive(MyHandle) = BASSActive.BASS_ACTIVE_STOPPED And MyHandle <> 0) Or MyHandle = 99999 Then '99999 és un meta
                CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Visible = False
                If mnuAutoDel.Checked = True Then
                    DeleteButtton(MyTile)
                    blflat = False
                    Exit Sub
                End If
            Else
                If MyHandle <> 0 Then ' 0 = pendent de reprocucció
                    CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Value = Bass.BASS_ChannelBytes2Seconds(MyHandle, Bass.BASS_ChannelGetPosition(MyHandle))
                    'stop
                    Dim ActualVol As Single = 0
                    Bass.BASS_ChannelGetAttribute(MyHandle, BASSAttribute.BASS_ATTRIB_VOL, ActualVol)
                    If ActualVol = 0 Then
                        Bass.BASS_ChannelStop(MyHandle)
                        Dim MyHandleDSP As Integer = GetHandleDSP(MyHandle)
                        If MyHandleDSP <> 0 Then Bass.BASS_ChannelRemoveDSP(MyHandle, MyHandleDSP)
                    End If

                    For H As Integer = 0 To PlayedHandle.Length - 1
                        If PlayedHandle(H).HANDLE_PLAY = MyHandle Then
                            Dim Inxlen As Integer = 0
                            Try
                                Inxlen = TempPlayedHandle.Length
                            Catch ex As Exception
                                Inxlen = 0
                            End Try
                            ReDim Preserve TempPlayedHandle(Inxlen)
                            TempPlayedHandle(Inxlen).HANDLE_PLAY = PlayedHandle(H).HANDLE_PLAY
                            TempPlayedHandle(Inxlen).HANDLE_DSP = PlayedHandle(H).HANDLE_DSP
                            Exit For
                        End If
                    Next
                End If
            End If
        Next
        PlayedHandle = TempPlayedHandle
        blflat = False
    End Sub

    Private Function GetHandleDSP(HdlPlay As Integer) As Integer
        For i As Integer = 0 To PlayedHandle.Length - 1
            If PlayedHandle(i).HANDLE_PLAY = HdlPlay Then
                Return PlayedHandle(i).HANDLE_DSP
            End If
        Next
    End Function

    Friend Sub PauseFilePlayer()
        Bass.BASS_SetDevice(DEV_PLAY)
        Bass.BASS_ChannelPause(ActualPlay.AUDIO_HANDLE)
        cmdPlayPause.Image = Me.picPlay.Image
        Me.Refresh()
    End Sub

    Friend Sub StopFilePlayer()
        If flowBotons.Controls.Count = 0 Then Exit Sub
        Bass.BASS_SetDevice(DEV_PLAY)
        Bass.BASS_ChannelStop(ActualPlay.AUDIO_HANDLE)
        Bass.BASS_StreamFree(ActualPlay.AUDIO_HANDLE)
        Try
            CType(flowBotons.Controls(ActualPlay.AUDIO_ListID).Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Visible = False
            CType(flowBotons.Controls(ActualPlay.AUDIO_ListID).Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).FontWeight = MetroFramework.MetroLabelWeight.Light
            CType(flowBotons.Controls(ActualPlay.AUDIO_ListID).Controls(ControlsInButton._LabelHandle), MetroFramework.Controls.MetroLabel).Text = 0
        Catch ex As Exception

        End Try

        cmdPlayPause.Image = Me.picPlay.Image
        lbDisplayTitol.Text = Params.NomRadio

        BorrarWaveForn()
        'If ActualPlay.AUDIO_ListID = 0 Then BorrarWaveForn()

        ActualPlay = Nothing
        Me.Text = StrNumCart & " " & My.Application.Info.Title
        Me.Refresh()
    End Sub

    Private Sub cmdNext_Click(sender As Object, e As EventArgs) Handles cmdNext.Click
        If flowBotons.Controls.Count = 0 Then Exit Sub
        Dim IdList As Integer
        If ProxPlay.Load = True Then
            IdList = ProxPlay.AUDIO_ListID
        Else
            IdList = ActualPlay.AUDIO_ListID + 1
        End If
        If IdList > (flowBotons.Controls.Count - 1) Then Exit Sub

        If ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Then
            PlayFilePlayer(IdList, True)
        ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
            LoadFilePlayer(IdList)
        End If
        Me.Refresh()
    End Sub

    Private Sub cmdPrev_Click(sender As Object, e As EventArgs) Handles cmdPrev.Click
        If flowBotons.Controls.Count = 0 Then Exit Sub
        Dim IdList As Integer = ActualPlay.AUDIO_ListID - 1

        If IdList < 0 Then Exit Sub

        If ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Then
            PlayFilePlayer(IdList, True)
        ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
            LoadFilePlayer(IdList)
        End If
        Me.Refresh()
    End Sub

    Private Sub cmdPlayPause_Click(sender As Object, e As EventArgs) Handles cmdPlayPause.Click
        If IsNothing(ActualPlay) = False AndAlso ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Then
            PauseFilePlayer()
        ElseIf IsNothing(ActualPlay) = False AndAlso ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Then
            PlayFilePlayer(ActualPlay.AUDIO_ListID)
        Else
            PlayFilePlayer(FindFocusedButton)
        End If
    End Sub

    Private Sub cmdStop_Click(sender As Object, e As EventArgs) Handles cmdStop.Click
        StopFilePlayer()
    End Sub

    Private Sub cmdCue_Click(sender As Object, e As EventArgs) Handles cmdCue.Click
        If PlayerPre.IsPlaying = False Then
            PlayerPre.PlayAudio(ActualPlay.AUDIO_Path)

        Else
            Dim pos As Long = Bass.BASS_ChannelGetPosition(PlayerPre.PreEscoltaHandle)
            ActualPlay.CuePosition = Bass.BASS_ChannelBytes2Seconds(PlayerPre.PreEscoltaHandle, pos)
            InitCue = True
            'Desplaçar-ho    
            If Not IsNothing(WF) Then
                WF.AddMarker("CUE", ActualPlay.CuePosition)
                DrawWave()
            End If
            Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, ActualPlay.CuePosition)
        End If
    End Sub

    Private Sub RegistrarPlay(ByVal strPathFitxer As String, ByVal id As Short, ByVal TipFitxer As mdlMscDefines.Tipus_Play)

        If TipFitxer < mdlMscDefines.Tipus_Play.CTL_PROGRAMA Then strPathFitxer = lbDisplayTitol.Text
        addCtlRadi(id, CShort(TipFitxer), 0, #1:00:00 AM#, 0, 0, Usuari.UsrID, VB.Left(strPathFitxer, 250), 0, ON_AIR)
        Dim StrSql As String = ""
        Select Case TipFitxer
            Case mdlMscDefines.Tipus_Play.CTL_MUSICA
                StrSql = "UPDATE temes "
                StrSql = StrSql & " SET"
                StrSql = StrSql & " tema_dataradiacio = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "'"
                StrSql = StrSql & ", tema_numradiacions = tema_numradiacions + 1"
                StrSql = StrSql & " WHERE tema_id = " & id & " ;"
            Case mdlMscDefines.Tipus_Play.CTL_PUBLICITAT
                StrSql = "UPDATE falques SET falc_dataradi ='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "',falc_nradia = falc_nradia+1 WHERE falc_id=" & id
            Case mdlMscDefines.Tipus_Play.CTL_PROGRAMA
                'S'ha de salvar les dades a la sessió 
                StrSql = "UPDATE programes SET ses_dataradiacio ='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "',ses_numradiacions = ses_numradiacions+1 WHERE ses_id=" & id
            Case mdlMscDefines.Tipus_Play.CTL_AUDIO_USR
                StrSql = "UPDATE audios_params SET audio_dataradiacio ='" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "',audio_nradia = audio_nradia+1 WHERE audio_id=" & id
            Case Else
                Exit Sub
        End Select
        Dim db As New MSC.dbs(Cloud)
        db.Update_ID(StrSql)
        db = Nothing
    End Sub

    ' Mixer sync proc callback for when the current track has ended
    'Private Delegate Sub UpdateFormDelegate()
    'Private UpdateFormDelegateDisplay As UpdateFormDelegate
    ' Mixer handle to the bass synch callback when the current track in the mixer ends
    'Private m_MixerSynchProc As Un4seen.Bass.SYNCPROC
    'Private m_MixerSyncHandle As Int32 = 0

    'Private Sub CurrentTrackEnded(ByVal MixerHandle As Int32, ByVal Channel As Int32, ByVal Data As Int32, ByVal User As IntPtr)
    '    ' Do stuff here when the track ends
    '    UpdateFormDelegateDisplay = New UpdateFormDelegate(AddressOf UpdateDisplay)
    '    Me.Invoke(UpdateFormDelegateDisplay)
    '    Beep()

    'End Sub

    'Private Sub UpdateDisplay()

    'End Sub

    Private Sub AutoDelete(InxToDel As Integer)
        If mnuAutoDel.Checked AndAlso Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS AndAlso InxToDel > -1 Then
            Dim MyControl As Control = flowBotons.Controls(InxToDel)
            DeleteButtton(MyControl)
        End If
    End Sub

    Private Sub ArrageDisplayLoad()

        If STANDALONE = False Then
            'Busca Loop
            Dim db As New MSC.dbs(Cloud)
            Dim StrSql As String = "SELECT loop_id FROM loops"
            If ActualPlay.AUDIO_ID > 0 Then
                StrSql = StrSql & " WHERE loop_id = " & ActualPlay.AUDIO_ID & " ;"
            Else
                Dim path As String = ActualPlay.AUDIO_Path
                StrSql = StrSql & " WHERE loop_idstr = '" & getMD5Hash(path) & "' ;"
            End If
            Dim ret As Object = db.ExecuteScalar(StrSql)
            cmdLoadLoop.Enabled = CBool(ret IsNot Nothing)
            ActualPlay.IntroSegons = 0
            If ActualPlay.AUDIO_TipFitxer = Tipus_Play.CTL_MUSICA Then
                ActualPlay.IntroSegons = SecondDec(db.ExecuteScalar("SELECT tema_intro FROM temes WHERE tema_id=" & ActualPlay.AUDIO_ID).ToString)
            End If
            db = Nothing
        End If
        Me.cmdLoopIn.BackColor = Color.Transparent
        ActualPlay.LoopIn = 0
        Me.cmdLoopOut.BackColor = Color.Transparent
        ActualPlay.LoopOut = ActualPlay.DurationTime
        If cmbTypePlayer.SelectedIndex = TypePlay.PLAY_LOOP Then refreshLoopLabels()
        ' end loop

        Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(ActualPlay.AUDIO_ListID), MetroFramework.Controls.MetroTile)

        MyTile.ForeColor = Color.Blue

        For i As Integer = 0 To flowBotons.Controls.Count - 1
            'Reset Bold                        
            'CType(flowBotons.Controls(i), MetroFramework.Controls.MetroTile).TileTextFontWeight = MetroFramework.MetroTileTextWeight.Light
            CType(CType(flowBotons.Controls(i), MetroFramework.Controls.MetroTile).Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).FontWeight = MetroFramework.MetroLabelWeight.Light
        Next
        CType(MyTile.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).FontWeight = MetroFramework.MetroLabelWeight.Bold


        Me.lbBPM.Text = ActualPlay.AUDIO_BPM
        lbDisplayTitol.Text = ActualPlay.AUDIO_Titol & " / " & ActualPlay.AUDIO_SubTitol
        Me.Text = StrNumCart & IIf(Len(Me.lbDisplayTitol.Text) > 31, VB.Left(Me.lbDisplayTitol.Text, 31) & "...", Me.lbDisplayTitol.Text)

        Dim TI As New Un4seen.Bass.AddOn.Tags.TAG_INFO
        BassTags.BASS_TAG_GetFromFile(ActualPlay.AUDIO_HANDLE, TI)
        Me.lbInfoTrack.Text = TI.channelinfo.ToString() & ", " & TI.bitrate & " Kbps"
        cmdPlayPause.Image = Me.picPlay.Image
        Me.cmdCue.Image = Me.picCueOFF.Image
        lbCue.BackColor = Color.Lime
        InitCue = False
        CalcTotalDurationList()
        Me.Refresh()
    End Sub

    Private Function CalcTotalDurationList() As Long

        Dim segTotals As Long = 0
        If flowBotons.Controls.Count > 0 Then
            For I As Integer = ActualPlay.AUDIO_ListID + 1 To Me.flowBotons.Controls.Count - 1
                Dim Duration As String = CType(flowBotons.Controls(I).Controls(ControlsInButton._LabelDuration), MetroFramework.Controls.MetroLabel).Tag
                segTotals = segTotals + SecondDec(Duration)
            Next
            If (ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING) Then
                segTotals += ActualPlay.RemainInSecons
            Else
                segTotals += ActualPlay.DurationInSecons
            End If
        End If
        lbTotal.Text = Un4seen.Bass.Utils.FixTimespan(segTotals, "HHMMSSFF")
        TotalDuration = segTotals
        Return TotalDuration
    End Function

    Private Sub RecCalcDuradaAll()
        Dim segTotals As Long = 0
        For I As Integer = 0 To Me.flowBotons.Controls.Count - 1
            Dim Duration As String = CType(flowBotons.Controls(I).Controls(ControlsInButton._LabelDuration), MetroFramework.Controls.MetroLabel).Tag
            segTotals = segTotals + SecondDec(Duration)
        Next
        lbTotal.Text = Un4seen.Bass.Utils.FixTimespan(segTotals, "HHMMSSFF")
        TotalDuration = segTotals
    End Sub

    Private Function findIndexActualPlay() As Integer
        If Me.picWave.BackgroundImage IsNot Nothing Then
            Return ActualPlay.AUDIO_ListID
        ElseIf ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Then
            Return ActualPlay.AUDIO_ListID
        Else
            Return FindFocusedButton()
        End If
    End Function

    Sub CalcPintaWavePunts()
        If Not IsNothing(WF) Then
            'CUE
            'WF.ClearAllMarker()
            WF.AddMarker("CUE", ActualPlay.CuePosition)

            'INTRO        
            WF.AddMarker("INTRO", ActualPlay.IntroSegons)

            'END
            If ActualPlay.AUDIO_TipFitxer <> mdlMscDefines.Tipus_Play.CTL_PUBLICITAT Then
                WF.GetCuePoints(0, ActualPlay.OutSegons, -40.0, Un4seen.Bass.Utils.LevelToDB(threshold, 32768), True)
            End If
            'si la posició OutSegons és més gran que 70 % estableix OutSegons = sldDuration.Maximum
            Dim MinEND As Integer = CInt(ActualPlay.DurationInSecons * 0.8)
            If ActualPlay.OutSegons < MinEND Then
                ActualPlay.OutSegons = ActualPlay.DurationInSecons - 1
            End If
            If ActualPlay.OutSegons > ActualPlay.DurationInSecons Then
                'error, borrar waveform
                ActualPlay.OutSegons = ActualPlay.DurationInSecons - 1
                Dim tmpWaveFile As String = IO.Path.ChangeExtension(ActualPlay.AUDIO_Path, ".wf")
                Try
                    If IO.File.Exists(tmpWaveFile) Then IO.File.Delete(tmpWaveFile)
                Catch ex As Exception

                End Try
            End If
            WF.AddMarker("END", ActualPlay.OutSegons)
        End If
    End Sub

    Friend Sub IniDirecte()
        picDirecte.Visible = mnuIniDirect.Checked
        If mnuIniDirect.Checked = True Then Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS
        lbInfo.Text = mnuIniDirect.Text
        lbInfo.Visible = mnuIniDirect.Checked
    End Sub

    Private Sub lbStrPith_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbStrPith.LinkClicked

        If ActualPlay.AUDIO_Freq = 0 Then
            sldPith.Value = 44100
            lbStrPith.BackColor = Color.Lime
        Else
            sldPith.Value = ActualPlay.AUDIO_Freq
            canviPith()
        End If
    End Sub

    Private Sub sldPith_Scroll(sender As Object, e As EventArgs) Handles sldPith.Scroll
        canviPith()
    End Sub

    Private Sub sldPith_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles sldPith.MouseWheel
        Try
            If e.Delta > 0 Then
                sldPith.Value = sldPith.Value + 1000
            Else
                sldPith.Value = sldPith.Value - 1000
            End If
        Catch ex As Exception
        End Try
        canviPith()
    End Sub

    Private Sub canviPith()
        Bass.BASS_SetDevice(DEV_PLAY)
        infoPlay.freq = sldPith.Value
        Bass.BASS_ChannelSetAttribute(ActualPlay.AUDIO_HANDLE, BASSAttribute.BASS_ATTRIB_FREQ, infoPlay.freq)
        lbStrPith.Text = Format(sldPith.Value / 1000, "00.0")
        lbStrPith.BackColor = IIf(infoPlay.freq = ActualPlay.AUDIO_Freq, Color.Lime, Color.Red)
    End Sub

    Friend Sub UP_Pitch()
        Dim valPith As Integer = sldPith.Value + 1000
        If valPith > sldPith.Maximum Then valPith = sldPith.Maximum
        sldPith.Value = valPith
        canviPith()
    End Sub

    Friend Sub DOWN_Pitch()
        Dim valPith As Integer = sldPith.Value - 1000
        If valPith < sldPith.Minimum Then valPith = sldPith.Minimum
        sldPith.Value = valPith
        canviPith()
    End Sub

    Friend Sub RemoteSetFFPosition()
        Dim pos As Long = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
        'Desplaçar-ho
        Bass.BASS_ChannelSetPosition(PlayerPre.PreEscoltaHandle, pos + 1000)
    End Sub

    Friend Sub RemoteSetRWPosition()
        Dim pos As Long = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
        'Desplaçar-ho
        Bass.BASS_ChannelSetPosition(PlayerPre.PreEscoltaHandle, pos - 1000)
    End Sub

    Friend Sub SetRemotePlay()
        If flowBotons.Controls.Count = 0 Then Exit Sub
        Dim IndexLlistat As Integer = findIndexActualPlay()
        If ActualPlay.AUDIO_HANDLE = 0 Then
            PlayFilePlayer(IndexLlistat)
        Else
            If ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Then Exit Sub
            PlayFilePlayer(IndexLlistat)
        End If
    End Sub

    Friend Sub SetRemotePause()
        If ActualPlay.AUDIO_HANDLE = 0 Then Exit Sub
        If ActualPlay.isActv <> BASSActive.BASS_ACTIVE_PLAYING Then Exit Sub
        If flowBotons.Controls.Count = 0 Then Exit Sub
        PauseFilePlayer()

    End Sub

    Friend Sub PlayPreEscolta()
        Dim Path As String = ""
        Try
            Dim IndexLlistat As Integer = FindFocusedButton()
            Path = flowBotons.Controls(IndexLlistat).Controls(ControlsInButton._LabelPath).Text
        Catch ex As Exception
            Exit Sub
        End Try
        If Len(Path) = 0 Then Exit Sub
        PlayerPre.PlayAudio(Path)
        PlayerPre.numCart = NumCart

    End Sub

    Friend Sub StopPreEscolta()
        PlayerPre.StopAudio()
    End Sub

    Private Sub cmdPlayPre_Click(sender As Object, e As EventArgs) Handles cmdPlayPre.Click
        If PlayerPre.IsPlaying Then
            StopPreEscolta()
        Else
            PlayPreEscolta()
        End If
    End Sub

#End Region

#Region "WaveForm"

    Private WF As Un4seen.Bass.Misc.WaveForm = Nothing

    Private Sub GetWaveForm(PathFile As String, gainFactor As Single)

        If ActualPlay.DurationInSecons < MAX_SEC_WAVE Then
            Dim tmpWaveFile As String = IO.Path.ChangeExtension(PathFile, ".wf")
            If IO.File.Exists(tmpWaveFile) = True Then
                WF = New WaveForm()
            Else
                WF = New WaveForm(PathFile, New Un4seen.Bass.Misc.WAVEFORMPROC(AddressOf MyWaveFormCallback), Me)
            End If

            WF.FrameResolution = 0.01F ' 10ms are nice
            WF.CallbackFrequency = 500 ' every 10 seconds rendered

            WF.ColorLeft = Color.GreenYellow
            WF.ColorLeftEnvelope = Color.Orange
            WF.ColorRight = Color.LightGray
            WF.ColorRightEnvelope = Color.Orange
            WF.DrawWaveForm = WaveForm.WAVEFORMDRAWTYPE.HalfMono
            If Me.Theme = MetroFramework.MetroThemeStyle.Dark Then
                WF.ColorMarker = Color.White
                WF.ColorBackground = Color.Black
            Else
                WF.ColorMarker = Color.Black
                WF.ColorBackground = Color.White
            End If
            WF.DrawMarker = WaveForm.MARKERDRAWTYPE.Line Or WaveForm.MARKERDRAWTYPE.Name Or WaveForm.MARKERDRAWTYPE.NamePositionAlternate Or WaveForm.MARKERDRAWTYPE.NameBoxFillInverted
            WF.MarkerLength = 0.75

            If WF.WaveFormLoadFromFile(tmpWaveFile, True) = True Then
                DrawWave()
            Else
                ' it is important to use the same resolution flags as for the playing stream
                ' e.g. if an already playing stream is 32-bit float, so this should also be 
                ' or use 'SyncPlayback' as shown below
                WF.RenderStart(True, BASSFlag.BASS_SAMPLE_FLOAT)
            End If
            CalcPintaWavePunts()
        Else
            WF = Nothing
            BorrarWaveForn()
            Me.picWave.BackgroundImage = CreateImage(picWave, MSG_ERROR_WAVE_TOO_LONG)
        End If

    End Sub

    Private Sub MyWaveFormCallback(framesDone As Integer, framesTotal As Integer, elapsedTime As TimeSpan, finished As Boolean)
        ' will be called during rendering...		

        If finished = True Then
            ManualEnd = False
            'CalcPintaWavePunts()
            ' if your playback stream uses a different resolution than the WF 
            ' use this to sync them
            WF.SyncPlayback(ActualPlay.AUDIO_HANDLE)
            If ActualPlay.AUDIO_ID > 0 Then
                Dim tmpWaveFile As String = IO.Path.ChangeExtension(ActualPlay.AUDIO_Path, ".wf")
                If IO.File.Exists(tmpWaveFile) = False Then
                    WF.WaveFormSaveToFile(tmpWaveFile, True)
                End If
            End If
        End If
        DrawWave()

    End Sub

    Private Sub DrawWave()
        If Not (WF Is Nothing) Then
            Try
                Me.picWave.BackgroundImage = WF.CreateBitmap(Me.picWave.Width, Me.picWave.Height, -1, -1, False)
                'Application.DoEvents()
            Catch ex As Exception
                Me.picWave.BackgroundImage = Nothing
            End Try
        End If
    End Sub

    Sub PicWaveMouseClick(sender As Object, e As MouseEventArgs) Handles picWave.MouseClick
        If Control.ModifierKeys = Keys.Control Then
            'Beep()
        End If
        If e.Button = System.Windows.Forms.MouseButtons.Left And e.Clicks = 1 Then
            If PlayerPre.IsPlaying = True Then
                'Despalcem la preescolta
                Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
                'Posició on s'ha de desplaçar 
                Dim len As Long = Bass.BASS_ChannelGetLength(PlayerPre.PreEscoltaHandle)
                Dim bpp As Double = len / CType(Me.picWave.Width, Double) ' bytes per pixel  
                Dim PosADespla As Single = Bass.BASS_ChannelBytes2Seconds(PlayerPre.PreEscoltaHandle, CLng(LocalMousePosition.X * bpp))
                'Desplaçar-ho
                Bass.BASS_ChannelSetPosition(PlayerPre.PreEscoltaHandle, PosADespla)
            Else
                PlayFromWave()
            End If
        End If
    End Sub

    Sub PlayFromWave()
        'estableix la posicio del ratolí a sobre el Pig
        '-----			
        'If ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED And ActualPlay.AUDIO_HANDLE <> 0 Then
        '    'No sé quin sentit té això 
        '    Dim path As String = ""
        '    Try
        '        Dim IndexLlistat As Integer = findIndexActualPlay()
        '        path = flowBotons.Controls(IndexLlistat).Controls(ControlsInButton._ProgressBar).Tag
        '    Catch ex As Exception
        '        If Me.flowBotons.Controls.Count = 0 Then Exit Sub
        '        path = flowBotons.Controls(0).Controls(ControlsInButton._ProgressBar).Tag
        '    End Try

        '    Select Case UCase(IO.Path.GetExtension(path))
        '        Case ".CDA"
        '            ActualPlay.AUDIO_HANDLE = BassCd.BASS_CD_StreamCreateFile(path, BASSFlag.BASS_STREAM_AUTOFREE)
        '        Case Else ' .mp3 .wav
        '            ActualPlay.AUDIO_HANDLE = Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_STREAM_AUTOFREE)
        '    End Select
        '    'Bass.BASS_ChannelPlay(ActualHandle,False)
        '    'Bass.BASS_ChannelPause(ActualHandle)			
        'End If

        '-----		
        If IsNothing(ActualPlay) Then Exit Sub
        If ActualPlay.isActv = BASSActive.BASS_ACTIVE_PAUSED Or ActualPlay.isActv = BASSActive.BASS_ACTIVE_PLAYING Then
            Dim len As Long = Bass.BASS_ChannelGetLength(ActualPlay.AUDIO_HANDLE)
            Dim bpp As Double = len / CType(Me.picWave.Width, Double) ' bytes per pixel        
            'Quin és el pixel on es troba el ratolí
            'Dim pr As Integer = 0
            Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
            'Posició on s'ha de desplaçar        
            Dim PosADespla As Single = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, CLng(LocalMousePosition.X * bpp))
            'Desplaçar-ho
            Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, PosADespla)
        End If
        'If ActualPlay.isActv = BASSActive.BASS_ACTIVE_STOPPED Then Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False)

    End Sub

    Sub Pic_MouseDown(sender As Object, e As MouseEventArgs) Handles picWave.MouseUp, picWave.MouseDown
        If IsNothing(WF) Then Exit Sub
        Dim segpix As Double = ActualPlay.DurationInSecons / CType(picWave.Width, Double) ' bytes per pixel        
        'Quin és el pixel on es troba el ratolí
        Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
        'Posició on s'ha de desplaçar        		
        Dim LocationX As Integer = LocalMousePosition.X
        If e.Button = System.Windows.Forms.MouseButtons.Right Then
            If LocationX < (picWave.Width / 2) Then
                If LocationX < 2 Then LocationX = 2
                Select Case ActualPlay.isActv
                    Case BASSActive.BASS_ACTIVE_PAUSED
                        ActualPlay.CuePosition = LocationX * segpix
                        WF.AddMarker("CUE", ActualPlay.CuePosition)
                        Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, ActualPlay.CuePosition)
                    Case BASSActive.BASS_ACTIVE_PLAYING
                        ActualPlay.IntroSegons = LocationX * segpix
                        WF.AddMarker("INTRO", ActualPlay.IntroSegons)
                    Case Else
                        Exit Sub
                End Select
            Else
                If LocationX > Me.picWave.Width Then LocationX = Me.picWave.Width - 2
                ActualPlay.OutSegons = LocationX * segpix
                WF.AddMarker("END", ActualPlay.OutSegons)
                ManualEnd = True
            End If
            DrawWave()
        End If
    End Sub

    Sub MnuWavePlayClick(sender As Object, e As EventArgs) Handles mnuWavePlay.Click
        PlayFromWave()
    End Sub

    Sub PicWaveResize(sender As Object, e As EventArgs) Handles picWave.SizeChanged
        DrawWave()
    End Sub

    Sub BorrarWaveForn()
        Me.picWave.BackgroundImage = Nothing
        picCover.Image = Nothing
        ActualPlay.IntroSegons = 0
        ActualPlay.CuePosition = 0
        Dim segpix As Double = ActualPlay.DurationInSecons / CType(picWave.Width, Double)
        ActualPlay.OutSegons = ActualPlay.DurationInSecons
    End Sub

#End Region

#Region "Loop Mode"

    Private Sub refreshLoopLabels()
        Dim LoopInSeg As Double = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.LoopIn)
        Dim LoopOutSeg As Double = Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.LoopOut)
        Me.lbLoopIn.Text = Un4seen.Bass.Utils.FixTimespan(LoopInSeg, "MMSSFF")
        Me.lbLoopOut.Text = Un4seen.Bass.Utils.FixTimespan(Bass.BASS_ChannelBytes2Seconds(ActualPlay.AUDIO_HANDLE, ActualPlay.LoopOut), "MMSSFF")
        'LoopIn
        'LoopOut 
        If Not IsNothing(WF) Then
            WF.AddMarker("Loop In", LoopInSeg)
            WF.AddMarker("Loop Out", LoopOutSeg)
            DrawWave()
        End If
    End Sub

    Sub DeleteLoopLabels()
        If Not IsNothing(WF) Then
            WF.RemoveMarker("Loop In")
            WF.RemoveMarker("Loop Out")
            DrawWave()
        End If
    End Sub
    Private Sub cmdSaveLoop_Click(sender As Object, e As EventArgs) Handles cmdSaveLoop.Click

        Dim db As New MSC.dbs(Cloud)
        Dim SqlStr As String = ""

        Dim idInt As Integer = 0
        Dim idStr As String = ""
        If ActualPlay.AUDIO_TipFitxer = mdlMscDefines.Tipus_Play.CTL_SISTEMA Then
            idStr = getMD5Hash(ActualPlay.AUDIO_Path)
            SqlStr = "DELETE FROM loops WHERE loop_idstr = '" & idStr & "' ;"
        Else
            idInt = ActualPlay.AUDIO_ID
            SqlStr = "DELETE FROM loops WHERE loop_id = " & idInt & " ;"
        End If
        db.Delete_ID(SqlStr)

        SqlStr = "INSERT INTO loops (loop_id, loop_in, loop_out, loop_idstr, loop_tipfitxer)"
        SqlStr = SqlStr & " VALUES "
        SqlStr = SqlStr & " ( " & idInt & ""
        SqlStr = SqlStr & ", '" & ActualPlay.LoopIn & "'"
        SqlStr = SqlStr & ", '" & ActualPlay.LoopOut & "'"
        SqlStr = SqlStr & ", '" & idStr & "'"
        SqlStr = SqlStr & ", " & ActualPlay.AUDIO_TipFitxer
        SqlStr = SqlStr & ");"

        db.New_ID(SqlStr)
        db = Nothing
        cmdSaveLoop.Enabled = False
    End Sub

    Private Sub cmdLoadLoop_Click(sender As Object, e As EventArgs) Handles cmdLoadLoop.Click
        If STANDALONE = False Then
            Dim db As New MSC.dbs(Cloud)
            Dim sCmd As String
            If ActualPlay.AUDIO_TipFitxer = mdlMscDefines.Tipus_Play.CTL_SISTEMA Then
                sCmd = "SELECT loop_in,loop_out FROM loops Where loop_idstr = '" & getMD5Hash(ActualPlay.AUDIO_Path) & "' ;"
            Else
                sCmd = "SELECT loop_in,loop_out FROM loops Where loop_id =" & ActualPlay.AUDIO_ID & " ;"
            End If
            Dim rs As DataTable = db.getTable(sCmd)
            If rs.Rows.Count > 0 Then
                ActualPlay.LoopIn = rs.Rows(0)("loop_in")
                ActualPlay.LoopOut = rs.Rows(0)("loop_out")
            End If

            refreshLoopLabels()

            cmdLoopIn.BackColor = Color.Lime
            'cmdLoopIn.Enabled = True
            cmdLoopOut.BackColor = Color.Lime
            'cmdLoopOut.Enabled = True

            db = Nothing
            rs.Dispose()
        End If
    End Sub

    Private Sub cmdLoopOut_Click(sender As Object, e As EventArgs) Handles cmdLoopOut.Click
        Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_LOOP
        If cmdLoopOut.BackColor = Color.Lime Then
            ActualPlay.LoopOut = ActualPlay.DurationTime
            cmdLoopOut.BackColor = Color.Transparent
        Else
            ActualPlay.LoopOut = ActualPlay.ElapseTime
            cmdLoopOut.BackColor = Color.Lime
        End If
        cmdSaveLoop.Enabled = Not STANDALONE

        refreshLoopLabels()
    End Sub

    Private Sub cmdLoopIn_Click(sender As Object, e As EventArgs) Handles cmdLoopIn.Click
        Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_LOOP
        If cmdLoopIn.BackColor = Color.Lime Then
            ActualPlay.LoopIn = 0
            cmdLoopIn.BackColor = Color.Transparent
        Else
            ActualPlay.LoopIn = ActualPlay.ElapseTime
            cmdLoopIn.BackColor = Color.Lime
        End If
        cmdSaveLoop.Enabled = Not STANDALONE
        refreshLoopLabels()
    End Sub

    Private Sub cmdLoopInMes_Click(sender As Object, e As EventArgs) Handles cmdLoopInMes.Click
        If cmdLoopIn.BackColor <> Color.Lime Then Exit Sub
        ActualPlay.LoopIn += 2000
        If ActualPlay.LoopIn > ActualPlay.DurationTime Then ActualPlay.LoopIn = ActualPlay.DurationTime
        refreshLoopLabels()
        cmdSaveLoop.Enabled = Not STANDALONE
    End Sub

    Private Sub cmdLoopInMenys_Click(sender As Object, e As EventArgs) Handles cmdLoopInMenys.Click
        If cmdLoopIn.BackColor <> Color.Lime Then Exit Sub
        ActualPlay.LoopIn -= 2000
        If ActualPlay.LoopIn < 0 Then ActualPlay.LoopIn = 0
        refreshLoopLabels()
        cmdSaveLoop.Enabled = Not STANDALONE
    End Sub

#End Region

#Region "Carregar Salvar i esborrar fitxers"

    Private Sub cmdLoad_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        Dim FirstToAdd As Integer = flowBotons.Controls.Count
        LoadAllFitxers()
        EnableButtons(FirstToAdd)
    End Sub

    Private Sub LoadAllFitxers()
        Dim i As Integer
        With CommonDialog1
            .FileName = ""
            .Title = COMMONDIALOG1_TITLE
            .Multiselect = True
            .ShowReadOnly = False 'Falgs, allows Multi select, Explorer style and hide the Read only tag            
            .Filter = LIST_FILTER_FILES & " MP3 (*.mp3) |*.mp3; |" &
                LIST_FILTER_FILES & " MP2 (*.mp2) |*.mp2; |" &
                LIST_FILTER_FILES & " MP1 (*.mp1) |*.mp1; |" &
                LIST_FILTER_FILES & " Wave (*.wav) |*.wav; |" &
                LIST_FILTER_FILES & " CD Audio (*.cda) |*.CDA; |" &
                LIST_FILTER_FILES & " Ogg (*.ogg)|*.ogg; |" &
                LIST_FILTER_FILES & " Aiff (*.aiff)|*.aiff; |" &
                LIST_FILTER_FILES & " Aif (*.aif)|*.aif; |" &
                LIST_FILTER_CART & " (*.ptc)|*.ptc; |" &
                LIST_FILTER_WINAMP & " (*.m3u) |*.m3u"
            .ShowDialog()

            For i = 0 To .FileNames.Length - 1
                CarregaFitxer(.FileNames(i))
            Next
        End With
    End Sub

    Friend Sub CarregaFitxer(ByRef path As String)
        Dim TI As New Un4seen.Bass.AddOn.Tags.TAG_INFO
        Dim Handle As Integer
        Dim tLength As Single
        Dim lenTrack As Long
        Dim Title As String = ""
        Dim interp As String = ""
        Dim Duration As Date
        Dim BPM As Single = 0

        If UCase(Microsoft.VisualBasic.Right(path, 3)) = "MP3" _
            Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "MP1" _
            Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "MP2" _
            Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "WAV" _
            Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "WMA" _
            Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "OGG" _
            Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "AIF" _
            Or UCase(Microsoft.VisualBasic.Right(path, 4)) = "AIFF" Then

            Handle = Bass.BASS_StreamCreateFile(path, 0, 0, BASSFlag.BASS_DEFAULT)
            If (BassTags.BASS_TAG_GetFromFile(Handle, TI)) Then
                interp = TI.artist
                Title = TI.title
            End If
            If Len(Title) = 0 Then Title = GetFileName(path)
            If Len(interp) = 0 Then interp = GetFileName(path)
            lenTrack = Bass.BASS_ChannelGetLength(Handle)
            tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
            Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
            If mnuCalcBPMOnLoad.Checked = True Then BPM = getBPMFitxer(path, Me.Handle)
            addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Title, interp, path, 0, Duration, #12:00:00 AM#, BPM)
            Bass.BASS_StreamFree(Handle)
        ElseIf UCase(Microsoft.VisualBasic.Right(path, 3)) = "PTC" Or UCase(Microsoft.VisualBasic.Right(path, 3)) = "M3U" Then
            CarregaLListat(path)
        ElseIf UCase(Microsoft.VisualBasic.Right(path, 3)) = "CDA" Then
            Handle = BassCd.BASS_CD_StreamCreateFile(path, BASSFlag.BASS_STREAM_AUTOFREE)
            Title = GetFileName(path)
            interp = path
            lenTrack = Bass.BASS_ChannelGetLength(Handle)
            tLength = Bass.BASS_ChannelBytes2Seconds(Handle, lenTrack)
            Duration = Un4seen.Bass.Utils.FixTimespan(tLength, "HHMMSS")
            If mnuCalcBPMOnLoad.Checked = True Then BPM = getBPMFitxer(path, Me.Handle)
            addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, Title, interp, path, 0, Duration, #12:00:00 AM#, BPM)
            Bass.BASS_StreamFree(Handle)
        End If
    End Sub

    Private Sub cmdSalvar_Click(sender As Object, e As EventArgs) Handles cmdSalvar.Click
        salvarLlistat()
    End Sub

    Private Sub salvarLlistat()
        Dim sNomFitxer As String

Line1:
        With SaveFileDialog1
            .InitialDirectory = Params.PathDefPauta & "\"
            .Title = SAVE_FILE_TITLE
            .Filter = SAVE_FILE_FILTER & " (*.ptc)|*.ptc"
            .FileName = SAVE_FILE_NAME
            .CheckFileExists = False
            If .ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then Exit Sub

            sNomFitxer = .FileName
            If IO.File.Exists(sNomFitxer) Then
                If MetroFramework.MetroMessageBox.Show(Me, String.Format(MSG_OVERWRITE_LIST, sNomFitxer), MSG_ATENCIO, MessageBoxButtons.YesNo, MessageBoxIcon.Error, 120) = DialogResult.No Then GoTo Line1
            End If
        End With
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
        WriteTracklist(sNomFitxer)
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub WriteTracklist(PathtrackList As String)
        Dim I As Short
        Dim strTXT As String
        'Dim NumFichero As Short
        'NumFichero = FreeFile()
        'Error si només és de lectura el dispositiu

        Using writer As System.IO.StreamWriter = My.Computer.FileSystem.OpenTextFileWriter(PathtrackList, False, System.Text.Encoding.UTF8)

            writer.WriteLine("V:" & FORMAT_FITXER)
            For I = 0 To flowBotons.Controls.Count - 1
                Dim Titol As String = flowBotons.Controls(I).Controls(ControlsInButton._LabelTitol).Text
                Dim Tipus As Short = flowBotons.Controls(I).Controls(ControlsInButton._LabelTipus).Text
                Dim SubTitol As String = flowBotons.Controls(I).Controls(ControlsInButton._LabelSubTitol).Text
                Dim Path As String = flowBotons.Controls(I).Controls(ControlsInButton._LabelPath).Text
                Dim id As String = flowBotons.Controls(I).Controls(ControlsInButton._LabelID).Text
                Dim Durada As String = flowBotons.Controls(I).Controls(ControlsInButton._LabelDuration).Tag

                strTXT = Chr(34) & Titol & Chr(34) & ","
                strTXT = strTXT & Chr(34) & SubTitol & Chr(34) & ","
                strTXT = strTXT & Durada & ","
                strTXT = strTXT & Tipus & ","
                strTXT = strTXT & id & ","
                strTXT = strTXT & Chr(34) & Path & Chr(34)                
                writer.WriteLine(strTXT)
            Next

        End Using


    End Sub

    Private Sub cmdBorrar_Click(sender As Object, e As EventArgs) Handles cmdBorrar.Click
        Click_Borrar()
    End Sub

    Private Sub cmdBorrar_DragDrop(sender As Object, e As DragEventArgs) Handles cmdBorrar.DragDrop
        Dim DaObj As New DataObject(sender)
        If DaObj.GetDataPresent(GetType(MetroFramework.Controls.MetroTile)) Then
            Click_Borrar()
        End If


    End Sub

    Private Sub cmdBorrar_DragEnter(sender As Object, e As DragEventArgs) Handles cmdBorrar.DragEnter

        Dim DaObj As New DataObject(sender)
        If DaObj.GetDataPresent(GetType(MetroFramework.Controls.MetroTile)) Then
            e.Effect = DragDropEffects.Move
        End If
    End Sub

    Private Sub Click_Borrar()
        cartOrigenMove = Nothing
        MoveInterPlayers = False

        Dim CountList As Integer = flowBotons.Controls.Count - 1
        Do Until CountList = -1
            Dim MyControl As Control = flowBotons.Controls(CountList)
            If CType(MyControl.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = True Then
                DeleteButtton(MyControl)
            End If
            CountList -= 1
        Loop

        TotalDuration = CalcTotalDurationList()
        Me.cmdBorrar.Enabled = IIf(flowBotons.Controls.Count > 0, True, False)
        Me.cmdSalvar.Enabled = cmdBorrar.Enabled
        mnuDel.Enabled = cmdBorrar.Enabled
        cmdBorrar.Enabled = cmdBorrar.Enabled
        mnuAutoSincroMare.Enabled = cmdBorrar.Enabled
        Me.mnuSaveFile.Enabled = cmdBorrar.Enabled

    End Sub

    Friend Sub DeleteButtton(MyControl As MetroFramework.Controls.MetroTile)

        Dim idList As Integer = flowBotons.Controls.GetChildIndex(MyControl)
        If ActualPlay.AUDIO_ListID = idList Then
            Select Case ActualPlay.isActv
                Case BASSActive.BASS_ACTIVE_PLAYING
                    If MetroFramework.MetroMessageBox.Show(Me, MSG_SURE_DELETE_PLAYING, MSG_ATENCIO, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 150) = DialogResult.Yes Then
                        StopFilePlayer()
                    Else
                        Exit Sub
                    End If
                Case BASSActive.BASS_ACTIVE_PAUSED : StopFilePlayer()
                Case BASSActive.BASS_ACTIVE_STOPPED : BorrarWaveForn()
            End Select
        ElseIf ProxPlay.AUDIO_ListID = idList Then
            SetPoxPlay(idList + 1)
        End If

        Try
            Dim DelHandle As Integer = CInt(CType(flowBotons.Controls(idList).Controls(ControlsInButton._LabelHandle), MetroFramework.Controls.MetroLabel).Text)
            If DelHandle > 0 Then
                Bass.BASS_SetDevice(DEV_PLAY)
                Bass.BASS_StreamFree(DelHandle)
            End If
        Catch ex As Exception
        End Try

        If idList <= ActualPlay.AUDIO_ListID Then ActualPlay.AUDIO_ListID -= 1
        If idList <= ProxPlay.AUDIO_ListID Then ProxPlay.AUDIO_ListID -= 1

        flowBotons.Controls.Remove(MyControl)

        'RemoveHandler MyControl.Click, AddressOf buttonsTracks_Click
        RemoveHandler MyControl.MouseDown, AddressOf buttonsTracks_MouseDown
        RemoveHandler MyControl.DragEnter, AddressOf buttonsTracks_DragEnter
        RemoveHandler MyControl.DragDrop, AddressOf buttonsTracks_DragDrop

        MyControl.Dispose()
    End Sub

#End Region

#Region "mnuDef"
    Private Sub mnuExplorerDBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExplorerDBS.Click
        OpenExplorerDBS()
    End Sub

    Private Sub OpenExplorerDBS()
        Dim frm As Form
        For Each frm In My.Application.OpenForms
            If frm.Name = "frmCartutxera" Then
                Dim formCart As Form = Nothing
                formCart = frm
                CType(formCart, frmCartutxera).mnuExplorerDBS.Checked = Not CType(formCart, frmCartutxera).mnuExplorerDBS.Checked
            End If
        Next

        If mnuExplorerDBS.Checked Then
            My.Forms.frmAudioDBS.Show()
        Else
            If IsLoadForm("frmAudioDBS") Then My.Forms.frmAudioDBS.Close()
        End If

        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "FormDBS", IIf(mnuExplorerDBS.Checked = True, 1, 0))
        FitxerINI = Nothing
    End Sub

    Private Sub mnuExplorerPC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExplorerPC.Click
        '		Dim p As System.Diagnostics.Process = New System.Diagnostics.Process()
        '		p.StartInfo.FileName = "explorer.exe"
        '		p.Start()
        Dim frmExplore As New frmFileExplorer
        frmExplore.Show()
    End Sub

    Private Sub mnuNewCartut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNewCartut.Click
        OpenNewPlayer()
    End Sub

    Private Sub OpenNewPlayer()
        If STANDALONE = True Then
            Dim cart As New frmCartutxera
            cart.Show()
        ElseIf MyAPP.NovaConnex("MSC Cartutxera", Usuari.UsrNom, True) = True Then
            Dim cart As New frmCartutxera
            cart.Show()
        End If
    End Sub

    Private Sub mnuMesInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMesInfo.Click
        Try
            Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
            Dim MyTile As MetroFramework.Controls.MetroTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
            Dim Tipus As Tipus_Play = MyTile.Controls(ControlsInButton._LabelTipus).Text
            Dim id As Integer = MyTile.Controls(ControlsInButton._LabelID).Text
            Dim strText As String = strInfoFitxer(Tipus, id)
            InfoMsg = New frmTip
            InfoMsg.ShowMessage(strText, MES_INFO, IconStyles.Information,
                    ContentAlignment.MiddleCenter, 0, 0, True, , , , , Themes.WinXpStyle, MessageBoxButtons.OK)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub PlayCue(Path As String)
        PlayerPre.numCart = NumCart
        If PlayerPre.IsPlaying Then PlayerPre.StopAudio()
        PlayerPre.PlayAudio(Path)
    End Sub

    Private Sub mnuPreEscolta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPreEscolta.Click
        Dim PathFitxer As String = ""
        Try
            Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
            Dim MyTile As MetroFramework.Controls.MetroTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
            PathFitxer = MyTile.Controls(ControlsInButton._LabelPath).Text
            PlayCue(PathFitxer)
        Catch ex As Exception
            For i As Integer = 0 To flowBotons.Controls.Count - 1
                If CType(flowBotons.Controls(i).Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = True Then
                    PathFitxer = flowBotons.Controls(i).Controls(ControlsInButton._LabelPath).Text
                    PlayCue(PathFitxer)
                    Exit For
                End If
            Next
        End Try
    End Sub

    Private Sub mnuReOrdCart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReOrdCart.Click
        Dim frm As Form
        Dim _top As Integer = 10
        Dim _left As Integer = 10
        For Each frm In My.Application.OpenForms
            'If frm.Name = "frmCartutxera"Then
            frm.Left = _left
            frm.Top = _top
            frm.Width = 405
            frm.Height = 500
            frm.Focus()
            _top = _top + 25
            _left = _left + 100
            'End If
        Next
    End Sub

    Private Sub mnuForceDirectMusic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuForceDirectMusic.Click
        mnuForceDirectMusic.Checked = Not mnuForceDirectMusic.Checked
    End Sub

    Private Sub mnuMoveDBS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMoveDBS.Click
        If IsLoadForm("frmAudioDBS") Then
            frmAudioDBS.Width = 814
            frmAudioDBS.Height = 343
            frmAudioDBS.Left = 31
            frmAudioDBS.Top = 583
        End If
    End Sub

    Private Sub mnuBucleList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBucleList.Click
        mnuBucleList.Checked = Not mnuBucleList.Checked
        If mnuBucleList.Checked = True Then mnuAutoDel.Checked = False
    End Sub


    Private Sub mnuProgEditAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuProgEditAudio.Click
        With OpenFileDialog1
            .FileName = ""
            .Title = OPENFILEDIALOG1_TITLE
            .Multiselect = False
            .Filter = LIST_FILTER_FILES & " EXE (*.exe) |*.exe"
            If .ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Dim IniFile As New IniFile
                IniFile.INIWrite(MyAPP.IniFile, "Data Control", "ProgEditAudio", .FileName)
                'My.Settings.ProgEditAudio = .FileName
                Dim versionInfo As FileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(.FileName)
                Me.mnuProgEditAudio.Text = "Editor: " & versionInfo.FileDescription
                mnuEditAudio.Enabled = True
            End If
        End With

    End Sub

    Private Sub EditAudiWave()
        If ProgEditAudio.Length > 0 Then
            'TODO:Possible problema si ja està sonant (S'hauria de parar per alliberar el fitxer)
            Dim PathFitxer As String = ""


            For i As Integer = 0 To flowBotons.Controls.Count - 1
                Dim tempPath As String = flowBotons.Controls(i).Controls(ControlsInButton._LabelPath).Text
                If IO.File.Exists(tempPath) And CType(flowBotons.Controls(i).Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = True Then
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
        End If
    End Sub

    Private Sub mnuEditAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditAudio.Click
        EditAudiWave()
    End Sub


    Private Sub mnuSavedevicePlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSavedevicePlay.Click
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "DevicePlay_" & NumCart.ToString, DEV_PLAY)
    End Sub

    Private Sub mnuDelDevicePlay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDelDevicePlay.Click
        Dim FitxerINI As New IniFile
        For i As Integer = 0 To 10
            FitxerINI.INIDelete(MyAPP.IniFile, "DATA Cartutx", "DevicePlay_" & i.ToString)
        Next
    End Sub

    Private Sub mnuCalcBPMOnLoad_Click(sender As Object, e As EventArgs) Handles mnuCalcBPMOnLoad.Click
        mnuCalcBPMOnLoad.Checked = Not mnuCalcBPMOnLoad.Checked
    End Sub

    Private Sub mnuCalBPMList_Click(sender As Object, e As EventArgs) Handles mnuCalBPMList.Click
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.AppStarting
        For MyId As Integer = 0 To flowBotons.Controls.Count - 1
            Dim Path As String = flowBotons.Controls(MyId).Controls(ControlsInButton._LabelPath).Text
            flowBotons.Controls(MyId).Controls(ControlsInButton._LabelBPM).Text = CInt(getBPMFitxer(Path, Me.Handle))
            'My.Application.DoEvents()
        Next MyId
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub mnuSH_Click(sender As Object, e As EventArgs) Handles mnuSH.Click
        If IO.File.Exists(Params.PathSenyalHora) Then
            Dim HoraEnd As Date
            Me.mnuSH.Checked = Not Me.mnuSH.Checked
            bl_AutoSinc = Me.mnuSH.Checked
            If bl_AutoSinc = True Then
                flowBotons.Controls.Clear()
                addElementlist(mdlMscDefines.Tipus_Play.CTL_SISTEMA, LABEL_SENYALS_HORARIS, Params.NomRadio, Params.PathSenyalHora, 0, TimeSerial(0, 0, SegSH), #12:00:00 AM#, 0)
                If TimeOfDay.Minute < 30 Then
                    TimeToStart = TimeSerial(TimeOfDay.Hour, 29, (60 - SegSH))
                    HoraEnd = TimeSerial(TimeOfDay.Hour, 29, 60)
                Else
                    TimeToStart = TimeSerial(TimeOfDay.Hour, 59, (60 - SegSH))
                    HoraEnd = TimeSerial(TimeOfDay.Hour, 59, 60)
                End If
                mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": " & TimeToStart.ToString("HH:mm:ss")
                lbInfo.Text = LABEL_H_INI & ": " & TimeToStart.ToString("HH:mm:ss") & "/ " & LABEL_H_END & ": " & HoraEnd.ToString("HH:mm:ss")
                If TimeToStart < TimeOfDay Then
                    'fas tard per sincronitzar	
                    MetroFramework.MetroMessageBox.Show(Me, String.Format(MSG_ERROR_DURADA_SINCRO, TimeSerial(0, 0, TotalDuration).ToString("HH:mm:ss")), MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Error, 120)
                    GoTo LineError
                End If
            End If
            lbInfo.Visible = bl_AutoSinc
            Exit Sub
LineError:
            bl_AutoSinc = False
            lbAutoSinc.BackColor = Color.Lime
            Me.mnuSH.Checked = False
            mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00"
        End If
    End Sub

    Private Sub mnuHistoryPlay_Click(sender As Object, e As EventArgs) Handles mnuHistoryPlay.Click
        frmHistory.Show()
    End Sub

    Private Sub mnuPanicButton_Click(sender As Object, e As EventArgs) Handles mnuPanicButton.Click
        PanicButton()
    End Sub

    Private Sub PanicButton()
        Try
            PlayerPre.StopAudio()
            Dim ActualFitxer As String = ActualPlay.AUDIO_Path
            Dim actualPosition As Long = Bass.BASS_ChannelGetPosition(ActualPlay.AUDIO_HANDLE)
            Call Bass.BASS_Stop()
            Call Bass.BASS_Free()

            If Bass.BASS_Init(DEV_PLAY, 44100, Un4seen.Bass.BASSInit.BASS_DEVICE_DEFAULT, Me.Handle) = False Then
                MetroFramework.MetroMessageBox.Show(Me, MSG_ERROR_BASS_NO_INI, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Error, 100)
                Exit Sub
            End If
            If ActualFitxer.Length <> 0 Then
                ActualPlay.AUDIO_HANDLE = Bass.BASS_StreamCreateFile(ActualFitxer, 0, 0, 0)
                Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, actualPosition)
                Bass.BASS_ChannelPlay(ActualPlay.AUDIO_HANDLE, False)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub mnuAirence_Click(sender As Object, e As EventArgs) Handles mnuAirence.Click
        Me.mnuBOXCTL04.Checked = False
        Me.mnuAirence.Checked = Not Me.mnuAirence.Checked
        Try
            If Me.mnuAirence.Checked = True Then
                MyMixer = Nothing
                MyMixer = New Airence()
                If MyMixer.myDeviceDetected = True Then frmAirence.Show()
                mnuAirence.Checked = MyMixer.myDeviceDetected
                mnuAirenceMap.Enabled = MyMixer.myDeviceDetected
            Else
                If MyMixer.myDeviceDetected = True Then frmAirence.Close()
                MyMixer = Nothing
            End If
        Catch ex As Exception
            Me.mnuAirence.Checked = False
        End Try
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "AIRENCE_ACTV", IIf(mnuAirence.Checked = True, 1, 0))
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "CTL04_ACTV", 0)
    End Sub

    Private Sub mnuInsertStop_Click(sender As Object, e As EventArgs) Handles mnuInsertStop.Click
        addElementlist(Tipus_Play.CTL_META_STOP, "STOP", Params.NomRadio, "stop.stop", 0, "00:00:00", #12:00:00 AM#, 0)
    End Sub

    Private Sub txtPlayURL_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPlayURL.KeyDown
        If e.KeyCode = Keys.Enter Then
            Dim SubTitle As String = ""
            Dim URL As String = Me.txtPlayURL.Text.Trim
            If UCase(Microsoft.VisualBasic.Right(URL, 3)) = "PLS" Then
                Dim FitxerINI As New IniFile
                URL = FitxerINI.INIRead(URL, "playlist", "File1", "")
                SubTitle = FitxerINI.INIRead(URL, "playlist", "Title1", "")
            End If

            Dim channel As Integer = 0

            If UCase(Microsoft.VisualBasic.Right(URL, 4)) = "M3U8" Then
                channel = BassHLS.BassHls.BASS_HLS_StreamCreateURL(URL, BASSFlag.BASS_DEFAULT, Nothing, IntPtr.Zero)
            Else
                channel = Bass.BASS_StreamCreateURL(URL, 0, BASSFlag.BASS_DEFAULT, Nothing, IntPtr.Zero)
            End If

            If channel <> 0 Then
                Dim tagInfo As New AddOn.Tags.TAG_INFO(URL)
                Dim Title As String = "Play Stream"
                SubTitle = Params.NomRadio
                If BassTags.BASS_TAG_GetFromURL(channel, tagInfo) Then
                    Title = tagInfo.title & " - " & tagInfo.artist
                    SubTitle = tagInfo.album
                End If
                URL = URL & ".STREAM"

                addElementlist(Tipus_Play.CTL_URL_STREAM, Title, SubTitle, URL, 0, "00:00:00", "00:00:00", 0)
                'My.Settings.AutoCompleteList.Add(URL)
                'My.Settings.Save()
                txtPlayURL.AutoCompleteCustomSource.Add(URL)
            Else
                'Error URL no valida	
                MetroFramework.MetroMessageBox.Show(Me, MSG_ERROR_LOAD_URL, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Error, 100)
            End If
        End If
    End Sub

    Private Sub mnuAirenceMap_Click(sender As Object, e As EventArgs) Handles mnuAirenceMap.Click
        frmAirence.Show()
    End Sub

    Sub MnuSpeedClick(sender As Object, e As EventArgs) Handles mnuSpeedSlow.Click, mnuSpeedNormal.Click, mnuSpeedFast.Click
        'Dim Mymnu As New ToolStripMenuItem = CType (sender,ToolStripMenuItem)
        For i As Integer = 0 To mnuSpeed.DropDownItems.Count - 1
            CType(mnuSpeed.DropDownItems(i), ToolStripMenuItem).Checked = False
        Next

        CType(sender, ToolStripMenuItem).Checked = True
        Select Case CType(sender, ToolStripMenuItem).Tag
            Case 0 : TimeFadeOut = 5000
            Case 1 : TimeFadeOut = 3000
            Case 2 : TimeFadeOut = 1000
        End Select

    End Sub

    Private Sub mnuAutoSincroStartPlay_Click(sender As Object, e As EventArgs) Handles mnuAutoSincroStartPlay.Click, mnuAutoSincroEndPlay.Click
        mnuAutoSincroStartPlay.Checked = False
        mnuAutoSincroEndPlay.Checked = False
        CType(sender, ToolStripMenuItem).Checked = True

        mnuAutoSincro59.Enabled = mnuAutoSincroEndPlay.Checked
        mnuAutoSincro29.Enabled = mnuAutoSincroEndPlay.Checked
    End Sub

    Private Sub mnuOpenFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        LoadAllFitxers()
    End Sub

    Private Sub mnuSaveFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveFile.Click
        salvarLlistat()
    End Sub

    Private Sub mnuPlayPause_Click(sender As Object, e As EventArgs) Handles mnuPlayPause.Click
        SetPlayPause()
    End Sub

    Private Sub SetPlayPause()
        If flowBotons.Controls.Count = 0 Then Exit Sub
        If ActualPlay.isActv = Un4seen.Bass.BASSActive.BASS_ACTIVE_PLAYING Then
            SetRemotePause()
        Else
            SetRemotePlay()
        End If
    End Sub

    Private Function FindFocusedButton() As Integer
        Dim Ctl As Control
        For Each Ctl In flowBotons.Controls
            Dim MyTile As MetroFramework.Controls.MetroTile = CType(Ctl, MetroFramework.Controls.MetroTile)
            If CType(MyTile.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = True Then
                Return flowBotons.Controls.GetChildIndex(MyTile)
            End If
        Next
        Return 0
    End Function

    Private Sub mnuStop_Click(sender As Object, e As EventArgs) Handles mnuStop.Click
        StopFilePlayer()
    End Sub

    Private Sub AutomàticaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutomàticaToolStripMenuItem.Click
        SetContinuousOrStepPlayer()
    End Sub

    Private Sub SetContinuousOrStepPlayer()
        AutomàticaToolStripMenuItem.Checked = Not AutomàticaToolStripMenuItem.Checked
        If AutomàticaToolStripMenuItem.Checked = True Then
            cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS
        Else
            cmbTypePlayer.SelectedIndex = TypePlay.PLAY_STEP
        End If
        LoopToolStripMenuItem.Checked = False
    End Sub

    Private Sub LoopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoopToolStripMenuItem.Click
        SetUnsetTypeLoop()
    End Sub

    Private Sub SetUnsetTypeLoop()
        LoopToolStripMenuItem.Checked = Not LoopToolStripMenuItem.Checked
        If LoopToolStripMenuItem.Checked = True Then
            cmbTypePlayer.SelectedIndex = TypePlay.PLAY_LOOP
        Else
            cmbTypePlayer.SelectedIndex = TypePlay.PLAY_STEP
        End If
        AutomàticaToolStripMenuItem.Checked = False
    End Sub

    Sub PichToolStripMenuItemClick(sender As Object, e As EventArgs)
        UP_Pitch()
    End Sub

    Sub PichToolStripMenuItem1Click(sender As Object, e As EventArgs)
        DOWN_Pitch()
    End Sub

    Private Sub mnuLoad_Click(sender As Object, e As EventArgs)
        LoadAllFitxers()
    End Sub

    Private Sub mnuDel_Click(sender As Object, e As EventArgs)
        Click_Borrar()
    End Sub

    Private Sub mnuAutoDel_Click(sender As Object, e As EventArgs) Handles mnuAutoDel.Click
        mnuAutoDel.Checked = Not mnuAutoDel.Checked
    End Sub

    Private Sub mnuAutoFader_Click(sender As Object, e As EventArgs) Handles mnuAutoFader.Click
        mnuAutoFader.Checked = Not mnuAutoFader.Checked
    End Sub

    Private Sub mnuIniDirect_Click(sender As Object, e As EventArgs) Handles mnuIniDirect.Click
        mnuIniDirect.Checked = Not mnuIniDirect.Checked
        mnuForceDirectMusic.Enabled = mnuIniDirect.Checked
        IniDirecte()
    End Sub

    Private Sub mnuAutoSincro59_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAutoSincro59.Click, mnuAutoSincro29.Click, mnuAutoSincroXX.Click, mnuAutoSincro00.Click
        Dim blAS As Boolean = Not CType(sender, ToolStripMenuItem).Checked

        mnuAutoSincroXX.Checked = False
        mnuAutoSincro29.Checked = False
        mnuAutoSincro59.Checked = False
        mnuAutoSincro00.Checked = False

        If blAS = True Then
            lbAutoSinc.Cursor = Cursors.Hand
            Dim HoraEnd As Date
            Dim seg_he As Integer
            Dim seg_total As Integer
            Dim frmresult As Boolean
            TotalDuration = CalcTotalDurationList()
            seg_total = TotalDuration
            seg_he = 60 - SegSH
            If mnuAutoSincroEndPlay.Checked = True Then
                'calcula hora final.
                Select Case CType(sender, ToolStripMenuItem).Name
                    Case "mnuAutoSincro59"
                        TimeToStart = TimeSerial(TimeOfDay.Hour, 59, seg_he - seg_total)
                        HoraEnd = TimeSerial(TimeOfDay.Hour, 59, seg_he)
                        mnuAutoSincro59.Checked = True
                        bl_AutoSinc = True
                    Case "mnuAutoSincro00"
                        TimeToStart = TimeSerial(TimeOfDay.Hour + 1, 0, -seg_total)
                        HoraEnd = TimeSerial(TimeOfDay.Hour + 1, 0, 0)
                        mnuAutoSincro00.Checked = True
                        bl_AutoSinc = True
                    Case "mnuAutoSincro29"
                        TimeToStart = TimeSerial(TimeOfDay.Hour, 29, seg_he - seg_total)
                        HoraEnd = TimeSerial(TimeOfDay.Hour, 29, seg_he)
                        mnuAutoSincro29.Checked = True
                        bl_AutoSinc = True
                    Case "mnuAutoSincroXX"
                        Dim DataSincroEnd As Long
                        Try
                            frmSincro.ShowDialog()
                            If frmSincro.DialogResult = System.Windows.Forms.DialogResult.OK Then
                                frmresult = True
                                HoraEnd = CDate(frmSincro.txtHoraEnd.Value)
                                DataSincroEnd = SecondDec(frmSincro.txtHoraEnd.Value)
                                TimeToStart = TimeSerial(0, 0, DataSincroEnd - seg_total)
                                mnuAutoSincroXX.Checked = True
                                bl_AutoSinc = True
                            Else
                                frmresult = False
                                TimeToStart = TimeOfDay.AddMinutes(-1)
                            End If
                        Catch ex As Exception
                            TimeToStart = TimeOfDay.AddMinutes(-1)
                        End Try
                        frmSincro.Close()
                End Select
                If (TimeToStart < TimeOfDay) Then
                    'fas tard per sincronitzar		
                    If frmresult = True Then MetroFramework.MetroMessageBox.Show(Me, String.Format(MSG_ERROR_DURADA_SINCRO, TimeSerial(0, 0, TotalDuration).ToString("HH:mm:ss")), MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Error, 120)
                    bl_AutoSinc = False
                    lbAutoSinc.BackColor = Color.Lime
                    mnuAutoSincro59.Checked = False
                    mnuAutoSincro29.Checked = False
                    mnuAutoSincroXX.Checked = False
                    mnuAutoSincro00.Checked = False
                    mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00"
                End If
            Else
                'Calcula hora inici
                Select Case CType(sender, ToolStripMenuItem).Name
                    Case "mnuAutoSincro00"
                        TimeToStart = TimeSerial(TimeOfDay.Hour + 1, 0, 0)
                        HoraEnd = TimeSerial(TimeOfDay.Hour + 1, 0, seg_total)
                        mnuAutoSincro00.Checked = True
                        bl_AutoSinc = True
                    Case "mnuAutoSincroXX"
                        Dim DataSincroEnd As Long
                        Try
                            Dim frmSync As New frmSincro
                            frmSync.isEnd = False
                            frmSync.ShowDialog()
                            If frmSync.DialogResult = System.Windows.Forms.DialogResult.OK Then
                                Dim horafrm As DateTime = CDate(frmSync.txtHoraEnd.Value)
                                TimeToStart = TimeSerial(horafrm.Hour, horafrm.Minute, horafrm.Second)
                                DataSincroEnd = SecondDec(frmSync.txtHoraEnd.Value)
                                HoraEnd = TimeSerial(0, 0, DataSincroEnd + seg_total)
                                mnuAutoSincroXX.Checked = True
                                bl_AutoSinc = True
                            Else
                                TimeToStart = TimeOfDay.AddMinutes(-1)
                            End If
                            frmSync = Nothing
                        Catch ex As Exception
                            TimeToStart = TimeOfDay.AddMinutes(-1)
                        End Try
                End Select
            End If
            mnuAutoFader.Checked = False
            Me.cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS
            mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": " & TimeToStart.ToString("HH:mm:ss")
            lbInfo.Text = LABEL_H_INI & ": " & TimeToStart.ToString("HH:mm:ss") & "/ " & LABEL_H_END & ": " & HoraEnd.ToString("HH:mm:ss")
        Else
            bl_AutoSinc = False
            mnuAutoSincH_PRG.Text = LABEL_HORA_SINCRO & ": 00:00:00"
        End If
        lbInfo.Visible = bl_AutoSinc

    End Sub

    Private Sub mnuLoadVar_Click(sender As Object, e As EventArgs) Handles mnuLoadVar.Click
        LoadParams()
    End Sub

    Private Sub mnuBOXCTL04_Click(sender As Object, e As EventArgs) Handles mnuBOXCTL04.Click
        Me.mnuAirence.Checked = False
        mnuBOXCTL04.Checked = Not mnuBOXCTL04.Checked
        Try
            If mnuBOXCTL04.Checked Then
                IniControlCom()
                'OpenFormControlCTL04()
            Else
                frmRemote.Close()
            End If
        Catch ex As Exception
            mnuBOXCTL04.Checked = False
        End Try
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "CTL04_ACTV", IIf(mnuBOXCTL04.Checked = True, 1, 0))
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "AIRENCE_ACTV", 0)
    End Sub

    Private Sub mnuHelpManual_Click(sender As Object, e As EventArgs) Handles mnuHelpManual.Click
        GoToHelp()
    End Sub

    Private Sub GoToHelp()
        Dim Proces As Process = New Process
        Try
            Process.Start(MSC.Help.HELP_CARTUTXERES.ToString)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub mnuHelpWeb_Click(sender As Object, e As EventArgs) Handles mnuHelpWeb.Click
        Dim Proces As Process = New Process
        Try
            Process.Start(MSC.Help.WEB_DEF_MSC.ToString)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub mnuHelpMail_Click(sender As Object, e As EventArgs) Handles mnuHelpMail.Click
        'TODO: omplir el missatge
        'Dim Texte As String = "Res de res"

        'Dim message As New MailMessage(EMAIL_DEF_MSC, FROM_EMAIL_MSC, Params.NomRadio, Texte)
        'Dim emailClient As New SmtpClient(EMAIL_SERVER_MSC)
        'emailClient.Send(message)
        Dim Proces As Process = New Process

        Dim sParams As String = "mailto:" & MSC.Help.EMAIL_SUPORT.ToString &
            "?subject=" & Params.NomRadio
        'sParams = sParams & "?body=" '& Body

        Try
            Process.Start(sParams)
        Catch
        End Try
    End Sub

    Private Sub mnuHelpAbout_Click(sender As Object, e As EventArgs) Handles mnuHelpAbout.Click
        FrmSplash.Show(Me)
    End Sub

    Private Sub mnuWaveSetIntro_Click(sender As Object, e As EventArgs) Handles mnuWaveSetIntro.Click
        If IsNothing(WF) Then Exit Sub
        'Dim MyPic As PictureBox = CType(sender, PictureBox)
        'Dim len As Long = Bass.BASS_ChannelGetLength(ActualHandle)
        Dim segpix As Double = ActualPlay.DurationInSecons / CType(picWave.Width, Double) ' bytes per pixel        
        'Quin és el pixel on es troba el ratolí

        Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
        'Posició on s'ha de desplaçar        		
        Dim LocationX As Integer = LocalMousePosition.X

        If LocationX < 2 Then LocationX = 2
        ActualPlay.IntroSegons = LocationX * segpix
        WF.AddMarker("INTRO", ActualPlay.IntroSegons)

        DrawWave()
    End Sub

    Private Sub mnuWaveSetPointMix_Click(sender As Object, e As EventArgs) Handles mnuWaveSetPointMix.Click
        If IsNothing(WF) Then Exit Sub

        Dim segpix As Double = ActualPlay.DurationInSecons / CType(picWave.Width, Double) ' bytes per pixel        
        'Quin és el pixel on es troba el ratolí

        Dim LocalMousePosition As Point = picWave.PointToClient(System.Windows.Forms.Cursor.Position)
        'Posició on s'ha de desplaçar        		
        Dim LocationX As Integer = LocalMousePosition.X


        If LocationX > Me.picWave.Width Then LocationX = Me.picWave.Width - 2
        ActualPlay.OutSegons = LocationX * segpix
        WF.AddMarker("END", ActualPlay.OutSegons)
        ManualEnd = True

        DrawWave()
    End Sub

    Private Sub MnuThemeDark_Click(sender As Object, e As EventArgs) Handles MnuThemeDark.Click, MnuThemeLight.Click
        MnuThemeDark.Checked = False
        MnuThemeLight.Checked = False
        CType(sender, ToolStripMenuItem).Checked = True

        If CType(sender, ToolStripMenuItem).Text = "Dark" Then
            MyThemeForm = Formthemes.dark
        Else
            MyThemeForm = Formthemes.light
        End If
        setThemeControls()
        Dim FitxerINI As New IniFile
        FitxerINI.INIWrite(MyAPP.IniFile, "DATA Cartutx", "theme", MyThemeForm)
        'Do the same in the others forms        
        Dim frm As Form
        For Each frm In My.Application.OpenForms
            If frm.Name = "frmCartutxera" Then
                If CType(frm, frmCartutxera).NumCart > 1 Then
                    CType(frm, frmCartutxera).setThemeControls()
                End If
            ElseIf frm.Name = "frmAudioDBS" Then
                CType(frm, frmAudioDBS).setThemeControls()
            ElseIf frm.Name = "frmBuscarTemes" Then
                CType(frm, frmBuscarTemes).setThemeControls()
            ElseIf frm.Name = "frmFileExplorer" Then
                CType(frm, frmFileExplorer).setThemeControls()
            ElseIf frm.Name = "frmHistory" Then
                CType(frm, frmHistory).setThemeControls()
            ElseIf frm.Name = "frmSincro" Then
                CType(frm, frmSincro).setThemeControls()
            ElseIf frm.Name = "frmAddMark" Then
                CType(frm, frmAddMark).setThemeControls()
            ElseIf frm.Name = "frmcanviDurada" Then
                CType(frm, frmCanviDurada).setThemeControls()
            End If
        Next

    End Sub

    Friend Sub setThemeControls()

        If MyThemeForm = Formthemes.dark Then
            Me.Theme = MetroFramework.MetroThemeStyle.Dark
            ToolStripPlayer.ForeColor = Color.DeepSkyBlue
            ToolStripList.ForeColor = Color.DeepSkyBlue
            cmbTypePlayer.ForeColor = Color.DeepSkyBlue
            ToolStripLabel1.ForeColor = Color.DeepSkyBlue
            cmbTypeShow.ForeColor = Color.DeepSkyBlue
            ToolStripLabel3.ForeColor = Color.DeepSkyBlue
            ToolStripLabel4.ForeColor = Color.DeepSkyBlue
            cmdLoopInMenys.ForeColor = Color.DeepSkyBlue
            lbLoopIn.ForeColor = Color.DeepSkyBlue
            cmdLoopInMes.ForeColor = Color.DeepSkyBlue
            ToolStripLabel5.ForeColor = Color.DeepSkyBlue
            cmdLoopOutMenys.ForeColor = Color.DeepSkyBlue
            lbLoopOut.ForeColor = Color.DeepSkyBlue
            cmdLoopOutMes.ForeColor = Color.DeepSkyBlue
            cmdOnAir.ForeColor = Color.DeepSkyBlue

            lbCPU.ForeColor = Color.DeepSkyBlue
            lbInfo.ForeColor = Color.DeepSkyBlue
            Me.lbInfoTrack.ForeColor = Color.DeepSkyBlue
            toolStripLabel2.ForeColor = Color.DeepSkyBlue
            lbTotal.ForeColor = Color.DeepSkyBlue

        Else
            'Formthemes.light 
            Me.Theme = MetroFramework.MetroThemeStyle.Light
            ToolStripPlayer.ForeColor = Color.Black
            ToolStripList.ForeColor = Color.Black
            cmbTypePlayer.ForeColor = Color.Black
            ToolStripLabel1.ForeColor = Color.Black
            cmbTypeShow.ForeColor = Color.Black
            ToolStripLabel3.ForeColor = Color.Black

            ToolStripLabel4.ForeColor = Color.Black
            cmdLoopInMenys.ForeColor = Color.Black
            lbLoopIn.ForeColor = Color.Black
            cmdLoopInMes.ForeColor = Color.Black
            ToolStripLabel5.ForeColor = Color.Black
            cmdLoopOutMenys.ForeColor = Color.Black
            lbLoopOut.ForeColor = Color.Black
            cmdLoopOutMes.ForeColor = Color.Black
            cmdOnAir.ForeColor = Color.Black

            lbCPU.ForeColor = Color.Black
            lbInfo.ForeColor = Color.Black
            Me.lbInfoTrack.ForeColor = Color.Black
            toolStripLabel2.ForeColor = Color.Black
            lbTotal.ForeColor = Color.Black
        End If
        TrackBarBigButtons.BackColor = Me.BackColor
        sldPith.BackColor = Me.BackColor
        TrackBarBigButtons.BackColor = Me.BackColor
        ToolStripPlayer.BackColor = Me.BackColor
        cmbTypePlayer.BackColor = Me.BackColor
        ToolStripLabel1.BackColor = Me.BackColor
        cmbTypeShow.BackColor = Me.BackColor
        ToolStripLabel3.BackColor = Me.BackColor

        ToolStripLabel4.BackColor = Me.BackColor
        cmdLoopInMenys.BackColor = Me.BackColor
        lbLoopIn.BackColor = Me.BackColor
        cmdLoopInMes.BackColor = Me.BackColor
        ToolStripLabel5.BackColor = Me.BackColor
        cmdLoopOutMenys.BackColor = Me.BackColor
        lbLoopOut.BackColor = Me.BackColor
        cmdLoopOutMes.BackColor = Me.BackColor

        lbCPU.BackColor = Me.BackColor
        lbInfo.BackColor = Me.BackColor
        toolStripLabel2.BackColor = Me.BackColor
        lbTotal.BackColor = Me.BackColor
        GetWaveForm(ActualPlay.AUDIO_Path, ActualPlay.gain_factor)
        Me.Refresh()
    End Sub

    Private Sub cmdLoopOutMes_Click(sender As Object, e As EventArgs) Handles cmdLoopOutMes.Click
        If cmdLoopOut.BackColor <> Color.Lime Then Exit Sub
        ActualPlay.LoopOut += 2000
        If ActualPlay.LoopOut > ActualPlay.DurationTime Then ActualPlay.LoopOut = ActualPlay.DurationTime
        refreshLoopLabels()
        cmdSaveLoop.Enabled = Not STANDALONE
    End Sub

    Private Sub cmdLoopOutMenys_Click(sender As Object, e As EventArgs) Handles cmdLoopOutMenys.Click
        If cmdLoopOut.BackColor <> Color.Lime Then Exit Sub
        ActualPlay.LoopOut -= 2000
        If ActualPlay.LoopOut < 0 Then ActualPlay.LoopOut = 0
        refreshLoopLabels()
        cmdSaveLoop.Enabled = Not STANDALONE
    End Sub

    Private Sub mnuPreEscoltaStop_Click(sender As Object, e As EventArgs) Handles mnuPreEscoltaStop.Click
        PlayerPre.StopAudio()
    End Sub

    Private Sub mnuContextPlay(sender As Object, e As EventArgs) Handles ToolStripTextBox1.Click
        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
        Dim MyTile As MetroFramework.Controls.MetroTile
        If cms.SourceControl.GetType.Name = "MetroTile" Then
            MyTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
        Else
            MyTile = CType(cms.SourceControl.Parent, MetroFramework.Controls.MetroTile)
        End If

        Dim IndexLlistat As Integer = flowBotons.Controls.GetChildIndex(MyTile)
        PlayFilePlayer(IndexLlistat)
    End Sub

    Private Sub mnuSetPoxPlay_Click(sender As Object, e As EventArgs) Handles mnuSetPoxPlay.Click
        cmbTypePlayer.SelectedIndex = TypePlay.PLAY_CUNTINUOS
        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
        Dim MyTile As MetroFramework.Controls.MetroTile
        If cms.SourceControl.GetType.Name = "MetroTile" Then
            MyTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
        Else
            MyTile = CType(cms.SourceControl.Parent, MetroFramework.Controls.MetroTile)
        End If

        'ResetNextPlay()

        Dim IndexLlistat As Integer = flowBotons.Controls.GetChildIndex(MyTile)
        SetPoxPlay(IndexLlistat)
    End Sub

    Private Sub mnuSelectUnselect_Click(sender As Object, e As EventArgs) Handles mnuSelectUnselect.Click
        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
        Dim MyTile As MetroFramework.Controls.MetroTile
        If cms.SourceControl.GetType.Name = "MetroTile" Then
            MyTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
        Else
            MyTile = CType(cms.SourceControl.Parent, MetroFramework.Controls.MetroTile)
        End If

        CType(MyTile.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = Not CType(MyTile.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked

    End Sub

    Private Sub mnuContextDel_Click(sender As Object, e As EventArgs) Handles mnuContextDel.Click
        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
        'Dim MyTile As MetroFramework.Controls.MetroTile
        'If cms.SourceControl.GetType.Name = "MetroTile" Then
        '    MyTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
        'Else
        '    MyTile = CType(cms.SourceControl.Parent, MetroFramework.Controls.MetroTile)
        'End If
        'DeleteButtton(MyTile)

        If cms.SourceControl.GetType.Name = "MetroTile" Then
            Click_Borrar()
        End If
    End Sub

    Private Sub frmCartutxera_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.Modifiers
            Case Keys.Control
                Select Case e.KeyCode
                    Case Keys.N : OpenNewPlayer()
                    Case Keys.P
                        'Dim frmExplore As New frmFileExplorer
                        'frmExplore.Show()
                    Case Keys.D : OpenExplorerDBS()
                    Case Keys.O : LoadAllFitxers()
                    Case Keys.S : salvarLlistat()
                    Case Keys.C : SetContinuousOrStepPlayer()
                    Case Keys.L : SetUnsetTypeLoop()
                    Case Keys.E : EditAudiWave()
                    Case Keys.Up : MoveButton(True)
                    Case Keys.Down : MoveButton(False)
                End Select
            Case Keys.Alt
                Select Case e.KeyCode
                    Case Keys.Up : UP_Pitch()
                    Case Keys.Down : DOWN_Pitch()
                End Select
            Case Keys.Shift
                Select Case e.KeyCode
                    Case Keys.F5
                        Try
                            Dim PathFitxer As String = ""
                            For i As Integer = 0 To flowBotons.Controls.Count - 1
                                If CType(flowBotons.Controls(i).Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = True Then
                                    PathFitxer = flowBotons.Controls(i).Controls(ControlsInButton._LabelPath).Text
                                    PlayCue(PathFitxer)
                                    Exit For
                                End If
                            Next
                        Catch ex As Exception
                        End Try
                    Case Keys.F7 : PlayerPre.StopAudio()
                End Select
            Case Else
                Select Case e.KeyCode
                    Case Keys.F1 : GoToHelp()
                    Case Keys.F5 : SetPlayPause()
                    Case Keys.F7 : StopFilePlayer()
                    Case Keys.Delete : Click_Borrar()
                        'Case Keys.Enter : SetPlayPause()
                End Select
        End Select
    End Sub

    Private Sub pichToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles pichToolStripMenuItem.Click
        UP_Pitch()
    End Sub

    Private Sub pichToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles pichToolStripMenuItem1.Click
        DOWN_Pitch()
    End Sub

    Private Sub mnuSetLoad_Click(sender As Object, e As EventArgs) Handles mnuSetLoad.Click
        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)
        Dim MyTile As MetroFramework.Controls.MetroTile
        If cms.SourceControl.GetType.Name = "MetroTile" Then
            MyTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
        Else
            MyTile = CType(cms.SourceControl.Parent, MetroFramework.Controls.MetroTile)
        End If
        Dim IndexLlistat As Integer = flowBotons.Controls.GetChildIndex(MyTile)
        LoadFilePlayer(IndexLlistat)
    End Sub

    Private Sub ContextMenuStripBotoDret_Opening(sender As Object, e As CancelEventArgs) Handles ContextMenuStripBotoDret.Opening
        'Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(sender, ContextMenuStrip)
        Dim MyTile As MetroFramework.Controls.MetroTile
        If cms.SourceControl.GetType.Name = "MetroTile" Then
            MyTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
        Else
            MyTile = CType(cms.SourceControl.Parent, MetroFramework.Controls.MetroTile)
        End If
        Me.mnuChangeDuration.Visible = CType(MyTile.Controls(ControlsInButton._LabelTipus), MetroFramework.Controls.MetroLabel).Text = Tipus_Play.CTL_URL_STREAM
        Me.mnuEditMark.Visible = CType(MyTile.Controls(ControlsInButton._LabelTipus), MetroFramework.Controls.MetroLabel).Text = Tipus_Play.CTL_META_MARK
    End Sub

    Private Sub mnuChangeDuration_Click(sender As Object, e As EventArgs) Handles mnuChangeDuration.Click
        Try
            Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
            Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)

            Dim MyTile As MetroFramework.Controls.MetroTile
            If cms.SourceControl.GetType.Name = "MetroTile" Then
                MyTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
            Else
                MyTile = CType(cms.SourceControl.Parent, MetroFramework.Controls.MetroTile)
            End If

            frmCanviDurada.ShowDialog()
            If frmCanviDurada.DialogResult = System.Windows.Forms.DialogResult.OK Then
                Dim newDuration As Date = frmCanviDurada.txtDurada.Value
                MyTile.Controls(ControlsInButton._LabelDuration).Text = newDuration.ToString("HH:mm:ss") & " " & LB_DURADA
                MyTile.Controls(ControlsInButton._LabelDuration).Tag = newDuration.ToString("HH:mm:ss")

                Dim idx As Integer = flowBotons.Controls.GetChildIndex(MyTile)
                If idx = ActualPlay.AUDIO_ListID Then
                    ActualPlay.DurationInSecons = SecondDec(newDuration.ToString("HH:mm:ss"))
                    ActualPlay.DurationTime = Bass.BASS_ChannelSeconds2Bytes(ActualPlay.AUDIO_HANDLE, ActualPlay.DurationInSecons)
                    CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).ProgressBarStyle = ProgressBarStyle.Continuous
                    CType(MyTile.Controls(ControlsInButton._ProgressBar), MetroFramework.Controls.MetroProgressBar).Maximum = ActualPlay.DurationInSecons
                End If
                frmCanviDurada.Close()
            End If
        Catch ex As Exception
        End Try

    End Sub

    Private Sub mnuGainVolum_Click(sender As Object, e As EventArgs) Handles mnuGainVolum.Click
        mnuGainVolum.Checked = Not mnuGainVolum.Checked
        If mnuGainVolum.Checked = True Then MetroFramework.MetroMessageBox.Show(Me, "This function is in beta, if you activate it could produce instability on the application", MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Warning, 150)
    End Sub

    Private Sub ChangeTitolStream()
        Static Titol As String = ""
        Dim tagInfo As New AddOn.Tags.TAG_INFO(ActualPlay.AUDIO_Path)
        If BassTags.BASS_TAG_GetFromURL(ActualPlay.AUDIO_HANDLE, tagInfo) Then
            If Titol <> tagInfo.title & " - " & tagInfo.artist Then
                lbDisplayTitol.Text = tagInfo.title & " - " & tagInfo.artist
                Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(ActualPlay.AUDIO_ListID), MetroFramework.Controls.MetroTile)
                MyTile.Controls(ControlsInButton._lbText).Text = lbDisplayTitol.Text
                MyTile.Controls(ControlsInButton._LabelTitol).Text = tagInfo.title
                ActualPlay.AUDIO_Titol = tagInfo.title
                MyTile.Controls(ControlsInButton._LabelSubTitol).Text = tagInfo.artist
                ActualPlay.AUDIO_SubTitol = tagInfo.artist
                Me.Text = StrNumCart & IIf(Len(Me.lbDisplayTitol.Text) > 31, VB.Left(Me.lbDisplayTitol.Text, 31) & "...", Me.lbDisplayTitol.Text)
                Me.Refresh()
                Dim MyImage As Image = tagInfo.PictureGetImage(0)
                If Not IsNothing(MyImage) Then
                    CType(MyTile.Controls(ControlsInButton._PictureBoxCover), PictureBox).Image = MyImage
                End If
                Titol = lbDisplayTitol.Text
                RegistrarPlay(lbDisplayTitol.Text, 0, Tipus_Play.CTL_URL_STREAM)
                frmHistory.addElementlistHistory(ActualPlay)
            End If
        End If
    End Sub

#End Region

#Region "SessionMarks"

    Structure session_marks
        Dim SESSION_ID As Integer
        Dim NOM_PRG As String
        Dim DATE_IN As Date
        Dim DATE_OUT As Date
    End Structure

    Dim Actual_SessionMarks As session_marks


    Private Sub mnuSession_Click(sender As Object, e As EventArgs) Handles mnuSession.Click
        LoadSession()
        If Actual_SessionMarks.SESSION_ID > 0 Then
            MetroFramework.MetroMessageBox.Show(Me, String.Format(MSG_MARKS_PRG, Actual_SessionMarks.NOM_PRG, Actual_SessionMarks.SESSION_ID), MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Information, 150)
        Else
            MetroFramework.MetroMessageBox.Show(Me, MSG_MARKS_NO_SESSION, MSG_ATENCIO, MessageBoxButtons.OK, MessageBoxIcon.Warning, 150)
            'mnuInsertMark.Enabled = False
            mnuSession.Text = LB_SESSION
        End If

    End Sub

    Private Sub LoadSession()
        Dim db As New MSC.dbs(Cloud)
        'todo: buscar-ho per una taula composta prglogger + prg_sessions
        'Dim StrSql As String = "SELECT ses_id, ses_prg FROM prg_sessions WHERE ses_estat=" & SessionsStates.SESSION_STANDBY & " OR ses_estat=" & SessionsStates.SESSION_RECORDING & " ;"
        Dim StrSql As String = "SELECT ses_id, prg_nom ,  log_datereg  as 'date_in',DATE_ADD(log_datereg, interval TIME_TO_SEC(ses_durada) second ) as 'date_out'
                                    FROM 
	                                    prg_sessions 
                                    INNER JOIN 
	                                    prglogger ON ses_id=log_sessio 
                                    INNER JOIN  
	                                    programes ON log_program=prg_id 
                                    WHERE  (ses_estat=" & SessionsStates.SESSION_STANDBY & " or ses_estat=" & SessionsStates.SESSION_RECORDING & ") ORDER BY ses_estat ASC ;"
        Dim dt As DataTable = db.getTable(StrSql) 'Standby o recording
        If dt.Rows.Count > 0 Then
            Actual_SessionMarks.SESSION_ID = CInt(dt.Rows(0)("ses_id"))
            Actual_SessionMarks.NOM_PRG = dt.Rows(0)("prg_nom").ToString
            Actual_SessionMarks.DATE_IN = CDate(dt.Rows(0)("date_in"))
            Actual_SessionMarks.DATE_OUT = CDate(dt.Rows(0)("date_out"))

            mnuSession.Text = Actual_SessionMarks.NOM_PRG & " (" & Actual_SessionMarks.SESSION_ID & ")"
            'mnuInsertMark.Enabled = True
        Else
            Actual_SessionMarks.SESSION_ID = 0
            Actual_SessionMarks.NOM_PRG = ""
            Actual_SessionMarks.DATE_IN = Now.AddYears(-1)
            Actual_SessionMarks.DATE_OUT = Now.AddYears(-1)
        End If
        db = Nothing
    End Sub

    Private Sub mnuInsertMark_Click(sender As Object, e As EventArgs) Handles mnuInsertMark.Click
        Dim frmInsertMark As New frmAddMark

        frmInsertMark.ShowDialog()
        If frmInsertMark.DialogResult = DialogResult.OK Then
            Dim Comment As String = frmInsertMark.txtCommentMark.Text
            addElementlist(Tipus_Play.CTL_META_MARK, LB_MARK, Comment, "mark.mark", Actual_SessionMarks.SESSION_ID, "00:00:00", #12:00:00 AM#, 0)
        End If
        frmInsertMark.Close()
        frmInsertMark.Dispose()
        frmInsertMark = Nothing
    End Sub

    Private Sub CreateMark(Comment As String)
        If Actual_SessionMarks.DATE_OUT < Now() Then LoadSession()
        If Actual_SessionMarks.SESSION_ID = 0 Then LoadSession()
        If Actual_SessionMarks.SESSION_ID > 0 Then
            Dim seconds As Integer = DateDiff(DateInterval.Second, Actual_SessionMarks.DATE_IN, Now)
            Dim StrSql As String = "INSERT INTO marks_session (session_id, mark_second, mark_comment) VALUES ( " & Actual_SessionMarks.SESSION_ID & "," & seconds & ",'" & AddSlashes(Comment) & "');"
            Dim db As New MSC.dbs(Cloud)
            db.New_ID(StrSql)
            db = Nothing
        End If
    End Sub

    Private Sub mnuEditMark_Click(sender As Object, e As EventArgs) Handles mnuEditMark.Click
        Dim myItem As ToolStripMenuItem = CType(sender, ToolStripMenuItem)
        Dim cms As ContextMenuStrip = CType(myItem.Owner, ContextMenuStrip)

        Dim MyTile As MetroFramework.Controls.MetroTile
        If cms.SourceControl.GetType.Name = "MetroTile" Then
            MyTile = CType(cms.SourceControl, MetroFramework.Controls.MetroTile)
        Else
            MyTile = CType(cms.SourceControl.Parent, MetroFramework.Controls.MetroTile)
        End If

        frmAddMark.ShowDialog()
        'TODO: Error al assignar el valor per defecte. 
        'frmAddMark.txtCommentMark.Text = MyTile.Controls(ControlsInButton._LabelSubTitol).Text
        If frmAddMark.DialogResult = System.Windows.Forms.DialogResult.OK Then
            Dim comment As String = frmAddMark.txtCommentMark.Text
            MyTile.Controls(ControlsInButton._LabelSubTitol).Text = comment
            MyTile.Controls(ControlsInButton._lbText).Text = LB_MARK & " - " & comment
            Me.MetroToolTipTrackMoreInfo.SetToolTip(MyTile, LB_MARK & " - " & comment)
        End If
        frmAddMark.Close()
    End Sub


#End Region

    Private Sub flowBotons_Click(sender As Object, e As EventArgs) Handles flowBotons.Click
        If flowBotons.Controls.Count > 0 Then
            flowBotons.Controls(flowBotons.Controls.Count - 1).Select()
        End If
    End Sub

    Private Sub MoveButton(Up As Boolean)
        If ButtonSelectedIndex = -1 Then Exit Sub
        Try
            Dim MyButton As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(ButtonSelectedIndex), MetroFramework.Controls.MetroTile)
            If Up = True Then
                ButtonSelectedIndex -= 1
                If ButtonSelectedIndex < 0 And ActualPlay.isActv <> BASSActive.BASS_ACTIVE_PLAYING Then
                    ButtonSelectedIndex = 0
                    LoadFilePlayer(0)
                End If
                If ActualPlay.AUDIO_ListID = ButtonSelectedIndex Then ActualPlay.AUDIO_ListID += 1
            Else
                ButtonSelectedIndex += 1
                If ButtonSelectedIndex > (flowBotons.Controls.Count - 1) Then ButtonSelectedIndex = flowBotons.Controls.Count - 1
                If ActualPlay.AUDIO_ListID = ButtonSelectedIndex Then ActualPlay.AUDIO_ListID -= 1
            End If
            flowBotons.Controls.SetChildIndex(MyButton, ButtonSelectedIndex)
            For I As Integer = 0 To flowBotons.Controls.Count - 1
                flowBotons.Controls(I).TabIndex = I + 999
            Next
        Catch ex As Exception
        End Try
    End Sub

    Private Sub mnuMoveUp_Click(sender As Object, e As EventArgs) Handles mnuMoveUp.Click
        MoveButton(True)
    End Sub

    Private Sub mnuMoveDown_Click(sender As Object, e As EventArgs) Handles mnuMoveDown.Click
        MoveButton(False)
    End Sub

    Private Sub lbAutoSinc_Click(sender As Object, e As EventArgs) Handles lbAutoSinc.Click
        If bl_AutoSinc = True Then
            bl_AutoSinc = False
            mnuAutoSincroXX.Checked = False
            mnuAutoSincro29.Checked = False
            mnuAutoSincro59.Checked = False
            mnuAutoSincro00.Checked = False
            lbAutoSinc.Cursor = Cursors.Default
            lbAutoSinc.BackColor = Color.Lime
        End If


    End Sub



    Private Sub cmdCue_MouseDown(sender As Object, e As MouseEventArgs) Handles cmdCue.MouseDown
        If e.Button = MouseButtons.Right Then
            'Pause i positioning the track
            PlayerPre.StopAudio()
            'PauseFilePlayer()
            Bass.BASS_ChannelSetPosition(ActualPlay.AUDIO_HANDLE, ActualPlay.CuePosition)
        End If
    End Sub

    Private Sub mnuCheckAll_Click(sender As Object, e As EventArgs) Handles mnuCheckAll.Click
        Static blmark As Boolean = True

        For I As Integer = 0 To flowBotons.Controls.Count - 1
            Try
                Dim MyTile As MetroFramework.Controls.MetroTile = CType(flowBotons.Controls(I), MetroFramework.Controls.MetroTile)
                CType(MyTile.Controls(ControlsInButton._chkSelect), MetroFramework.Controls.MetroCheckBox).Checked = blmark
            Catch ex As Exception
            End Try
        Next
        blmark = Not blmark
    End Sub

    Private Sub mnuLoadAndPlay_Click(sender As Object, e As EventArgs) Handles mnuLoadAndPlay.Click
        mnuLoadAndPlay.Checked = Not mnuLoadAndPlay.Checked
    End Sub

    Private Sub lbTotal_DoubleClick(sender As Object, e As EventArgs) Handles lbTotal.DoubleClick
        RecCalcDuradaAll()
    End Sub



    Private Sub TrackBarBigButtons_Scroll(sender As Object, e As EventArgs) Handles TrackBarBigButtons.Scroll
        Dim MyControl As Control
        For Each MyControl In flowBotons.Controls
            If (TypeOf MyControl Is MetroFramework.Controls.MetroTile) Then
                MyControl.Height = TrackBarBigButtons.Value
                If Me.cmbTypeShow.SelectedIndex = TypeShow.SHOW_BUTTONS Then MyControl.Width = TrackBarBigButtons.Value * 3.7
                Dim MyPicSize As Integer = MyControl.Height - 7
                MyControl.Controls(ControlsInButton._PictureBoxCover).Width = MyPicSize
                MyControl.Controls(ControlsInButton._PictureBoxCover).Height = MyPicSize
                MyControl.Controls(ControlsInButton._PictureBoxCover).Top = MyControl.Height - MyPicSize - 5
                Select Case TrackBarBigButtons.Value
                    Case 30 To 45 : CType(MyControl.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).FontSize = MetroFramework.MetroLabelSize.Small
                    Case 46 To 55 : CType(MyControl.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).FontSize = MetroFramework.MetroLabelSize.Medium
                    Case Else : CType(MyControl.Controls(ControlsInButton._lbText), MetroFramework.Controls.MetroLabel).FontSize = MetroFramework.MetroLabelSize.Tall
                End Select
            End If
        Next MyControl
    End Sub


    Private Sub splitContainer1_Panel1_ClientSizeChanged(sender As Object, e As EventArgs) Handles splitContainer1.Panel1.ClientSizeChanged
        'TrackBarBigButtons.Location = New Point(TrackBarBigButtons.Location.X, ToolStripList.Location.Y)        
    End Sub

    Private Sub cmdOnAir_Click(sender As Object, e As EventArgs) Handles cmdOnAir.Click
        ON_AIR = Not ON_AIR
        If ON_AIR = True Then
            cmdOnAir.Image = PicOnAir.InitialImage
        Else
            cmdOnAir.Image = PicOnAir.ErrorImage
        End If
    End Sub

    Private Sub panel3_Resize(sender As Object, e As EventArgs) Handles panel3.Resize
        picCover.Width = panel3.Width - 275
        picCover.Left = panel3.Width - picCover.Width
    End Sub

    Private Sub buttonDonate_Click(sender As Object, e As EventArgs) Handles buttonDonate.Click
        Try
            Dim my_Proces As Process = New Process
            my_Proces.Start(MSC.Help.WEB_DONATE_MSC.ToString)
        Catch ex As Exception

        End Try
    End Sub
End Class