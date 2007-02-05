Imports System
Imports System.IO
Imports System.Text
Imports str = Microsoft.VisualBasic.Strings
Imports System.Text.RegularExpressions
Public Class dramatizer
    '    Public main.iCurrentClipNumber As Integer
    Public Sub New()
        Try
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            '  Me.Text = Main.sProjectName & " - " & Main.sProgramName + " - " & Main.sProgramVersion
        Catch ex As Exception
            MessageBox.Show("oops 2 " & ex.Message)
        End Try
        ' Add any initialization after the InitializeComponent() call.
        Try
            If Me.lbForwardBackBy.SelectedItem = Nothing Then Me.lbForwardBackBy.SelectedItem = 1
         Catch ex As Exception
            MessageBox.Show("oops 3 " & ex.Message)
        End Try
    End Sub
    Public Sub displayMasterAndVoiceTalentText()
        displayPropertiesOfClip(2)
        '  VoiceTalentText.Show()
        MasterText.Show()
        Main.displayStatusText()
    End Sub
    Public Sub displayPropertiesOfClip(ByVal context)
        Dim temp As String = ""
        MasterText.rtbTextWithContext.Text = ""
        resetColorsOnButtons()
        showClipSizeAndContinued()
        getContextText(context)
        temp = Main.sScript(Main.iCurrentClipNumber)
        temp = formatQcodes(temp)
        Main.identifyOmittedText()
        temp = processRemoveVerseNumbers(temp)
        temp = processRemoveSFMcodes(temp)
        showContext(temp)
        showSpeakerText(temp)
        MasterText.rtbTextWithContext.Text = temp
        showBookChapterVerse()
        fillCharactersAndPromptInComboBoxs(Main.iCurrentClipNumber) ' for current clip number
        showMultipleCharactersOrEdit()
        tbCurrentClipNumber.Text = Main.iCurrentClipNumber
        showVoiceNumber()
    End Sub
    Private Sub resetColorsOnButtons()
        Me.btnUpdate.BackColor = Color.LightGray
        Me.btnForward.BackColor = Color.LightGray
    End Sub
    Private Sub showBookChapterVerse()
        tbBook.Text = Main.sBook(Main.iCurrentClipNumber)
        tbChapter.Text = Main.sChapter(Main.iCurrentClipNumber)
        tbVerse.Text = Main.sVerse(Main.iCurrentClipNumber)
    End Sub
    Private Sub showVoiceNumber()
        If Main.sSpeakerNumber(Main.iCurrentClipNumber) = "" Then
            upDownSpeakerNumber.Value = 0
        Else
            upDownSpeakerNumber.Value = Main.sSpeakerNumber(Main.iCurrentClipNumber)
        End If

    End Sub
    Private Sub getContextText(ByVal context As Int16)
        Dim i As Integer
        Dim temp As String = ""
        If Main.iCurrentClipNumber > context Then
            For i = context To 1 Step -1
                temp = temp + vbCrLf & Main.sCharacter(Main.iCurrentClipNumber - i, 1) & vbCrLf & Main.sScript(Main.iCurrentClipNumber - i)
            Next
            MasterText.rtbContextAbove.Text = temp
        Else
            '    temp = sScript(main.iCurrentClipNumber + i)
        End If

    End Sub
    Private Sub showClipSizeAndContinued()
        Try
            MasterText.tbClipSize.Text = Main.calculateClipSize(Main.sClipSize(Main.iCurrentClipNumber))
            If Main.sContinued(Main.iCurrentClipNumber + 1) > 0 Then
                MasterText.tbContinued.Text = MainMenu.sLocalizationStrings(MainMenu.iSpeechContinuedToNextClip, MainMenu.iLanguageSelected)
                MasterText.tbContinued.BackColor = Color.AliceBlue
            Else
                MasterText.tbContinued.Text = ""
                MasterText.tbContinued.BackColor = Color.LightGray
            End If
        Catch ex As Exception
            ' just ignore
        End Try

    End Sub
    Private Sub showSpeakerText(ByVal temp As String)
        If MasterText.chkbxShowSpeakerText.Checked = True Then
            Main.setFontForRTB()
            '    Main.changeFont()
            VoiceTalentText.rtbText.Text = temp
            VoiceTalentText.Show()
        Else
            VoiceTalentText.Hide()
        End If

    End Sub
    Private Sub showContext(ByVal temp As String)
        If MasterText.chkbxShowContext.Checked = True Then
            MasterText.rtbTextWithContext.Show()
            MasterText.rtbTextOnly.Hide()
            MasterText.rtbContextAbove.Show()
            MasterText.Height = 600
        Else
            MasterText.rtbTextWithContext.Hide()
            MasterText.rtbTextOnly.Text = temp
            MasterText.rtbTextOnly.Show()
            MasterText.rtbContextAbove.Hide()
            MasterText.Height = 410
        End If

    End Sub
    Private Function processRemoveSFMcodes(ByVal temp As String)
        If MasterText.chkbxShowSFMcodes.Checked = True Then
        Else
            temp = removeSFMcodes(temp)
        End If
        Return temp
    End Function
    Private Function processRemoveVerseNumbers(ByVal temp As String)
        If MasterText.chkbxShowVerses.Checked = True Then
        Else
            temp = removeVerseNumber(temp)
        End If
        Return temp
    End Function
    Private Function extractPrompt(ByVal input As String)
        Dim temp As String = Main.regexReplace(input, "(.*?)(\[)(.*?)(\])(.*?)", "$3")
        If input.Length = temp.Length Then
            ' not found
            temp = ""
        Else
            ' found so keep temp value
        End If
        Return temp
    End Function
    Private Function removePrompt(ByVal input As String)
        Dim temp As String = Main.regexReplace(input, "(.*?)(\[)(.*?)(\])(.*?)", "$1$5")
        Return temp
        ' if not found you get whole string
        ' if found you get the [xxx] removed
    End Function
    Private Sub fillCharactersAndPromptInComboBoxs(ByVal iCurrentClip As Integer)
        Dim i As Int16
        cbCharacters.Items.Clear()
        Me.cbCharacters.Text = ""
        Me.cbCharactersEdit.Text = ""
        Me.cbCharacters.DroppedDown = False

        For i = 1 To Main.iNumberOfCharactersInClip(iCurrentClip)
            If cbCharacters.Items.IndexOf(Main.getCharacterShort(Main.sCharacter(iCurrentClip, i))) > -1 Then
                ' already have so do nothing
            Else
                ' add short charact  name
                cbCharacters.Items.Add(Main.getCharacterShort(Main.sCharacter(iCurrentClip, i)))
            End If
        Next
        If Main.sCharacter(iCurrentClip, 0) <> "" Then
            ' human has made a choice stored in 0  (confirmed)
            Me.cbCharacters.DroppedDown = False
            Me.cbCharacters.Text = Main.sCharacter(iCurrentClip, 0)
            Me.cbCharacters.BackColor = Color.LightBlue
        ElseIf Main.iNumberOfCharactersInClip(iCurrentClip) = 0 Then
            ' unidentified and unconfirmed
            Me.cbCharacters.Text = Main.sCharacter(iCurrentClip, 1)
            Me.cbCharacters.BackColor = Color.HotPink
            editCharacter()
        ElseIf Main.iNumberOfCharactersInClip(iCurrentClip) = 1 Then
            ' just one character - so no need to confirm 
            Me.cbCharacters.Text = Main.sCharacter(iCurrentClip, 1)
            Me.cbCharacters.BackColor = Color.LightBlue
        Else
            ' multiple unconfirmed
            Me.cbCharacters.DroppedDown = True
            Me.cbCharacters.Text = Main.sCharacter(iCurrentClip, 1)
            Me.cbCharacters.BackColor = Color.HotPink
        End If
        Me.cbCharacterPrompt.Text = Me.extractPrompt(Me.cbCharacters.Text)
        Me.cbCharacters.Text = Me.removePrompt(Me.cbCharacters.Text)
        Me.cbCharactersEdit.Text = Me.cbCharacters.Text
        Me.cbCharactersEdit.Focus()
    End Sub
    Private Sub btnForward_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForward.Click
        '   Dim myclip As New Clip(main.iCurrentClipNumber, sTextArray, iLastClipNumber)
        '  myclip.goForward(Me.lbForwardBackBy.SelectedItem, iLastClipNumber)
        goForward()
    End Sub
    Private Sub btnBack_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        goBack()
    End Sub
    Private Sub btnStart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        goHome()
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Me.Hide()
        MainMenu.Show()
        MainMenu.setCheckMarksAndEnableMenuItems()
        ' MainMenu.enableMenuChoices()
        'If MainMenu.lblStartProcessing.Visible = True Then
        ' MainMenu.rbInitialize.Focus()
        ' End If
        ' If MainMenu.lblInitialize.Visible = True Then
        ' MainMenu.rbUnidentified.Focus()
        ' End If
        ' If MainMenu.lblUnidentified.Visible = True Then
        ' MainMenu.rbMultiple.Focus()
        ' End If
        ' If MainMenu.lblMultiple.Visible = True Then
        ' MainMenu.rbVerifyUpdated.Focus()
        ' End If

        MasterText.Hide()
        VoiceTalentText.Hide()
        Main.showStatsForUnidentifiedMultipleTotal()
    End Sub
    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        ' set the properties of the current clip to displayed values
        ' character
        updateCharactersFile()
        Me.btnUpdate.BackColor = Color.LightGray
        Me.goForward()
    End Sub
    '  Private Sub cbCharacters_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharacters.SelectedValueChanged
    '     Me.btnUpdate.BackColor = Color.LawnGreen
    'End Sub
    Private Sub cbCharacters_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharacters.Enter
        updateCharactersFile()
        Me.btnForward.BackColor = Color.LightGray
    End Sub
    Private Sub updateCharactersFile()
        If Me.cbCharacters.Text = Nothing Then
            ' skip
        Else
            If Me.cbCharacterPrompt.Text = Nothing Then
                ' 0 is for the human input
                Main.sCharacter(Main.iCurrentClipNumber, 0) = Me.cbCharacters.Text
            Else
                Main.sCharacter(Main.iCurrentClipNumber, 0) = Me.cbCharacters.Text + " [" + Me.cbCharacterPrompt.Text + "]"
            End If
        End If
        ' voice number associated with character
        Main.sSpeakerNumber(Main.iCurrentClipNumber) = Main.assignVoiceToCharacter(Me.cbCharacters.Text)
        ' prompt
        '  Main.sPrompt(Main.iCurrentClipNumber) = Me.cbCharacterPrompt.Text
        Main.writeClipsToMasterFileAndAdjustClipSize(False) ' adjust clip size false
        displayPropertiesOfClip(2)  ' check for redundant xxxxxxxxxxxxxxxx
        Main.displayStatusText()
    End Sub
    Private Sub pressedEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharacters.Enter
        ' pressed enter on the forward back control
        updateCharactersFile()
    End Sub
    ' Private Sub pressedKey(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
    '' not working
    '   Select Case e.KeyCode
    '      Case Keys.Left
    '         goForward()
    '    Case Keys.Right
    '       goBack()
    '  Case Keys.Enter
    '     updateCharactersFile()
    '    Case Keys.Home
    '       goHome()
    '      Case Else
    '' ignore
    '  End Select
    '' pressed enter on the forward back control
    '   updateCharactersFile()
    ' End Sub
    Public Sub goForward()
        If Main.iCurrentClipNumber >= (Main.iLastClipNumber) Then Beep() : Main.iCurrentClipNumber = 0
        ' iLastClipNumber is really 1 over in order to handle splits properly
        ' so don't show the "last" one as it is blank
        Dim i As Integer = Main.iCurrentClipNumber
        Select Case lbForwardBackBy.SelectedIndex
            Case -1, 0, 5 ' "Next clip" "Verify All" "record all" ' can't match string as string changes
                i = skipForward(i)
            Case 1 ' "Unidentified character clip"
                Do
                    i = skipForward(i)
                    If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
                Loop Until Main.iNumberOfCharactersInClip(i) = 0
            Case 2 '"Multiple characters in a clip"
                Do
                    i = skipForward(i)
                    If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
                Loop Until Main.iNumberOfCharactersInClip(i) > 1
            Case 3 '"Verify Updated clip"
                Do
                    i = skipForward(i)
                    If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
                Loop Until Main.sCharacter(i, 0) <> Nothing
            Case 4 ' "Same speaker number" Record
                Do
                    i = skipForward(i)
                    If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
                Loop Until Main.sSpeakerNumber(i) = Me.upDownSpeakerNumber.Value.ToString
            Case Else
                i = skipForward(i)
        End Select
        Main.iCurrentClipNumber = i
        '    If Me.btnForward.BackColor = Color.Lime Then
        '   Me.updateCharactersFile()
        '   Me.btnForward.BackColor = color.lightgray
        '   Else
        displayPropertiesOfClip(2)
        '    End If
    End Sub
    Private Function skipForward(ByVal clipNumber As Integer)
        clipNumber += 1
        If Me.chkbxDisplayOmittedClips.Checked = True Then
            If clipNumber >= Main.iLastClipNumber Then Beep() : clipNumber = 1
        Else
            ' hide omitted clip
            Do Until (Main.blnOmit(clipNumber) = False Or clipNumber = Main.iLastClipNumber)
                clipNumber += 1
                If clipNumber >= Main.iLastClipNumber Then Beep() : clipNumber = 1 : Exit Do
            Loop
        End If
        clipNumber = Me.skipForwardProcessed(clipNumber)
        Return clipNumber
    End Function
    Private Function skipBack(ByVal clipNumber As Integer)
        clipNumber -= 1
        If Me.chkbxDisplayOmittedClips.Checked = True Then
            If clipNumber >= Main.iLastClipNumber Then Beep() : clipNumber = 1
        Else
            ' hide omitted clip
            Do Until (Main.blnOmit(clipNumber) = False Or clipNumber = Main.iLastClipNumber)
                clipNumber -= 1
                If clipNumber <= 1 Then Beep() : clipNumber = 1 : Exit Do
            Loop
        End If
        clipNumber = Me.skipBackProcessed(clipNumber)
        Return clipNumber
    End Function
    Private Function skipForwardProcessed(ByVal clipNumber)
        If Me.chkbxDisplayUnprocessedOnly.Checked = True Then
            If Main.sCharacter(clipNumber, 0) = Nothing Then
                '  not processed
            Else
                ' processed
                clipNumber += 1
            End If
        Else
            ' show all
        End If
        Return clipNumber
    End Function
    Private Function skipBackProcessed(ByVal clipNumber)
        If Me.chkbxDisplayUnprocessedOnly.Checked = True Then
            If Main.sCharacter(clipNumber, 0) = Nothing Then
                '  not processed
            Else
                ' processed
                clipNumber -= 1
            End If
        Else
            ' show all
        End If
        Return clipNumber
    End Function
    Private Sub goBack()
        Me.btnUpdate.BackColor = Color.LightGray
        Me.btnForward.BackColor = Color.LightGray
        If Main.iCurrentClipNumber = 1 Then Main.iCurrentClipNumber = Main.iLastClipNumber + 1
        Dim i As Integer = Main.iCurrentClipNumber
        Select Case lbForwardBackBy.SelectedIndex
            Case -1, 0 ' "Next clip" "Verify All"
                i = skipBack(i)
            Case 1 ' "Unidentified character clip"
                Do
                    i = skipBack(i)
                    If i <= 1 Then Beep() : i = 1 : Exit Do
                Loop Until Main.iNumberOfCharactersInClip(i) = 0
            Case 2 '"Multiple characters in a clip"
                Do
                    i = skipBack(i)
                    If i <= 1 Then Beep() : i = 1 : Exit Do
                Loop Until Main.iNumberOfCharactersInClip(i) > 1
            Case 3 '"Verify Updated clip"
                Do
                    i = skipBack(i)
                    If i <= 1 Then Beep() : i = 1 : Exit Do
                Loop Until Main.sCharacter(i, 0) <> Nothing
            Case 4 ' "Same speaker number"
                Do
                    i = skipBack(i)
                    If i <= 1 Then Beep() : i = 1 : Exit Do
                Loop Until Main.sSpeakerNumber(i) = Me.upDownSpeakerNumber.Value.ToString
            Case Else
                i = skipBack(i)
        End Select
        Main.iCurrentClipNumber = i
        displayPropertiesOfClip(2)
    End Sub
    Private Sub goHome()
        Me.btnUpdate.BackColor = Color.LightGray
        Me.btnForward.BackColor = Color.LightGray
        Main.iCurrentClipNumber = 1
        displayPropertiesOfClip(2)
    End Sub
    Private Sub cbCharacters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharacters.SelectedIndexChanged
        Me.btnUpdate.BackColor = Color.LawnGreen
    End Sub
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        editCharacter()
    End Sub
    Public Sub editCharacter()
        '   Me.cbCharacters.Visible = False
        Me.cbCharacters.DroppedDown = False
        Me.cbCharactersEdit.Show()
        Me.cbCharactersEdit.Visible = True
        Me.cbCharactersEdit.Focus()
    End Sub
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        End
    End Sub
    Public Function removeVerseNumber(ByVal temp)
        temp = Main.regexReplace(temp, "(<verse>)(.*?)(</verse>)", " ")
        Return temp
    End Function
    Public Function formatQcodes(ByVal temp)
        temp = Main.regexReplace(temp, "(\\q)(\s|(\<))", vbCrLf & "    $1$2$3")
        temp = Main.regexReplace(temp, "(\\q1)(\s|(\<))", vbCrLf & "    $1$2$3")
        temp = Main.regexReplace(temp, "(\\q2)(\s|(\<))", vbCrLf & "        $1$2$3")
        temp = Main.regexReplace(temp, "(\\q3)(\s|(\<))", vbCrLf & "            $1$2$3")
        temp = Main.regexReplace(temp, "(\\q)($)", vbCrLf & "     $1 ")
        temp = Main.regexReplace(temp, "(\\p)($)", vbCrLf & "     $1 ")
        temp = Main.regexReplace(temp, "(<<verse>)", "<verse>")
        Return temp
    End Function
    Public Function removeSFMcodes(ByVal temp)
        temp = Main.regexReplace(temp, "(\\.*?)(\s|(\<)|" & vbCrLf & ")", " $3")
        Return temp
    End Function
    Private Sub tbChapter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbChapter.TextChanged
        ' take chapter number and move to chapter number verse 1'
        '   Dim i As Integer
        '  For i = 1 To Main.iLastClipNumber
        'If Main.sBook(i) = tbBook.Text And _
        ' Main.sChapter(i) = tbChapter.Text Then
        ' Main.iCurrentClipNumber = i
        ' Me.displayPropertiesOfClip(2)
        ' Exit Sub
        ' End If
        ' Next
        ' MessageBox.Show("The chapter number was not found in the current book: " & tbBook.Text & " " & tbChapter.Text, "Chapter error", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnRecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecord.Click
        ' result should be   XXX-000-00000 where XXX is ISO code and -000- is book and -00000- is sequence
        ' save tag id
        ' tag='\id'
        Dim startTime, endTime
        Dim i As Integer = Main.iCurrentClipNumber
        'Dim sequence As String = sPadNumber(5, Me.tbCurrentClipNumber.Text)
        'Dim temp As String = Main.sRecordingFolder & "\" & Main.tbISOcode.Text
        Dim tempRecordingFolder As String = Main.sRecordingFolder & "\" & Main.tbISOcode.Text & "-"
        Dim tempC As String = "c:\" & Main.tbISOcode.Text & "-"
        Dim bookNumber As String = Main.getBookNumber(i)
        Dim sequence As String = sPadNumber(5, i.ToString)
        Dim tempWaveFile As String = tempRecordingFolder & bookNumber & sequence & ".wav"
        Dim tempCWaveFile As String = tempC & bookNumber & sequence & ".wav"
        Try
            File.Move(tempWaveFile, tempCWaveFile)
            startTime = File.GetCreationTime(tempCWaveFile)
            Shell(Main.cbAudioProgram.Text + " " + "" + tempCWaveFile + "", AppWinStyle.NormalFocus, False)
            Do
                endTime = File.GetLastWriteTime(tempCWaveFile)
            Loop Until endTime > startTime
            File.Move(tempCWaveFile, tempWaveFile)
        Catch ex As Exception
            MessageBox.Show("Possibly a missing wave file. " & vbCrLf & ex.Message, "Missing file?", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Function sPadNumber(ByVal i As Integer, ByVal temp As String)
        Return temp.PadLeft(i, "0")
    End Function

    Private Sub dramatizer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MainMenu.rbRecord.Checked Then
            Me.btnRecord.Visible = True
            Me.chkbxDisplayUnprocessedOnly.Text = MainMenu.sLocalizationStrings(MainMenu.iDisplayUnrecordedClipsOnly, MainMenu.iLanguageSelected)
            Me.chkbxRecordOneSpeakerAtATime.Visible = True
            Me.tbSpeakerNumber.Visible = True
        Else
            Me.btnRecord.Visible = False
            Me.chkbxDisplayUnprocessedOnly.Text = MainMenu.sLocalizationStrings(MainMenu.iDisplayUnprocessedClipsOnly, MainMenu.iLanguageSelected)
            Me.chkbxRecordOneSpeakerAtATime.Visible = False
            Me.tbSpeakerNumber.Visible = False

        End If
    End Sub

    Private Sub chkbxRecordOneSpeakerAtATime_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxRecordOneSpeakerAtATime.CheckedChanged
        If Me.chkbxRecordOneSpeakerAtATime.Checked = True Then
            Me.tbSpeakerNumber.Visible = True
        Else
            Me.tbSpeakerNumber.Visible = False

        End If
    End Sub

    Private Sub chkbxShowPrompt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxShowPrompt.CheckedChanged
        If Me.chkbxShowPrompt.Checked = True Then
            Me.lblCharacterPrompt.Visible = True
            Me.cbCharacterPrompt.Visible = True
        Else
            Me.cbCharacterPrompt.Visible = False
            Me.lblCharacterPrompt.Visible = False

        End If
    End Sub

    Private Sub cbCharactersEdit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharactersEdit.SelectedIndexChanged
        Me.cbCharacters.Text = Me.cbCharactersEdit.Text
        Me.btnUpdate.BackColor = Color.LawnGreen
    End Sub
    Private Sub showMultipleCharactersOrEdit()
        If Main.iNumberOfCharactersInClip(Main.iCurrentClipNumber) > 1 Then
            'Me.cbCharacters.DroppedDown = True
            '  Me.cbCharacters.DroppedDown = False
            Me.cbCharacters.BackColor = Color.AntiqueWhite
        Else
            ' Me.cbCharacters.DroppedDown = False
            Me.cbCharacters.BackColor = Color.AliceBlue
        End If
        If Main.iNumberOfCharactersInClip(Main.iCurrentClipNumber) = 0 Then
            ' Me.cbCharacters.DroppedDown = False
            Me.cbCharacters.BackColor = Color.AliceBlue
            Me.cbCharactersEdit.Visible = True
            Me.btnEdit.Visible = False
        Else
            Me.cbCharactersEdit.Visible = False
            Me.btnEdit.Visible = True
        End If
    End Sub

    Private Sub chkbxDisplayUnprocessedOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxDisplayUnprocessedOnly.CheckedChanged
        If Me.chkbxDisplayUnprocessedOnly.Checked = True Then
            Me.chkbxDisplayOmittedClips.Checked = False
        Else
            ' do nothing
        End If

    End Sub

    Private Sub chkbxDisplayUnrecordedOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub chkbxDisplayOmittedClips_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxDisplayOmittedClips.CheckedChanged
        If Me.chkbxDisplayOmittedClips.Checked = True Then
            Me.chkbxDisplayUnprocessedOnly.Checked = False

        Else
            ' do nothing
        End If
    End Sub

    Private Sub btnNotAQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotAQuote.Click
        Me.cbCharacters.Text = "Not a quote"
    End Sub
End Class
