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

        protected int Head;

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
            Head = coin.Units;

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


                //if (remainder > 0 && remainder % Head == 0 )
                //arr[remainder / Head + totalCoins]++;
            }
            else
            {
                if (UseCache() && remainder > 0)
                {
                    CoinCompressor cc = null;
                    if (cache.ContainsRemainder(remainder) == false)
                    {
                        cc = new CoinCompressor();
                        GenerateCache(value, remainder, 0, cc);
                        cc.Finished();
                        cache.AddCombinationsToCache(remainder, cc);
                    }
                    else
                    {
                        cc = cache.GetCompressor(remainder);
                    }

                    /*
                    foreach (var c in new CoinDecompressor(cc))
                    {
                        arr[c + totalCoins]++;
                    }
                    */
                    
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
        
        void GenerateCache(int value, int remainder,int totalCoins,CoinCompressor res)
        {
            if (parent == null)
            {
                if (remainder > 0)
                {
                    if (Head == 1)
                        res.Add(remainder+totalCoins);
                    else if (Head == 2 && (remainder & (Head - 1)) == 0)
                    {
                        res.Add(remainder / Head + totalCoins);
                    }
                    else if (remainder % Head == 0)
                    {
                        res.Add(remainder / Head + totalCoins);
                    }
                }

                //if (remainder > 0 && remainder % Head == 0)
                  //  res.Add(remainder / Head + totalCoins);
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