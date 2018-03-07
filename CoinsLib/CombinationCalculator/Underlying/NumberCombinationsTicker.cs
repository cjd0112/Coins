using System;

namespace CoinsLib.CombinationCalculator.Underlying
{
    /*
     * very simple class - on every increment
     * if it has been 'hit'  ... is an increment of 'X' based on its start number
     * it outputs the number of times it has been "hit"
     * so - if startNumber is, say, '7' (because parent is doing combination (5,2))
     * and this unit is '2' then it will tick every 2nd increment from '7' and
     * GetValue() will return how many times it has hit
     */
    /// <summary>
    /// Simple class to keep track of number of times this 'Unit' (within a combination) has been 'hit'. 
    /// Starts with startNumber which represents the minimum number from a combination 
    /// i.e.m for '6,2,1' would be 9
    /// after the startNumber is triggered
    /// we return true from Increment function every time our unit triggers
    /// and the number returned from GetValue() increases by '1'. 
    /// The result is that when NCT is combined 
    /// we get the MAX number of each unit after increment.
    /// This is necessary input to the 'EvenFactor' Optimized way of calculating combos. 
    /// </summary>
    public class NumberCombinationsTicker
    {
        public NumberCombinationsTicker(int units,int startNumber)
        {
            multiplesOf = units;
            this.startNumber = originalStartNumber = startNumber;
        }

        private int originalStartNumber; // useful for debugging only;
        int multiplesOf;
        int startNumber;
        int value;

        private int numberToTrack;

        // returns 0 if no trigger
        // or the number if we have triggered 
        public bool Increment()
        {
            numberToTrack++;
            if (numberToTrack - startNumber != multiplesOf)
                return false;
            else
            {
                startNumber += multiplesOf;
                value++;
                return true;
            }
        }

        public int GetValue()
        {
            return value;
        }

        public int GetMultiple()
        {
            return multiplesOf;
        }

        public (Int32, Int32) VM()
        {
            return (GetValue(), GetMultiple());
        }

    }

}
