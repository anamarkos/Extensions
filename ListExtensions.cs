using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// LINQ Concat() method didn't trigger the CollectionChanged event. This method does.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="oc"></param>
        /// <param name="collection"></param>
        public static void AddRange<T>(this ObservableCollection<T> oc, IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            foreach (var item in collection)
            {
                oc.Add(item);
            }
        }
    }
}
