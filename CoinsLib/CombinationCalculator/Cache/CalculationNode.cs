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

        private Dictionary<(int, int), Int64[]> cache;
        
        public CalculationNode(Coin c,CalculationGrid grid)
        {
            coin = c;
            SumOfUnits = c.GenerateMyUnits().Sum();
            Grid = grid;

            ComboKey = Grid.ComboKey(this);

            if (Depth == 2)
            {
                 cache = new Int64[grid.MaxValue][];
            }
        }

        private bool UseCache()
        {
            return cache != null;
        }
        private Int64[] GetCache(int value)
        {
            if (cache == null)
                return null;
            return cache[value];
        }

        private Int64[] UpdateCache(int value, Int64[] arr)
        {
            cache[value] = arr;
            return arr;
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
                if (cache != null)
                {
                    var foo = GetCache(remainder);
                    if (foo == null)
                    {
                        var lst = new List<Int64>();
                        GenerateCache(value, remainder, totalCoins, lst);

                        foo = UpdateCache(remainder,lst.ToArray());
                    }

                    foreach (var c in foo)
                    {
                        arr[c]++;
                    }
                    return;

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
        
        public void GenerateCache(int value, int remainder,int totalCoins,List<Int64> res)
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