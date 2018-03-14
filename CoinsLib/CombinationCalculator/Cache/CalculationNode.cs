using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class CalculationNode
    {
        Coin coin;

        public int Depth => coin.NodeCount();

        protected int Head => coin.Units;

        protected CalculationNode parent;

        protected int SumOfUnits;

        public string ComboKey;

        protected CalculationGrid Grid;

        private CompressedIntegerCache cache;
        
        public CalculationNode(Coin c,CalculationGrid grid)
        {
            coin = c;
            SumOfUnits = c.GenerateMyUnits().Sum();
            Grid = grid;

            ComboKey = Grid.ComboKey(this);

            if (Depth == 2 || Depth == 3 || Depth == 4)
            {
                  cache = new CompressedIntegerCache(grid.MaxValue);
            }
        }

        public bool UseCache()
        {
            return cache != null;
        }


        public CompressedIntegerCache GetCache()
        {
            return cache;
        }

        public void OnStartProcessing(int value)
        {
            
        }

        public void OnEndProcessing(int value)
        {
            if (UseCache())
                cache.OnEndProcessing(value);
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

        public void CalculateTotalCoins(int value, Int64[] arr, int remainder,int totalCoins)
        {
            if (parent == null)
            {
                if (remainder % Head == 0 && remainder > 0)
                    arr[remainder / Head + totalCoins]++;
            }
            else
            {
                if (UseCache() && remainder > 0)
                {
                    if (remainder == 495 && Head == 12)
                    {
                        
                    }
                    if (cache.ContainsRemainder(remainder) == false)
                    {
                        var lst = new List<int>();
                        GenerateCache(value, remainder, 0, lst);

                        if (lst.Count() > 2)
                        {
                            int gap = lst[0] - lst[1];

                            var blah = -1;
                            foreach (var c in lst)
                            {
                                if (blah == -1)
                                    blah = c;
                                else
                                {
                                    if (blah - c != gap)
                                    {
                                        Console.WriteLine("hit here");

                                    }
                                    blah = c;
                                }
                            }
                        }

                        if (lst.Any())
                            cache.AddCombinationsToCache(remainder, lst);
                    }
                    
                    cache.AddTotalCoinsToCache(remainder,totalCoins);
                    
                }
                else
                {
                    for (int i = 1; i <= remainder / Head; i++)
                    {
                        totalCoins++;
                        parent.CalculateTotalCoins(value, arr, remainder - i * Head, totalCoins);
                    }
                    
                }

            }
        }
        
        void GenerateCache(int value, int remainder,int totalCoins,List<int> res)
        {
            if (parent == null)
            {
                if (remainder % Head == 0 && remainder > 0)
                    res.Add(remainder / Head + totalCoins);
            }
            else
            {
                for (int i = 1; i <=remainder/Head;i++)
                {
                    totalCoins++;
                    parent.GenerateCache(value, remainder - i * Head, totalCoins,res);
                }
            }
        }
    }
}