Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports str = Microsoft.VisualBasic.Strings
Public Class SFMQuotes
    Public sBlankSpeakingCharacter = "character1="""""
    Public colMarkers As New Collection
    Dim sProgramDirectory As String
    Public sTempFolder As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\Dramatizer\"
    ' Public sDramatizeFolder As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\My Dramatizer"
    ' Public sProgramDirectory As String = Directory.GetCurrentDirectory ' beware that this may change
    ' Public sRequiredFilesFolder As String = sProgramDirectory & "\RequiredFiles" ' beware that this may change
    Public sINIfile As String = sRequiredFilesFolder & "\zany.ini"
    Public sGetBookChapterVerseFileName As String = sTempFolder & "\02 - bookChapterVerse.tmp"
    Public sIdentifySpeakingCharactersFileName As String = sTempFolder & "\03 - identifyCharacters.tmp"
    Public sCreateClipsFileName As String = sTempFolder & "\01 - createClips.tmp"
    Public sMasterFileName As String = sTempFolder & "\04 - master.tmp"
    '   Public sCharacterNames_BookChapterVerseFileName As String = sRequiredFilesFolder & "\CharacterNames_BookChapterVerse.txt"
    Public sTempFileName As String = sTempFolder & "\temp.tmp"
    Dim sRequiredFilesFolder As String
    Dim sCharacterNames_BookChapterVerseFileName As String
    Dim myStream As Stream
    Public Sub New(ByVal stream As Stream, ByVal temp As String)
        If stream Is Nothing Then
            Throw New ArgumentNullException()
        End If
        myStream = stream
        Me.sProgramDirectory = temp
        sRequiredFilesFolder = sProgramDirectory & "\RequiredFiles" ' beware that this may change
        sCharacterNames_BookChapterVerseFileName = sRequiredFilesFolder & "\CharacterNames_BookChapterVerse.txt"
    End Sub
    Public Function identifySpeakingCharacters()
        '  ForwardBackControl.Show()
        '  Me.Show()
        ' Me.progressBar.Show()
        'Me.tbProgress.Show()
        Dim sr As StreamReader = New StreamReader(sGetBookChapterVerseFileName, Text.Encoding.UTF8, True)
        Dim sw As StreamWriter = New StreamWriter(sIdentifySpeakingCharactersFileName, False, Text.Encoding.UTF8)
        Dim id, clip, newClip, sSpeakingCharacters As String
        Dim i As Integer
        Do While Not sr.EndOfStream
            i += 1
            '   Me.tbProgress.Text = i.ToString
            '  Me.tbProgress.Update()
            clip = sr.ReadLine
            newClip = clip
            If clip.Contains(sBlankSpeakingCharacter) Then
                ' get id for clip
                id = getID(clip)
                ' get all matching data from DataFile
                sSpeakingCharacters = getAllMatchingDataFromDataFile(id)
                ' insert data into output stream
                If sSpeakingCharacters = Nothing Then
                    ' skip - this one will be unidentified
                Else
                    ' remove blank speaking character
                    newClip = str.Replace(newClip, sBlankSpeakingCharacter, "")
                    newClip = str.Replace(newClip, "id=""" & id, sSpeakingCharacters + " id=""" + id)
                End If
            End If
            sw.WriteLine(newClip)
        Loop
        sr.Close()
        sw.Close()
        Dim sr2 As StreamReader = New StreamReader(sIdentifySpeakingCharactersFileName, Text.Encoding.UTF8, True)
        Dim sText As String = sr2.ReadToEnd
        '    ProgressIndicator.Hide()
        Return sText
    End Function
    Private Function getAllMatchingDataFromDataFile(ByVal id As String)
        Dim sr As StreamReader = New StreamReader(sCharacterNames_BookChapterVerseFileName, Text.Encoding.UTF8)
        Dim sSpeakingCharacters As String = ""
        Dim data As String
        Dim iCounter As Integer = 0
        Do While Not sr.EndOfStream
            data = sr.ReadLine
            If str.InStr(data, id + " ") Then
                iCounter = iCounter + 1
                sSpeakingCharacters = sSpeakingCharacters + "character" + iCounter.ToString + "=""" + getSpeakingCharacter(data) + """" + " "
            Else
            End If
          Loop
        If iCounter > 1 Then
            sSpeakingCharacters = "multiple=""" + iCounter.ToString + """ " + sSpeakingCharacters
        End If
        Return sSpeakingCharacters
    End Function
    Private Function getSpeakingCharacter(ByVal data As String)
        Dim SpeakingCharacter As String
        ' 1CH 10.4 character=Saul
        SpeakingCharacter = getStringValue(data, "character=", 0)
        Return SpeakingCharacter
    End Function
    Private Function getID(ByVal clip As String)
        Dim ID
        ' <clip character1="" id='GEN 1.6'>
        ID = getStringValue(clip, "id=""", 2)
        Return ID
    End Function
    Private Function getStringValue(ByVal input As String, ByVal searchFor As String, ByVal offsetFromRight As Integer)
        Dim sFound As String = ""
        Dim startOfFound As Int16
        Dim endOfFound As Int16
        Dim lengthOfFound As Int16
        startOfFound = str.InStr(input, searchFor) + searchFor.Length - 1
        endOfFound = input.Length - offsetFromRight
        lengthOfFound = endOfFound - startOfFound
        sFound = input.Substring(startOfFound, lengthOfFound)
        Return sFound
    End Function
    Public Function stream2string(ByVal sEncoding)
        Dim myString As String
        ' Create an instance of StreamReader to read from a file.
        Dim sr As StreamReader = New StreamReader(myStream, getStringEncoding(sEncoding), True)
        sr.BaseStream.Position = 0
        myString = sr.ReadToEnd
        sr.Close()
        Return myString
    End Function
    Public Shared Function processDialogueQuotesToMakeRegular(ByVal temp As String)
        Select Case Main.cbDialogueQuoteType.Text
            Case Main.sQuoteTypeNA
                ' nothing to process as we don't have dialogue quotes
            Case Main.sQuoteTypeANSIEmDash
                ' paragraph optional verse space em-dash text optional verse text  stop at nl 
                temp = regexReplace(temp, "\n?\r?" + Chr(151), "quotation-quote-start")
            Case Main.sQuoteTypeEmDash
                temp = regexReplace(temp, "\n?\r?" + ChrW(8212), "quotation-quote-start")
            Case Main.sQuoteTypeQuotationDash
                temp = regexReplace(temp, "\n?\r?" + ChrW(8213), "quotation-quote-start")
            Case Main.sQuoteTypeDoubleEquals
                temp = regexReplace(temp, "\n?\r?" + "==", "quotation-quote-start")
            Case Main.sQuoteTypeDoubleHyphen
                temp = regexReplace(temp, "\n?\r?" + "--", "quotation-quote-start")
            Case Else
                Debug.Assert(True, "unknown quote type in process quotes to make regular")
        End Select
        ' no verse number at start, put quote mark      dialogue quotes end with /nl/, /(/, or /--/.
        ' qqqqqqqqqqq used to replace quotation-quote-start so it isn't rematched
        temp = regexReplace(temp, "(\\p\n?\r?)(\n?\\v [0-9]+)?(\s?quotation-quote-start)(.*)((\n\\v)(.*))*(\(|quotation-quote-start)", "$2«--$1 qqqqqqqqqqq$4$5$6$7$8--»")
        temp = regexReplace(temp, "(\\p\n?\r?)(\n?\\v [0-9]+)?(\s?quotation-quote-start)(.*)((\n\\v)(.*))*", "$2«--$1 qqqqqqqqqqq$4$5$6$7$8--»")
        'temp = regexReplace(temp, "(\\p\n?\r?)(\s?quotation-quote-start)((.*)(\n\\v)?)*((quotation-quote-start)|(\())?", "«--yyy$1 qqqqqqqqqqq$3$4$5$6$7$8--»")
        'temp = regexReplace(temp, "(\\p\n?\r?)(\n?\\v [0-9]+)?(\s)?(quotation-quote-start)((.*)(\n\\v)?)*((quotation-quote-start)|(\())?", "«--$1 $2qqqqqqqqqqq$4$5$6$7$8--»")
        ' put first verse number before quote mark so quote will be identified correctly
        'temp = regexReplace(temp, "(\\p\n?\r?)(\s)?(\\v [0-9]+\n?\r?)?(\s)?(quotation-quote-start)((.*)(\n\\v)?)*((\s?quotation-quote-start)|(\())?", "$2$3«--$1 $4qqqqqqqqqqq$6$7$8--»")
        ' no verse number at start, put quote mark      dialogue quotes end with /nl/, /(/, or /--/.
        ' possible verse number start -- is so rearrange verse number at start, put quote mark      dialogue quotes end with /nl/, /(/, or /--/.
        ' put first verse number before quote mark so quote will be identified correctly
        'temp = regexReplace(temp, "(\\p\n?\r?)(\s)?(\\v [0-9]+\n?\r?)?(\s)?(quotation-quote-start)((.*)(\n\\v)?)*", "$2$3«--$1 $4qqqqqqqqqqq$6$7$8--»")

        ' temp = regexReplace(temp, "(\\p\n?\r?)(\s)(\s\\v [0-9]+\n?\r?)(\s)(quotation-quote-start)(.*)(\n?\r?\\v)*(.*)(quotation-quote-start)|(\\p\n?\r?)(\s)(\s\\v [0-9]+\n?\r?)(\s)(quotation-quote-start)(.*)(\n?\r?\\v)*(.*)(\()|(\\p\n?\r?)(\s)(\s\\v [0-9]+\n?\r?)(\s)(quotation-quote-start)(.*)(\n?\r?\\v)*(.*)", "«--$1 $2$3$4$5$6$7$8--»")
        'temp = regexReplace(temp, "(\\p\n?\r?)(\s\\v [0-9]+\n?\r?)(\s)(quotation-quote-start)(.*)(\n?\r?\\v)*(.*)(\()|(\\p\n?\r?)(\s)(quotation-quote-start)(.*)(\n?\r?\\v)*(.*)(--)|(\\p\n?\r?)(\s)(quotation-quote-start)(.*)(\n?\r?\\v)*(.*)", "«--$1 $2$3$4$5$6$7$8--»")
        '      temp = regexReplace(temp, "(\\p\n?\r?)(\s\\v [0-9]+\n?\r?)(\s)(quotation-quote-start)(.*)(\n?\r?\\v)*(.*)(\()?|(\-\-)?", "$2$3«--$1 $4$5$6$7$8--»")
        ' remove /quotation-quote-start/

        temp = regexReplace(temp, "qqqqqqqqqqq", "")
        temp = regexReplace(temp, "quotation-quote-start", "")
        ' remove /$7/ /$8/  --- fix this at latter date if not needed xxxxxxxxxxxxxxxxxxxxx
        temp = regexReplace(temp, "\$7", "")
        temp = regexReplace(temp, "\$8", "")
        Return temp

    End Function
    Public Shared Function processQuotesToMakeRegular(ByVal temp As String)
        Select Case Main.cbQuoteType.Text
            Case Main.sQuoteTypeCheverons
                ' this is how we want all the quotes
            Case Main.sQuoteTypeSIL
                temp = regexReplace(temp, "<<", "«")
                temp = regexReplace(temp, ">>", "»")
            Case Main.sQuoteTypeSmart
                temp = regexReplace(temp, Main.sQuoteTypeSmartOpen, "«")
                temp = regexReplace(temp, Main.sQuoteTypeSmartClose, "»")
            Case Main.sQuoteTypeStraight
                temp = regexReplace(temp, " """, " «")
                temp = regexReplace(temp, """ ", "» ")
                temp = regexReplace(temp, """", "»") ' any left over straight quote is a final
            Case Else
                Debug.Assert(True, "unknown quote type in process quotes to make regular")
        End Select
        ' quotes regularly follow footnote end so you must check for this condition in the text
        ' temp = regexReplace(temp, "\f* «", "\f* XXXXXX»") ' any quote following footnote should be final.
        Return temp

    End Function
    ' find all the clips
    Public Function createClips(ByVal sEncoding As String)
        Dim sw As StreamWriter = New StreamWriter(sCreateClipsFileName, False, Text.Encoding.UTF8, 512)
        Dim quotes(100) As String
        Dim temp As String
        Dim sText As String
        Dim sClipUnknown As String = vbCrLf & "</clip>" & vbCrLf & "<clip " & sBlankSpeakingCharacter & ">" & vbCrLf
        Dim sClipNarrator As String = vbCrLf & "</clip>" & vbCrLf & "<clip character1=""narrator-"">" & vbCrLf
        Dim sClipExtra As String = vbCrLf & "</clip>" & vbCrLf & "<clip character1=""extra-"" tag=""$1"">$2" & vbCrLf
        Dim sClipExtra2 As String = vbCrLf & "</clip>" & vbCrLf & "<clip character1=""extra-"" tag=""$1"">$3" & vbCrLf
        Dim sClipPossibleContinuation As String = sClipUnknown
        Try
            sText = stream2string(sEncoding)
            colMarkers = createListOfMarkers(sText)
            Try
                temp = processRemoveUnusedText(sText)
                temp = processQuotesToMakeRegular(temp)
                temp = processDialogueQuotesToMakeRegular(temp)
                'sw.Write(temp)
                'sw.Close()
                temp = regexReplace(temp, "(\\id)(\s)(...)(\s.*?)", "</clip>" & vbCrLf & "<clip character1=""book-chapter"" tag=""$1"">" & vbCrLf & "$3" & sClipNarrator & "$4")
                ' keep introduction
                temp = processIntroduction(temp, sClipExtra, sClipExtra2)
                ' get rid of cr and lf put them back in later
                temp = removeCRLF(temp)
                temp = processRemoveUnusedText(temp)
                Debug.Assert(Main.cbQuoteType.Text <> "", "cb quote type not set")
                temp = processDirectQuote(temp, sClipUnknown, sClipNarrator)
                temp = restoreCRLFandAddWhenVerticalBarPresent(temp)
                '   temp = processRemoveUnusedText(temp)
                'sw.Write(temp)
                'sw.Close()
                temp = processRegularize(temp)
                ' guarantee that all \ start on new line except the tag='\xxx'
                'temp = regexReplace(temp, "([^""])(\\)", vbCrLf & "$1")
                temp = regexReplace(temp, "(\\)", vbCrLf & "$1")
                '    temp = regexReplace(temp, "(\r\n)(\\id)", "$2")
                temp = processStartAndEnd(temp, sClipNarrator)
                ' remove double new lines
                ' temp = regexReplace(temp, "(\r\n)+", vbCrLf)
                ' remove double new lines,
                'temp = regexReplace(temp, vbCrLf & vbCrLf, vbCrLf)
                temp = processChapterAndVerse(temp, sClipNarrator)
                ' section
                temp = regexReplace(temp, "(\\s\d?)(\s)(.+)(\r\n)", "</clip>" & vbCrLf & "<clip character1=""extra-"" tag=""$1"">" & vbCrLf & "$3" & sClipNarrator & "$4")
                ' heading
                temp = regexReplace(temp, "(\\h\d?)(\s)(.*)(\r\n)", "</clip>" & vbCrLf & "<clip character1=""book-chapter"" tag=""$1"">" & vbCrLf & "$3" & sClipNarrator & "$4")
                ' book
                temp = regexReplace(temp, "(\\mt\d?)(\s)(.*)(\r\n)", "</clip>" & vbCrLf & "<clip character1=""book-chapter"" tag=""$1"">" & vbCrLf & "$3" & sClipNarrator & "$4")
                temp = processContinuingQuotes(temp, sClipUnknown)
                temp = processCleanUp(temp)
                ' must do this one first
                temp = processReturnToStartingDialogueQuoteMarks(temp)
                temp = processReturnToStartingQuoteMarks(temp)
                ' save intermediate work
                sw.Write(temp)
                sw.Close()
                sw.Dispose()
            Catch exp As Exception
                MsgBox("An error was encountered when attempting to parse the " & _
                    "source text. This can be caused by an invalid regex pattern." & _
                    "  Check your expression and try again." & vbCrLf & exp.Message, _
                    MsgBoxStyle.Critical, Main.Text)
                Return "An error was encountered when attempting to parse the " & _
                    "source text. This can be caused by an invalid regex pattern." & _
                    "  Check your expression and try again." & vbCrLf & exp.Message
            End Try
            Return temp
        Catch E As Exception
            ' Let the user know what went wrong.
            Console.WriteLine("The file could not be read:")
            Console.WriteLine(E.Message)
            MessageBox.Show("Error in create clips " & vbCrLf & E.Message, "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Return "Regex Error -- or file in use by another program"
        End Try
        Return "Error opening file - maybe regex problem"
    End Function
    Private Function processReturnToStartingQuoteMarks(ByVal temp As String)
        Select Case Main.cbQuoteType.Text
            Case Main.sQuoteTypeCheverons
                ' this is how we want all the quotes
            Case Main.sQuoteTypeSIL
                temp = regexReplace(temp, "«", "<<")
                temp = regexReplace(temp, "»", ">>")
            Case Main.sQuoteTypeSmart
                temp = regexReplace(temp, "«", Main.sQuoteTypeSmartOpen)
                temp = regexReplace(temp, "»", Main.sQuoteTypeSmartClose)
            Case Main.sQuoteTypeStraight
                temp = regexReplace(temp, "«", """")
                temp = regexReplace(temp, "»", """")
            Case Else
                Debug.Assert(True, "Missing quote type in processReturnToStartingQuoteMarks")
        End Select
        Return temp
    End Function
    Private Function processReturnToStartingDialogueQuoteMarks(ByVal temp As String)
        Select Case Main.cbDialogueQuoteType.Text
            Case Main.sQuoteTypeNA, ""
                ' skip as not used
            Case Main.sQuoteTypeANSIEmDash
                temp = regexReplace(temp, "«--", Chr(151))
                temp = regexReplace(temp, "--»", "")
            Case Main.sQuoteTypeEmDash
                temp = regexReplace(temp, "«--", ChrW(8212))
                temp = regexReplace(temp, "--»", "")
            Case Main.sQuoteTypeQuotationDash
                temp = regexReplace(temp, "«--", ChrW(8213))
                temp = regexReplace(temp, "--»", "")
            Case Main.sQuoteTypeDoubleEquals
                temp = regexReplace(temp, "«--", "==")
                temp = regexReplace(temp, "--»", "")
            Case Main.sQuoteTypeDoubleHyphen
                temp = regexReplace(temp, "«--", "--")
                temp = regexReplace(temp, "--»", "")
            Case Else
                Debug.Assert(True, "Missing quote type in processReturnToStartingQuoteMarks")
        End Select
        Return temp
    End Function
    Private Function removeCRLF(ByVal temp As String)
        temp = regexReplace(temp, "\n", "--jaa---")
        temp = regexReplace(temp, "\r", "oojaaooo")
        Return temp
    End Function
    Private Function processStartAndEnd(ByVal temp As String, ByVal sClipNarrator As String)
        ' start file with <clip character1=""narrator-"">
        temp = regexReplace(temp, "^", sClipNarrator & vbCrLf)
        ' end file with </clip>
        temp = regexReplace(temp, "(\r\n)$", "$1" & vbCrLf & "</clip>" & vbCrLf)
        Return temp
    End Function
    Private Function processChapterAndVerse(ByVal temp As String, ByVal sClipNarrator As String)
        ' chapter
        ' keep chapter number
        temp = regexReplace(temp, "(\\c)(\s)(\d+)", "</clip>" & vbCrLf & "<clip character1=""book-chapter"" tag=""$1"">" & vbCrLf & "$3" & sClipNarrator) ' sClipPossibleContinuation)
        ' temp = regexReplace(temp, "(\\c)(\s)(\d+)", vbCrLf & "<chapterNumber>$3</chapterNumber>")
        ' --------------------------------------------------------
        ' verse
        ' temp = regexReplace(temp, "(\\v)(\s)(.+?)(\s)", "</clip>" & vbCrLf & "<clip character1=""extra-"" tag=""$1"">" & vbCrLf & "$3" & sClipNarrator & "$4")
        temp = regexReplace(temp, "(\\v)(\s)(.+?)(\s)", vbCrLf & "<verse>" & vbCrLf & "$3" & vbCrLf & "</verse>$4")
        ' --------------------------------------------------------
        Return temp
    End Function
    Private Function restoreCRLFandAddWhenVerticalBarPresent(ByVal temp)
        temp = regexReplace(temp, "--jaa---", vbCrLf)
        temp = regexReplace(temp, "oojaaooo", vbCrLf)
        temp = regexReplace(temp, vbCrLf & vbCrLf, vbCrLf)
        temp = regexReplace(temp, "\|\|", "\|")
        temp = regexReplace(temp, "\|", vbCrLf)
        Return temp
    End Function
    Private Function processRegularize(ByVal temp)
        ' change \m to \p
        temp = regexReplace(temp, "(\\m.?)", "\p")
        Return temp
    End Function
    Private Function processCleanUp(ByVal temp)
        ' remove double new lines
        temp = regexReplace(temp, vbCrLf & vbCrLf, vbCrLf)
        ' remove empty narrator at start
        temp = regexReplace(temp, "(<clip character1=""narrator-"".*)(\r\n)(</clip>)", vbCrLf)
        ' add nl before </clip>
        temp = regexReplace(temp, "(</clip>)", vbCrLf & "$1")
        ' remove double new lines
        temp = regexReplace(temp, "(\r\n)+", vbCrLf)
        ' remove double close
        temp = regexReplace(temp, "</clip>" & vbCrLf & "</clip>", "</clip>")
        ' remove double new lines
        temp = regexReplace(temp, vbCrLf & vbCrLf, vbCrLf + "333333xxxxx")
        ' remove initial nl
        temp = regexReplace(temp, "(^\r\n)(</clip>)", "")
        ' remove empty text following chapter
        ' <clip character1="" id="MRK 1.1">nl</clip>
        temp = regexReplace(temp, "(<clip character1="""".*?>)(" + vbCrLf + ")*(</clip>)", vbCrLf + "xxxx")
        Return temp
    End Function
    Private Function processContinuingQuotes(ByVal temp As String, ByVal sClipUnknown As String)
        ' continuing quotes
        temp = regexReplace(temp, "([^\n])(«)", sClipUnknown & "$1$2")
        Return temp
    End Function
    Public Shared Function processRemoveUnusedText(ByVal temp As String)
        ' \quote \-quote \b   remove
        temp = regexReplace(temp, "(\\quote)|(\\-quote)|(\\b)", "")
        ' \ide  encoding
        temp = regexReplace(temp, "(\\ide)(\s)(.*?)(\r\n)", "****ide removed****")
        ' \r  cross references
        temp = regexReplace(temp, "(\\r)(\s)(.*?)(\r\n)", "****r removed****")
        ' \f \f*  footnote
        ' adding the \s at the end seems strange .. needed for niv smart quotes MRK 10.19 XXXXXXXXXXX
        temp = regexReplace(temp, "(\\f)(\s)(.*?)(\\f\*)\s""", """****footnote removed****")
        temp = regexReplace(temp, "(\\f)(\s)(.*?)(\\f\*)", "****footnote removed****")
        '   ' \f \f*  footnote
        '  temp = regexReplace(temp, "(\\f)(\s)(.*?)(\\f\*)", "****footnote removed****")
        ' \xref to \-xref  reference    
        temp = regexReplace(temp, "(\\xref)(.*?)(\-xref)", "****xref -xref removed****")
        ' \ref to \-ref  reference    
        temp = regexReplace(temp, "(\\ref)(.*?)(\-ref)", "****ref -ref removed****")
        ' remove note saying what was removed
        temp = regexReplace(temp, "(\*\*\*\*)(.*?)(\*\*\*\*)", "")
        Return temp
    End Function
    Private Function processDirectQuote(ByVal temp As String, ByVal sClipUnknown As String, ByVal sClipNarrator As String)
        temp = regexReplace(temp, "(«)(.*?)(»)", "|" & sClipUnknown & "$1$2$3" & sClipNarrator & "|")
        '  If Main.cbQuoteType.Text = "«...»" Then ' 
        ' temp = regexReplace(temp, "(«)(.*?)(»)", "|" & sClipUnknown & "$1$2$3" & sClipNarrator & "|")
        ' ElseIf Main.cbQuoteType.Text = "<<...>>" Then ' 
        ' temp = regexReplace(temp, "(<<)(.*?)(>>)", "|" & sClipUnknown & "$1$2$3" & sClipNarrator & "|")
        ' ElseIf Main.cbQuoteType.Text = " ""..."" " Then ' "" equals " 
        ' temp = regexReplace(temp, "( "")(.*?)("")( |oojaaooo)", "|" & sClipUnknown & "$1$2$3" & sClipNarrator & "|")
        ' End If
        Return temp
    End Function
    Private Function processIntroduction(ByVal temp As String, ByVal sClipExtra As String, ByVal sClipExtra2 As String)
        ' \imt \imt1 \imt2 \imt3 intro material    
        temp = regexReplace(temp, "(\\imt(1|2|3)?)(.*?)(\r\n)", sClipExtra2)
        ' \io \io1 \io2 \io3  intro material    
        temp = regexReplace(temp, "(\\io(1|2|3)?)(.*?)(\r\n)", sClipExtra2)
        ' \ip  intro material    
        temp = regexReplace(temp, "(\\ip)(.*?)(\r\n)", sClipExtra)
        ' \is  intro material    
        temp = regexReplace(temp, "(\\is)(.*?)(\r\n)", sClipExtra)
        Return temp
    End Function
    Private Function regexReplaceDotIncludesNL(ByVal sInput As String, ByVal sFind As String, ByVal sReplace As String)
        Dim expression As Regex
        expression = New Regex(sFind, RegexOptions.Singleline)
        Return expression.Replace(sInput, sReplace)
    End Function
    Public Shared Function regexReplace(ByVal sInput As String, ByVal sFind As String, ByVal sReplace As String)
        Dim expression As Regex
        expression = New Regex(sFind)
        Return expression.Replace(sInput, sReplace)
    End Function
    Public Function getStringEncoding(ByVal sTextEncoding)
        Dim sEncoding As Text.Encoding
        If sTextEncoding = "ANSI" Then
            sEncoding = System.Text.Encoding.GetEncoding(0)
        Else
            sEncoding = System.Text.Encoding.UTF8
        End If
        Return sEncoding
    End Function
    Public Function getBookChapterVerse()
        Dim sr As StreamReader = New StreamReader(sCreateClipsFileName, Text.Encoding.UTF8, True, 512)
        Dim sw As StreamWriter = New StreamWriter(sGetBookChapterVerseFileName, False, Text.Encoding.UTF8, 512)
        Dim blnIdFound = False
        Dim blnChapterFound = False
        Dim blnVerseFound = False
        Dim book As String = "BBB"
        Dim chapter As String = "0"
        Dim verse As String = "0"
        Dim temp1 As String = ""
        Dim temp2 As String = ""
        ' assumption that <clip ....> and </clip> are on lines by themselves
        ' read line
        Do While Not sr.EndOfStream
            temp1 = sr.ReadLine
            temp2 = ""
            ' store book, chapter, verse info
            If temp1.Contains("\ide") = True Then ' contents of \id on following line
                temp2 = sr.ReadLine
            ElseIf temp1.Contains("\id") = True Then ' contents of \id on following line
                blnIdFound = True
                temp2 = sr.ReadLine
                book = temp2
            ElseIf temp1.Contains("\c") Then ' contents of \c on following line
                blnChapterFound = True
                temp2 = sr.ReadLine
                chapter = temp2
                verse = "1"
            ElseIf temp1.StartsWith("<verse>") Then ' contents of \v on following line
                blnVerseFound = True
                temp2 = sr.ReadLine
                verse = temp2
                ' ElseIf temp1.Contains("'>") Then
                '         temp = temp.Replace("'>", "' id=""" & book & " " & chapter & "." & verse & "'>")
                'ElseIf temp1.Contains(""">") Then
                'temp = temp.Replace(""">", """ id=""" & book & " " & chapter & "." & verse & "'>")
            End If
            ' end in single quotes 
            ' temp1 = temp1.Replace("'>", "' id=""" & book & " " & chapter & "." & verse & """>")
            ' with double quote
            temp1 = temp1.Replace(""">", """ id=""" & book & " " & chapter & "." & verse & """>")
            temp1 = temp1.Replace("""narrator-""", """narrator-" & book & """")
            temp1 = temp1.Replace("""extra-""", """extra-" & book & """")
            If temp1 <> "" Then sw.WriteLine(temp1)
            If temp2 <> "" Then sw.WriteLine(temp2)
        Loop
        sr.Close()
        sw.Close()
        If Not blnIdFound Then MessageBox.Show("missing \id", "missing marker", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        If Not blnChapterFound Then MessageBox.Show("missing \c", "missing marker", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        If Not blnVerseFound Then MessageBox.Show("missing \v", "missing marker", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        If blnIdFound And blnChapterFound And blnVerseFound Then
            ' continue
        Else
            End
        End If
        Return temp1 & temp2 ' for testing
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
    Private Function createListOfMarkers(ByVal sText As String)
        Dim markers As New Collection
        Dim temp() As String
        Dim temp2 As String
        Try
            temp = sText.Split("\")
            Dim i
            For i = 1 To temp.Length - 1
                temp2 = regexReplace(temp(i), "^(.+?)(\s|\t)(.*)", "$1")
                Select Case temp2
                    Case "id"
                        ' ignore \id and \ide
                    Case "ide"
                        ' ignore \id and \ide
                    Case "f", "f*"
                        ' ignore \f and \f*
                    Case "r"
                        ' ignore \r
                    Case Else
                        markers.Add(temp2)
                End Select
            Next
        Catch ex As Exception
            MessageBox.Show("Error creating list of markers " & vbCrLf & ex.Message, "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Stop)
        End Try
        Return markers
    End Function
End Class
