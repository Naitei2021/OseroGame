<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Osr002
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Lbl_PowerSelect = New System.Windows.Forms.Label()
        Me.Btn_Hard = New System.Windows.Forms.Button()
        Me.Btn_Normal = New System.Windows.Forms.Button()
        Me.Btn_Easy = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Lbl_PowerSelect
        '
        Me.Lbl_PowerSelect.BackColor = System.Drawing.Color.LightCyan
        Me.Lbl_PowerSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Lbl_PowerSelect.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Lbl_PowerSelect.Location = New System.Drawing.Point(12, 9)
        Me.Lbl_PowerSelect.Name = "Lbl_PowerSelect"
        Me.Lbl_PowerSelect.Size = New System.Drawing.Size(410, 46)
        Me.Lbl_PowerSelect.TabIndex = 4
        Me.Lbl_PowerSelect.Text = "敵の強さを選んで下さい"
        Me.Lbl_PowerSelect.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Btn_Hard
        '
        Me.Btn_Hard.BackColor = System.Drawing.SystemColors.Window
        Me.Btn_Hard.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Hard.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Btn_Hard.Location = New System.Drawing.Point(302, 75)
        Me.Btn_Hard.Name = "Btn_Hard"
        Me.Btn_Hard.Size = New System.Drawing.Size(120, 67)
        Me.Btn_Hard.TabIndex = 4
        Me.Btn_Hard.Text = "強い"
        Me.Btn_Hard.UseVisualStyleBackColor = False
        '
        'Btn_Normal
        '
        Me.Btn_Normal.BackColor = System.Drawing.SystemColors.Window
        Me.Btn_Normal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Normal.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Btn_Normal.Location = New System.Drawing.Point(162, 75)
        Me.Btn_Normal.Name = "Btn_Normal"
        Me.Btn_Normal.Size = New System.Drawing.Size(120, 67)
        Me.Btn_Normal.TabIndex = 3
        Me.Btn_Normal.Text = "普通"
        Me.Btn_Normal.UseVisualStyleBackColor = False
        '
        'Btn_Easy
        '
        Me.Btn_Easy.BackColor = System.Drawing.SystemColors.Window
        Me.Btn_Easy.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Btn_Easy.Font = New System.Drawing.Font("Yu Gothic UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point)
        Me.Btn_Easy.Location = New System.Drawing.Point(12, 75)
        Me.Btn_Easy.Name = "Btn_Easy"
        Me.Btn_Easy.Size = New System.Drawing.Size(120, 67)
        Me.Btn_Easy.TabIndex = 2
        Me.Btn_Easy.Text = "弱い"
        Me.Btn_Easy.UseVisualStyleBackColor = False
        '
        'Osr002
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(434, 179)
        Me.ControlBox = False
        Me.Controls.Add(Me.Btn_Easy)
        Me.Controls.Add(Me.Btn_Normal)
        Me.Controls.Add(Me.Btn_Hard)
        Me.Controls.Add(Me.Lbl_PowerSelect)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(450, 195)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(450, 195)
        Me.Name = "Osr002"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Lbl_PowerSelect As Label
    Friend WithEvents Btn_Hard As Button
    Friend WithEvents Btn_Normal As Button
    Friend WithEvents Btn_Easy As Button
End Class
