using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CoinsLib.Values;

namespace CoinsLib.Coins
{
    public class CoinCalculator2
    {
        public static IEnumerable<RuntimeCoin> GenerateAllCombinationsForValueKeepingTrackOfTotalCoinsAndAddingLeafNodesToList(int value,int accNoCoins,Coin c,List<int> leafNodes)
        {
            IEnumerable<(int totalCoins, int coinsThisLevel, int remainder)> CoinsAndRemaindersAtThisLevel()
            {
                if (c.IsLeaf())
                    yield return (accNoCoins + value,value, c.Units);
                else
                {
                    for (int i = 0; i <= value; i += c.Units)
                    {
                        yield return (i/c.Units + accNoCoins, i / c.Units, value - i );
                    }
                }
            }

            var ret = CoinsAndRemaindersAtThisLevel().Select(x =>
                new RuntimeCoin(c, x.totalCoins,
                    c.Next != null
                        ? GenerateAllCombinationsForValueKeepingTrackOfTotalCoinsAndAddingLeafNodesToList(x.remainder,
                            x.totalCoins, c.Next, leafNodes)
                        : Enumerable.Empty<RuntimeCoin>())).ToArray();

            if (c.IsLeaf())
                leafNodes.AddRange(ret.Select(x=>x.NumCoins));

            return ret;
        }

        public static int NumCoinsForLeafNode(RuntimeCoin c)
        {
            return c.NumCoins;
        }


        public static int CalculateTotalWaysToShare(int value,Coin g)
        {
            var sw = new Stopwatch();
            sw.Start();
            int cnt = 0;
            Dictionary<int,IEnumerable<int>> comboMap = new Dictionary<int, IEnumerable<int>>();
            foreach (var c in ValuePartitioner.PossibleWaysToDivideValueInTwo(value))
            {
                List<int> leafNodes = new List<int>();
                GenerateAllCombinationsForValueKeepingTrackOfTotalCoinsAndAddingLeafNodesToList(c.lhs,0, g,leafNodes);
                comboMap[c.lhs] = leafNodes;

            }

            sw.Stop();

            Console.WriteLine(sw.ElapsedMilliseconds/1000);

            sw.Start();

            foreach (var c in ValuePartitioner.PossibleWaysToDivideValueInTwo(value))
            {
                var lhs_total_coins = comboMap[c.lhs];
                var rhs_total_coins = comboMap[c.rhs];
                foreach (var n in lhs_total_coins)
                {
                    cnt += rhs_total_coins.Count(b => b == n);
                }

            }

            Console.WriteLine(sw.ElapsedMilliseconds / 1000);


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
