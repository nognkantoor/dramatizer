Public Class MasterText


    ' Private Sub btnShowHideContext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If btnShowHideContext.Text = MainMenu.sLocalizationStrings(MainMenu.iShowContext, MainMenu.iLanguageSelected) Then
    '       btnShowHideContext.Text = MainMenu.sLocalizationStrings(MainMenu.iHideContext, MainMenu.iLanguageSelected)
    '      Me.rtbContextAbove.Show()
    '     Me.rtbTextSmall.Height() = 600
    ''    Me.rtbText.Location() = System.Drawing.Point(0, 311)

    '   Else
    '      Me.rtbContextAbove.Hide()
    '     Me.rtbTextSmall.Height() = 300
    '    btnShowHideContext.Text = MainMenu.sLocalizationStrings(MainMenu.iShowContext, MainMenu.iLanguageSelected)
    '     End If
    ' End Sub

    Private Sub chkbxShowSpeakerText_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxShowSpeakerText.CheckedChanged
        Try
            ' all options set in displayPropertiesOfClip
            dramatizer.displayPropertiesOfClip(2)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkbxShowVerses_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxShowVerses.CheckedChanged
        Try
            dramatizer.displayPropertiesOfClip(2)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkbxShowSFMcodes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxShowSFMcodes.CheckedChanged
        Try
            dramatizer.displayPropertiesOfClip(2)

        Catch ex As Exception

        End Try

    End Sub

    Private Sub chkbxShowContext_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxShowContext.CheckedChanged
        Try
            dramatizer.displayPropertiesOfClip(2)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
 
    End Sub
 
    Private Sub MasterText_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.load
        localizeMasterText()
        Main.processOmittedTextBasedOnCheckedInfo()
    End Sub
    Private Sub localizeMasterText()
        Me.chkbxShowContext.Text = MainMenu.sLocalizationStrings(MainMenu.iShowContext, MainMenu.iLanguageSelected)
        Me.chkbxShowSFMcodes.Text = MainMenu.sLocalizationStrings(MainMenu.iShowSFMcodes, MainMenu.iLanguageSelected)
        Me.chkbxShowSpeakerText.Text = MainMenu.sLocalizationStrings(MainMenu.iShowSpeakerText, MainMenu.iLanguageSelected)
        Me.chkbxShowVerses.Text = MainMenu.sLocalizationStrings(MainMenu.iShowVerses, MainMenu.iLanguageSelected)
        Me.lblOmitTextFoundIn.Text = MainMenu.sLocalizationStrings(MainMenu.iDoNotSpeakThis, MainMenu.iLanguageSelected)
        Me.chkbxSectionHeads.Text = MainMenu.sLocalizationStrings(MainMenu.iSectionHeads, MainMenu.iLanguageSelected)
        Me.chkbxIntroduction.Text = MainMenu.sLocalizationStrings(MainMenu.iIntroductions, MainMenu.iLanguageSelected)
        Me.chkbxHeading.Text = MainMenu.sLocalizationStrings(MainMenu.iHeadings, MainMenu.iLanguageSelected)
        Me.chkbxFootnotes.Text = MainMenu.sLocalizationStrings(MainMenu.iFootnotes, MainMenu.iLanguageSelected)
        Me.chkbxChapterNumbers.Text = MainMenu.sLocalizationStrings(MainMenu.iChapterNumbers, MainMenu.iLanguageSelected)
        Me.chkbxReferences.Text = MainMenu.sLocalizationStrings(MainMenu.iReferences, MainMenu.iLanguageSelected)
        Me.chkbxThisOne.Text = MainMenu.sLocalizationStrings(MainMenu.iThisOne, MainMenu.iLanguageSelected)
        Me.lblClipSize.Text = MainMenu.sLocalizationStrings(MainMenu.iClipSize, MainMenu.iLanguageSelected)
        Me.Text = MainMenu.sLocalizationStrings(MainMenu.iMasterText, MainMenu.iLanguageSelected)

    End Sub

    Private Sub chkbxSectionHeads_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxSectionHeads.CheckedChanged
        Try
            If Me.chkbxSectionHeads.Checked = True Then
                Main.removeSectionHeads()
            Else
                Main.showSectionHeads()
            End If
            Main.writeCurrentSettings()
            Main.identifyOmittedText()
            Main.writeClipsToMasterFileAndAdjustClipSize(False)
        Catch ex As Exception
            ' first time through this doesn't work
            '    MessageBox.Show("error " & ex.Message, "Error")
        End Try
    End Sub

    Private Sub chkbxChapterNumbers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxChapterNumbers.CheckedChanged
        Try
            If Me.chkbxChapterNumbers.Checked = True Then
                Main.removeChapterNumbers()
            Else
                Main.showChapterNumbers()
            End If
            Main.writeCurrentSettings()
            Main.identifyOmittedText()
            Main.writeClipsToMasterFileAndAdjustClipSize(False)
        Catch ex As Exception
            ' first time through this doesn't work
            '   MessageBox.Show("error " & ex.Message, "Error")
        End Try

    End Sub

    Private Sub chkbxIntroduction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxIntroduction.CheckedChanged
        Try
            If Me.chkbxIntroduction.Checked = True Then
                Main.removeIntroduction()
            Else
                Main.showIntroduction()
            End If
            Main.writeCurrentSettings()
            Main.identifyOmittedText()
            Main.writeClipsToMasterFileAndAdjustClipSize(False)
        Catch ex As Exception
            ' first time through this doesn't work
            '  MessageBox.Show("error " & ex.Message, "Error")
        End Try

    End Sub

    Private Sub chkbxHeading_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxHeading.CheckedChanged
        Try
            If Me.chkbxHeading.Checked = True Then
                Main.removeHeading()
            Else
                Main.showHeading()
            End If
            Main.writeCurrentSettings()
            Main.identifyOmittedText()
            Main.writeClipsToMasterFileAndAdjustClipSize(False)
        Catch ex As Exception
            ' first time through this doesn't work
            ' MessageBox.Show("error " & ex.Message, "Error")
        End Try

    End Sub

    Private Sub chkbxThisOne_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxThisOne.CheckedChanged
        Try

        If Me.chkbxThisOne.Checked Then
                Main.blnOmit(Main.iCurrentClipNumber) = True
            Else
                Main.blnOmit(Main.iCurrentClipNumber) = False
            End If
            Main.identifyOmittedText()
            Main.writeClipsToMasterFileAndAdjustClipSize(False)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub chkbxFootnotes_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxFootnotes.CheckedChanged
        chkbxFootnotes.Checked = True
    End Sub
    Private Sub chkbxReferences_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxReferences.CheckedChanged
        chkbxReferences.Checked = True
    End Sub

    Private Sub lblOmitTextFoundIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblOmitTextFoundIn.Click

    End Sub
End Class