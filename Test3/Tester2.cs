using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml.Schema;
using CoinsLib.CombinationCalculator;
using CoinsLib.CombinationCalculator.Cache;
using CoinsLib.CombinationCalculator.CalculationForest;
using CoinsLib.CombinationCalculator.Underlying;

namespace Test3
{
    public class Tester2
    {
        void Assert(Func<bool> n)
        {
            if (!n())
            {
                Console.WriteLine("test failed ... ");
            }
        }

        public void Test1()
        {
            var foo = new CompressedIntegerCache(1000);
            
            
            foo.AddCombinationsToCache(64, Enumerable.Range(1,100));
            foo.AddTotalCoinsToCache(64,12);
            foo.AddTotalCoinsToCache(64,13);
            foo.AddCombinationsToCache(65, Enumerable.Range(1,100));
            foo.AddTotalCoinsToCache(65,12);
            foo.AddTotalCoinsToCache(65,13);

            foo.AddCombinationsToCache(66, Enumerable.Range(1,100));
            foo.AddTotalCoinsToCache(66,99);

            int cnt = 0;
            foo.StreamCombinationsFromCache((next =>
            {
                if (next.finished == false)
                {
                    Assert(() => next.combinations.Count() == 100);
                    Assert(() => next.combinations.First() == 1);
                    Assert(() => next.combinations.Last() == 100);
                }

                if (cnt == 0 || cnt == 1)
                {
                    Assert(()=>next.totalCoins.Count() == 2);
                    Assert(()=>next.totalCoins.First() == 12);
                    Assert(()=>next.totalCoins.Last() == 13);
                    Assert(() => next.finished == false);

                }
                else if (cnt == 2)
                {
                    Assert(()=>next.totalCoins.Count() == 1);
                    Assert(()=>next.totalCoins.First() == 99);                    
                    Assert(() => next.finished == false);

                }
                else if (cnt == 3)
                {
                    Assert(() => next.finished == true);
                }
                cnt++;

            }));

        }
        
  
        
        
        public void RunTests(String oneTest = "")
        {
            foreach (var c in typeof(Tester2).GetMethods().Where(x => x.Name.StartsWith("Test")))
            {
                if (oneTest == "" || c.Name == oneTest)
                {
                    Console.WriteLine("Running " + c.Name);
                    c.Invoke(this, new object[] { });
                }
            }
            
            Console.WriteLine("tests finished");
        }

    }
}