﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Xml.Schema;
using CoinsLib.Coins;

namespace CoinsLib.CombinationCalculator.Cache
{

    public static class ShareCoinsEvenly2
    {
        public static int TimesCalled = 0;
        public static int MaxWaysToShare = 0;

        private static int[][] cache;

        public  static int WaysToShare(Stack<int> coins)
        {
            if (cache == null)
            {
                cache = new int[4098][];
                for (int i = 0; i < 4098; i++)
                {
                    cache[i] = new int[4098];
                }
            }

            TimesCalled++;
#if DEBUG

            if (coins.Sum() % 2 != 0)
                throw new Exception($"Unexpected sum of coins - should be even number - received {coins.Sum()}");

            if (coins.Count > CoinFactory.MaxCoins)
                throw new Exception($"Only support {CoinFactory.MaxCoins} at present");

#endif
            var c = coins.Count;
            if (c== 1)
                return 1;

            var q = coins.ToArray();
            Array.Sort(q);

     //       if (coins.Sum() > 50)
     //       {
    //          if (values.TryGetValue(q, out var val))
    //             return val;
    //        }

            int target = coins.Sum() / 2;

            var res = 0;
            if (c == 2)
            {
                res = WaysToShare(target,q[0], q[1]);
            }
            else if (c == 3)
            {
                res = WaysToShare(target, q[0], q[1],q[2]);

            }
            else if (c == 4)
            {
                res = WaysToShare(target, q[0], q[1], q[2],q[3]);

            }
            else if (c == 5)
            {
                res = WaysToShare(target, q[0], q[1], q[2], q[3],q[4]);

            }
            else if (c == 6)
            {
                res = WaysToShare(target, q[0], q[1], q[2], q[3], q[4],q[5]);
            }
            else if (c == 7)
            {
                res = WaysToShare(target, q[0], q[1], q[2], q[3], q[4], q[5],q[6]);
            }

   //         if (coins.Sum() > 50)
     //           values[q] = res;

            MaxWaysToShare = Math.Max(MaxWaysToShare, res);
            return res;

        }

        static int WaysToShare(int target,int a1, int b1)
        {
            if (cache[a1][b1] != 0)
                return cache[a1][b1];

            int waysToShare = 0;
            for (int a = 0; a <= a1 && a <=target; a++)
            {
                if (target-a <= b1)
                    waysToShare++;
            }

            cache[a1][b1] = waysToShare;

            return waysToShare;
        }

       

        static int WaysToShare(int target, int a1, int b1,int c1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 && a <= target; a++)
            {
                if (cache[b1][c1] != 0)
                    return cache[b1][c1];

                for (int b = 0; b <= b1 && a + b <= target; b++)
                {
                    var t = target - (b + a);
                    if (t >= 0 && t <= c1)
                        waysToShare++;
                }
            }
            return waysToShare;
        }

        static int WaysToShare(int target, int a1, int b1, int c1,int d1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 && a <= target; a++)
            {
                for (int b = 0; b <= b1 && a + b <= target; b++)
                {
                    for (int c = 0; c <= c1 && a + b +c <= target; c++)
                    {
                        var t = target - (c+b + a);
                        if (t >= 0 && t <= d1)
                            waysToShare++;

                    }
                }
            }
            return waysToShare;
        }
        static int WaysToShare(int target, int a1, int b1, int c1, int d1,int e1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 && a <= target; a++)
            {
                for (int b = 0; b <= b1 && a + b <=target; b++)
                {
                    for (int c = 0; c <= c1 && a + b + c <= target; c++)
                    {
                        for (int d = 0; d <= d1 && a + b + c + d <= target; d++)
                        {
                            var t = target - (d+c + b + a);
                            if (t >= 0 && t <= e1)
                                waysToShare++;
                        }
                    }
                }
            }
            return waysToShare;
        }

        static int WaysToShare(int target, int a1, int b1, int c1, int d1, int e1,int f1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 && a <= target; a++)
            {
                for (int b = 0; b <= b1 && a + b <= target; b++)
                {
                    for (int c = 0; c <= c1 && a + b + c <=target; c++)
                    {
                        for (int d = 0; d <= d1 && a + b + c + d <= target; d++)
                        {
                            for (int e = 0; e <= e1 && a + b + c + d + e <= target; e++)
                            {
                                var t = target - (e+d + c + b + a);
                                if (t >= 0 && t <= f1)
                                    waysToShare++;

                            }
                        }
                    }
                }
            }
            return waysToShare;
        }

        static int WaysToShare(int target, int a1, int b1, int c1, int d1, int e1, int f1,int g1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 && a <= target; a++)
            {
                waysToShare += WaysToShare(a, b1, c1, d1, e1, f1, g1);
                for (int b = 0; b <= b1 && a + b <= target; b++)
                {
                    for (int c = 0; c <= c1 && a + b + c <= target; c++)
                    {
                        for (int d = 0; d <= d1 && a + b + c + d <= target; d++)
                        {
                            for (int e = 0; e <= e1 && a + b + c + d + e <= target; e++)
                            {
                                for (int f = 0; f <= f1 && a + b + c + d + e + f <= target; f++)
                                {
                                    var t = target - (f+e + d + c + b + a);
                                    if (t >= 0 && t <= g1)
                                        waysToShare++;

                                }
                            }
                        }
                    }
                }
            }
            return waysToShare;
        }


    }
}