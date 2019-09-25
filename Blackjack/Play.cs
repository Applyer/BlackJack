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
        bool isWriteBurst = false;

        // プレイヤーがブラックジャック
        bool isPlayerBJ = false;

        // ディーラーがブラックジャック
        bool isDealerBJ = false;

        // 結果に行くか
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

            // 両者の手札を公開
            ManageDealerhand();
            ManagePlayerhand();
        }

        // カードを1枚引く
        public void Draw()
        {
            Drawcard = Shuffled.DeckRecip[0];
            NowDeck.Remove(NowDeck[0]);
            if (isPlayerTurn)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("カードを1枚引くぜ！");
                PlayerHand.Add(Drawcard);
                PlayerScore = 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("ヤツがカードを1枚引いた！");
                DealerHand.Add(Drawcard);
                DealerScore = 0;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }


        // プレイヤーの手札
        public void ManagePlayerhand()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("");
            Console.WriteLine("俺の手札");

            foreach (Card hand in PlayerHand)
            {
                if ((hand.Mark == "ダイヤ") || (hand.Mark == "ハート"))
                    Console.ForegroundColor = ConsoleColor.Red;

                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write(hand.Mark);

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

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("合計値{0}", PlayerScore);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;

            // バースト
            if (PlayerScore > 21)
            {
                isWriteBurst = true;
                isPlayerTurn = false;
            }

            // ブラックジャック
            if (PlayerScore == 21)
            {
                isPlayerBJ = true;
                isPlayerTurn = false;
            }
        }

        // ディーラーの手札
        public void ManageDealerhand()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (isPlayerTurn)
            {
                Console.WriteLine("");
                Console.WriteLine("ディーラーの手札");

                if ((DealerHand[0].Mark == "ダイヤ") || (DealerHand[0].Mark == "ハート"))
                    Console.ForegroundColor = ConsoleColor.Red;

                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write(DealerHand[0].Mark);

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

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("と謎のカード1枚");
            }
            else
            {
                DealerAI();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // ディーラーAI
        public void DealerAI()
        {
            foreach (Card ManageDealerhand in DealerHand)
            {
                switch (ManageDealerhand.Number)
                {
                    case 1:
                        DealerScore += 1;
                        break;
                    case 11:
                        DealerScore += 10;
                        break;
                    case 12:
                        DealerScore += 10;
                        break;
                    case 13:
                        DealerScore += 10;
                        break;
                    default:
                        DealerScore += ManageDealerhand.Number;
                        break;
                }
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            // 手札が17未満ならカードを引く
            if (DealerScore < 17)
            {
                Draw();
            }

            // バースト
            else if (DealerScore > 21)
            {
                isWriteBurst = true;
                isPlayerTurn = true;
            }

            // ブラックジャック
            else if (DealerScore == 21)
            {
                isDealerBJ = true;
                isPlayerTurn = true;
            }

            else
            {
                isPlayerTurn = true;
            }
        }

        // デッキから1枚引くか？
        public void ConfirmDraw()
        {
            do
            {
                Console.Write("どうする？「ヒット(H)」or「スタンド(S)」");
                decision = Console.ReadLine();
            }
            while ((decision != "h") && (decision != "s"));
        }

        // まだ引くか
        public void RuleDraw()
        {
            if (decision == "h")
            {
                Draw();
                ManagePlayerhand();
            }
            else if (decision == "s")
            {
                isPlayerTurn = false;
            }
        }

        // 勝った
        public void WriteWin()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("俺の勝ちだぁぁぁ！");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 負けた
        public void WriteLose()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("負けちまった・・・");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 引き分け表示
        public void WriteTie()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("まさかのひきわけ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 結果がブラックジャック
        public void WriteBJ()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("ブラックジャック");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // 結果がバースト
        public void WriteBurst()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("バースト");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // ゲームの流れ
        public void GameFlow()
        {
            Standby();
            while (isPlayerTurn)
            {
                ConfirmDraw();
                RuleDraw();
            }

            // バーストか
            if (isWriteBurst)
            {
                WriteBurst();
                WriteLose();
                isPlayerTurn = true;
            }

            if (!isWriteBurst)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("ディーラーのターン！");
            }

            // ディーラーのターン
            while (!isPlayerTurn)
            {
                ManageDealerhand();
                canCompare = true;
            }

            if (canCompare)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("");
                Console.WriteLine("ディーラーの手札");

                foreach (Card dManageDealerhand in DealerHand)
                {
                    if ((dManageDealerhand.Mark == "ダイヤ") || (dManageDealerhand.Mark == "ハート"))
                        Console.ForegroundColor = ConsoleColor.Red;

                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                    Console.Write(dManageDealerhand.Mark);

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

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("合計は{0}", DealerScore);
                Console.ForegroundColor = ConsoleColor.Gray;

                // バーストか
                if (isWriteBurst)
                {
                    WriteBurst();
                    WriteWin();
                }
                // 両者がブラックジャックなら
                else if (isDealerBJ && isPlayerBJ)
                {
                    WriteBJ();
                    WriteTie();
                }
                // プレイヤーがブラックジャックなら
                else if (isPlayerBJ)
                {
                    WriteBJ();
                    WriteWin();
                }
                // ディーラーがブラックジャックなら
                else if (isDealerBJ)
                {
                    WriteBJ();
                    WriteLose();
                }
                // ふつうの判定
                else
                {
                    if (PlayerScore > DealerScore)
                        WriteWin();
                    else if (PlayerScore < DealerScore)
                        WriteLose();
                    else
                    {
                        WriteTie();
                    }

                }

                isPlayerTurn = true;
            }
        }
    }
}
