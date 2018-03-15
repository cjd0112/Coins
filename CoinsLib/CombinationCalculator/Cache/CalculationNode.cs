using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class CalculationNode
    {
        Coin coin;

        public int Depth => coin.NodeCount();

        protected int Head;

        protected CalculationNode parent;

        protected int SumOfUnits;

        public string ComboKey;

        protected CalculationGrid Grid;

        private int[][] cache;

        private int[] totalCoinCount;

        public bool SupportsCache()
        {
            return cache != null;
        }

        private bool isEven;
        
        public CalculationNode(Coin c,CalculationGrid grid)
        {
            coin = c;
            SumOfUnits = c.GenerateMyUnits().Sum();
            Grid = grid;
            Head = coin.Units;
            isEven = c.GenerateMyUnits().Last() == 2;

           ComboKey = Grid.ComboKey(this);
        }

        private int maxValue;
        public void InitializeCache(int maxValue)
        {
            this.maxValue = maxValue + 1;
            cache = new int[this.maxValue][];
            totalCoinCount = new int[this.maxValue];
        }

  
        public void SetParent(CalculationNode parent)
        {
            this.parent = parent;
        }

        public Coin GetCoin()
        {
            return coin;
        }


        public CalculationNode AddChild(CalculationNode n)
        {
            children.Add(n);
            n.SetParent(this);
            return this;
        }

        public IEnumerable<CalculationNode> GetChildren()
        {
            return children;
        }

        IEnumerable<Int32> TailUnits()
        {
            if (coin.GenerateMyUnits().Count() == 1)
                return Enumerable.Empty<Int32>();
            return coin.GenerateMyUnits().Skip(1);

        }

        public IEnumerable<CalculationNode> GetAllNodes()
        {
            if (this.children.Count == 0)
                yield return this;
            else
            {
                yield return this;
                foreach (var c in children.SelectMany(x=>x.GetAllNodes()))
                    yield return c;
            }
        }

        public bool IsParent(CalculationNode candidate)
        {
            return coin.GenerateMyUnits().SequenceEqual(candidate.TailUnits());
        }

        protected List<CalculationNode> children = new List<CalculationNode>();

        
        public static int ExcludedEven = 0;

        public static int total = 0;

        public static int excludedMax = 0;

        public void Process(int value, Int64[] arr, int remainder, int totalCoins,int maxCoins)
        {
            total++;
            if (totalCoins > maxCoins)
            {
                excludedMax++;
                return;

            }

            if (isEven && remainder % 2 == 1)
            {
                ExcludedEven++;
                return;
            }


            if (remainder != 0)
            {
                if (parent == null)
                {
                    if (remainder > 0)
                    {
                        if (Head == 1)
                            arr[remainder + totalCoins]++;
                        else if (Head == 2 && (remainder & (Head - 1)) == 0)
                        {
                            arr[remainder / Head + totalCoins]++;
                        }
                        else if (remainder % Head == 0)
                        {
                            arr[remainder / Head + totalCoins]++;
                        }
                    }
                }
                else
                {
                    if (SupportsCache())
                    {
                        if (cache[remainder] == null)
                        {
                            cache[remainder] = new int[maxValue];

                        }
                        cache[remainder][totalCoins]++;
                        totalCoinCount[remainder]++;
                    }
                    else
                    {
                        for (int i = 1; i <=remainder/Head;i++)
                        {
                            totalCoins++;
                            if (totalCoins > maxCoins)
                            {
                                excludedMax++;
                                break;

                            }
                            parent.Process(value,arr, remainder - i * Head, totalCoins,maxCoins);
                        }                        
                    }
                }
            }
        }

        public void ProcessCache(int value, Int64[] arr,int maxCoins)
        {
           if (SupportsCache() == false)
               throw new Exception("Doesn't support cache");

            List<int> thisCoins = new List<int>();

            for (int i = 0; i < maxValue; i++)
            {
                var totalCoinList = cache[i];
                if (totalCoinList != null)
                {
                    thisCoins.Clear();

                    GenerateCache2(value, i, 0, thisCoins,maxCoins);

                    foreach (var c in thisCoins)
                    {
                        int cnt = 0;
                        int tcc = totalCoinCount[i];
                        for (int p = 0; p < maxValue; p++)
                        {
                            if (cnt == tcc)
                                break;
                            if (totalCoinList[p] != 0)
                            {
                                if (value == 150)
                                {

                                }
                                cnt++;
                                
                                arr[p + c] += totalCoinList[p];
                            }
                        }
                    }

                }
            }
        }


        void GenerateCache2(int value, int remainder,int totalCoins,List<int> res,int maxCoins)
        {
            if (parent == null)
            {
                if (remainder > 0)
                {
                    if (Head == 1)
                        res.Add(remainder + totalCoins);
                    else if (Head == 2 && (remainder & (Head - 1)) == 0)
                    {
                        res.Add(remainder / Head + totalCoins);
                    }
                    else if (remainder % Head == 0)
                    {
                        res.Add(remainder / Head + totalCoins);
                    }
                }
            }
            else
            {
                for (int i = 1; i <=remainder/Head;i++)
                {
                    totalCoins++;
                    if (totalCoins > maxCoins)
                    {
                        excludedMax++;
                        break;
                    }

                    parent.GenerateCache2(value, remainder - i * Head, totalCoins,res,maxCoins);
                }
            }
        }
    }
}