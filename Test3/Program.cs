using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using CoinsLib.CombinationCalculator;
using CoinsLib.Util;

namespace Test3
{
    class Program
    {

        static void CheckEquals(IEnumerable<Int32> a, IEnumerable<Int32> b)
        {
            if (a.Count() != b.Count())
                Console.WriteLine("failed count");
            a.ForEach(x =>
            {
                if (b.Contains(x) == false)
                    Console.WriteLine("faile");
            });
            
            b.ForEach(x =>
            {
                if (a.Contains(x) == false)
                    Console.WriteLine("fail");
            });
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            

            var arr = new[] {32,16, 8, 4, 2, 1};

            var targetValue = arr.Sum() +1000;
            
            Console.WriteLine("target value is " + targetValue);


            var foo = CoinFactory.GenerateTestCoin(arr.ToList());

            var s = new Stopwatch();

            s.Start();

            var cnnt = 0;
            
            Combinations.BruteForceCombinations(h =>
            {
                cnnt++;
            }, 32, 16, 8, 4,2,1,targetValue);

            Console.WriteLine(cnnt);
            
            //Combinations.BruteForceCombinations((x) => cnnt++, foo, targetValue);
                
            s.Stop();
            
            Console.WriteLine(s.ElapsedMilliseconds);

            var g = Combinations.GenerateCoinsAndMultiples(foo, targetValue).ToArray();
            
            s = new Stopwatch();

            s.Start();

            int cnt = 0;
            Combinations.QuickCalculateCombinations2((i) =>
            {
                cnt++;
            },targetValue, g[0], g[1], g[2], g[3], g[4], g[5]);
         
            s.Stop();
            
            Console.WriteLine(s.ElapsedMilliseconds);


            //CheckEquals(res1, res2);
            
            Console.WriteLine("total nodes:" + cnt);
           
            Console.WriteLine("finisheed");

            Console.ReadLine();

        }
    }
}