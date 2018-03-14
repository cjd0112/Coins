using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;

namespace CoinsLib.CombinationCalculator.Cache
{
    /// <summary>
    /// keeps buffer of totalCoinsInCombinations list
    /// which is list of: 
    /// Remainder (int), (count of combinations), (combinations)
    /// ... repeated for every remainder value. 
    /// and periodically compresses list to save memory. 
    /// keeps track of the 'totalCoins' list 
    /// when asked to 'streamResults' it 
    /// will return the totalCoinsInCombinations list
    /// 
    /// Idea is that the underlying Combinations list is same
    /// for each 'totalCoins' so when we stream them back 
    /// the client can easily total up the actual coins 
    /// by adding 'totalCoins' to each integer
    /// </summary>
    public class CompressedIntegerCache
    {
        List<CoinCompressor> compressors = new List<CoinCompressor>();
        
        public CompressedIntegerCache(int maxValue)
        {
            for (int i = 0;i<maxValue;i++)
                compressors.Add(null); 
                        
        }

        public bool ContainsRemainder(int remainder)
        {
            return compressors[remainder] != null;
        }


        public void AddCombinationsToCache(int remainder, CoinCompressor c)
        {
            compressors[remainder] = c;
        }

        public CoinCompressor GetCompressor(int remainder)
        {
            return compressors[remainder];
        }
    }
}