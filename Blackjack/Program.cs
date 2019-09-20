using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    // カードクラス
    class Card
    {
        // マーク
        public string mark { get; set; }

        // 数字
        public int number { get; set; }
    }

    // デッキクラス
    class Deck
    {
        // デッキのプロパティ
        public List<Card> DeckRecip { get; set; }

        // デッキ作成
        public List<Card> CreatDeck ()
        {
            // デッキ用のリスト
            List<Card> deck = new List<Card>();

            // カードデータはまだ Null
            Card carddata = null;

            // カードの最高値(キング)
            int maxvalue = 13;

            // デッキの最大枚数(13 * 4)
            int deckvalue = 52;

            // デッキの枚数分確保
            int[] card = new int[deckvalue];

            // デッキに番号を割り振る
            for (int i = 0; i < card.Length; i++)
            {
                card[i] = i;
            }

            // 乱数宣言
            Random random = new Random();

            // デッキの番号をシャッフルする
            for (int i = 0; i < card.Length; i++)
            {
                int randnum = random.Next(deckvalue);

                int prov = card[i];

                card[i] = card[randnum];

                card[randnum] = prov;
            }

            // 番号振り直し用
            int quantity = 0;

            string[] instantmark = new string[] { "スペード", "ダイヤ", "ハート", "クローバー" };

            // デッキ番号が13以上を13以下に変える
            for (int i = 0; i < card.Length; i++)
            {
                // 余りに1を足し1～13を振りなおす
                quantity = card[i] % maxvalue + 1;

                // カードを生成
                carddata = new Card
                {
                    // 1～13に同じマークを与える
                    mark = instantmark[card[i] / maxvalue],
                    // 1～13を割り振る
                    number = quantity
                };

                // デッキにカードを追加
                deck.Add(carddata);
            }

            DeckRecip = deck;
            return deck;
        }
    }

    class Play
    {
        // 引いたカード
        public Card drawcard { get; set; }

        // プレイヤーの手札
        List<Card> PlayerHand = new List<Card>();

        // ディーラーの手札
        List<Card> DealerHand = new List<Card>();

        // 今のデッキ
        List<Card> NowDeck = new List<Card>();

        // デッキ作成
        Deck Shuffled = new Deck();

        // プレイヤーのターンか？
        bool turnplayer = true;

        // バースト
        bool burst = false;

        // ブラックジャック
        bool blackjack = false;

        // 結果に行くか
        bool Compare = false;

        // プレイヤーの手札の合計
        int PlayerScore = 0;

        // ディーラーの手札の合計
        int DealerScore = 0;

        // 入力されたもの
        string decision;
        // デッキを取得
        public void Stand()
        {
            // 今回使うデッキ
            NowDeck = Shuffled.CreatDeck();

            // プレイヤーにカードを2枚配る
            Draw();
            Draw();
            turnplayer = false;

            // ディーラーにカードを2枚配る
            Draw();
            Draw();
            turnplayer = true;

            // 両者の手札を公開
            Dhand();
            Phand();
        }

        // カードを1枚引く
        public void Draw()
        {
            drawcard = Shuffled.DeckRecip[0];
            NowDeck.Remove(NowDeck[0]);
            if (turnplayer)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("カードを1枚引くぜ！");
                PlayerHand.Add(drawcard);
                PlayerScore = 0;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("ヤツがカードを1枚引いた！");
                DealerHand.Add(drawcard);
                DealerScore = 0;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }


        // プレイヤーの手札
        public void Phand()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("");
            Console.WriteLine("俺の手札");

            foreach (Card hand in PlayerHand)
            {
                if ((hand.mark == "ダイヤ") || (hand.mark == "ハート"))
                    Console.ForegroundColor = ConsoleColor.Red;

                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write(hand.mark);

                switch (hand.number)
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
                        Console.WriteLine(hand.number);
                        PlayerScore += hand.number;
                        break;
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            Console.WriteLine("合計値{0}",PlayerScore);
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Gray;

            // バースト
            if (PlayerScore > 21)
            {
                burst = true;
                turnplayer = false;
            }

            // ブラックジャック
            if (PlayerScore == 21)
            {
                blackjack = true;
                turnplayer = false;
            }
        }

        // ディーラーの手札
        public void Dhand()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (turnplayer)
            {
                Console.WriteLine("");
                Console.WriteLine("ディーラーの手札");

                if ((DealerHand[0].mark == "ダイヤ") || (DealerHand[0].mark == "ハート"))
                    Console.ForegroundColor = ConsoleColor.Red;

                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                Console.Write(DealerHand[0].mark);

                switch (DealerHand[0].number)
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
                        Console.WriteLine(DealerHand[0].number);
                        break;
                }

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("と謎のカード1枚");
            }
            else
            {
                Dai();
            }

            Console.ForegroundColor = ConsoleColor.Gray;
        }

        // ディーラーAI
        public void Dai()
        {
            foreach (Card dhand in DealerHand)
            {
                switch (dhand.number)
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
                        DealerScore += dhand.number;
                        break;
                }
                 Console.ForegroundColor = ConsoleColor.Gray;
            }

            // 手札が17未満ならカードを引く
            if (DealerScore < 17)
            {
                Draw();
            }

            // バースト
            else if (DealerScore > 21)
            {
                burst = true;
                turnplayer = true;
            }

            // ブラックジャック
            else if (DealerScore == 21)
            {
                blackjack = true;
                turnplayer = true;
            }

            else
            {
                turnplayer = true;
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
        public void Rule()
        {
            if (decision == "h")
            {
                Draw();
                Phand();
            }
            else if (decision == "s")
            {
                turnplayer = false;
            }
        }

        // 勝った
        public void Win()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("俺の勝ちだぁぁぁ！");
        }

        // 負けた
        public void Lose()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("負けちまった・・・");
        }

        // 結果がブラックジャック
        public void BJack()
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine("ブラックジャック");
        }

        public void BurST()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("バースト");
        }

        // ゲームの流れ
        public void Swing()
        {
            Stand();
            while (turnplayer)
            {
                ConfirmDraw();
                Rule();
            }

            // バーストか
            if (burst)
            {
                BurST();
                Lose();
                turnplayer = true;
            }

            // ブラックジャックか
            if (blackjack)
            {
                BJack();
                Win();

                Console.ForegroundColor = ConsoleColor.Gray;
                turnplayer = true;
            }

            if (!burst && !blackjack)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("ディーラーのターン！");
            }

            // ディーラーのターン
            while (!turnplayer)
            {
                Dhand();
                Compare = true;
            }

            if (Compare)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("");
                Console.WriteLine("ディーラーの手札");

                foreach (Card ddhand in DealerHand)
                {
                    if ((ddhand.mark == "ダイヤ") || (ddhand.mark == "ハート"))
                        Console.ForegroundColor = ConsoleColor.Red;

                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray;

                    Console.Write(ddhand.mark);

                    switch (ddhand.number)
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
                            Console.WriteLine(ddhand.number);
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }

                Console.WriteLine("合計は{0}", DealerScore);

                // バーストか
                if (burst)
                {
                    BurST();
                    Win();
                }
                // ブラックジャックか
                else if (blackjack)
                {
                    BJack();
                    Lose();
                }
                // ふつうの判定
                else
                {
                    if (PlayerScore > DealerScore)
                        Win();
                    else if (PlayerScore < DealerScore)
                        Lose();
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("まさかのひきわけ");
                    }

                }

                turnplayer = true;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string rematch;

            // 無現ループ
            while (true)
            {
                // インスタンスを生成
                Play BJ = new Play();

                // ゲーム本体
                BJ.Swing();

                Console.ForegroundColor = ConsoleColor.Gray;

                // 再戦するか確認
                do
                {
                    Console.Write("再戦する？「おう！(Y)」or「断る！(N)」");
                    rematch = Console.ReadLine();
                } while ((rematch != "y") && (rematch != "n"));

                Console.Clear();

                // 再戦しない場合ループを抜けて終了
                if (rematch == "n")
                {
                    break;
                }
            }
            
        }
    }
}
