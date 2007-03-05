Imports System
Imports System.IO
Imports System.Text
Imports str = Microsoft.VisualBasic.Strings
Imports System.Text.RegularExpressions
Public Class Main
    Public iSavedLanguageIndexFromFile As Integer
    Public rowLocalization As Integer
    Public columnLocalization As Integer
    Public blnMasterFileWrittenSinceLastRead As Boolean = True

    Public iTempClipNumber As Integer
    Public sSavedLanguage As String = "English"
    Public sNotAQuote = "Not A Quote"
    ' these three also defined in MainMenu
    Public sProgramDirectory As String = Directory.GetCurrentDirectory ' beware that this may change
    Public sRequiredFilesFolder As String = sProgramDirectory & "\RequiredFiles" ' beware that this may change
    Public sINIfile As String = sRequiredFilesFolder & "\zany.ini"
    ' links to table with info for each language
    ' 1 is the language country identifier
    Public iLanguageNames = 2
    Public iProgramName = 3
    Public iLanguage = 4
    Public iInitialize = 5
    Public iProcess = 6
    Public iUnidentified = 7
    Public iMultiple = 8
    Public iVerifyUpdated = 9
    Public iVerifyAll = 10
    Public iAssignVoices = 11
    Public iCreateScripts = 12
    Public iRecord = 13
    Public iAbout = 14
    Public iExit = 15
    Public iNext = 16
    Public iInfoStartProject = 17
    Public iInfoStartProcessing = 18
    Public iInfo0Unidentified = 19
    Public iInfo1Unidentified = 20
    Public iInfoManyUnidentified = 21
    Public iSpeakerNumberText = 22
    Public iClipNumber = 23
    Public iPrompt = 24
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
    Public iMaxClipSizeText = 52
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
    Public iStartText = 117
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
    Public iMoveUp = 130
    Public iMoveDown = 131
    Public iRecordSpeaker = 132
    Public iRecordTotalText = 133
    Public iAllScriptsAndWavFilesCreated = 134
    Public iText = 135
    Public iTextEncodingTab = 136
    Public iRequiredInformationMissing = 137
    Public iPleaseCorrect = 138
    Public iInfoProcessing = 139
    Public iAddNewMenuLanguage = 140
    Public iCorrectCurrentMenu = 141












    Public sProgramName As String = "Dramatizer"
    Public sProgramVersion As String = "2.0"
    Public sProgramVersionFull As String = sProgramVersion + ".18 2007-02-19"
    Public sProjectName As String
    Public sProjectFileName As String
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
    Public sTempRecordingInProgressFolder As String = sRecordingFolder + "\recording in progress"
    Public sSpeakerFile As String = sRequiredFilesFolder & "\Character-Speaker.txt"
    Public sInsertFile As String
    Public utf8 As System.Text.UTF8Encoding = New System.Text.UTF8Encoding(False)
    Public sLocalizationFile As String = sRequiredFilesFolder & "\localization.txt" ' tab delimeted text
    Public sLocalizationBackupFile As String = sRequiredFilesFolder & "\localization.copy" ' tab delimeted text
    Public iMaximumLocalizationLanguages As Int16 = 4
    Public iMaximumLocalizationStrings As Int16 = 2000
    Public sLocalizationStrings(iMaximumLocalizationStrings, iMaximumLocalizationLanguages) As String
    Public iLanguageSelected As Int16
    Public timeOfLastReadOfMaster As Double
    Public blnContinuingQuoteFound As Boolean = False
    Public blnQuoteMarkerErrorFound As Boolean = False
    Public blnRecordingByVoice As Boolean = False
    Public blnRecordingInProgress As Boolean = False
    Public filenum As Int16 = 1
    Public fontBackgroundColor
    Public fontColor
    Public iClipsTotal As Integer
    Public iCurrentClipNumber As Integer = 1
    Public iCurrentPosition As Int32 = 0
    Public iEnd As Integer
    Public iLastClipNumber As Integer
    Public iMaxCharacters As Integer = 5
    Public iMaxClipSize As Integer = 500 ' characters = 30 seconds
    Public iMaxClips As Integer = 50000
    Public iMaxISOcode As Integer
    Public iMaxVoices As Int16 = 30
    Public iMultipleSpeakingCharacter As Integer
    Public iMultipleToFixTotal As Integer
    Public iMultipleTotal As Integer
    Public iNumberOfCharactersInClip(iMaxClips) As Integer
    Public iNumberToRecord(iMaxVoices) As Integer
    Public iOneSpeakingCharacter As Integer
    Public iRawClipsTotal As Integer
    Public iRecordTotal As Integer
    Public iStart As Integer
    Public iUnidentfiedTotal As Integer
    Public iUnidentifiedSpeakingCharacter As Integer
    Public iUpperMaxClipSize As String = 550
    Public iSpeakerFileColor(iMaxClips)
    Public iSpeakerFileSpeakerNumber(iMaxClips)
    Public isoCode(10000) As String
    Public isoName(10000) As String
    Public sAllClips(iMaxClips) As String
    Public sAudioProgram As String
    Public sBook(iMaxClips) As String
    Public sChapter(iMaxClips) As String
    Public sCharacter(iMaxClips, iMaxCharacters) As String
    Public sCharacterPrompts(iMaxClips) As String ' for prompt control -- this is really too large
    Public sSpeakerFileCharacterShort(iMaxClips) As String ' this is really too large
    Public sClipSize(iMaxClips) As String
    Public sClosingQuote As String
    Public sContinued(iMaxClips) As String
    Public sFontBackgroundColor As String
    Public sFontColor As String
    Public sID(iMaxClips) As String
    Public sISOCode As String
    Public sLanguageFolder As String
    Public sMasterFileName As String
    Public sOpeningQuote As String
    ' Public sProgramName As String = "Dramatizer"
    ' Public sProgramVersion As String = "2.0"
    ' Public sProjectFileName As String
    Public sProjectFolder As String
    '  Public sProjectName As String
    Public sProjectParatextFileName As String
    Public sProjectPath As String
    Public sPrompt As String ' get from sCharacter(i,j)
    Public sQuoteType As String
    Public sRecordingFolder As String
    Public sScript(iMaxClips) As String
    Public sScriptSpeakerFile As String
    Public sScriptsFolder As String
    Public sSpeakerNumber(iMaxClips) As String
    Public sTag(iMaxClips) As String
    Public sText As String
    Public sTextArray(100000) As String
    Public sTextEncoding As String
    Public sVerse(iMaxClips) As String
    Public sSpeakerFileCharacter(iMaxClips)
    Public scripture As Stream
    Public selectedFont As Font
    Public blnOmit(iMaxClips) As Boolean
    Public blnRecorded(iMaxClips) As Boolean

    Public sQuoteTypeCheverons As String = "«...»"
    Public sQuoteTypeSIL As String = "<<...>>"
    Public sQuoteTypeStraight As String = " ""..."" "
    Public sQuoteTypeSmartOpen As String = str.Chr(147)
    Public sQuoteTypeSmartClose As String = str.Chr(148)
    Public sQuoteTypeSmart As String = Me.sQuoteTypeSmartOpen + "..." + Me.sQuoteTypeSmartClose






    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        testForRequiredFoldersAndFiles()
        '  fillForwardBackControl("English")
        fillFontControl()
        fillQuoteTypeControl()
        fillAudioProgramControl()
        Me.cbOutputFolder.Text = Me.sDramatizeFolder
        readCurrentSettings()
        If File.Exists(sProjectFileName) Then
            sProjectName = getFileNameWithoutExtensionFromFullName(sProjectFileName)
        Else
            ' skip
            sProjectFileName = ""
            sProjectName = ""
        End If
        createFoldersAndMasterAndScriptsFileNames()
        Me.panelSettings.Show()
        readSpeakerFile()
        readISOfile()
        fillISOcontrol()
        showClipSize()
        showAvailableDiskSpace()
        '        localize("English") ' xxxxxxxx 
    End Sub
    Private Sub showAvailableDiskSpace()
        showAvailableDiskSpaceX("C") ' order is important here
        If File.Exists("d:\con") Then
            showAvailableDiskSpaceX("D")
        Else
            ' no D drive so skip
        End If
    End Sub
    Private Sub showAvailableDiskSpaceX(ByVal driveLetter As String)
        Try
            Dim xdrive As System.IO.DriveInfo
            Dim sTotalFreeSpace As String = ""
            xdrive = My.Computer.FileSystem.GetDriveInfo(driveLetter + ":\")
            Dim percentFree = (xdrive.TotalFreeSpace / xdrive.TotalSize) * 100
            If xdrive.DriveType = DriveType.Fixed Then
                sTotalFreeSpace = formatAvailableFreeSpace(xdrive.TotalFreeSpace)
                If driveLetter = "C" Then
                    Me.tbAvailableDiskSpaceDriveC.Text = sTotalFreeSpace
                    showDriveLetterCinfo(percentFree)
                    Me.lblAvailableDiskSpaceDriveD.Visible = False
                    Me.tbAvailableDiskSpaceDriveD.Visible = False
                Else
                    Me.tbAvailableDiskSpaceDriveD.Text = sTotalFreeSpace
                    showDriveLetterDinfo(percentFree)
                    Me.lblAvailableDiskSpaceDriveD.Visible = True
                    Me.tbAvailableDiskSpaceDriveD.Visible = True
                End If
            Else
                ' not a hard drive so skip
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub showDriveLetterCinfo(ByVal percentFree)
        If percentFree > 10 Then
            Me.tbAvailableDiskSpaceDriveC.BackColor = Color.LawnGreen
        ElseIf percentFree > 5 Then
            Me.tbAvailableDiskSpaceDriveC.BackColor = Color.Pink
        Else
            Me.tbAvailableDiskSpaceDriveC.BackColor = Color.Red
        End If
    End Sub
    Private Sub showDriveLetterDinfo(ByVal percentFree)
        If percentFree > 10 Then
            Me.tbAvailableDiskSpaceDriveD.BackColor = Color.LawnGreen
        ElseIf percentFree > 5 Then
            Me.tbAvailableDiskSpaceDriveD.BackColor = Color.Pink
        Else
            Me.tbAvailableDiskSpaceDriveD.BackColor = Color.Red
        End If
    End Sub
    Private Function formatAvailableFreeSpace(ByVal dTotalFreeSpace As Double)
        Dim iTotalFreeSpace As Integer
        Dim temp As String
        If dTotalFreeSpace > 1000000000 Then
            iTotalFreeSpace = dTotalFreeSpace / 1000000000
            temp = iTotalFreeSpace.ToString + "Gb"
        ElseIf dTotalFreeSpace > 1000000 Then
            iTotalFreeSpace = dTotalFreeSpace / 1000000
            temp = iTotalFreeSpace.ToString + "Mb"
        ElseIf dTotalFreeSpace > 1000 Then
            iTotalFreeSpace = dTotalFreeSpace / 1000
            temp = iTotalFreeSpace.ToString + "K"
        Else
            iTotalFreeSpace = dTotalFreeSpace
            temp = iTotalFreeSpace.ToString + " bytes"
        End If
        Return temp
    End Function
    Public Sub setFontForRTB()
        Dim fontName As String = cbFontName.SelectedItem
        Dim fontSize As String = cbFontSize.SelectedItem
        If cbFontName.SelectedItem = Nothing Then
            fontName = cbFontName.Text
        End If
        If fontName = Nothing Then fontName = "Microsoft Sans Serif"
        If cbFontSize.SelectedItem = Nothing Then
            fontSize = cbFontSize.Text
        End If
        If fontSize = Nothing Then fontSize = 14
        MasterText.tbFontSize.Text = fontSize
        ' ProgressIndicator.Show()
        ToolStripProgressBar1.ProgressBar.Visible = True
        ToolStripProgressBar1.ProgressBar.Maximum = 6
        ToolStripProgressBar1.ProgressBar.Value = 1
        SpeakerText.rtbText.Font = New Font(fontName, fontSize)
        ToolStripProgressBar1.ProgressBar.Value = 2
        MasterText.rtbTextWithContext.Font = New Font(fontName, fontSize)
        MasterText.rtbTextOnly.Font = New Font(fontName, fontSize)
        ToolStripProgressBar1.ProgressBar.Value = 3
        MasterText.rtbContextAbove.Font = New Font(fontName, fontSize)
        ToolStripProgressBar1.ProgressBar.Value = 4
        Me.rtbEncodingANSI.Font = New Font(fontName, fontSize)
        ToolStripProgressBar1.ProgressBar.Value = 5
        Me.rtbEncodingUTF8.Font = New Font(fontName, fontSize)
        ToolStripProgressBar1.ProgressBar.Value = 6
        ToolStripProgressBar1.Visible = False
    End Sub
    Private Sub fillFontColorControl()
        Dim FontFamilyCollection As New System.Drawing.Text.InstalledFontCollection
        Dim FontSize As Int16
        Try
            For FontSize = 8 To 12
                cbFontSize.Items.Add(FontSize)
            Next
            For FontSize = 14 To 28 Step 2
                cbFontSize.Items.Add(FontSize)
            Next
            For FontSize = 36 To 72 Step 12
                cbFontSize.Items.Add(FontSize)
            Next
        Catch ex As Exception
            MessageBox.Show("Problem filling font names and sizes into list box.", "Problem loading fonts names" & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub fillFontControl()
        cbFontName.Items.Clear()
        cbFontSize.Items.Clear()
        Dim FontFamilyCollection As New System.Drawing.Text.InstalledFontCollection
        Dim oFontFamily As FontFamily
        Dim FontSize As Int16
        Try
            For Each oFontFamily In FontFamilyCollection.Families
                If oFontFamily.IsStyleAvailable(FontStyle.Regular) Then
                    cbFontName.Items.Add(oFontFamily.Name)
                Else
                    'skip
                End If
            Next
            For FontSize = 8 To 12
                cbFontSize.Items.Add(FontSize)
            Next
            For FontSize = 14 To 28 Step 2
                cbFontSize.Items.Add(FontSize)
            Next
            For FontSize = 36 To 72 Step 12
                cbFontSize.Items.Add(FontSize)
            Next
        Catch ex As Exception
            MessageBox.Show("Problem filling font names and sizes into list box.", "Problem loading fonts names" & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub fillQuoteTypeControl()
        cbQuoteType.Items.Clear()
        Try
            cbQuoteType.Items.Add(Me.sQuoteTypeCheverons)
            cbQuoteType.Items.Add(Me.sQuoteTypeSIL)
            cbQuoteType.Items.Add(Me.sQuoteTypeSmart)
            cbQuoteType.Items.Add(Me.sQuoteTypeStraight)

            '          cbQuoteType.Items.Add("«...»")
            '         cbQuoteType.Items.Add("<<...>>")
            '        cbQuoteType.Items.Add(" ""..."" ")
            '            cbQuoteType.Items.Add("» ... «")
            '           cbQuoteType.Items.Add("== ... " & vbCrLf & "<< .. >>")
            '          cbQuoteType.Items.Add("unknown")
            ' cbQuoteType.DroppedDown = True
        Catch ex As Exception
            MessageBox.Show("Problem filling quote type names into list box.", "Problem loading quote type names" & ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub testForRequiredFoldersAndFiles()
        ifDirectoryDoesNotExistCreateIt(Me.sDramatizeFolder)
        ifDirectoryDoesNotExistCreateIt(Me.sRequiredFilesFolder) ' but should have been installed
        ifDirectoryDoesNotExistCreateIt(Me.sTempFolder)
        If My.Computer.FileSystem.FileExists(sCharacterNames_BookChapterVerseFileName) Then
            ' good
        Else
            MessageBox.Show("It appears that the program is not properly installed." & vbCrLf & "One or more required files are missing." & vbCrLf & vbCrLf & "The folder ""requiredFiles"" located with this program at " & Me.sProgramDirectory & " should contain the following files:" & vbCrLf & getFileNameWithExtensionFromFullName(Me.sCharacterNames_BookChapterVerseFileName), "Improper installation", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
            End
        End If
    End Sub
    Private Sub ButtonEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        End
    End Sub
    Public Function getFileNameWithoutExtensionFromFullName(ByVal filename As String)
        If filename <> Nothing Then
            filename = getFileNameWithExtensionFromFullName(filename)
            ' assume one .
            filename = str.Left(filename, InStr(filename, ".") - 1)
        Else
            ' skip
        End If
        Return filename
    End Function
    Public Function getFileNameWithExtensionFromFullName(ByVal filename As String)
        Dim i As Int16
        If filename <> Nothing Then
            Do While InStr(filename, "\") > 0
                i = InStr(filename, "\")
                filename = str.Mid(filename, InStr(filename, "\") + 1)
            Loop
        Else
            ' skip
        End If
        Return filename
    End Function
    Private Function selectEncodingFromRadioButtons()
        If rbEncodingANSI.Checked = True Then
            sTextEncoding = "ANSI"
        Else
            sTextEncoding = "UTF8"
        End If
        Return sTextEncoding
    End Function
    Private Sub splitOutAllFirstLevelQuotes()
        ' first select quote
        MasterText.rtbTextWithContext.Find(Me.sOpeningQuote)
        iStart = MasterText.rtbTextWithContext.SelectionStart()
        MasterText.rtbTextWithContext.Find(Me.sClosingQuote)
        iEnd = MasterText.rtbTextWithContext.SelectionStart()
        MasterText.rtbTextWithContext.SelectionStart = iStart
        MasterText.rtbTextWithContext.SelectionLength = iEnd - iStart
        MasterText.rtbTextWithContext.SelectionColor = Color.Blue
        ' apply color
        ' repeat to end
    End Sub
    Private Sub identifyAllQuotes()
        Dim quote As String
        Dim i As Integer = 1
        Dim markerName As String
        loadDocumentIntoScriptureFileStream()
        Dim sfmQuotes As SFMQuotes = New SFMQuotes(scripture, sProgramDirectory)
        sTextEncoding = selectEncodingFromRadioButtons()
        sfmQuotes.createClips(sTextEncoding)
        ' while we have sfmQuotes defined
        Dim temp
        For Each markerName In sfmQuotes.colMarkers
            If Me.cklbMarkers.Items.IndexOf(markerName) > 0 Then
                ' skip as already found
            Else
                Me.cklbMarkers.Items.Add(markerName)
                temp = markerName.Length
            End If
        Next
        Me.cklbMarkers.Show()
        sfmQuotes.getBookChapterVerse()
        ' use Barbe's data to identify speakers (characters)
        quote = sfmQuotes.identifySpeakingCharacters() ' start with sGetBookChapterVerseFileName and sBarbesData
        '  displayPropertiesOfClip()
    End Sub
    Private Sub getStatics()
        ' total clip
        ' total unidentified
        ' total multiple
        ' total unidentified to fix
        ' total multiple to fix
    End Sub
    Private Sub loadDocumentIntoScriptureFileStream()
        Try
            scripture = New FileStream(Me.sProjectFileName, FileMode.Open)
        Catch ex As Exception
            MessageBox.Show("Error trying to open " & Me.sProjectFileName & vbCrLf & "See if another application has the file open. " & ex.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
        '     writeCurrentSettings()
    End Sub
    Private Function buildArrayOfClips()
        ' create array of clips (<clip .....</clip> from the file
        Dim sr As StreamReader = New StreamReader(sIdentifyCharactersFileName, System.Text.Encoding.UTF8, True)
        Dim sw As StreamWriter = New StreamWriter(sOneLinePerClip, False, System.Text.Encoding.UTF8)
        Dim sText As String = ""
        Dim clipEnd As String = "</clip>"
        ' splits on < making two items here clip and /clip
        Dim i As Integer
        Dim temp As String = ""
        Do While Not sr.EndOfStream
            Do
                sText = sText + temp
                temp = sr.ReadLine
            Loop Until temp = clipEnd
            sText = sText + temp
            i = i + 1
            sTextArray(i) = sText
            temp = ""
            sText = ""
        Loop
        Dim j As Integer
        For j = 1 To i
            sw.WriteLine(sTextArray(j))
        Next
        sr.Close()
        sw.Close()
        Return i  ' xxxxxxxxxxxxxxxxxxxxx last clip number
    End Function
    '  Private Sub ButtonChangeFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '     FontDialog1.ShowDialog()
    '    VoiceTalentText.rtbText.Font = Me.FontDialog1.Font
    '   MasterText.rtbTextSmall.Font = Me.FontDialog1.Font
    '  MasterText.rtbContextAbove.Font = Me.FontDialog1.Font
    ' End Sub
    Public Sub writeClipsToMasterFileAndAdjustClipSize(ByVal blnAdjustClipSize As Boolean)
        Dim i As Integer
        Dim continued As Integer
        Dim splitText(100) As String
        Dim iMaxSplit As Integer
        Try
            Dim sw As StreamWriter = New StreamWriter(sTempFileName, False, Encoding.UTF8)
            sw.WriteLine("<?xml version='1.0' encoding='utf-8'?>")
            Dim sRecordingInProgress = isRecordingInProgress()
            sw.WriteLine("<dramatizer version=""" & sProgramVersion & """  recordingInProgress=""" & sRecordingInProgress & """ totalClips=""" & iLastClipNumber.ToString & """ date=""" & Microsoft.VisualBasic.DateAndTime.DateString & """  time=""" & DateAndTime.TimeOfDay & """ > ")
            ' For i = 1 To iLastClipNumber + 1 ' added one to force output of last clip okay
            For i = 1 To iLastClipNumber
                If blnAdjustClipSize Then
                    ' try to adjust size
                    splitText = makeClips30SecondsOrLess(i)
                    iMaxSplit = splitText.Length - 1
                Else
                    ' don't adjust size
                    splitText(0) = sScript(i)
                    iMaxSplit = 0
                End If
                For continued = 0 To iMaxSplit
                    If splitText(continued) = Nothing Then : Exit For
                        ' skip
                    Else
                        sw.Write("<clip ")
                        writeBookChapterVerse(i, sw)
                        writeContinuedAndSize(i, sw, continued.ToString, splitText(continued).Length.ToString, blnAdjustClipSize)
                        sw.Write(""" tag=""" & sTag(i))
                        '     If blnContinuingQuoteFound = True Then
                        '    Else
                        ' End If
                        writeSpeakerOmitRecordedMultipleCharacters(i, sw)
                        sw.Write(""" >")
                        Me.iTempClipNumber = i
                        writeSplitText(sw, continued, splitText(continued))
                        If Me.iTempClipNumber = 0 Then
                            ' skip
                        Else
                            ' this advances over Not a quote and following text
                            i = Me.iTempClipNumber
                            Me.iTempClipNumber = 0
                        End If
                        sw.WriteLine("</clip>")
                    End If
                Next
            Next
            sw.Write("</dramatizer>")
            sw.Close()
            ifError_QuoteMarkerFoundInsideOfText_doNoSaveWork()
            Me.readClipsFromFileMaster()
            blnMasterFileWrittenSinceLastRead = True
        Catch ex As Exception
            MessageBox.Show("Unable to write to file " & sTempFileName & vbCrLf & "Check to see if file is open in another application " & vbCrLf & ex.Message & vbCrLf & "Current clip " & Me.iCurrentClipNumber.ToString & vbCrLf & "Maximum clip number " & Me.iLastClipNumber.ToString, "File writing error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End
        End Try
    End Sub
    Private Sub writeContinuedAndSize(ByVal i As Integer, ByVal sw As StreamWriter, ByVal temp1 As Integer, ByVal temp2 As String, ByVal blnAdjustClipSize As Boolean)
        If blnAdjustClipSize Then
            ' for initial work we need to supply the split information
            sw.Write(""" continued=""" & temp1)
            sw.Write(""" size=""" & temp2)
        Else
            ' after the inital write we have split the clips
            sw.Write(""" continued=""" & sContinued(i))
            sw.Write(""" size=""" & sClipSize(i))
        End If
    End Sub
    Private Sub writeSplitText(ByVal sw As StreamWriter, ByVal continued As Integer, ByVal temp As String)
        ' check to see content should include Not a quote from next i
        If MainMenu.rbCreateScripts.Checked = True Then
            temp = getTempStringWithNotAQuotesAddedToIt()
        Else
            ' skip
        End If
        If continued > 0 Then
            sw.Write("\p" & temp)
        Else
            sw.Write(temp)
        End If
    End Sub
    Private Sub writeSpeakerOmitRecordedMultipleCharacters(ByVal i As Integer, ByVal sw As StreamWriter)
        Dim j As Integer
        sw.Write(""" speaker=""" & sSpeakerNumber(i))
        ' prompt is carried with character
        '  sw.Write(""" prompt=""" & sPrompt(i))
        sw.Write(""" omit=""" & blnOmit(i))
        sw.Write(""" recorded=""" & blnRecorded(i))
        sw.Write(""" multiple=""" & iNumberOfCharactersInClip(i).ToString)
        For j = 0 To iNumberOfCharactersInClip(i)
            sw.Write(""" character" & j.ToString & "=""" & sCharacter(i, j))
        Next
    End Sub
    Private Sub writeBookChapterVerse(ByVal i As Integer, ByVal sw As StreamWriter)
        sw.Write(" book=""" & sBook(i))
        sw.Write(""" chapter=""" & sChapter(i))
        sw.Write(""" verse=""" & sVerse(i))
    End Sub
    Private Sub ifError_QuoteMarkerFoundInsideOfText_doNoSaveWork()
        If blnQuoteMarkerErrorFound Then
            ' don't create master
            MessageBox.Show("Please correct the text now.", "Unable to process text.", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            ' xxxxxxxxxxxxxxx remove next line
            '  System.IO.File.Copy(sTempFileName, sMasterFileName, True) ' makes it less likely we will loose data on power failure
            End
        Else
            File.Copy(sTempFileName, sMasterFileName, True) ' makes it less likely we will loose data on power failure
        End If
    End Sub
    Private Function isRecordingInProgress()
        Dim temp As String = "#FALSE#'"
        If MainMenu.lblCreateScripts.Visible = True Then
            temp = "#TRUE#"
        Else
            temp = "#FALSE#"
        End If
        Return temp
    End Function
    '    Private Function breakTextAtParagraphVerseOrSentence(ByVal i)
    'Dim temp() As String
    '   temp = sScript(i).Split("\")
    ' Dim iCount = temp.Length
    '    Return temp
    ' End Function
    Private Function makeClips30SecondsOrLess(ByVal i)
        Me.iCurrentClipNumber = i
        Dim temp(2) As String
        Dim break(100) As String
        Dim j As Integer
        temp = breakTextAtParagraphVerseOrSentence(sScript(i))
        ' giving too many possible errors
        ' isOpenQuoteFoundAgain(temp(0))
        break(j) = temp(0)
        If temp(0).Length > 0 Then
            Dim iAdjustSize As Integer = Me.TrackBarClipSize.Value / 10
            Do
                j += 1
                temp = breakTextAtParagraphVerseOrSentence(temp(1))
                If temp(0) = Nothing Then
                    ' skip
                    break(j) = Nothing
                Else
                    break(j) = temp(0)
                End If
            Loop While temp(1).Length > Me.iMaxClipSize * iAdjustSize
        Else
        End If
        Return break
    End Function
    Private Sub isOpenQuoteFoundAgain(ByVal temp)
        Dim sOpenQuote As String = "«"
        Dim iIsOpenQuoteFoundAgain As Integer
        Dim sStringToCheckOut As String
        Select Case cbQuoteType.Text
            Case Me.sQuoteTypeCheverons
                sOpenQuote = "«"
            Case Me.sQuoteTypeSIL
                sOpenQuote = "<<"
            Case Me.sQuoteTypeSmart
                sOpenQuote = Me.sQuoteTypeSmartOpen    ' xxxxxxxxxx possible error here
            Case Me.sQuoteTypeStraight
                sOpenQuote = """"
            Case Else
                MessageBox.Show("Quote type error", "", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End
        End Select


        sStringToCheckOut = temp
        iIsOpenQuoteFoundAgain = str.InStr(3, sStringToCheckOut, sOpenQuote)
        '  Debug.Assert(iIsOpenQuoteFoundAgain = 0, "Error :" & sOpenQuote & " A second open quote found inside of clip." & vbCrLf & sStringToCheckOut & vbCrLf & sChapter(i) & "." & sVerse(i))
        If iIsOpenQuoteFoundAgain = 0 Then
            ' great no extra opening quote mark found
        Else
            ' oops this may be a continuing quote mark or an error
            Dim response = MessageBox.Show(Me.sLocalizationStrings(Me.iVerifyOpeningQuote, Me.iLanguageSelected) & " (" & sOpenQuote & ") " & Me.sLocalizationStrings(Me.iContinueVerifyOpeningQuote, Me.iLanguageSelected) & vbCrLf & vbCrLf & Me.sLocalizationStrings(Me.iContinuingQuoteTip, Me.iLanguageSelected) & vbCrLf & vbCrLf & sBook(Me.iCurrentClipNumber) & " " & sChapter(Me.iCurrentClipNumber) & "." & sVerse(Me.iCurrentClipNumber) & vbCrLf & vbCrLf & sStringToCheckOut & vbCrLf & vbCrLf & Me.sLocalizationStrings(Me.iContinuingQuote, Me.iLanguageSelected), Me.sLocalizationStrings(Me.iSecondQuoteFound, Me.iLanguageSelected), MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If response = vbYes Then
                ' continuing quote marker - good
                ' fix marker here.
                blnContinuingQuoteFound = True
            Else
                blnQuoteMarkerErrorFound = True
                response = MessageBox.Show(Me.sLocalizationStrings(Me.iTextUnusable, Me.iLanguageSelected) & Me.sLocalizationStrings(Me.iCorrectTheText, Me.iLanguageSelected) & vbCrLf & vbCrLf & Me.sLocalizationStrings(Me.iSeeMoreProblems, Me.iLanguageSelected), Me.sLocalizationStrings(Me.iCorrectTheText, Me.iLanguageSelected), MessageBoxButtons.YesNo)
                If response = vbYes Then
                    ' continue looking
                Else
                    End
                End If
            End If
        End If
    End Sub
    Private Sub resetScript()
        Dim i As Integer
        For i = 0 To Me.iMaxClips
            sScript(i) = ""
        Next
    End Sub
    Private Sub readClipsFromFileOneLinePerClip()
        Dim i As Integer
        Dim j As Int16
        Dim temp As String
        Dim temp2 As String
        '   Dim temp3 As String
        resetScript()
        Try
            ' ProgressIndicator.Show()
            MainMenu.progressBar.Visible = True
            '    Me.progressBar.Show()
            MainMenu.progressBar.Maximum = iLastClipNumber + 50
            Dim sr As StreamReader = New StreamReader(Me.sOneLinePerClip, Encoding.UTF8)
            Do While Not sr.EndOfStream
                MainMenu.tbProgress.Text = i.ToString
                MainMenu.progressBar.Value = i
                MainMenu.progressBar.Update()
                i = i + 1
                temp = sr.ReadLine()
                ' moved to readMaster
                ' isThereASpeakingPart
                ' extract script
                '        temp3 = (Me.regexReplace(temp, "(<clip.*?>)(.*?)(</clip>)", "$2")).ToString.Trim
                ' remove verses
                '       temp3 = Me.regexReplace(temp3, "(.*?)(<verse>.*?</verse>)(.*?)", "$1$3")
                ' remove \q, \p, \pc, q2, ..., \p\p
                '      temp3 = Me.regexReplace(temp3, "(.*?)(\\..?)*(.*?)", "$1$3")
                ' remove --
                '     temp3 = Me.regexReplace(temp3, "(.*?)(--)(.*?)", "$1$3")
                ' if nothing to say then toss it out
                '    If temp3 = Nothing Then
                '' toss out as no text to speak
                ' i -= 1
                '  Else
                ' keep this

                ' must use double on characters quotes in order to handle "Herodias' daughter"
                temp2 = regexReplace(temp, "(.+?)(character1="""")(.+)", "$2")
                If temp2.StartsWith("character1=""") Then
                    ' no characters in this line to use .. = unidentified speaker
                    sCharacter(i, 1) = ""
                    iNumberOfCharactersInClip(i) = 0

                Else
                    ' there is at least one character
                    For j = 1 To iMaxCharacters
                        '      sCharacter(i, j) = regexReplace(temp, "(.+\s)(character" & j.ToString & "=')(.*?)(.+)", "$3")
                        temp2 = regexReplace(temp, "(.+?character" & j.ToString & "="")" & "(.*?)("".+)", "$2")
                        ' if temp2 starts with <clip .... that means that it failed above test
                        If temp2.StartsWith("<clip") Then
                            ' no match found so skip looking for more
                            Exit For
                        End If
                        sCharacter(i, j) = temp2
                        iNumberOfCharactersInClip(i) = j
                    Next
                End If
                sID(i) = regexReplace(temp, "(.+?id="")(.+?)("".+)", "$2")
                sBook(i) = regexReplace(temp, "(.+\s)(id="")(.*?)(\s.+)", "$3")
                sChapter(i) = regexReplace(temp, "(.+\s)(id="".*?\s)(.*?)(\..+)", "$3")
                sVerse(i) = regexReplace(temp, "(.+\s)(id="".*?\.)(.*?)("".+)", "$3")
                ' xxxxsprompt(i) = regexReplace(temp, "(.+\s)(id='.*?\.)(.*?)('.+)", "$3")
                temp2 = regexReplace(temp, "(.+\s)(tag="")(.*?)("".+)", "$3")
                If temp2.StartsWith("<clip") Then
                    ' no match found so 
                    sTag(i) = Nothing
                Else
                    sTag(i) = temp2
                End If
                '   sSpeakerNumber(i) = regexReplace(temp, "(.+\s)(voice=')(.*?)('.+)", "$3")
                ' sScript(i) = (regexReplace(temp, "(.+>)(.*?)(<.+)", "$2")).ToString.Trim
                sScript(i) = (regexReplace(temp, "(<clip.*?>)(.*?)(</clip.+)", "$2")).ToString.Trim
                ' End If
            Loop     ' next line is the </clip>
            sr.Close()
            '     System.IO.File.Copy(sTempFileName, sMasterFileName) ' makes it less likely we will loose data on power failure
            MainMenu.progressBar.Visible = False
            MainMenu.tbProgress.Visible = False
            'ProgressIndicator.Hide()
            Me.iLastClipNumber = i ' some are blank though
        Catch ex As Exception
            MessageBox.Show("Unable to read file " & sMasterFileName & vbCrLf & "Check to see if file is open in another application." & ex.Message, "File writing error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
    End Sub
    Private Sub resetOmitToFalse()
        Dim i As Integer
        For i = 1 To Me.iLastClipNumber
            blnOmit(i) = False

        Next
    End Sub
    Private Sub resetblnToFalseAndStringToNul()
        Dim i As Integer
        For i = 1 To Me.iLastClipNumber
            blnRecorded(i) = False
            blnOmit(i) = False
            Me.sCharacter(i, 0) = ""
            Me.sCharacter(i, 1) = ""
            Me.sCharacter(i, 2) = ""
            Me.sCharacter(i, 3) = ""
            Me.sCharacter(i, 4) = ""
            Me.sCharacter(i, 5) = ""
            Me.sContinued(i) = ""
        Next
    End Sub
    Private Sub resetArrays()
        resetblnToFalseAndStringToNul()

        'Me.blnRecorded.Initialize()
        '  Me.blnOmit.Initialize() didnt work
        'resetOmitToFalse()
        ' Me.sTag.Initialize()
        Me.iNumberOfCharactersInClip.Initialize()
        Me.iNumberToRecord.Initialize()
        Me.iSpeakerFileColor.Initialize()
        Me.iSpeakerFileSpeakerNumber.Initialize()
        'Me.sAllClips.Initialize()
        'Me.sBook.Initialize()
        'Me.sChapter.Initialize()
        'Me.sCharacterPrompts.Initialize()
        'Me.sSpeakerFileCharacterShort.Initialize()
        'Me.sCharacter.Initialize()
        ' resetsCharacterToNothing()

        ' Dim x As Integer
        'For x = 1 To 600
        'If sCharacter(x, 0) <> Nothing Then
        'Dim y = 1
        ' End If
        ' Next

        'Me.sClipSize.Initialize()
        'Me.sContinued.Initialize()
        'Me.sID.Initialize()
        '    Me.sPrompt.Initialize()
        'Me.sScript.Initialize()
        'Me.sSpeakerNumber.Initialize()
        'Me.sTextArray.Initialize()
        'Me.sVerse.Initialize()
        'Me.sSpeakerFileCharacter.Initialize()
        'Me.sCharacter.Initialize()
    End Sub
    Public Sub readClipsFromFileMaster()
        createFoldersAndMasterAndScriptsFileNames()
        '<clip  book=GEN chapter=2 verse=23 tag= voice= prompt= recorded=False multiple=1 character1=narrator-GEN >yeno.<verse>24</verse> Adýma nangaro kwa yanju-a bawanju-a kolje kamunjuro kýrdiye kam fallo waljaidý.<verse>25</verse> Kamdý-a kamudý-a tiyinja de, kattenjan nangu bawo.</clip>
        '<clip  book=GEN chapter=3 verse=1 tag=\c voice= prompt= recorded=False multiple=1 character1=book-chapter >3</clip>
        resetArrays()
        Me.iUnidentifiedSpeakingCharacter = 0
        Me.iMultipleSpeakingCharacter = 0

        Dim i As Integer
        Dim temp As String
        If File.Exists(Me.sMasterFileName) Then
            Try
                Dim sr As StreamReader = New StreamReader(Me.sMasterFileName, Encoding.UTF8)
                temp = sr.ReadLine() ' xml
                temp = sr.ReadLine()
                iLastClipNumber = getInitialInfo(temp)
                Do While Not sr.EndOfStream
                    i += 1  ' count clip numbers as iLastClipNumber is wrong at this time.
                    '  Me.progressBar.Value = i
                    ' Me.progressBar.Update()
                    temp = sr.ReadLine()
                    If temp.StartsWith("</dramatizer>") Then Exit Do
                    If isThereAnySpeakingPart(temp) = True Then

                        readBookChapterVerse(temp, i)
                        sContinued(i) = regexReplace(temp, "(.+\s)(continued="")(.*?)(""\s.+)", "$3")
                        getOmit(temp, i)
                        sClipSize(i) = regexReplace(temp, "(.+\s)(size="")(.*?)(""\s.+)", "$3")
                        getNumberOfCharactersInClip(temp, i)
                        readRecorded(temp, i)
                        sScript(i) = ""
                        sScript(i) = (regexReplace(temp, "(<clip.+?>)(.*?)(</clip.+)", "$2")).ToString.Trim
                        sSpeakerNumber(i) = ""
                        sSpeakerNumber(i) = regexReplace(temp, "(.+\s)(speaker="")(.*?)(""\s.+)", "$3")
                        ' prompt is carried in the character name
                        '  sPrompt(i) = regexReplace(temp, "(.+\s)(prompt="")(.*?)("".+)", "$3")
                        sContinued(i) = regexReplace(temp, "(.+\s)(continued="")(.*?)("".+)", "$3")
                        countUndentifiedSpeakingCharacgters_OneSpeakingCharacter_AndMultipleSpeakingCharacters(i)
                        ' read in all the characters
                        getCharacters(temp, i)
                        getTag(temp, i)
                    Else
                        ' no speaking part
                        i -= 1
                    End If
                Loop
                sr.Close()
                ' ProgressIndicator.Hide()
                '    Me.progressBar.Visible = False
                ' Me.tbProgress.Visible = False
                Me.iLastClipNumber = i - 1  ' this is required to set it right
                '    Dim x3 As String = Me.sCharacter(77, 1)
                '   Dim x4 As String = Me.sCharacter(79, 1)
                '  Dim x1 = x4
            Catch ex As Exception
                MessageBox.Show("Unable to read master file -- error 2 " & vbCrLf & sMasterFileName & vbCrLf & "Check to see if file is open in another application. " & vbCrLf & ex.Message & vbCrLf & "i = " & i.ToString, "File reading error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End
            End Try
        Else
            ' no master file so skip reading
        End If
    End Sub
    Public Function isThereAnySpeakingPart(ByVal temp As String)
        Dim temp3 As String
        ' extract script
        temp3 = (Me.regexReplace(temp, "(<clip.*?>)(.*?)(</clip>)", "$2")).ToString.Trim
        ' remove verses
        temp3 = Me.regexReplace(temp3, "(.*?)(<verse>.*?</verse>)(.*?)", "$1$3")
        ' remove \q, \p, \pc, q2, ..., \p\p
        temp3 = Me.regexReplace(temp3, "(.*?)(\\.\d?)*(.*?)", "$1$3")
        ' remove --
        temp3 = Me.regexReplace(temp3, "(.*?)(--)(.*?)", "$1$3")
        ' if nothing to say then toss it out
        If temp3.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Private Function getInitialInfo(ByVal temp As String)
        Dim total = regexReplace(temp, "(.+\s)(totalClips="")(.*?)(""\s.+)", "$3")
        Dim sRecordingInProgress As String = regexReplace(temp, "(.+\s)(recordingInProgress="")(.*?)(""\s.+)", "$3")
        ' probably not right xxxxxxxxxxxx
        ' MainMenu.setRecordingStatus(sRecordingInProgress)
        '   ProgressIndicator.Visible = True
        ' Me.progressBar.Visible = True
        ' Me.progressBar.Maximum = total + total * 0.5
        ' Me.progressBar.Step = 100
        Return total
    End Function
    Private Sub getNumberOfCharactersInClip(ByVal temp As String, ByVal i As Integer)
        Dim itemp As String
        itemp = regexReplace(temp, "(.+\s)(multiple="")(.*?)(""\s.+)", "$3")
        iNumberOfCharactersInClip(i) = Convert.ToInt32(itemp)
    End Sub
    Private Sub getTag(ByVal temp As String, ByVal i As Integer)
        Dim temp2 As String
        temp2 = regexReplace(temp, "(.+\s)(tag="")(.*?)("".+)", "$3")
        If temp2.StartsWith("<clip") Then
            ' no match found so skip looking for more
            sTag(i) = Nothing
        Else
            sTag(i) = temp2
        End If
    End Sub
    Private Sub getCharacters(ByVal temp As String, ByVal i As Integer)
        Dim j As Integer
        sCharacter(i, 0) = ""
        sCharacter(i, 1) = ""
        sCharacter(i, 2) = ""
        sCharacter(i, 3) = ""
        sCharacter(i, 4) = ""
        For j = 0 To iNumberOfCharactersInClip(i)
            sCharacter(i, j) = regexReplace(temp, "(.+\s)(character" + j.ToString + "="")(.*?)(""\s.+)", "$3")
        Next
    End Sub
    Private Sub countUndentifiedSpeakingCharacgters_OneSpeakingCharacter_AndMultipleSpeakingCharacters(ByVal i As Integer)
        Select Case iNumberOfCharactersInClip(i)
            Case 0
                iUnidentifiedSpeakingCharacter += 1
            Case 1
                iOneSpeakingCharacter += 1
            Case Is > 1
                iMultipleSpeakingCharacter += 1
            Case Else
                ' should never happen
                Debug.Assert(False, "number of characters negative ... this should never happen")
        End Select
    End Sub
    Private Sub readRecorded(ByVal temp As String, ByVal i As Integer)
        Dim blnTemp As Boolean
        blnTemp = regexReplace(temp, "(.+\s)(recorded="")(.*?)(""\s.+)", "$3")
        blnRecorded(i) = False
        blnRecorded(i) = Convert.ToBoolean(blnTemp)
    End Sub
    Private Sub getOmit(ByVal temp As String, ByVal i As Integer)
        Dim blnTemp As Boolean
        blnTemp = regexReplace(temp, "(.+\s)(omit="")(.*?)(""\s.+)", "$3")
        ' in case this is first time through and not set up yet all omits are false
        blnOmit(i) = False
        Try
            blnOmit(i) = Convert.ToBoolean(blnTemp)
        Catch ex As Exception
            ' false
        End Try
        If blnOmit(1) = True Then
            Dim x = 1
        End If

    End Sub
    Private Sub readBookChapterVerse(ByVal temp As String, ByVal i As Integer)
        sBook(i) = regexReplace(temp, "(.+\s)(book="")(.*?)(""\s.+)", "$3")
        sChapter(i) = regexReplace(temp, "(.+\s)(chapter="")(.*?)(""\s.+)", "$3")
        sVerse(i) = regexReplace(temp, "(.+\s)(verse="")(.*?)(""\s.+)", "$3")
    End Sub
    Public Function getCharacterShort(ByVal character)
        Dim temp As String
        temp = regexReplace(character, "(\[.+])|(\(.+)", "")
        temp = Trim(temp)
        Return temp
    End Function
    Public Sub writeCurrentSettings()
        Try
            FileOpen(filenum, sINIfile, OpenMode.Output, OpenAccess.Write)
            WriteLine(filenum, "<settings>")
            WriteLine(filenum, "<currentFilePathAndName>")
            WriteLine(filenum, sProjectFileName)
            WriteLine(filenum, "</currentFilePathAndName>")
            WriteLine(filenum, "<currentTextEncoding>")
            WriteLine(filenum, sTextEncoding)
            WriteLine(filenum, "</currentTextEncoding>")
            WriteLine(filenum, "<currentFont>")
            Try
                WriteLine(filenum, Me.rtbEncodingANSI.Font.ToString)
            Catch ex As Exception
                ' ignore problem
            End Try
            WriteLine(filenum, "</currentFont>")
            WriteLine(filenum, "<currentQuoteType>")
            WriteLine(filenum, Me.cbQuoteType.Text)
            WriteLine(filenum, "</currentQuoteType>")
            WriteLine(filenum, "<lastClipNumber>")
            WriteLine(filenum, Me.iLastClipNumber)
            WriteLine(filenum, "</lastClipNumber>")
            WriteLine(filenum, "<ISOcode>")
            WriteLine(filenum, Me.tbISOcode.Text)
            WriteLine(filenum, "</ISOcode>")
            WriteLine(filenum, "<omitSectionHeads>")
            WriteLine(filenum, MasterText.chkbxSectionHeads.Checked())
            WriteLine(filenum, "</omitSectionHeads>")
            WriteLine(filenum, "<omitChapterNumbers>")
            WriteLine(filenum, MasterText.chkbxChapterNumbers.Checked())
            WriteLine(filenum, "</omitChapterNumbers>")
            WriteLine(filenum, "<omitIntroduction>")
            WriteLine(filenum, MasterText.chkbxIntroduction.Checked())
            WriteLine(filenum, "</omitIntroduction>")
            WriteLine(filenum, "<omitHeading>")
            WriteLine(filenum, MasterText.chkbxHeading.Checked())
            WriteLine(filenum, "</omitHeading>")
            WriteLine(filenum, "<audioProgram>")
            WriteLine(filenum, Me.cbAudioProgram.Text)
            WriteLine(filenum, "</audioProgram>")
            WriteLine(filenum, "<maxClipSize>")
            WriteLine(filenum, Me.TrackBarClipSize.Value)
            WriteLine(filenum, "</maxClipSize>")
            WriteLine(filenum, "<outputFolderName>")
            WriteLine(filenum, Me.cbOutputFolder.Text)
            WriteLine(filenum, "</outputFolderName>")
            WriteLine(filenum, "<breakAtEachParagraph>")
            WriteLine(filenum, Me.chkbxBreakAtParagraphs.Checked())
            WriteLine(filenum, "</breakAtEachParagraph>")
            WriteLine(filenum, "<adjustClipSize>")
            WriteLine(filenum, Me.chkbxAdjustClipSize.Checked())
            WriteLine(filenum, "</adjustClipSize>")
            WriteLine(filenum, "<language>")
            Try
                Dim x = Me.sSavedLanguage
                WriteLine(filenum, MainMenu.cbLanguage.SelectedIndex)
            Catch ex As Exception
                WriteLine(filenum, 1)
            End Try
            WriteLine(filenum, "</language>")
            WriteLine(filenum, "</settings>")
            FileClose(filenum)
        Catch ex As Exception
            '    MessageBox.Show("Error trying to write the zany.ini file.", "Can not create file", MessageBoxButtons.OK, MessageBoxIcon.Information)
            FileClose(filenum)
        End Try
    End Sub
    Public Sub readCurrentSettings()
        Dim temp As String = ""
        If System.IO.File.Exists(sINIfile) Then
            Try
                FileOpen(filenum, sINIfile, OpenMode.Input, OpenAccess.Read)
                readAndSetCurrentFile()
                readAndSetEncoding()
                readAndSetSelectedFontInRTBcontrol()
                readAndSetQuoteType()
                readAndSetLastClipNumber() ' but if file was erased set to 0
                readAndSetISOcode()
                readAndSetOmitSectionHeads()
                readAndSetOmitChapterNumbers()
                readAndSetOmitIntroduction()
                readAndSetOmitHeading()
                readAndSetAudioProgram()
                readAndSetMaxClipSize()
                readAndSetOutputFolderName()
                readAndSetBreakAtEachParagraph()
                readAndSetAdjustClipSize()
                Try
                    readLanguage()
                Catch ex As Exception
                End Try
                FileClose(filenum)
            Catch ex As Exception
                ' if can't read a settings file then just display defaults
                '   MessageBox.Show("error " & ex.Message, "Error")
                FileClose(filenum)
            End Try
        Else
            ' do nothing
        End If
        Return
    End Sub
    Private Sub readAndSetAdjustClipSize()
        Dim temp As String = getCurrentInfoFromSettingsFile("<adjustClipSize>")
        If temp = "#TRUE#" Then
            Me.chkbxAdjustClipSize.Checked = True
        Else
            Me.chkbxAdjustClipSize.Checked = False
        End If
    End Sub
    Private Sub readAndSetBreakAtEachParagraph()
        Dim temp As String = getCurrentInfoFromSettingsFile("<breakAtEachParagraph>")
        If temp = "#TRUE#" Then
            Me.chkbxBreakAtParagraphs.Checked = True
        Else
            Me.chkbxBreakAtParagraphs.Checked = False
        End If
    End Sub
    Private Sub readAndSetOutputFolderName()
        Me.cbOutputFolder.Text = getCurrentInfoFromSettingsFile("<outputFolderName>")
    End Sub
    Private Sub readAndSetMaxClipSize()
        Me.TrackBarClipSize.Value = getCurrentInfoFromSettingsFile("<maxClipSize>")
    End Sub
    Private Sub readAndSetAudioProgram()
        Me.cbAudioProgram.Text = getCurrentInfoFromSettingsFile("<audioProgram>")
    End Sub
    Private Sub readAndSetOmitSectionHeads()
        Dim temp As String = getCurrentInfoFromSettingsFile("<omitSectionHeads>")
        If temp = "#TRUE#" Then
            MasterText.chkbxSectionHeads.Checked = True
        Else
            MasterText.chkbxSectionHeads.Checked = False
        End If
    End Sub
    Private Sub readAndSetOmitChapterNumbers()
        Dim temp As String = getCurrentInfoFromSettingsFile("<omitChapterNumbers>")
        If temp = "#TRUE#" Then
            MasterText.chkbxChapterNumbers.Checked = True
        Else
            MasterText.chkbxChapterNumbers.Checked = False
        End If
    End Sub
    Private Sub readAndSetOmitIntroduction()
        Dim temp As String = getCurrentInfoFromSettingsFile("<omitIntroduction>")
        If temp = "#TRUE#" Then
            MasterText.chkbxIntroduction.Checked = True
        Else
            MasterText.chkbxIntroduction.Checked = False
        End If
    End Sub
    Private Sub readAndSetOmitHeading()
        Dim temp As String = getCurrentInfoFromSettingsFile("<omitHeading>")
        If temp = "#TRUE#" Then
            MasterText.chkbxHeading.Checked = True
        Else
            MasterText.chkbxHeading.Checked = False
        End If
    End Sub
    Private Sub readAndSetISOcode()
        Me.sISOCode = getCurrentInfoFromSettingsFile("<ISOcode>")
        Me.tbISOcode.Text = Me.sISOCode
    End Sub
    Private Sub readAndSetLastClipNumber()
        Me.iLastClipNumber = getCurrentInfoFromSettingsFile("<lastClipNumber>")
    End Sub
    Private Sub readAndSetCurrentFile()
        sProjectFileName = getCurrentInfoFromSettingsFile("<currentFilePathAndName>")
        Me.cbFileName.Text = sProjectFileName
        sProjectName = getFileNameWithoutExtensionFromFullName(sProjectFileName)
        Me.fillSampleEncodingRTBoxes()
    End Sub
    Private Sub readLanguage()
        Dim temp As Integer
        Try
            temp = getCurrentInfoFromSettingsFile("<language>")
            Me.iSavedLanguageIndexFromFile = temp
        Catch ex As Exception
            ' default language English is set in 
            MainMenu.cbLanguage.SelectedIndex = 0
        End Try
    End Sub
    '  Private Sub readAndSetLanguage()
    'Dim temp As String
    '   temp = getCurrentInfoFromSettingsFile("<language>")
    '  If temp = "" Then
    '  '    Me.cbQuoteType.Text = Me.sQuoteTypeStraight
    'Else
    '   MainMenu.cbLanguage.Text = temp
    '      End If
    ' End Sub
    Private Sub readAndSetQuoteType()
        Dim temp As String
        temp = getCurrentInfoFromSettingsFile("<currentQuoteType>")
        If temp = "" Then
            ' default
            Me.cbQuoteType.Text = Me.sQuoteTypeCheverons
        ElseIf temp = " ... " Then
            Me.cbQuoteType.Text = Me.sQuoteTypeStraight
        Else
            Me.cbQuoteType.Text = temp
        End If
    End Sub
    Private Sub readAndSetEncoding()
        sTextEncoding = getCurrentInfoFromSettingsFile("<currentTextEncoding>")
        If sTextEncoding = "ANSI" Then
            Me.rbEncodingANSI.Checked = True
            '     Me.rbEncodingUTF8.Checked = False
        Else
            '    Me.rbEncodingANSI.Checked = False
            Me.rbEncodingUTF8.Checked = True
        End If
    End Sub
    Private Function getCurrentInfoFromSettingsFile(ByVal sCurrentItem As String)
        Do
        Loop Until inputLine() = sCurrentItem
        Return inputLine()
    End Function
    Private Sub readAndSetSelectedFontInRTBcontrol()
        ' [Font: Name=Manga SILCharis, Size=15.75, Units=3, GdiCharSet=0, GdiVerticalFont=False]
        Dim fontName As String
        Dim fontSize As String
        Dim temp = getCurrentInfoFromSettingsFile("<currentFont>")
        fontName = regexReplace(temp, "(.+Name=)(.+?)(, Size.+?])", "$2")
        fontSize = regexReplace(temp, "(.+Size=)(.+?)(, Units.+?])", "$2")
        SpeakerText.rtbText.Font = New Font(fontName, fontSize)
        MasterText.rtbTextWithContext.Font = New Font(fontName, fontSize)
        MasterText.rtbContextAbove.Font = New Font(fontName, fontSize - 2)
        Me.cbFontName.Text = fontName
        Me.cbFontSize.Text = fontSize
    End Sub
    Private Sub ButtonParatextProject_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Paratext files (*.ptx, *.ssf, *.lds)|*.ptx;*.ssf;*.lds|All files (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        sProjectParatextFileName = OpenFileDialog1.FileName
    End Sub
    Private Function inputLine()
        Dim temp As String
        temp = LineInput(filenum)
        temp = Replace(temp, """", "")
        Return temp
    End Function
    Private Function string2stream(ByVal testString As String)
        ' create unicode encoding
        Dim utf8Encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
        ' convert string to byte array using encoding
        Dim bytes As Byte() = utf8Encoding.GetBytes(testString)
        ' initialize memory steam using byte array
        Dim convertedMemoryStream As Stream = New MemoryStream(bytes)
        Return convertedMemoryStream
    End Function
    Public Sub New_Project_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Text files (*.txt, *.ptx,*.sfm)|*.txt;*.ptx;*.sfm|All files (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        sProjectFileName = OpenFileDialog1.FileName
        sProjectName = getFileNameWithoutExtensionFromFullName(sProjectFileName)
        sProjectPath = getProjectPathFromFullName(sProjectFileName)
        Me.Text = sProjectName + " - " + sProgramName
        Me.fillSampleEncodingRTBoxes()
        setFontForRTB()
        ' loadDocumentIntoScriptureFileStream()
    End Sub
    Private Function getProjectPathFromFullName(ByVal sProjectFileName As String)
        Dim temp As String
        temp = str.Left(sProjectFileName, str.InStrRev(sProjectFileName, "\"))
        Return temp
    End Function
    Private Sub End_Project_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        End
    End Sub
    Public Sub saveStringAsFile(ByVal sToSave As String, ByVal sFileName As String)
        '   Dim sPathAndFileName As String = sProjectPath & "\" & sFileName
        Dim sPathAndFileName As String = "c:\temp\" & sFileName
        Dim sw As StreamWriter = New StreamWriter(sPathAndFileName, False, Encoding.UTF8)
        sw.WriteLine(sToSave)
        sw.Close()
    End Sub
    Public Function convertStringToUTF8(ByVal sToSave As String, ByVal sFileName As String)
        Dim sPathAndFileName As String = "c:\temp\" & sFileName
        saveStringAsFile(sToSave, "test")
        Dim sr As StreamReader = New StreamReader(sPathAndFileName, Encoding.UTF8)
        Dim temp As String = sr.ReadToEnd
        sr.Close()
        Return temp
    End Function
    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' test area
    End Sub
    ' Private Sub FontToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Me.FontDialog1.ShowDialog()
    '   selectedFont = Me.FontDialog1.Font
    '  VoiceTalentText.rtbText.Font = selectedFont
    ' MasterText.rtbTextSmall.Font = selectedFont
    '     writeCurrentSettings()
    ' End Sub
    Public Function regexReplace(ByVal sInput As String, ByVal sFind As String, ByVal sReplace As String)
        Try
            ' nul input allowed
            Dim expression As Regex
            expression = New Regex(sFind)
            Return expression.Replace(sInput, sReplace)
        Catch ex As Exception
            Return sInput
        End Try
    End Function
    Private Sub cbFontName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFontName.SelectedIndexChanged
        setFontForRTB()
    End Sub
    Private Sub cbFontSize_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFontSize.SelectedIndexChanged
        setFontForRTB()
    End Sub
    Private Sub rbEncodingUTF8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If Me.rbEncodingUTF8.Checked Then
        Else
        End If
    End Sub
    Public Sub initializeText()
        createFoldersAndMasterAndScriptsFileNames()
        deleteMasterFile()
        Me.resetArrays()
        identifyAllQuotes()
        iLastClipNumber = buildArrayOfClips()  ' should set last correctly xxxxxxxxxxxxxxxx
        readClipsFromFileOneLinePerClip()
        readSpeakerFile()
        assignVoicesToCharacters()
        '    setDefaultCharacters()
        Me.writeClipsToMasterFileAndAdjustClipSize(Me.chkbxAdjustClipSize.Checked()) ' adjust clip size True
        '    writeCurrentSettings()
        '     Me.readClipsFromFileMaster()
        Me.iCurrentClipNumber = 1 ' always start at 1
        ' ForwardBackControl.displayMasterAndVoiceTalentText()
        MainMenu.showStatsForUnidentifiedMultipleTotal()
    End Sub
    Private Sub rbQuoteChevrons_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        sOpeningQuote = "«"
        sClosingQuote = "»"
    End Sub
    Private Sub btnHighlightQuote_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        splitOutAllFirstLevelQuotes()
    End Sub
    Private Sub fillSampleEncodingRTBoxes()
        ' Me.rtbEncodingANSI.Text = sProjectFileName
        ' check file to be sure it is valid
        Try
            If Me.rbEncodingANSI.Checked = True Then
                Dim ansitext As String = file2stringWithEncoding(sProjectFileName, System.Text.Encoding.UTF7)
                ansitext = regexReplace(ansitext, "\r", "oojaaooo")
                ansitext = regexReplace(ansitext, "\n", "--jaa---")
                '  ansitext = regexReplace(ansitext, "(.*?)(\\c 4)(.*)", "$1") ' cut file
                ansitext = regexReplace(ansitext, "--jaa---", vbCrLf)
                ansitext = regexReplace(ansitext, "oojaaooo", vbCrLf)
                Me.rtbEncodingANSI.Text = ansitext
                Me.rtbEncodingANSI.Show()
                Me.rtbEncodingUTF8.Hide()
            Else
                Dim utf8text As String = file2stringWithEncoding(sProjectFileName, System.Text.Encoding.UTF8)
                utf8text = regexReplace(utf8text, "\r", "oojaaooo")
                utf8text = regexReplace(utf8text, "\n", "--jaa---")
                utf8text = regexReplace(utf8text, "(.*?)(\\c 4)(.*)", "$1") ' cut file
                utf8text = regexReplace(utf8text, "--jaa---", vbCrLf)
                utf8text = regexReplace(utf8text, "oojaaooo", vbCrLf)
                Me.rtbEncodingUTF8.Text = utf8text
                Me.rtbEncodingANSI.Hide()
                Me.rtbEncodingUTF8.Show()
            End If
        Catch ex As Exception
            ' MessageBox.Show("Error 1 trying to open " & Me.sProjectFileName & vbCrLf & "See if another application has the file open.", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
        Me.panelEncoding.Show()
    End Sub
    Public Function file2stringWithEncoding(ByVal myFile As String, ByVal encEncoding As System.Text.Encoding)
        Dim myString As String
        ' Create an instance of StreamReader to read from a file.
        Dim sr As StreamReader = New StreamReader(myFile, encEncoding)
        sr.BaseStream.Position = 0
        myString = sr.ReadToEnd
        sr.Close()
        Return myString
    End Function
    Private Sub btnContinueWithMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.readClipsFromFileMaster()
        dramatizer.displayMasterAndVoiceTalentText()
    End Sub
    Private Sub verifyFileIsNotDOC(ByVal blnDocFile As Boolean)
        If blnDocFile = True Then
            Dim response = MessageBox.Show("This seems to be a .doc file. Please convert it to .txt." & vbCrLf & _
                       "In Microsoft Word you will do a file save as Plain text (*.txt)." & vbCrLf & _
                       "Do you want to stop and do that now?", "Wrong file type.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If response = vbYes Then
                End
            Else
                ' we will loop until it is fixed
            End If
        Else
            ' not a doc file ... good
            ' see if first line starts with \id
            verifyFirstLineStartsWithID()
        End If

    End Sub
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Dim blnDocFile As Boolean
        Do

            OpenFileDialog1.FileName = ""
            OpenFileDialog1.Filter = "Text files (*.txt, *.ptx,*.sfm)|*.txt;*.ptx;*.sfm|All files (*.*)|*.*"
            OpenFileDialog1.ShowDialog()
            sProjectFileName = OpenFileDialog1.FileName
            blnDocFile = sProjectFileName.Contains(".doc")
            verifyFileIsNotDOC(blnDocFile)
        Loop Until blnDocFile = False
        sProjectName = getFileNameWithoutExtensionFromFullName(sProjectFileName)
        sProjectPath = getProjectPathFromFullName(sProjectFileName)
        Me.cbFileName.Text = sProjectFileName
        Me.Text = sProjectName + " - " + sProgramName
        MainMenu.Text = Me.Text

        Me.panelEncoding.Show()
        Me.fillSampleEncodingRTBoxes()
        setFontForRTB()
        If File.Exists(Me.sMasterFileName + ".sav") Then File.Delete(Me.sMasterFileName + ".sav")
        If File.Exists(Me.sMasterFileName) Then
            File.Copy(Me.sMasterFileName, Me.sMasterFileName + ".sav")
            File.Delete(Me.sMasterFileName)
        End If
        MainMenu.setCheckMarksAndEnableMenuItems()
        resetVariables()
    End Sub
    Private Sub VScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar1.Scroll
        If Me.rbEncodingANSI.Checked = True Then
            Me.rtbEncodingANSI.Text = sAllClips(e.OldValue)
            Me.rtbEncodingANSI.BackColor = Color.LightGoldenrodYellow
        Else
            Me.rtbEncodingUTF8.Text = sAllClips(e.OldValue)
            Me.rtbEncodingANSI.BackColor = Color.LightCyan
        End If
        Me.lblCurrentPosition.Text = e.OldValue.ToString
        Me.VScrollBar1.Show()
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
        MainMenu.Show()
    End Sub
    Private Sub btnDisplayClips_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisplayClips.Click
        fillSampleEncodingRTBoxes() ' in case we are doing this again
        ' reset updown to 0
        VerticalScroll.Value = 0
        Dim temp As String
        If Me.rbEncodingANSI.Checked Then
            ' do work here
            temp = regexReplace(Me.rtbEncodingANSI.Text, "\n", "--jaa---")
        Else
            temp = regexReplace(Me.rtbEncodingUTF8.Text, "\n", "--jaa---")
            ' do work on UTF8 box
        End If
        temp = SFMQuotes.processQuotesToMakeRegular(temp)
        temp = SFMQuotes.processRemoveUnusedText(temp)
        ' remove note saying what was removed
        temp = regexReplace(temp, "(\*\*\*\*)(.*?)(\*\*\*\*)", "")
        temp = regexReplace(temp, "\r", "oojaaooo")
        temp = regexReplace(temp, "(«)(.*?)(»)", "|" & "$1$2$3" & "|")
        temp = regexReplace(temp, "--jaa---", vbCrLf)
        temp = regexReplace(temp, "oojaaooo", vbCrLf)
        temp = regexReplace(temp, vbCrLf & vbCrLf, vbCrLf)
        temp = regexReplace(temp, "\|\|", "\|")
        sAllClips = temp.Split("|")
        sAllClips = removeBlanksFromArray(sAllClips)
        iRawClipsTotal = sAllClips.Length
        Me.lblCurrentPosition.Text = "0"
        Me.VScrollBar1.Maximum = iRawClipsTotal
        '  Me.VScrollBar1_Scroll(btnDisplayClips, 1)
    End Sub
    Private Function removeBlanksFromArray(ByVal temp() As String)
        Dim temp2(temp.Length) As String
        Dim i As Int16
        Dim skip As Int16 = 0
        Dim sString As String
        For i = 0 To temp.Length - 1
            sString = temp(i)
            If sString = " " Then
                ' skip
                skip += 1
            Else
                temp2(i - skip) = sString
            End If
        Next
        ' ReDim Preserve temp2(temp.Length - skip)
        Return temp2
    End Function
    '  Private Sub btnFontColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '     Me.ColorDialog1.SolidColorOnly = False
    '    Me.ColorDialog1.ShowDialog()
    '   Me.sFontColor = Me.ColorDialog1.Color.ToString
    '  Me.rtbEncodingANSI.BackColor = Me.fontColor
    ' Me.rtbEncodingANSI.ForeColor = Me.fontColor
    ' VoiceTalentText.BackColor = Me.fontColor
    '     MasterText.BackColor = Me.fontColor
    ' End Sub
    '  Private Sub btnFontBackgroundColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '     Me.ColorDialog1.SolidColorOnly = True
    '    Me.ColorDialog1.ShowDialog()
    '   Me.fontBackgroundColor = Me.ColorDialog1.Color
    '  Me.rtbEncodingANSI.BackColor = Me.fontBackgroundColor
    ' Me.rtbEncodingUTF8.BackColor = Me.fontBackgroundColor
    '    VoiceTalentText.BackColor = Me.fontBackgroundColor
    '   MasterText.BackColor = Me.fontBackgroundColor
    ' End Sub
    'Private Sub cbFontSize_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '   changeFont()
    ' End Sub
    ' Private Sub cbFontName_SelectedIndexChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '     changeFont()
    ' End Sub
    Public Sub changeFont()
        Dim fontName As String = cbFontName.SelectedItem
        Dim fontSize As String = cbFontSize.SelectedItem
        If cbFontName.SelectedItem = Nothing Then
            fontName = cbFontName.Text
        End If
        If cbFontSize.SelectedItem = Nothing Then
            If cbFontSize.Text = "" Then
                fontSize = 14
            Else
                fontSize = cbFontSize.Text
            End If
        End If
        Dim x
        For x = 1 To 10
            '  me.cbFontName.Items.Add (
        Next
        Me.rtbEncodingANSI.Font = New Font(fontName, fontSize)
        Me.rtbEncodingUTF8.Font = New Font(fontName, fontSize)
        SpeakerText.rtbText.Font = New Font(fontName, fontSize)
        MasterText.rtbContextAbove.Font = New Font(fontName, fontSize - 2)
        MasterText.rtbTextOnly.Font = New Font(fontName, fontSize)
        MasterText.rtbTextWithContext.Font = New Font(fontName, fontSize)
    End Sub
    Private Sub setEncoding()
        If Me.rbEncodingANSI.Checked Then
            Me.rtbEncodingANSI.Show()
            Me.rtbEncodingUTF8.Hide()
            sTextEncoding = "ANSI"
        Else
            Me.rtbEncodingANSI.Hide()
            Me.rtbEncodingUTF8.Show()
            sTextEncoding = "UTF8"
        End If
    End Sub
    Private Sub btnSetOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetOptions.Click
        ' checks : fileName, quoteType, ISO code
        If Me.areRequiredTabsCompleted() Then
            ' If checkISOcodePresent() And Me.checkQuoteTypePresent And Me.checkFileNamePresent Then
            Me.writeCurrentSettings()
            ' moved to starting
            ' createFoldersAndMasterAndScriptsFileNames()
            Me.Hide()
            MainMenu.setCheckMarksAndEnableMenuItems()
            'Me.enableMenuChoices()
            MainMenu.Show()

        Else
            Beep()
            MessageBox.Show(Me.sLocalizationStrings(Me.iRequiredInformationMissing, Me.iLanguageSelected), Me.sLocalizationStrings(Me.iPleaseCorrect, Me.iLanguageSelected), MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
    Public Sub createFoldersAndMasterAndScriptsFileNames()
        '      Public sDramatizeFolder As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\My Dramatizer"
        If Me.tbISOcode.Text = "Id" Then
            ' skip as not set yet
        Else
            sDramatizeFolder = Me.cbOutputFolder.Text
            sLanguageFolder = sDramatizeFolder & "\" & Me.tbISOcode.Text
            sProjectFolder = Me.sLanguageFolder & "\" & Me.sProjectName
            sScriptsFolder = Me.sProjectFolder & "\" & "scripts"
            sRecordingFolder = Me.sProjectFolder & "\" & "recordings"
            sScriptSpeakerFile = sScriptsFolder & "\ScriptForSpeakerNumber-"
            sMasterFileName = Me.sProjectFolder & "\" & "master.txt"
            If File.Exists(sMasterFileName) Then
                Dim x
                x = 1
            End If
            ifDirectoryDoesNotExistCreateIt(Me.sLanguageFolder)
            ifDirectoryDoesNotExistCreateIt(Me.sProjectFolder)
            ifDirectoryDoesNotExistCreateIt(Me.sScriptsFolder)
            ifDirectoryDoesNotExistCreateIt(Me.sRecordingFolder)
        End If
    End Sub
    Public Sub ifDirectoryDoesNotExistCreateIt(ByVal folder As String)
        If My.Computer.FileSystem.DirectoryExists(folder) Then
            ' good
        Else
            Directory.CreateDirectory(folder)
        End If
    End Sub
    Public Sub readSpeakerFile()
        Dim temp, temp2 As String
        Dim i, j, k As Integer
        Dim sr As StreamReader = New StreamReader(sSpeakerFile, Encoding.UTF8)
        Do Until sr.EndOfStream
            temp = sr.ReadLine()
            i += 1
            ' "Elisha (prophecy)",13,0
            Me.sSpeakerFileCharacter(i) = regexReplace(temp, "("")(.*?)("")(.*?\d,)(.*?\d\d?)", "$2")
            Me.iSpeakerFileSpeakerNumber(i) = regexReplace(temp, "(.*?"",)(.*?)(,\d\d?)", "$2")
            Me.iSpeakerFileColor(i) = regexReplace(temp, "(.*?\d,)(.*?)", "$2")
            temp2 = str.Trim(getCharacterShort(Me.sSpeakerFileCharacter(i)))
            If temp2 <> Nothing Then
                j += 1
                Me.sSpeakerFileCharacterShort(j) = temp2
            Else
                ' toss out nul 
            End If
            temp2 = str.Trim(getCharacterPrompt(Me.sSpeakerFileCharacter(i)))
            If temp2 <> Nothing Then
                k += 1
                Me.sCharacterPrompts(k) = temp2
            Else
                ' toss out nul 
            End If
        Loop
    End Sub
    Public Sub assignVoicesToCharacters()
        Dim i As Integer
        Do
            i += 1
            ' store the correct choice in 0
            If sCharacter(i, 0) = "" Then
                ' if nothing chosen say that 1 is correct
                ' don't fix this now
                ' sCharacter(i, 0) = sCharacter(i, 1)
                sSpeakerNumber(i) = assignSpeakerToCharacter(Me.sCharacter(i, 1))
            Else
                ' use what is in 0
                sSpeakerNumber(i) = assignSpeakerToCharacter(Me.sCharacter(i, 0))
            End If
        Loop Until i > iLastClipNumber
    End Sub
    Public Function assignSpeakerToCharacter(ByVal character As String)
        Dim i As Integer
        Dim temp As String = str.Trim(getCharacterShort(character))
        Do
            i += 1
            If Me.sSpeakerFileCharacterShort(i) = temp Then Return iSpeakerFileSpeakerNumber(i)
        Loop Until sSpeakerFileCharacter(i) = "" ' end of characters in array
        Return "0" ' not assigned
    End Function
    Public Function countUnidentified()
        Dim i, total As Integer
        total = 0
        For i = 1 To iLastClipNumber
            If iNumberOfCharactersInClip(i) = 0 Then
                total += 1
            End If
        Next
        Return total
    End Function
    Public Function countUnidentifiedNotFixedYet()
        Dim i, total As Integer
        Dim x = Me.sScript(iLastClipNumber)
        Dim y = Me.sScript(iLastClipNumber)
        For i = 1 To iLastClipNumber
            If i = iLastClipNumber - 1 Then
                Dim z = 1
            End If
            If iNumberOfCharactersInClip(i) = 0 Then
                If sCharacter(i, 0) = Nothing Then
                    total += 1
                Else
                    ' character fixed
                End If
            End If
        Next
        Return total
    End Function
    Public Function countMultiple()
        Dim i, total As Integer
        For i = 1 To iLastClipNumber
            If iNumberOfCharactersInClip(i) > 1 Then
                total += 1
            End If
        Next
        Return total
    End Function
    Public Function countTotal()
        Return iLastClipNumber
    End Function
    Public Function countClipsToRecord()
        Dim i, total As Integer
        For i = 1 To iLastClipNumber
            If (blnRecorded(i) = True Or blnOmit(i) = True) Then
                ' recorded or we aren't going to record it because it is omitted
            Else
                ' not recorded
                total += 1
            End If
        Next
        Return total
    End Function
    Public Function countMultipleNotFixedYet()
        Dim i, total As Integer
        If iNumberOfCharactersInClip(1) = 0 Then ' not initialized yet
            total = 1 ' this will void showing that it is done.
        Else
            For i = 1 To iLastClipNumber
                If iNumberOfCharactersInClip(i) > 1 Then ' multiple
                    If sCharacter(i, 0) = Nothing Then   ' needs to be fixed
                        total += 1                       ' increment total to be fixed
                    Else
                        ' character fixed
                    End If
                End If
            Next
        End If
        Return total
    End Function
    Public Function countUnassignedCharacters()
        Dim i, total As Integer
        For i = 1 To iLastClipNumber
            If sSpeakerNumber(i) = "0" Then
                total += 1
            Else
                ' voice assigned
            End If
        Next
        Return total
    End Function
    Private Function getCharacterPrompt(ByVal character)
        Dim temp As String
        temp = regexReplace(character, "(.*?)(\[.*?])(.*)", "$2")
        If temp = character Then
            temp = "" ' nothing found
        Else
            ' use temp as it has the found Prompt
        End If
        Return temp.Trim
    End Function
    Public Sub fillPromptControl()
        Dim temp As String
        Array.Sort(Me.sCharacterPrompts)
        Dim i As Integer
        For i = 1 To Me.sCharacterPrompts.Length - 1
            temp = removeBrackets(sCharacterPrompts(i))
            '           temp = regexReplace(sCharacterPrompts(i), "(\[|])", "") ' remove [ and ]
            If dramatizer.cbCharacterPrompt.Items.Contains(temp) Then
                ' skip .. this is a duplicate
            Else
                dramatizer.cbCharacterPrompt.Items.Add(temp)
            End If
        Next
    End Sub
    Public Function removeBrackets(ByVal temp As String)
        If temp = Nothing Then
            temp = ""
        Else
            temp = regexReplace(temp, "(\[|])", "") ' remove [ and ]
        End If
        Return temp
    End Function
    Public Sub setDefaultCharacters()
        Dim i As Integer
        For i = 1 To iLastClipNumber
            If Me.sCharacter(i, 2) = Nothing Then
                ' not multiple so we know for sure that character 1 should be used
                Me.sCharacter(i, 0) = Me.sCharacter(i, 1)
            End If
        Next
    End Sub
    Private Sub resetVariables()
        Dim i, j As Integer
        For i = 1 To iLastClipNumber
            For j = 0 To Me.iMaxCharacters
                Me.sCharacter(i, j) = ""
            Next
            Me.sCharacterPrompts(i) = ""
            Me.sBook(i) = ""
            Me.sChapter(i) = ""
            Me.sVerse(i) = ""
            Me.sScript(i) = ""
        Next
        Me.iLastClipNumber = 0
    End Sub
    Public Sub fillAudioProgramControl()
        Try
            Me.cbAudioProgram.Items.Add("c:\Program Files\Audacity\audacity.exe")
            Me.cbAudioProgram.Items.Add("c:\Program Files\Adobe\audition 1.5\Audition.exe")
            Me.cbAudioProgram.Items.Add("c:\Program Files\Adobe\Adobe audition 2.0\Audition.exe")
            Me.cbAudioProgram.Items.Add("c:\Program Files\Cool2000\cool2000.exe")
            Me.cbAudioProgram.Text = "c:\Program Files\Adobe\audition 1.5\Audition.exe"
        Catch ex As Exception
            MessageBox.Show("Problem filling the audio program names control " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    ' Private Sub btnTranslateMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    translate.Show()
    ' End Sub
    Private Sub btnDisplayText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fillSampleEncodingRTBoxes()
        '       Me.panelEncoding.Show()
    End Sub
    Public Sub readMasterFile()
        Dim currentTime As Double = Microsoft.VisualBasic.Timer
        If sProjectName = Nothing Then
            ' do nothing
        Else
            If File.Exists(sMasterFileName) Then
                ' if file saved since time of last read
                ' set flag for masterFileWritten
                If blnMasterFileWrittenSinceLastRead Then
                    blnMasterFileWrittenSinceLastRead = False
                    Me.readClipsFromFileMaster()
                Else
                    ' already just read so skip

                End If
            Else
                Me.iLastClipNumber = 0
                '  loadDocument()  ' name is misleading
            End If
        End If
    End Sub
    Public Sub readMasterFileold()
        Dim currentTime As Double = Microsoft.VisualBasic.Timer
        If sProjectName = Nothing Then
            ' do nothing
        Else
            If System.IO.File.Exists(sMasterFileName) Then
                If currentTime - Me.timeOfLastReadOfMaster > 10 Then
                    Me.readClipsFromFileMaster()
                    timeOfLastReadOfMaster = Microsoft.VisualBasic.Timer
                Else
                    ' already just read so skip
                End If
            Else
                Me.iLastClipNumber = 0
                '  loadDocument()  ' name is misleading
            End If
        End If
    End Sub
    Public Sub deleteMasterFile()
        Dim previousCopy As String = Me.sMasterFileName + ".copy"
        If sProjectName = Nothing Then
            ' do nothing
        Else
            If File.Exists(sMasterFileName) Then
                If File.Exists(previousCopy) Then
                    ' master and previous exist
                    File.Delete(previousCopy)
                Else
                    ' no previous to delete
                End If
                ' master exists
                File.Copy(Me.sMasterFileName, previousCopy)
                File.Delete(sMasterFileName)
            Else
                ' no master file
            End If
        End If
    End Sub
    Public Sub readISOfile()
        Dim temp As String
        Dim temp2(10) As String
        Dim i As Integer
        Try
            Dim sr As StreamReader = New StreamReader(Me.sISO639_3file, Encoding.UTF8)
            Do While Not sr.EndOfStream
                temp = sr.ReadLine()
                temp2 = temp.Split(vbTab)
                Me.isoCode(i) = temp2(0)
                Me.isoName(i) = temp2(6)
                i += 1
            Loop
            iMaxISOcode = i
            sr.Close()
        Catch ex As Exception
            MessageBox.Show("Problem reading the ISO code file." & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Public Sub fillISOcontrol()
        Dim i As Integer
        For i = 0 To Me.iMaxISOcode
            Me.cbISOcode.Items.Add(Me.isoCode(i) & " - " & Me.isoName(i))
            Me.cbFindISOcode.Items.Add(Me.isoName(i) & " - " & Me.isoCode(i))
        Next
        Dim temp As String = Me.cbFindISOcode.Items.Item(0)
        Me.cbFindISOcode.Items.Remove(0)
        Me.cbFindISOcode.Sorted = True
        Me.cbISOcode.Text = cbISOcode.Items.Item(0)
        Me.cbFindISOcode.Text = temp
        If sISOCode = Nothing Then
        Else
            Me.tbISOcode.Text = sISOCode
        End If
        '      checkISOcodePresent()
        areRequiredTabsCompleted()
    End Sub
    Private Sub cbISOcode_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbISOcode.Enter
        Dim temp As String = str.Left(Me.cbISOcode.SelectedItem, 3).Trim
        Me.tbISOcode.Text = temp
        '  checkISOcodePresent()
        '     areRequiredTabsCompleted()
    End Sub
    Private Sub cbISOcode_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbISOcode.SelectedIndexChanged
        Dim temp As String = str.Left(Me.cbISOcode.SelectedItem, 3).Trim
        Me.tbISOcode.Text = Trim(temp)
        ' checkISOcodePresent()
        areRequiredTabsCompleted()

    End Sub
    Private Sub cbISOcode_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbISOcode.SelectedValueChanged
        Dim temp As String = str.Left(Me.cbISOcode.SelectedItem, 3).Trim
        Me.tbISOcode.Text = Trim(temp)
        ' checkISOcodePresent()
        areRequiredTabsCompleted()
    End Sub
    Private Sub cbFindISOcode_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFindISOcode.SelectedValueChanged
        Dim temp As String = str.Right(Me.cbFindISOcode.SelectedItem, 3).Trim
        Me.tbISOcode.Text = temp
        '  checkISOcodePresent()
        areRequiredTabsCompleted()
    End Sub
    Public Function checkISOcodePresent()
        Dim temp As Boolean
        If Me.tbISOcode.Text = "Id" Then
            Me.tbISOcode.BackColor = Color.Pink
            Me.tabControlOptions.SelectedIndex = 3
            temp = False
        Else
            Me.tbISOcode.BackColor = Color.LawnGreen
            temp = True
        End If
        Return temp
    End Function
    Public Function checkFontPresent()
        Dim temp As Boolean
        If Me.cbFontName.Text = "" Then
            Me.tbISOcode.BackColor = Color.Pink
            Me.tabControlOptions.SelectedIndex = 2
            temp = False
        Else
            Me.tbISOcode.BackColor = Color.LawnGreen
            temp = True
        End If
        Return temp
    End Function
    Public Function checkFileNamePresent()
        Dim temp As Boolean
        If Me.cbFileName.Text.Length > 0 Then
            Me.cbFileName.BackColor = Color.LawnGreen
            temp = True
        Else
            Me.cbFileName.BackColor = Color.Pink
            Me.tabControlOptions.SelectedIndex = 0
            temp = False
        End If
        Return temp
    End Function
    Public Function checkQuoteTypePresent()
        Dim temp As Boolean
        If Me.cbQuoteType.Text.Length > 0 Then
            Me.cbQuoteType.BackColor = Color.LawnGreen
            temp = True
        Else
            Me.cbQuoteType.BackColor = Color.Pink
            Me.tabControlOptions.SelectedIndex = 4
            temp = False
        End If
        Return temp
    End Function
    Private Sub TrackBarClipSize_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBarClipSize.Scroll
        'If Me.TrackBarClipSize.Value < 6 Then Me.TrackBarClipSize.Value = 6
        showClipSize()
    End Sub
    Public Function calculateClipSize(ByVal clipSizeInCharacters As Integer)
        Dim temp As Integer = (clipSizeInCharacters * (Me.TrackBarClipSize.Value + 6) / 10 / 16.66).ToString
        If temp = 0 Then temp = 1
        Return temp.ToString()
    End Function
    Private Sub showClipSize()
        Me.tbClipSize.Text = calculateClipSize(Me.iMaxClipSize)
    End Sub
    Private Sub btnBrowseAudio_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseAudio.Click
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Program files (*.exe)|*.exe|All files (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        Me.sAudioProgram = OpenFileDialog1.FileName
        Me.cbAudioProgram.Text = Me.sAudioProgram
    End Sub
    Public Sub createWaveFiles()
        Dim i As Integer
        For i = 1 To Me.iLastClipNumber
            Dim temp As String = sRecordingFolder & "\" & tbISOcode.Text & "-"
            Dim bookNumber As String = getBookNumber(i)
            Dim sequence As String = dramatizer.sPadNumber(5, i.ToString)
            Dim extra As String = justTagOrOmit(i)
            Dim waveFile As String = temp & bookNumber & sequence & extra & ".wav"
            If Me.isWavFileAlreadyRecorded(waveFile) Then
                MessageBox.Show("Trying to overwrite a recorded file.", "Please check the files", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End
            Else
                File.Copy(sMasterWaveFile, waveFile)
            End If
        Next
    End Sub
    Private Function isWavFileAlreadyRecorded(ByVal waveFile As String)
        If File.Exists(waveFile) Then
            ' see it it is larger than master wave file
            If Microsoft.VisualBasic.FileLen(waveFile) = Microsoft.VisualBasic.FileLen(sMasterWaveFile) Then
                ' write over it
                File.Delete(waveFile)
                Return False
            Else
                ' file size different
                Return True
            End If
        Else
            Return False
            ' skip
        End If
    End Function
    Public Function justTagOrOmit(ByVal i As Integer)
        Dim temp As String = Me.sTag(i)
        temp = Me.regexReplace(temp, "\\", "")
        If temp = Nothing Then
            If Me.blnOmit(i) = True Then
                Return ".omit"
            Else
                'skip
                Return ""
            End If
        Else
            Return "." + temp
        End If
    End Function
    Public Function getBookNumber(ByVal i As Integer)
        Select Case Me.sBook(i)
            Case "GEN"
                Return "001-"
            Case "EXO"
                Return "002-"
            Case "LEV"
                Return "003-"
            Case "NUM"
                Return "004-"
            Case "DEU"
                Return "005-"
            Case "JOS"
                Return "006-"
            Case "JDG"
                Return "007-"
            Case "RUT"
                Return "008-"
            Case "1SA"
                Return "009-"
            Case "2SA"
                Return "010-"
            Case "1KI"
                Return "011-"
            Case "2KI"
                Return "012-"
            Case "1CH"
                Return "013-"
            Case "2CH"
                Return "014-"
            Case "EZR"
                Return "015-"
            Case "NEH"
                Return "016-"
            Case "EST"
                Return "017-"
            Case "JOB"
                Return "018-"
            Case "PSA"
                Return "019-"
            Case "PRO"
                Return "020-"
            Case "ECC"
                Return "021-"
            Case "SNG"
                Return "022-"
            Case "ISA"
                Return "023-"
            Case "JER"
                Return "024-"
            Case "LAM"
                Return "025-"
            Case "EZK"
                Return "026-"
            Case "DAN"
                Return "027-"
            Case "HOS"
                Return "028-"
            Case "JOL"
                Return "029-"
            Case "AMO"
                Return "030-"
            Case "OBA"
                Return "031-"
            Case "JON"
                Return "032-"
            Case "MIC"
                Return "033-"
            Case "NAH"
                Return "034-"
            Case "HAB"
                Return "035-"
            Case "ZEP"
                Return "036-"
            Case "HAG"
                Return "037-"
            Case "ZEC"
                Return "038-"
            Case "MAL"
                Return "039-"
            Case "MAT"
                Return "040-"
            Case "MRK"
                Return "041-"
            Case "LUK"
                Return "042-"
            Case "JHN"
                Return "043-"
            Case "ACT"
                Return "044-"
            Case "ROM"
                Return "045-"
            Case "1CO"
                Return "046-"
            Case "2CO"
                Return "047-"
            Case "GAL"
                Return "048-"
            Case "EPH"
                Return "049-"
            Case "PHP"
                Return "050-"
            Case "COL"
                Return "051-"
            Case "1TH"
                Return "052-"
            Case "2TH"
                Return "053-"
            Case "1TI"
                Return "054-"
            Case "2TI"
                Return "055-"
            Case "TIT"
                Return "056-"
            Case "PHM"
                Return "057-"
            Case "HEB"
                Return "058-"
            Case "JAS"
                Return "059-"
            Case "1PE"
                Return "060-"
            Case "2PE"
                Return "061-"
            Case "1JN"
                Return "062-"
            Case "2JN"
                Return "063-"
            Case "3JN"
                Return "064-"
            Case "JUD"
                Return "065-"
            Case "REV"
                Return "066-"
            Case Else
                Return "000-"
        End Select
    End Function
    Private Sub btnBrowseOutputFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseOutputFolder.Click
        Me.FolderBrowserDialog1.SelectedPath = Me.sDramatizeFolder
        Me.FolderBrowserDialog1.ShowDialog()
        Me.sDramatizeFolder = Me.FolderBrowserDialog1.SelectedPath
        Me.cbOutputFolder.Text = Me.sDramatizeFolder
    End Sub
    Public Sub displayStatusText()
        Dim currentSpeaker As Int16 = dramatizer.upDownSpeakerNumber.Value
        Dim notRecordedForCurrentSpeaker As Integer = countNotRecorded(dramatizer.upDownSpeakerNumber.Value)
        Dim notRecordedTotal As Integer = countNotRecordedTotal()
        If MainMenu.rbRecord.Checked = True Then
            Try
                Dim temp As String = notRecordedForCurrentSpeaker.ToString & " " & Me.sLocalizationStrings(Me.iRecordSpeaker, Me.iLanguageSelected) & " " _
             & "           " _
             & countNotRecordedTotal.ToString() & " " & Me.sLocalizationStrings(Me.iRecordTotalText, Me.iLanguageSelected)
                If notRecordedForCurrentSpeaker > 0 Then
                    ' still need to record more
                    dramatizer.statusBar.BackColor = Color.LightYellow
                Else
                    dramatizer.statusBar.BackColor = Color.LawnGreen
                    If countNotRecordedTotal() = 0 Then
                        ' all done
                        MessageBox.Show("All recordings for all speakers are now completed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        ' more to record
                        '         MessageBox.Show("All recording for the speaker number " + currentSpeaker.ToString + " completed!", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
                dramatizer.statusBar.Text = temp
            Catch ex As Exception
                MessageBox.Show("Error trying to display recording status text in dramatizer." & vbCrLf & ex.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        Else
            Try
                Dim temp As String = Me.sLocalizationStrings(Me.iUnidentifiedCharacter, Me.iLanguageSelected) & ": " _
             & countUnidentifiedNotFixedYet.ToString() & "           " _
             & Me.sLocalizationStrings(Me.iMultipleCharacters, Me.iLanguageSelected) & ": " _
             & countMultipleNotFixedYet.ToString()
                If countUnidentifiedNotFixedYet() + countMultipleNotFixedYet() = 0 Then
                    dramatizer.statusBar.BackColor = Color.LawnGreen
                Else
                    dramatizer.statusBar.BackColor = Color.LightYellow
                End If
                dramatizer.statusBar.Text = temp
            Catch ex As Exception
                MessageBox.Show("Error trying to display status text in dramatizer." & vbCrLf & ex.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try
        End If
    End Sub
    Private Function countNotRecorded(ByVal speaker As Int16)
        Dim i, total As Integer
        Try
            For i = 1 To Me.iLastClipNumber
                If Me.blnRecorded(i) = True Then
                    ' recorded
                Else
                    If Me.sSpeakerNumber(i) = speaker Then
                        If Me.blnOmit(i) = True Then
                            ' don't count omitted text
                        Else
                            total += 1
                        End If
                    End If
                End If
            Next
            Return total

        Catch ex As Exception
        End Try
        Return total
    End Function
    Private Function countNotRecordedTotal()
        Dim i, total As Integer
        For i = 1 To Me.iLastClipNumber
            If Me.blnRecorded(i) = True Then
                ' recorded
            Else
                If Me.blnOmit(i) = True Then
                    ' don't count omitted text
                Else
                    total += 1
                End If
            End If
        Next
        Return total
    End Function
    Public Sub removeChapterNumbers()
        Dim i As Integer
        ' remove section heads
        For i = 1 To Me.iLastClipNumber
            If Me.sTag(i).Contains("\c") Then
                Me.blnOmit(i) = True
            Else
                ' skip
            End If
        Next
    End Sub
    Public Sub showChapterNumbers()
        Dim i As Integer
        ' remove section heads
        For i = 1 To Me.iLastClipNumber
            If Me.sTag(i).Contains("\c") Then
                Me.blnOmit(i) = False
            Else
                ' skip
            End If
        Next
    End Sub
    Public Sub removeIntroduction()
        Dim i As Integer
        ' remove section heads
        For i = 1 To Me.iLastClipNumber
            If Me.sTag(i).Contains("\i") Then
                Me.blnOmit(i) = True
            Else
                ' skip
            End If
        Next
        If blnOmit(1) = True Then
            Dim x = 1
        End If
    End Sub
    Public Sub showIntroduction()
        Dim i As Integer
        ' show section heads
        For i = 1 To Me.iLastClipNumber
            If Me.sTag(i).Contains("\i") Then
                Me.blnOmit(i) = False
            Else
                ' skip
            End If
        Next
        If blnOmit(1) = True Then
            Dim x = 1
        End If

    End Sub
    Public Sub removeHeading()
        Dim i As Integer
        ' remove section heads
        For i = 1 To Me.iLastClipNumber
            If Me.sTag(i).Contains("\h") Then
                Me.blnOmit(i) = True
            Else
                ' skip
            End If
        Next
        If blnOmit(1) = True Then
            Dim x = 1
        End If
    End Sub
    Public Sub showHeading()
        Dim i As Integer
        ' show section heads
        For i = 1 To Me.iLastClipNumber
            If Me.sTag(i).Contains("\h") Then
                Me.blnOmit(i) = False
            Else
                ' skip
            End If
        Next
    End Sub
    Public Sub removeSectionHeads()
        Dim i As Integer
        ' remove section heads
        For i = 1 To Me.iLastClipNumber
            If Me.sTag(i).Contains("\s") Then
                Me.blnOmit(i) = True
            Else
                ' skip
            End If
        Next
        If blnOmit(1) = True Then
            Dim x = 1
        End If

    End Sub
    Public Sub showSectionHeads()
        Dim i As Integer
        ' show section heads
        For i = 1 To Me.iLastClipNumber
            If Me.sTag(i).Contains("\s") Then
                Me.blnOmit(i) = False
            Else
                ' skip
            End If
        Next
    End Sub
    Public Sub identifyOmittedText()
        MasterText.rtbTextOnly.BackColor = Color.White
        MasterText.rtbTextWithContext.BackColor = Color.White
        MasterText.chkbxSectionHeads.BackColor = Color.LightGray
        MasterText.chkbxIntroduction.BackColor = Color.LightGray
        MasterText.chkbxHeading.BackColor = Color.LightGray
        MasterText.chkbxChapterNumbers.BackColor = Color.LightGray
        MasterText.chkbxThisOne.BackColor = Color.LightGray
        ' MasterText.chkbxThisOne.Checked = False
        If Me.blnOmit(Me.iCurrentClipNumber) Then
            MasterText.rtbTextOnly.BackColor = Color.OrangeRed
            MasterText.rtbTextWithContext.BackColor = Color.OrangeRed
            Select Case Me.sTag(Me.iCurrentClipNumber)
                Case "\s", "\s1", "\s2", "\s3"
                    MasterText.chkbxSectionHeads.BackColor = Color.OrangeRed
                Case "\id", "\ip", "\is"
                    MasterText.chkbxIntroduction.BackColor = Color.OrangeRed
                Case "\h", "\h1", "\h2", "\h3"
                    MasterText.chkbxHeading.BackColor = Color.OrangeRed
                Case "\c"
                    MasterText.chkbxChapterNumbers.BackColor = Color.OrangeRed
                Case Else
                    MasterText.chkbxThisOne.BackColor = Color.OrangeRed
                    MasterText.chkbxThisOne.Checked = True
            End Select
        Else
            MasterText.chkbxThisOne.Checked = False
        End If
    End Sub
    Public Sub processOmittedTextBasedOnCheckedInfo()
        If blnOmit(1) = True Then
            Dim x = 1
        End If
        Try
            If MasterText.chkbxChapterNumbers.Checked = True Then
                removeChapterNumbers()
            Else
                showChapterNumbers()
            End If
            If MasterText.chkbxHeading.Checked = True Then
                removeHeading()
            Else
                showHeading()
            End If
            If MasterText.chkbxIntroduction.Checked = True Then
                removeIntroduction()
            Else
                showIntroduction()
            End If
            If MasterText.chkbxSectionHeads.Checked = True Then
                removeSectionHeads()
            Else
                showSectionHeads()
            End If
            identifyOmittedText()
            writeClipsToMasterFileAndAdjustClipSize(False)
            If blnOmit(1) = True Then
                Dim x = 1
            End If
        Catch ex As Exception
            MessageBox.Show("ERROR in process omitted text" & vbCrLf & ex.Message)
        End Try
    End Sub
    Public Sub fillCharacterNamesControl()
        Try
            Dim characterShort As String
            For Each characterShort In Me.sSpeakerFileCharacterShort
                If characterShort = Nothing Then
                    ' skip all the blanks - first one and lots at the end
                Else
                    If dramatizer.cbCharactersEdit.Items.Contains(characterShort) Then
                        ' skip .. this is a duplicate
                    Else
                        dramatizer.cbCharactersEdit.Items.Add(characterShort)
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Problem filling the character names control " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Private Function breakTextAtParagraphVerseOrSentence(ByVal temp)
        If Me.iCurrentClipNumber = Me.iLastClipNumber Then
            Dim s = 1
        End If
        Dim break(1) As String
        Dim iFoundString As Integer
        Dim iAdjustSize = (Me.TrackBarClipSize.Value + 6) / 10
        Dim iLowestMaxClipSize As Integer = (Me.iMaxClipSize * 0.5) * iAdjustSize
        Dim iLowerMaxClipSize As Integer = (Me.iMaxClipSize * 0.6) * iAdjustSize
        Dim iMidMaxClipSize As Integer = (Me.iMaxClipSize * 0.7) * iAdjustSize
        Dim iUpperMaxClipSize As Integer = (Me.iMaxClipSize) * iAdjustSize
        '  iUpperMaxClipSize = (Me.iUpperMaxClipSize) * iAdjustSize
        '    If Me.iCurrentClipNumber = 213 Then
        If temp.Length > iUpperMaxClipSize Then   ' about 30 seconds of speech
            ' is there a \p after 75 characters? try breaking here
            iFoundString = str.InStr(75, temp, "\p")
            If iFoundString > 0 And iFoundString < iUpperMaxClipSize Then
                break(0) = str.Left(temp, iFoundString - 1)
                break(1) = str.Mid(temp, iFoundString + 1)
            Else
                ' look for <verse>
                iFoundString = str.InStr(iMidMaxClipSize, temp, "<verse>")
                If iFoundString > 0 And iFoundString < iUpperMaxClipSize Then
                    break(0) = str.Left(temp, iFoundString - 1)
                    break(1) = str.Mid(temp, iFoundString)
                    Dim x1 = break(1).Length
                Else
                    ' look for verse making a shorter clip
                    iFoundString = str.InStr(iLowestMaxClipSize, temp, "<verse>")
                    If iFoundString > 0 And iFoundString < iUpperMaxClipSize Then
                        break(0) = str.Left(temp, iFoundString - 1)
                        break(1) = str.Mid(temp, iFoundString)
                    Else
                        ' look for period
                        iFoundString = str.InStr(iLowerMaxClipSize, temp, ".")
                        If iFoundString > 0 And iFoundString < iUpperMaxClipSize Then
                            break(0) = str.Left(temp, iFoundString - 1) & "."
                            break(1) = str.Mid(temp, iFoundString)
                        Else
                            iFoundString = str.InStr(iLowestMaxClipSize, temp, ".")
                            If iFoundString > 0 And iFoundString < iUpperMaxClipSize Then
                                break(0) = str.Left(temp, iFoundString - 1) & "."
                                break(1) = str.Mid(temp, iFoundString)
                            Else
                                Dim response = MessageBox.Show("Maximum clip size setting: " & iUpperMaxClipSize & vbCrLf & "Long clip: " & Me.iCurrentClipNumber.ToString & vbCrLf & sBook(Me.iCurrentClipNumber) & " " & sChapter(Me.iCurrentClipNumber) & "." & sVerse(Me.iCurrentClipNumber) & vbCrLf & iFoundString.ToString & vbCrLf & temp & vbCrLf & vbCrLf & " Do you want to start over with a different Clip size.", "Long clip", MessageBoxButtons.YesNo)
                                If response = vbYes Then
                                    End
                                Else
                                    ' keep working
                                End If
                                break(0) = temp
                                break(1) = ""
                            End If
                        End If
                    End If
                End If
            End If
            '       break(0) = "dummy"
            '      break(1) = "dummy2"
            Return break
        Else
            ' smaller than minimum size
            If Me.chkbxBreakAtParagraphs.Checked = True Then
                ' looking for \p 
                iFoundString = str.InStr(20, temp, "\p")
                If iFoundString > 0 Then
                    '\p found in string
                    break(0) = str.Left(temp, iFoundString - 1)
                    break(1) = str.Mid(temp, iFoundString)
                Else
                    '\p not found in string
                    break(0) = temp
                    break(1) = ""
                End If
            Else
                ' not looking for \p at all
                break(0) = temp
                break(1) = ""
            End If
        End If
        Dim iCount = temp.Length
        Return break
    End Function
    Private Sub btnDefaultSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDefaultSettings.Click
        Me.TrackBarClipSize.Value = 4  ' this is the 30 seconds
        Me.chkbxBreakAtParagraphs.Checked = True
        Me.chkbxAdjustClipSize.Checked = True
        Me.showClipSize()
    End Sub
    Private Sub cbQuoteType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbQuoteType.SelectedIndexChanged
        '       checkQuoteTypePresent()
        areRequiredTabsCompleted()
    End Sub
    Private Function areRequiredTabsCompleted()
        If Me.checkFileNamePresent And checkFontPresent() And checkISOcodePresent() And Me.checkQuoteTypePresent Then
            Me.btnSetOptions.BackColor = Color.LawnGreen
            Return True
        Else
            Me.btnSetOptions.BackColor = Color.LightYellow
            Return False
        End If
    End Function
    Private Sub tbISOcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbISOcode.TextChanged
        areRequiredTabsCompleted()
    End Sub
    Public Sub changeFontSize(ByVal increase As Boolean)
        Dim fontName As String = cbFontName.SelectedItem
        Dim fontSize As String = cbFontSize.SelectedItem
        If cbFontName.SelectedItem = Nothing Then
            fontName = cbFontName.Text
        End If
        Try
            If increase Then
                cbFontSize.SelectedIndex = cbFontSize.SelectedIndex + 1
            Else
                cbFontSize.SelectedIndex = cbFontSize.SelectedIndex - 1
            End If
        Catch ex As Exception
            Beep()
        End Try
    End Sub
    ' main menu
    Public Sub readLocalizationFile()
        Dim blnResizedArray = False
        Try
            ' Excel saves as ANSI file
            Dim sr As StreamReader = New StreamReader(Me.sLocalizationFile, Encoding.UTF7, True)
            Dim temp As String
            Dim temp2(1000) As String 'items in string
            Dim item, language As Integer
            Do While Not sr.EndOfStream
                item += 1
                temp = sr.ReadLine()
                temp = Me.regexReplace(temp, """", "") ' remove quote marks
                temp2 = temp.Split(vbTab)
                If blnResizedArray = True Then
                    ' skip
                Else
                    ' need to do it one time
                    redimLocalizationStringsArray(temp2.Length - 1)
                    blnResizedArray = True
                End If
                For language = 0 To temp2.Length - 1
                    If temp2(language) = "" Then temp2(language) = temp2(1) ' Column two always has English data 
                    ' row is the item and column is the language
                    sLocalizationStrings(item, language) = temp2(language)
                Next
            Loop
            rowLocalization = item
            columnLocalization = language

            sr.Close()
        Catch ex As Exception
            MessageBox.Show(Me.sLocalizationStrings(iFileReadError, Me.iLanguageSelected) & vbCrLf & Me.sLocalizationFile & vbCrLf & vbCrLf & ex.Message, Me.sLocalizationStrings(Me.iFileReadError, Me.iLanguageSelected), MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End
        End Try
    End Sub
    Public Sub localizeTranslate(ByVal language As Int16)
        translate.btnOK.Text = sLocalizationStrings(iOK, language)
        translate.btnCancel.Text = sLocalizationStrings(iCancel, language)
        translate.lblSource.Text = sLocalizationStrings(2, 1)
        translate.lblTarget.Text = sLocalizationStrings(2, iLanguageSelected)
    End Sub
    Public Sub localizeSpeakerText(ByVal language As Int16)
        SpeakerText.Text = sLocalizationStrings(iText, language)
    End Sub
    Public Sub localizeMain(ByVal language As Int16)
        Me.Text = Me.Text
        Me.lblDiskSpaceDriveC.Text = Me.sLocalizationStrings(Me.iAvailableDiskSpaceC, language)
        Me.lblFileName.Text = Me.sLocalizationStrings(iFileName, language)
        Me.lblFontName.Text = Me.sLocalizationStrings(Me.iFontName, language)
        Me.lblFontSize.Text = Me.sLocalizationStrings(Me.iFontSize, language)
        Me.btnDefaultSettings.Text = Me.sLocalizationStrings(Me.iDefaultSettings, language)
        Me.btnBrowse.Text = Me.sLocalizationStrings(Me.iBrowse, language)
        Me.btnBrowseOutputFolder.Text = Me.sLocalizationStrings(Me.iBrowse, language)
        Me.lblDramatizerOutputFolder.Text = Me.sLocalizationStrings(Me.iDramatizerOutputFolder, language)
        Me.lblFirstLevelQuoteType.Text = Me.sLocalizationStrings(Me.iQuoteType, language)
        Me.lblAudioRecordingProgram.Text = Me.sLocalizationStrings(Me.iAudioRecordingProgram, language)
        Me.btnBrowseAudio.Text = Me.sLocalizationStrings(Me.iBrowse, language)
        '  Me.btnChooseEncoding.Text = Me.sLocalizationStrings(Me.iTextEncoding, language)
        Me.btnCancel.Text = Me.sLocalizationStrings(Me.iCancel, language)
        Me.btnSetOptions.Text = Me.sLocalizationStrings(Me.iSetOptions, language)
        Me.btnDisplayClips.Text = Me.sLocalizationStrings(Me.iDisplayQuotes, language)
        ' Me.btnChooseEncoding.Text = Me.sLocalizationStrings(Me.iTextEncoding, language)
        Me.rbEncodingANSI.Text = Me.sLocalizationStrings(Me.iANSI, language)
        Me.rbEncodingUTF8.Text = Me.sLocalizationStrings(Me.iUTF8, language)
        Me.lblEncoding.Text = Me.sLocalizationStrings(Me.iTextEncoding, language)
        Me.lblEthnologueCode.Text = Me.sLocalizationStrings(Me.iEthnologueCode, language)
        Me.lblCurrentPosition.Text = "" ' Me.sLocalizationStrings(Me.iCurrentLocationNumber, language)
        Me.lblMaxClipSize.Text = Me.sLocalizationStrings(Me.iMaxClipSizeText, language)
        Me.chkbxBreakAtParagraphs.Text = Me.sLocalizationStrings(Me.iBreakAtMostParagraphs, language)
        Me.chkbxAdjustClipSize.Text = Me.sLocalizationStrings(Me.iUseSettingsToAdjustClipSize, language)
        Me.TabPage1.Text = Me.sLocalizationStrings(Me.iInputTab, language)
        Me.TabPage1.Text = Me.sLocalizationStrings(Me.iInputTab, language)
        Me.TabPage2.Text = Me.sLocalizationStrings(Me.iQuoteTypeTab, language)
        Me.TabPage3.Text = Me.sLocalizationStrings(Me.iTextEncodingTab, language)
        Me.TabPage4.Text = Me.sLocalizationStrings(Me.iRecordingProgramTab, language)
        Me.TabPage5.Text = Me.sLocalizationStrings(Me.iClipSizeTab, language)
        Me.TabPage6.Text = Me.sLocalizationStrings(Me.iEthnologueCodeTab, language)
        Me.TabPage7.Text = Me.sLocalizationStrings(Me.iOutputTab, language)
        Me.TabPage8.Text = Me.sLocalizationStrings(Me.iFontTab, language)
    End Sub
    Public Sub prepareToWorkFromMaster()
        dramatizer.Show()
        MainMenu.Hide()
        Me.Hide()
        Me.readClipsFromFileMaster()
        dramatizer.displayMasterAndVoiceTalentText()
        Me.fillPromptControl()
        Me.fillCharacterNamesControl()
    End Sub
    Public Sub localizeDramatizer(ByVal language As Int16)
        '   fillForwardBackByControl(language)
        dramatizer.Text = Me.Text
        dramatizer.rbUpdated.Text = sLocalizationStrings(Me.iUpdatedCharacter, language)
        dramatizer.chkbxShowPrompt.Text = sLocalizationStrings(Me.iShowPrompt, language)
        dramatizer.chkbxDisplayOmittedClips.Text = sLocalizationStrings(Me.iDisplayOmittedClipsToo, language)
        ' dramatizer.chkbxDisplayUnrecordedOnly.Text = sLocalizationStrings(Me.iDisplayUnrecordedClipsOnly, language)
        dramatizer.chkbxDisplayOnlyClipsToProcess.Text = sLocalizationStrings(Me.iDisplayUnprocessedClipsOnly, language)
        '     dramatizer.chkbxSpeaker.Text = sLocalizationStrings(Me.iRecordOneSpeakerAtATime, language)
        dramatizer.lblClipNumber.Text = sLocalizationStrings(iClipNumber, language)
        dramatizer.lblCharacterSpeakerNumber.Text = sLocalizationStrings(iSpeakerNumberText, language)
        dramatizer.lblCharacterPrompt.Text = sLocalizationStrings(iPrompt, language)
        dramatizer.lblCharacterName.Text = sLocalizationStrings(Me.iCharacterName, language)
        dramatizer.lblDisplay.Text = sLocalizationStrings(Me.iDisplayClipsBy, language)
        dramatizer.btnNotAQuote.Text = sLocalizationStrings(Me.iNotAQuote, language)
        dramatizer.btnNext.Text = sLocalizationStrings(iNext, language)
        'dramatizer.btnUpdate.Text = sLocalizationStrings(iUpdate, language)
        ' dramatizer.btnUpdate.Text = "" ' using icons now
        dramatizer.btnEdit.Text = sLocalizationStrings(iEdit, language)
        dramatizer.btnEnd.Text = sLocalizationStrings(iExit, language)
        dramatizer.btnMoreOptions.Text = sLocalizationStrings(Me.iMore, language)
        dramatizer.btnLessOptions.Text = sLocalizationStrings(Me.iLess, language)
        dramatizer.btnMoveDown.Text = Me.sLocalizationStrings(Me.iMoveDown, language)
        dramatizer.rbAll.Text = sLocalizationStrings(Me.iAllClips, language)
        ' dramatizer.rbCharacter.Text = sLocalizationStrings(Me.iCharacterNameClips, language)
        dramatizer.rbMultiple.Text = sLocalizationStrings(Me.iMultipleClips, language)
        dramatizer.rbSpeaker.Text = sLocalizationStrings(Me.iSpeakerNumberClips, language)
        dramatizer.rbUnidentified.Text = sLocalizationStrings(Me.iUnidentifiedClips, language)
        dramatizer.rbUpdated.Text = sLocalizationStrings(Me.iUpdatedCharacter, language)
        '        ForwardBackControl.lbForwardBackBy.Text = Me.sLocalizationStrings(iNextClip, language)
    End Sub
    '   Private Sub localizeMasterText(ByVal language As Int16)
    '      MasterText.chkbxShowContext.Text = sLocalizationStrings(iShowContext, language)
    '     MasterText.chkbxShowSFMcodes.Text = sLocalizationStrings(iShowSFMcodes, language)
    '    MasterText.chkbxShowSpeakerText.Text = sLocalizationStrings(iShowSpeakerText, language)
    '   MasterText.chkbxShowVerses.Text = sLocalizationStrings(iShowVerses, language)
    '  End Sub
    Public Sub createScripts1to30()
        Try
            Dim fontName As String = Me.cbFontName.SelectedItem
            Dim fontSize As String = Me.cbFontSize.SelectedItem
            Dim temp As String
            Dim i As Integer
            Dim iScriptClipNumber As Integer
            Dim iSpeakerNumber As Integer = 0
            For iSpeakerNumber = 1 To 30
                i = 0 ' counter
                Dim sw As StreamWriter = New StreamWriter(Me.sScriptSpeakerFile & iSpeakerNumber.ToString & ".html", False, System.Text.Encoding.UTF8)
                writeStartHTML(sw)
                sw.WriteLine("td.clip {font-family: " + fontName + ", Arial, Helvetica, sans-serif ; font-size: " + fontSize + "px; 	background-color: transparent}")
                sw.WriteLine(" -->")
                sw.WriteLine("</style>")
                sw.WriteLine("</head>")
                sw.WriteLine("<body>")
                sw.WriteLine("<table>")
                For iScriptClipNumber = 1 To Me.iLastClipNumber
                    If Me.sSpeakerNumber(iScriptClipNumber) = iSpeakerNumber.ToString Then
                        i += 1
                        sw.WriteLine("<tr >")
                        sw.Write("<td class=""counter"">")
                        sw.Write(i)
                        sw.Write("</td><td class=""speaker"">")
                        sw.Write(Me.sSpeakerNumber(iScriptClipNumber))
                        sw.Write("</td><td>")
                        If Me.sCharacter(iScriptClipNumber, 0) <> Nothing Then
                            sw.Write(Me.sCharacter(iScriptClipNumber, 0))
                        Else
                            sw.Write(Me.sCharacter(iScriptClipNumber, 1))
                        End If
                        sw.Write("</td><td>")
                        sw.Write(Me.sBook(iScriptClipNumber))
                        sw.Write(" ")
                        sw.Write(Me.sChapter(iScriptClipNumber))
                        sw.Write(".")
                        sw.Write(Me.sVerse(iScriptClipNumber))
                        sw.Write("</td><td>")
                        sw.WriteLine(iScriptClipNumber)
                        sw.Write("</td>")
                        sw.Write("</tr>")
                        temp = getScriptAndRemoveSFMandVerses(iScriptClipNumber)
                        temp = fixHTMLcodes(temp)
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
            MessageBox.Show("Error writing script for speaker number.", "error writing file", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
    Public Sub createScriptsMaster()
        Try
            Dim fontName As String = Me.cbFontName.SelectedItem
            Dim fontSize As String = Me.cbFontSize.SelectedItem
            Dim temp As String = ""
            Dim tempCurrentChar As String
            Dim tempNextChar As String = ""
            Dim tempCharFollowingNotAQuote As String = ""
            Dim tempNotAQuote As String = ""
            Dim tempFollowingNotAQuote As String = ""
            Dim tempCharFollowingNotAQuoteAndText As String = ""
            Dim iScriptClipNumber, i As Integer
            i = 0 ' counter
            Dim sw As StreamWriter = New StreamWriter(Me.sScriptSpeakerFile & "master.html", False, System.Text.Encoding.UTF8)
            writeStartHTML(sw)
            sw.WriteLine("td.clip {font-family: " + fontName + ", Arial, Helvetica, sans-serif ; font-size: " + fontSize + "px; 	background-color: transparent}")
            sw.WriteLine(" -->")
            sw.WriteLine("</style>")
            sw.WriteLine("</head>")
            sw.WriteLine("<body>")
            sw.WriteLine("<table>")
            For iScriptClipNumber = 1 To Me.iLastClipNumber
                i += 1
                sw.WriteLine("<tr >")
                sw.Write("<td class=""counter"">")
                sw.Write(i)
                sw.Write("</td><td class=""speaker"">")
                sw.Write(Me.sSpeakerNumber(iScriptClipNumber))
                sw.Write("</td><td>")
                tempCurrentChar = getCharacter(iScriptClipNumber)
                sw.Write(tempCurrentChar)
                sw.Write("</td><td>")
                sw.Write(Me.sBook(iScriptClipNumber))
                sw.Write(" ")
                sw.Write(Me.sChapter(iScriptClipNumber))
                sw.Write(".")
                sw.Write(Me.sVerse(iScriptClipNumber))
                sw.Write("</td><td>")
                sw.WriteLine(iScriptClipNumber)
                sw.Write("</td>")
                sw.Write("</tr>")
                temp = getScriptAndRemoveSFMandVerses(iScriptClipNumber)
                temp = fixHTMLcodes(temp)
                sw.WriteLine("<tr>")
                sw.Write("<td class=""clip"">")
                sw.WriteLine(temp)
                sw.Write("</td>")
                sw.Write("</tr>")
                sw.WriteLine()
                tempNotAQuote = ""
                tempNextChar = ""
                tempCurrentChar = ""
                tempCharFollowingNotAQuote = ""
                temp = ""
            Next
            sw.Write("</table>")
            sw.Write("</body>")
            sw.Write("</html>")
            sw.Close()
        Catch ex As Exception
            MessageBox.Show("Error writing script for master " & Me.sScriptSpeakerFile & vbCrLf & ex.Message, "error writing file", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Private Function fixHTMLcodes(ByVal temp As String)
        temp = Me.regexReplace(temp, "&", "&amp;")
        temp = Me.regexReplace(temp, "<", "&lt;")
        temp = Me.regexReplace(temp, ">", "&gt;")
        Return temp
    End Function
    Public Function getTempStringWithNotAQuotesAddedToIt()
        Dim temp As String
        Dim tempNextChar As String = ""
        Dim tempNotAQuote As String = ""
        Dim tempCharFollowingNotAQuote As String = ""
        Dim tempCurrentChar As String = getCharacter(iTempClipNumber)
        Dim tempFollowingNotAQuote As String

        temp = getScriptAndRemoveSFMandVerses(iTempClipNumber)
        tempNextChar = getCharacter(iTempClipNumber + 1)
        Do While tempNextChar = sNotAQuote
            iTempClipNumber += 1
            tempNotAQuote = getScriptAndRemoveSFMandVerses(iTempClipNumber)
            temp = temp + " " + tempNotAQuote
            tempCharFollowingNotAQuote = getCharacter(iTempClipNumber + 1)
            If tempCharFollowingNotAQuote = tempCurrentChar Then
                tempFollowingNotAQuote = getScriptAndRemoveSFMandVerses(iTempClipNumber + 1)
                If tempFollowingNotAQuote = Nothing Then
                    ' skip
                Else
                    temp = temp + " " + tempFollowingNotAQuote
                End If
                iTempClipNumber += 2
            Else
                iTempClipNumber += 1
            End If
            '            iTempClipNumber += 1
            tempNextChar = getCharacter(iTempClipNumber)
        Loop
        Return temp

    End Function
    Public Function getScriptAndRemoveSFMandVerses(ByVal iNumber As Integer)
        Dim temp As String
        temp = Me.sScript(iNumber)
        ' remove verse and \ codes
        temp = dramatizer.removeVerseNumber(temp)
        temp = dramatizer.removeSFMcodes(temp)
        Return temp
    End Function
    Public Function getCharacter(ByVal iClipNumber As Integer)
        If Me.sCharacter(iClipNumber, 0) <> Nothing Then
            Return (Me.sCharacter(iClipNumber, 0))
        Else
            Return (Me.sCharacter(iClipNumber, 1))
        End If

    End Function
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
    Public Function formatTextForTextBox(ByVal temp As String)
        temp = Me.regexReplace(temp, "(\()(\d)", vbCrLf & "    ($2")
        temp = Me.regexReplace(temp, "\|", vbCrLf & vbCrLf) ' new line with space above 
        Return temp
    End Function
    Public Function formatFolderStructure(ByVal temp As String)
        Me.regexReplace(temp, "\", "$1\$2" + vbCrLf)
        Return temp
    End Function

    Private Sub rbEncodingANSI_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbEncodingANSI.CheckedChanged
        Try
            setEncoding()
            fillSampleEncodingRTBoxes()
            ' don't need to do this for UTF8 also ... as changed handles both cases.
        Catch ex As Exception
            ' error thrown first time through because we don't know file name yet
        End Try
    End Sub
    Private Function verifyFirstLineStartsWithID()
        Try
            Dim sr As StreamReader = New StreamReader(sProjectFileName, System.Text.Encoding.UTF8, True)
            sr.Close()
        Catch ex As Exception
            MessageBox.Show("File is already open or locked by another program.", "File not available.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End
        End Try
        Try
            Dim sr As StreamReader = New StreamReader(sProjectFileName, System.Text.Encoding.UTF8, True)
            Dim temp As String
            Do
                temp = sr.ReadLine
            Loop Until (temp.StartsWith("\id"))
            sr.Close()
            ' valid looking file
        Catch ex As Exception
            Dim response = MessageBox.Show("File does not begin with \id." & vbCrLf & "Please verify that this is the correct file by opening the file in NotePad or some other text editor.", "Missing \id line", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End
        End Try
        Return True
    End Function
    Public Sub redimLocalizationStringsArray(ByVal iNewLanguage As Integer)
        If iNewLanguage > iMaximumLocalizationLanguages Then
            ' too many languages
            ReDim Preserve sLocalizationStrings(iMaximumLocalizationStrings, iNewLanguage)
            iMaximumLocalizationLanguages = iNewLanguage
        Else
            ' everything okay
        End If

    End Sub

End Class
