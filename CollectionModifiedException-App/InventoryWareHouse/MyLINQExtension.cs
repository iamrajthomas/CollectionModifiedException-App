using System;
using System.Collections.Generic;

namespace CollectionModifiedException_App.InventoryWareHouse 
{
    public static class MyLINQExtension
    {
        /// <summary>
        /// Returns distinct elements of the given source based one the key (property) selector.
        /// Usage:  for single property - source.DistinctBy(x => x.p1);
        ///         for multiple properties - source.DistinctBy( x=> new {x.p1, x.p2});
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TKey">The type of the key element.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keySelector">The key selector.</param>
        /// <returns>Sequence consisting distinct elements</returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
