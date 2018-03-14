using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    /// <summary>
    /// combination results are extremely regular. 
    /// a simple encoding whereby you keep 
    /// track of d-gaps is a very efficient 
    /// way to store results
    /// format is ((#,count,gap),(#,count,gap)) etc., 
    /// </summary>
    public class CoinCompressor 
    {
        private List<int> values = new List<int>();

        private int lastValue;

        // note positive - we expect values to decrease 
        private int lastGap = int.MaxValue;
        private int count = 0;
        private bool init = false;
        public void Add(int r)
        {
            if (!init)
            {
                lastValue = r;
                values.Add(r);
                init = true;
            }
            else
            {               
                if (r-lastValue!= lastGap)
                {
                    if (lastGap == int.MaxValue)
                    {
                        values.Add(1);
                        values.Add(r-lastValue); 
                        lastGap = r-lastValue;
                    }
                    else
                    {
                        values.Add(r);
                        lastGap = int.MaxValue;
                    }
                }
                else
                    // increment our count 
                    values[values.Count - 2]++;

                lastValue = r;

            }

        }

        public IList<int> Finished()
        {
            if (lastGap == int.MaxValue && init) // we finished on a singleton
                values.Add(0); // no further numbers 
            return values;
        }

        public IList<int> GetValues()
        {
            return values;
        }

    }
}
