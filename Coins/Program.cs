using System;
using System.Diagnostics;
using System.Linq;
using CoinsLib.Coins;
using CoinsLib.CombinationCalculator;
using CoinsLib.Util;

namespace Coins
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int num = 960;

                //var oldRes = CoinCalculator2.CalculateTotalWaysToShare(num, CoinFactory.GenerateCoinStatic());

                //Console.WriteLine($"Correct res = {oldRes}");

                var s = new Stopwatch();

                s.Start();


                var coins = MagicPurse.GenerateCoinStatic();

                var reducer = new ComboReducer(num);

                bool validate = false;

                // use ToArray() to make sure we don't unnecessarily duplicate calculations
                var calculationGrid = coins.AllCombinations().ToArray()
                    .Select<CoinsLib.CombinationCalculator.Coin, IComboCalculator>(x =>
                    {
                        if (validate)
                            return new ComboCalculatorRecursive(x);
                        else
                        {
                            if (x.RequiresUnevenFactorCalculator())
                                return new ComboCalculatorUnevenFactors(x);
                            else
                                return new ComboCalculatorEvenFactors(x);
                            
                        }
                    }).ToArray();

                Int64 totalNumberCombinations;
                foreach (var i in Enumerable.Range(1, num-1).Zip(Enumerable.Range(1,num-1).Reverse(),(x,y)=>(x,y)))
                {
                    var maxCoins = i.Item2;
                    
                    totalNumberCombinations = calculationGrid.Sum(x => x.Calculate(i.Item1, maxCoins, reducer.GetStorageArrayForValue(i.Item1)));
                }

                // now the total number of ways to divide the number is the sum of each
                // pairs 'number of combinations in common'
                // so if number is 3 - can divide that only one way 1 - 2
                // a->1 = (1) 
                // b->2 = ((1 1),(1))  
                // result = 1
                Console.Write($"Our res =");
                Console.WriteLine(Enumerable.Range(1,num-1).Zip(Enumerable.Range(1,num-1).Reverse(),(x,y)=>(x,y)).Sum(x=>reducer.SharedCombinationOfTwoValues(x)));

                s.Stop();
                
                Console.WriteLine($"Seconds = {s.ElapsedMilliseconds/1000}");


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}
