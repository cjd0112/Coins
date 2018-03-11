using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoinsLib.CombinationCalculator;
using CoinsLib.CombinationCalculator.CalculationForest;
using NUnit.Framework;

namespace CoinsTest
{
    [TestFixture]
    public class CalculationGridTests
    {
        [Test]
        public void Test2()
        {
            int val = 2;
            Int64 [] arr = new Int64[val+1];
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] {2, 1}.ToList()).AllCombinations());
            var nocombinations = z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(nocombinations == 2);
            Assert.IsTrue(arr[2] == 1);  // one combo with 2 
            Assert.IsTrue(arr[1]==1); // one combo with 1
        }

        [Test]
        public void Test3()
        {
            int val = 3;
            Int64[] arr = new Int64[val + 1];
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 2, 1 }.ToList()).AllCombinations());
            var nocombinations = z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(nocombinations == 2);
            Assert.IsTrue(arr[3]== 1); 
            Assert.IsTrue(arr[2] == 1);  // one combo with 2 
            Assert.IsTrue(arr[1] == 0); // one combo with 1
        }

        [Test]
        public void Test4()
        {
            int val = 4;
            Int64[] arr = new Int64[val + 1];
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 2, 1 }.ToList()).AllCombinations());
            var nocombinations = z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(nocombinations == 3);
            Assert.IsTrue(arr[4] == 1);
            Assert.IsTrue(arr[3] == 1);
            Assert.IsTrue(arr[2] == 1);  // one combo with 2 
            Assert.IsTrue(arr[1] == 0); // one combo with 1
        }

        [Test]
        public void Test5()
        {
            int val = 5;
            Int64[] arr = new Int64[val + 1];
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 2, 1 }.ToList()).AllCombinations());
            var nocombinations = z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(nocombinations == 3);
            Assert.IsTrue(arr[5] ==1 );
            Assert.IsTrue(arr[4] == 1);
            Assert.IsTrue(arr[3] == 1);
            Assert.IsTrue(arr[2] == 0);  // one combo with 2 
            Assert.IsTrue(arr[1] == 0); // one combo with 1

        }

        [Test]
        public void TestTwoVectorsAndTriangleThatShouldNotTrigger()
        {
            int val = 5;
            Int64[] arr = new Int64[val + 1];
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] {3, 2, 1 }.ToList()).AllCombinations());
            var nocombinations = z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(nocombinations == 4);
            Assert.IsTrue(arr[5] == 1);
            Assert.IsTrue(arr[4] == 1);
            Assert.IsTrue(arr[3] == 2);
            Assert.IsTrue(arr[2] == 0);  // one combo with 2 
            Assert.IsTrue(arr[1] == 0); // one combo with 1

        }

        [Test]
        public void TestTriangleWith1ComboMatching()
        {
            int val = 7;
            Int64[] arr = new Int64[val + 1];
            bool comboHit = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] {4, 2, 1}.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "421", (state =>
                {
                    if (state.node.ComboKey == "421")
                    {
                        Assert.IsTrue(state.CoinsAndCombos.Count() == 1);
                        Assert.IsTrue(state.CoinsAndCombos.Contains((3,1)));
                        comboHit = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(comboHit);

        }

        [Test]
        public void TestTriangle3With1ComboIncrement1()
        {
            int val = 8;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            bool comboHit2 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 4, 2, 1 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "421" || comboKey=="21", (state =>
                {
                    if (state.node.ComboKey == "421")
                    {
                        Assert.IsTrue(state.CoinsAndCombos.Count() == 1);
                        Assert.IsTrue(state.CoinsAndCombos.Contains((4, 1)));
                        comboHit1 = true;
                    }

                    if (state.node.ComboKey == "21")
                    {
                        Assert.IsTrue(state.CoinsAndCombos.Count() == 3);
                        Assert.IsTrue(state.CoinsAndCombos.Contains((7, 1)));
                        Assert.IsTrue(state.CoinsAndCombos.Contains((6, 1)));
                        Assert.IsTrue(state.CoinsAndCombos.Contains((5, 1)));

                        comboHit2 = true;

                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(comboHit1 && comboHit2);

        }


        [Test]
        public void TestTriangle3With2Combo()
        {
            int val = 14;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 4, 2, 1 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "421" , (state =>
                {
                    if (state.node.ComboKey == "421")
                    {
                        Assert.IsTrue(state.CoinsAndCombos.Count() == 2);
                        Assert.IsTrue(state.CoinsAndCombos.Contains((3, 1)));
                        Assert.IsTrue(state.CoinsAndCombos.Contains((4, 1)));
                        comboHit1 = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(comboHit1);

        }


    }
}
