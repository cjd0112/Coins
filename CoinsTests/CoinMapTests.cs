using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsLib.CombinationCalculator.Cache;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class CoinMapTests
    {

        [Test]
        public void TestOne()
        {
            var c = new CoinMap();

            c.AddOrIncrementEntry(100);
            c.AddOrIncrementEntry(100);
            c.AddOrIncrementEntry(100);
            c.AddOrIncrementEntry(100);

            var q = c.IterateValues().ToArray();

            Assert.IsTrue(q.Length == 1);

            Assert.IsTrue(q[0].numberTimes ==4);
            Assert.IsTrue(q[0].coins== 100);

        }

        [Test]
        public void TestTwo()
        {
            var c = new CoinMap();

            c.AddOrIncrementEntry(100);
            c.AddOrIncrementEntry(100);
            c.AddOrIncrementEntry(100);
            c.AddOrIncrementEntry(100);

            c.AddOrIncrementEntry(103);

            var q = c.IterateValues().ToArray();

            Assert.IsTrue(q.Length == 2);

            Assert.IsTrue(q[0].numberTimes == 4);
            Assert.IsTrue(q[0].coins == 100);

            Assert.IsTrue(q[1].numberTimes == 1);
            Assert.IsTrue(q[1].coins == 103);

        }

        [Test]
        public void TestThree()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var c = new CoinMap();
            for (int i = 0; i < 10000000; i++)
            {
                c.AddOrIncrementEntry(i);
            }

            sw.Stop();
            var n = sw.ElapsedMilliseconds;
        }
    }
}
