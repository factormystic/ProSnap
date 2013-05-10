using System;
using System.Collections.Generic;

namespace ProSnap
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Realize<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T el in source)
                action(el);

            return source;
        }

        public static IEnumerable<T> Realize<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var i = 0;
            foreach (T el in source)
                action(el, i++);

            return source;
        }
    }
}
