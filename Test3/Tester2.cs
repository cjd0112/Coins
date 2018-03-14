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