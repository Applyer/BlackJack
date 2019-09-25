using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            // 再戦か読み取り用
            string rematch;

            // 無現ループ
            while (true)
            {
                // インスタンスを生成
                Play BJ = new Play();

                // ゲーム本体
                BJ.GameFlow();

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
