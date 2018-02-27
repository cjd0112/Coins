using System;
using System.Diagnostics;
using System.Linq;
using CoinsLib.Coins;
using CoinsLib.LSD;
using CoinsLib.Values;

namespace Coins
{
    class Program
    {
        static void Main(string[] args)
        {
            var z = new LSDParser("8/1d");

            var a = new Stopwatch();
            a.Start();
            var res = CoinCalculator2.CalculateTotalWaysToShare(z.ValueInHalfD(), CoinFactory.GenerateCoinStatic());
            a.Stop();
            Console.WriteLine($"{res} returned in {a.ElapsedMilliseconds/1000}seconds" );

            Console.ReadLine();


        }
    }
}
