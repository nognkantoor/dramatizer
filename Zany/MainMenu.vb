Imports System
Imports System.IO
Imports System.Text
Imports str = Microsoft.VisualBasic.Strings

Public Class MainMenu
    Public sInsertFile As String
    Public utf8 As System.Text.UTF8Encoding = New System.Text.UTF8Encoding(False)
    Public sProgramDirectory As String = Directory.GetCurrentDirectory ' beware that this may change
    Public sRequiredFilesFolder As String = sProgramDirectory & "\RequiredFiles" ' beware that this may change
    Public sINIfile As String = sRequiredFilesFolder & "\zany.ini"
    Public sLocalizationFile As String = sRequiredFilesFolder & "\localization.txt" ' tab delimeted text
    Public sLocalizationBackupFile As String = sRequiredFilesFolder & "\localization.copy" ' tab delimeted text
    Public iMaximumLocalizationLanguages As Int16 = 3
    Public iMaximumLocalizationStrings As Int16 = 2000
    Public sLocalizationStrings(iMaximumLocalizationStrings, iMaximumLocalizationLanguages) As String
    Public iLanguageSelected As Int16
    ' links to table with info for each language
    ' 1 is the language country identifier
    Const iLanguageNames = 2
    Const iProgramName = 3
    Const iLanguage = 4
    Const iInitialize = 5
    Const iProcess = 6
    Const iUnidentified = 7
    Const iMultiple = 8
    Const iVerifyUpdated = 9
    Const iVerifyAll = 10
    Const iAssignVoices = 11
    Const iCreateScripts = 12
    Const iRecord = 13
    Const iAbout = 14
    Const iExit = 15
    Const iNext = 16
    Const iInfoStartProject = 17
    Const iInfoStartProcessing = 18
    Const iInfo0Unidentified = 19
    Const iInfo1Unidentified = 20
    Const iInfoManyUnidentified = 21
    Const iSpeakerNumber = 22
    Const iClipNumber = 23
    Const iPrompt = 24
    ' need to make public in order to use in other form
    Public iShowContext = 25
    Public iHideContext = 26
    Public iNextClip = 27
    Public iUnidentifiedCharacter = 28
    Public iMultipleCharacters = 29
    Public iUpdatedCharacter = 30
    Public iInfo0Multiple = 31
    Public iInfo1Multiple = 32
    Public iInfoManyMultiples = 33
    Public iStartProcessingWarning = 34
    Public iTranslateMenu = 35
    Public iFileName = 36
    Public iBrowse = 37
    Public iFontName = 38
    Public iFontSize = 39
    Public iQuoteType = 40
    Public iAudioRecordingProgram = 41
    Public iDisplayQuotes = 42
    Public iTextEncoding = 43
    Public iANSI = 44
    Public iUTF8 = 45
    Public iCancel = 46
    Public iSetOptions = 47
    Public iCurrentLocationNumber = 48
    Public iOK = 49
    Public iUpdate = 50
    Public iEdit = 51
    Public iMaxClipSize = 52
    Public iEthnologueCode = 53
    Public iHeadings = 54
    Public iIntroductions = 55
    Public iSectionHeads = 56
    Public iFootnotes = 57
    Public iReferences = 58
    Public iChapterNumbers = 59
    Public iDoNotSpeakThis = 60
    Public iCharacterName = 61
    Public iNoINIfile_Short = 62
    Public iNoINIfile_Long = 63
    Public iCorrectTheText = 64
    Public iUnableToProcessText = 65
    Public iVerifyOpeningQuote = 66
    Public iContinueVerifyOpeningQuote = 67
    Public iContinuingQuote = 68
    Public iSecondQuoteFound = 69
    Public iTextUnusable = 70
    Public iSeeMoreProblems = 71
    Public iContinuingQuoteTip = 72
    Public iFileReadError = 73
    Public iUnidentifiedCharactersToFix = 74
    Public iMultipleCharactersToFix = 75
    Public iTotalClips = 76
    Public iClipsToRecord = 77
    Public iClickNextToStart = 78
    Public iVerifyUpdatedText = 79
    Public iVerifyAllText = 80
    Public iAssignCharacterToSpeakerText = 81
    Public iCreateScriptsText = 82
    Public iRecordText = 83
    Public iUnassignedCharactersToFix = 84
    Public iShowVerses = 85
    Public iShowSFMcodes = 86
    Public iShowSpeakerText = 87
    Public iUseSettingsToAdjustClipSize = 88
    Public iInputTab = 89
    Public iQuoteTypeTab = 90
    Public iOmitTextTab = 91
    Public iRecordingProgramTab = 92
    Public iClipSizeTab = 93
    Public iEthnologueCodeTab = 94
    Public iOutputTab = 95
    Public iDisplayUnrecordedClipsOnly = 96
    Public iRecordOneSpeakerAtATime = 97
    Public iShowPrompt = 98
    Public iFontTab = 99
    Public iThisOne = 100
    Public iDramatizerOutputFolder = 101
    Public iSpeechContinuedToNextClip = 102
    Public iClipSize = 103
    Public iPercentIdentified = 104
    Public iBreakAtMostParagraphs = 105
    Public iDefaultSettings = 106
    Public iMasterText = 107
    Public iAvailableDiskSpaceC = 108
    Public iAvailableDiskSpaceD = 109
    Public iNotAQuote = 110
    Public iDisplayUnprocessedClipsOnly = 111
    Public iDisplayOmittedClipsToo = 112


    Public iSaveChangeAndSeeNext = 113
    Public iCancelChangeAndCloseMenu = 114
    Public iSaveChangeAndCloseMenu = 115
    Public iBackAndCancelChange = 116
    Public iStart = 117
    Public iTypeInOtherBox = 118
    Public iMoveToTopRight = 119
    Public iMoveToBottomLeft = 120
    Public iMore = 121
    Public iLess = 122
    Public iDisplayClipsBy = 123
    Public iAllClips = 124
    Public iUnidentifiedClips = 125
    Public iMultipleClips = 126
    Public iCharacterNameClips = 127
    Public iSpeakerNumberClips = 128
    Public iCongratulationsRecord = 129





    Public sProgramName As String = "Dramatizer"
    Public sProgramVersion As String = "2.0"
    Public sProjectName As String
    Public sProjectFileName As String
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        readLocalizationFile()
        fillLanguageControl()
        localizeMainMenu(1) ' this must be first
        localizeMain(1)
        localizeTranslate(1)
        localizeDramatizer(1)
        Main.readMasterFile()
        setCheckMarksAndEnableMenuItems()
    End Sub
    Private Sub readLocalizationFile()
        Try
            ' Excel saves as ANSI file
            Dim sr As StreamReader = New StreamReader(Me.sLocalizationFile, Encoding.UTF7, True)
            Dim temp As String
            Dim temp2(1000) As String 'items in string
            Dim item, language As Integer
            Do While Not sr.EndOfStream
                item += 1
                temp = sr.ReadLine()
                temp = Main.regexReplace(temp, """", "") ' remove quote marks
                temp2 = temp.Split(vbTab)
                For language = 0 To temp2.Length - 1
                    If temp2(language) = "" Then temp2(language) = temp2(1) ' Column two always has English data 
                    ' row is the item and column is the language
                    sLocalizationStrings(item, language) = temp2(language)
                Next
            Loop
            sr.Close()
        Catch ex As Exception
            MessageBox.Show(Me.sLocalizationStrings(iFileReadError, Me.iLanguageSelected) & vbCrLf & Me.sLocalizationFile & vbCrLf & vbCrLf & ex.Message, Me.sLocalizationStrings(Me.iFileReadError, Me.iLanguageSelected), MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End
        End Try
    End Sub

    Private Sub fillLanguageControl()
        Try
            Dim language As Int16 = 0
            Do
                language += 1
                If language = 1 Then
                    Me.lblLanguage.Text = Me.sLocalizationStrings(iLanguage, language)
                Else
                    ' add space between languages
                    Me.lblLanguage.Text = Me.lblLanguage.Text + "  " + Me.sLocalizationStrings(iLanguage, language)
                End If
                Me.cbLanguage.Items.Add(sLocalizationStrings(iLanguageNames, language))
                If language = Me.iMaximumLocalizationLanguages Then Exit Do
            Loop Until Me.sLocalizationStrings(iLanguageNames, language) = Nothing
            ' set default to English -- maybe it already is
            cbLanguage.Text = cbLanguage.Items.Item(0)
        Catch ex As Exception
            MessageBox.Show("Problem loading language names into list box." & vbCrLf & ex.Message, "Problem loading language names", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub fillForwardBackByControl(ByVal language As Int16)
        Try
            dramatizer.lbForwardBackBy.Items.Clear()
            dramatizer.lbForwardBackBy.Items.Add(Me.sLocalizationStrings(iNextClip, language))
            dramatizer.lbForwardBackBy.Items.Add(Me.sLocalizationStrings(iUnidentifiedCharacter, language))
            dramatizer.lbForwardBackBy.Items.Add(Me.sLocalizationStrings(iMultipleCharacters, language))
            dramatizer.lbForwardBackBy.Items.Add(Me.sLocalizationStrings(iUpdatedCharacter, language))
            dramatizer.lbForwardBackBy.Items.Add(Me.sLocalizationStrings(iSpeakerNumber, language))
            dramatizer.lbForwardBackBy.Items.Add(Me.sLocalizationStrings(iNextClip, language))
            dramatizer.btnMoreOptions.Text = Me.sLocalizationStrings(Me.iMore, language)
            dramatizer.btnLessOptions.Text = Me.sLocalizationStrings(Me.iLess, language)
            dramatizer.lblDisplay.Text = Me.sLocalizationStrings(Me.iDisplayClipsBy, language)
            dramatizer.rbAll.Text = Me.sLocalizationStrings(Me.iAllClips, language)
            dramatizer.rbUnidentified.Text = Me.sLocalizationStrings(Me.iUnidentifiedClips, language)
            dramatizer.rbMultiple.Text = Me.sLocalizationStrings(Me.iMultipleClips, language)
            dramatizer.rbCharacter.Text = Me.sLocalizationStrings(Me.iCharacterNameClips, language)
            dramatizer.rbSpeaker.Text = Me.sLocalizationStrings(Me.iSpeakerNumberClips, language)
        Catch ex As Exception
            MessageBox.Show("Problem loading forward and back names into list box." & vbCrLf & ex.Message, "Problem loading forward and back names", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub localizeTranslate(ByVal language As Int16)
        translate.btnOK.Text = sLocalizationStrings(iOK, language)
        translate.btnCancel.Text = sLocalizationStrings(iCancel, language)
        translate.lblSource.Text = sLocalizationStrings(2, 1)
        translate.lblTarget.Text = sLocalizationStrings(2, iLanguageSelected)
    End Sub
    Public Sub localizeMain(ByVal language As Int16)
        Main.Text = Me.Text
        Main.lblDiskSpaceDriveC.Text = Me.sLocalizationStrings(Me.iAvailableDiskSpaceC, language)
        Main.lblFileName.Text = Me.sLocalizationStrings(iFileName, language)
        Main.lblFontName.Text = Me.sLocalizationStrings(Me.iFontName, language)
        Main.lblFontSize.Text = Me.sLocalizationStrings(Me.iFontSize, language)
        Main.btnDefaultSettings.Text = Me.sLocalizationStrings(Me.iDefaultSettings, language)
        Main.btnBrowse.Text = Me.sLocalizationStrings(Me.iBrowse, language)
        Main.btnBrowseOutputFolder.Text = Me.sLocalizationStrings(Me.iBrowse, language)
        Main.lblDramatizerOutputFolder.Text = Me.sLocalizationStrings(Me.iDramatizerOutputFolder, language)
        Main.lblFirstLevelQuoteType.Text = Me.sLocalizationStrings(Me.iQuoteType, language)
        Main.lblAudioRecordingProgram.Text = Me.sLocalizationStrings(Me.iAudioRecordingProgram, language)
        Main.btnBrowseAudio.Text = Me.sLocalizationStrings(Me.iBrowse, language)
        '  Main.btnChooseEncoding.Text = Me.sLocalizationStrings(Me.iTextEncoding, language)
        Main.btnCancel.Text = Me.sLocalizationStrings(Me.iCancel, language)
        Main.btnSetOptions.Text = Me.sLocalizationStrings(Me.iSetOptions, language)
        Main.btnDisplayClips.Text = Me.sLocalizationStrings(Me.iDisplayQuotes, language)
        ' Main.btnChooseEncoding.Text = Me.sLocalizationStrings(Me.iTextEncoding, language)
        Main.rbEncodingANSI.Text = Me.sLocalizationStrings(Me.iANSI, language)
        Main.rbEncodingUTF8.Text = Me.sLocalizationStrings(Me.iUTF8, language)
        Main.lblEncoding.Text = Me.sLocalizationStrings(Me.iTextEncoding, language)
        Main.lblEthnologueCode.Text = Me.sLocalizationStrings(Me.iEthnologueCode, language)
        Main.lblMaxClipSize.Text = Me.sLocalizationStrings(Me.iMaxClipSize, language)
        Main.lblCurrentPosition.Text = "" ' Me.sLocalizationStrings(Me.iCurrentLocationNumber, language)
        Main.lblMaxClipSize.Text = Me.sLocalizationStrings(Me.iMaxClipSize, language)
        Main.chkbxBreakAtParagraphs.Text = Me.sLocalizationStrings(Me.iBreakAtMostParagraphs, language)
        Main.chkbxAdjustClipSize.Text = Me.sLocalizationStrings(Me.iUseSettingsToAdjustClipSize, language)
        Main.TabPage1.Text = Me.sLocalizationStrings(Me.iInputTab, language)
        Main.TabPage1.Text = Me.sLocalizationStrings(Me.iInputTab, language)
        Main.TabPage2.Text = Me.sLocalizationStrings(Me.iQuoteTypeTab, language)
        ' moved to mastertext Main.TabPage3.Text = Me.sLocalizationStrings(Me.iOmitTextTab, language)
        Main.TabPage4.Text = Me.sLocalizationStrings(Me.iRecordingProgramTab, language)
        Main.TabPage5.Text = Me.sLocalizationStrings(Me.iClipSizeTab, language)
        Main.TabPage6.Text = Me.sLocalizationStrings(Me.iEthnologueCodeTab, language)
        Main.TabPage7.Text = Me.sLocalizationStrings(Me.iOutputTab, language)
        Main.TabPage8.Text = Me.sLocalizationStrings(Me.iFontTab, language)
    End Sub
    Private Sub localizeMainMenu(ByVal language As Int16)
        Me.rbInitialize.Text = Me.sLocalizationStrings(iInitialize, language)
        Me.rbMultiple.Text = Me.sLocalizationStrings(iMultiple, language)
        Me.rbRecord.Text = Me.sLocalizationStrings(iRecord, language)
        Me.rbStartProcessing.Text = Me.sLocalizationStrings(iProcess, language)
        Me.rbUnidentified.Text = Me.sLocalizationStrings(iUnidentified, language)
        Me.rbAssignVoices.Text = Me.sLocalizationStrings(iAssignVoices, language)
        Me.rbVerifyUpdated.Text = Me.sLocalizationStrings(iVerifyUpdated, language)
        Me.rbVerifyAll.Text = Me.sLocalizationStrings(iVerifyAll, language)
        Me.rbAssignVoices.Text = Me.sLocalizationStrings(iAssignVoices, language)
        Me.rbAssignVoices.Text = Me.sLocalizationStrings(iAssignVoices, language)
        Me.rbAssignVoices.Text = Me.sLocalizationStrings(iAssignVoices, language)
        Me.rbCreateScripts.Text = Me.sLocalizationStrings(iCreateScripts, language)
        Me.Text = Main.sProjectName & " - " & Me.sLocalizationStrings(iProgramName, language) & " - " & Main.sProgramVersion
        Me.btnEnd.Text = Me.sLocalizationStrings(iExit, language)
        Me.btnNext.Text = Me.sLocalizationStrings(iNext, language)
        Me.btnTranslateMenu.Text = Me.sLocalizationStrings(iTranslateMenu, language)
    End Sub
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        End
    End Sub
    Private Sub cbLanguage_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbLanguage.SelectedIndexChanged
        iLanguageSelected = Me.cbLanguage.SelectedIndex + 1
        Me.localizeMainMenu(Me.iLanguageSelected)
        Me.localizeMain(Me.iLanguageSelected)
        Me.localizeDramatizer(Me.iLanguageSelected)
        Me.localizeTranslate(Me.iLanguageSelected)
        ' moved to mastertext form
        ' Me.localizeMasterText(Me.iLanguageSelected)
    End Sub
    Private Sub rbInitialize_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbInitialize.CheckedChanged
        If Me.rbInitialize.Checked = True Then
            Me.TextBox1.Text = formatTextForTextBox(Me.sLocalizationStrings(iInfoStartProject, Me.iLanguageSelected))
            Me.TextBox1.BackColor = Color.LightGoldenrodYellow
        Else
            ' ignore
        End If
    End Sub
    Private Sub rbStartProcessing_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbStartProcessing.CheckedChanged
        ' you can't depend on the checked status
        ' how else do we know that we have processed the text
        ' if the lastclip is big
        ' If Me.rbStartProcessing.Checked = True Then
        If Main.iLastClipNumber > 0 Then
            ' If Me.lblStartProcessing.Visible = True Then
            ' going to overwrite master file - give warning
            Me.TextBox1.Text = Me.formatTextForTextBox(Me.sLocalizationStrings(iStartProcessingWarning, Me.iLanguageSelected) & _
            Me.sLocalizationStrings(iInfoStartProcessing, Me.iLanguageSelected))
            Me.TextBox1.BackColor = Color.Pink
        Else
            Me.TextBox1.Text = formatTextForTextBox(Me.sLocalizationStrings(iInfoStartProcessing, Me.iLanguageSelected))
            Me.TextBox1.BackColor = Color.LightGoldenrodYellow
        End If
        ' Else
        '' ignore
        ' End If
    End Sub
    Private Sub rbUnidentified_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbUnidentified.CheckedChanged
        Try
            If Me.rbUnidentified.Checked = True Then
                '  Me.TextBox1.BackColor = Color.LightGoldenrodYellow
                Main.iUnidentfiedTotal = Main.countUnidentified()
                If Main.iUnidentfiedTotal = 0 Then
                    Me.TextBox1.Text = Me.sLocalizationStrings(iInfo0Unidentified, Me.iLanguageSelected).Replace("""", "")
                    '      Me.TextBox1.BackColor = Color.LawnGreen

                ElseIf Main.iUnidentfiedTotal = 1 Then
                    Me.TextBox1.Text = Me.sLocalizationStrings(iInfo1Unidentified, Me.iLanguageSelected).Replace("""", "")
                ElseIf Main.iUnidentfiedTotal > 1 Then
                    Me.TextBox1.Text = Me.sLocalizationStrings(iInfoManyUnidentified, Me.iLanguageSelected).Replace("""", "")
                Else
                    '
                    ' we got here because we set iunidentifiedtotal to -1 if there wasn't a count

                    ' Debug.Assert(False, "unidentified: " & Main.iUnidentfiedTotal.ToString)
                End If

                Main.showStatsForUnidentifiedMultipleTotal()
            Else
                ' ignore
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            If Me.rbInitialize.Checked = True Then
                dramatizer.Hide()
                Me.Hide()
                Main.ToolStripProgressBar1.Maximum = 10
                Main.ToolStripProgressBar1.ProgressBar.Value = 1
                Main.Show()
                ' finish the initialize and move on
                Me.rbStartProcessing.Checked = True
            ElseIf Me.rbStartProcessing.Checked = True Then
                Main.Hide()
                Main.initializeText()
                ' finish the processing and move on
                Me.rbUnidentified.Checked = True
                dramatizer.btnRecord.Visible = False
                dramatizer.btnUpdate.Visible = True
              
            ElseIf Me.rbUnidentified.Checked = True Then
                prepareToWorkFromMaster()
                dramatizer.rbUnidentified.Checked = True
                dramatizer.goForward()
                ' when come back do multiple
                Me.rbMultiple.Checked = True

            ElseIf Me.rbMultiple.Checked = True Then
                prepareToWorkFromMaster()
                dramatizer.rbMultiple.Checked = True

                dramatizer.goForward()
                ' when come back do updated
                Me.rbVerifyUpdated.Checked = True
            ElseIf Me.rbVerifyUpdated.Checked = True Then
                prepareToWorkFromMaster()
                dramatizer.rbUpdated.Checked = True

                dramatizer.goForward()
                ' when come back do verify all
                Me.rbVerifyAll.Checked = True
    
            ElseIf Me.rbVerifyAll.Checked = True Then
                prepareToWorkFromMaster()
                dramatizer.rbAll.Checked = True
                Me.rbAssignVoices.Checked = True
    
            ElseIf Me.rbAssignVoices.Checked = True Then
                '  prepareToWorkFromMaster()
                MessageBox.Show("Here we assign any unassigned characters to a voice", "Assign", MessageBoxButtons.OK)
                Me.lblCreateScripts.Visible = False
                Me.rbCreateScripts.Checked = True
    
            ElseIf Me.rbCreateScripts.Checked = True Then
                Main.readClipsFromFileMaster()
                Me.rbCreateScripts.Enabled = True
                createScriptsMaster()
                createScripts1to30()
                Main.createWaveFiles() ' do just once
                MessageBox.Show("All scripts created. See My Documents\My Dramatizer\scripts", "file created", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.rbRecord.Enabled = True
                Me.rbRecord.Checked = True
            ElseIf Me.rbRecord.Checked = True Then
                dramatizer.rbSpeaker.Checked = True

                '  dramatizer.lbForwardBackBy.SelectedIndex = 4
                dramatizer.displayMasterAndVoiceTalentText()
                dramatizer.btnRecord.Visible = True
                dramatizer.btnUpdate.Visible = False

                dramatizer.Show()
                Me.Hide()
                MasterText.Show()

    
            End If
        Catch ex As Exception
            MessageBox.Show("oops 1" & ex.Message)
        End Try
        Me.setCheckMarksAndEnableMenuItems()

    End Sub
    Private Sub prepareToWorkFromMaster()
        dramatizer.Show()
        Me.Hide()
        Main.Hide()
        Main.readClipsFromFileMaster()
        dramatizer.displayMasterAndVoiceTalentText()
        Main.fillPromptControl()
        Main.fillCharacterNamesControl()
    End Sub
    Private Sub localizeDramatizer(ByVal language As Int16)
        fillForwardBackByControl(language)
        dramatizer.Text = Main.Text
        dramatizer.rbUpdated.Text = sLocalizationStrings(Me.iUpdatedCharacter, language)
        dramatizer.chkbxShowPrompt.Text = sLocalizationStrings(Me.iShowPrompt, language)
        dramatizer.chkbxDisplayOmittedClips.Text = sLocalizationStrings(Me.iDisplayOmittedClipsToo, language)
        ' dramatizer.chkbxDisplayUnrecordedOnly.Text = sLocalizationStrings(Me.iDisplayUnrecordedClipsOnly, language)
        dramatizer.chkbxDisplayUnprocessedOnly.Text = sLocalizationStrings(Me.iDisplayUnprocessedClipsOnly, language)
        '     dramatizer.chkbxSpeaker.Text = sLocalizationStrings(Me.iRecordOneSpeakerAtATime, language)
        dramatizer.lblClipNumber.Text = sLocalizationStrings(iClipNumber, language)
        dramatizer.lblCharacterSpeakerNumber.Text = sLocalizationStrings(iSpeakerNumber, language)
        dramatizer.lblCharacterPrompt.Text = sLocalizationStrings(iPrompt, language)
        dramatizer.lblCharacterName.Text = sLocalizationStrings(Me.iCharacterName, language)
        dramatizer.btnNotAQuote.Text = sLocalizationStrings(Me.iNotAQuote, language)
        dramatizer.btnNext.Text = sLocalizationStrings(iNext, language)
        'dramatizer.btnUpdate.Text = sLocalizationStrings(iUpdate, language)
        dramatizer.btnUpdate.Text = "" ' using icons now
        dramatizer.btnEdit.Text = sLocalizationStrings(iEdit, language)
        dramatizer.btnEnd.Text = sLocalizationStrings(iExit, language)

        '        ForwardBackControl.lbForwardBackBy.Text = Me.sLocalizationStrings(iNextClip, language)
    End Sub
    '   Private Sub localizeMasterText(ByVal language As Int16)
    '      MasterText.chkbxShowContext.Text = sLocalizationStrings(iShowContext, language)
    '     MasterText.chkbxShowSFMcodes.Text = sLocalizationStrings(iShowSFMcodes, language)
    '    MasterText.chkbxShowSpeakerText.Text = sLocalizationStrings(iShowSpeakerText, language)
    '   MasterText.chkbxShowVerses.Text = sLocalizationStrings(iShowVerses, language)
    '  End Sub
    Private Sub createScripts1to30()
        Try
            Dim fontName As String = Main.cbFontName.SelectedItem
            Dim fontSize As String = Main.cbFontSize.SelectedItem
            Dim temp As String
            Dim iScriptClipNumber, i As Integer
            Dim iSpeakerNumber As Int16
            For iSpeakerNumber = 1 To 30
                i = 0 ' counter
                Dim sw As StreamWriter = New StreamWriter(Main.sScriptSpeakerFile & iSpeakerNumber.ToString & ".html", False, System.Text.Encoding.UTF8)
                writeStartHTML(sw)
                sw.WriteLine("td.clip {font-family: " + fontName + ", Arial, Helvetica, sans-serif ; font-size: " + fontSize + "px; 	background-color: transparent}")
                sw.WriteLine(" -->")
                sw.WriteLine("</style>")
                sw.WriteLine("</head>")
                sw.WriteLine("<body>")
                sw.WriteLine("<table>")
                For iScriptClipNumber = 1 To Main.iLastClipNumber
                    If Main.sSpeakerNumber(iScriptClipNumber) = iSpeakerNumber.ToString Then
                        i += 1
                        sw.WriteLine("<tr >")
                        sw.Write("<td class=""counter"">")
                        sw.Write(i)
                        sw.Write("</td><td class=""speaker"">")
                        sw.Write(Main.sSpeakerNumber(iScriptClipNumber))
                        sw.Write("</td><td>")
                        If Main.sCharacter(iScriptClipNumber, 0) <> Nothing Then
                            sw.Write(Main.sCharacter(iScriptClipNumber, 0))
                        Else
                            sw.Write(Main.sCharacter(iScriptClipNumber, 1))
                        End If
                        sw.Write("</td><td>")
                        sw.Write(Main.sBook(iScriptClipNumber))
                        sw.Write(" ")
                        sw.Write(Main.sChapter(iScriptClipNumber))
                        sw.Write(".")
                        sw.Write(Main.sVerse(iScriptClipNumber))
                        sw.Write("</td><td>")
                        sw.WriteLine(iScriptClipNumber)
                        sw.Write("</td>")
                        sw.Write("</tr>")
                        temp = Main.sScript(iScriptClipNumber)
                        ' remove verse and \ codes
                        temp = dramatizer.removeVerseNumber(temp)
                        temp = dramatizer.removeSFMcodes(temp)
                        sw.WriteLine("<tr>")
                        sw.Write("<td class=""clip"">")
                        sw.WriteLine(temp)
                        sw.Write("</td>")
                        sw.Write("</tr>")
                        sw.WriteLine()
                    Else
                        ' skip
                    End If
                Next
                sw.Write("</table>")
                sw.Write("</body>")
                sw.Write("</html>")
                sw.Close()
            Next
         Catch ex As Exception
            MessageBox.Show("Error writing script for voice number " & iSpeakerNumber.ToString, "error writing file", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Private Sub writeStartHTML(ByVal fileOutput)
        sInsertFile = sRequiredFilesFolder & "\scriptHead.html"
        insertFileIntoCurrentFileOutput(fileOutput)
    End Sub
    Private Sub insertFileIntoCurrentFileOutput(ByVal fileOutput)
        Dim fileReader As StreamReader = New StreamReader(sInsertFile, utf8, 512)
        Do Until fileReader.EndOfStream
            fileOutput.WriteLine(fileReader.ReadLine)
        Loop
        fileReader.Close()
    End Sub
    Private Sub createScriptsMaster()
        Try
            Dim fontName As String = Main.cbFontName.SelectedItem
            Dim fontSize As String = Main.cbFontSize.SelectedItem
            Dim temp As String
            Dim iScriptClipNumber, i As Integer
            i = 0 ' counter
            Dim sw As StreamWriter = New StreamWriter(Main.sScriptSpeakerFile & "master.html", False, System.Text.Encoding.UTF8)
            writeStartHTML(sw)
            sw.WriteLine("td.clip {font-family: " + fontName + ", Arial, Helvetica, sans-serif ; font-size: " + fontSize + "px; 	background-color: transparent}")
            sw.WriteLine(" -->")
            sw.WriteLine("</style>")
            sw.WriteLine("</head>")
            sw.WriteLine("<body>")
            sw.WriteLine("<table>")
            For iScriptClipNumber = 1 To Main.iLastClipNumber
                i += 1
                sw.WriteLine("<tr >")
                sw.Write("<td class=""counter"">")
                sw.Write(i)
                sw.Write("</td><td class=""speaker"">")
                sw.Write(Main.sSpeakerNumber(iScriptClipNumber))
                sw.Write("</td><td>")
                If Main.sCharacter(iScriptClipNumber, 0) <> Nothing Then
                    sw.Write(Main.sCharacter(iScriptClipNumber, 0))
                Else
                    sw.Write(Main.sCharacter(iScriptClipNumber, 1))
                End If
                sw.Write("</td><td>")
                sw.Write(Main.sBook(iScriptClipNumber))
                sw.Write(" ")
                sw.Write(Main.sChapter(iScriptClipNumber))
                sw.Write(".")
                sw.Write(Main.sVerse(iScriptClipNumber))
                sw.Write("</td><td>")
                sw.WriteLine(iScriptClipNumber)
                sw.Write("</td>")
                sw.Write("</tr>")
                temp = Main.sScript(iScriptClipNumber)
                ' remove verse and \ codes
                temp = dramatizer.removeVerseNumber(temp)
                temp = dramatizer.removeSFMcodes(temp)
                sw.WriteLine("<tr>")
                sw.Write("<td class=""clip"">")
                sw.WriteLine(temp)
                sw.Write("</td>")
                sw.Write("</tr>")
                sw.WriteLine()
            Next
            sw.Write("</table>")
            sw.Write("</body>")
            sw.Write("</html>")
            sw.Close()
        Catch ex As Exception
            MessageBox.Show("Error writing script for master " & Main.sScriptSpeakerFile & vbCrLf & ex.Message, "error writing file", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
        Public Sub writeLocalizationFile()
        Try
            ' make backup
            If File.Exists(sLocalizationBackupFile) Then File.Delete(sLocalizationBackupFile)
            File.Copy(Me.sLocalizationFile, Me.sLocalizationBackupFile)
            ' Excel saves as ANSI file
            Dim sw As StreamWriter = New StreamWriter(Me.sLocalizationFile, False, Encoding.UTF7)
            Dim columnMax As Int16 = findMaxColumnForLocalization()
            Dim row, column As Int16
            ' write tab delimited file output
            For row = 1 To Me.iMaximumLocalizationStrings
                For column = 0 To columnMax - 1
                    sw.Write(sLocalizationStrings(row, column))
                    sw.Write(vbTab)
                Next
                sw.Write(sLocalizationStrings(row, column))
                sw.WriteLine()
                If sLocalizationStrings(row + 1, 1) = Nothing Then Exit For
            Next
            sw.Close()
        Catch ex As Exception
            MessageBox.Show("Problem writing the localization file: " & vbCrLf & Me.sLocalizationFile & vbCrLf & ex.Message, "File writing error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End
        End Try
    End Sub
    Public Function findMaxColumnForLocalization()
        ' find maximum columns
        Try
            Dim i As Int16
            Do
                i += 1
            Loop Until sLocalizationStrings(1, i) = Nothing Or i = Me.iMaximumLocalizationLanguages
            Return i
        Catch ex As Exception
            MessageBox.Show("Problem finding max columns for localization: " & vbCrLf & ex.Message, "Localization error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return 0
        End Try
    End Function

    Private Sub btnTranslateMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTranslateMenu.Click
        translate.Show()

    End Sub
    Public Function formatTextForTextBox(ByVal temp As String)
        temp = Main.regexReplace(temp, "(\()(\d)", vbCrLf & "    ($2")
        temp = Main.regexReplace(temp, "\|", vbCrLf & vbCrLf) ' new line with space above 
        Return temp
    End Function

    Private Sub rbMultiple_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbMultiple.CheckedChanged
        Try
            ' Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Main.iMultipleToFixTotal = Main.countMultipleNotFixedYet()
            If Main.iMultipleToFixTotal = 0 Then
                Me.TextBox1.Text = formatTextForTextBox(Me.sLocalizationStrings(Me.iInfo0Multiple, Me.iLanguageSelected))
                Me.TextBox1.BackColor = Color.LawnGreen
            ElseIf Main.iMultipleToFixTotal = 1 Then
                Me.TextBox1.Text = (Me.sLocalizationStrings(Me.iInfo1Multiple, Me.iLanguageSelected))
            ElseIf Main.iMultipleToFixTotal > 1 Then
                Me.TextBox1.Text = (Me.sLocalizationStrings(Me.iInfoManyMultiples, Me.iLanguageSelected))
            Else
                Debug.Assert(False, "multiple: " & Main.iMultipleToFixTotal.ToString)
            End If
            Main.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbVerifyUpdated_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVerifyUpdated.CheckedChanged
        Try
            ' Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = (Me.sLocalizationStrings(Me.iVerifyUpdatedText, Me.iLanguageSelected))
            Main.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbVerifyAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbVerifyAll.CheckedChanged
        Try
            '  Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = (Me.sLocalizationStrings(Me.iVerifyAllText, Me.iLanguageSelected))
            Main.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbAssignVoices_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAssignVoices.CheckedChanged
        Try
            '   Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = (Me.sLocalizationStrings(Me.iAssignCharacterToSpeakerText, Me.iLanguageSelected))
            Main.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbCreateScripts_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbCreateScripts.CheckedChanged
        Try
            '    Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = (Me.sLocalizationStrings(Me.iCreateScriptsText, Me.iLanguageSelected))
            Main.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbRecord_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbRecord.CheckedChanged
        Try
            '     Me.TextBox1.BackColor = Color.LightGoldenrodYellow
            Me.TextBox1.Text = Me.sLocalizationStrings(Me.iRecordText, Me.iLanguageSelected)
            Dim temp As String = Main.sRecordingFolder
            Dim iFound = str.InStr(temp, "My Documents")
            If iFound > 0 Then
                ' start with "My Documents
                temp = str.Mid(temp, iFound)
            Else
                ' keep as is
            End If
            temp = formatFolderStructure(temp)
            ' only show message when nothing is recorded
            MessageBox.Show(Me.sLocalizationStrings(iCongratulationsRecord, iLanguageSelected) + vbCrLf + temp, sLocalizationStrings(iRecord, iLanguageSelected), MessageBoxButtons.OK, MessageBoxIcon.Information)
            Main.showStatsForUnidentifiedMultipleTotal()
        Catch ex As Exception

        End Try

    End Sub
    Private Function formatFolderStructure(ByVal temp As String)
        Main.regexReplace(temp, "\", "$1\$2" + vbCrLf)
        Return temp
    End Function
    Private Sub setStart()
        If Main.checkISOcodePresent() And Main.checkQuoteTypePresent And Main.checkFileNamePresent Then
            Me.lblInitialize.Visible = True
            Me.lblInitialize.Update()
            Me.rbStartProcessing.Enabled = True
            Me.rbStartProcessing.Checked = True

        Else
            Me.lblInitialize.Visible = False
            Me.lblInitialize.Update()
            ' Me.rbStartProcessing.Enabled = False
        End If
    End Sub
    Private Sub setProcessed()
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
    Private Sub resetAllMarksAndMenuItems()
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
    Public Sub setCheckMarksAndEnableMenuItems()
        Main.createFoldersAndMasterAndScriptsFileNames()
        resetAllMarksAndMenuItems()

        setStart()
        setProcessed()
        ' see if master txt exists
        If File.Exists(Main.sMasterFileName) Then
            Me.rbUnidentified.Checked = True

            setUnidentified()
            setMultiple()
            setUnassigned()
            setCreateScripts()
            setRecord()
            setRecordDone()
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
    Private Sub setRecordDone()
        If Main.countClipsToRecord = 0 Then
            Me.lblRecord.Visible = True
        Else
            '  Me.lblRecord.Visible = False
        End If

    End Sub
    Private Sub setRecord()
        If Me.lblCreateScripts.Visible() = True Then
            Me.rbRecord.Enabled = True
        Else
            '   Me.rbRecord.Enabled = False
        End If

    End Sub
    Private Sub setCreateScripts()
        Dim y As Integer = Main.countUnidentifiedNotFixedYet + Main.countMultipleNotFixedYet + Main.countUnassignedCharacters
        If Main.countUnidentifiedNotFixedYet + Main.countMultipleNotFixedYet + Main.countUnassignedCharacters = 0 Then
            Me.rbCreateScripts.Enabled = True
        Else
            '    Me.rbCreateScripts.Enabled = False
        End If

    End Sub
    Private Sub setUnassigned()
        Dim iTemp As Integer = Main.countUnassignedCharacters ' 
        If iTemp = 0 And Me.lblStartProcessing.Visible Then ' all assigned and whe have started processing
            Me.lblAssignVoice.Visible = True
        Else
            '      Me.lblAssignVoice.Visible = False
        End If

    End Sub
    Private Sub setMultiple()
        Dim iTemp As Integer = Main.countMultipleNotFixedYet
        If iTemp = 0 Then
            Me.lblMultiple.Visible = True
        Else
            '       Me.lblMultiple.Visible = False
        End If
    End Sub
    Private Sub setUnidentified()
        Dim iTemp As Integer = Main.countUnidentifiedNotFixedYet
        If iTemp = 0 Then
            Me.lblUnidentified.Visible = True
        Else
            '        Me.lblUnidentified.Visible = False
        End If
    End Sub
End Class