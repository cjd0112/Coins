using System;
using CoinsLib.LSD;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class LSDParserTests
    {
        [Test]
        public void LSD_Convert_3_Way()
        {
            var l = new LSDParser("1/2/2.5");
            Assert.IsTrue(l.L == 1 );
            Assert.IsTrue(l.S == 2);
            Assert.IsTrue(l.D == 2);
            Assert.IsTrue(l.HD == 1);
            Assert.IsTrue(l.ValueInHalfD() == 1 + 2*2 + 2*2*12 + 1*2*12*20);

        }

        [Test]
        public void LSD_Throws_On_Invalid_CharIn_L()
        {
            Assert.Throws<ArgumentException>(()=>new LSDParser("a/2/2.5"));
        }
        [Test]
        public void LSD_Throws_On_Invalid_CharIn_S()
        {
            Assert.Throws<ArgumentException>(() => new LSDParser("1/c/2.5"));
        }

        [Test]
        public void LSD_Throws_On_Invalid_CharIn_D()
        {
            Assert.Throws<ArgumentException>(() => new LSDParser("1/2/z"));
        }

        [Test]
        public void LSD_Accepts_d_In_D()
        {
            Assert.DoesNotThrow(() => new LSDParser("1/2/2.5d"));
        }

        [Test]
        public void LSD_Accepts_hyphen_In_L()
        {
            Assert.DoesNotThrow(() => new LSDParser("-/2/2.5d"));
        }

        [Test]
        public void LSD_Accepts_hyphen_In_S()
        {
            Assert.DoesNotThrow(() => new LSDParser("1/-/2.5d"));
        }

        [Test]
        public void LSD_Accepts_hyphen_In_S_and_D()
        {
            Assert.DoesNotThrow(() => new LSDParser("1/-/-"));
        }

        [Test]
        public void LSD_Accepts_One_Slash_Phrase()
        {
            Assert.DoesNotThrow(() => new LSDParser("-/1d"));
            Assert.IsTrue(new LSDParser("-/1d").ValueInHalfD() == 2);
        }

        [Test]
        public void LSD_Accepts_One_Slash_Phrase_WithS()
        {
            Assert.DoesNotThrow(() => new LSDParser("2/1d"));
            Assert.IsTrue(new LSDParser("2/1d").ValueInHalfD() == 50);
        }

        [Test]
        public void LSD_Accepts_No_Slash_Phrase()
        {
            Assert.DoesNotThrow(() => new LSDParser("3.5d"));
            Assert.IsTrue(new LSDParser("3.5d").ValueInHalfD() == 7);
        }

        [Test]
        public void LSD_Throws_On_S_Overflow()
        {
            Assert.Throws<ArgumentException>(() => new LSDParser("20/1d"));
        }

        [Test]
        public void LSD_Throws_On_D_Overflow()
        {
            Assert.Throws<ArgumentException>(() => new LSDParser("12/12d"));
        }

        [Test]
        public void LSD_Throws_On_L_Negative()
        {
            Assert.Throws<ArgumentException>(() => new LSDParser("-12/12/11d"));
        }

        [Test]
        public void LSD_Throws_On_S_Negative()
        {
            Assert.Throws<ArgumentException>(() => new LSDParser("12/-1/11d"));
        }

        [Test]
        public void LSD_Throws_On_D_Negative()
        {
            Assert.Throws<ArgumentException>(() => new LSDParser("12/2/-11d"));
        }










    }
}