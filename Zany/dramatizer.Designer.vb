<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class dramatizer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(dramatizer))
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.lblCharacterPrompt = New System.Windows.Forms.Label
        Me.cbCharacterPrompt = New System.Windows.Forms.ComboBox
        Me.btnNext = New System.Windows.Forms.Button
        Me.tbSpeakerNumber = New System.Windows.Forms.TextBox
        Me.lblCharacterSpeakerNumber = New System.Windows.Forms.Label
        Me.cbCharacters = New System.Windows.Forms.ComboBox
        Me.lblClipNumber = New System.Windows.Forms.Label
        Me.tbCurrentClipNumber = New System.Windows.Forms.TextBox
        Me.lbForwardBackBy = New System.Windows.Forms.ListBox
        Me.tbBook = New System.Windows.Forms.TextBox
        Me.btnEnd = New System.Windows.Forms.Button
        Me.tbChapter = New System.Windows.Forms.TextBox
        Me.tbVerse = New System.Windows.Forms.TextBox
        Me.btnForward = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.btnStart = New System.Windows.Forms.Button
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.statusBar = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar
        Me.btnEdit = New System.Windows.Forms.Button
        Me.lblCharacterName = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnNotAQuote = New System.Windows.Forms.Button
        Me.cbCharactersEdit = New System.Windows.Forms.ComboBox
        Me.chkbxShowPrompt = New System.Windows.Forms.CheckBox
        Me.upDownSpeakerNumber = New System.Windows.Forms.NumericUpDown
        Me.chkbxRecordOneSpeakerAtATime = New System.Windows.Forms.CheckBox
        Me.btnRecord = New System.Windows.Forms.Button
        Me.chkbxDisplayUnprocessedOnly = New System.Windows.Forms.CheckBox
        Me.chkbxDisplayOmittedClips = New System.Windows.Forms.CheckBox
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.upDownSpeakerNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnUpdate
        '
        Me.btnUpdate.Image = CType(resources.GetObject("btnUpdate.Image"), System.Drawing.Image)
        Me.btnUpdate.Location = New System.Drawing.Point(111, 15)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(46, 29)
        Me.btnUpdate.TabIndex = 39
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'lblCharacterPrompt
        '
        Me.lblCharacterPrompt.AutoSize = True
        Me.lblCharacterPrompt.Location = New System.Drawing.Point(124, 91)
        Me.lblCharacterPrompt.Name = "lblCharacterPrompt"
        Me.lblCharacterPrompt.Size = New System.Drawing.Size(57, 13)
        Me.lblCharacterPrompt.TabIndex = 38
        Me.lblCharacterPrompt.Text = "prompt xxx"
        '
        'cbCharacterPrompt
        '
        Me.cbCharacterPrompt.FormattingEnabled = True
        Me.cbCharacterPrompt.Location = New System.Drawing.Point(127, 107)
        Me.cbCharacterPrompt.Name = "cbCharacterPrompt"
        Me.cbCharacterPrompt.Size = New System.Drawing.Size(265, 21)
        Me.cbCharacterPrompt.TabIndex = 37
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(456, 14)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 36
        Me.btnNext.Text = "Next xxx"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'tbSpeakerNumber
        '
        Me.tbSpeakerNumber.Location = New System.Drawing.Point(119, 21)
        Me.tbSpeakerNumber.Name = "tbSpeakerNumber"
        Me.tbSpeakerNumber.Size = New System.Drawing.Size(77, 20)
        Me.tbSpeakerNumber.TabIndex = 35
        '
        'lblCharacterSpeakerNumber
        '
        Me.lblCharacterSpeakerNumber.AutoSize = True
        Me.lblCharacterSpeakerNumber.Location = New System.Drawing.Point(2, 5)
        Me.lblCharacterSpeakerNumber.Name = "lblCharacterSpeakerNumber"
        Me.lblCharacterSpeakerNumber.Size = New System.Drawing.Size(101, 13)
        Me.lblCharacterSpeakerNumber.TabIndex = 34
        Me.lblCharacterSpeakerNumber.Text = "speaker number xxx"
        '
        'cbCharacters
        '
        Me.cbCharacters.Location = New System.Drawing.Point(6, 65)
        Me.cbCharacters.Name = "cbCharacters"
        Me.cbCharacters.Size = New System.Drawing.Size(386, 21)
        Me.cbCharacters.TabIndex = 24
        '
        'lblClipNumber
        '
        Me.lblClipNumber.AutoSize = True
        Me.lblClipNumber.Location = New System.Drawing.Point(160, 7)
        Me.lblClipNumber.Name = "lblClipNumber"
        Me.lblClipNumber.Size = New System.Drawing.Size(79, 13)
        Me.lblClipNumber.TabIndex = 28
        Me.lblClipNumber.Text = "clip number xxx"
        '
        'tbCurrentClipNumber
        '
        Me.tbCurrentClipNumber.Location = New System.Drawing.Point(163, 23)
        Me.tbCurrentClipNumber.Name = "tbCurrentClipNumber"
        Me.tbCurrentClipNumber.Size = New System.Drawing.Size(86, 20)
        Me.tbCurrentClipNumber.TabIndex = 27
        '
        'lbForwardBackBy
        '
        Me.lbForwardBackBy.FormattingEnabled = True
        Me.lbForwardBackBy.Location = New System.Drawing.Point(255, 14)
        Me.lbForwardBackBy.Name = "lbForwardBackBy"
        Me.lbForwardBackBy.Size = New System.Drawing.Size(195, 30)
        Me.lbForwardBackBy.TabIndex = 24
        '
        'tbBook
        '
        Me.tbBook.Location = New System.Drawing.Point(5, 49)
        Me.tbBook.Name = "tbBook"
        Me.tbBook.Size = New System.Drawing.Size(65, 20)
        Me.tbBook.TabIndex = 23
        '
        'btnEnd
        '
        Me.btnEnd.Location = New System.Drawing.Point(456, 46)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(75, 23)
        Me.btnEnd.TabIndex = 4
        Me.btnEnd.Text = "End xxx"
        Me.btnEnd.UseVisualStyleBackColor = True
        '
        'tbChapter
        '
        Me.tbChapter.Location = New System.Drawing.Point(76, 49)
        Me.tbChapter.Name = "tbChapter"
        Me.tbChapter.Size = New System.Drawing.Size(44, 20)
        Me.tbChapter.TabIndex = 22
        '
        'tbVerse
        '
        Me.tbVerse.Location = New System.Drawing.Point(126, 49)
        Me.tbVerse.Name = "tbVerse"
        Me.tbVerse.Size = New System.Drawing.Size(38, 20)
        Me.tbVerse.TabIndex = 21
        '
        'btnForward
        '
        Me.btnForward.Image = CType(resources.GetObject("btnForward.Image"), System.Drawing.Image)
        Me.btnForward.Location = New System.Drawing.Point(76, 14)
        Me.btnForward.Name = "btnForward"
        Me.btnForward.Size = New System.Drawing.Size(29, 29)
        Me.btnForward.TabIndex = 19
        Me.btnForward.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.Location = New System.Drawing.Point(40, 14)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(29, 29)
        Me.btnBack.TabIndex = 20
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Image = CType(resources.GetObject("btnStart.Image"), System.Drawing.Image)
        Me.btnStart.Location = New System.Drawing.Point(7, 14)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(29, 29)
        Me.btnStart.TabIndex = 18
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.statusBar, Me.ToolStripProgressBar1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 243)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(547, 22)
        Me.StatusStrip1.TabIndex = 14
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'statusBar
        '
        Me.statusBar.Name = "statusBar"
        Me.statusBar.Size = New System.Drawing.Size(78, 17)
        Me.statusBar.Text = "Status Bar xxx"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(100, 16)
        Me.ToolStripProgressBar1.Visible = False
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(409, 65)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(115, 23)
        Me.btnEdit.TabIndex = 40
        Me.btnEdit.Text = "Edit xxx"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'lblCharacterName
        '
        Me.lblCharacterName.AutoSize = True
        Me.lblCharacterName.Location = New System.Drawing.Point(3, 49)
        Me.lblCharacterName.Name = "lblCharacterName"
        Me.lblCharacterName.Size = New System.Drawing.Size(100, 13)
        Me.lblCharacterName.TabIndex = 41
        Me.lblCharacterName.Text = "Character name xxx"
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.chkbxDisplayOmittedClips)
        Me.Panel1.Controls.Add(Me.btnNotAQuote)
        Me.Panel1.Controls.Add(Me.chkbxDisplayUnprocessedOnly)
        Me.Panel1.Controls.Add(Me.cbCharactersEdit)
        Me.Panel1.Controls.Add(Me.chkbxShowPrompt)
        Me.Panel1.Controls.Add(Me.upDownSpeakerNumber)
        Me.Panel1.Controls.Add(Me.cbCharacters)
        Me.Panel1.Controls.Add(Me.chkbxRecordOneSpeakerAtATime)
        Me.Panel1.Controls.Add(Me.lblCharacterName)
        Me.Panel1.Controls.Add(Me.lblCharacterPrompt)
        Me.Panel1.Controls.Add(Me.btnEdit)
        Me.Panel1.Controls.Add(Me.cbCharacterPrompt)
        Me.Panel1.Controls.Add(Me.lblCharacterSpeakerNumber)
        Me.Panel1.Controls.Add(Me.tbSpeakerNumber)
        Me.Panel1.Location = New System.Drawing.Point(5, 86)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(530, 143)
        Me.Panel1.TabIndex = 42
        '
        'btnNotAQuote
        '
        Me.btnNotAQuote.Location = New System.Drawing.Point(6, 105)
        Me.btnNotAQuote.Name = "btnNotAQuote"
        Me.btnNotAQuote.Size = New System.Drawing.Size(115, 23)
        Me.btnNotAQuote.TabIndex = 48
        Me.btnNotAQuote.Text = "Not a quote xxx"
        Me.btnNotAQuote.UseVisualStyleBackColor = True
        '
        'cbCharactersEdit
        '
        Me.cbCharactersEdit.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cbCharactersEdit.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbCharactersEdit.BackColor = System.Drawing.Color.Gold
        Me.cbCharactersEdit.Location = New System.Drawing.Point(6, 65)
        Me.cbCharactersEdit.Name = "cbCharactersEdit"
        Me.cbCharactersEdit.Size = New System.Drawing.Size(386, 21)
        Me.cbCharactersEdit.TabIndex = 47
        '
        'chkbxShowPrompt
        '
        Me.chkbxShowPrompt.AutoSize = True
        Me.chkbxShowPrompt.Checked = True
        Me.chkbxShowPrompt.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxShowPrompt.Location = New System.Drawing.Point(197, 90)
        Me.chkbxShowPrompt.Name = "chkbxShowPrompt"
        Me.chkbxShowPrompt.Size = New System.Drawing.Size(106, 17)
        Me.chkbxShowPrompt.TabIndex = 46
        Me.chkbxShowPrompt.Text = "Show prompt xxx"
        Me.chkbxShowPrompt.UseVisualStyleBackColor = True
        '
        'upDownSpeakerNumber
        '
        Me.upDownSpeakerNumber.Location = New System.Drawing.Point(6, 21)
        Me.upDownSpeakerNumber.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.upDownSpeakerNumber.Name = "upDownSpeakerNumber"
        Me.upDownSpeakerNumber.Size = New System.Drawing.Size(86, 20)
        Me.upDownSpeakerNumber.TabIndex = 45
        '
        'chkbxRecordOneSpeakerAtATime
        '
        Me.chkbxRecordOneSpeakerAtATime.AutoSize = True
        Me.chkbxRecordOneSpeakerAtATime.Checked = True
        Me.chkbxRecordOneSpeakerAtATime.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbxRecordOneSpeakerAtATime.Location = New System.Drawing.Point(119, 5)
        Me.chkbxRecordOneSpeakerAtATime.Name = "chkbxRecordOneSpeakerAtATime"
        Me.chkbxRecordOneSpeakerAtATime.Size = New System.Drawing.Size(184, 17)
        Me.chkbxRecordOneSpeakerAtATime.TabIndex = 44
        Me.chkbxRecordOneSpeakerAtATime.Text = "Record one speaker at a time xxx"
        Me.chkbxRecordOneSpeakerAtATime.UseVisualStyleBackColor = True
        '
        'btnRecord
        '
        Me.btnRecord.BackgroundImage = CType(resources.GetObject("btnRecord.BackgroundImage"), System.Drawing.Image)
        Me.btnRecord.Location = New System.Drawing.Point(111, 15)
        Me.btnRecord.Name = "btnRecord"
        Me.btnRecord.Size = New System.Drawing.Size(29, 29)
        Me.btnRecord.TabIndex = 43
        Me.btnRecord.UseVisualStyleBackColor = True
        '
        'chkbxDisplayUnprocessedOnly
        '
        Me.chkbxDisplayUnprocessedOnly.AutoSize = True
        Me.chkbxDisplayUnprocessedOnly.Location = New System.Drawing.Point(314, 5)
        Me.chkbxDisplayUnprocessedOnly.Name = "chkbxDisplayUnprocessedOnly"
        Me.chkbxDisplayUnprocessedOnly.Size = New System.Drawing.Size(167, 17)
        Me.chkbxDisplayUnprocessedOnly.TabIndex = 46
        Me.chkbxDisplayUnprocessedOnly.Text = "Display unprecessed only  xxx"
        Me.chkbxDisplayUnprocessedOnly.UseVisualStyleBackColor = True
        '
        'chkbxDisplayOmittedClips
        '
        Me.chkbxDisplayOmittedClips.AutoSize = True
        Me.chkbxDisplayOmittedClips.Location = New System.Drawing.Point(314, 28)
        Me.chkbxDisplayOmittedClips.Name = "chkbxDisplayOmittedClips"
        Me.chkbxDisplayOmittedClips.Size = New System.Drawing.Size(142, 17)
        Me.chkbxDisplayOmittedClips.TabIndex = 49
        Me.chkbxDisplayOmittedClips.Text = "Display omitted clips  xxx"
        Me.chkbxDisplayOmittedClips.UseVisualStyleBackColor = True
        '
        'dramatizer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(547, 265)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.lblClipNumber)
        Me.Controls.Add(Me.tbCurrentClipNumber)
        Me.Controls.Add(Me.lbForwardBackBy)
        Me.Controls.Add(Me.tbBook)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.tbChapter)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.tbVerse)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnForward)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnRecord)
        Me.Name = "dramatizer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "dramatizer xxx"
        Me.TopMost = True
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.upDownSpeakerNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbCharacters As System.Windows.Forms.ComboBox
    Friend WithEvents lblClipNumber As System.Windows.Forms.Label
    Friend WithEvents tbCurrentClipNumber As System.Windows.Forms.TextBox
    Friend WithEvents lbForwardBackBy As System.Windows.Forms.ListBox
    Friend WithEvents tbBook As System.Windows.Forms.TextBox
    Friend WithEvents btnEnd As System.Windows.Forms.Button
    Friend WithEvents tbChapter As System.Windows.Forms.TextBox
    Friend WithEvents tbVerse As System.Windows.Forms.TextBox
    Friend WithEvents btnForward As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents statusBar As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblCharacterSpeakerNumber As System.Windows.Forms.Label
    Friend WithEvents tbSpeakerNumber As System.Windows.Forms.TextBox
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents cbCharacterPrompt As System.Windows.Forms.ComboBox
    Friend WithEvents lblCharacterPrompt As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents lblCharacterName As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnRecord As System.Windows.Forms.Button
    Friend WithEvents chkbxRecordOneSpeakerAtATime As System.Windows.Forms.CheckBox
    Friend WithEvents upDownSpeakerNumber As System.Windows.Forms.NumericUpDown
    Friend WithEvents chkbxShowPrompt As System.Windows.Forms.CheckBox
    Friend WithEvents cbCharactersEdit As System.Windows.Forms.ComboBox
    Friend WithEvents btnNotAQuote As System.Windows.Forms.Button
    Friend WithEvents chkbxDisplayUnprocessedOnly As System.Windows.Forms.CheckBox
    Friend WithEvents chkbxDisplayOmittedClips As System.Windows.Forms.CheckBox
End Class
