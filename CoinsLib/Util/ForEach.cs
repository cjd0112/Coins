using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoinsLib.Util
{
    public static class FE
    {
        public static void ForEach<T>(this IEnumerable<T> n,Action<T> g)
        {
            foreach (var c in n)
            {
                g(c);
            }
        }

        public static T Head<T>(this List<T> foo)
        {
            if (foo.Count > 0)
                return foo[0];
            return default(T);
        }

        public static List<T> Tail<T>(this List<T> foo)
        {
            if (foo.Count == 0)
                return Enumerable.Empty<T>().ToList();

            return foo.GetRange(1, foo.Count - 1);
        }
    }
}
