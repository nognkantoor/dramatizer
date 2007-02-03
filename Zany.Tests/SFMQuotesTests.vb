Imports Nunit.Framework
Imports Zany
Imports System
Imports System.IO
Imports System.Text
Imports str = Microsoft.VisualBasic.Strings

<TestFixture()> Public Class SFMQuotesTests
    Public sProgramDirectory As String
    <TestFixtureSetUp()> Public Sub TestFixtureSetup()
    End Sub
    <SetUp()> Public Sub setup()

    End Sub
    <TearDown()> Public Sub teardown()

    End Sub
    <TestFixtureTearDown()> Public Sub TestFixtureTearDown()
    End Sub
    <Test()> Public Sub Create()
        Dim sfmquotes = New SFMQuotes(New MemoryStream(), sProgramDirectory)
        Assert.IsNotNull(sfmquotes)
    End Sub
    <Test(), ExpectedException(GetType(ArgumentNullException))> _
        Public Sub Create_Nothing_Throws()
        Dim sfmquotes = New SFMQuotes(Nothing, sProgramDirectory)
    End Sub
    <Test()> Public Sub FindAllQuotes_justText()
        Dim sInput As String = "test data"
        Dim sExpectedOutput As String = convertStringToUTF8(sInput, "test")
        Dim sOutput As String
        Dim sfmQuotes As SFMQuotes = New SFMQuotes(string2stream(sInput), sProgramDirectory)
        sOutput = sfmQuotes.createClips("UTF8")
        Debug.Assert(str.StrComp(sOutput, sExpectedOutput), "output and expected output differ", "output " & sOutput & " expectedOutput " & sExpectedOutput)
    End Sub
    <Test()> Public Sub FindAllQuotes_oneSimpleQuote()
        Dim sInput As String = "before «test data» after"
        Dim sExpectedOutput As String = convertStringToUTF8("before <block character1=''>test data</block> after", "test")
        Dim sOutput As String
        Dim sr As Stream = string2stream(sInput)
        Dim sfmQuotes As SFMQuotes = New SFMQuotes(sr, sProgramDirectory)
        sOutput = sfmQuotes.createClips("UTF8")
        Debug.Assert(str.StrComp(sOutput, sExpectedOutput), "output and expected output differ", "output " & sOutput & " expectedOutput " & sExpectedOutput)
    End Sub
    <Test()> Public Sub FindAllQuotes_twoSimpleQuotes()
        Dim sInput As String = "before «test data» between «more test data» after"
        Dim sExpectedOutput As String = convertStringToUTF8("<block character1='narrator-GEN'>before <block character1=''>test data<block character1='narrator-GEN'> between <block character1=''>more test data<block character1='narrator-'> after", "test")
        Dim sOutput As String
        Dim sr As Stream = string2stream(sInput)
        Dim sfmQuotes As SFMQuotes = New SFMQuotes(sr, sProgramDirectory)
        sOutput = sfmQuotes.createClips("UTF8")
        Debug.Assert(str.StrComp(sOutput, sExpectedOutput), "output and expected output differ", "output " & sOutput & " expectedOutput " & sExpectedOutput)
    End Sub
    <Test()> Public Sub GetBookChapterVerseForQuotes_bbb()
        Dim sInput As String = "\id GEN"
        Dim sExpectedOutput As String = ("<block character1='book-chapter' tag='\id' id='GEN 0.0'>" & vbCrLf & "GEN()" & vbCrLf & "")
        Dim sOutput As String
        Dim sr As Stream = string2stream(sInput)
        Dim sfmQuotes As SFMQuotes = New SFMQuotes(sr, sProgramDirectory)
        sOutput = sfmQuotes.getBookChapterVerse()
        Debug.Assert(str.StrComp(sOutput, sExpectedOutput), "output and expected output differ", "output " & sOutput & " expectedOutput " & sExpectedOutput)
    End Sub
    Private Function string2stream(ByVal testString As String)
        ' create unicode encoding
        Dim utf8Encoding As System.Text.UTF8Encoding = New System.Text.UTF8Encoding
        ' convert string to byte array using encoding
        Dim bytes As Byte() = utf8Encoding.GetBytes(testString)
        ' initialize memory steam using byte array
        Dim convertedMemoryStream As Stream = New MemoryStream(bytes)
        Return convertedMemoryStream
    End Function
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
        sr.Dispose()
        Return temp
    End Function
End Class
