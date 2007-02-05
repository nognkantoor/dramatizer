Public Class translate

    Public row As Int16 = 1

    Public column As Int16 = 1


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
     
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        MainMenu.sLocalizationStrings(row, MainMenu.iLanguageSelected) = tbTarget.Text
        MainMenu.writeLocalizationFile()
        Me.Hide()

    End Sub
    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.btnForward.BackColor = Color.LightSlateGray

    End Sub
    Public Sub loadTranslate(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        fillControls()
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
        toolTip1.SetToolTip(Me.btnBack, MainMenu.sLocalizationStrings(MainMenu.iBackAndCancelChange, MainMenu.iLanguageSelected))
        toolTip1.SetToolTip(Me.btnForward, MainMenu.sLocalizationStrings(MainMenu.iSaveChangeAndSeeNext, MainMenu.iLanguageSelected))
        toolTip1.SetToolTip(Me.btnStart, MainMenu.sLocalizationStrings(MainMenu.iStart, MainMenu.iLanguageSelected))
        toolTip1.SetToolTip(Me.tbSource, MainMenu.sLocalizationStrings(MainMenu.iTypeInOtherBox, MainMenu.iLanguageSelected))
        toolTip1.SetToolTip(Me.btnOK, MainMenu.sLocalizationStrings(MainMenu.iSaveChangeAndCloseMenu, MainMenu.iLanguageSelected))
        toolTip1.SetToolTip(Me.btnCancel, MainMenu.sLocalizationStrings(MainMenu.iCancelChangeAndCloseMenu, MainMenu.iLanguageSelected))


    End Sub
    Public Sub fillControls()
        Try
            '   Public sLocalizationStrings(, iMaximumLocalizationLanguages) As String
            Debug.Assert(column < MainMenu.iMaximumLocalizationLanguages + 2, "column greater than maximum allowed languages in fill translate controls")
            Debug.Assert(column > 0, "column equals 0 in fill translate controls")
            Me.tbSource.Text = MainMenu.sLocalizationStrings(row, 1)
            Me.tbTarget.Text = MainMenu.sLocalizationStrings(row, MainMenu.iLanguageSelected)
            Me.lblLocation.Text = currentLocation()
        Catch ex As Exception
            MessageBox.Show("Problem filling current location in translate control." & vbCrLf & ex.Message, "Error in translate.", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub
    Private Sub btnForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForward.Click
        MainMenu.sLocalizationStrings(row, MainMenu.iLanguageSelected) = tbTarget.Text
        row += 1
        If row = MainMenu.iMaximumLocalizationStrings Then Beep() : row = MainMenu.iMaximumLocalizationStrings
        fillControls()
        Me.btnForward.BackColor = Color.LightSlateGray
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        row -= 1
        If row = 0 Then Beep() : row = 1
        fillControls()
        Me.btnForward.BackColor = Color.SlateGray
    End Sub
    Private Sub btnStart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStart.Click
        row = 1
        fillControls()
    End Sub
    Private Function currentLocation()
        Return row.ToString & ", " & MainMenu.iLanguageSelected.ToString
    End Function

    Private Sub tbTarget_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbTarget.TextChanged
        Me.btnForward.BackColor = Color.LimeGreen
    End Sub

    Private Sub tbSource_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbSource.TextChanged
        fillControls()

    End Sub
End Class