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
    /// we can work in base units and do the sums
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
    public class CoinTriangle : IEnumerable<(int triangleRow,int number)>,IEnumerator<(int triangleRow,int number)>
    {
        private readonly int topDecrement = 0;
        private readonly int bottomDecrement = 0;
        private readonly int heightFirstColumn = 0;
        private int currentRow= 0;
        private readonly int endRow = 0;
        private int currentNumberCombinations = 0;
        private int iterations = 0;
        private readonly int convertBacktoCoins;
        public CoinTriangle(int maxCoins, int heightFirstColumn, int width, int topDecrement, int bottomDecrement)
        {
            this.heightFirstColumn = heightFirstColumn;
            this.topDecrement = topDecrement;
            this.bottomDecrement = bottomDecrement;
            this.endRow = - ((width -1)* bottomDecrement);
            this.currentRow = heightFirstColumn;
            this.convertBacktoCoins = maxCoins - heightFirstColumn;
            Debug.Assert(endRow <= 0);
            Debug.Assert(width >= 1);
            Debug.Assert(topDecrement > bottomDecrement);
            
        }

        public IEnumerator<(int triangleRow, int number)> GetEnumerator()
        {
            return this;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool MoveNext()
        {
            bool ret = currentRow >  endRow;
            if (ret)
            {
                if (currentRow > 0)
                {
                    if (iterations % topDecrement == 0)
                        currentNumberCombinations++;
                }
                else
                {
                    if (iterations % topDecrement == 0)
                        currentNumberCombinations++;

                    if (iterations % bottomDecrement == 0)
                        currentNumberCombinations--;
                }
                iterations++;
                currentRow--;
            }

            return ret;
        }

        public void Reset()
        {
            currentRow = heightFirstColumn;
        }

        public (int triangleRow, int number) Current => ((currentRow+1) + convertBacktoCoins, currentNumberCombinations);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }
}
