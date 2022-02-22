
'対戦相手がどこに石を置くか考えるクラス
Public Class Brain
    Inherits OseroCom

    Private ReadOnly gm As New Game(0)

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
        Dim ret As Boolean = False

        '①裏返せる石が１番多い個所を探す
        Dim startPoint As New Point(0, 0)
        Dim setMax As Integer = 0
        Dim setCount As Integer = 0

        For i = 0 To BoadSize - 1
            For ii = 0 To BoadSize - 1
                'x,yマスに石が置けるか
                If CanSetStone(myStone, startPoint.X, startPoint.Y) = False Then
                    startPoint.Y = startPoint.Y + 1
                    Continue For
                End If

                setCount = Set_Count(startPoint)

                '裏返せる数の最大を更新
                If setMax < setCount Then
                    setMax = setCount
                    'このマスに決める
                    x = startPoint.X
                    y = startPoint.Y
                    ret = True
                End If

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


    'どこに置くか（レベル：普通）
    Private Function Think_Normal(ByRef x As Integer, ByRef y As Integer) As Boolean
        Dim ret As Boolean = False

        Dim startPoint As New Point(0, 0)
        Dim setMax As Integer = -100000
        Dim setCount As Integer = 0
        Dim HyokaFunction As Integer = 0

        For i = 0 To BoadSize - 1
            For ii = 0 To BoadSize - 1
                'x,yマスに石が置けるか
                If CanSetStone(myStone, startPoint.X, startPoint.Y) = False Then
                    startPoint.Y = startPoint.Y + 1
                    Continue For
                End If

                '裏返せる石の個数
                setCount = Set_Count(startPoint)

                '巡目が前半の場合は裏返せる石の個数が少ない方を、後半の場合は多い方を評価する
                If gm.GameIndex <= 30 Then
                    HyokaFunction = -setCount
                Else
                    HyokaFunction = setCount
                End If

                '座標に応じてポイント追加
                HyokaFunction += PointCount(startPoint, 0)

                '裏返せる数の最大を更新
                If setMax < HyokaFunction Then
                    setMax = HyokaFunction
                    'このマスに決める
                    x = startPoint.X
                    y = startPoint.Y
                    ret = True
                End If

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


    'どこに置くか（レベル：難しい）
    Private Function Think_Hard(ByRef x As Integer, ByRef y As Integer) As Boolean
        Dim ret As Boolean = False

        Dim startPoint As New Point(0, 0)
        Dim setMax As Integer = -100000
        Dim setCount As Integer = 0
        Dim AroundCount As Integer = 0
        Dim HyokaFunction As Integer = 0

        For i = 0 To BoadSize - 1
            For ii = 0 To BoadSize - 1
                'x,yマスに石が置けるか
                If CanSetStone(myStone, startPoint.X, startPoint.Y) = False Then
                    startPoint.Y = startPoint.Y + 1
                    Continue For
                End If

                '裏返せる石の個数
                setCount = Set_Count(startPoint)

                '巡目が前半の場合は裏返せる石の個数が少ない方を、後半の場合は多い方を評価する
                If gm.GameIndex <= 30 Then
                    setCount = -setCount
                Else
                    setCount = setCount
                End If

                '周りの空白でないマスの数
                AroundCount = Count_around_Nocolor(startPoint)

                If gm.GameIndex > 20 AndAlso gm.GameIndex < 40 Then
                    '中盤は周りの空白でないマスの数を優先して評価
                    HyokaFunction = 10 * AroundCount + setCount
                Else
                    '序盤は裏返せる石の個数が少ない方を評価し、終盤は裏返せる石の個数が多い方を評価
                    HyokaFunction = AroundCount + 10 * setCount
                End If

                '座標に応じてポイント増減
                HyokaFunction += PointCount(startPoint, 1)

                '評価関数の最大を更新
                If setMax < HyokaFunction Then
                    setMax = HyokaFunction
                    'このマスに決める
                    x = startPoint.X
                    y = startPoint.Y
                    ret = True
                End If

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

