using System;
using System.Collections.Generic;
using System.Text;

namespace Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Creates a range of values given start-end indexes.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static IEnumerable<long> CreateRange(long start, long count)
        {
            var limit = start + count;

            while (start < limit)
            {
                yield return start;
                start++;
            }
        }
    }
}
