<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MasterText
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
        Me.rtbTextWithContext = New System.Windows.Forms.RichTextBox
        Me.rtbContextAbove = New System.Windows.Forms.RichTextBox
        Me.tbClipSize = New System.Windows.Forms.TextBox
        Me.tbContinued = New System.Windows.Forms.TextBox
        Me.chkbxShowSpeakerText = New System.Windows.Forms.CheckBox
        Me.chkbxShowVerses = New System.Windows.Forms.CheckBox
        Me.chkbxShowSFMcodes = New System.Windows.Forms.CheckBox
        Me.chkbxShowContext = New System.Windows.Forms.CheckBox
        Me.rtbTextOnly = New System.Windows.Forms.RichTextBox
        Me.chkbxChapterNumbers = New System.Windows.Forms.CheckBox
        Me.chkbxHeading = New System.Windows.Forms.CheckBox
        Me.chkbxSectionHeads = New System.Windows.Forms.CheckBox
        Me.chkbxIntroduction = New System.Windows.Forms.CheckBox
        Me.lblOmitTextFoundIn = New System.Windows.Forms.Label
        Me.chkbxReferences = New System.Windows.Forms.CheckBox
        Me.chkbxFootnotes = New System.Windows.Forms.CheckBox
        Me.chkbxThisOne = New System.Windows.Forms.CheckBox
        Me.lblClipSize = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'rtbTextWithContext
        '
        Me.rtbTextWithContext.Location = New System.Drawing.Point(0, 285)
        Me.rtbTextWithContext.MaximumSize = New System.Drawing.Size(600, 500)
        Me.rtbTextWithContext.MinimumSize = New System.Drawing.Size(100, 100)
        Me.rtbTextWithContext.Name = "rtbTextWithContext"
        Me.rtbTextWithContext.Size = New System.Drawing.Size(500, 338)
        Me.rtbTextWithContext.TabIndex = 2
        Me.rtbTextWithContext.Text = ""
        '
        'rtbContextAbove
        '
        Me.rtbContextAbove.Location = New System.Drawing.Point(0, 0)
        Me.rtbContextAbove.MaximumSize = New System.Drawing.Size(600, 500)
        Me.rtbContextAbove.MinimumSize = New System.Drawing.Size(100, 100)
        Me.rtbContextAbove.Name = "rtbContextAbove"
        Me.rtbContextAbove.Size = New System.Drawing.Size(500, 259)
        Me.rtbContextAbove.TabIndex = 3
        Me.rtbContextAbove.Text = ""
        '
        'tbClipSize
        '
        Me.tbClipSize.Location = New System.Drawing.Point(507, 125)
        Me.tbClipSize.Name = "tbClipSize"
        Me.tbClipSize.ReadOnly = True
        Me.tbClipSize.Size = New System.Drawing.Size(50, 20)
        Me.tbClipSize.TabIndex = 5
        '
        'tbContinued
        '
        Me.tbContinued.Location = New System.Drawing.Point(506, 151)
        Me.tbContinued.Name = "tbContinued"
        Me.tbContinued.ReadOnly = True
        Me.tbContinued.Size = New System.Drawing.Size(182, 20)
        Me.tbContinued.TabIndex = 6
        '
        'chkbxShowSpeakerText
        '
        Me.chkbxShowSpeakerText.Location = New System.Drawing.Point(18, 1)
        Me.chkbxShowSpeakerText.MaximumSize = New System.Drawing.Size(134, 24)
        Me.chkbxShowSpeakerText.Name = "chkbxShowSpeakerText"
        Me.chkbxShowSpeakerText.Size = New System.Drawing.Size(134, 24)
        Me.chkbxShowSpeakerText.TabIndex = 7
        Me.chkbxShowSpeakerText.Text = "show speaker text xxx"
        Me.chkbxShowSpeakerText.UseVisualStyleBackColor = True
        '
        'chkbxShowVerses
        '
        Me.chkbxShowVerses.Location = New System.Drawing.Point(18, 28)
        Me.chkbxShowVerses.Name = "chkbxShowVerses"
        Me.chkbxShowVerses.Size = New System.Drawing.Size(134, 17)
        Me.chkbxShowVerses.TabIndex = 8
        Me.chkbxShowVerses.Text = "show verses xxx"
        Me.chkbxShowVerses.UseVisualStyleBackColor = True
        '
        'chkbxShowSFMcodes
        '
        Me.chkbxShowSFMcodes.Location = New System.Drawing.Point(18, 48)
        Me.chkbxShowSFMcodes.Name = "chkbxShowSFMcodes"
        Me.chkbxShowSFMcodes.Size = New System.Drawing.Size(134, 17)
        Me.chkbxShowSFMcodes.TabIndex = 9
        Me.chkbxShowSFMcodes.Text = "show sfm codes xxx"
        Me.chkbxShowSFMcodes.UseVisualStyleBackColor = True
        '
        'chkbxShowContext
        '
        Me.chkbxShowContext.Location = New System.Drawing.Point(18, 68)
        Me.chkbxShowContext.Name = "chkbxShowContext"
        Me.chkbxShowContext.Size = New System.Drawing.Size(134, 17)
        Me.chkbxShowContext.TabIndex = 10
        Me.chkbxShowContext.Text = "show context xxx"
        Me.chkbxShowContext.UseVisualStyleBackColor = True
        '
        'rtbTextOnly
        '
        Me.rtbTextOnly.Location = New System.Drawing.Point(0, 24)
        Me.rtbTextOnly.MaximumSize = New System.Drawing.Size(700, 611)
        Me.rtbTextOnly.MinimumSize = New System.Drawing.Size(100, 100)
        Me.rtbTextOnly.Name = "rtbTextOnly"
        Me.rtbTextOnly.Size = New System.Drawing.Size(500, 338)
        Me.rtbTextOnly.TabIndex = 11
        Me.rtbTextOnly.Text = ""
        '
        'chkbxChapterNumbers
        '
        Me.chkbxChapterNumbers.AutoSize = True
        Me.chkbxChapterNumbers.Location = New System.Drawing.Point(19, 43)
        Me.chkbxChapterNumbers.Name = "chkbxChapterNumbers"
        Me.chkbxChapterNumbers.Size = New System.Drawing.Size(124, 17)
        Me.chkbxChapterNumbers.TabIndex = 55
        Me.chkbxChapterNumbers.Text = "Chapter numbers xxx"
        Me.chkbxChapterNumbers.UseVisualStyleBackColor = True
        '
        'chkbxHeading
        '
        Me.chkbxHeading.AutoSize = True
        Me.chkbxHeading.Checked = True
        Me.chkbxHeading.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxHeading.Location = New System.Drawing.Point(19, 88)
        Me.chkbxHeading.Name = "chkbxHeading"
        Me.chkbxHeading.Size = New System.Drawing.Size(84, 17)
        Me.chkbxHeading.TabIndex = 54
        Me.chkbxHeading.Text = "Heading xxx"
        Me.chkbxHeading.UseVisualStyleBackColor = True
        '
        'chkbxSectionHeads
        '
        Me.chkbxSectionHeads.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbxSectionHeads.AutoSize = True
        Me.chkbxSectionHeads.Checked = True
        Me.chkbxSectionHeads.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxSectionHeads.Location = New System.Drawing.Point(19, 20)
        Me.chkbxSectionHeads.Name = "chkbxSectionHeads"
        Me.chkbxSectionHeads.Size = New System.Drawing.Size(112, 17)
        Me.chkbxSectionHeads.TabIndex = 53
        Me.chkbxSectionHeads.Text = "Section heads xxx"
        Me.chkbxSectionHeads.UseVisualStyleBackColor = True
        '
        'chkbxIntroduction
        '
        Me.chkbxIntroduction.AutoSize = True
        Me.chkbxIntroduction.Checked = True
        Me.chkbxIntroduction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxIntroduction.Location = New System.Drawing.Point(19, 65)
        Me.chkbxIntroduction.Name = "chkbxIntroduction"
        Me.chkbxIntroduction.Size = New System.Drawing.Size(100, 17)
        Me.chkbxIntroduction.TabIndex = 52
        Me.chkbxIntroduction.Text = "Introduction xxx"
        Me.chkbxIntroduction.UseVisualStyleBackColor = True
        '
        'lblOmitTextFoundIn
        '
        Me.lblOmitTextFoundIn.AutoSize = True
        Me.lblOmitTextFoundIn.Location = New System.Drawing.Point(3, 4)
        Me.lblOmitTextFoundIn.Name = "lblOmitTextFoundIn"
        Me.lblOmitTextFoundIn.Size = New System.Drawing.Size(107, 13)
        Me.lblOmitTextFoundIn.TabIndex = 51
        Me.lblOmitTextFoundIn.Text = "Omit text found in xxx"
        '
        'chkbxReferences
        '
        Me.chkbxReferences.AutoSize = True
        Me.chkbxReferences.Checked = True
        Me.chkbxReferences.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxReferences.Location = New System.Drawing.Point(18, 134)
        Me.chkbxReferences.Name = "chkbxReferences"
        Me.chkbxReferences.Size = New System.Drawing.Size(102, 17)
        Me.chkbxReferences.TabIndex = 57
        Me.chkbxReferences.Text = "References  xxx"
        Me.chkbxReferences.UseVisualStyleBackColor = True
        '
        'chkbxFootnotes
        '
        Me.chkbxFootnotes.AutoSize = True
        Me.chkbxFootnotes.Checked = True
        Me.chkbxFootnotes.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxFootnotes.Location = New System.Drawing.Point(18, 111)
        Me.chkbxFootnotes.Name = "chkbxFootnotes"
        Me.chkbxFootnotes.Size = New System.Drawing.Size(91, 17)
        Me.chkbxFootnotes.TabIndex = 56
        Me.chkbxFootnotes.Text = "Footnotes xxx"
        Me.chkbxFootnotes.UseVisualStyleBackColor = True
        '
        'chkbxThisOne
        '
        Me.chkbxThisOne.AutoSize = True
        Me.chkbxThisOne.Location = New System.Drawing.Point(18, 157)
        Me.chkbxThisOne.Name = "chkbxThisOne"
        Me.chkbxThisOne.Size = New System.Drawing.Size(81, 17)
        Me.chkbxThisOne.TabIndex = 58
        Me.chkbxThisOne.Text = "this one xxx"
        Me.chkbxThisOne.UseVisualStyleBackColor = True
        '
        'lblClipSize
        '
        Me.lblClipSize.AutoSize = True
        Me.lblClipSize.Location = New System.Drawing.Point(504, 109)
        Me.lblClipSize.Name = "lblClipSize"
        Me.lblClipSize.Size = New System.Drawing.Size(62, 13)
        Me.lblClipSize.TabIndex = 59
        Me.lblClipSize.Text = "clip size xxx"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblOmitTextFoundIn)
        Me.Panel1.Controls.Add(Me.chkbxIntroduction)
        Me.Panel1.Controls.Add(Me.chkbxThisOne)
        Me.Panel1.Controls.Add(Me.chkbxSectionHeads)
        Me.Panel1.Controls.Add(Me.chkbxReferences)
        Me.Panel1.Controls.Add(Me.chkbxHeading)
        Me.Panel1.Controls.Add(Me.chkbxFootnotes)
        Me.Panel1.Controls.Add(Me.chkbxChapterNumbers)
        Me.Panel1.Location = New System.Drawing.Point(507, 174)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(181, 188)
        Me.Panel1.TabIndex = 60
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.chkbxShowContext)
        Me.Panel2.Controls.Add(Me.chkbxShowSpeakerText)
        Me.Panel2.Controls.Add(Me.chkbxShowVerses)
        Me.Panel2.Controls.Add(Me.chkbxShowSFMcodes)
        Me.Panel2.Location = New System.Drawing.Point(506, 11)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(182, 94)
        Me.Panel2.TabIndex = 61
        '
        'MasterText
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(692, 622)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblClipSize)
        Me.Controls.Add(Me.rtbTextOnly)
        Me.Controls.Add(Me.tbContinued)
        Me.Controls.Add(Me.tbClipSize)
        Me.Controls.Add(Me.rtbContextAbove)
        Me.Controls.Add(Me.rtbTextWithContext)
        Me.Location = New System.Drawing.Point(0, 300)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(700, 700)
        Me.Name = "MasterText"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "MasterText xxx"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents rtbTextWithContext As System.Windows.Forms.RichTextBox
    Friend WithEvents rtbContextAbove As System.Windows.Forms.RichTextBox
    Friend WithEvents tbClipSize As System.Windows.Forms.TextBox
    Friend WithEvents tbContinued As System.Windows.Forms.TextBox
    Friend WithEvents chkbxShowSpeakerText As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxShowVerses As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxShowSFMcodes As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxShowContext As System.Windows.Forms.CheckBox
    Friend WithEvents rtbTextOnly As System.Windows.Forms.RichTextBox
    Friend WithEvents chkbxChapterNumbers As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxHeading As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxSectionHeads As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxIntroduction As System.Windows.Forms.CheckBox
    Friend WithEvents lblOmitTextFoundIn As System.Windows.Forms.Label
    Friend WithEvents chkbxReferences As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxFootnotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxThisOne As System.Windows.Forms.CheckBox
    Friend WithEvents lblClipSize As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class
