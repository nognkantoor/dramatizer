Public Class translate
    Public row As Int16 = 1
    Public column As Int16 = 1
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        MainMenu.cbLanguage.SelectedIndex = MainMenu.iSavedLanguageIndex
        Me.Hide()
    End Sub
    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Main.sLocalizationStrings(row, Main.iLanguageSelected) = tbTarget.Text
        Main.writeLocalizationFile()
        Main.readLocalizationFile()
        MainMenu.fillLanguageControl()
        Me.Hide()
        Me.Close()
        MainMenu.cbLanguage.SelectedIndex = MainMenu.iSavedLanguageIndex
    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Me.btnForward.BackColor = Color.LightGray
        Me.btnOK.BackColor = Color.LawnGreen
    End Sub
    Public Sub loadTranslate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        fillControls()
        Dim iAddLanguage = MainMenu.cbLanguage.Items.Count - 1
        ' This example assumes that the Form_Load event handling method
        ' is connected to the Load event of the form.
        ' Create the ToolTip and associate with the Form container.
        Dim toolTip1 As New ToolTip()
        ' Set up the delays for the ToolTip.
        toolTip1.AutoPopDelay = 5000
        toolTip1.InitialDelay = 1000
        toolTip1.ReshowDelay = 500
        ' Force the ToolTip text to be displayed whether or not the form is active.
        toolTip1.ShowAlways = True
        ' Set up the ToolTip text for the Buttons and Textbox.
        If MainMenu.cbLanguage.SelectedIndex = iAddLanguage Then
            ' use English
            toolTip1.SetToolTip(Me.btnBack, Main.sLocalizationStrings(Main.iBackAndCancelChange, 1))
            toolTip1.SetToolTip(Me.btnForward, Main.sLocalizationStrings(Main.iSaveChangeAndSeeNext, 1))
            toolTip1.SetToolTip(Me.btnStart, Main.sLocalizationStrings(Main.iStartText, 1))
            toolTip1.SetToolTip(Me.tbSource, Main.sLocalizationStrings(Main.iTypeInOtherBox, 1))
            toolTip1.SetToolTip(Me.btnOK, Main.sLocalizationStrings(Main.iSaveChangeAndCloseMenu, 1))
            toolTip1.SetToolTip(Me.btnCancel, Main.sLocalizationStrings(Main.iCancelChangeAndCloseMenu, 1))
        Else
            toolTip1.SetToolTip(Me.btnBack, Main.sLocalizationStrings(Main.iBackAndCancelChange, Main.iLanguageSelected))
            toolTip1.SetToolTip(Me.btnForward, Main.sLocalizationStrings(Main.iSaveChangeAndSeeNext, Main.iLanguageSelected))
            toolTip1.SetToolTip(Me.btnStart, Main.sLocalizationStrings(Main.iStartText, Main.iLanguageSelected))
            toolTip1.SetToolTip(Me.tbSource, Main.sLocalizationStrings(Main.iTypeInOtherBox, Main.iLanguageSelected))
            toolTip1.SetToolTip(Me.btnOK, Main.sLocalizationStrings(Main.iSaveChangeAndCloseMenu, Main.iLanguageSelected))
            toolTip1.SetToolTip(Me.btnCancel, Main.sLocalizationStrings(Main.iCancelChangeAndCloseMenu, Main.iLanguageSelected))
        End If

    End Sub
    Public Sub fillControls()
        Try
            '   Public sLocalizationStrings(, iMaximumLocalizationLanguages) As String
            Debug.Assert(column < Main.iMaximumLocalizationLanguages + 2, "column greater than maximum allowed languages in fill translate controls")
            Debug.Assert(column > 0, "column equals 0 in fill translate controls")
            Me.tbSource.Text = Main.sLocalizationStrings(row, 1)
            Me.tbTarget.Text = Main.sLocalizationStrings(row, Main.iLanguageSelected)
            Me.lblLocation.Text = currentLocation()
        Catch ex As Exception
            MessageBox.Show("Problem filling current location in translate control." & vbCrLf & ex.Message, "Error in translate.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Private Sub btnForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForward.Click
        Main.sLocalizationStrings(row, Main.iLanguageSelected) = tbTarget.Text
        '  row += 1
        If row = Main.iMaximumLocalizationStrings Then Beep() : row = Main.iMaximumLocalizationStrings
        fillControls()
        Me.btnForward.BackColor = Color.LightGray
        Main.writeLocalizationFile()
    End Sub
    Private Sub btnForward_RightClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForward.Click
        Main.sLocalizationStrings(row, Main.iLanguageSelected) = tbTarget.Text
        row += 1
        If row = Main.iMaximumLocalizationStrings Then Beep() : row = Main.iMaximumLocalizationStrings
        fillControls()
        Me.btnForward.BackColor = Color.LightGray
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        row -= 1
        If row = 0 Then Beep() : row = 1
        fillControls()
        Me.btnForward.BackColor = Color.LightGray
    End Sub
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        row = 1
        fillControls()
    End Sub
    Private Function currentLocation()
        Return row.ToString & ", " & Main.iLanguageSelected.ToString
    End Function
    Private Sub tbTarget_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbTarget.TextChanged
        Me.btnForward.BackColor = Color.LimeGreen
    End Sub
    Private Sub tbSource_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSource.TextChanged
        fillControls()
    End Sub
    Private Sub btnFastForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFastForward.Click
        Main.sLocalizationStrings(row, Main.iLanguageSelected) = tbTarget.Text
        row += 10
        If row = Main.iMaximumLocalizationStrings Then Beep() : row = Main.iMaximumLocalizationStrings
        fillControls()
        Me.btnForward.BackColor = Color.LightGray
    End Sub
End Class