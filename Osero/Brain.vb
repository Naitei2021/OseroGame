
'対戦相手がどこに石を置くか考えるクラス
Public Class Brain
    Inherits OseroCom

#Region "実装してほしい内容"

    'どこへ石を置くか考える部分
    '置くべきマス目が決まったら、x（マス目の横方向、0～7の値）と y（縦方向、0～7の値）を設定してください 
    '例）左上から数えて横に3マス目、上から5マス目に決めた！
    'x = 2  y = 4  となります
    '
    '盤面の状況は、変数 BattleField の中に入っています(白石なら0、黒石なら1、空白なら2)
    '例）盤面の左上から数えて2マス目、上から数えて５マス目に白石が置かれていたら
    'BattleField(1,4) には「0」が設定されています
    '
    '指定したマス目（左上が起点）に何が置かれているか取得したい場合、 getBattleField()メソッドを利用してください
    '例）1番右下のマスに何が置かれているか取得したい
    'Dim 変数名 As Integer = getBattleField(7,7)

#End Region


#Region "どこに石を置くか"

    '石を置く場所を検討する
    '引数：x  ->  置きたい場所  
    '      y  ->　置きたい場所
    '戻値：置き場所がない場合 false
    Public Function Think(ByRef x As Integer, ByRef y As Integer, userStone As Integer, level As Integer) As Boolean
        Dim ret As Boolean = True
        '石の色(myStone が今回置く石です）
        myStone = 1 - userStone
        enemyStone = userStone

        '初期設定
        x = 99
        y = 99

        '敵のレベルによって石の置き場所を考える
        Select Case level
            Case Enemy.Level.easy
                Return Think_Easy(x, y)
            Case Enemy.Level.normal
                Return Think_Normal(x, y)
            Case Enemy.Level.hard
                Return Think_Hard(x, y)
            Case Else
                Return Think_Nakamura(x, y)
        End Select
    End Function


    'どこに置くか（レベル：優しい）
    Private Function Think_Easy(ByRef x As Integer, ByRef y As Integer) As Boolean

    End Function


    'どこに置くか（レベル：普通）
    Private Function Think_Normal(ByRef x As Integer, ByRef y As Integer) As Boolean

    End Function


    'どこに置くか（レベル：難しい）
    Private Function Think_Hard(ByRef x As Integer, ByRef y As Integer) As Boolean

    End Function


    'どこに置くか(中村が10月に考えたやつ)
    Private Function Think_Nakamura(ByRef x As Integer, ByRef y As Integer) As Boolean
        Dim ret As Boolean = False

        '①裏返せる石が１番多い個所を探す
        Dim startPoint As New Point(0, 0)
        Dim setMax As Integer = 0
        Dim setCount As Integer = 0

        Dim vector As Integer
        Dim flgChangeEnd As Boolean = False
        Dim nextX As Integer = startPoint.X
        Dim nextY As Integer = startPoint.Y

        For i = 0 To BoadSize - 1
            For ii = 0 To BoadSize - 1
                'x,yマスに石が置けるか
                If CanSetStone(myStone, startPoint.X, startPoint.Y) = False Then
                    startPoint.Y = startPoint.Y + 1
                    Continue For
                End If
                '裏返せる石を裏返す
                For vector = 0 To BoadSize - 1
                    'vector(上とか下とか) 方向に裏返せる石があるかどうか
                    If CanSetStoneVectol(myStone, UserStone, startPoint.X, startPoint.Y, vector) Then
                        nextX = startPoint.X
                        nextY = startPoint.Y
                        '全方向で裏返せる数を取得
                        setCount = 0
                        Do While flgChangeEnd = False
                            '検証する座標を設定
                            Select Case vector
                                Case 0
                                    nextX = nextX
                                    nextY = nextY - 1
                                    If nextY < 0 Then
                                        Continue For
                                    End If
                                Case 1
                                    nextX = nextX + 1
                                    nextY = nextY - 1
                                    If nextX > 7 Or nextY < 0 Then
                                        Continue For
                                    End If
                                Case 2
                                    nextX = nextX + 1
                                    nextY = nextY
                                    If nextX > 7 Then
                                        Continue For
                                    End If
                                Case 3
                                    nextX = nextX + 1
                                    nextY = nextY + 1
                                    If nextX > 7 Or nextY > 7 Then
                                        Continue For
                                    End If
                                Case 4
                                    nextX = nextX
                                    nextY = nextY + 1
                                    If nextY > 7 Then
                                        Continue For
                                    End If
                                Case 5
                                    nextX = nextX - 1
                                    nextY = nextY + 1
                                    If nextX < 0 Or nextY > 7 Then
                                        Continue For
                                    End If
                                Case 6
                                    nextX = nextX - 1
                                    nextY = nextY
                                    If nextX < 0 Then
                                        Continue For
                                    End If
                                Case 7
                                    nextX = nextX - 1
                                    nextY = nextY - 1
                                    If nextX < 0 Or nextY < 0 Then
                                        Continue For
                                    End If
                            End Select

                            If GetBattleField(nextX, nextY) = myStone Then
                                flgChangeEnd = True
                                Continue Do
                            End If
                            'カウントアップ
                            setCount = setCount + 1
                        Loop
                        '裏返せる数の最大を更新
                        If setMax < setCount Then
                            setMax = setCount
                            'このマスに決める
                            x = startPoint.X
                            y = startPoint.Y
                            ret = True
                        End If
                    End If
                Next
                startPoint.Y = startPoint.Y + 1
            Next ii
            startPoint.X = i + 1
            startPoint.Y = 0
        Next i

        '置くべきマス目が見つからなかった場合
        If x = 99 Or y = 99 Then
            ret = False
        ElseIf OutofBoadCheck(x, y) = False Then
            ret = False
        End If
        Return ret
    End Function

#End Region


#Region "Private変数"

    '石の色(分かりにくいけど、myStone が自分の石ね）
    Private myStone As Integer
    Private enemyStone As Integer

#End Region

#Region "コンストラクタ"

    'コンストラクタ
    Public Sub New(battleField As Integer(,))
        SetBoad(battleField)
    End Sub


#End Region

End Class
