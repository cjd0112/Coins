using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class CalculationGrid
    { 
        private IList<CalculationNode> forest = new List<CalculationNode>();

        public String ComboKey(CalculationNode n)
        {
            return n.GetCoin().GenerateMyUnits().Aggregate("", (x, y) => x += y);
        }
        
        private CalculationGridDebugger debugger;

        public void StartDebug(CalculationNode n, int value)
        {
            debugger.StartDebug(n,value);
        }

        public void Debug(CalculationNode n, int noCoins, int noCombinations)
        {
            debugger.Debug(n,noCoins,noCombinations);
        }

        public void EndDebug(CalculationNode n)
        {
            debugger.EndDebug(n);
        }

        private IEnumerable<CalculationNode> allNodes;

        public int MaxValue;
        
        public CalculationGrid(IEnumerable<Coin> allCombinations,int maxValue)
        {
            MaxValue = maxValue;
            IEnumerable<CalculationNode> GetLevel(IEnumerable<CalculationNode> nodes,int level)
            {
                if (nodes.First().Depth == level)
                    return nodes;
                return GetLevel(nodes.SelectMany(x => x.GetChildren()), level);
                
            }
            void AddToForest(CalculationNode c,int thisLevel)
            {
                if (thisLevel == 1)
                    forest.Add(c);
                else
                    GetLevel(forest,thisLevel-1).Where(x=>x.IsParent(c)).ForEach(n=>n.AddChild(c));
            }

            CalculationNode Select(Coin c, CalculationGrid grid)
            {
                return new CalculationNode(c, grid);
            }
            
            // take all combinations group by node-count, sort ascending and add-to forest
            // i.e., do lowest order - '1','2' before '1->6' etc. before '2->6->10' 
            allCombinations
                .GroupBy(x => x.NodeCount())
                .OrderBy(x => x.Key)                
                .ForEach(x=>x.ForEach(n=>AddToForest(Select(n,this),x.Key)));

            allNodes = forest.SelectMany(x => x.GetAllNodes()).ToArray();
        }

        public IEnumerable<CalculationNode> GetAllNodes()
        {
            return allNodes;
        }
        
        public Int64 CalculateTotalCoins(int value)
        {
            Int64 res = 0;
            allNodes.Where(x => x.Depth == 100).ForEach(x=>x.InitializeCache(value));
            allNodes.ForEach(x=>x.Process(value,ref res,value,new Stack<int>()));
            //allNodes.Where(x=>x.SupportsCache()).ForEach(x=>x.ProcessCache(value,results));
            return res;
        }

    }
}