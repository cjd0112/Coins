using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class MergedGlobalState
    {
        private CoinMap map;

        public MergedGlobalState()
        {
            map = new CoinMap();

        }

        /// <summary>
        /// adds our 'finishing' state - which is the last two levels of coin counts
        /// to existing state -> which is all the coins at each level up to the last two levels. 
        /// it checks that sum of coins is even - if not no point continuing ... 
        /// it then sorts the array in ascending order
        /// it then converts it to our 'long' representation
        /// and adds it to our global CoinMap 
        /// this means at the end we have a compressed and de-duplicated list of 
        /// each coin-count for each scenarios associated with how-many times it has been set. 
        /// So.... we should be able to run the expensive ShareCoinsEvenly.WaysToShare() function
        /// only once for each unique state and then tally up the results ... at least we hope so ... 
        /// </summary>
        /// <param name="listOfCompressedCoins"></param>
        /// <param name="arrayFinalCoinsToAddToEachCompressedCoin"></param>
        public void Merge(List<long> listOfCompressedCoins, int[] arrayFinalCoinsToAddToEachCompressedCoin)
        {
            foreach (var c in listOfCompressedCoins)
            {
                var arr = IntArrayToLong.ConvertFromLong(c);
                var newArr = new int[arr.Length + 2];
                Array.Copy(arr,newArr,arr.Length);
                newArr[arr.Length] = arrayFinalCoinsToAddToEachCompressedCoin[0];
                newArr[arr.Length + 1] = arrayFinalCoinsToAddToEachCompressedCoin[1];
                if (newArr.Sum() % 2 == 0)
                {
                    Array.Sort(newArr);
                    map.AddOrIncrementEntry(IntArrayToLong.ConvertToLong(newArr));
                }
            }
        }


        public long ProcessCoinMapAndReturnFinalResult()
        {
            long res = 0;
            foreach (var c in map.IterateValues().Select(x => (x.numberTimes, IntArrayToLong.ConvertFromLong(x.coins))))
            {
                res += c.Item1 * ShareCoinsEvenly.WaysToShare(c.Item2);
            }
            return res;

//            return map.IterateValues().
  //              Select(x => (x.numberTimes, IntArrayToLong.ConvertFromLong(x.coins))).
    //            Sum(x => x.Item1 * ShareCoinsEvenly.WaysToShare(x.Item2));
        }
    }
}
