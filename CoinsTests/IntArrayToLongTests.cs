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
    public class IntArrayToLongTests
    {

        [Test]
        public void TestOne()
        {
            var l = IntArrayToLong.ConvertToLong(new uint[] {20});
            var a = IntArrayToLong.ConvertFromLong(l);
            Assert.IsTrue(a.Length == 1);
            Assert.IsTrue(a[0]== 20);
        }

        [Test]
        public void TestTwo()
        {
            var l = IntArrayToLong.ConvertToLong(new uint[] { 204,298 });
            var a = IntArrayToLong.ConvertFromLong(l);
            Assert.IsTrue(a.Length == 2);
            Assert.IsTrue(a[0] == 204);
            Assert.IsTrue(a[1] == 298);



        }

        [Test]
        public void TestThree()
        {
            var l = IntArrayToLong.ConvertToLong(new uint[] { 20, 2,98 });
            var a = IntArrayToLong.ConvertFromLong(l);
            Assert.IsTrue(a.Length == 3);
            Assert.IsTrue(a[0] == 20);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 98);



        }

        [Test]
        public void TestFour()
        {
            var l = IntArrayToLong.ConvertToLong(new uint[] { 1, 2, 27,98 });
            var a = IntArrayToLong.ConvertFromLong(l);
            Assert.IsTrue(a.Length == 4);
            Assert.IsTrue(a[0] == 1);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 27);
            Assert.IsTrue(a[3] == 98);




        }

        [Test]
        public void TestFive()
        {
            var l = IntArrayToLong.ConvertToLong(new uint[] { 14, 2, 27, 98,200 });
            var a = IntArrayToLong.ConvertFromLong(l);
            Assert.IsTrue(a.Length == 5);
            Assert.IsTrue(a[0] == 14);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 27);
            Assert.IsTrue(a[3] == 98);
            Assert.IsTrue(a[4] == 200);




        }

        [Test]
        public void TestSix()
        {
            var l = IntArrayToLong.ConvertToLong(new uint[] { 14, 2, 27, 98, 200,1000 });
            var a = IntArrayToLong.ConvertFromLong(l);
            Assert.IsTrue(a.Length == 6);
            Assert.IsTrue(a[0] == 14);
            Assert.IsTrue(a[1] == 2);
            Assert.IsTrue(a[2] == 27);
            Assert.IsTrue(a[3] == 98);
            Assert.IsTrue(a[4] == 200);
            Assert.IsTrue(a[5] == 1000);
        }

        [Test]
        public void TestOverflow()
        {
            Assert.Catch<ArgumentOutOfRangeException>(() => { IntArrayToLong.ConvertToLong(new uint[] {256, 2, 27, 98, 200, 1000}); });
        }

        [Test]
        public void TestJustUnderOverflowWorks()
        {
            var l = IntArrayToLong.ConvertToLong(new uint[] { 255, 2, 27, 98, 200, 1000 });
            var a = IntArrayToLong.ConvertFromLong(l);
            Assert.IsTrue(a[0] == 255);
        }



    }
}
