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
    }
}
