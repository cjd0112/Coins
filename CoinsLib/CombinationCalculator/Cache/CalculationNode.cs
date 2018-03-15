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

        public bool SupportsCache()
        {
            return cache != null;
        }

        
        public CalculationNode(Coin c,CalculationGrid grid)
        {
            coin = c;
            SumOfUnits = c.GenerateMyUnits().Sum();
            Grid = grid;
            Head = coin.Units;

           ComboKey = Grid.ComboKey(this);

            if (Depth == 2)
            {
                cache = new int[grid.MaxValue][];
            }

        }

        void ClearCache()
        {
            cache = new int[Grid.MaxValue][];
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
        
        public void Process(int value, Int64[] arr, int remainder, int totalCoins,int maxCoins)
        {
            if (totalCoins > maxCoins)
                return;
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
                            cache[remainder] = new int[Grid.MaxValue];

                        }
                        cache[remainder][totalCoins]++;
                    }
                    else
                    {
                        for (int i = 1; i <=remainder/Head;i++)
                        {
                            totalCoins++;
                            if (totalCoins > maxCoins)
                                break;
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
            
            for (int i = 0; i < Grid.MaxValue; i++)
            {
                var totalCoinList = cache[i];
                if (totalCoinList != null)
                {
                    List<int> thisCoins = new List<int>();

                    GenerateCache2(value, i, 0, thisCoins,maxCoins);

                    foreach (var c in thisCoins)
                    {
                        
                        for (int p = 0; p < Grid.MaxValue; p++)
                        {
                            if (totalCoinList[p] != 0)
                            {
                                arr[p + c] += totalCoinList[p];
                            }
                        }
                    }

                }
            }
            ClearCache();
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
                        break;
                    parent.GenerateCache2(value, remainder - i * Head, totalCoins,res,maxCoins);
                }
            }
        }
    }
}