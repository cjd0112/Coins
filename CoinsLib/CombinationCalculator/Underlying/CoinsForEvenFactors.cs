using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CoinsLib.CombinationCalculator.Underlying
{
    /// <summary>
    /// 'CoinsForEvenFactors' expects input representing a value that shows
    /// the TOTAL number of times the particular unit can go into the value. 
    /// A table makes it clearer:
    ///       6    2    1
    /// 9     1    1    1 
    /// 10    1    1    2
    /// 11    1    2    3
    /// 12    1    2    4
    /// 13    1    3    5
    /// 14    1    3    6
    /// 16    2    4    7
    /// 
    /// Given this representation, where the units are successive multiples (as they are except for
    /// Half-Crown).  The algorithm can grind through each combination using only multiplication and 
    /// addition. 
    /// 
    /// Where the units are not successive multiples (Half-Crown) this will not work. 
    /// For that case 'CoinsForUnevenFactors' a more general (slower) approach is necessary that uses '/' and '%'
    /// to derive the set of numbers of coins given a value.  
    ///
    /// For efficiency the caller has to pass in an array[X] where X is 'value' to store results. i.e., 
    /// if the function finds one combination has 213 coins then it needs to add it -> array[213] += 213
    /// thus you efficiently build up the tally of coin #'s for each combination.  It would be better for
    /// division of responsibility to pass in, say Action<Int>, to keep track, or to return IEnumerable<Int> 
    /// but either of these adds significantly to latency.  
    /// 
    /// 'CoinsByRecursion' is a simpler recursive version of 'CoinsForUnevenFactors' (with an easier interface) which can be used
    /// for validation but is too slow for general use. 
    /// 
    /// </summary>
    public class CoinsForEvenFactors
    {
                /// <summary>
        /// Takes a representation of a combination of units - as per discussion in Class header
        /// and efficiently calculates # of coins in each combination and adds them to the 
        /// passed in array in the appropriate slot for # of coins
        /// </summary>
        /// <param name="arr">Input array of same size as original value</param>
        /// <param name="valueForAssert">The input number in decimal form - used in Debug more for validation</param>
        /// <param name="maximumCoins">Stop calculating if you exceed this # coins - the 'other side' of the calculation will not match </param>
        /// <param name="n1">Tuple representing number of these units and how many of them there are to each base unit.</param>
        /// <param name="n2"></param>
        /// <param name="n3"></param>
        /// <param name="n4"></param>
        /// <param name="n5"></param>
        /// <param name="n6"></param>
        /// <param name="n7"></param>
        /// <returns>Number of combinations found</returns>
        /// <exception cref="Exception">In debug more validates and throws exception if a combination is not a factor of 'valueForAssert' or array too small </exception>
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr, int maximumCoins, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4,(int value,int multiple) n5,(int value,int multiple) n6,(int value,int multiple) n7)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;
            var transitionFactor5 = n5.multiple / n6.multiple;
            var transitionFactor6 = n6.multiple / n7.multiple;

            Int64 cnt = 0;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                if (j_coins > maximumCoins)
                    continue;

                int tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    if (j_coins + k_coins > maximumCoins)
                        continue;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;
                        
                        if (j_coins + k_coins + l_coins > maximumCoins)
                            continue;

                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        for (var m = 0; m < n4.value - tf3; m++)
                        {
                            int m_coins = m + 1;
                            
                            if (j_coins + k_coins + l_coins + m_coins > maximumCoins)
                                continue;


                            var tf4 = (tf3 * transitionFactor4) + (transitionFactor4 * m);

                            for (var n = 0; n < n5.value - tf4; n++)
                            {
                                int n_coins = n + 1;

                                if (j_coins + k_coins + l_coins + m_coins + n_coins > maximumCoins)
                                    continue;

                                var tf5 = (tf4 * transitionFactor5) + (transitionFactor5 * n);

                                for (var o = 0; o < n6.value - tf5; o++)
                                {
                                    var o_coins = o + 1;
                                    
                                    if (j_coins + k_coins + l_coins + m_coins + n_coins + o_coins > maximumCoins)
                                        continue;

                                    var tf6 = (tf5 * transitionFactor6) + (transitionFactor6 * o);

                                    var p_coins = n7.value - tf6;

                                    if (j_coins + k_coins + l_coins + m_coins + n_coins + o_coins + p_coins  > maximumCoins)
                                        continue;
#if DEBUG
                                    if (arr.Length < valueForAssert)
                                        throw new Exception("array length has to be greater than or equal to valueForAssert");
                                    
                                    if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                                        m_coins * n4.multiple + n_coins * n5.multiple + o_coins * n6.multiple + p_coins * n7.multiple !=
                                        valueForAssert)
                                    {
                                        throw new Exception(
                                            $"found a combination that does not add up to our expected total - {valueForAssert}");
                                    }
#endif
                                    cnt++;
                                    
                                    var ret =  j_coins + k_coins + l_coins + m_coins + n_coins + o_coins + p_coins;
                                    arr[ret]++;
                                }
                            }
                        }
                    }
                }
            }

            return cnt;
        }

        /// <summary>
        /// See comment above
        /// </summary>           
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr, int maximumCoins, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4,(int value,int multiple) n5,(int value,int multiple) n6)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;
            var transitionFactor5 = n5.multiple / n6.multiple;

            Int64 cnt = 0;
            
            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                if (j_coins > maximumCoins)
                    continue;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;
                    
                    if (j_coins + k_coins > maximumCoins)
                        continue;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;

                        if (j_coins + k_coins + l_coins > maximumCoins)
                            continue;

                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        for (var m = 0; m < n4.value - tf3; m++)
                        {
                            int m_coins = m + 1;
                            
                            if (j_coins + k_coins + l_coins + m_coins > maximumCoins)
                                continue;


                            var tf4 = (tf3 * transitionFactor4) + (transitionFactor4 * m);

                            for (var n = 0; n < n5.value - tf4; n++)
                            {
                                int n_coins = n + 1;
                                
                                if (j_coins + k_coins + l_coins + m_coins + n_coins > maximumCoins)
                                    continue;

                                var tf5 = (tf4 * transitionFactor5) + (transitionFactor5 * n);

                                var o_coins = n6.value - tf5;
                                
                                if (j_coins + k_coins + l_coins + m_coins + o_coins > maximumCoins)
                                    continue;
#if DEBUG
                                if (arr.Length < valueForAssert)
                                    throw new Exception("arr length needs to be > valueForAssert");
                                if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                                    m_coins * n4.multiple + n_coins * n5.multiple + o_coins * n6.multiple != valueForAssert)
                                {
                                    throw new Exception(
                                        $"found a combination that does not add up to our expected total - {valueForAssert}");
                                }
#endif
                                var g = j_coins + k_coins + l_coins + m_coins + n_coins + o_coins;
                                arr[g]++;
                                cnt++;
                            }
                        }
                    }
                }
            }
            return cnt;
        }
        
        /// <summary>
        /// See comment above
        /// </summary>           
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr, int maximumCoins, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4,(int value,int multiple) n5)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;
            var transitionFactor4 = n4.multiple / n5.multiple;

            Int64 cnt = 0;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                if (j_coins > maximumCoins)
                    continue;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;
                    
                    if (j_coins + k_coins  > maximumCoins)
                        continue;


                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;
                        
                        if (j_coins + k_coins + l_coins > maximumCoins)
                            continue;

                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        for (var m = 0; m < n4.value - tf3; m++)
                        {
                            int m_coins = m + 1;
                            
                            if (j_coins + k_coins + l_coins + m_coins > maximumCoins)
                                continue;

                            var tf4 = (tf3 * transitionFactor4) + (transitionFactor4 * m);

                            int n_coins = n5.value - tf4;
                            
                            if (j_coins + k_coins + l_coins + m_coins + n_coins > maximumCoins)
                                continue;

#if DEBUG
                            if (arr.Length < valueForAssert)
                                throw new Exception("arr.Length has to be <= valueForAssert");
                            
                            if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                                m_coins * n4.multiple + n_coins * n5.multiple  != valueForAssert)
                            {
                                throw new Exception(
                                    $"found a combination that does not add up to our expected total - {valueForAssert}");
                            }
