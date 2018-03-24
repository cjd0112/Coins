using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    public static class CalculationNodeFunctions
    {
        public static long CalculateBottomLayer(this CalculationNode node,int grandNumber)
        {
            if (node.GetParent() != null)
                throw new ArgumentException("This function should only be called with parent-less node of depth 1");

            if (node.Depth != 1)
                throw new ArgumentException("This function should only be called with parent-less node of depth 1");


            if (grandNumber != 0 && grandNumber % node.GetHead() == 0)
            {
                return ShareCoinsEvenly.WaysToShare(new Stack<int>(new int[] {grandNumber / (int)node.GetHead()}));
            }

            return 0;
        }


        public static void CollectCoinStateUpToLevelTwo(this CalculationNode node, int remainder, MergedStateForNodesUpToLevelTwo sourceMergedStateForNodesUpToLevelTwo)
        {
            if (node.Depth == 2)
            {
                sourceMergedStateForNodesUpToLevelTwo.SetFinalNodeRemainder(remainder);
            }
            else
            {
                for (int i = 1; i <= remainder / node.GetHead(); i++)
                {
                    sourceMergedStateForNodesUpToLevelTwo.Push(i);
                    node.GetParent().CollectCoinStateUpToLevelTwo( remainder - i * node.GetHead(), sourceMergedStateForNodesUpToLevelTwo);
                    sourceMergedStateForNodesUpToLevelTwo.Pop();
                }
            }
        }

        /// <summary>
        /// process the 'rest' of our state - now that we have collected all common items
        /// that are affected by the bottom two results.   
        /// get the rest of the state and send it to the MergedGlobalState to get merged
        /// for final calculation of ways to share
        /// </summary>
        /// <param name="node"></param>
        /// <param name="localState"></param>
        /// <param name="globalState"></param>
        public static void FinishLevelTwoProcessingAndUpdateGlobalState(this CalculationNode node,
            MergedStateForNodesUpToLevelTwo localState, MergedGlobalState globalState)
        {
            if (node.Depth != 2)
                throw new ArgumentException($"Expected a node of Depth - 2 not {node.Depth}");

            var thisStack = new Stack<int>();
            foreach (var c in localState.ExtractRemaindersAndCoinState())
            {
                for (int i = 1; i <= c.remainder/ node.GetHead(); i++)
                {
                    thisStack.Push(i);
                    var level1Remainder= c.remainder - i * node.GetHead();
                    if (level1Remainder > 0 && level1Remainder% node.GetParent().GetHead() == 0)
                    {
                        // add however many coins we can make at level 1
                        thisStack.Push(level1Remainder/node.GetParent().GetHead());
                        globalState.Merge(c.coinState, thisStack.ToArray());
                        thisStack.Pop();
                    }

                    thisStack.Pop();
                }
            }
        }
    }
}
