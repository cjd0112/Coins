using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Xml.Schema;
using CoinsLib.Coins;

namespace CoinsLib.CombinationCalculator.Cache
{
   
    public static class ShareCoinsEvenly
    {
        public static int WaysToShare(Stack<int> coins)
        {
            return WaysToShare(coins.ToArray());
        }
        public  static int WaysToShare(int[] coins)
        {
#if DEBUG
            if (coins.Length > CoinFactory.MaxCoins)
                throw new Exception($"Only support {CoinFactory.MaxCoins} at present");
#endif
            if (coins.Sum() % 2 != 0)
                return 0;

            var c = coins.Length;
            if (c== 1)
                return 1;

            var q = coins.ToArray();
            Array.Sort(q);

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
            return res;

        }

        static int WaysToShare(int target,int a1, int b1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 ; a++)
            {
                if (target-a <= b1)
                    waysToShare++;
            }

            return waysToShare;
        }

       

        static int WaysToShare(int target, int a1, int b1,int c1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 ; a++)
            {
                for (int b = 0; b <= b1 ; b++)
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
            for (int a = 0; a <= a1 ; a++)
            {
                for (int b = 0; b <= b1 ; b++)
                {
                    for (int c = 0; c <= c1 ; c++)
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
            for (int a = 0; a <= a1 ; a++)
            {
                for (int b = 0; b <= b1 ; b++)
                {
                    for (int c = 0; c <= c1 ; c++)
                    {
                        for (int d = 0; d <= d1 ; d++)
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

        static int WaysToShare(int target, int a1, int b1, int c1, int d1, int e1, int f1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 ; a++)
            {
                for (int b = 0; b <= b1 ; b++)
                {
                    for (int c = 0; c <= c1 ; c++)
                    {
                        for (int d = 0; d <= d1 ; d++)
                        {
                            for (int e = 0; e <= e1 ; e++)
                            {
                                var t = target - (e + d + c + b + a);
                                if (t >= 0 && t <= f1)
                                    waysToShare++;

                            }
                        }
                    }
                }
            }
            return waysToShare;
        }

        static int WaysToShare(int target, int a1, int b1, int c1, int d1, int e1, int f1, int g1)
        {
            int waysToShare = 0;
            for (int a = 0; a <= a1 ; a++)
            {
                for (int b = 0; b <= b1 ; b++)
                {
                    for (int c = 0; c <= c1 ; c++)
                    {
                        for (int d = 0; d <= d1 ; d++)
                        {
                            for (int e = 0; e <= e1 ; e++)
                            {
                                for (int f = 0; f <= f1 ; f++)
                                {
                                    var t = target - (f + e + d + c + b + a);
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
