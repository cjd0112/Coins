using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Xml.Schema;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class ShareCoinsEvenly
    {
        public ShareCoinsEvenly(int maxNumber)
        {
            GeneratePascalTriangle(maxNumber);
        }

        private int[][] triangle;
        void GeneratePascalTriangle(int height)
        {
            int previousSum(int myRow,int myColumn)
            {
                if (myRow == 0)
                    return 1;
                var previousRow = triangle[myRow - 1];
                int left=0, right = 0;
                if (myColumn > 0)
                    left = previousRow[myColumn - 1];

                if (myColumn < previousRow.Length)
                    right = previousRow[myColumn ];

                return left + right;

            }

            triangle = new int[height][];
            int size = 0;
            for (int i = 0; i < height; i++)
            {
                var thisWidth = i + 1;
                triangle[i] = new int[thisWidth];
                for (int q = 0; q < thisWidth; q++)
                {
                    triangle[i][q] = previousSum(i, q);
                }

            }
        }

        int NoCombinations(int n, int k)
        {
            if (n < 0 || n >= triangle.Length)
                throw new Exception($"Invalid input row - {n}, triangle height is {triangle.Length}");

            var row = triangle[n];
            if (k < 0 || k >= row.Length)
                throw new Exception($"Invalid input column - {k}, max row width is {row.Length}");

            return triangle[n][k];

        }


        public int WaysToShare(Stack2 coins)
        {
#if DEBUG
            if (coins.Sum() % 2 != 0)
                throw new Exception($"Unexpected sum of coins - should be even number - received {coins.Sum()}");
#endif

            // even, with only one set of coins ... one way to share - split down middle.
            if (coins.Count() == 1)
                return 1;
            
            // extract even amounts from odd

            var (even, odd) = coins.SplitEvenOdd();
            
            if (odd.Count() % 2 == 1)
                throw new Exception("Count of odd items is odd - which should not be the case");

            return MergeBlocks(MergeBlocks(even), MergeBlocks(odd));
        }

        int MergeBlocks(Stack2 foo)
        {
            int blockRowTotal = 1;
            foreach (var (left, right) in foo.GetPairs())
            {
                var thisHeight = Math.Max(left, right) + 1;

                blockRowTotal = blockRowTotal * (thisHeight + (thisHeight / 2));
            }

            return blockRowTotal;
        }

        int MergeBlocks(int left, int right)
        {
            return left * (right + (right / 2));
        }
    }
}
