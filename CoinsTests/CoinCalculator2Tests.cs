using System.Collections.Generic;
using System.Linq;
using CoinsLib.Coins;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class CoinCalculator2Tests
    {
        IEnumerable<int> Gen(int val)
        {
            List<int> leafNodes = new List<int>();
            CoinCalculator2.GenerateAllCombinationsForValueKeepingTrackOfTotalCoinsAndAddingLeafNodesToList(val, 0,CoinFactory.GenerateCoinStatic(),leafNodes);
            return leafNodes;
        }


        [Test]
        public void CC2_Returns_1_Where_Value_Is_1()
        {
            Assert.IsTrue(Gen(1).Sum() == 1);
        }

        [Test]
        public void CC2_Returns_0_Where_Value_Is_0()
        {
            Assert.IsTrue(Gen(0).Sum() == 0);
        }

        [Test]
        public void CC2_Returns_2_Combos_Totalling_3_Coins_Where_Value_Is_2()
        {
            Assert.IsTrue(Gen(2).Count() == 2);
            Assert.IsTrue(Gen(2).Sum() == 3);
        }

        [Test]
        public void CC2_Returns_2_Combos_Totalling_5_Coins_Where_Value_Is_3()
        {
            /*
             *  .5d,.5d,.5d
             *  1d,.5d
             */
            Assert.IsTrue(Gen(3).Count() == 2);
            Assert.IsTrue(Gen(3).Sum() == 5);
        }


        [Test]
        public void CC2_Returns_3_Combos_Totalling_9_Coins_Where_Value_Is_4()
        {
            /*
             *  .5d,.5d,.5d,.5d,
             *  1d,.5d,.5d
             *  1d,1d
             *  
             */
            Assert.IsTrue(Gen(4).Count() == 3);
            Assert.IsTrue(Gen(4).Sum() == 9);
        }

        [Test]
        public void CC2_Returns_3_Combos_Totalling_12_Coins_Where_Value_Is_5()
        {
            /*
             *  .5d,.5d,.5d,.5d,.5d,
             *  1d,.5d,.5d,5d
             *  1d,1d,.5d
             *  
             */
            Assert.IsTrue(Gen(5).Count() == 3);
            Assert.IsTrue(Gen(5).Sum() == 12);
        }

        [Test]
        public void CC_Returns_4_Combos_Totalling_X_Coins_Where_Value_Is_6()
        {
            /*
            *  .5d,.5d,.5d,.5d,.5d,.5d
            *  1d,.5d,.5d,5d,.5d
            *  1d,1d,.5d,.5d
             * 1d,1d,1d
             * 3p
            *  
            */
            Assert.IsTrue(Gen(6).Count() == 5);
            Assert.IsTrue(Gen(6).Sum() == 19);
        }

        [Test]
        public void CC2_Returns_77_Where_Value_Is_10d()
        {
            Assert.IsTrue(CoinCalculator2.CalculateTotalWaysToShare(20, CoinFactory.GenerateCoinStatic()) == 77);
        }

        [Test]
        public void CC2_Returns_141_Where_Value_Is_12d()
        {
            Assert.IsTrue(CoinCalculator2.CalculateTotalWaysToShare(24, CoinFactory.GenerateCoinStatic()) == 141);
        }

        [Test]
        public void CC2_Returns_2377_Where_Value_Is_24d()
        {
            Assert.IsTrue(CoinCalculator2.CalculateTotalWaysToShare(48, CoinFactory.GenerateCoinStatic()) == 2377);
        }



    }
}