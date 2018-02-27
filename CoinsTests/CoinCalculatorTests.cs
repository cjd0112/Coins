using System.Collections.Generic;
using System.Linq;
using CoinsLib.Coins;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class CoinCalculatorTests
    {
        IEnumerable<RuntimeCoin> Gen(int val)
        {
            return CoinCalculator.GenerateAllCombinationsForValue(val, CoinFactory.GenerateCoinStatic());
        }


        [Test]
        public void CC_Returns_1_Where_Value_Is_1()
        {
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(1).First()).Sum() == 1);
        }

        [Test]
        public void CC_Returns_0_Where_Value_Is_0()
        {
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(0).First()).Sum() == 0);
        }

        [Test]
        public void CC_Returns_2_Combos_Totalling_3_Coins_Where_Value_Is_2()
        {
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(2).First()).Count()== 2);
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(2).First()).Sum() == 3);
        }

        [Test]
        public void CC_Returns_2_Combos_Totalling_5_Coins_Where_Value_Is_3()
        {
            /*
             *  .5d,.5d,.5d
             *  1d,.5d
             */
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(3).First()).Count() == 2);
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(3).First()).Sum() == 5);
        }

        [Test]
        public void CC_Returns_3_Combos_Totalling_9_Coins_Where_Value_Is_4()
        {
            /*
             *  .5d,.5d,.5d,.5d,
             *  1d,.5d,.5d
             *  1d,1d
             *  
             */
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(4).First()).Count() == 3);
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(4).First()).Sum() == 9);
        }

        [Test]
        public void CC_Returns_3_Combos_Totalling_12_Coins_Where_Value_Is_5()
        {
            /*
             *  .5d,.5d,.5d,.5d,.5d,
             *  1d,.5d,.5d,5d
             *  1d,1d,.5d
             *  
             */
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(5).First()).Count() == 3);
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(5).First()).Sum() == 12);
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
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(6).First()).Count() == 5);
            Assert.IsTrue(CoinCalculator.TotalCoinsForEachCombination(Gen(6).First()).Sum() == 19);
        }

        [Test]
        public void CC_Returns_77_Where_Value_Is_10d()
        {
            Assert.IsTrue(CoinCalculator.CalculateTotalWaysToShare(20, CoinFactory.GenerateCoinStatic()) == 77);
        }

        [Test]
        public void CC_Returns_141_Where_Value_Is_12d()
        {
            Assert.IsTrue(CoinCalculator.CalculateTotalWaysToShare(24, CoinFactory.GenerateCoinStatic()) == 141);
        }

        [Test]
        public void CC_Returns_2377_Where_Value_Is_24d()
        {
            Assert.IsTrue(CoinCalculator.CalculateTotalWaysToShare(48, CoinFactory.GenerateCoinStatic()) == 2377);
        }



    }
}