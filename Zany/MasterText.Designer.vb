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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MasterText))
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
        Me.chkbxIntroduction = New System.Windows.Forms.CheckBox
        Me.lblOmitTextFoundIn = New System.Windows.Forms.Label
        Me.chkbxReferences = New System.Windows.Forms.CheckBox
        Me.chkbxFootnotes = New System.Windows.Forms.CheckBox
        Me.chkbxThisOne = New System.Windows.Forms.CheckBox
        Me.lblClipSize = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.chkbxSectionHeads = New System.Windows.Forms.CheckBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.btnMagnify = New System.Windows.Forms.Button
        Me.btnShrink = New System.Windows.Forms.Button
        Me.tbFontSize = New System.Windows.Forms.TextBox
        Me.btnMoveThis = New System.Windows.Forms.Button
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
        Me.rtbContextAbove.Location = New System.Drawing.Point(0, 24)
        Me.rtbContextAbove.MaximumSize = New System.Drawing.Size(600, 500)
        Me.rtbContextAbove.MinimumSize = New System.Drawing.Size(100, 100)
        Me.rtbContextAbove.Name = "rtbContextAbove"
        Me.rtbContextAbove.Size = New System.Drawing.Size(500, 235)
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
        Me.tbContinued.Size = New System.Drawing.Size(230, 20)
        Me.tbContinued.TabIndex = 6
        '
        'chkbxShowSpeakerText
        '
        Me.chkbxShowSpeakerText.Location = New System.Drawing.Point(18, 1)
        Me.chkbxShowSpeakerText.Name = "chkbxShowSpeakerText"
        Me.chkbxShowSpeakerText.Size = New System.Drawing.Size(204, 24)
        Me.chkbxShowSpeakerText.TabIndex = 7
        Me.chkbxShowSpeakerText.Text = "show speaker text xxx"
        Me.chkbxShowSpeakerText.UseVisualStyleBackColor = True
        '
        'chkbxShowVerses
        '
        Me.chkbxShowVerses.Location = New System.Drawing.Point(18, 28)
        Me.chkbxShowVerses.Name = "chkbxShowVerses"
        Me.chkbxShowVerses.Size = New System.Drawing.Size(204, 17)
        Me.chkbxShowVerses.TabIndex = 8
        Me.chkbxShowVerses.Text = "show verses xxx"
        Me.chkbxShowVerses.UseVisualStyleBackColor = True
        '
        'chkbxShowSFMcodes
        '
        Me.chkbxShowSFMcodes.Location = New System.Drawing.Point(18, 48)
        Me.chkbxShowSFMcodes.Name = "chkbxShowSFMcodes"
        Me.chkbxShowSFMcodes.Size = New System.Drawing.Size(204, 17)
        Me.chkbxShowSFMcodes.TabIndex = 9
        Me.chkbxShowSFMcodes.Text = "show sfm codes xxx"
        Me.chkbxShowSFMcodes.UseVisualStyleBackColor = True
        '
        'chkbxShowContext
        '
        Me.chkbxShowContext.Location = New System.Drawing.Point(18, 68)
        Me.chkbxShowContext.Name = "chkbxShowContext"
        Me.chkbxShowContext.Size = New System.Drawing.Size(204, 17)
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
        Me.chkbxChapterNumbers.Location = New System.Drawing.Point(19, 89)
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
        Me.chkbxHeading.Location = New System.Drawing.Point(19, 26)
        Me.chkbxHeading.Name = "chkbxHeading"
        Me.chkbxHeading.Size = New System.Drawing.Size(84, 17)
        Me.chkbxHeading.TabIndex = 54
        Me.chkbxHeading.Text = "Heading xxx"
        Me.chkbxHeading.UseVisualStyleBackColor = True
        '
        'chkbxIntroduction
        '
        Me.chkbxIntroduction.AutoSize = True
        Me.chkbxIntroduction.Checked = True
        Me.chkbxIntroduction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxIntroduction.Location = New System.Drawing.Point(19, 47)
        Me.chkbxIntroduction.Name = "chkbxIntroduction"
        Me.chkbxIntroduction.Size = New System.Drawing.Size(100, 17)
        Me.chkbxIntroduction.TabIndex = 52
        Me.chkbxIntroduction.Text = "Introduction xxx"
        Me.chkbxIntroduction.UseVisualStyleBackColor = True
        '
        'lblOmitTextFoundIn
        '
        Me.lblOmitTextFoundIn.AutoSize = True
        Me.lblOmitTextFoundIn.Location = New System.Drawing.Point(2, 10)
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
        Me.chkbxReferences.Location = New System.Drawing.Point(19, 131)
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
        Me.chkbxFootnotes.Location = New System.Drawing.Point(19, 110)
        Me.chkbxFootnotes.Name = "chkbxFootnotes"
        Me.chkbxFootnotes.Size = New System.Drawing.Size(91, 17)
        Me.chkbxFootnotes.TabIndex = 56
        Me.chkbxFootnotes.Text = "Footnotes xxx"
        Me.chkbxFootnotes.UseVisualStyleBackColor = True
        '
        'chkbxThisOne
        '
        Me.chkbxThisOne.AutoSize = True
        Me.chkbxThisOne.Location = New System.Drawing.Point(19, 152)
        Me.chkbxThisOne.Name = "chkbxThisOne"
        Me.chkbxThisOne.Size = New System.Drawing.Size(81, 17)
        Me.chkbxThisOne.TabIndex = 58
        Me.chkbxThisOne.Text = "this one xxx"
        Me.chkbxThisOne.UseVisualStyleBackColor = True
        '
        'lblClipSize
        '
        Me.lblClipSize.AutoSize = True
        Me.lblClipSize.Location = New System.Drawing.Point(506, 109)
        Me.lblClipSize.Name = "lblClipSize"
        Me.lblClipSize.Size = New System.Drawing.Size(62, 13)
        Me.lblClipSize.TabIndex = 59
        Me.lblClipSize.Text = "clip size xxx"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.chkbxSectionHeads)
        Me.Panel1.Controls.Add(Me.lblOmitTextFoundIn)
        Me.Panel1.Controls.Add(Me.chkbxIntroduction)
        Me.Panel1.Controls.Add(Me.chkbxThisOne)
        Me.Panel1.Controls.Add(Me.chkbxReferences)
        Me.Panel1.Controls.Add(Me.chkbxChapterNumbers)
        Me.Panel1.Controls.Add(Me.chkbxHeading)
        Me.Panel1.Controls.Add(Me.chkbxFootnotes)
        Me.Panel1.Location = New System.Drawing.Point(507, 174)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(229, 188)
        Me.Panel1.TabIndex = 60
        '
        'chkbxSectionHeads
        '
        Me.chkbxSectionHeads.AutoSize = True
        Me.chkbxSectionHeads.Checked = True
        Me.chkbxSectionHeads.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxSectionHeads.Location = New System.Drawing.Point(19, 68)
        Me.chkbxSectionHeads.Name = "chkbxSectionHeads"
        Me.chkbxSectionHeads.Size = New System.Drawing.Size(112, 17)
        Me.chkbxSectionHeads.TabIndex = 59
        Me.chkbxSectionHeads.Text = "Section heads xxx"
        Me.chkbxSectionHeads.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel2.Controls.Add(Me.chkbxShowContext)
        Me.Panel2.Controls.Add(Me.chkbxShowSpeakerText)
        Me.Panel2.Controls.Add(Me.chkbxShowVerses)
        Me.Panel2.Controls.Add(Me.chkbxShowSFMcodes)
        Me.Panel2.Location = New System.Drawing.Point(507, 1)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(229, 94)
        Me.Panel2.TabIndex = 61
        '
        'btnMagnify
        '
        Me.btnMagnify.BackgroundImage = CType(resources.GetObject("btnMagnify.BackgroundImage"), System.Drawing.Image)
        Me.btnMagnify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnMagnify.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMagnify.Location = New System.Drawing.Point(742, 226)
        Me.btnMagnify.Name = "btnMagnify"
        Me.btnMagnify.Size = New System.Drawing.Size(50, 50)
        Me.btnMagnify.TabIndex = 62
        Me.btnMagnify.Text = "+"
        Me.btnMagnify.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnMagnify.UseVisualStyleBackColor = True
        '
        'btnShrink
        '
        Me.btnShrink.BackgroundImage = CType(resources.GetObject("btnShrink.BackgroundImage"), System.Drawing.Image)
        Me.btnShrink.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnShrink.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShrink.Location = New System.Drawing.Point(743, 312)
        Me.btnShrink.Name = "btnShrink"
        Me.btnShrink.Size = New System.Drawing.Size(50, 50)
        Me.btnShrink.TabIndex = 63
        Me.btnShrink.Text = "-"
        Me.btnShrink.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnShrink.UseVisualStyleBackColor = True
        '
        'tbFontSize
        '
        Me.tbFontSize.Location = New System.Drawing.Point(742, 284)
        Me.tbFontSize.Name = "tbFontSize"
        Me.tbFontSize.ReadOnly = True
        Me.tbFontSize.Size = New System.Drawing.Size(50, 20)
        Me.tbFontSize.TabIndex = 64
        '
        'btnMoveThis
        '
        Me.btnMoveThis.Location = New System.Drawing.Point(343, 1)
        Me.btnMoveThis.Name = "btnMoveThis"
        Me.btnMoveThis.Size = New System.Drawing.Size(157, 23)
        Me.btnMoveThis.TabIndex = 65
        Me.btnMoveThis.Text = "move this xxx"
        Me.btnMoveThis.UseVisualStyleBackColor = True
        '
        'MasterText
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(792, 622)
        Me.Controls.Add(Me.btnMoveThis)
        Me.Controls.Add(Me.tbFontSize)
        Me.Controls.Add(Me.btnShrink)
        Me.Controls.Add(Me.btnMagnify)
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
        Me.MaximumSize = New System.Drawing.Size(800, 700)
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
    Friend WithEvents chkbxIntroduction As System.Windows.Forms.CheckBox
    Friend WithEvents lblOmitTextFoundIn As System.Windows.Forms.Label
    Friend WithEvents chkbxReferences As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxFootnotes As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxThisOne As System.Windows.Forms.CheckBox
    Friend WithEvents lblClipSize As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnMagnify As System.Windows.Forms.Button
    Friend WithEvents btnShrink As System.Windows.Forms.Button
    Friend WithEvents tbFontSize As System.Windows.Forms.TextBox
    Friend WithEvents btnMoveThis As System.Windows.Forms.Button
    Friend WithEvents chkbxSectionHeads As System.Windows.Forms.CheckBox
End Class
