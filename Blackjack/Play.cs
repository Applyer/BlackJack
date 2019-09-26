using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Play
    {
        // 引いたカード
        public Card Drawcard { get; set; }

        // プレイヤーの手札
        List<Card> PlayerHand = new List<Card>();

        // ディーラーの手札
        List<Card> DealerHand = new List<Card>();

        // 今のデッキ
        List<Card> NowDeck = new List<Card>();

        // デッキ作成
        Deck Shuffled = new Deck();

        // プレイヤーのターンか？
        bool isPlayerTurn = true;

        // バースト
        bool isBurst = false;

        // プレイヤーがブラックジャック
        bool isPlayerBJ = false;

        // ディーラーがブラックジャック
        bool isDealerBJ = false;

        // 比較可能か
        bool canCompare = false;

        // プレイヤーの手札の合計
        int PlayerScore = 0;

        // ディーラーの手札の合計
        int DealerScore = 0;

        // 入力されたもの
        string decision;

        // ゲームを始める準備
        public void Standby()
        {
            // 今回使うデッキ
            NowDeck = Shuffled.CreatDeck();

            // プレイヤーにカードを2枚配る
            Draw();
            Draw();
            isPlayerTurn = false;

            // ディーラーにカードを2枚配る
            Draw();
            Draw();
            isPlayerTurn = true;

            // コンソールを初期化
            Console.Clear();

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Yellow;

            // ゲーム開始
            Console.WriteLine("☆ﾐ ゲーム開始 ﾐ☆");

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Gray;

            // 両者の手札を公開
            ManageDealerhand();
            ManagePlayerhand();
        }

        // カードを1枚引く
        public void Draw()
        {
            // デッキの1番最初のカードを取得
            Drawcard = Shuffled.DeckRecip[0];

            // デッキの1番最初のカードを削除
            NowDeck.Remove(NowDeck[0]);

            // プレイヤーのターンなら
            if (isPlayerTurn)
            {
                // 書き込みの色設定
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("カードを1枚引くぜ！");

                // 取得したカードをプレイヤーの手札に加える
                PlayerHand.Add(Drawcard);

                // プレイヤーのスコアの初期化
                PlayerScore = 0;
            }
            // ディーラーのターンなら
            else
            {
                // 書き込みの色設定
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("ヤツがカードを1枚引いた！");

                // 取得したカードをディーラーの手札に加える
                DealerHand.Add(Drawcard);

                // ディーラーのスコアの初期化
                DealerScore = 0;
            }

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Gray;
        }


        // プレイヤーの手札
        public void ManagePlayerhand()
        {
            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("");
            Console.WriteLine("俺の手札");

            // マークによって表示色の変更
            foreach (Card hand in PlayerHand)
            {
                // ダイヤかハートなら赤
                if ((hand.Mark == "ダイヤ") || (hand.Mark == "ハート"))
                    Console.ForegroundColor = ConsoleColor.Red;

                // それ以外ならグレー
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write(hand.Mark);

                // 一部数字を記号化し手札の合計を計算
                switch (hand.Number)
                {
                    case 1:
                        Console.WriteLine("A");
                        PlayerScore += 1;
                        break;
                    case 11:
                        Console.WriteLine("J");
                        PlayerScore += 10;
                        break;
                    case 12:
                        Console.WriteLine("Q");
                        PlayerScore += 10;
                        break;
                    case 13:
                        Console.WriteLine("K");
                        PlayerScore += 10;
                        break;
                    default:
                        Console.WriteLine(hand.Number);
                        PlayerScore += hand.Number;
                        break;
                }
            }

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("合計値{0}", PlayerScore);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;

            // 21を超えたらバースト
            if (PlayerScore > 21)
            {
                isBurst = true;

                // ターンを代わる
                isPlayerTurn = false;
            }

            // 21ぴったりならブラックジャック
            if (PlayerScore == 21)
            {
                isPlayerBJ = true;

                // ターンを代わる
                isPlayerTurn = false;
            }
        }

        // ディーラーの手札
        public void ManageDealerhand()
        {
            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Magenta;

            // 最初の手札表示用
            if (isPlayerTurn)
            {
                Console.WriteLine("");
                Console.WriteLine("ディーラーの手札");

                // ダイヤかハートなら赤
                if ((DealerHand[0].Mark == "ダイヤ") || (DealerHand[0].Mark == "ハート"))
                    Console.ForegroundColor = ConsoleColor.Red;

                // それ以外ならグレー
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write(DealerHand[0].Mark);

                // 一部カードの数字を変更
                switch (DealerHand[0].Number)
                {
                    case 1:
                        Console.WriteLine("A");
                        break;
                    case 11:
                        Console.WriteLine("J");
                        break;
                    case 12:
                        Console.WriteLine("Q");
                        break;
                    case 13:
                        Console.WriteLine("K");
                        break;
                    default:
                        Console.WriteLine(DealerHand[0].Number);
                        break;
                }

                // 書き込みの色設定
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("＋謎のカード1枚");
            }
            // 最初のターンでないなら
            else
            {
                // ディーラーのAIを実行
                DealerAI();
            }

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // ディーラーAI
        public void DealerAI()
        {
            // 手札の合計を計算
            foreach (Card ManageDealerhand in DealerHand)
            {
                // 11以上なら10として計算
                if (ManageDealerhand.Number >= 11)
                    DealerScore += 10;

                // それ以外なら普通に計算
                else
                    DealerScore += ManageDealerhand.Number;
            }

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Gray;

            // 手札が17未満ならカードを引く
            if (DealerScore < 17)
                Draw();

            // 21を超えたらバースト
            else if (DealerScore > 21)
            {
                isBurst = true;
                isPlayerTurn = true;
            }

            // 21ぴったりならブラックジャック
            else if (DealerScore == 21)
            {
                isDealerBJ = true;
                isPlayerTurn = true;
            }
            // それ以外ならターンを代わる
            else
                isPlayerTurn = true;
        }

        // デッキから1枚引くか確認
        public void ConfirmDraw()
        {
            // プレイヤーにカードを引くか確認をする
            do
            {
                Console.Write("どうする？「ヒット(H)」or「スタンド(S)」");
                decision = Console.ReadLine();
            }
            // 入力が「h」か「s」以外なら無限ループ
            while ((decision != "h") && (decision != "s"));
        }

        // まだ引くかターンを終わるか
        public void RuleDraw()
        {
            // 入力が「h」なら
            if (decision == "h")
            {
                // カードを引く
                Draw();

                // プレイヤーの手札
                ManagePlayerhand();
            }
            // 入力が「s」なら
            else if (decision == "s")
                // ターンを代わる
                isPlayerTurn = false;
        }

        // 勝った表示
        public void WriteWin()
        {
            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("俺の勝ちだぁぁぁ！");

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 負けた表示
        public void WriteLose()
        {
            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("負けちまった・・・");

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 引き分け表示
        public void WriteTie()
        {
            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("まさかのひきわけ");

            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 結果がブラックジャック
        public void WriteBJ()
        {
            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("ブラックジャック");

            // 書き込みの色設定
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 結果がバースト
        public void WriteBurst()
        {
            // 書き込みの色設定
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("バースト");

            // 書き込みの色設定
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // ゲームの流れ
        public void GameFlow()
        {
            // ゲームを始める準備
            Standby();

            // プレイヤーのターンである限り
            while (isPlayerTurn)
            {
                // デッキから1枚引くか確認
                ConfirmDraw();

                // まだ引くかターンを終わるか
                RuleDraw();
            }

            // バーストなら負けて試合終了
            if (isBurst)
            {
                WriteBurst();
                WriteLose();
                isPlayerTurn = true;
            }
            
            // バースト以外ならターンを代わる
            if (!isBurst)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("ディーラーのターン！");
            }

            // ディーラーのターン
            while (!isPlayerTurn)
            {
                // 手札の処理
                ManageDealerhand();

                // 比較可能
                canCompare = true;
            }

            // 比較可能なら
            if (canCompare)
            {
                // 書き込みの色設定
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("");
                Console.WriteLine("ディーラーの手札");

                foreach (Card dManageDealerhand in DealerHand)
                {
                    // ダイヤかハートなら赤
                    if ((dManageDealerhand.Mark == "ダイヤ") || (dManageDealerhand.Mark == "ハート"))
                        Console.ForegroundColor = ConsoleColor.Red;

                    // それ以外ならグレー
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                    Console.Write(dManageDealerhand.Mark);

                    // 一部カードの数字を変更
                    switch (dManageDealerhand.Number)
                    {
                        case 1:
                            Console.WriteLine("A");
                            break;
                        case 11:
                            Console.WriteLine("J");
                            break;
                        case 12:
                            Console.WriteLine("Q");
                            break;
                        case 13:
                            Console.WriteLine("K");
                            break;
                        default:
                            Console.WriteLine(dManageDealerhand.Number);
                            break;
                    }
                }

                // 書き込みの色設定
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("合計値{0}", DealerScore);
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Gray;

                // バーストなら負けて試合終了
                if (isBurst)
                {
                    // バースト表示
                    WriteBurst();

                    // プレイヤーの勝ち
                    WriteWin();
                }
                // 両者がブラックジャックなら
                else if (isDealerBJ && isPlayerBJ)
                {
                    // ブラックジャック表示
                    WriteBJ();

                    // 引き分け
                    WriteTie();
                }
                // プレイヤーがブラックジャックなら
                else if (isPlayerBJ)
                {
                    // ブラックジャック表示
                    WriteBJ();

                    // プレイヤーの勝ち
                    WriteWin();
                }
                // ディーラーがブラックジャックなら
                else if (isDealerBJ)
                {
                    // ブラックジャック表示
                    WriteBJ();

                    // プレイヤーの負け
                    WriteLose();
                }
                // ふつうの判定
                else
                {
                    // プレイヤーの合計値が大きいなら
                    if (PlayerScore > DealerScore)

                        // プレイヤーの勝ち
                        WriteWin();

                    // ディーラーの合計値が大きいなら
                    else if (PlayerScore < DealerScore)

                        // プレイヤーの負け
                        WriteLose();

                    // それ以外なら
                    else

                        // 引き分け
                        WriteTie();

                }

                // ターンを代わる
                isPlayerTurn = true;
            }

            Console.WriteLine("");
        }
    }
}
