Imports System
Imports System.IO
Imports System.Text
Imports str = Microsoft.VisualBasic.Strings
Imports System.Text.RegularExpressions
Public Class dramatizer
    Public blnMoreOrLess As Boolean = True
    Public blnMoveDown As Boolean = True
    Public tempSourceFile
    '    Public main.iCurrentClipNumber As Integer
    Public Sub New()
        Try
            ' This call is required by the Windows Form Designer.
            InitializeComponent()
            '  Me.Text = Main.sProjectName & " - " & Main.sProgramName + " - " & Main.sProgramVersion

            Me.Update()
        Catch ex As Exception
            MessageBox.Show("oops 2 " & ex.Message)
        End Try
        ' Add any initialization after the InitializeComponent() call.
        Try
            showLessOptions()
            '        If Me.lbForwardBackBy.SelectedItem = Nothing Then Me.lbForwardBackBy.SelectedItem = 1
        Catch ex As Exception
            MessageBox.Show("oops 3 " & ex.Message)
        End Try
    End Sub
    Public Sub displayMasterAndVoiceTalentText()
        '  VoiceTalentText.Show()
        MasterText.Show()
        Main.displayStatusText()
        displayPropertiesOfClip(2) ' 2007-07-16 tried placing this last ... drop down to stay dropped down in multiple
    End Sub
    Public Sub displayPropertiesOfClip(ByVal context)
        Dim temp As String = ""
        Me.Update()
        MasterText.rtbTextWithContext.Text = ""
        MasterText.rtbContextAbove.Text = ""
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
        showSpeakerNumber()
    End Sub
    Private Sub resetColorsOnButtons()
        '    Me.btnUpdate.BackColor = Color.LightGray
        Me.btnForward.BackColor = Color.LightGray
    End Sub
    Private Sub showBookChapterVerse()
        tbBook.Text = Main.sBook(Main.iCurrentClipNumber)
        tbChapter.Text = Main.sChapter(Main.iCurrentClipNumber)
        tbVerse.Text = Main.sVerse(Main.iCurrentClipNumber)
    End Sub
    Private Sub getAndShowSpeakerNumberForCharacter()
        Dim speaker As Integer
        speaker = Main.assignSpeakerToCharacter(Me.cbCharactersEdit.Text)
        Me.upDownSpeakerNumber.Value = speaker

    End Sub
    Private Sub showSpeakerNumber()
        If Main.sSpeakerNumber(Main.iCurrentClipNumber) = "" Then
            upDownSpeakerNumber.Value = 0
        Else
            upDownSpeakerNumber.Value = Main.sSpeakerNumber(Main.iCurrentClipNumber)
        End If
        Me.upDownSpeakerNumber.Update()

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
                MasterText.tbContinued.Text = Main.sLocalizationStrings(Main.iSpeechContinuedToNextClip, Main.iLanguageSelected)
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
            SpeakerText.rtbText.Text = temp
            SpeakerText.Show()
        Else
            SpeakerText.Hide()
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
            MasterText.Height = 384
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
        Me.cbCharacterPrompt.Text = ""
        Me.cbCharacters.DroppedDown = False
        For i = 1 To Main.iNumberOfCharactersInClip(iCurrentClip)
            If cbCharacters.Items.IndexOf(Main.getCharacterShort(Main.sCharacter(iCurrentClip, i))) > -1 Then
                ' already have so do nothing
            Else
                ' add short character  name
                cbCharacters.Items.Add(Main.getCharacterShort(Main.sCharacter(iCurrentClip, i)))
            End If
        Next
        If Main.sCharacter(iCurrentClip, 0) <> "" Then
            ' human has made a choice stored in 0  (confirmed)
            Me.cbCharacters.DroppedDown = False
            Me.cbCharacters.Text = Main.sCharacter(iCurrentClip, 0)
            Me.cbCharacters.BackColor = Color.LawnGreen
            Me.cbCharactersEdit.BackColor = Color.LawnGreen
            Me.btnForward.BackColor = Color.LightGray
        ElseIf Main.iNumberOfCharactersInClip(iCurrentClip) = 0 Then
            ' unidentified and unconfirmed
            Me.cbCharacters.Text = Main.sCharacter(iCurrentClip, 1)
            Me.cbCharacters.BackColor = Color.HotPink
            Me.cbCharactersEdit.BackColor = Color.HotPink
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
        If Main.iCurrentClipNumber = 1 Then
            Me.btnForward.BackColor = Color.LightGray
        Else
            ' keep current color
        End If
    End Sub
    Private Function skipForwardIfOmittedProcessedOrRecorded(ByVal clipNumber As Integer)
        ' before changing anything here check for omit bug

        ' main reason is checked for in the case statement
        ' additional reasons to skip this clip are
        ' 1 omitted text and show omitted not checked
        clipNumber += 1
        clipNumber = skipForwardOverOmitted(clipNumber)
        ' 2 display only clips to process is checked
        If Me.chkbxDisplayOnlyClipsToProcess.Checked = True Then
            ' to process or to record
            ' 2a recording
            If MainMenu.rbRecord.Checked = True Then
                ' recording
                ' 2a loop until this clip is to be recorded
                clipNumber = skipForwardOverRecorded(clipNumber)
            Else
                ' not recording
                ' 2b loop until this clip is to be processed
                clipNumber = skipForwardOverProcessed(clipNumber)
            End If
        Else
            ' show all
        End If

        '   clipNumber = Me.skipForwardProcessed(clipNumber)
        Return clipNumber
    End Function
    Private Function skipForwardOverProcessed(ByVal clipNumber As Integer)
        Do Until Main.sCharacter(clipNumber, 0) = Nothing
            clipNumber += 1
            If clipNumber >= Main.iLastClipNumber Then Beep() : clipNumber = 1 : Exit Do
        Loop
        Return clipNumber
    End Function
    Private Function skipBackOverProcessed(ByVal clipNumber As Integer)
        Do Until Main.sCharacter(clipNumber, 0) = Nothing
            clipNumber -= 1
            If clipNumber = 0 Then Beep() : clipNumber = Main.iLastClipNumber : Exit Do
        Loop
        Return clipNumber
    End Function
    Private Function skipForwardOverRecorded(ByVal clipNumber As Integer)
        Do Until Main.blnRecorded(clipNumber) = False
            clipNumber += 1
            If clipNumber >= Main.iLastClipNumber Then Beep() : clipNumber = 1 : Exit Do
        Loop
        Return clipNumber
    End Function
    Private Function skipBackOverRecorded(ByVal clipNumber As Integer)
        Do Until Main.blnRecorded(clipNumber) = False
            clipNumber -= 1
            If clipNumber = 0 Then Beep() : clipNumber = Main.iLastClipNumber : Exit Do
        Loop
        Return clipNumber
    End Function
    Private Function skipForwardOverOmitted(ByVal clipNumber As Integer)
        If Me.chkbxDisplayOmittedClips.Checked = True Then
            ' showing ommittede clips
            If clipNumber >= Main.iLastClipNumber Then Beep() ': clipNumber = 1
        Else
            ' skip over any omitted clips
            Do Until (Main.blnOmit(clipNumber) = False) ' Or clipNumber = Main.iLastClipNumber)
                clipNumber += 1
                If clipNumber >= Main.iLastClipNumber Then Beep() : Exit Do ' clipNumber = 1
            Loop
        End If
        Return clipNumber
    End Function
    Private Function skipBackOverOmitted(ByVal clipNumber As Integer)
        If clipNumber < 1 Then Beep() : clipNumber = Main.iLastClipNumber
        If Me.chkbxDisplayOmittedClips.Checked = True Then
            ' showing ommittede clips
            If clipNumber = 0 Then Beep() : clipNumber = Main.iLastClipNumber
        Else
            ' skip over any omitted clips
            Do Until (Main.blnOmit(clipNumber) = False) ' Or clipNumber = Main.iLastClipNumber)
                clipNumber -= 1
                If clipNumber < 1 Then Beep() : clipNumber = Main.iLastClipNumber : Exit Do
            Loop
        End If
        Return clipNumber
    End Function

    Private Function skipBack(ByVal clipNumber As Integer)
        clipNumber -= 1
        If Me.chkbxDisplayOmittedClips.Checked = True Then
            If clipNumber >= Main.iLastClipNumber Then Beep() : clipNumber = 1
        Else
            ' hide omitted clip
            If clipNumber <= 1 Then Beep() : clipNumber = 1 : Return clipNumber
            Do Until (Main.blnOmit(clipNumber) = False Or clipNumber = Main.iLastClipNumber)
                clipNumber -= 1
                If clipNumber <= 1 Then Beep() : clipNumber = 1 : Exit Do
            Loop
        End If
        clipNumber = Me.skipBackProcessed(clipNumber)
        Return clipNumber
    End Function
    Private Function skipForwardProcessed(ByVal clipNumber)
        If MainMenu.rbRecord.Checked = True Then
            ' recording
            If Me.chkbxDisplayOnlyClipsToProcess.Checked = True Then ' unprocessed = unrecorded
                ' hide omitted clip
                'If  Then
                ' '  not processed
                ' Else
                '    ' processed so skip forward
                '   clipNumber += 1
            End If
        Else
            ' show all
        End If
        '     Else
        '        ' not recording
        '   End If
        Return clipNumber
    End Function
    Private Function skipBackProcessed(ByVal clipNumber)
        If Me.chkbxDisplayOnlyClipsToProcess.Checked = True Then
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

    Private Sub goHome()
        '   Me.btnUpdate.BackColor = Color.LightGray
        Me.btnForward.BackColor = Color.LightGray
        Main.iCurrentClipNumber = 1
        displayPropertiesOfClip(2)
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
        temp = Main.regexReplace(temp, "(\\.[1-3]?)", "")
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
        ' Dim startTime, endTime
        Dim i As Integer = Main.iCurrentClipNumber
        If i = 1 Then
            Beep()
            ' skip #1
        Else
            Dim isoCode As String = "\" & Main.tbISOcode.Text & "-"
            Dim bookNumber As String = Main.getBookNumber(i)
            Dim sequence As String = sPadNumber(5, i.ToString)
            Dim tempWaveFileStart As String = isoCode + bookNumber + sequence
            '    Dim tempSourceFile As String
            '      Dim tempOutputFile As String
            '   Dim tempCWaveFile As String
            Dim blnTagFound As Boolean = False
            Dim extra As String = ""
            '     createTempRecordingFolder()
            blnTagFound = str.InStr(Main.sTag(i), "\")
            extra = Main.justTagOrOmit(i) ' empty if no tag
            tempSourceFile = Main.sRecordingFolder + tempWaveFileStart + extra + ".wav"
            'tempOutputFile = Main.sTempRecordingInProgressFolder + tempWaveFileStart + extra + ".wav"
            If File.Exists(tempSourceFile) Then
                ' good
                ' program to launch
                Dim startInfo As New ProcessStartInfo(Main.cbAudioProgram.Text)
                ' file to open
                startInfo.Arguments = """" + tempSourceFile + """"
                Process.Start(startInfo)
                ' removed loop here as it slows everything down
                ' use event instead
            Else
                ' missing file
                MessageBox.Show("Missing wave file. This should not happen unless someone deleted files accidently. You will need to recreate the .wav files." & vbCrLf & tempSourceFile, "Missing file", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End If
    End Sub
    Public Function sPadNumber(ByVal i As Integer, ByVal temp As String)
        Return temp.PadLeft(i, "0")
    End Function
    Private Sub dramatizer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If MainMenu.rbRecord.Checked Then
            Me.btnRecord.Visible = True
            Me.chkbxDisplayOnlyClipsToProcess.Text = Main.sLocalizationStrings(Main.iDisplayUnrecordedClipsOnly, Main.iLanguageSelected)
            '       Me.tbSpeakerNumber.Visible = True
        Else
            Me.btnRecord.Visible = False
            Me.chkbxDisplayOnlyClipsToProcess.Text = Main.sLocalizationStrings(Main.iDisplayUnprocessedClipsOnly, Main.iLanguageSelected)
            '      Me.tbSpeakerNumber.Visible = False
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
    Private Sub showMultipleCharactersOrEdit()
        If Main.iNumberOfCharactersInClip(Main.iCurrentClipNumber) > 1 Then
            Me.cbCharacters.DroppedDown = True
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
    End Sub ' drop down working to here
    Private Sub chkbxDisplayUnrecordedOnly_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub btnNotAQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNotAQuote.Click
        Me.cbCharacters.Text = Main.sNotAQuote
        Me.cbCharactersEdit.Text = Main.sNotAQuote
        Me.btnForward.BackColor = Color.LawnGreen
        Me.cbCharactersEdit.BackColor = Color.LawnGreen
    End Sub
    Private Sub btnMoreOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoreOptions.Click
        ' show more
        Me.btnLessOptions.Show()
        Me.Height = 433
        Me.Panel2.Show()
        Me.btnMoreOptions.Hide()
    End Sub
    Private Sub btnLessOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLessOptions.Click
        ' show less
        showLessOptions()
    End Sub
    Private Sub showLessOptions()
        Me.btnMoreOptions.Show()
        Me.Height = 300
        Me.Panel2.Hide()
        Me.btnLessOptions.Hide()
    End Sub
    Private Sub lblDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblDisplay.Click
    End Sub
    Private Sub chkbxDisplayOmittedClips_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxDisplayOmittedClips.CheckedChanged
        If Me.chkbxDisplayOmittedClips.Checked = True Then
            Me.chkbxDisplayOnlyClipsToProcess.Checked = False
        Else
            ' do nothing
        End If
    End Sub
    Private Sub chkbxDisplayOnlyClipsToProcess_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxDisplayOnlyClipsToProcess.CheckedChanged
        If Me.chkbxDisplayOnlyClipsToProcess.Checked = True Then
            Me.chkbxDisplayOmittedClips.Checked = False
        Else
            ' do nothing
        End If
    End Sub
    Public Sub goForward()
        ifRecordingDidWeWriteNewFile(Main.iCurrentClipNumber)

        If Main.iCurrentClipNumber >= (Main.iLastClipNumber) Then Beep() : Main.iCurrentClipNumber = 0
        Dim i As Integer = Main.iCurrentClipNumber
        ' iLastClipNumber is really 1 over in order to handle splits properly
        ' so don't show the "last" one as it is blank
        If Me.rbAll.Checked = True Then
            ' "Next clip" "Verify All" "record all" ' can't match string as string changes
            i = skipForwardIfOmittedProcessedOrRecorded(i)
        ElseIf Me.rbUnidentified.Checked = True Then
            '"Unidentified character clip"
            Do
                i = skipForwardIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.iNumberOfCharactersInClip(i) = 0
        ElseIf Me.rbMultiple.Checked = True Then
            '"Multiple characters in a clip"
            Do
                i = skipForwardIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.iNumberOfCharactersInClip(i) > 1
        ElseIf Me.rbUpdated.Checked = True Then
            '"Verify Updated clip"
            Do
                i = skipForwardIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.sCharacter(i, 0) <> Nothing
        ElseIf Me.rbSpeaker.Checked Then
            ' "Same speaker number" Record
            Do
                i = skipForwardIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.sSpeakerNumber(i) = Me.upDownSpeakerNumber.Value.ToString
        ElseIf Me.rbSpeaker.Checked = True Then
            Do
                i = skipForwardIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.sSpeakerNumber(i) = Me.upDownSpeakerNumber.Value.ToString
        Else
            i = skipForwardIfOmittedProcessedOrRecorded(i)
        End If
        ' Main.writeClipsToMasterFileAndAdjustClipSize(False) -- not necessary every time we go forward ... just when background has changed and we update master file then
        Main.displayStatusText()
        Main.iCurrentClipNumber = i
        displayPropertiesOfClip(2)
    End Sub
    Private Sub ifRecordingDidWeWriteNewFile(ByVal i As Integer)
        If tempSourceFile = Nothing Then
            ' skip
        Else
            ' check if file size got bigger
            Dim information As System.IO.FileInfo
            information = My.Computer.FileSystem.GetFileInfo(tempSourceFile)
            If information.Length > 44 Then
                ' new info
                Main.blnRecorded(i) = True
            Else
                ' same file so nothing worthwhile was saved
            End If
        End If

    End Sub
    Private Sub rbUnidentified_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUnidentified.CheckedChanged
        Me.tbDisplayClipsBy.Text = Main.sLocalizationStrings(Main.iUnidentifiedClips, Main.iLanguageSelected)
    End Sub
    Private Sub rbMultiple_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMultiple.CheckedChanged
        Me.tbDisplayClipsBy.Text = Main.sLocalizationStrings(Main.iMultipleClips, Main.iLanguageSelected)
    End Sub
    Private Sub rbSpeaker_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbSpeaker.CheckedChanged
        Me.tbDisplayClipsBy.Text = Main.sLocalizationStrings(Main.iSpeakerNumberClips, Main.iLanguageSelected)
    End Sub
    Private Sub rbUpdated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUpdated.CheckedChanged
        Me.tbDisplayClipsBy.Text = Main.sLocalizationStrings(Main.iUpdate, Main.iLanguageSelected)
    End Sub
    Private Sub rbAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAll.CheckedChanged
        Me.tbDisplayClipsBy.Text = Main.sLocalizationStrings(Main.iAllClips, Main.iLanguageSelected)
    End Sub
    Private Sub btnMoveDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveDown.Click
        If Me.blnMoveDown = True Then
            ' move to bottom
            Me.btnMoveDown.Text = Main.sLocalizationStrings(Main.iMoveUp, Main.iLanguageSelected)
            Me.Location = New Point(0, 444)
            MasterText.Location = New Point(512, 360)
            Me.blnMoveDown = False
        Else
            ' move to top
            Me.btnMoveDown.Text = Main.sLocalizationStrings(Main.iMoveDown, Main.iLanguageSelected)
            MasterText.topRight()
            Me.Location = New Point(0, 0)
            MasterText.blnTopRight = True
            Me.blnMoveDown = True
        End If
    End Sub
    Private Sub upDownSpeakerNumber_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles upDownSpeakerNumber.ValueChanged
        ' go to start of file
        '  Main.iCurrentClipNumber = 1
        ' find first for this number
        '   Me.goForward()
        ' endless loop

        Main.displayStatusText()
    End Sub
    Public Sub goBack()
        ifRecordingDidWeWriteNewFile(Main.iCurrentClipNumber)
        ' Me.btnUpdate.BackColor = Color.LightGray
        Me.btnForward.BackColor = Color.LightGray
        If Main.iCurrentClipNumber = 1 Then Main.iCurrentClipNumber = Main.iLastClipNumber + 1
        Dim i As Integer = Main.iCurrentClipNumber
        ' iLastClipNumber is really 1 over in order to handle splits properly
        ' so don't show the "last" one as it is blank
        If Me.rbAll.Checked = True Then
            ' "Next clip" "Verify All" "record all" ' can't match string as string changes
            i = skipBackIfOmittedProcessedOrRecorded(i)
        ElseIf Me.rbUnidentified.Checked = True Then
            '"Unidentified character clip"
            Do
                i = skipBackIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.iNumberOfCharactersInClip(i) = 0
        ElseIf Me.rbMultiple.Checked = True Then
            '"Multiple characters in a clip"
            Do
                i = skipBackIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.iNumberOfCharactersInClip(i) > 1
        ElseIf Me.rbUpdated.Checked = True Then
            '"Verify Updated clip"
            Do
                i = skipBackIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.sCharacter(i, 0) <> Nothing
        ElseIf Me.rbSpeaker.Checked Then
            ' "Same speaker number" Record
            Do
                i = skipBackIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.sSpeakerNumber(i) = Me.upDownSpeakerNumber.Value.ToString
        ElseIf Me.rbSpeaker.Checked = True Then
            Do
                i = skipBackIfOmittedProcessedOrRecorded(i)
                If i >= Main.iLastClipNumber Then Beep() : i = 1 : Exit Do
            Loop Until Main.sSpeakerNumber(i) = Me.upDownSpeakerNumber.Value.ToString
        Else
            i = skipBackIfOmittedProcessedOrRecorded(i)
        End If
        Main.iCurrentClipNumber = i
        displayPropertiesOfClip(2)
    End Sub
    Private Function skipBackIfOmittedProcessedOrRecorded(ByVal clipNumber As Integer)
        ' main reason is checked for in the case statement
        ' additional reasons to skip this clip are
        ' 1 omitted text and show omitted not checked
        clipNumber -= 1
        clipNumber = skipBackOverOmitted(clipNumber)
        ' 2 display only clips to process is checked
        If Me.chkbxDisplayOnlyClipsToProcess.Checked = True Then
            ' to process or to record
            ' 2a recording
            If MainMenu.rbRecord.Checked = True Then
                ' recording
                ' 2a loop until this clip is to be recorded
                clipNumber = skipBackOverRecorded(clipNumber)
            Else
                ' not recording
                ' 2b loop until this clip is to be processed
                clipNumber = skipBackOverProcessed(clipNumber)
            End If
        Else
            ' show all
        End If
        Return clipNumber
    End Function
    '  Private Sub cbCharacters_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharacters.SelectedValueChanged
    '     Me.btnUpdate.BackColor = Color.LawnGreen
    'End Sub
    Private Sub cbCharacters_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharacters.Enter
        Me.btnForward.BackColor = Color.LawnGreen
        btnNext.BackColor = Color.LawnGreen
        getAndShowSpeakerNumberForCharacter()

    End Sub
    Private Sub updateCharactersInMasterFile()
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
        ' speaker number associated with character
        Dim speaker As Integer
        speaker = Main.assignSpeakerToCharacter(Me.cbCharacters.Text)
        Me.upDownSpeakerNumber.Value = speaker
        Main.sSpeakerNumber(Main.iCurrentClipNumber) = speaker
        ' prompt
        '  Main.sPrompt(Main.iCurrentClipNumber) = Me.cbCharacterPrompt.Text
        Main.writeClipsToMasterFileAndAdjustClipSize(False) ' adjust clip size false
        '  displayPropertiesOfClip(2)  ' check for redundant xxxxxxxxxxxxxxxx
        Main.displayStatusText()
    End Sub
    ' Private Sub btnForward_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForward.Click
    ''   Dim myclip As New Clip(main.iCurrentClipNumber, sTextArray, iLastClipNumber)
    ''  myclip.goForward(Me.lbForwardBackBy.SelectedItem, iLastClipNumber)
    '  If btnForward.BackColor = Color.LawnGreen Then
    '     btnForward.BackColor = Color.Yellow
    '    updateCharactersInMasterFile()
    '   Me.btnForward.BackColor = Color.LightCyan
    '   End If
    '  goForward()
    '     Me.btnForward.BackColor = Color.LightGray
    ' End Sub
    Private Sub btnForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForward.Click
        Me.UseWaitCursor = True
        Me.Update()
        If btnForward.BackColor = Color.LawnGreen Then
            btnForward.BackColor = Color.Yellow ' show that processing is taking place
            btnForward.Update()
            ' this takes 6 seconds or so
            updateCharactersInMasterFile()
        End If
        Me.btnForward.BackColor = Color.LightCyan
        ' this is short time
        goForward()
        Me.UseWaitCursor = False
        ' Me.cbCharacters.DroppedDown = True
        Me.btnForward.BackColor = Color.LightGray

    End Sub
    Private Sub btnBack_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        goBack()
    End Sub
    Private Sub btnStart_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        goHome()
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Me.UseWaitCursor = True ' 2007-07-16
        Me.Hide()
        MainMenu.Show()
        MainMenu.setCheckMarksAndEnableMenuItems()
        MasterText.Hide()
        SpeakerText.Hide()
        Main.processOmittedTextBasedOnCheckedInfo()
        MainMenu.showStatsForUnidentifiedMultipleTotal()
        If MainMenu.rbUnidentified.Checked = True Then
            MainMenu.rbMultiple.Checked = True
        End If

    End Sub
    '  Private Sub cbCharactersEdit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharactersEdit.SelectedIndexChanged
    '     Me.cbCharacters.Text = Me.cbCharactersEdit.Text
    '' Me.cbCharacterPrompt.Text = Me.extractPrompt(Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex))
    '   Me.btnForward.BackColor = Color.LawnGreen
    ' End Sub
    '    Private Sub pressedEnter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharacters.Enter
    ' pressed enter on the forward back control
    '  Me.cbCharacterPrompt.Text = ""
    '    updateCharactersInMasterFile()
    ' End Sub

    '    Private Sub cbCharacters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCharacters.SelectedIndexChanged
    '' Dim x = Me.cbCharacters.SelectedIndex
    '   updateCharacterPrompt()
    '  End Sub
    Private Sub cbCharacters_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCharacters.SelectedValueChanged
        '   Dim x = Me.cbCharacters.SelectedIndex
        updateCharacterPrompt()
        btnForward.BackColor = Color.LawnGreen
        showSpeakerText(cbCharactersEdit.Text)
        If Me.btnForward.BackColor = Color.LawnGreen Then
            Me.cbCharacters.DroppedDown = False ' 2007-05-14
            Me.btnForward.BackColor = Color.LawnGreen
        Else
            Me.cbCharacters.DroppedDown = True ' 2007-05-14
            Me.btnForward.BackColor = Color.LightGray
        End If

    End Sub

    Private Sub updateCharacterPrompt()
        Dim x = cbCharacters.Text
        Dim temp, temp2, temp3 As String
        If Me.cbCharacters.SelectedIndex = -1 Then
            'skip
        Else
            'Me.cbCharacters.Text = Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex)
            'Dim temp2 = Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex)
            Me.cbCharacterPrompt.Text = Me.extractPrompt(Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex + 1))
            temp2 = Me.cbCharacters.SelectedIndex + 1
            temp = Me.extractPrompt(Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex + 1))
            ' Me.cbCharacterPrompt.Text = Me.extractPrompt(Me.cbCharacters.Text)
        End If
        temp2 = Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex + 1)
        temp2 = Main.sCharacter(Main.iCurrentClipNumber, 0)
        temp2 = Main.sCharacter(Main.iCurrentClipNumber, 1)
        temp2 = Main.sCharacter(Main.iCurrentClipNumber, 2)
        temp3 = Me.cbCharacters.SelectedIndex
        '  Me.cbCharacters.Text = Me.removePrompt(Me.cbCharacters.Text)
        Me.cbCharacters.Update()
        Me.cbCharacterPrompt.Update()
        Me.cbCharactersEdit.Text = Me.cbCharacters.Text
        Me.cbCharacters.BackColor = Color.LawnGreen
        Me.cbCharacterPrompt.BackColor = Color.LawnGreen
        ' updateCharactersInMasterFile()
        Me.btnForward.BackColor = Color.LightGray

    End Sub


    '  End Sub
    ' Private Sub cbCharacters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbCharacters.SelectedIndexChanged
    '    If Me.cbCharacters.SelectedIndex = -1 Then
    ''skip
    '  Else
    ''Me.cbCharacters.Text = Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex)
    ''Dim temp2 = Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex)
    '        Me.cbCharacterPrompt.Text = Me.extractPrompt(Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex))
    '        Me.cbCharacterPrompt.Text = Me.extractPrompt(Me.cbCharacters.Text)
    '    End If
    'Dim temp2 = Main.sCharacter(Main.iCurrentClipNumber, Me.cbCharacters.SelectedIndex)
    '    temp2 = Main.sCharacter(Main.iCurrentClipNumber, 0)
    '   temp2 = Main.sCharacter(Main.iCurrentClipNumber, 1)
    ''    temp2 = Main.sCharacter(Main.iCurrentClipNumber, 2)
    'Dim temp3 = Me.cbCharacters.SelectedIndex
    '    Me.cbCharacters.Text = Me.removePrompt(Me.cbCharacters.Text)
    ''    Me.cbCharacters.Update()
    '    Me.cbCharacterPrompt.Update()
    '    Me.cbCharactersEdit.Text = Me.cbCharacters.Text
    '    Me.cbCharacters.BackColor = Color.LawnGreen
    '    Me.cbCharacterPrompt.BackColor = Color.LawnGreen
    '    updateCharactersInMasterFile()
    '    Me.btnForward.BackColor = Color.LightGray
    'End Sub

    Private Sub cbCharactersEdit_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbCharactersEdit.SelectedValueChanged
        Me.cbCharacters.Text = Me.cbCharactersEdit.Text
        Me.btnForward.BackColor = Color.LawnGreen
        getAndShowSpeakerNumberForCharacter()
    End Sub

    Private Sub ToolStripProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub statusBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles statusBar.Click

    End Sub
End Class
