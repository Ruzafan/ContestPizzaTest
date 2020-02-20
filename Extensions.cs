using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GoogleContestTest
{
    public static class Extensions
    {
        public static IEnumerable<IEnumerable<T>> Combinaciones<T>(this IEnumerable<T> elementos, long n)
        {
            return n == 0 ? new[] { new T[0] } :
                elementos.SelectMany((e, i) =>
                elementos.Skip(i + 1).Combinaciones(n - 1).Select(c => (new[] { e }).Concat(c)));
        }
    }
}