#endif
                            var g = j_coins + k_coins + l_coins + m_coins + n_coins;
                            arr[g]++;
                        }
                    }
                }
            }
            return cnt;
        }

        /// <summary>
        /// See comment above
        /// </summary>           
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int maximumCoins, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3, (int value, int multiple) n4)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
            var transitionFactor3 = n3.multiple / n4.multiple;

            Int64 cnt = 0;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                if (j_coins > maximumCoins)
                    continue;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    if (j_coins +k_coins > maximumCoins)
                        continue;

                    
                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    for (var l = 0; l < n3.value - tf2; l++)
                    {
                        int l_coins = l + 1;

                        if (j_coins +k_coins + l_coins > maximumCoins)
                            continue;


                        var tf3 = (tf2* transitionFactor3) + (transitionFactor3 * l);

                        int m_coins = n4.value - tf3;
                        
                        if (j_coins +k_coins + l_coins + m_coins > maximumCoins)
                            continue;
#if DEBUG
                        if (arr.Length < valueForAssert)
                            throw new Exception("arr.length needs to be >= valueForAssert ");
                        
                        if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple +
                            m_coins * n4.multiple != valueForAssert)
                        {
                            throw new Exception($"found a combination that does not add up to our expected total - {valueForAssert}");
                        }
                        
