using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsLib.CombinationCalculator;
using CoinsLib.Util;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class QuickCoinCalculatorTests
    {
        [Test]
        public void QC_Test_1_1_1()
        {
            var foo = QuickCoinsCalculator.Calculate((1, 6), (1, 2), (1, 1));
            Assert.IsTrue(foo.Count() == 1);
            Assert.IsTrue(foo.First() == 3);


        }

        [Test]
        public void QC_Test_1_2_3()
        {
            var foo = QuickCoinsCalculator.Calculate((1, 6), (2, 2), (3, 1)).ToArray();
            Assert.IsTrue(foo.Count() == 2);
            Assert.IsTrue(foo.Contains(4));
            Assert.IsTrue(foo.Contains(5));
        }

        [Test]
        public void QC_Test_1_1_2()
        {
            var foo = QuickCoinsCalculator.Calculate((1, 6), (1, 2), (2, 1)).ToArray();
            Assert.IsTrue(foo.Count() == 1);
            Assert.IsTrue(foo.Contains(4));
        }

        [Test]
        public void QC_Test_2_4_8()
        {
            var foo = QuickCoinsCalculator.Calculate((1, 4), (1, 2), (2, 1)).ToArray();
            Assert.IsTrue(foo.Count() == 1);
            Assert.IsTrue(foo.Contains(4));
        }

        [Test]
        public void BruteForceCalculation1()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] {2, 1})), 3);

            Assert.IsTrue(foo.Count() == 1);
            Assert.IsTrue(foo.ToList().Head() == 2);

        }

        [Test]
        public void BruteForceCalculation2()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 2, 1 })), 4);

            Assert.IsTrue(foo.Count() == 1);
            Assert.IsTrue(foo.ToList().Head() == 3);

        }

        [Test]
        public void BruteForceCalculation3()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 2, 1 })), 5);

            Assert.IsTrue(foo.Count() == 2);
            Assert.IsTrue(foo.Contains(3));
            Assert.IsTrue(foo.Contains(4));

        }

        [Test]
        public void BruteForceCalculation4()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 4,2,1 })), 6);

            Assert.IsTrue(foo.Count() == 0);

        }

        [Test]
        public void BruteForceCalculation5()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 4, 2, 1 })), 7);

            Assert.IsTrue(foo.Count() == 1);
            Assert.IsTrue(foo.ToList()[0] == 3);


        }

        [Test]
        public void BruteForceCalculation6()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 4, 2, 1 })), 8);

            Assert.IsTrue(foo.Count() == 1);
            Assert.IsTrue(foo.ToList()[0] == 4);


        }

        [Test]
        public void BruteForceCalculation7()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 4, 2, 1 })), 9);

            Assert.IsTrue(foo.Count() == 2);
            Assert.IsTrue(foo.Contains(4));
            Assert.IsTrue(foo.Contains(5));
        }

        [Test]
        public void BruteForceCalculation8()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 4, 2, 1 })), 11);

            Assert.IsTrue(foo.Count() == 4);
            Assert.IsTrue(foo.Contains(4));
            Assert.IsTrue(foo.Contains(5));
            Assert.IsTrue(foo.Contains(6));
            Assert.IsTrue(foo.Contains(7));

        }

        void AssertListsAreEquivalent(IEnumerable<Int32> a, IEnumerable<Int32> b)
        {
            Assert.IsTrue(a.Count() == b.Count());

            foreach (var c in a)
            {
                Assert.IsTrue(b.Contains(c));
            }

            foreach (var c in b)
            {
                Assert.IsTrue(a.Contains(c));
            }
        }

        [Test]
        public void BruteForceMatchesQuickSolution()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 8, 4, 2 })), 14);

            var foo2 = QuickCoinsCalculator.Calculate((1, 8), (1, 4), (1, 2)).ToArray();

            AssertListsAreEquivalent(foo, foo2);

        }

        [Test]
        public void BruteForceMatchesQuickSolution1()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 8, 4, 2 })), 16);

            var foo2 = QuickCoinsCalculator.Calculate((1, 8), (1, 4), (2, 2)).ToArray();

            AssertListsAreEquivalent(foo, foo2);

        }

        [Test]
        public void BruteForceMatchesQuickSolution2()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 8, 4, 2 })), 18);

            var foo2 = QuickCoinsCalculator.Calculate((1, 8), (2, 4), (3, 2)).ToArray();

            AssertListsAreEquivalent(foo, foo2);

        }

        [Test]
        public void BruteForceMatchesQuickSolution3()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 8, 4, 2 })), 20);

            var foo2 = QuickCoinsCalculator.Calculate((1, 8), (2, 4), (4, 2)).ToArray();

            AssertListsAreEquivalent(foo, foo2);

        }

        [Test]
        public void BruteForceMatchesQuickSolution4()
        {
            var foo = QuickCoinsCalculator.BruteForceCombinations(
                CoinFactory.GenerateTestCoin(new List<int>(new[] { 8, 4, 2 })), 22);

            var foo2 = QuickCoinsCalculator.Calculate((2, 8), (3, 4), (5, 2)).ToArray();

            AssertListsAreEquivalent(foo, foo2);

        }


    }
}
