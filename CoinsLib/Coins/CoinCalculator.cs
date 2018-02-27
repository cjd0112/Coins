using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoinsLib.Values;

namespace CoinsLib.Coins
{
    public class CoinCalculator
    {
        public static IEnumerable<RuntimeCoin> GenerateAllCombinationsForValue(int value,Coin c)
        {
            IEnumerable<(int coins, int remainder)> CoinsAndRemaindersAtThisLevel()
            {
                if (c.IsLeaf())
                    yield return (value, c.Units);
                else
                {
                    for (int i = 0; i <= value; i += c.Units)
                    {
                        yield return (i / c.Units, value - i );
                    }
                }
            }

            return CoinsAndRemaindersAtThisLevel().Select(x =>
                new RuntimeCoin(c, x.coins, c.Next != null? GenerateAllCombinationsForValue(x.remainder, c.Next):Enumerable.Empty<RuntimeCoin>()));
        }
        public static IEnumerable<int> TotalCoinsForEachCombination(RuntimeCoin c)
        {
            if (!c.Next.Any())
                return new[] {c.NumCoins};

            return c.Next.SelectMany(TotalCoinsForEachCombination).Select(x => x + c.NumCoins);
        }

        public static int CalculateTotalWaysToShare(int value,Coin g)
        {
            int cnt = 0;
            Dictionary<int,IEnumerable<RuntimeCoin>> comboMap = new Dictionary<int, IEnumerable<RuntimeCoin>>();
            foreach (var c in ValuePartitioner.PossibleWaysToDivideValueInTwo(value))
            {
                comboMap[c.lhs] =  GenerateAllCombinationsForValue(c.lhs, g);

            }

            foreach (var c in ValuePartitioner.PossibleWaysToDivideValueInTwo(value))
            {
                var lhs_total_coins = comboMap[c.lhs].SelectMany(TotalCoinsForEachCombination).ToArray();
                var rhs_total_coins = comboMap[c.rhs].SelectMany(TotalCoinsForEachCombination).ToArray();

                foreach (var n in lhs_total_coins)
                {
                    cnt += rhs_total_coins.Count(b => b == n);
                }

            }

            return cnt;
        }


        public static void Print(IEnumerable<RuntimeCoin> c,int level,TextWriter o)
        {
            foreach (var g in c)
            {
                o.Write($"{new String('\t', level)} {g.Coin.Name} #{g.NumCoins}");
                o.WriteLine();
                Print(g.Next,level+1,o);
            }
        }
    }
}
