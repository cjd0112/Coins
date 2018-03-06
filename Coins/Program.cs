using System;
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
            int num = 10;

            var oldRes = CoinCalculator2.CalculateTotalWaysToShare(num, CoinFactory.GenerateCoinStatic());

            Console.WriteLine($"Correct res = {oldRes}");


            var coins = MagicPurse.GenerateCoinStatic();

            var reducer = new ComboReducer(num);

            // use ToArray() to make sure we don't unnecessarily duplicate calculations
            var calculationGrid = coins.AllCombinations().ToArray()
                .Select<CoinsLib.CombinationCalculator.Coin, IComboCalculator>(x =>
                {
                    if (x.RequiresUnevenFactorCalculator())
                        return new ComboCalculatorUnevenFactors(x);
                    else
                        return new ComboCalculatorEvenFactors(x);
                }).ToArray();

            foreach (var i in Enumerable.Range(0, num))
            {
                calculationGrid.ForEach(x => x.Increment(reducer.GetStorageArrayForValue(i)));
            }

            // now the total number of ways to divide the number is the sum of each
            // pairs 'number of combinations in common'
            // so if number is 3 - can divide that only one way 1 - 2
            // a->1 = (1) 
            // b->2 = ((1 1),(1))  
            // result = 1
            Console.Write($"Our res =");
            Console.WriteLine(Enumerable.Range(1,num-1).Zip(Enumerable.Range(1,num-1).Reverse(),(x,y)=>(x,y)).Sum(x=>reducer.SharedCombinationOfTwoValues(x)));

            Console.ReadLine();

        }
    }
}
