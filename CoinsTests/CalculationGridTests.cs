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

        /*
         * Value,14
4,2,1,TotalCoins
1,1,8,10
1,2,6,9
1,3,4,8
1,4,2,7
2,1,4,7
2,2,2,6
         */
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
                        Assert.IsTrue( state.CoinsAndCombos.Count() == 5);
                        Assert.IsTrue( state.CoinsAndCombos.Contains((10, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((9, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((8, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((7, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((6, 1)));
                        comboHit1 = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(comboHit1);

        }

        /*
      * Value,15
4,2,1,TotalCoins
1,1,9,11
1,2,7,10
1,3,5,9
1,4,3,8
1,5,1,7
2,1,5,8
2,2,3,7
2,3,1,6
3,1,1,5

      */

        [Test]
        public void Test10()
        {
            int val = 15;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 4, 2, 1 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "421", (state =>
                {
                    if (state.node.ComboKey == "421")
                    {
                        Assert.IsTrue( state.CoinsAndCombos.Count() == 7);
                        Assert.IsTrue( state.CoinsAndCombos.Contains((11, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((10, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((9, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((8, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((7, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((6, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((5, 1)));




                        comboHit1 = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue( comboHit1);

        }
        /*
         * Value,16
4,2,1,TotalCoins
1,1,10,12
1,2,8,11
1,3,6,10
1,4,4,9
1,5,2,8
2,1,6,9
2,2,4,8
2,3,2,7
3,1,2,6

         */
         [Test]
        public void Test11()
        {
            int val = 16;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 4, 2, 1 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "421", (state =>
                {
                    if (state.node.ComboKey == "421")
                    {
                        Assert.IsTrue( state.CoinsAndCombos.Count() == 7);
                        Assert.IsTrue( state.CoinsAndCombos.Contains((12, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((11, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((10, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((9, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((8, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((7, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((6, 1)));
                        comboHit1 = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue( comboHit1);

        }

        /*
         * Value,17
4,2,1,TotalCoins
1,1,11,13
1,2,9,12
1,3,7,11
1,4,5,10
1,5,3,9
1,6,1,8
2,1,7,10
2,2,5,9
2,3,3,8
2,4,1,7
3,1,3,7
3,2,1,6

         */

        [Test]
        public void Test12()
        {
            int val = 17;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 4, 2, 1 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "421", (state =>
                {
                    if (state.node.ComboKey == "421")
                    {
                        Assert.IsTrue( state.CoinsAndCombos.Count() == 8);
                        Assert.IsTrue( state.CoinsAndCombos.Contains((13, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((12, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((11, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((10, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((9, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((8, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((7, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((6, 1)));
                        comboHit1 = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue( comboHit1);

        }
        /*
         *        Value,14
        8,4,2,TotalCoins
        1,1,1,3
 
         */

        [Test]
        public void Test13_EvenRoot()
        {
            int val = 14;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 8, 4, 2 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "842", (state =>
                {
                    if (state.node.ComboKey == "842")
                    {
                        Assert.IsTrue( state.CoinsAndCombos.Count() == 1);
                        Assert.IsTrue( state.CoinsAndCombos.Contains((3, 1)));
                        comboHit1 = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue( comboHit1);

        }

        /*Value,28
8,4,2,TotalCoins
1,1,8,10
1,2,6,9
1,3,4,8
1,4,2,7
2,1,4,7
2,2,2,6
*/
        [Test]
        public void Test15()
        {
            int val = 28;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 8, 4, 2 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "842", (state =>
                {
                    if (state.node.ComboKey == "842")
                    {
                        Assert.IsTrue( state.CoinsAndCombos.Count() == 5);
                        Assert.IsTrue( state.CoinsAndCombos.Contains((10, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((9, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((8, 1)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((7, 2)));
                        Assert.IsTrue( state.CoinsAndCombos.Contains((6, 1)));

                        comboHit1 = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue( comboHit1);

        }

        /*
    Value,15
 8,4,2,1,TotalCoins
 1,1,1,1,4


         */

        [Test]
        public void Test16()
        {
            int val = 15;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            bool comboHit2 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 8, 4, 2, 1 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "8421" || comboKey == "841", (state =>
                {
                    if (state.node.ComboKey == "8421")
                    {
                        Assert.IsTrue( state.CoinsAndCombos.Count() == 1);
                        Assert.IsTrue( state.CoinsAndCombos.Contains((4, 1)));

                        comboHit1 = true;
                    }

                    if (state.node.ComboKey == "841")
                    {
                        Assert.IsTrue(state.CoinsAndCombos.Count() == 1);
                        Assert.IsTrue(state.CoinsAndCombos.Contains((5,1)));
                        comboHit2 = true;
                    }
                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue( comboHit1 && comboHit2);

        }

        [Test]
        public void Test17()
        {
            int val = 9;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 6, 2, 1 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "621", (state =>
                {
                    if (state.node.ComboKey == "621")
                    {
                        Assert.IsTrue(state.CoinsAndCombos.Count() == 1);
                        Assert.IsTrue(state.CoinsAndCombos.Contains((3, 1)));

                        comboHit1 = true;
                    }

                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(comboHit1);

        }


        [Test]
        public void Test18()
        {
            int val = 21;
            Int64[] arr = new Int64[val + 1];
            bool comboHit1 = false;
            var z = new CalculationGrid(MagicPurse.GenerateTestCoin(new[] { 12, 6, 2, 1 }.ToList()).AllCombinations(),
                (string comboKey, int value) => comboKey == "12621" , (state =>
                {
                    if (state.node.ComboKey == "12621")
                    {
                        Assert.IsTrue(state.CoinsAndCombos.Count() == 1);
                        Assert.IsTrue(state.CoinsAndCombos.Contains((4, 1)));

                        comboHit1 = true;
                    }

                }));
            z.CalculateTotalCoins(arr, val);
            Assert.IsTrue(comboHit1 );

        }


    }
}