#endif
                        
                        var g = j_coins + k_coins + l_coins + m_coins;
                        arr[g]++;
                    }
                }
            }
            return cnt;
        }

        /// <summary>
        /// See comment above
        /// </summary>           
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr, int maximumCoins, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2, (int value, int multiple) n3)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            var transitionFactor2 = n2.multiple / n3.multiple;
           

            Int64 cnt = 0;

            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                if (j_coins > maximumCoins)
                    continue;

                var tf1 = transitionFactor1 * j;
                
                for (var k = 0; k < n2.value - tf1; k++)
                {
                    int k_coins = k + 1;

                    if (j_coins + k_coins > maximumCoins)
                        continue;

                    var tf2 = (tf1*transitionFactor2) + (transitionFactor2 * k);

                    int l_coins = n3.value - tf2;
                    
                    if (j_coins + k_coins + l_coins  > maximumCoins)
                        continue;

#if DEBUG
                    if (arr.Length < valueForAssert)
                        throw new Exception("arr.length needs to be >= valueForAssert");
                    if (j_coins * n1.multiple + k_coins * n2.multiple + l_coins * n3.multiple != valueForAssert)
                    {
                        throw new Exception($"found a combination that does not add up to our expected total - {valueForAssert}");
                    }
#endif
                    var g = j_coins + k_coins + l_coins;
                    arr[g]++;
                }
            }
            return cnt;
        }
        /// <summary>
        /// See comment above
        /// </summary>           
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int maximumCoins, int valueForAssert, (int value, int multiple) n1, (int value, int multiple) n2)
        {
            var transitionFactor1 = n1.multiple / n2.multiple;
            

            Int64 cnt = 0;
            for (var j = 0; j < n1.value; j++)
            {
                int j_coins = j + 1;

                if (j_coins > maximumCoins)
                    continue;

                var tf1 = transitionFactor1 * j;
                
                int k_coins = n2.value - tf1;
                
                if (j_coins + k_coins  > maximumCoins)
                    continue;


#if DEBUG
                if (j_coins * n1.multiple + k_coins * n2.multiple != valueForAssert)
                {
                    throw new Exception($"found a combination that does not add up to our expected total - {valueForAssert}");
                }
#endif
                var g = j_coins + k_coins;
                arr[g]++;
            }
            return cnt;
        }
        
        
        
        
        /// <summary>
        /// See comment above
        /// </summary>           
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr, int maximumCoins, int valueForAssert,(int noCoins, int multiple) n1)
        {
            Int64 cnt = 0;
#if DEBUG
                        
            if (arr.Length < valueForAssert)
                throw new Exception("arr.Length has to be >= valueForAssert");
            if (n1.noCoins * n1.multiple != valueForAssert)
                throw new Exception($"Unexpected value found - noCoins * multiple should be {valueForAssert}");
#endif
            if (n1.noCoins <= maximumCoins)
            {
                arr[n1.noCoins]++;
                cnt++;
                
            }
            return cnt;
        }    
    }
}