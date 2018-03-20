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
            if (coins.Count == 1)
                return 1;

        }


        int Process(Stack2 coins)
        {
            var c = coins.Pop();
            if (c % 2 == 0)
            {
                return ProcessEvenSameType(c, Process(coins));
            }
            else
            {
                var g = coins.Peek();
                coins.UpdateHead(g-1);
                ProcessWithBorrowedItem(c, g, Process(coins));
            }
        }

        int ProcessEvenSameType(int myCount, int rest)
        {
#if DEBUG
            if (myCount % 2 != 0)
                throw new Exception("Invalid input to process even same type");
#endif 
            if (rest == 1)
                return myCount + 1 * rest;
            else
                return myCount * rest;
        }

        int ProcessWithBorrowedItem(int c, int borrowedType, int ret)
        {
#if DEBUG
            if (c + borrowedType % 2 != 0)
                throw new Exception("Invalid input to process even same type");
#endif 
            return 


        }

    }
}
