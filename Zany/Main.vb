Imports System
Imports System.IO
Imports System.Text
Imports str = Microsoft.VisualBasic.Strings
Imports System.Text.RegularExpressions
Public Class Main

    Public sProgramDirectory As String = Directory.GetCurrentDirectory ' beware that this may change
    Public sDramatizeFolder As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\My Dramatizer"
    Public sRequiredFilesFolder As String = sProgramDirectory & "\RequiredFiles" ' beware that this may change
    Public sTempFolder As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\Dramatizer"
    Public sCreateClipsFileName As String = sTempFolder & "\01 - createClips.tmp"
    Public sGetBookChapterVerseFileName As String = sTempFolder & "\02 - bookChapterVerse.tmp"
    Public sIdentifyCharactersFileName As String = sTempFolder & "\03 - identifyCharacters.tmp"
    Public sOneLinePerClip As String = sTempFolder & "\04 - oneLinePerClip.tmp"
    Public sMasterWaveFile As String = sRequiredFilesFolder & "\master.wav"
    Public sCharacterNames_BookChapterVerseFileName As String = sRequiredFilesFolder & "\CharacterNames_BookChapterVerse.txt"
    Public sINIfile As String = sRequiredFilesFolder & "\zany.ini"
    Public sISO639_3file As String = sRequiredFilesFolder & "\iso-fdis-639-3_20061114.tab"
    Public sTempFileName As String = sTempFolder & "\temp.tmp"
    Public sVoiceFile As String = sRequiredFilesFolder & "\Character-Voice.txt"

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
    Public iMaxClipSize As String = 500
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
    Public iVoiceColor(iMaxClips)
    Public iVoiceVoiceNumber(iMaxClips)
    Public isoCode(10000) As String
    Public isoName(10000) As String
    Public sAllClips(iMaxClips) As String
    Public sAudioProgram As String
    Public sBook(iMaxClips) As String
    Public sChapter(iMaxClips) As String
    Public sCharacter(iMaxClips, iMaxCharacters) As String
    Public sCharacterPrompts(iMaxClips) As String ' for prompt control -- this is really too large
    Public sCharacterShort(iMaxClips) As String ' this is really too large
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
    Public sProgramName As String = "Dramatizer"
    Public sProgramVersion As String = "2.0"
    Public sProjectFileName As String
    Public sProjectFolder As String
    Public sProjectName As String
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
    Public sVoiceCharacter(iMaxClips)
    Public scripture As Stream
    Public selectedFont As Font
    Public blnOmit(iMaxClips) As Boolean
    Public blnRecorded(iMaxClips) As Boolean


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
        sProjectName = getFileNameWithoutExtensionFromFullName(sProjectFileName)
        '    createFoldersAndMasterAndScriptsFileNames()
        Me.panelSettings.Show()
        readVoiceFile()
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
        VoiceTalentText.rtbText.Font = New Font(fontName, fontSize)
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
            cbQuoteType.Items.Add("« ... »")
            cbQuoteType.Items.Add("<< ... >>")
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
    Private Sub AssignCharacters()

    End Sub
    Private Sub AssignVoices()

    End Sub
    Private Sub CreateScripts()

    End Sub
    Private Sub Record()

    End Sub
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
                        writeSplitText(i, sw, continued, splitText(continued))
                        sw.WriteLine("</clip>")
                    End If
                Next
            Next
            sw.Write("</dramatizer>")
            sw.Close()
            ifError_QuoteMarkerFoundInsideOfText_doNoSaveWork()
        Catch ex As Exception
            MessageBox.Show("Unable to write to file " & sMasterFileName & vbCrLf & "Check to see if file is open in another application " & vbCrLf & ex.Message & vbCrLf & "Current clip " & Me.iCurrentClipNumber.ToString & vbCrLf & "Maximum clip number " & Me.iLastClipNumber.ToString, "File writing error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
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
    Private Sub writeSplitText(ByVal i As Integer, ByVal sw As StreamWriter, ByVal continued As Integer, ByVal temp As String)
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
            System.IO.File.Copy(sTempFileName, sMasterFileName, True) ' makes it less likely we will loose data on power failure
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
        '  isOpenQuoteFoundAgain(temp(0))
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
        If cbQuoteType.Text = "« ... »" Then ' 
            sOpenQuote = "«"
        ElseIf cbQuoteType.Text = "<< ... >>" Then ' 
            sOpenQuote = "<<"
        End If
        sStringToCheckOut = temp
        iIsOpenQuoteFoundAgain = str.InStr(3, sStringToCheckOut, sOpenQuote)
        '  Debug.Assert(iIsOpenQuoteFoundAgain = 0, "Error :" & sOpenQuote & " A second open quote found inside of clip." & vbCrLf & sStringToCheckOut & vbCrLf & sChapter(i) & "." & sVerse(i))
        If iIsOpenQuoteFoundAgain = 0 Then
            ' great no extra opening quote mark found
        Else
            ' oops this may be a continuing quote mark or an error
            Dim response = MessageBox.Show(MainMenu.sLocalizationStrings(MainMenu.iVerifyOpeningQuote, MainMenu.iLanguageSelected) & " (" & sOpenQuote & ") " & MainMenu.sLocalizationStrings(MainMenu.iContinueVerifyOpeningQuote, MainMenu.iLanguageSelected) & vbCrLf & vbCrLf & MainMenu.sLocalizationStrings(MainMenu.iContinuingQuoteTip, MainMenu.iLanguageSelected) & vbCrLf & vbCrLf & sBook(Me.iCurrentClipNumber) & " " & sChapter(Me.iCurrentClipNumber) & "." & sVerse(Me.iCurrentClipNumber) & vbCrLf & vbCrLf & sStringToCheckOut & vbCrLf & vbCrLf & MainMenu.sLocalizationStrings(MainMenu.iContinuingQuote, MainMenu.iLanguageSelected), MainMenu.sLocalizationStrings(MainMenu.iSecondQuoteFound, MainMenu.iLanguageSelected), MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If response = vbYes Then
                ' continuing quote marker - good
                ' fix marker here.
                blnContinuingQuoteFound = True
            Else
                blnQuoteMarkerErrorFound = True
                response = MessageBox.Show(MainMenu.sLocalizationStrings(MainMenu.iTextUnusable, MainMenu.iLanguageSelected) & MainMenu.sLocalizationStrings(MainMenu.iCorrectTheText, MainMenu.iLanguageSelected) & vbCrLf & vbCrLf & MainMenu.sLocalizationStrings(MainMenu.iSeeMoreProblems, MainMenu.iLanguageSelected), MainMenu.sLocalizationStrings(MainMenu.iCorrectTheText, MainMenu.iLanguageSelected), MessageBoxButtons.YesNo)
                If response = vbYes Then
                    ' continue looking
                Else
                    End
                End If
            End If
        End If

    End Sub
    Private Sub readClipsFromFileOneLinePerClip()
        Dim i As Integer
        Dim j As Int16
        Dim temp As String
        Dim temp2 As String
        Try
            ' ProgressIndicator.Show()
            MainMenu.progressBar.Visible = True
            '    MainMenu.progressBar.Show()
            MainMenu.progressBar.Maximum = iLastClipNumber + 50
            Dim sr As StreamReader = New StreamReader(Me.sOneLinePerClip, Encoding.UTF8)
            Do While Not sr.EndOfStream
                MainMenu.tbProgress.Text = i.ToString
                MainMenu.progressBar.Value = i
                MainMenu.progressBar.Update()
                i = i + 1
                temp = sr.ReadLine()
                temp2 = regexReplace(temp, "(.+?)(character1="""")(.+)", "$2")
                ' if temp2 starts with <clip .... that means that it failed above test
                If temp2.StartsWith("character1=""""") Then
                    sCharacter(i, 1) = ""
                    iNumberOfCharactersInClip(i) = 0
                Else
                    ' there is at least one character
                    For j = 1 To iMaxCharacters
                        '      sCharacter(i, j) = regexReplace(temp, "(.+\s)(character" & j.ToString & "=')(.*?)(.+)", "$3")
                        temp2 = regexReplace(temp, "(.+?character" & j.ToString & "="")" & "(.*?)("".+)", "$2")
                        If temp2.StartsWith("<clip") Then
                            ' no match found so skip looking for more
                            Exit For
                        End If
                        sCharacter(i, j) = temp2
                        iNumberOfCharactersInClip(i) = j
                    Next
                End If
                sID(i) = regexReplace(temp, "(.+?id=')(.+?)('.+?)", "$2")
                sBook(i) = regexReplace(temp, "(.+\s)(id=')(.*?)(\s.+)", "$3")
                sChapter(i) = regexReplace(temp, "(.+\s)(id='.*?\s)(.*?)(\..+)", "$3")
                sVerse(i) = regexReplace(temp, "(.+\s)(id='.*?\.)(.*?)('.+)", "$3")
                ' xxxxsprompt(i) = regexReplace(temp, "(.+\s)(id='.*?\.)(.*?)('.+)", "$3")
                temp2 = regexReplace(temp, "(.+\s)(tag=')(.*?)('.+)", "$3")
                If temp2.StartsWith("<clip") Then
                    ' no match found so skip looking for more
                    sTag(i) = Nothing
                Else
                    sTag(i) = temp2
                End If
                '   sSpeakerNumber(i) = regexReplace(temp, "(.+\s)(voice=')(.*?)('.+)", "$3")
                ' sScript(i) = (regexReplace(temp, "(.+>)(.*?)(<.+)", "$2")).ToString.Trim
                sScript(i) = (regexReplace(temp, "(<clip.+'>)(.*?)(</clip.+)", "$2")).ToString.Trim
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
    Private Sub resetArrays()
        Me.blnRecorded.Initialize()
        Me.blnOmit.Initialize()
        Me.iNumberOfCharactersInClip.Initialize()
        Me.iNumberToRecord.Initialize()
        Me.iVoiceColor.Initialize()
        Me.iVoiceVoiceNumber.Initialize()
        Me.sAllClips.Initialize()
        Me.sBook.Initialize()
        Me.sChapter.Initialize()
        Me.sCharacterPrompts.Initialize()
        Me.sCharacterShort.Initialize()
        Me.sCharacter.Initialize()
        Me.sClipSize.Initialize()
        Me.sContinued.Initialize()
        Me.sID.Initialize()
        '    Me.sPrompt.Initialize()
        Me.sScript.Initialize()
        Me.sSpeakerNumber.Initialize()
        Me.sTag.Initialize()
        Me.sTextArray.Initialize()
        Me.sVerse.Initialize()
        Me.sVoiceCharacter.Initialize()
        Me.sCharacter.Initialize()


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
        Try
            Dim sr As StreamReader = New StreamReader(Me.sMasterFileName, Encoding.UTF8)
            temp = sr.ReadLine() ' xml
            temp = sr.ReadLine()
            iLastClipNumber = getInitialInfo(temp)
            Do While Not sr.EndOfStream
                i += 1  ' count clip numbers as iLastClipNumber is wrong at this time.
                '  MainMenu.progressBar.Value = i
                ' MainMenu.progressBar.Update()


                temp = sr.ReadLine()
                If temp.StartsWith("</dramatizer>") Then Exit Do
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
            Loop
            sr.Close()
            ' ProgressIndicator.Hide()
            '    MainMenu.progressBar.Visible = False
            ' MainMenu.tbProgress.Visible = False
            Me.iLastClipNumber = i - 1  ' this is required to set it right
            '    Dim x3 As String = Me.sCharacter(77, 1)
            '   Dim x4 As String = Me.sCharacter(79, 1)
            '  Dim x1 = x4
        Catch ex As Exception
            MessageBox.Show("Unable to read master file -- error 2 " & vbCrLf & sMasterFileName & vbCrLf & "Check to see if file is open in another application. " & vbCrLf & ex.Message & vbCrLf & "i = " & i.ToString, "File reading error", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End
        End Try
    End Sub
    Private Function getInitialInfo(ByVal temp As String)
        Dim total = regexReplace(temp, "(.+\s)(totalClips="")(.*?)(""\s.+)", "$3")
        Dim sRecordingInProgress As String = regexReplace(temp, "(.+\s)(recordingInProgress="")(.*?)(""\s.+)", "$3")
        setRecordingStatus(sRecordingInProgress)
        '   ProgressIndicator.Visible = True
        ' MainMenu.progressBar.Visible = True
        ' MainMenu.progressBar.Maximum = total + total * 0.5

        ' MainMenu.progressBar.Step = 100
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
    End Sub
    Private Sub readBookChapterVerse(ByVal temp As String, ByVal i As Integer)
        sBook(i) = regexReplace(temp, "(.+\s)(book="")(.*?)(""\s.+)", "$3")
        sChapter(i) = regexReplace(temp, "(.+\s)(chapter="")(.*?)(""\s.+)", "$3")
        sVerse(i) = regexReplace(temp, "(.+\s)(verse="")(.*?)(""\s.+)", "$3")
    End Sub
    Private Sub setRecordingStatus(ByVal sRecordingInProgress)
        If sRecordingInProgress = "#TRUE#" Then
            blnRecordingInProgress = True
            MainMenu.rbInitialize.Enabled = True
            MainMenu.rbStartProcessing.Enabled = True
            MainMenu.rbUnidentified.Enabled = False
            MainMenu.rbMultiple.Enabled = False
            MainMenu.rbVerifyUpdated.Enabled = False
            MainMenu.rbVerifyAll.Enabled = False
            MainMenu.rbAssignVoices.Enabled = False
            MainMenu.rbCreateScripts.Enabled = False
            MainMenu.rbRecord.Enabled = True
        Else
            blnRecordingInProgress = False
            '   MainMenu.rbInitialize.Enabled = True
            '  MainMenu.rbStartProcessing.Enabled = True
            ' MainMenu.rbUnidentified.Enabled = True
            ' MainMenu.rbMultiple.Enabled = False

            '  MainMenu.rbVerifyUpdated.Enabled = True
            ' MainMenu.rbVerifyAll.Enabled = True
            ' MainMenu.rbAssignVoices.Enabled = True
            '  MainMenu.rbCreateScripts.Enabled = True
            '  MainMenu.rbRecord.Enabled = True
        End If

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

                FileClose(filenum)
            Catch ex As Exception
                ' if can't read a settings file then just display defaults
                MessageBox.Show("error " & ex.Message, "Error")
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
    Private Sub readAndSetQuoteType()
        Me.cbQuoteType.Text = getCurrentInfoFromSettingsFile("<currentQuoteType>")
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
        VoiceTalentText.rtbText.Font = New Font(fontName, fontSize)
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
        identifyAllQuotes()
        iLastClipNumber = buildArrayOfClips()  ' should set last correctly xxxxxxxxxxxxxxxx
        readClipsFromFileOneLinePerClip()
        readVoiceFile()
        assignVoicesToCharacters()
        '    setDefaultCharacters()
        Me.writeClipsToMasterFileAndAdjustClipSize(Me.chkbxAdjustClipSize.Checked()) ' adjust clip size True
        '    writeCurrentSettings()
        Me.readClipsFromFileMaster()
        Me.iCurrentClipNumber = 1 ' always start at 1
        ' ForwardBackControl.displayMasterAndVoiceTalentText()
        showStatsForUnidentifiedMultipleTotal()
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
        Try
            Me.rtbEncodingANSI.Text = file2stringWithEncoding(sProjectFileName, System.Text.Encoding.UTF7)
            Me.rtbEncodingUTF8.Text = file2stringWithEncoding(sProjectFileName, System.Text.Encoding.UTF8)
        Catch ex As Exception
            MessageBox.Show("Error 1 trying to open " & Me.sProjectFileName & vbCrLf & "See if another application has the file open.", "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Stop)
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
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Filter = "Text files (*.txt, *.ptx,*.sfm)|*.txt;*.ptx;*.sfm|All files (*.*)|*.*"
        OpenFileDialog1.ShowDialog()
        sProjectFileName = OpenFileDialog1.FileName
        sProjectName = getFileNameWithoutExtensionFromFullName(sProjectFileName)
        sProjectPath = getProjectPathFromFullName(sProjectFileName)
        Me.cbFileName.Text = sProjectFileName
        Me.Text = sProjectName + " - " + sProgramName
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
        Dim temp As String
        If Me.rbEncodingANSI.Checked Then
            ' do work here
            temp = regexReplace(Me.rtbEncodingANSI.Text, "\n", "--jaa---")
        Else
            temp = regexReplace(Me.rtbEncodingUTF8.Text, "\n", "--jaa---")
            ' do work on UTF8 box
        End If
        temp = regexReplace(temp, "\r", "oojaaooo")
        If Me.cbQuoteType.SelectedIndex = 0 Then
            ' («)
            temp = regexReplace(temp, "(«)(.*?)(»)", "|" & "$1$2$3" & "|")
        ElseIf Me.cbQuoteType.SelectedIndex = 1 Then
            ' ("<<") Then
            temp = regexReplace(temp, "(<<)(.*?)(>>)", "|" & "$1$2$3" & "|")
        End If
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
        VoiceTalentText.rtbText.Font = New Font(fontName, fontSize)
        MasterText.rtbContextAbove.Font = New Font(fontName, fontSize - 2)
        MasterText.rtbTextOnly.Font = New Font(fontName, fontSize)
        MasterText.rtbTextWithContext.Font = New Font(fontName, fontSize)
    End Sub
    Private Sub rbEncodingANSI_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbEncodingANSI.CheckedChanged
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


        If checkISOcodePresent() And Me.checkQuoteTypePresent And Me.checkFileNamePresent Then
            Me.writeCurrentSettings()
            ' moved to starting
            ' createFoldersAndMasterAndScriptsFileNames()
            Me.Hide()
            MainMenu.setCheckMarksAndEnableMenuItems()
            'MainMenu.enableMenuChoices()
            MainMenu.Show()
        Else
            Beep()
            MessageBox.Show("Please correct.", "Required information missing", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
    Public Sub readVoiceFile()
        Dim temp, temp2 As String
        Dim i, j, k As Integer
        Dim sr As StreamReader = New StreamReader(sVoiceFile, Encoding.UTF8)
        Do Until sr.EndOfStream
            temp = sr.ReadLine()
            i += 1
            ' "Elisha (prophecy)",13,0
            Me.sVoiceCharacter(i) = regexReplace(temp, "("")(.*?)("")(.*?\d,)(.*?\d\d?)", "$2")
            Me.iVoiceVoiceNumber(i) = regexReplace(temp, "(.*?"",)(.*?)(,\d\d?)", "$2")
            Me.iVoiceColor(i) = regexReplace(temp, "(.*?\d,)(.*?)", "$2")
            temp2 = str.Trim(getCharacterShort(Me.sVoiceCharacter(i)))
            If temp2 <> Nothing Then
                j += 1
                Me.sCharacterShort(j) = temp2
            Else
                ' toss out nul 
            End If
            temp2 = str.Trim(getCharacterPrompt(Me.sVoiceCharacter(i)))

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
                sSpeakerNumber(i) = assignVoiceToCharacter(Me.sCharacter(i, 1))
            Else
                ' use what is in 0
                sSpeakerNumber(i) = assignVoiceToCharacter(Me.sCharacter(i, 0))
            End If
        Loop Until i > iLastClipNumber
    End Sub
    Public Function assignVoiceToCharacter(ByVal character As String)
        Dim i As Integer
        Do
            i += 1
            If sVoiceCharacter(i) = character Then Return iVoiceVoiceNumber(i)
        Loop Until sVoiceCharacter(i) = "" ' end of characters in array
        Return "0" ' not assigned
    End Function
    Public Function countUnidentified()
        Dim i, total As Integer
        total = -1
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
            If sVoiceCharacter(i) = "0" Then
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
            Me.cbAudioProgram.Items.Add("c:\Program Files\Adobe\Adobe audition 2.0\Audition.exe")
            Me.cbAudioProgram.Items.Add("c:\Program Files\Cool2000\cool2000.exe")
            Me.cbAudioProgram.Text = "c:\Program Files\Audacity\audacity.exe"
        Catch ex As Exception
            MessageBox.Show("Problem filling the audio program names control " & vbCrLf & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Private Sub btnTranslateMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        translate.Show()
    End Sub
    Private Sub btnDisplayText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        fillSampleEncodingRTBoxes()
        '       Me.panelEncoding.Show()
    End Sub
    Public Sub readMasterFile()
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
        If sProjectName = Nothing Then
            ' do nothing
        Else
            If System.IO.File.Exists(sMasterFileName) Then
                File.Delete(sMasterFileName)
            Else
                '  nothing to delete
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
        checkISOcodePresent()
    End Sub
    Private Sub cbISOcode_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbISOcode.Enter
        Dim temp As String = str.Left(Me.cbISOcode.SelectedItem, 3).Trim
        Me.tbISOcode.Text = temp
        checkISOcodePresent()
    End Sub
    Private Sub cbISOcode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbISOcode.SelectedIndexChanged
        Dim temp As String = str.Left(Me.cbISOcode.SelectedItem, 3).Trim
        Me.tbISOcode.Text = temp
        checkISOcodePresent()
    End Sub
    Private Sub cbFindISOcode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbFindISOcode.SelectedIndexChanged
        Dim temp As String = str.Right(Me.cbFindISOcode.SelectedItem, 3).Trim
        Me.tbISOcode.Text = temp
        checkISOcodePresent()
    End Sub
    Public Function checkISOcodePresent()
        Dim temp As Boolean
        If Me.tbISOcode.Text.Length = 3 Then
            Me.tbISOcode.BackColor = Color.LawnGreen
            temp = True
        Else
            Me.tbISOcode.BackColor = Color.Pink
            Me.tabControlOptions.SelectedIndex = 2
            temp = False
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
            Me.tabControlOptions.SelectedIndex = 3
            temp = False
        End If
        Return temp
    End Function
    Private Sub TrackBarClipSize_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBarClipSize.Scroll
        If Me.TrackBarClipSize.Value < 6 Then Me.TrackBarClipSize.Value = 6
        showClipSize()
    End Sub
    Public Function calculateClipSize(ByVal clipSizeInCharacters As Integer)
        Dim temp As Integer = (clipSizeInCharacters * Me.TrackBarClipSize.Value / 10 / 16.66).ToString
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
    Public Sub showStatsForUnidentifiedMultipleTotal()
        readClipsFromFileMaster()
        Dim unidentifiedToFix As String = Me.countUnidentifiedNotFixedYet.ToString & "  "
        unidentifiedToFix = unidentifiedToFix + MainMenu.sLocalizationStrings(MainMenu.iUnidentifiedCharactersToFix, MainMenu.iLanguageSelected)
        Dim unidentified As String = iUnidentifiedSpeakingCharacter & "  " & MainMenu.sLocalizationStrings(MainMenu.iUnidentifiedCharacter, MainMenu.iLanguageSelected)
        Dim multipleToFix As String = Me.countMultipleNotFixedYet.ToString & "  " & MainMenu.sLocalizationStrings(MainMenu.iMultipleCharactersToFix, MainMenu.iLanguageSelected)
        Dim multiple As String = iMultipleSpeakingCharacter & "  " & MainMenu.sLocalizationStrings(MainMenu.iMultipleCharacters, MainMenu.iLanguageSelected)
        Dim unassigned As String = Me.countUnassignedCharacters.ToString & "  " & MainMenu.sLocalizationStrings(MainMenu.iUnassignedCharactersToFix, MainMenu.iLanguageSelected)
        Dim clipsToRecord As String = Me.countClipsToRecord.ToString & "  " & MainMenu.sLocalizationStrings(MainMenu.iClipsToRecord, MainMenu.iLanguageSelected)
        Dim totalClips As String = Me.countTotal.ToString & "  " & MainMenu.sLocalizationStrings(MainMenu.iTotalClips, MainMenu.iLanguageSelected)
        Dim percent As Integer = (countTotal() - iUnidentifiedSpeakingCharacter - iMultipleSpeakingCharacter) * 100 / countTotal()
        Dim temp As String
        temp = MainMenu.TextBox1.Text
        temp = temp + "|" + unidentifiedToFix & vbCrLf & unidentified & vbCrLf & multipleToFix & vbCrLf & multiple & vbCrLf & unassigned & vbCrLf & clipsToRecord & vbCrLf & totalClips
        temp = temp + vbCrLf + percent.ToString + MainMenu.sLocalizationStrings(MainMenu.iPercentIdentified, MainMenu.iLanguageSelected)
        temp = temp + "|" & MainMenu.sLocalizationStrings(MainMenu.iClickNextToStart, MainMenu.iLanguageSelected)
        MainMenu.TextBox1.Text = MainMenu.formatTextForTextBox(temp)
        If countUnidentifiedNotFixedYet() + countMultipleNotFixedYet() = 0 Then
            MainMenu.TextBox1.BackColor = Color.LawnGreen
        Else
            MainMenu.TextBox1.BackColor = Color.LightYellow
        End If
        ' MainMenu.TextBox1.BackColor = Color.Cyan
    End Sub
    Public Sub createWaveFiles()
        Dim i As Integer

        For i = 1 To Me.iLastClipNumber
            Dim temp As String = sRecordingFolder & "\" & tbISOcode.Text & "-"
            Dim bookNumber As String = getBookNumber(i)
            Dim sequence As String = dramatizer.sPadNumber(5, i.ToString)
            Dim extra As String = justTag(i)
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

    Private Function justTag(ByVal i As Integer)
        Dim temp As String = Me.sTag(i)
        temp = Me.regexReplace(temp, "\\", "")
        If temp = Nothing Then
            If Me.blnOmit(i) = True Then
                Return ".omit"
            Else
                'skip
                Return temp
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
        Try
            Dim temp As String = MainMenu.sLocalizationStrings(MainMenu.iUnidentifiedCharacter, MainMenu.iLanguageSelected) & ": " _
         & countUnidentifiedNotFixedYet.ToString() & "           " _
         & MainMenu.sLocalizationStrings(MainMenu.iMultipleCharacters, MainMenu.iLanguageSelected) & ": " _
         & countMultipleNotFixedYet.ToString()
            If countUnidentifiedNotFixedYet() + countMultipleNotFixedYet() = 0 Then
                dramatizer.statusBar.BackColor = Color.LawnGreen
            Else
                dramatizer.statusBar.BackColor = Color.LightYellow
            End If
            dramatizer.statusBar.Text = temp
            '  MainMenu.TextBox1.Text = temp
        Catch ex As Exception
            MessageBox.Show("Error trying to display status text in forward back control." & vbCrLf & ex.Message, "Oops", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
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
        Catch ex As Exception
            MessageBox.Show("ERROR in process omitted text" & vbCrLf & ex.Message)
        End Try
    End Sub
    Public Sub fillCharacterNamesControl()
        Try
            Dim characterShort As String
            For Each characterShort In Me.sCharacterShort
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
        Dim iAdjustSize = Me.TrackBarClipSize.Value / 10

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
        Me.TrackBarClipSize.Value = 10  ' this is the middle
        Me.chkbxBreakAtParagraphs.Checked = True
        Me.chkbxAdjustClipSize.Checked = True

        Me.showClipSize()

    End Sub

    Private Sub cbQuoteType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbQuoteType.SelectedIndexChanged
        checkQuoteTypePresent()
    End Sub

    Private Sub tbISOcode_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbISOcode.TextChanged

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
        'If cbFontSize.SelectedItem = Nothing Then
        'If cbFontSize.Text = "" Then
        'fontSize = 14
        'Else
        'fontSize = cbFontSize.Text
        'End If
        'End If

        ' Dim x
        ' For x = 1 To 10
        '  me.cbFontName.Items.Add (
        ' Next
        'Me.rtbEncodingANSI.Font = New Font(fontName, fontSize)
        'Me.rtbEncodingUTF8.Font = New Font(fontName, fontSize)
        'VoiceTalentText.rtbText.Font = New Font(fontName, fontSize)
        'MasterText.rtbContextAbove.Font = New Font(fontName, fontSize - 2)
        'MasterText.rtbTextOnly.Font = New Font(fontName, fontSize)
        'MasterText.rtbTextWithContext.Font = New Font(fontName, fontSize)
    End Sub

End Class
