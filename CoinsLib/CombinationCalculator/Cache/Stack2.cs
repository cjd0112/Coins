using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinsLib.CombinationCalculator.Cache
{
    public class Stack2
    {
        List<int> myStack = new List<int>();
       

        public Stack2(params int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                Push(a[i]);
            }

        }

        public void Push(int i)
        {
            myStack.Add(i);
        }

        public int Pop()
        {
            var ret = myStack[myStack.Count - 1];
            myStack.RemoveAt(myStack.Count-1);
            return ret;
        }

        public int Peek()
        {
            return myStack[myStack.Count-1];
        }

        public void UpdateHead(int i)
        {
            myStack[myStack.Count-1] = i;
        }

        public int Sum()
        {
            return myStack.Sum();
        }

        public int Count()
        {
            return myStack.Count();
        }

        public (Stack2 even, Stack2 odd) SplitEvenOdd()
        {
            return (new Stack2(myStack.Where(x => x % 2 == 0).ToArray()), new Stack2(myStack.Where(x => x % 2 != 0)
                .ToArray()));
        }


        public IEnumerable<(int left, int right)> GetPairs()
        {
            if (myStack.Count() % 2 != 0)
                throw new Exception("Expecting even number of items");

            for (int i = 0; i < myStack.Count(); i+=2)
            {
                yield return (myStack[i], myStack[i + 1]);

            }
        }

        public bool Any()
        {
            return Count() > 0;
        }
    }
}
