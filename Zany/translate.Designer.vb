<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class translate
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
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.lblSource = New System.Windows.Forms.Label
        Me.lblTarget = New System.Windows.Forms.Label
        Me.btnForward = New System.Windows.Forms.Button
        Me.btnBack = New System.Windows.Forms.Button
        Me.tbSource = New System.Windows.Forms.TextBox
        Me.tbTarget = New System.Windows.Forms.TextBox
        Me.btnStart = New System.Windows.Forms.Button
        Me.lblLocation = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(435, 213)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "cancel xxx"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(516, 213)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK xxx"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblSource
        '
        Me.lblSource.AutoSize = True
        Me.lblSource.Location = New System.Drawing.Point(432, 37)
        Me.lblSource.Name = "lblSource"
        Me.lblSource.Size = New System.Drawing.Size(57, 13)
        Me.lblSource.TabIndex = 5
        Me.lblSource.Text = "source xxx"
        '
        'lblTarget
        '
        Me.lblTarget.AutoSize = True
        Me.lblTarget.Location = New System.Drawing.Point(432, 124)
        Me.lblTarget.Name = "lblTarget"
        Me.lblTarget.Size = New System.Drawing.Size(52, 13)
        Me.lblTarget.TabIndex = 6
        Me.lblTarget.Text = "target xxx"
        '
        'btnForward
        '
        Me.btnForward.Location = New System.Drawing.Point(129, 12)
        Me.btnForward.Name = "btnForward"
        Me.btnForward.Size = New System.Drawing.Size(29, 29)
        Me.btnForward.TabIndex = 21
        Me.btnForward.Text = ">"
        Me.btnForward.UseVisualStyleBackColor = True
        '
        'btnBack
        '
        Me.btnBack.Location = New System.Drawing.Point(94, 12)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(29, 29)
        Me.btnBack.TabIndex = 22
        Me.btnBack.Text = "<"
        Me.btnBack.UseVisualStyleBackColor = True
        '
        'tbSource
        '
        Me.tbSource.Location = New System.Drawing.Point(57, 53)
        Me.tbSource.Multiline = True
        Me.tbSource.Name = "tbSource"
        Me.tbSource.Size = New System.Drawing.Size(509, 68)
        Me.tbSource.TabIndex = 23
        '
        'tbTarget
        '
        Me.tbTarget.Location = New System.Drawing.Point(57, 139)
        Me.tbTarget.Multiline = True
        Me.tbTarget.Name = "tbTarget"
        Me.tbTarget.Size = New System.Drawing.Size(509, 68)
        Me.tbTarget.TabIndex = 24
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(57, 12)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(29, 29)
        Me.btnStart.TabIndex = 25
        Me.btnStart.Text = "<<"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Location = New System.Drawing.Point(164, 28)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(62, 13)
        Me.lblLocation.TabIndex = 26
        Me.lblLocation.Text = "location xxx"
        '
        'translate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(692, 273)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.tbTarget)
        Me.Controls.Add(Me.tbSource)
        Me.Controls.Add(Me.btnForward)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.lblTarget)
        Me.Controls.Add(Me.lblSource)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Name = "translate"
        Me.Text = "translate"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblSource As System.Windows.Forms.Label
    Friend WithEvents lblTarget As System.Windows.Forms.Label
    Friend WithEvents btnForward As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents tbSource As System.Windows.Forms.TextBox
    Friend WithEvents tbTarget As System.Windows.Forms.TextBox
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents lblLocation As System.Windows.Forms.Label
End Class
