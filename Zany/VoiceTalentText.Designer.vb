<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VoiceTalentText
    Inherits System.Windows.Forms.Form
    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub
    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.rtbText = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'rtbText
        '
        Me.rtbText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtbText.Location = New System.Drawing.Point(0, 0)
        Me.rtbText.MaximumSize = New System.Drawing.Size(500, 800)
        Me.rtbText.MinimumSize = New System.Drawing.Size(100, 100)
        Me.rtbText.Name = "rtbText"
        Me.rtbText.Size = New System.Drawing.Size(500, 311)
        Me.rtbText.TabIndex = 1
        Me.rtbText.Text = ""
        '
        'VoiceTalentText
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(545, 311)
        Me.Controls.Add(Me.rtbText)
        Me.Name = "VoiceTalentText"
        Me.Text = "Text xxx"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents rtbText As System.Windows.Forms.RichTextBox
End Class
