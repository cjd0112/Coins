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

        protected int SumOfUnits;

        public string ComboKey;

        protected CalculationGrid Grid;
        
        protected CalculationNode(Coin c,CalculationGrid grid)
        {
            coin = c;
            SumOfUnits = c.GenerateMyUnits().Sum();
            Grid = grid;

            ComboKey = Grid.ComboKey(this);
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

        protected 
            
        List<CalculationNode> children = new List<CalculationNode>();

        public abstract Int64 CalculateTotalCoins(int valuetoCalculate,  Int64[] arr, CalcState parentState=null, int depth = 1);

        public virtual int GetMaxRemainderForValue(int value)
        {
            if (parent == null)
                throw new Exception("Internal error - null parent should be overridden by CalculationNodeScalar ...");

            return parent.GetMaxRemainderForValue(value) - Head;
        }

        /// <summary>
        /// Given a value will return the maximum combinations that this Coin
        /// will return. 
        /// We get the maximum remainder from the tail of the list. 
        /// Then return how many times our units will go into that. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Max combinations for this value for this Coin </returns>
        public virtual int GetMaxCombinationsForValue(int value)
        {
            if (parent == null)
                throw new Exception("Internal error - null parent should be overridden by CalculationNodeScalar ...");

            return parent.GetMaxRemainderForValue(value) / Head;
        }

    }
}