using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using CSharpFastPFOR;
using CSharpFastPFOR.Differential;

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
        private const int buffer_end_marker = -9999;
        private List<Int32> liveCache = new List<Int32>();
        private int liveBufferMax = 128;
        private List<int[]> compressedLists = new List<int[]>();

//        private IntCompressor ic = new IntCompressor(new FastPFOR128());
        
        private IntCompressor ic = new IntCompressor();

        private List<List<Int32>> totalCoinList = new List<List<int>>();

        private List<int[]> compressedTotalCoinList = new List<int[]>();

        private bool[] containsRemainder;
        
        public CompressedIntegerCache(int maxValue)
        {
            for (int i = 0;i<maxValue;i++)
                totalCoinList.Add(new List<int>());
            
            containsRemainder = new bool[maxValue];
            
        }

        public bool ContainsRemainder(int remainder)
        {
            return containsRemainder[remainder];
        }

        public void AddCombinationsToCache(int remainder, IEnumerable<Int32> totalCoinsInCombinations)
        {
            containsRemainder[remainder] = true;

            liveCache.Add(remainder);
            liveCache.Add(totalCoinsInCombinations.Count());
            var deltas = totalCoinsInCombinations.ToArray();
            Delta.delta(deltas);
            liveCache.AddRange(deltas);
            
            if (liveCache.Count() >= liveBufferMax)
            {
                liveCache.AddRange(Enumerable.Range(0,128-liveCache.Count()%128).Select(x=>buffer_end_marker));

                compressedLists.Add(ic.compress(liveCache.ToArray()));
                liveCache.Clear();
            }
        }

        public void AddTotalCoinsToCache(int remainder, int totalCoins)
        {
            totalCoinList[remainder].Add(totalCoins);
        }

        public void OnEndProcessing(int value)
        {
            // if anything left in our live cache compress it 
            if (liveCache.Any())
            {
                liveCache.AddRange(Enumerable.Range(0,128-liveCache.Count()%128).Select(x=>buffer_end_marker));
                var l2 = ic.compress(liveCache.ToArray());
                if (l2 == null)
                    throw new Exception("Error with compression - likely buffer is not equal to minimum size - 128");
                compressedLists.Add(l2);
                liveCache.Clear();                
            }

            if (totalCoinList.Any(x => x.Any()))
            {
                var l = new List<int>();
                // add entry for the value this cache corresponds to
                l.Add(value);

                for (int i = 0; i < totalCoinList.Count(); i++)
                {
                    if (totalCoinList[i].Any())
                    {
                        // add the remainder which is lookup to our totalCoins list
                        l.Add(i);
                        var p = totalCoinList[i].ToArray();
                        // add how many entries we've got
                        l.Add(p.Length);
                        // add the actual entries for this remainder in delta format for good compression
                        Delta.delta(p);
                        l.AddRange(p);
                    }
                }
                // make sure we are aligned to compression algo boundary. 
                l.AddRange(Enumerable.Range(0, 128 - l.Count() % 128).Select(x => buffer_end_marker));

                // compress ... 
                compressedTotalCoinList.Add(ic.compress(l.ToArray()));
                // clear our coin list ready for next value
                totalCoinList.ForEach(x => x.Clear());
            }


        }

        Dictionary<int, Dictionary<int, List<int>>> decompressValueAndTotalCoinsMap()
        {
            var map = new Dictionary<int, Dictionary<int,List<int>>>();
            foreach (var c in compressedTotalCoinList)
            {
                var uncompressed = ic.uncompress(c);
                int pos = 0;
                Dictionary<int,List<int>> thisMap = null;
                List<int> thisList = null;
                int thisCount = 0;
                int delta = 0;
                foreach (var i in uncompressed)
                {
                    if (i == buffer_end_marker)
                        break;
                    if (pos == 0)
                    {
                        // add value entry
                        thisMap = map[i] = new Dictionary<int,List<int>>();
                        pos++;
                    }
                    else if (pos == 1)
                    {
                        // add the remainder entry 
                        thisList = thisMap[i] = new List<int>();
                        pos++;
                    }
                    else if (pos == 2)
                    {
                        thisCount = i;
                        pos++;
                    }
                    else if (pos == 3)
                    {
                        // our delta start;
                        thisList.Add(i);
                        delta = i;
                        pos++;
                    }
                    else if (pos - 3 < thisCount)
                    {
                        thisList.Add(delta + i);
                        delta = delta + i;
                        pos++;
                    }
                    if (pos - 3 == thisCount)
                    {
                        pos = 1;
                    }
                }
            }
            compressedTotalCoinList.Clear();
            return map;
        }

        public void StreamCombinationsFromCache(
            Action<(int value, IEnumerable<Int32> combinations, IEnumerable<Int32> totalCoins,bool finished)> next)
        {
            var compressedCoinsMap = decompressValueAndTotalCoinsMap();
            foreach (var c in compressedLists)
            {
                var unCompressed = ic.uncompress(c);
                int pos = 0;
                int remainder = 0;
                int count = 0;
                int delta = 0;
                Int32[] uncompressed = null;
                foreach (var i in unCompressed)
                {
                    if (i == buffer_end_marker)
                        break;  // come to end of buffer;
                    
                    if (pos == 0)
                    {
                        remainder = i;
                        pos++;
                    }
                    else if (pos == 1)
                    {
                        count = i;
                        uncompressed = new int[count];
                        pos++;
                    }
                    else if (pos == 2)
                    {
                        // our delta start;
                        uncompressed[0] = i;
                        delta = i;
                        pos++;

                    }
                    else if (pos - 2 < count)
                    {
                        uncompressed[pos - 2] = delta + i;
                        delta = delta + i;
                        pos++;
                    }
                    if (pos -2 == count)
                    {
                        foreach (var value_key in compressedCoinsMap.Keys)
                        {
                            var remainderMap = compressedCoinsMap[value_key];
                            if (remainderMap.ContainsKey(remainder))
                            {
                                next((value_key, uncompressed, remainderMap[remainder], false));
                            }
                        }
                        pos = 0; // start again. 
                    }
                    
                }
            }
            next((-1,null,null,true));
        }
    }
}