Imports System
Imports System.IO
Imports System.Text
Imports str = Microsoft.VisualBasic.Strings
Public Class MainMenu
    Public blnStartRecording As Boolean = False
    Public sProgramDirectory As String = Directory.GetCurrentDirectory ' beware that this may change
    Public sRequiredFilesFolder As String = sProgramDirectory & "\RequiredFiles" ' beware that this may change
    Public sINIfile As String = sRequiredFilesFolder & "\zany.ini"
    Public sDramatizeFolder As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\My Dramatizer"
    Public sTempFolder As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\Dramatizer"
    Public sCreateClipsFileName As String = sTempFolder & "\01 - createClips.tmp"
    Public sGetBookChapterVerseFileName As String = sTempFolder & "\02 - bookChapterVerse.tmp"
    Public sIdentifyCharactersFileName As String = sTempFolder & "\03 - identifyCharacters.tmp"
    Public sOneLinePerClip As String = sTempFolder & "\04 - oneLinePerClip.tmp"
    Public sMasterWaveFile As String = sRequiredFilesFolder & "\master.wav"
    Public sCharacterNames_BookChapterVerseFileName As String = sRequiredFilesFolder & "\CharacterNames_BookChapterVerse.txt"
    Public sISO639_3file As String = sRequiredFilesFolder & "\iso-fdis-639-3_20061114.tab"
    Public sTempFileName As String = sTempFolder & "\temp.tmp"
    Public sVoiceFile As String = sRequiredFilesFolder & "\Character-Voice.txt"
    Public sInsertFile As String
    Public utf8 As System.Text.UTF8Encoding = New System.Text.UTF8Encoding(False)
    '    Public sProgramDirectory As String = Directory.GetCurrentDirectory ' beware that this may change
    '    Public sRequiredFilesFolder As String = sProgramDirectory & "\RequiredFiles" ' beware that this may change
    '    Public sINIfile As String = sRequiredFilesFolder & "\zany.ini"
    Public sLocalizationFile As String = sRequiredFilesFolder & "\localization.txt" ' tab delimeted text
    Public sLocalizationBackupFile As String = sRequiredFilesFolder & "\localization.copy" ' tab delimeted text
    '   Public iMaximumLocalizationLanguages As Int16 = 3
    '  Public iMaximumLocalizationStrings As Int16 = 2000
    '    Public sLocalizationStrings(iMaximumLocalizationStrings, iMaximumLocalizationLanguages) As String
    ' Public iLanguageSelected As Int16
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Main.readLocalizationFile()
        Me.fillLanguageControl()
        Me.localizeMainMenu(1) ' this must be first
        Main.localizeMain(1)
        Main.localizeTranslate(1)
        Main.localizeDramatizer(1)
        'Main.readCurrentSettings()
        Main.readMasterFile()
        Me.Show()
        Me.setCheckMarksAndEnableMenuItems()
    End Sub
    Public Sub setCheckMarksAndEnableMenuItems()
        Main.createFoldersAndMasterAndScriptsFileNames()
        Me.resetAllMarksAndMenuItems()
        Me.setStart()
        Me.setProcessed()
        ' see if master txt exists
        If File.Exists(Main.sMasterFileName) Then
            Me.rbUnidentified.Checked = True
            Me.setUnidentified()
            Me.setMultiple()
            Me.setUnassigned()
            Me.setCreateScripts()
            Me.setRecord()
            Me.setRecordDone()
            If Directory.GetFiles(Main.sRecordingFolder).Length > 0 Then
                ' wav files creates so we can start recording
                Me.resetAllMarksAndMenuItems()
                Me.rbRecord.Checked = True
                Me.rbRecord.Enabled = True
            Else
                ' not ready to record yet
            End If
            ' not ready yet
        End If
    End Sub
    Public Sub localizeMainMenu(ByVal language As Int16)
        Me.rbInitialize.Text = Main.sLocalizationStrings(Main.iInitialize, language)
        Me.rbMultiple.Text = Main.sLocalizationStrings(Main.iMultiple, language)
        Me.rbRecord.Text = Main.sLocalizationStrings(Main.iRecord, language)
        Me.rbStartProcessing.Text = Main.sLocalizationStrings(Main.iProcess, language)
        Me.rbUnidentified.Text = Main.sLocalizationStrings(Main.iUnidentified, language)
        Me.rbAssignVoices.Text = Main.sLocalizationStrings(Main.iAssignVoices, language)
        Me.rbVerifyUpdated.Text = Main.sLocalizationStrings(Main.iVerifyUpdated, language)
        Me.rbVerifyAll.Text = Main.sLocalizationStrings(Main.iVerifyAll, language)
        Me.rbAssignVoices.Text = Main.sLocalizationStrings(Main.iAssignVoices, language)
        Me.rbAssignVoices.Text = Main.sLocalizationStrings(Main.iAssignVoices, language)
        Me.rbAssignVoices.Text = Main.sLocalizationStrings(Main.iAssignVoices, language)
        Me.rbCreateScripts.Text = Main.sLocalizationStrings(Main.iCreateScripts, language)
        Me.Text = Main.sProjectName & " - " & Main.sLocalizationStrings(Main.iProgramName, language) & " - " & Main.sProgramVersion
        Me.btnEnd.Text = Main.sLocalizationStrings(Main.iExit, language)
        Me.btnNext.Text = Main.sLocalizationStrings(Main.iNext, language)
        Me.btnTranslateMenu.Text = Main.sLocalizationStrings(Main.iTranslateMenu, language)
    End Sub
    Public Sub fillLanguageControl()
        Try
            Dim language As Int16 = 0
            Do
                language += 1
                If language = 1 Then
                    Me.lblLanguage.Text = Main.sLocalizationStrings(Main.iLanguage, language)
                Else
                    ' add space between languages
                    Me.lblLanguage.Text = Me.lblLanguage.Text + "  " + Main.sLocalizationStrings(Main.iLanguage, language)
                End If
                Me.cbLanguage.Items.Add(Main.sLocalizationStrings(Main.iLanguageNames, language))
                If language = Main.iMaximumLocalizationLanguages Then Exit Do
            Loop Until Main.sLocalizationStrings(Main.iLanguageNames, language) = Nothing
            ' set default to English -- maybe it already is
            Me.cbLanguage.Text = Me.cbLanguage.Items.Item(0)
        Catch ex As Exception
            MessageBox.Show("Problem loading language names into list box." & vbCrLf & ex.Message, "Problem loading language names", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        End
    End Sub
    Private Sub cbLanguage_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLanguage.SelectedIndexChanged
        Main.iLanguageSelected = Me.cbLanguage.SelectedIndex + 1
        Me.localizeMainMenu(Main.iLanguageSelected)
        Main.localizeMain(Main.iLanguageSelected)
        Main.localizeDramatizer(Main.iLanguageSelected)
        Main.localizeTranslate(Main.iLanguageSelected)
        ' moved to mastertext form
        ' Me.localizeMasterText(Me.iLanguageSelected)
    End Sub
    Private Sub rbInitialize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbInitialize.CheckedChanged
        If Me.rbInitialize.Checked = True Then
            Me.TextBox1.Text = Main.formatTextForTextBox(Main.sLocalizationStrings(Main.iInfoStartProject, Main.iLanguageSelected))
            Me.TextBox1.BackColor = Color.LightGoldenrodYellow
        Else
            ' ignore
        End If
    End Sub
    Private Sub rbStartProcessing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbStartProcessing.CheckedChanged
        ' you can't depend on the checked status
        ' how else do we know that we have processed the text
        ' if the lastclip is big
        ' If me.rbStartProcessing.Checked = True Then
        If Main.iLastClipNumber > 0 Then
            ' If Me.lblStartProcessing.Visible = True Then
            ' going to overwrite master file - give warning
            Me.TextBox1.Text = Main.formatTextForTextBox(Main.sLocalizationStrings(Main.iStartProcessingWarning, Main.iLanguageSelected) & _
            Main.sLocalizationStrings(Main.iInfoStartProcessing, Main.iLanguageSelected))
            Me.TextBox1.BackColor = Color.Pink
        Else
            Me.TextBox1.Text = Main.formatTextForTextBox(Main.sLocalizationStrings(Main.iInfoStartProcessing, Main.iLanguageSelected))
            Me.TextBox1.BackColor = Color.LightGoldenrodYellow
        End If
    End Sub
    Private Sub rbUnidentified_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUnidentified.CheckedChanged
        Try
            If Me.rbUnidentified.Checked = True Then
                Main.iUnidentfiedTotal = Main.countUnidentified()
                If Main.iUnidentfiedTotal = 0 Then
                    Me.TextBox1.Text = Main.sLocalizationStrings(Main.iInfo0Unidentified, Main.iLanguageSelected).Replace("""", "")
                ElseIf Main.iUnidentfiedTotal = 1 Then
                    Me.TextBox1.Text = Main.sLocalizationStrings(Main.iInfo1Unidentified, Main.iLanguageSelected).Replace("""", "")
                ElseIf Main.iUnidentfiedTotal > 1 Then
                    Me.TextBox1.Text = Main.sLocalizationStrings(Main.iInfoManyUnidentified, Main.iLanguageSelected).Replace("""", "")
                Else
                    ' we got here because we set iunidentifiedtotal to -1 if there wasn't a count
                    ' Debug.Assert(False, "unidentified: " & Me.iUnidentfiedTotal.ToString)
                End If
                Me.showStatsForUnidentifiedMultipleTotal()
            Else
                ' ignore
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Me.Hide()
        Try
            If Me.rbInitialize.Checked = True Then
                '  dramatizer.Hide()
                Me.Show()
                Main.ToolStripProgressBar1.Maximum = 10
                Main.ToolStripProgressBar1.ProgressBar.Value = 1
                Main.Show()
                ' finish the initialize and move on
                Me.rbStartProcessing.Checked = True
            ElseIf Me.rbStartProcessing.Checked = True Then
                Me.Show()
                Main.initializeText()
                ' finish the processing and move on
                Me.rbUnidentified.Checked = True
                dramatizer.btnRecord.Visible = False
                dramatizer.btnUpdate.Visible = True
            ElseIf Me.rbUnidentified.Checked = True Then
                Main.prepareToWorkFromMaster()
                dramatizer.rbUnidentified.Checked = True
                dramatizer.goForward()
                ' when come back do multiple
                Me.rbMultiple.Checked = True
            ElseIf Me.rbMultiple.Checked = True Then
                Main.prepareToWorkFromMaster()
                dramatizer.rbMultiple.Checked = True
                dramatizer.goForward()
                ' when come back do updated
                Me.rbVerifyUpdated.Checked = True
            ElseIf Me.rbVerifyUpdated.Checked = True Then
                Main.prepareToWorkFromMaster()
                dramatizer.rbUpdated.Checked = True
                dramatizer.chkbxDisplayOnlyClipsToProcess.Checked = False
                dramatizer.goForward()
                ' when come back do verify all
                Me.rbVerifyAll.Checked = True
            ElseIf Me.rbVerifyAll.Checked = True Then
                Main.prepareToWorkFromMaster()
                dramatizer.rbAll.Checked = True
                Me.rbAssignVoices.Checked = True
            ElseIf Me.rbAssignVoices.Checked = True Then
                '  main.prepareToWorkFromMaster()
                MessageBox.Show("Here we assign any unassigned characters to a voice", "Assign", MessageBoxButtons.OK)
                'Me.Show()
                Me.lblCreateScripts.Visible = False
                Me.rbCreateScripts.Checked = True
            ElseIf Me.rbCreateScripts.Checked = True Then
                Main.readClipsFromFileMaster()
                Me.rbCreateScripts.Enabled = True
                Main.createScriptsMaster()
                Main.createScripts1to30()
                Main.createWaveFiles() ' do just once
                MessageBox.Show("All scripts created. See My Documents\My Dramatizer\scripts", "file created", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.rbRecord.Enabled = True
                Me.rbRecord.Checked = True
            ElseIf Me.rbRecord.Checked = True Then
                'Me.Hide()
                Main.blnRecordingInProgress = True
                dramatizer.rbSpeaker.Checked = True
                dramatizer.displayMasterAndVoiceTalentText()
                dramatizer.btnRecord.Visible = True
                '   dramatizer.btnUpdate.Visible = False
                dramatizer.Show()
                MasterText.Show()
                MasterText.topRight()
            End If
        Catch ex As Exception
            MessageBox.Show("oops 1" & ex.Message)
        End Try
        Me.setCheckMarksAndEnableMenuItems()
    End Sub
    Private Sub btnTranslateMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTranslateMenu.Click
        translate.Show()
    End Sub
    Private Sub rbMultiple_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMultiple.CheckedChanged
        Try
            ' Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Main.iMultipleToFixTotal = Main.countMultipleNotFixedYet()
            If Main.iMultipleToFixTotal = 0 Then
                Me.TextBox1.Text = Main.formatTextForTextBox(Main.sLocalizationStrings(Main.iInfo0Multiple, Main.iLanguageSelected))
                Me.TextBox1.BackColor = Color.LawnGreen
            ElseIf Main.iMultipleToFixTotal = 1 Then
                Me.TextBox1.Text = (Main.sLocalizationStrings(Main.iInfo1Multiple, Main.iLanguageSelected))
            ElseIf Main.iMultipleToFixTotal > 1 Then
                Me.TextBox1.Text = (Main.sLocalizationStrings(Main.iInfoManyMultiples, Main.iLanguageSelected))
            Else
                Debug.Assert(False, "multiple: " & Main.iMultipleToFixTotal.ToString)
            End If
            Me.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub rbVerifyUpdated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVerifyUpdated.CheckedChanged
        Try
            ' Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = (Main.sLocalizationStrings(Main.iVerifyUpdatedText, Main.iLanguageSelected))
            Me.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub rbVerifyAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVerifyAll.CheckedChanged
        Try
            '  Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = (Main.sLocalizationStrings(Main.iVerifyAllText, Main.iLanguageSelected))
            Me.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub rbAssignVoices_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAssignVoices.CheckedChanged
        Try
            '   Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = (Main.sLocalizationStrings(Main.iAssignCharacterToSpeakerText, Main.iLanguageSelected))
            Me.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub rbCreateScripts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCreateScripts.CheckedChanged
        Try
            '    Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = (Main.sLocalizationStrings(Main.iCreateScriptsText, Main.iLanguageSelected))
            Me.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub rbRecord_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbRecord.CheckedChanged
        Try
            '     Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = Main.sLocalizationStrings(Main.iRecordText, Main.iLanguageSelected)
            Dim temp As String = Main.sRecordingFolder
            Dim iFound = str.InStr(temp, "My Documents")
            If iFound > 0 Then
                ' start with "My Documents
                temp = str.Mid(temp, iFound)
            Else
                ' keep as is
            End If
            temp = Main.formatFolderStructure(temp)
            ' only show message when nothing is recorded
            If Me.rbRecord.Checked = True And Main.blnRecordingInProgress = False Then
                MessageBox.Show(Main.sLocalizationStrings(Main.iCongratulationsRecord, Main.iLanguageSelected) + vbCrLf + temp, Main.sLocalizationStrings(Main.iRecord, Main.iLanguageSelected), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ' skip
            End If
            Me.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub resetAllMarksAndMenuItems()
        Me.lblAssignVoice.Visible = False
        Me.lblCreateScripts.Visible = False
        Me.lblInitialize.Visible = False
        Me.lblMultiple.Visible = False
        Me.lblRecord.Visible = False
        Me.lblStartProcessing.Visible = False
        Me.lblUnidentified.Visible = False
        Me.lblVerifyAll.Visible = False
        Me.lblVerifyUpdated.Visible = False
        Me.rbAssignVoices.Enabled = False
        Me.rbCreateScripts.Enabled = False
        Me.rbInitialize.Enabled = True ' always on
        Me.rbMultiple.Enabled = False
        Me.rbRecord.Enabled = False
        Me.rbStartProcessing.Enabled = False
        Me.rbUnidentified.Enabled = False
        Me.rbVerifyAll.Enabled = False
        Me.rbVerifyUpdated.Enabled = False
    End Sub
    Public Sub setStart()
        If Main.checkISOcodePresent() And Main.checkQuoteTypePresent And Main.checkFileNamePresent Then
            Me.lblInitialize.Visible = True
            Me.lblInitialize.Update()
            Me.rbStartProcessing.Enabled = True
            '           Me.rbStartProcessing.Checked = True
        Else
            Me.lblInitialize.Visible = False
            Me.lblInitialize.Update()
            ' me.rbStartProcessing.Enabled = False
        End If
    End Sub
    Public Sub setProcessed()
        If File.Exists(Main.sMasterFileName) Then
            Me.lblStartProcessing.Visible = True
            Me.lblStartProcessing.Update()
            Me.rbUnidentified.Enabled = True
            Me.rbMultiple.Enabled = True
            Me.rbVerifyAll.Enabled = True
            Me.rbVerifyUpdated.Enabled = True
        Else
        End If
    End Sub
    Public Sub setRecordDone()
        If Main.countClipsToRecord = 0 Then
            Me.lblRecord.Visible = True
        Else
            '  me.lblRecord.Visible = False
        End If
    End Sub
    Public Sub setRecord()
        If Me.lblCreateScripts.Visible() = True Then
            Me.rbRecord.Enabled = True
            Me.rbRecord.Checked = True
        Else
            '   me.rbRecord.Enabled = False
        End If
    End Sub
    Public Sub setCreateScripts()
        Dim y As Integer = Main.countUnidentifiedNotFixedYet + Main.countMultipleNotFixedYet + Main.countUnassignedCharacters
        If Main.countUnidentifiedNotFixedYet + Main.countMultipleNotFixedYet + Main.countUnassignedCharacters = 0 Then
            Me.rbCreateScripts.Enabled = True
            '          Me.rbCreateScripts.Checked = True
        Else
            '    me.rbCreateScripts.Enabled = False
        End If
    End Sub
    Public Sub setUnassigned()
        Dim iTemp As Integer = Main.countUnassignedCharacters ' 
        If iTemp = 0 And Me.lblStartProcessing.Visible Then ' all assigned and whe have started processing
            Me.lblAssignVoice.Visible = True
            '         Me.rbStartProcessing.Checked = True
        Else
            '      me.lblAssignVoice.Visible = False
        End If
    End Sub
    Public Sub setMultiple()
        Dim iTemp As Integer = Main.countMultipleNotFixedYet
        If iTemp = 0 Then
            Me.lblMultiple.Visible = True
            '        Me.rbVerifyUpdated.Checked = True
        Else
            '       me.lblMultiple.Visible = False
        End If
    End Sub
    Public Sub setUnidentified()
        Dim iTemp As Integer = Main.countUnidentifiedNotFixedYet
        If iTemp = 0 Then
            Me.lblUnidentified.Visible = True
            '       Me.rbStartProcessing.Checked = True
        Else
            '        me.lblUnidentified.Visible = False
        End If
    End Sub
    Public Sub setRecordingStatus(ByVal sRecordingInProgress)
        If sRecordingInProgress = "#TRUE#" Then
            Main.blnRecordingInProgress = True
            Me.rbInitialize.Enabled = True
            Me.rbStartProcessing.Enabled = True
            Me.rbUnidentified.Enabled = False
            Me.rbMultiple.Enabled = False
            Me.rbVerifyUpdated.Enabled = False
            Me.rbVerifyAll.Enabled = False
            Me.rbAssignVoices.Enabled = False
            Me.rbCreateScripts.Enabled = False
            Me.rbRecord.Enabled = True
        Else
            Main.blnRecordingInProgress = False
            '   me.rbInitialize.Enabled = True
            '  me.rbStartProcessing.Enabled = True
            ' me.rbUnidentified.Enabled = True
            ' me.rbMultiple.Enabled = False
            '  me.rbVerifyUpdated.Enabled = True
            ' me.rbVerifyAll.Enabled = True
            ' me.rbAssignVoices.Enabled = True
            '  me.rbCreateScripts.Enabled = True
            '  Me.rbRecord.Enabled = True
        End If
    End Sub
    Public Sub showStatsForUnidentifiedMultipleTotal()
        Main.readClipsFromFileMaster()
        Dim unidentifiedToFix As String = Main.countUnidentifiedNotFixedYet.ToString & "  " & Main.sLocalizationStrings(Main.iUnidentifiedCharactersToFix, Main.iLanguageSelected)
        Dim unidentified As String = Main.iUnidentifiedSpeakingCharacter & "  " & Main.sLocalizationStrings(Main.iUnidentifiedCharacter, Main.iLanguageSelected)
        Dim multipleToFix As String = Main.countMultipleNotFixedYet.ToString & "  " & Main.sLocalizationStrings(Main.iMultipleCharactersToFix, Main.iLanguageSelected)
        Dim multiple As String = Main.iMultipleSpeakingCharacter & "  " & Main.sLocalizationStrings(Main.iMultipleCharacters, Main.iLanguageSelected)
        Dim unassigned As String = Main.countUnassignedCharacters.ToString & "  " & Main.sLocalizationStrings(Main.iUnassignedCharactersToFix, Main.iLanguageSelected)
        Dim clipsToRecord As String = Main.countClipsToRecord.ToString & "  " & Main.sLocalizationStrings(Main.iClipsToRecord, Main.iLanguageSelected)
        Dim totalClips As String = Main.countTotal.ToString & "  " & Main.sLocalizationStrings(Main.iTotalClips, Main.iLanguageSelected)
        Dim percent As Integer = (Main.countTotal() - Main.iUnidentifiedSpeakingCharacter - Main.iMultipleSpeakingCharacter) * 100 / Main.countTotal()
        Dim temp As String
        temp = Me.TextBox1.Text
        temp = temp + "|" + unidentifiedToFix & vbCrLf & unidentified & vbCrLf & multipleToFix & vbCrLf & multiple & vbCrLf & unassigned & vbCrLf & clipsToRecord & vbCrLf & totalClips
        temp = temp + vbCrLf + percent.ToString + Main.sLocalizationStrings(Main.iPercentIdentified, Main.iLanguageSelected)
        temp = temp + "|" & Main.sLocalizationStrings(Main.iClickNextToStart, Main.iLanguageSelected)
        Me.TextBox1.Text = Main.formatTextForTextBox(temp)
        If Main.countUnidentifiedNotFixedYet() + Main.countMultipleNotFixedYet() = 0 Then
            Me.TextBox1.BackColor = Color.LawnGreen
        Else
            Me.TextBox1.BackColor = Color.LightYellow
        End If
        ' Mainmenu.TextBox1.BackColor = Color.Cyan
    End Sub
End Class