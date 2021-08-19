using System.Collections.Generic;
using Algorithms.Common;
using UnityEngine;
namespace Algorithms.Sorting
{
    public static class RandomSorter
    {
        public static void RandomSort<T>(this IList<T> collection, int times = 1000)
        {
            int count = collection.Count;
            for (int i = 0; i < times; i++)
            {
                collection.Swap( Random.Range( 0, 100 ), Random.Range( 0, 100 ) );
            }
        }


    }
}
