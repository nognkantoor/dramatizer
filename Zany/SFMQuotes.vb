Imports System
Imports System.IO
Imports System.Text.RegularExpressions
Imports str = Microsoft.VisualBasic.Strings
Public Class SFMQuotes
    Const sBlankSpeakingCharacter = "character1="""""
    Public colMarkers As New Collection
    Dim sProgramDirectory As String
    Public sTempFolder As String = My.Computer.FileSystem.SpecialDirectories.Temp & "\Dramatizer\"
    ' Public sDramatizeFolder As String = My.Computer.FileSystem.SpecialDirectories.MyDocuments & "\My Dramatizer"
    ' Public sProgramDirectory As String = Directory.GetCurrentDirectory ' beware that this may change
    '    Public sRequiredFilesFolder As String = sProgramDirectory & "\RequiredFiles" ' beware that this may change
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
                    newClip = str.Replace(newClip, "id='" & id, sSpeakingCharacters + " id='" + id)
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
            sSpeakingCharacters = "multiple='" + iCounter.ToString + "' " + sSpeakingCharacters
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
        ' <clip character1='' id='GEN 1.6'>
        ID = getStringValue(clip, "id='", 2)
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
    ' find all the clips
    Public Function createClips(ByVal sEncoding As String)
        Dim sw As StreamWriter = New StreamWriter(sCreateClipsFileName, False, Text.Encoding.UTF8, 512)
        Dim quotes(100) As String
        Dim temp As String
        Dim sText As String
        Dim sClipUnknown As String = vbCrLf & "</clip>" & vbCrLf & "<clip character1="""">" & vbCrLf
        Dim sClipNarrator As String = vbCrLf & "</clip>" & vbCrLf & "<clip character1=""narrator-"">" & vbCrLf
        Dim sClipExtra As String = vbCrLf & "</clip>" & vbCrLf & "<clip character1=""extra-"" tag='$1'>$2" & vbCrLf
        Dim sClipPossibleContinuation As String = sClipUnknown
        Try
            sText = stream2string(sEncoding)
            colMarkers = createListOfMarkers(sText)
            Try
                temp = regexReplace(sText, "(\\id)(\s)(...)(\s.*?)", "</clip>" & vbCrLf & "<clip character1=""book-chapter"" tag='$1'>" & vbCrLf & "$3" & sClipNarrator & "$4")
                ' remove text not used
                temp = removeUnusedText(temp)
                ' keep introduction
                temp = processIntroduction(temp, sClipExtra)
                ' End If
                ' get rid of cr and lf put them back in later
                temp = removeCRLF(temp)
                ' remove note saying what was removed
                temp = regexReplace(temp, "(\*\*\*\*)(.*?)(\*\*\*\*)", "")
                Debug.Assert(Main.cbQuoteType.Text <> "", "cb quote type not set")
                temp = processDirectQuote(temp, sClipUnknown, sClipNarrator)
                temp = restoreCRLFandAddWhenVerticalBarPresent(temp)
                ' guarantee that all \ start on new line
                temp = regexReplace(temp, "\\", vbCrLf & "\")
                temp = processStartAndEnd(temp)
                ' remove double new lines,
                temp = regexReplace(temp, vbCrLf & vbCrLf, vbCrLf)
                temp = processChapterAndVerse(temp, sClipPossibleContinuation)
                ' section
                temp = regexReplace(temp, "(\\s\d?)(\s)(.+)(\r\n)", "</clip>" & vbCrLf & "<clip character1=""extra-"" tag='$1'>" & vbCrLf & "$3" & sClipNarrator & "$4")
                ' heading
                temp = regexReplace(temp, "(\\h\d?)(\s)(.*)(\r\n)", "</clip>" & vbCrLf & "<clip character1=""book-chapter"" tag='$1'>" & vbCrLf & "$3" & sClipNarrator & "$4")
                ' book
                temp = regexReplace(temp, "(\\mt\d?)(\s)(.*)(\r\n)", "</clip>" & vbCrLf & "<clip character1=""book-chapter"" tag='$1'>" & vbCrLf & "$3" & sClipNarrator & "$4")
                temp = processContinuingQuotes(temp, sClipUnknown)
                temp = processCleanUP(temp)
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
    Private Function removeCRLF(ByVal temp As String)
        temp = regexReplace(temp, "\n", "--jaa---")
        temp = regexReplace(temp, "\r", "oojaaooo")
        Return temp
    End Function
    Private Function processStartAndEnd(ByVal temp As String)
        ' start file with <clip character1='narrator-'>
        temp = regexReplace(temp, "^", "<clip character1=""narrator-"">" & vbCrLf)
        ' end file with </clip>
        temp = regexReplace(temp, "(\r\n)$", "$1" & vbCrLf & "</clip>" & vbCrLf)
        Return temp
    End Function
    Private Function processChapterAndVerse(ByVal temp As String, ByVal sClipPossibleContinuation As String)
        ' chapter
        ' keep chapter number
        temp = regexReplace(temp, "(\\c)(\s)(\d+)", "</clip>" & vbCrLf & "<clip character1=""book-chapter"" tag='$1'>" & vbCrLf & "$3" & sClipPossibleContinuation)
        ' temp = regexReplace(temp, "(\\c)(\s)(\d+)", vbCrLf & "<chapterNumber>$3</chapterNumber>")
        ' --------------------------------------------------------
        ' verse
        ' temp = regexReplace(temp, "(\\v)(\s)(.+?)(\s)", "</clip>" & vbCrLf & "<clip character1=""extra-"" tag='$1'>" & vbCrLf & "$3" & sClipNarrator & "$4")
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
    Private Function processCleanUp(ByVal temp)
        ' remove double new lines
        temp = regexReplace(temp, vbCrLf & vbCrLf, vbCrLf)
        ' remove empty narrator
        temp = regexReplace(temp, "(<clip character1=""narrator-"">)(\r\n)*(</clip>)", vbCrLf)
        ' add nl before </clip>
        temp = regexReplace(temp, "(</clip>)", vbCrLf & "$1")
        ' remove double new lines
        temp = regexReplace(temp, "(\r\n)+", vbCrLf)
        ' remove double close
        temp = regexReplace(temp, "</clip>" & vbCrLf & "</clip>", "</clip>")
        ' remove double new lines
        temp = regexReplace(temp, vbCrLf & vbCrLf, vbCrLf)
        ' remove initial nl
        temp = regexReplace(temp, "(^\r\n)(<clip)", "$2")
        Return temp
    End Function
    Private Function processContinuingQuotes(ByVal temp As String, ByVal sClipUnknown As String)
        ' continuing quotes
        If Main.cbQuoteType.Text = "« ... »" Then ' 
            '         temp = regexReplace(temp, "(>\w)(«)", "|" & "</clip>" & vbCrLf & sClipUnknown & "$1$2$3")
            'temp = regexReplace(temp, "(\\p\n\n<verse>.*?\n</verse>)(\w)(«)", "|" & "</clip>" & vbCrLf & sClipUnknown & "$1$2$3")
            temp = regexReplace(temp, "([^\n])(«)", sClipUnknown & "$1$2$3")
            ' works but then makes the verse ref wrong for the following quote start loaction           temp = regexReplace(temp, "(\\p)(\r\n)+(<verse>\r\n\d*\r\n</verse> )?(«)", sClipUnknown & "$1$2$3$4")
        ElseIf Main.cbQuoteType.Text = "<< ... >>" Then ' 
            temp = regexReplace(temp, "([^\n])(<<)", sClipUnknown & "$1$2$3")
        End If
        Return temp

    End Function
    Private Function removeUnusedText(ByVal temp As String)
        ' \ide  encoding
        temp = regexReplace(temp, "(\\ide)(\s)(.*?)(\r\n)", "****ide removed****")
        ' \r  cross references
        temp = regexReplace(temp, "(\\r)(\s)(.*?)(\r\n)", "****r removed****")
        ' \f \f*  footnote
        temp = regexReplace(temp, "(\\f)(\s)(.*?)(\\f\*)", "****footnote removed****")
        ' \xref to \-xref  reference    
        temp = regexReplace(temp, "(\\xref)(.*?)(\-xref)", "****xref -xref removed****")
        ' \ref to \-ref  reference    
        temp = regexReplace(temp, "(\\ref)(.*?)(\-ref)", "****ref -ref removed****")
        Return temp
    End Function
    Private Function processDirectQuote(ByVal temp As String, ByVal sClipUnknown As String, ByVal sClipNarrator As String)
        If Main.cbQuoteType.Text = "« ... »" Then ' 
            temp = regexReplace(temp, "(«)(.*?)(»)", "|" & sClipUnknown & "$1$2$3" & sClipNarrator & "|")
        ElseIf Main.cbQuoteType.Text = "<< ... >>" Then ' 
            temp = regexReplace(temp, "(<<)(.*?)(>>)", "|" & sClipUnknown & "$1$2$3" & sClipNarrator & "|")
        End If
        Return temp
    End Function
    Private Function processIntroduction(ByVal temp As String, ByVal sClipExtra As String)
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
    Private Function regexReplace(ByVal sInput As String, ByVal sFind As String, ByVal sReplace As String)
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
            If temp1.Contains("\id") = True Then ' contents of \id on following line
                temp2 = sr.ReadLine
                book = temp2
            ElseIf temp1.Contains("\c") Then ' contents of \c on following line
                temp2 = sr.ReadLine
                chapter = temp2
                verse = "1"
            ElseIf temp1.StartsWith("<verse>") Then ' contents of \v on following line
                temp2 = sr.ReadLine
                verse = temp2
                ' ElseIf temp1.Contains("'>") Then
                '         temp = temp.Replace("'>", "' id='" & book & " " & chapter & "." & verse & "'>")
                'ElseIf temp1.Contains(""">") Then
                'temp = temp.Replace(""">", """ id='" & book & " " & chapter & "." & verse & "'>")
            End If
            ' ending with single quote
            temp1 = temp1.Replace("'>", "' id='" & book & " " & chapter & "." & verse & "'>")
            ' ending with double quote
            temp1 = temp1.Replace(""">", """ id='" & book & " " & chapter & "." & verse & "'>")
            temp1 = temp1.Replace("""narrator-""", """narrator-" & book & """")
            temp1 = temp1.Replace("""extra-""", """extra-" & book & """")
            If temp1 <> "" Then sw.WriteLine(temp1)
            If temp2 <> "" Then sw.WriteLine(temp2)
        Loop
        sr.Close()
        sw.Close()
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
