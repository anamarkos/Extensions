using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class ForEachExtensions
    {
        /// <summary>
        /// Usage: values.ForEachWithIndex((item, idx) => Console.WriteLine("{0}: {1}", idx, item));
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="handler"></param>
        public static void ForEachWithIndex<T>(this IEnumerable<T> enumerable, Action<T, int> handler)
        {
            int idx = 0;
            foreach (T item in enumerable)
                handler(item, idx++);
        }
    }

}
