using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class MergedStateForNodesUpToLevelTwo
    {
        private CalculationNode level2Node;
        private Stack<int> state;
        private int finalNodeRemainderToCalculate;
        private Dictionary<int, List<long>> remainderCoinStateList;
        public MergedStateForNodesUpToLevelTwo(CalculationNode level2Node)
        {
            this.level2Node = level2Node;
            state = new Stack<int>();
            remainderCoinStateList = new Dictionary<int, List<long>>(level2Node.GetGrid().MaxValue);
        }

        public CalculationNode GetNode()
        {
            return level2Node;
        }

        public void Push(int i)
        {
            state.Push(i);
        }

        public int Pop()
        {
            return state.Pop();
        }

        // keep track of remainder at Level2 level in dictionary.
        // associated with remainder is all the Coin states that produced it. 
        // i.e., there can be many Coin comboxs - 6,2,1 ... 12,6,2,1, .. 24,12,6,2,1
        // that end up with the same final value to be processed by this 2,1 node (for example). 
        // here we ensure that we process each remainder only once and the attribute the 
        // results to the associated state. 
        public void SetFinalNodeRemainder(int remainder)
        {
            var arr = state.ToArray();
            Array.Sort(arr);
            if (remainderCoinStateList.TryGetValue(remainder,out var value))
                value.Add(IntArrayToLong.ConvertToLong(arr));
            else
            {
                var val = new List<long>();
                remainderCoinStateList[remainder] = val;
                val.Add(IntArrayToLong.ConvertToLong(arr));
            }
        }

       
        public void FinishLevelTwoProcessingAndUpdateGlobalState(MergedGlobalState globalState)
        {
            GetNode().FinishLevelTwoProcessingAndUpdateGlobalState(this,globalState);
        }

        public IEnumerable<(int remainder, List<long> coinState)> ExtractRemaindersAndCoinState()
        {
            foreach (var c in remainderCoinStateList)
            {
                yield return (c.Key, c.Value);
            }
        }

    }
}
