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

        private int[] minValueArray;

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

            minValueArray = new int[Grid.MaxValue];

            for (int i = 0; i < Grid.MaxValue; i++)
            {
                minValueArray[i] = coin.GetMinimumCoins(i);
            }
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

        public void Process(int value, Int64[] arr, int remainder, int totalCoins,int maxCoins)
        {
            if (minValueArray[remainder] > maxCoins)
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
                            cache[remainder] = new int[maxValue];

                        }
                        cache[remainder][totalCoins]++;
                    }
                    else
                    {
                        for (int i = 1; i <=remainder/Head;i++)
                        {
                            totalCoins++;
                            parent.Process(value,arr, remainder - i * Head, totalCoins,maxCoins);
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


        IEnumerable<(int remainder, IEnumerable<(int numberCoinsInCombo,int count)> totalCoins)> CompressCache2()
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
                        (int,int)[] arr = new (int,int)[cnt];
                        int cnt2 = 0;
                        for (int q = 0; q < maxValue; q++)
                        {
                            if (totalCoinList[q] != 0)
                                arr[cnt2++] = (q, totalCoinList[q]);

                        }
                        yield return (i, arr);
                    }
                }
            }
        }

        
        
        public void ProcessCache(int value, Int64[] arr,int maxCoins)
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
        
        public void ProcessCache3(int value, Int64[] arr,int maxCoins)
        {
            if (SupportsCache() == false)
                throw new Exception("Doesn't support cache");

            List<Vector<short>> thisCoins = new List<Vector<short>>();

            var compress = CompressCache().ToArray();
            foreach (var i in compress)
            {
                thisCoins.Clear();
                
                GenerateCache3(value, i.remainder, 0, thisCoins);

                //GenerateCache2(value,i.remainder,0,thisCoins,maxCoins);
                
                foreach (var c in thisCoins.SelectMany(x=>new []{x[0],x[1],x[2],x[3],x[4],x[5],x[6],x[7],x[8],x[9],x[10],x[11],x[12],x[13],x[14],x[15]}))
                {
                    if (c != 0)
                    {
                        foreach (var p in i.totalCoins)
                        {
                            //arr[p.numberCoinsInCombo + c] += p.count;
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
                        break;
                    }

                    parent.GenerateCache2(value, remainder - i * Head, totalCoins,res,maxCoins);
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