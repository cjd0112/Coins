using System;

namespace CoinsLib.CombinationCalculator.Underlying
{
    /// <summary>
    /// Class holds functions that determine count of coins
    /// for each combination of a value where the input
    /// is a series of units that are 'uneven' e.g., 
    ///  56,48,24,12,6,3,2,1 ... 
    ///  Deals with The Half-Crown case
    /// 
    /// For efficiency the caller has to pass in an array[X] where X is 'value' to store results. i.e., 
    /// if the function finds one combination has 213 coins then it needs to add it -> array[213] += 213
    /// thus you efficiently build up the tally of coin #'s for each combination.  It would be better for
    /// division of responsibility to pass in, say Action<Int>, to keep track, or to return IEnumerable<Int> 
    /// but either of these adds significantly to latency.  
    /// 
    /// </summary>
    public static class CoinsForUnevenFactors
    {
        /// <summary>
        /// Where factors in a combination i.e., 7,5,1 
        /// are not all multiples of succeeding need to use a less-optimal technique for calculating
        /// this determines the number of coins for each multiple in the value and then passes
        /// on the remainder to the successor units ... and so on.
        /// at the end (if the remainder is zero) you have a valid combination and have kept track
        /// of total coins in that combination. 
        /// </summary>
        /// <param name="arr">must be > size of largest value</param>
        /// <param name="value">input value in decimal format</param>
        /// <param name="unit1">first multiple </param>
        /// <param name="unit2">etc., </param>
        /// <param name="unit3"></param>
        /// <param name="unit4"></param>
        /// <param name="unit5"></param>
        /// <param name="unit6"></param>
        /// <param name="unit7"></param>
        /// <returns>number of combinations found</returns>
        /// <exception cref="Exception">throws in debug mode if array length is too small </exception>        
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int value, Int32 unit1,Int32 unit2,Int32 unit3, Int32 unit4,Int32 unit5,Int32 unit6, Int32 unit7)
        {
#if DEBUG
            if (arr.Length < value)
                throw new Exception("arr.length needs to be >= value");
#endif
            Int64 cnt = 0;
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
                                    n_Coins++;

                                    var o_val = n_val - n * unit6;
                                    if (o_val % unit7 == 0 && o_val > 0)
                                    {
                                        var g = o_val / unit7 + n_Coins;
                                        arr[g] += g;
                                        cnt++;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return cnt;
        }
        
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int value, Int32 unit1,Int32 unit2,Int32 unit3, Int32 unit4,Int32 unit5,Int32 unit6)
        {
#if DEBUG
            if (arr.Length < value)
                throw new Exception("arr.length needs to be >= value");
#endif
            Int64 cnt = 0;
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
                                {
                                    var g = n_val / unit6 + m_Coins;
                                    arr[g] += g;
                                    cnt++;
                                }
                            }
                        }
                    }
                }
            }
            return cnt;
        }

        /// <summary>
        /// See comments above
        /// </summary>
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int value, Int32 unit1,Int32 unit2,Int32 unit3, Int32 unit4,Int32 unit5)
        {
#if DEBUG
            if (arr.Length < value)
                throw new Exception("arr.length needs to be >= value");
#endif
            Int64 cnt = 0;
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


                            if (m_val % unit5 == 0 && m_val > 0)
                            {
                                var g = m_val / unit5 + l_Coins;
                                arr[g] += g;
                                cnt++;
                            }
                        }
                    }
                }
            }
            return cnt;
        }
        
        /// <summary>
        /// See comments above
        /// </summary>
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int value, Int32 unit1,Int32 unit2,Int32 unit3, Int32 unit4)
        {
#if DEBUG
            if (arr.Length < value)
                throw new Exception("arr.length needs to be >= value");
#endif
            Int64 cnt = 0;
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

                        if (l_val % unit4 == 0 && l_val > 0)
                        {
                            var g = l_val / unit4 + k_Coins;
                            arr[g] += g;
                            cnt++;
                        }
                    }
                }
            }
            return cnt;
        }
        /// <summary>
        /// See comments above
        /// </summary>
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int value, Int32 unit1,Int32 unit2,Int32 unit3)
        {
#if DEBUG
            if (arr.Length < value)
                throw new Exception("arr.length needs to be >= value");
#endif
            Int64 cnt = 0;
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
                    {
                        var g = k_val / unit3 + j_Coins;
                        arr[g] += g;
                        cnt++;
                    }
                    
                }
            }
            return cnt;
        }
        
        /// <summary>
        /// See comments above
        /// </summary>
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int value, Int32 unit1,Int32 unit2)
        {
#if DEBUG
            if (arr.Length < value)
                throw new Exception("arr.length needs to be >= value");
#endif
            Int64 cnt = 0;
            var i_Coins  = 0;
            for (int i = 1; i <= value / unit1; i++)
            {
                i_Coins++;
                var j_val = value - i * unit1;

                if (j_val % unit2 == 0 && j_val > 0)
                {
                    var g = j_val / unit2 + i_Coins;
                    arr[g] += g;
                    cnt++;
                }
            }
            return cnt;
        }
        
        /// <summary>
        /// See comments above
        /// </summary>
        public static Int64 CalculateTotalCoinsForEachComboAndReturnCount(Int64[] arr,int value, Int32 unit1)
        {
#if DEBUG
            if (arr.Length < value)
                throw new Exception("arr.length needs to be >= value");
#endif
            Int64 cnt = 0;

            if (value % unit1 == 0 && value > 0)
            {
                var g = 1;
                arr[g] += g;
                cnt++;
            }
            return cnt;
        }

    }
}