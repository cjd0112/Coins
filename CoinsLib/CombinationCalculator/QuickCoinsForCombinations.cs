using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace CoinsLib.CombinationCalculator
{
    public static class QuickCoinsCalculator
    {
        public static IEnumerable<Int32> Calculate((int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4, (int value, int multiple) n5, (int value, int multiple) n6,(int value,int multiple) n7)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;
            var transitionFactor5 = n5.multiple / n6.multiple;
            var transitionFactor6 = n5.multiple / n7.multiple;


            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;
                for (var k = 0; k < n2.value - (transitionFactor1 * j); k++)
                {
                    int k_coins = k + 1;

                    for (var l = 0; l < n3.value - (transitionFactor2 * k); l++)
                    {
                        int l_coins = l + 1;

                        for (var m = 0; m < n4.value - (transitionFactor3 * l); m++)
                        {
                            int m_coins = m + 1;

                            for (var n = 0; n < n5.value - (transitionFactor4 * m); n++)
                            {
                                int n_coins = n + 1;

                                for (var o = 0; o < n6.value - (transitionFactor5 * n); o++)
                                {
                                    int o_coins = o + 1;
                                    int p_coins = n6.value - (
                                                      (transitionFactor6 * transitionFactor5 * transitionFactor4 * transitionFactor3 *transitionFactor2 * transitionFactor1 * j) +
                                                      (transitionFactor6 * transitionFactor5 * transitionFactor4 * transitionFactor3 * transitionFactor2 * k) +
                                                      (transitionFactor6 * transitionFactor5 * transitionFactor4 * transitionFactor3 * l) +
                                                      (transitionFactor6 * transitionFactor5 * transitionFactor4 * m) +
                                                      (transitionFactor6 * transitionFactor5 * n) + 
                                                      (transitionFactor6 * o));
                                    yield return p_coins + o_coins + n_coins + m_coins + j_coins + k_coins + l_coins;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static IEnumerable<Int32> Calculate((int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4, (int value, int multiple) n5, (int value, int multiple) n6)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;
            var transitionFactor5 = n5.multiple / n6.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;
                for (var k = 0; k < n2.value - (transitionFactor1 * j); k++)
                {
                    int k_coins = k + 1;

                    for (var l = 0; l < n3.value - (transitionFactor2 * k); l++)
                    {
                        int l_coins = l + 1;

                        for (var m = 0; m < n4.value - (transitionFactor3 * l); m++)
                        {
                            int m_coins = m + 1;

                            for (var n = 0; n < n5.value - (transitionFactor4 * m); n++)
                            {
                                int n_coins  = n + 1;
                                int o_coins = n6.value - (
                                                  (transitionFactor5 * transitionFactor4 * transitionFactor3 *  transitionFactor2 * transitionFactor1 * j) +
                                                   (transitionFactor5 * transitionFactor4 * transitionFactor3 * transitionFactor2* k) +
                                                   (transitionFactor5 * transitionFactor4 * transitionFactor3 * l) +
                                                   (transitionFactor5 * transitionFactor4 * m) + 
                                                    (transitionFactor5 * n));
                                yield return o_coins + n_coins + m_coins + j_coins + k_coins + l_coins;
                            }
                        }
                    }
                }
            }
        }
        public static IEnumerable<Int32> Calculate((int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4, (int value, int multiple) n5)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;
                for (var k = 0; k < n2.value - (transitionFactor1 * j); k++)
                {
                    int k_coins = k + 1;

                    for (var l = 0; l < n3.value - (transitionFactor2 * k); l++)
                    {
                        int l_coins = l + 1;

                        for (var m = 0; m < n4.value - (transitionFactor3 * l); m++)
                        {
                            int m_coins = m + 1;


                            int n_coins = n5.value - (
                                              ((transitionFactor4 * transitionFactor3 * transitionFactor2 * transitionFactor1 * j) +
                                               (transitionFactor4 * transitionFactor3 * transitionFactor2 * k) +
                                               (transitionFactor4 * transitionFactor3 * l) +
                                               (transitionFactor4 * m)));
                            yield return n_coins + m_coins + j_coins + k_coins + l_coins;
                        }
                    }
                }
            }
        }

        public static IEnumerable<Int32> Calculate((int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;
                for (var k = 0; k < n2.value - (transitionFactor1 * j); k++)
                {
                    int k_coins = k + 1;

                    for (var l = 0; l < n3.value - (transitionFactor2 * k); l++)
                    {
                        int l_coins = l + 1;

                        int m_coins = n4.value - (
                                          ((transitionFactor3 * transitionFactor2 * transitionFactor1 * j) + 
                                           (transitionFactor3 * transitionFactor2 * k) + 
                                           (transitionFactor3*l)));
                        yield return m_coins + j_coins + k_coins + l_coins;
                    }
                }
            }
        }


        public static IEnumerable<Int32> Calculate((int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;
                for (var k = 0; k < n2.value - (transitionFactor1 * j); k++)
                {
                    int k_coins = k + 1;

                    int l_coins = n3.value - ((transitionFactor2 * transitionFactor1 * j) +
                                              (transitionFactor2 * k));
                    yield return j_coins + k_coins + l_coins;
                }
            }
        }

        public static IEnumerable<Int32> Calculate((int value, int multiple) n1, (int value, int multiple) n2)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;

            for (var k = 0; k < n1.value; k++)
            {
                int k_coins = k + 1;

                int l_coins = n2.value - ((transitionFactor1 * k));
                yield return k_coins + l_coins;
            }     
        }

        public static IEnumerable<Int32> Calculate((int noCoins, int multiple) n1)
        {
            yield return n1.noCoins;
        }

        public static IEnumerable<Int32> BruteForceCombinations(Coin c, int value, int totalCoins=0)
        {
            if (c.Next == null )
            {
                if (value % c.Units == 0  && value > 0)  
                    yield return value / c.Units + totalCoins;
            }
            else
            {
                for (int i = 1; i <= value/c.Units;i++)
                {
                    totalCoins++;
                    foreach (var x in BruteForceCombinations(c.Next, value - i*c.Units, totalCoins))
                    {
                        yield return x;
                    }
                }
            }
        }


    }
}
