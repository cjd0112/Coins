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
            return grid.GetAllNodes().Where(x => x.Depth > 1).ToArray().Select(x =>
                    new MergedStateForNodesUpToLevelTwo(x).CollectCoinStateUpToLevelTwo((int) GrandNumber))
                .ToArray();
        }

        MergedGlobalState ThirdGenerateLevelTwoNumberCoinsAndUpdateGlobalState(IEnumerable<MergedStateForNodesUpToLevelTwo> mergedlevelTwoState)
        {
            var globalState = new MergedGlobalState();
            mergedlevelTwoState.ForEach(x=>x.FinishLevelTwoProcessingAndUpdateGlobalState(globalState));
            return globalState;
        }

        long FourthProcessMergedGlobalStateAndReturnFinalResult(MergedGlobalState globalState)
        {
            GrandTotal += globalState.ProcessCoinMapAndReturnFinalResult();
            return GrandTotal;
        }
    }
}
