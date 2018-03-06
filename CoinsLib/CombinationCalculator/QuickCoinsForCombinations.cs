using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace CoinsLib.CombinationCalculator
{
    public static class Combinations
    {
        public static IEnumerable<Int32> QuickCalculateCombinations(int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4,(int value,int multiple) n5,(int value,int multiple) n6,(int value,int multiple) n7)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;
            var transitionFactor5 = n5.multiple / n6.multiple;
            var transitionFactor6 = n6.multiple / n7.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;

                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        for (var m = 0; m < n4.value - tf3; m++)
                        {
                            int m_coins = m + 1;

                            var tf4 = (tf3 * transitionFactor4) + (transitionFactor4 * m);

                            for (var n = 0; n < n5.value - tf4; n++)
                            {
                                int n_coins = n + 1;

                                var tf5 = (tf4 * transitionFactor5) + (transitionFactor5 * n);

                                for (var o = 0; o < n6.value - tf5; o++)
                                {
                                    var o_coins = o + 1;

                                    var tf6 = (tf5 * transitionFactor6) + (transitionFactor6 * o);

                                    var p_coins = n7.multiple - tf6;
#if DEBUG
                                    if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                                        m_coins * n4.multiple + n_coins * n5.multiple + o_coins * n6.multiple + p_coins * n7.multiple !=
                                        valueForAssert)
                                    {
                                        throw new Exception(
                                            $"found a combination that does not add up to our expected total - {valueForAssert}");
                                    }
#endif
                                    yield return j_coins + k_coins + l_coins + m_coins + n_coins + o_coins + p_coins;
                                }
                            }
                        }
                    }
                }
            }
        }


        public static IEnumerable<Int32> QuickCalculateCombinations(int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4,(int value,int multiple) n5,(int value,int multiple) n6)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;
            var transitionFactor5 = n5.multiple / n6.multiple;
            
            

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;

                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        for (var m = 0; m < n4.value - tf3; m++)
                        {
                            int m_coins = m + 1;

                            var tf4 = (tf3 * transitionFactor4) + (transitionFactor4 * m);

                            for (var n = 0; n < n5.value - tf4; n++)
                            {
                                int n_coins = n + 1;

                                var tf5 = (tf4 * transitionFactor5) + (transitionFactor5 * n);

                                var o_coins = n6.value - tf5;

#if DEBUG
                                if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                                    m_coins * n4.multiple + n_coins * n5.multiple + o_coins * n6.multiple != valueForAssert)
                                {
                                    throw new Exception(
                                        $"found a combination that does not add up to our expected total - {valueForAssert}");
                                }
#endif
                                yield return j_coins + k_coins + l_coins + m_coins + n_coins + o_coins;
                            }
                        }
                    }
                }
            }
        }
        
        public static void QuickCalculateCombinations2(Action<Int32> act, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4,(int value,int multiple) n5,(int value,int multiple) n6)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;
            var transitionFactor5 = n5.multiple / n6.multiple;
            
            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;

                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        for (var m = 0; m < n4.value - tf3; m++)
                        {
                            int m_coins = m + 1;

                            var tf4 = (tf3 * transitionFactor4) + (transitionFactor4 * m);

                            for (var n = 0; n < n5.value - tf4; n++)
                            {
                                int n_coins = n + 1;

                                var tf5 = (tf4 * transitionFactor5) + (transitionFactor5 * n);

                                var o_coins = n6.value - tf5;

#if DEBUG
                                if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                                    m_coins * n4.multiple + n_coins * n5.multiple + o_coins * n6.multiple != valueForAssert)
                                {
                                    throw new Exception(
                                        $"found a combination that does not add up to our expected total - {valueForAssert}");
                                }
#endif
                                act(j_coins + k_coins + l_coins + m_coins + n_coins + o_coins);
                            }
                        }
                    }
                }
            }
        }


        
        
        
        
        
        public static IEnumerable<Int32> QuickCalculateCombinations(int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4,(int value,int multiple) n5)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;

                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        for (var m = 0; m < n4.value - tf3; m++)
                        {
                            int m_coins = m + 1;

                            var tf4 = (tf3 * transitionFactor4) + (transitionFactor4 * m);

                            int n_coins = n5.value - tf4;

#if DEBUG
                            if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                                m_coins * n4.multiple + n_coins * n5.multiple  != valueForAssert)
                            {
                                throw new Exception(
                                    $"found a combination that does not add up to our expected total - {valueForAssert}");
                            }
#endif
                            yield return j_coins + k_coins + l_coins + m_coins + n_coins;
                        }
                    }
                }
            }
        }

        public static IEnumerable<Int32> QuickCalculateCombinations(int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;

                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        int m_coins = n4.value - tf3;

#if DEBUG
                        if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                            m_coins * n4.multiple != valueForAssert)
                        {
                            throw new Exception($"found a combination that does not add up to our expected total - {valueForAssert}");
                        }
                        
#endif
                        
                        yield return j_coins + k_coins + l_coins + m_coins;
                    }
                }
            }
        }


        public static IEnumerable<Int32> QuickCalculateCombinations(int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    int l_coins = n3.value - tf2;

#if DEBUG
                    if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple != valueForAssert)
                    {
                        throw new Exception($"found a combination that does not add up to our expected total - {valueForAssert}");
                    }
