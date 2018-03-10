using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    /// <summary>
    /// hides the messiness of the CalculationNode worker
    /// let our caller define the triangle parameters
    /// we can work in 'coin-space' and do the sums
    /// we are provided the first (right) triangle
    /// |
    /// |
    /// | _______
    /// which is the height of first column and the width
    /// we are given how the bottom and top slopes down so can calculate
    /// new downward sloping- triange:
    ///  X 
    /// | X  
    /// |  X 
    /// |   X
    /// |    X
    /// |IIIIIX
    ///     ___X
    ///         _X
    ///           
    /// 
    /// That gives us totalHeight 
    /// 
    /// Inflection point is Row = 0 (I in the diagram above)
    /// and is where we stop incrementing no-combinations and start decrementing
    /// 
    /// </summary>
    public class CoinTriangle : IEnumerable<(int coins,int number)>,IEnumerator<(int coins,int number)>
    {
        private readonly int convertToCoins = 0;
        private readonly int topDecrement = 0;
        private readonly int bottomDecrement = 0;
        private readonly int heightFirstColumn = 0;
        private int currentRow= 0;
        private readonly int endRow = 0;
        private int currentNumberCombinations = 1;
        private int iterations = 0;
        public CoinTriangle(int maxCoins, int heightFirstColumn, int width, int topDecrement, int bottomDecrement)
        {
            this.heightFirstColumn = heightFirstColumn;
            this.topDecrement = topDecrement;
            this.bottomDecrement = bottomDecrement;
            this.endRow = heightFirstColumn - (width * bottomDecrement);
            this.currentRow = heightFirstColumn;
            this.convertToCoins = maxCoins - heightFirstColumn; // add convertToCoins to go back to coin-units.. 

            Debug.Assert(endRow <= 0);
            Debug.Assert(convertToCoins> 0);
            Debug.Assert(width >= 1);
            Debug.Assert(maxCoins >0);
            Debug.Assert(topDecrement > bottomDecrement);
            
        }

        public IEnumerator<(int coins, int number)> GetEnumerator()
        {
            return this;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            bool ret = currentRow-- >  endRow;
            if (ret)
            {
                iterations++;
                if (currentRow >= 0)
                {
                    if (iterations % topDecrement == 0)
                        currentNumberCombinations++;
                }
                else
                {
                    if (iterations % bottomDecrement == 0)
                        currentNumberCombinations--;
                }
            }

            return ret;
        }

        public void Reset()
        {
            currentRow = heightFirstColumn;
        }

        public (int coins, int number) Current => (currentRow + convertToCoins, currentNumberCombinations);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
