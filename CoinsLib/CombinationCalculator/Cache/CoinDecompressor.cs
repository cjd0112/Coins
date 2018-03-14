using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    /// <summary>
    /// combination results are extremely regular. 
    /// - always descending - 
    /// a simple encoding whereby you keep 
    /// track of d-gaps is a very efficient 
    /// way to store results
    /// </summary>
    public class CoinDecompressor : IEnumerator<int>,IEnumerable<int>
    {
        private IList<int> list;
        public CoinDecompressor(IList<int> compressedList)
        {
            list = compressedList;
        }

        public CoinDecompressor(CoinCompressor cc)
        {
            list = cc.GetValues();
        }

        private int cnt = 0;
        private int gap = 0;
        private int gap_cnt = 0;
        public bool MoveNext()
        {
            if (gap_cnt-- > 0)
            {
                Current += gap;
                return true;
            }
            else
            {
                if (cnt == list.Count())
                    return false;
                Current = list[cnt++];
                gap_cnt = list[cnt++];
                if (gap_cnt != 0)
                    gap = list[cnt++];
                return true;
            }
        }

        public void Reset()
        {
            gap = 0;
            cnt = 0;
            gap_cnt = 0;
            Current = 0;
        }

        public int Current { get; private set; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new CoinDecompressor(list);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
