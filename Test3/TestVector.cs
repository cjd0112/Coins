using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;

namespace Test3
{
    public class TestVector
    {

        public static void TestThis()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var z = 100;
            for (int i = 0; i < 1000000; i++)
            {
                for (int q = 0; q < 10000; q++)
                {
                    z += i + q;
                }
            }
            Console.WriteLine(sw.ElapsedMilliseconds);
        }

        public static void Vector()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 100000; i++)
            {
                var c = new List<Int32>(Enumerable.Range(1, 4).ToArray());
                var d = new List<Int32>(Enumerable.Range(10, 4).ToArray());

                var x = new List<Int32>(Enumerable.Range(1,4));
                for (int q = 0; q < 4; q++)
                {
                    x[q] = c[q] + d[q];
                }

                //var p = c + d;
            }


            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);



        }
    }
}
