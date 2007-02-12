Public Class MasterText
    Public blnTopRight As Boolean = True
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
    Private Sub MasterText_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        localizeMasterText()
        Main.processOmittedTextBasedOnCheckedInfo()
        If Main.blnOmit(1) = True Then
            Dim x = 1
        End If

    End Sub
    Private Sub localizeMasterText()
        Me.chkbxShowContext.Text = Main.sLocalizationStrings(Main.iShowContext, Main.iLanguageSelected)
        Me.chkbxShowSFMcodes.Text = Main.sLocalizationStrings(Main.iShowSFMcodes, Main.iLanguageSelected)
        Me.chkbxShowSpeakerText.Text = Main.sLocalizationStrings(Main.iShowSpeakerText, Main.iLanguageSelected)
        Me.chkbxShowVerses.Text = Main.sLocalizationStrings(Main.iShowVerses, Main.iLanguageSelected)
        Me.lblOmitTextFoundIn.Text = Main.sLocalizationStrings(Main.iDoNotSpeakThis, Main.iLanguageSelected)
        Me.chkbxSectionHeads.Text = Main.sLocalizationStrings(Main.iSectionHeads, Main.iLanguageSelected)
        Me.chkbxIntroduction.Text = Main.sLocalizationStrings(Main.iIntroductions, Main.iLanguageSelected)
        Me.chkbxHeading.Text = Main.sLocalizationStrings(Main.iHeadings, Main.iLanguageSelected)
        Me.chkbxFootnotes.Text = Main.sLocalizationStrings(Main.iFootnotes, Main.iLanguageSelected)
        Me.chkbxChapterNumbers.Text = Main.sLocalizationStrings(Main.iChapterNumbers, Main.iLanguageSelected)
        Me.chkbxReferences.Text = Main.sLocalizationStrings(Main.iReferences, Main.iLanguageSelected)
        Me.chkbxThisOne.Text = Main.sLocalizationStrings(Main.iThisOne, Main.iLanguageSelected)
        Me.lblClipSize.Text = Main.sLocalizationStrings(Main.iClipSize, Main.iLanguageSelected)
        Me.Text = Main.sLocalizationStrings(Main.iMasterText, Main.iLanguageSelected)
        Me.btnMoveThis.Text = Main.sLocalizationStrings(Main.iMoveToTopRight, Main.iLanguageSelected)
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
    Private Sub textBox_Shift(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
       Handles rtbTextOnly.KeyPress
        ' Check for the flag being set in the KeyDown event.
        If ((Control.ModifierKeys And Keys.Shift) = Keys.Shift) Then
            btnMagnify.Text = "-"
        Else
            btnMagnify.Text = "+"
        End If
    End Sub 'textBox1_KeyPress
    Private Sub btnMagnify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMagnify.Click
        Dim increase As Boolean = True
        Main.changeFontSize(increase)
    End Sub
    Private Sub btnShrink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShrink.Click
        Dim increase As Boolean = False
        Main.changeFontSize(increase)
    End Sub
    Private Sub btnMoveThis_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveThis.Click
        If Me.blnTopRight = True Then
            ' move to top right
            topRight()
        Else
            ' move to lower left
            Me.btnMoveThis.Text = Main.sLocalizationStrings(Main.iMoveToTopRight, Main.iLanguageSelected)
            Me.Location = New Point(0, 300)
            Me.blnTopRight = True
        End If
    End Sub
    Public Sub topRight()
        Me.btnMoveThis.Text = Main.sLocalizationStrings(Main.iMoveToBottomLeft, Main.iLanguageSelected)
        Me.Location = New Point(512, 0)
        Me.blnTopRight = False
    End Sub
    Private Sub chkbxSectionHeads_CheckedChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbxSectionHeads.CheckedChanged
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
End Class