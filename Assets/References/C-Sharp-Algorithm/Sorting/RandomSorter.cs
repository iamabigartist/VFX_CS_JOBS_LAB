using System.Collections.Generic;
using Algorithms.Common;
using UnityEngine;
using Random = System.Random;
namespace Algorithms.Sorting
{
    public static class RandomSorter
    {
        public static void RandomSort<T>(this IList<T> collection, int times = 1000)
        {
            Debug.Log( collection.Count );
            var g = new Random();
            int count = collection.Count;
            for (int i = 0; i < times; i++)
            {
                collection.Swap( g.Next( 0, count ), g.Next( 0, count ) );
            }
        }

        public static void RandomSort(this float[] collection, int times = 1000)
        {
            var g = new Random();
            int count = collection.Length;
            for (int i = 0; i < times; i++)
            {
                collection.Swap( g.Next( 0, count ), g.Next( 0, count ) );
            }
        }

    }
}
