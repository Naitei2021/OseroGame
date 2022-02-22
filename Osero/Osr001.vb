
'ゲーム画面に関するクラス
Public Class Osr001


#Region "Private変数"

    'ユーザが選択した敵のレベル
    Private selectedEnemyLV As Integer

    Private Flag As Boolean

#End Region

#Region "他クラス"

    'ゲームクラス
    Private game As New Game(Game.Color.White)

#End Region

    'Osr001_Shown 内に「機能仕様書（再開処理）の項番1」を実装してください
#Region "フォームロード関係"

    'FormLoad
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'フォームサイズを固定
        Me.FormBorderStyle = FormBorderStyle.FixedSingle

    End Sub

    'Shown
    Private Sub Osr001_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim A As New UserData(id:="OseroUser")
        Try
            'Nothingの場合ここで処理終了
            If A Is Nothing Then
                Exit Try
            Else
                '中断フラグが１じゃない場合ここで処理終了
                If A.BreakFlg = UserData.Flg.FlgOn Then
                    '中断処理
                    Dim log As DialogResult = MessageBox.Show("前回中断したデータが存在します。途中から再開しますか？", "確認メッセージ", MessageBoxButtons.YesNo)
                    '中断する場合(はい)
                    If log = DialogResult.Yes Then
                        game.GameResumption(A)
                        Flag = True
                        '中断しない場合(いいえ) 中断データを消去
                    ElseIf log = DialogResult.No Then
                        'データを保存
                        A.BreakFlg = UserData.Flg.FlgOff
                        A.EnemyType = Nothing
                        A.EnemyLV = Nothing
                        A.MoveCount = 0
                        A.LastBoad = Nothing
                        A.SaveUserData()
                    End If
                Else
                    Exit Try
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("画面表示に失敗しました")
            Close()
        End Try
    End Sub

#End Region

    'Else の部分に「機能仕様所（中断処理）の項番1」を実装してください
#Region "ボタンクリック関係"

    'ゲームスタート(中断)ボタン
    Private Sub Btn_Start_Click(sender As Object, e As EventArgs) Handles Btn_Start.Click
        Try
            If game.GameStatus = Game.Status.Game_status_NoGame Then
                game.GameStart(Enemy.EnemyIs.CPU, Enemy.Level.nakamura)
                Btn_Start.Text = "中断する"
            Else
                'If MessageBox.Show("途中だけど止める？", "確認", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                '    game.GameGiveup()
                '    Btn_Start.Text = "ゲームスタート"
                'End If

            End If

        Catch ex As Exception
            MessageBox.Show("中断処理に失敗しました", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


#End Region

#Region "オセロ盤 関係"

    '画面にオセロ用の線を引く
    Private Sub Pnl_GameArea_Paint(sender As Object, e As PaintEventArgs) Handles Pnl_GameArea.Paint
        ImageDraw.DrawField(Pnl_GameArea, e)
        ImageDraw.Refresh(Pnl_GameArea)
    End Sub

    'マス目をクリックした時
    Private Sub Pnl_GameArea_Click(sender As Object, e As EventArgs) Handles Pnl_GameArea.Click
        If game.GameStatus <> Game.Status.Game_status_Game And game.Turn = 0 Then
            Exit Sub
        End If

        'クリックしたマスを取得
        Dim x As Integer
        Dim y As Integer
        getMasu(x, y)

        '勝敗が決まった終了処理
        If game.GameAdvance(x, y) = Game.AfterTurnEnd.GameEnd Then
            game.GameEnd()
            Btn_Start.Text = "ゲームスタート"
        End If
    End Sub

    'クリックしたマスを取得
    Private Sub getMasu(ByRef x As Integer, ByRef y As Integer)
        'マスの左上を取得
        Dim sp As System.Drawing.Point = System.Windows.Forms.Cursor.Position
        Dim cp As System.Drawing.Point = Me.PointToClient(sp)
        Dim fieldX As Integer = Pnl_GameArea.Location.X
        Dim fieldY As Integer = Pnl_GameArea.Location.Y
        'カーソルの位置を取得
        Dim xx As Integer = cp.X
        Dim yy As Integer = cp.Y
        'クリックしたマス目
        Dim masuX As Integer = Math.Floor((yy - fieldY + 60) / 60)
        Dim masuY As Integer = Math.Floor((xx - fieldX + 60) / 60)

        x = masuY - 1
        y = masuX - 1
    End Sub

#End Region

#Region "フォームクローズ"

    'フォームを閉じるとき
    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If game.GameStatus <> 1 Then
            Exit Sub
        End If


        'If MessageBox.Show("途中だけど止める？", "確認", MessageBoxButtons.YesNo) = DialogResult.No Then
        '    e.Cancel = True
        'End If
    End Sub




#End Region



End Class
