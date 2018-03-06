using System;
using System.Collections.Generic;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /// <summary>
    /// Single factory to define and retrieve the coin static structures 
    /// and to generate Test Coins based on different Units
    /// for testing. 
    /// </summary>
    public class MagicPurse
    {
        /// <summary>
        /// starting point 
        /// </summary>
        /// <returns>standard list of coins</returns>
        public static Coin GenerateCoinStatic()
        {

            return new Coin(54,"HC",new Coin(48,"FL",new Coin(24,"SH",new Coin(12, "6d", new Coin(6, "3d", new Coin(2, "1d", new Coin(1, ".5d", null)))))));
        }

        public static Coin GenerateTestCoin(List<Int32> factors)
        {
            if (factors.Count == 1)
            {
                return new Coin(factors.Head(),"dontcare",null);
            }
            else
            {
                return new Coin(factors.Head(),"dont care",GenerateTestCoin(factors.Tail()));
            }

            
        }
    }}