using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class ModOptimiser
    {
        [Test]
        public void TestMod2()
        {
            Assert.IsTrue(455 % 16 == (455 & (16 - 1)));

        }
    }
}
