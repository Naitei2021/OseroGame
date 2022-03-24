
'敵レベルを選択する画面
Public Class Osr002

    Private selectedEnemyLv As Integer
    Private Sflg As Boolean
    Private eflg As Boolean
    Private cflg As Boolean

    '敵のレベルを参照するプロパティ
    Public ReadOnly Property EnemyLv As Integer
        Get
            Return selectedEnemyLv
        End Get
    End Property

    '弱いボタン
    Private Sub Btn_Easy_Click(sender As Object, e As EventArgs) Handles Btn_Easy.Click
        'Me.DialogResult = Enemy.Level.easy
        selectedEnemyLv = Enemy.Level.easy

        'フラグが立っている場合VeryEasyをセット
        If cflg Then
            selectedEnemyLv = Enemy.Level.VeryEasy
        End If

        Me.Close()
    End Sub

    '普通ボタン
    Private Sub Btn_Normal_Click(sender As Object, e As EventArgs) Handles Btn_Normal.Click
        'Me.DialogResult = Enemy.Level.normal
        selectedEnemyLv = Enemy.Level.normal
        Me.Close()
    End Sub

    '強いボタン
    Private Sub Btn_Hard_Click(sender As Object, e As EventArgs) Handles Btn_Hard.Click
        'Me.DialogResult = Enemy.Level.hard
        selectedEnemyLv = Enemy.Level.hard

        'フラグが立っている場合VeryHardをセット
        If cflg Then
            selectedEnemyLv = Enemy.Level.VeryHard
        End If

        Me.Close()
    End Sub

    'フォームクローズ
    Private Sub Osr002_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    'フォームロード
    Private Sub Osr002_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'フラグを初期化
        Sflg = False
        eflg = False
        cflg = False
    End Sub

    'モード変更
    Private Sub Osr002_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If cflg Then
            Exit Sub
        End If

        If e.KeyChar = "s" Then
            Sflg = True
            Exit Sub
        End If

        If e.KeyChar = "e" AndAlso Sflg Then
            eflg = True
            Exit Sub
        End If

        If e.KeyChar = "c" AndAlso eflg Then
            cflg = True
            MessageBox.Show("モードを変更します", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Btn_Easy.Text = "とても弱い"
            Btn_Hard.Text = "とても強い"
        End If
    End Sub

End Class