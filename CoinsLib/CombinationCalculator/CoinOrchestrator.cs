using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoinsLib.CombinationCalculator.Cache;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /// <summary>
    /// brings together all the pieces
    /// </summary>
    public class CoinOrchestrator
    {
        private CalculationGrid grid;
        private long GrandTotal = 0;
        private int GrandNumber = 0;
        public CoinOrchestrator(int value)
        {
            GrandNumber = value;
            grid = new CalculationGrid(MagicPurse.GenerateCoinStatic().AllCombinations().ToArray(), value);
        }

        public long Calculate()
        {
            GrandTotal += FirstGenerateBottomLevelWaysToShare();
            var mergedState = SecondGenerateStateForAllCoinsUpToLevelTwo();
            var globalState = ThirdGenerateLevelTwoNumberCoinsAndUpdateGlobalState(mergedState);
            GrandTotal += FourthProcessMergedGlobalStateAndReturnFinalResult(globalState);
            return GrandTotal;

        }

        long FirstGenerateBottomLevelWaysToShare()
        {
            return grid.GetAllNodes()
                .Where(x=>x.Depth == 1)
                .Sum(x =>x.CalculateBottomLayer(GrandNumber));
        }

        IEnumerable<MergedStateForNodesUpToLevelTwo>  SecondGenerateStateForAllCoinsUpToLevelTwo()
        {
            CalculationNode GetParentAtDepth2(CalculationNode n)
            {
                if (n.Depth == 2)
                    return n;
                return GetParentAtDepth2(n.GetParent());
            }

            // create a state object for each level 2 object
            var levelTwoNodes = grid.GetAllNodes().Where(x => x.Depth > 1).GroupBy(GetParentAtDepth2);

            // apply it to each parent (+ level 2 object itself) ... which will recurse to 
            // level 2 object collecting objects representing state of calculation along the way
            foreach (var c in levelTwoNodes)
            {
                var mergedState = new MergedStateForNodesUpToLevelTwo(c.Key);
                foreach (var q in c)
                {
                    q.CollectCoinStateUpToLevelTwo(GrandNumber,mergedState);
                }

                yield return mergedState;
            }
        }

        MergedGlobalState ThirdGenerateLevelTwoNumberCoinsAndUpdateGlobalState(IEnumerable<MergedStateForNodesUpToLevelTwo> mergedlevelTwoState)
        {
            var globalState = new MergedGlobalState();
            mergedlevelTwoState.ForEach(x=>x.FinishLevelTwoProcessingAndUpdateGlobalState(globalState));
            return globalState;
        }

        long FourthProcessMergedGlobalStateAndReturnFinalResult(MergedGlobalState globalState)
        {
            return globalState.ProcessCoinMapAndReturnFinalResult();
        }
    }
}
