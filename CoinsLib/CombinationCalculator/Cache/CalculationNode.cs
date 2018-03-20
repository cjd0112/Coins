using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Schema;
using System.Numerics;

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
        }

        private int maxValue;
        public void InitializeCache(int maxValue)
        {
            this.maxValue = maxValue + 1;
            cache = new int[this.maxValue][];
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

        public void Process(int value, ref Int64 total,  int remainder, Stack2 state)
        {
            if (remainder != 0)
            {
                if (parent == null)
                {
                    if (remainder > 0)
                    {                        
                        if (remainder % Head == 0)
                        {
                            var tc = remainder / Head + state.Sum();
                            if (tc % 2 == 0)
                            {
                                state.Push(remainder/Head);
                                total += new ShareCoinsEvenly(100).WaysToShare(state);
                                state.Pop();
                            }
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
                        cache[remainder][state.Sum()]++;
                    }
                    else
                    {
                        for (int i = 1; i <=remainder/Head;i++)
                        {
                            state.Push(i);
                            parent.Process(value, ref total, remainder - i * Head, state);
                            state.Pop();
                        }                        
                    }
                }
            }
        }
        
        IEnumerable<(int remainder, IEnumerable<(Vector<short> numberCoinsInCombo,Vector<short> count)> totalCoins)> CompressCache()
        {
            for (int i = 0; i < maxValue; i++)
            {
                var totalCoinList = cache[i];
                if (totalCoinList != null)
                {
                    int cnt = 0;
                    for (int q = 0; q < maxValue; q++)
                    {
                        if (totalCoinList[q] != 0)
                        {
                            cnt++;
                        }
                    }
                    if (cnt > 0)
                    {
                        (Vector<short>,Vector<short>)[] arr = new (Vector<short>,Vector<short>)[cnt];
                        int cnt2 = 0;
                        for (int q = 0; q < maxValue; q++)
                        {
                            if (totalCoinList[q] != 0)
                                arr[cnt2++] = (new Vector<short>((short)q), new Vector<short>((short)totalCoinList[q]));

                        }
                        yield return (i, arr);
                    }
                }
            }
        }


    
        
        public void ProcessCache(int value, Int64[] arr)
        {
            if (SupportsCache() == false)
                throw new Exception("Doesn't support cache");

            List<Vector<short>> thisCoins = new List<Vector<short>>();


            var mask = new Vector<short>(0);

            var compress = CompressCache().ToArray();
            foreach (var i in compress)
            {
                thisCoins.Clear();
                
                GenerateCache3(value, i.remainder, 0, thisCoins);

                foreach (var c in thisCoins)
                {
                    foreach (var p in i.totalCoins)
                    {                        
                        var zeros = Vector.Equals(c, mask);

                        var cs =Vector.ConditionalSelect(zeros, mask, p.numberCoinsInCombo);

                        var q = cs + c;                        
                    
                        for (int n = 0; n < Vector<ushort>.Count; n++)
                        {
                            if (q[n] != 0)
                                arr[q[n]] += p.count[0];

                        }
                    }
                }
            }            
        }
        
       

        
        
        void GenerateCache3(int value, int remainder,int totalCoins,List<Vector<short>> res)
        {
            if (Depth != 2)
                throw new Exception("Only depth-2 supported currently for vectors ... ");

            if (Head == 2 && parent.Head == 1 && remainder >= 48)
            {
            }

            if (remainder > 0)
            {
                var totalCnt = remainder / Head;

                var vectorCnt = 1 +totalCnt / Vector<short>.Count;
                 
                var totalCoinCount = 1;


                for (short i = 0; i < vectorCnt; i++)
                {
                    var tmpArray = new short[Vector<short>.Count];

                    for (int q = 0; q < Vector<short>.Count && totalCoinCount <= totalCnt ; q++,totalCoinCount++)
                    {
                        totalCoins++;
                        var childRemainder = remainder - totalCoinCount * Head;
                        if (childRemainder > 0 && childRemainder % parent.Head == 0)
                            tmpArray[q] = (short) (totalCoins + (childRemainder / parent.Head));
                        else
                            tmpArray[q] = 0;
                    }
                    res.Add(new Vector<short>(tmpArray));
                }
            }

        }
    }
}