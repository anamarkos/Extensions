using System;
using System.Collections.Generic;

namespace Extensions
{
    /// <summary>
    /// Extension class helping with LINQ queries.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Using this extension when we need to use list's index and value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ie"></param>
        /// <param name="action"></param>
        public static void Each<T>(this IEnumerable<T> ie, Action<T, int> action)
        {
            var i = 0;
            foreach (var e in ie) action(e, i++);
        }
    }
}
