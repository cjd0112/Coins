using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    /// <summary>
    /// Recursive structure representing coins!
    /// 
    /// Contains functionality around Coin definitions
    /// and gives the capability to e.g., produce 'all-combinations' of this coin.
    /// </summary>
    public class Coin
    {
        /// <summary>
        /// The units or multiples this Coin supports
        /// given in terms of the lowest Coin in the system
        /// i.e., if .5d = 1 then 3d Units would be 6
        /// </summary>
        public readonly Int32 Units;
        
        /// <summary>
        /// Tag for recognizing the coin - not really used. 
        /// </summary>
        public readonly String Name;
        
        /// <summary>
        /// In-built list for easy recursion
        /// </summary>
        public  Coin Next;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="units">Units for this coin - in multiples of lowest coin</param>
        /// <param name="name">name of coin</param>
        /// <param name="next">Next coin LOWER DOWN in the series</param>
        public Coin(Int32 units,String name,Coin next)
        {
            Units = units;
            Name = name;
            Next = next;
        }

        Coin ShallowCopy()
        {
            var n = MemberwiseClone() as Coin;
            n.Next = null;
            return n;
        }

        Coin DeepCopy()
        {
            if (Next == null)
                return ShallowCopy();
            var foo = ShallowCopy();
            foo.Next = Next.DeepCopy();
            return foo;
        }

        /// <summary>
        /// pretty-print series of coins.
        /// </summary>
        /// <param name="w">writer</param>
        /// <param name="depth"></param>
        public void Print(TextWriter w,int depth=0)
        {
            w.Write($"{new String('\t',depth)}");
            w.WriteLine($"{Units}");
            if (Next != null)
                Next.Print(w,depth+1);

        }


        
        /*
         * create tree, depth-first and return list of all child-nodes + this node
         * so that parent can connect to all children as well....
         *  e.g., for a three-level tree you return a list ...
         * x -> y -> Z
         * x1->y1
         * x2->z1
         * y2
         * z2
         * which covers all combinations
         * * Note that 'clone's are used because each combination has to have it's own state
         */

        /// <summary>
        ///  create depth-first tree and return list of all child-nodes + this node
        /// so that the parent can connect to all children as well.
        /// E.g., for a three-level tree you return a list ... 
        /// x -> y -> z
        /// x1 -> y1
        /// x2->z1
        /// y2
        /// z2
        /// (where the numbers indicate they are COPIES of the original coin)
        /// </summary>
        /// <returns>List of all combinations of coins</returns>
        public IEnumerable<Coin> AllCombinations()
        {
            if (Next == null)
                yield return DeepCopy();
            else
            {
                yield return ShallowCopy();

                var z = Next.AllCombinations().ToArray();
                foreach (var c in z)
                {
                    yield return c;
                }

                foreach (var c in z)
                {
                    var me = ShallowCopy();
                    me.Next = c.DeepCopy();
                    yield return me;
                }
            }

        }


        /// <summary>
        /// You can optimize calculations where the combination of coins has
        /// only units that are multiples of previous units. 
        /// 
        /// This function says where this is not the case so you can 
        /// use an un-optimized version (i.e., Half-Crown combinations)
        /// </summary>
        /// <param name="parentUnits"></param>
        /// <returns></returns>
        public bool RequiresUnevenFactorCalculator(int parentUnits = 0)
        {
            if (parentUnits == 0 || parentUnits % Units == 0)
            {
                if (Next != null)
                    return Next.RequiresUnevenFactorCalculator(Units);
                return false;
            }
            return true;
        }

        /// <summary>
        /// returns a flat list of units in the Coin list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Int32> GenerateMyUnits()
        {
            if (Next == null)
                yield return Units;

            else
            {
                yield return Units;
                foreach (var c in Next.GenerateMyUnits())
                    yield return c;
            }
        }

        /// <summary>
        ///  how many nodes ... 
        /// </summary>
        /// <returns></returns>
        public int NodeCount()
        {
            if (Next == null)
                return 1;
            return 1 + Next.NodeCount();

        }
    }

   
}
