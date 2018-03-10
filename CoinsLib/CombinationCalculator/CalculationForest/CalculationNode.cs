using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    
    public abstract class CalculationNode
    {
        protected Coin coin;

        public int Depth => coin.NodeCount();

        protected int Head => coin.Units;

        protected CalculationNode parent;
        
        protected CalculationNode(Coin c)
        {
            coin = c;
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

        public int GetRootUnit()
        {
            if (parent == null)
                return Head;
            return parent.GetRootUnit();
        }

        public bool IsParent(CalculationNode candidate)
        {
            return coin.GenerateMyUnits().SequenceEqual(candidate.TailUnits());
        }
            
        List<CalculationNode> children = new List<CalculationNode>();

        public abstract Int64 CalculateTotalCoins(int valuetoCalculate, Int64[] arr, CalcState parentState=null, int depth = 1);

    }
}