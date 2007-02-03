Public Class translate

    Public row As Int16 = 1

    Public column As Int16 = 1


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Hide()
     
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
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