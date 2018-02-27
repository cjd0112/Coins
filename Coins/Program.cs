using System;
using System.Linq;
using CoinsLib.Coins;
using CoinsLib.Values;

namespace Coins
{
    class Program
    {
        static void Main(string[] args)
        {
            var coinStatic = CoinFactory.GenerateCoinStatic();

            var combos = CoinCalculator.GenerateAllCombinationsForValue(5, coinStatic).ToArray();

            CoinCalculator.Print(combos, 0, Console.Out);

            foreach (var c in combos)
            {
                foreach (var q in CoinCalculator.TotalCoinsForEachCombination(c))
                {
                    Console.WriteLine(q);
                }
            }


            foreach (var q in ValuePartitioner.PossibleWaysToDivideValueInTwo(5))
            {
                Console.WriteLine(q);
            }


            int res = CoinCalculator.CalculateTotalWaysToShare(48, coinStatic);

            Console.WriteLine(res);

            Console.ReadLine();


        }
    }
}
