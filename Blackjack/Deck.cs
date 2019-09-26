using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    // デッキクラス
    class Deck
    {
        // デッキのプロパティ
        public List<Card> DeckRecip { get; set; }

        // デッキ作成
        public List<Card> CreatDeck()
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

            string[] instantMark = new string[] { "ｽﾍﾟｰﾄﾞ", "ダイヤ", "ハート", "ｸﾛｰﾊﾞｰ" };

            // デッキ番号が13以上を13以下に変える
            for (int i = 0; i < card.Length; i++)
            {
                // 余りに1を足し1～13を振りなおす
                quantity = card[i] % maxvalue + 1;

                // カードを生成
                carddata = new Card
                {
                    // 1～13に同じマークを与える
                    Mark = instantMark[card[i] / maxvalue],
                    // 1～13を割り振る
                    Number = quantity
                };

                // デッキにカードを追加
                deck.Add(carddata);
            }

            // デッキをプロパティに保存
            DeckRecip = deck;
            return deck;
        }
    }
}
