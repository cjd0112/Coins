using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator.CalculationForest
{
    /// <summary>
    /// Takes a list of all combinations of Units 
    /// and turns into a forest where the 
    /// top nodes each each tree in the forest are 
    /// of cardinality '1' i.e., 60,48,24 etc., 
    /// and the branches have cardinality 'n+1'. 
    /// where the last Unit in each coin matches
    /// the parent 
    /// 6
    ///     -> 6->12
    ///     -> 6 -> 24
    /// 2
    ///     -> 2->6
    ///     -> 2->12 
    ///             -> 2->12->24
    /// etc., 
    /// 
    /// This way by pumping numbers through tops of trees
    /// you touch each possible combination when 
    /// a parent node 'triggers' 
    /// 
    /// </summary>
    public class CalculationGrid
    { 
        private IList<CalculationNode> forest = new List<CalculationNode>();
      
        public CalculationGrid(IEnumerable<Coin> allCombinations)
        {
            IEnumerable<CalculationNode> GetLevel(IEnumerable<CalculationNode> nodes,int level)
            {
                if (nodes.First().Depth == level)
                    return nodes;
                return GetLevel(nodes.SelectMany(x => x.GetChildren()), level);
                
            }
            void AddToForest(CalculationNode c,int parentLevel)
            {
                if (parentLevel == 1)
                    forest.Add(c);
                else
                    GetLevel(forest,parentLevel).Where(x=>x.IsParent(c)).ForEach(n=>n.AddChild(c));
            }            
            // take all combinations group by node-count, sort ascending and add-to forest
            // i.e., do lowest order - '1','2' before '1->6' etc. before '2->6->10' 
            allCombinations
                .GroupBy(x => x.NodeCount())
                .OrderBy(x => x.Key)                
                .ForEach(x=>x.ForEach(n=>AddToForest(new CalculationNodeWorker(n),x.Key)));
        }        
        
        
        public Int64 CalculateTotalCoins(Int64[] results,int value)
        {
            //return forest.Sum(x => x.CalculateTotalCoins(results, value,0));

            return 1;
        }

    }
    
    
}