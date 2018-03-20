using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsLib.CombinationCalculator.Cache;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class WaysToShareTests
    {
        [Test]
        public void TestOddNoCoinsThrows()
        {
            Assert.Throws<Exception>(() => { new ShareCoinsEvenly(100).WaysToShare(new Stack2(1, 2, 6));});
        }

        [Test]
        public void TestSingleIsOne()
        {
            Assert.AreEqual(1, new ShareCoinsEvenly(100).WaysToShare(new Stack2(10)));
        }

        [Test]
        public void TestTwoEvenCombinations()
        {
            Assert.AreEqual(2, new ShareCoinsEvenly(100).WaysToShare(new Stack2(1,1)));
        }

        [Test]
        public void TestFourEvenCombinations()
        {
            Assert.AreEqual(6, new ShareCoinsEvenly(100).WaysToShare(new Stack2( 1, 1,1,1 )));

        }

        [Test]
        public void TestSixEvenCombinations()
        {
            Assert.AreEqual(20, new ShareCoinsEvenly(100).WaysToShare(new Stack2( 1, 1, 1, 1,1,1 )));

        }

        [Test]
        public void TestTwoByTwoCombinations()
        {
            Assert.AreEqual(3, new ShareCoinsEvenly(100).WaysToShare(new Stack2( 2, 2)));
        }

        [Test]
        public void TestThreeByThreeCombinations()
        {
            Assert.AreEqual(4, new ShareCoinsEvenly(100).WaysToShare(new Stack2( 3, 3 )));
        }

        [Test]
        public void TestFourByFourCombinations()
        {
            Assert.AreEqual(5, new ShareCoinsEvenly(100).WaysToShare(new Stack2( 4, 4 )));
        }

        [Test]
        public void TestTwoByFourCombinations()
        {
            Assert.AreEqual(3, new ShareCoinsEvenly(100).WaysToShare(new Stack2( 2, 4 )));
        }

        [Test]
        public void TestFourByTwoCombinations()
        {
            Assert.AreEqual(3, new ShareCoinsEvenly(100).WaysToShare(new Stack2( 4,2 )));
        }

        [Test]
        public void TestTwoSingletonsAndLongChain()
        {
            Assert.AreEqual(3, new ShareCoinsEvenly(100).WaysToShare(new Stack2( 1,1,6 )));
        }
    }
}
