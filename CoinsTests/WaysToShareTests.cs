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
        public void TestOne()
        {
            Assert.AreEqual(1, ShareCoinsEvenly.WaysToShare(new Stack<int>(new[] {6})));

        }

        [Test]
        public void Test1x1()
        {
            Assert.AreEqual(2, ShareCoinsEvenly.WaysToShare(new Stack<int>(new[] { 1,1 })));

        }

        [Test]
        public void Test2x2()
        {
            Assert.AreEqual(3, ShareCoinsEvenly.WaysToShare(new Stack<int>(new[] { 2, 2 })));

        }

        [Test]
        public void Test3x3()
        {
            Assert.AreEqual(4, ShareCoinsEvenly.WaysToShare(new Stack<int>(new[] { 3, 3 })));

        }

        [Test]
        public void Test2x2x2()
        {
            Assert.AreEqual(7, ShareCoinsEvenly.WaysToShare(new Stack<int>(new[] { 2, 2,2 })));

        }

        [Test]
        public void Test1x1x2()
        {
            Assert.AreEqual(4, ShareCoinsEvenly.WaysToShare(new Stack<int>(new[] { 1, 1, 2 })));

        }


    }
}