#endif
                    yield return j_coins + k_coins + l_coins;
                }
            }
        }

        public static IEnumerable<Int32> QuickCalculateCombinations(int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                var tf1 = transitionFactor1 * j;
                
                int k_coins = n2.value - tf1;

#if DEBUG
                if (j_coins * n1.multiple + k_coins * n2.multiple != valueForAssert)
                {
                    throw new Exception($"found a combination that does not add up to our expected total - {valueForAssert}");
                }
#endif
                yield return j_coins + k_coins;
            }
        }

        public static IEnumerable<Int32> QuickCalculateCombinations(int valueForAssert,(int noCoins, int multiple) n1)
        {
#if DEBUG
            if (n1.noCoins * n1.multiple != valueForAssert)
                throw new Exception($"Unexpected value found - noCoins * multiple should be {valueForAssert}");
#endif
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
                for (int i = 1; i <=value/c.Units;i++)
                {
                    totalCoins++;
                    foreach (var x in BruteForceCombinations(c.Next, value - i*c.Units, totalCoins))
                    {
                        yield return x;
                    }
                }
            }
        }
        
        public static void BruteForceCombinations(Action<Int32> act,Int32 unit1,Int32 unit2,Int32 unit3, Int32 unit4,Int32 unit5,Int32 unit6, Int32 unit7, int value)
        {

            var i_Coins  = 0;
            for (int i = 1; i <= value / unit1; i++)
            {
                i_Coins++;
                var j_val = value - i * unit1;
                var j_Coins = i_Coins;
                for (int j = 1; j <= j_val / unit2; j++)
                {
                    j_Coins++;
                    
                    var k_val = j_val - j * unit2;

                    var k_Coins = j_Coins;
                    for (int k = 1; k <= k_val / unit3; k++)
                    {
                        k_Coins++;

                        var l_val = k_val - k * unit3;

                        var l_Coins = k_Coins;

                        for (int l = 1; l <= l_val / unit4 ; l++)
                        {
                            l_Coins++;

                            var m_val = l_val - l * unit4;

                            var m_Coins = l_Coins;

                            for (int m = 1; m <= m_val / unit5; m++)
                            {
                                m_Coins++;

                                var n_val = m_val - m * unit5;

                                var n_Coins = m_Coins;

                                for (int n = 1; n <= n_val / unit6; n++)
                                {
                                    var o_Coins = n_Coins;
                                    o_Coins++;

                                    var o_val = n_val - n * unit6;
                                    if (o_val % unit7 == 0 && o_val > 0)
                                        act(o_val / unit7 + o_Coins);
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public static void BruteForceCombinations(Action<Int32> act,Int32 unit1,Int32 unit2,Int32 unit3, Int32 unit4,Int32 unit5,Int32 unit6, int value)
        {
            var i_Coins  = 0;
            for (int i = 1; i <= value / unit1; i++)
            {
                i_Coins++;
                var j_val = value - i * unit1;
                var j_Coins = i_Coins;
                for (int j = 1; j <= j_val / unit2; j++)
                {
                    j_Coins++;
                    
                    var k_val = j_val - j * unit2;

                    var k_Coins = j_Coins;
                    for (int k = 1; k <= k_val / unit3; k++)
                    {
                        k_Coins++;

                        var l_val = k_val - k * unit3;

                        var l_Coins = k_Coins;

                        for (int l = 1; l <= l_val / unit4 ; l++)
                        {
                            l_Coins++;

                            var m_val = l_val - l * unit4;

                            var m_Coins = l_Coins;

                            for (int m = 1; m <= m_val / unit5; m++)
                            {
                                m_Coins++;

                                var n_val = m_val - m * unit5;
                                
                                if (n_val % unit6 == 0 && n_val > 0)
                                    act(n_val / unit6 +  m_Coins);
                            }
                        }
                    }
                }
            }
        }

        
        public static void BruteForceCombinations(Action<Int32> act,Int32 unit1,Int32 unit2,Int32 unit3, int value)
        {

            var i_Coins  = 0;
            for (int i = 1; i <= value / unit1; i++)
            {
                i_Coins++;
                var j_val = value - i * unit1;
                var j_Coins = i_Coins;
                for (int j = 1; j <= j_val / unit2; j++)
                {
                    j_Coins++;
                    
                    var k_val = j_val - j * unit2;
                    if (k_val % unit3 == 0 && k_val > 0)
                        act(k_val / unit3 + j_Coins);
                }
            }
        }


        public static IEnumerable<(int value, int multiple)> GenerateCoinsAndMultiples(Coin c, Int32 value,int startPoint = 0)
        {
            if (c.RequiresBruteForceCalculator())
                throw new Exception("invalid option - requires BruteForceCalculator");

            if (value % c.GenerateMyUnits().Last() != 0)
                throw new Exception($"Generate coins and multiples requires factor of lowest value - {c.GenerateMyUnits().Last()}");
            
            if (startPoint == 0)
                startPoint = c.GenerateMyUnits().Sum();

            var differenceBetweenValAndStartPoint = value - startPoint;

            int coins = (differenceBetweenValAndStartPoint / c.Units) + 1;

            yield return (coins, c.Units);
            
            if (c.Next != null)
                foreach (var y in GenerateCoinsAndMultiples(c.Next, value, startPoint))
                    yield return y;

        }

    }
}
