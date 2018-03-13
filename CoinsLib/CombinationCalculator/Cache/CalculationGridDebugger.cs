using System;
using System.Collections.Generic;

namespace CoinsLib.CombinationCalculator.Cache
{
    /// <summary>
    /// collect output of calculation nodes in such a way 
    /// that a client can easily see what a particular operations outputs
    /// without impacting on efficiency (much)
    /// </summary>
    public class CalculationGridDebugger
    {
        private Func<String, int, bool> filter;
        private Action<DebugState> reporter;
        private bool debug = false;
        public CalculationGridDebugger(Func<String, int, bool> filter, Action<DebugState> reporter)
        {
            this.filter = filter;
            this.reporter = reporter;
            if (filter != null && reporter != null)
                debug = true;

        }

        private bool startCapture = false;
        private int value;
        public void StartDebug(CalculationNode n, int val)
        {
            if (debug)
            {
                startCapture = filter(n.ComboKey, value);
                value = val;
                debugState = new List<(int noCoins, int noCombinations)>();
            }
        }

        private List<(int noCoins, int noCombinations)> debugState;

        public void Debug(CalculationNode n, int noCoins, int noCombinations)
        {
            if (debug && startCapture)
            {
                debugState.Add((noCoins,noCombinations));
            }

        }

        public void EndDebug(CalculationNode n)
        {
            if (debug && startCapture)
            {
                reporter(new DebugState(n,value,debugState));
            }

        }
    }
}
