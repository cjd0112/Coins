using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using CoinsLib.Coins;
using CoinsLib.CombinationCalculator;
using CoinsLib.CombinationCalculator.Cache;
using CoinsLib.Util;

namespace Coins
{

    class Program
    {

        static void Main(string[] args)
        {
            try
            {

                int num = 202;
                
                var s = new Stopwatch();

                s.Start();
                
          //      var oldRes = CoinCalculator2.CalculateTotalWaysToShare(num, CoinFactory.GenerateCoinStatic());

            //    Console.WriteLine($"Correct res = {oldRes}");
                
              //  Console.WriteLine($"Time - {s.ElapsedMilliseconds/1000}");

                s.Stop();
                
                s = new Stopwatch();

                s.Start();

                var coins = MagicPurse.GenerateCoinStatic();

                var calculationGrid = new CalculationGrid(coins.AllCombinations().ToArray(), num);

                Console.WriteLine("myres = " + calculationGrid.CalculateTotalCoins(num));                

                s.Stop();
                
                Console.WriteLine($"Seconds = {s.ElapsedMilliseconds}");

                s = new Stopwatch();

                var orchestrator = new CoinOrchestrator(num);

                var res = orchestrator.Calculate();

                Console.WriteLine($"result22 = {res}");

                s.Stop();

                Console.WriteLine($"Seconds = {s.ElapsedMilliseconds}");


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}
