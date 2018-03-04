using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using CoinsLib.Util;

namespace CoinsLib.CombinationCalculator
{
    public class Coin
    {
        public readonly Int32 Units;
        public readonly String Name;
        public  Coin Next;
        public Coin(Int32 units,String name,Coin next)
        {
            Units = units;
            Name = name;
            Next = next;
        }

        public bool IsLeaf()
        {
            return Next == null;
        }

        public override bool Equals(object obj)
        {
            return ((Coin) obj).Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public Coin ShallowCopy()
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


        public bool RequiresBruteForceCalculator(int parentUnits = 0)
        {
            if (parentUnits == 0 || parentUnits % Units == 0)
                return Next.RequiresBruteForceCalculator(Units);
            return false;
        }

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

        public int NodeCount()
        {
            if (Next == null)
                return 1;
            return 1 + Next.NodeCount();

        }
    }

    public class CoinFactory
    {
        public static Coin GenerateCoinStatic()
        {

            return new Coin(54,"HC",new Coin(48,"FL",new Coin(24,"SH",new Coin(12, "6d", new Coin(6, "3d", new Coin(2, "1d", new Coin(1, ".5d", null)))))));
        }

        public static Coin GenerateTestCoin(List<Int32> factors)
        {
            if (factors.Count == 1)
            {
                return new Coin(factors.Head(),"dontcare",null);
            }
            else
            {
                return new Coin(factors.Head(),"dont care",GenerateTestCoin(factors.Tail()));
            }

            
        }
    }
}