#Region "メソッド"
    '裏返せる石の個数を返す
    Private Function Set_Count(startPoint As Point) As Integer
        Dim setCount As Integer = 0
        Dim vector As Integer
        Dim flgChangeEnd As Boolean
        Dim nextX As Integer = startPoint.X
        Dim nextY As Integer = startPoint.Y

        For vector = 0 To BoadSize - 1
            'vector(上とか下とか) 方向に裏返せる石があるかどうか
            If Not CanSetStoneVectol(myStone, UserStone, startPoint.X, startPoint.Y, vector) Then
                Continue For
            End If

            nextX = startPoint.X
            nextY = startPoint.Y
            '全方向で裏返せる数を取得
            setCount = 0
            flgChangeEnd = False
            Do While flgChangeEnd = False
                '検証する座標を設定
                Select Case vector
                    Case 0
                        nextX = nextX
                        nextY = nextY - 1
                    Case 1
                        nextX = nextX + 1
                        nextY = nextY - 1
                    Case 2
                        nextX = nextX + 1
                        nextY = nextY
                    Case 3
                        nextX = nextX + 1
                        nextY = nextY + 1
                    Case 4
                        nextX = nextX
                        nextY = nextY + 1
                    Case 5
                        nextX = nextX - 1
                        nextY = nextY + 1
                    Case 6
                        nextX = nextX - 1
                        nextY = nextY
                    Case 7
                        nextX = nextX - 1
                        nextY = nextY - 1
                End Select

                If nextX < 0 OrElse nextX > 7 OrElse nextY < 0 OrElse nextY > 7 Then
                    Continue For
                End If

                If GetBattleField(nextX, nextY) = myStone Then
                    flgChangeEnd = True
                    Continue Do
                End If
                'カウントアップ
                setCount = setCount + 1
            Loop
        Next

        Return setCount

    End Function

    '周りの空白でないマスの数を返す
    Private Function Count_around_Nocolor(Startpoint As Point)
        Dim setCount As Integer = 0
        Dim vector As Integer
        Dim nextX As Integer = Startpoint.X
        Dim nextY As Integer = Startpoint.Y

        For vector = 0 To BoadSize - 1
            '検証する座標を設定
            Select Case vector
                Case 0
                    nextX = nextX
                    nextY = nextY - 1
                Case 1
                    nextX = nextX + 1
                    nextY = nextY - 1
                Case 2
                    nextX = nextX + 1
                    nextY = nextY
                Case 3
                    nextX = nextX + 1
                    nextY = nextY + 1
                Case 4
                    nextX = nextX
                    nextY = nextY + 1
                Case 5
                    nextX = nextX - 1
                    nextY = nextY + 1
                Case 6
                    nextX = nextX - 1
                    nextY = nextY
                Case 7
                    nextX = nextX - 1
                    nextY = nextY - 1
            End Select

            '盤面外の場合カウントアップ
            If nextX < 0 OrElse nextX > 7 OrElse nextY < 0 OrElse nextY > 7 Then
                setCount += 1
                Continue For
            End If

            '空白でない場合にカウントアップ
            If GetBattleField(nextX, nextY) <> 2 Then
                setCount += 1
            End If
        Next

        Return setCount
    End Function


    '置く場所に応じて点数を返す
    Private Function PointCount(startPoint As Point, mode As Integer)
        Dim Nomalmode As Integer = 0
        Dim Hardmode As Integer = 1
        Dim nextX As Integer = startPoint.X
        Dim nextY As Integer = startPoint.Y
        Dim count As Integer = 0
        Dim CountMax As Integer = 10000
        Dim CountMin As Integer = -10000

        '角の場合にCountMaxを返す
        If nextX = 0 AndAlso nextY = 0 Then
            count = CountMax
            Return count
        ElseIf nextX = 0 AndAlso nextY = 7 Then
            count = CountMax
            Return count
        ElseIf nextX = 7 AndAlso nextY = 0 Then
            count = CountMax
            Return count
        ElseIf nextX = 7 AndAlso nextY = 7 Then
            count = CountMax
            Return count
        End If

        'ノーマルモードの場合はここで処理終わり
        If mode = Nomalmode Then
            Return count
        End If

        '角の一つ手前の場合にCountMinを返す
        If nextX < 2 AndAlso nextY < 2 Then
            count = CountMin
        ElseIf nextX < 2 AndAlso nextY > 5 Then
            count = CountMax
        ElseIf nextX > 5 AndAlso nextY < 2 Then
            count = CountMax
        ElseIf nextX > 5 AndAlso nextY > 5 Then
            count = CountMax
        End If

        Return count
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
