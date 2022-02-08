<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Osr001
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Btn_Start = New System.Windows.Forms.Button()
        Me.Pnl_GameArea = New System.Windows.Forms.Panel()
        Me.Lbl_YourStone = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Btn_Start
        '
        Me.Btn_Start.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Btn_Start.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        Me.Btn_Start.Location = New System.Drawing.Point(0, 555)
        Me.Btn_Start.Name = "Btn_Start"
        Me.Btn_Start.Size = New System.Drawing.Size(939, 61)
        Me.Btn_Start.TabIndex = 0
        Me.Btn_Start.Text = "ゲームスタート"
        Me.Btn_Start.UseVisualStyleBackColor = True
        '
        'Pnl_GameArea
        '
        Me.Pnl_GameArea.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl_GameArea.BackColor = System.Drawing.SystemColors.HighlightText
        Me.Pnl_GameArea.Location = New System.Drawing.Point(227, 41)
        Me.Pnl_GameArea.Name = "Pnl_GameArea"
        Me.Pnl_GameArea.Size = New System.Drawing.Size(480, 480)
        Me.Pnl_GameArea.TabIndex = 1
        '
        'Lbl_YourStone
        '
        Me.Lbl_YourStone.AutoSize = True
        Me.Lbl_YourStone.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_YourStone.Location = New System.Drawing.Point(12, 41)
        Me.Lbl_YourStone.Name = "Lbl_YourStone"
        Me.Lbl_YourStone.Size = New System.Drawing.Size(153, 21)
        Me.Lbl_YourStone.TabIndex = 2
        Me.Lbl_YourStone.Text = "あなたの選んだ石：〇"
        '
        'Osr001
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(939, 616)
        Me.Controls.Add(Me.Lbl_YourStone)
        Me.Controls.Add(Me.Pnl_GameArea)
        Me.Controls.Add(Me.Btn_Start)
        Me.Name = "Osr001"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "オセロ"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Btn_Start As Button
    Friend WithEvents Pnl_GameArea As Panel
    Friend WithEvents Lbl_YourStone As Label
End Class
