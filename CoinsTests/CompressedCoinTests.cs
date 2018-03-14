using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsLib.CombinationCalculator.Cache;
using CoinsLib.Util;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class CompressedCoinTests
    {
        [Test]
        public void TestCompressedCoinSimple()
        {
            var z = new CoinCompressor();
            Enumerable.Range(1, 100).Reverse().ForEach(z.Add);

            var foo = new CoinDecompressor(z.Finished());

            Assert.IsTrue(foo.Count() == 100);
            foreach (var d in Enumerable.Range(1, 100).Reverse())
            {
                Assert.IsTrue(foo.Contains(d));
            }

        }

        [Test]
        public void TestCompressedCoinSimpleGaps()
        {
            var z = new CoinCompressor();
            Enumerable.Range(1, 100).Select(x=>x*2).Reverse().ForEach(z.Add);

            var foo = new CoinDecompressor(z.Finished());

            Assert.IsTrue(foo.Count() == 100);
            foreach (var d in Enumerable.Range(1, 100).Select(x=>x*2).Reverse())
            {
                Assert.IsTrue(foo.Contains(d));
            }

        }

        [Test]
        public void TestCompressedCoinComplexGaps()
        {
            var z = new CoinCompressor();
            z.Add(100);
            z.Add(98);
            z.Add(94);
            z.Add(92);
            z.Add(91);
            z.Add(85);
         

            var foo = new CoinDecompressor(z.Finished());

            Assert.IsTrue(foo.Count() == 6);

            Assert.IsTrue(foo.SequenceEqual(new int[] {100, 98, 94, 92, 91, 85}));

        }

        [Test]
        public void TestSingleton()
        {
            var z = new CoinCompressor();
            z.Add(100);


            var foo = new CoinDecompressor(z.Finished());

            Assert.IsTrue(foo.Count() == 1);

            Assert.IsTrue(foo.SequenceEqual(new int[] { 100}));

        }

        [Test]
        public void TestMassive()
        {
            var z = new CoinCompressor();
            Enumerable.Range(1,100000).Reverse().ForEach(z.Add);


            var foo = new CoinDecompressor(z.Finished());

            Assert.IsTrue(foo.Count() == 100000);

        }

        [Test]
        public void TestEmptyList()
        {
            var z = new CoinCompressor();
            Enumerable.Range(1, 0).Reverse().ForEach(z.Add);


            var foo = new CoinDecompressor(z.Finished());

            Assert.IsTrue(foo.Count() == 0);

        }

        [Test]
        public void TestWhereTwoValuesSame()
        {
            var z = new CoinCompressor();
            new int[]{5,5,5,4}.ForEach(z.Add);


            var foo = new CoinDecompressor(z.Finished());

            Assert.IsTrue(foo.Count() == 4);

            Assert.IsTrue(foo.SequenceEqual(new int[]{5,5,5,4}));

        }

        [Test]
        public void TestAscending()
        {
            var z = new CoinCompressor();
            new int[] { 1,2,3,4,7,6,2 }.ForEach(z.Add);


            var foo = new CoinDecompressor(z.Finished());

            Assert.IsTrue(foo.Count() == 7);

            Assert.IsTrue(foo.SequenceEqual(new int[] { 1,2,3,4,7,6,2}));

        }

    }
}
