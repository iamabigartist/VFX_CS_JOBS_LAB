using System.Collections.Generic;
using Algorithms.Common;
namespace Algorithms.Sorting
{
    public static class SelectionSorter
    {
        public static void SelectionSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            collection.SelectionSortAscending( comparer );
        }

        public static void SelectionSort(this float[] collection)
        {
            collection.SelectionSortAscending();
        }

        public static void SelectionSortAscending(this float[] collection)
        {
            int i;
            for (i = 0; i < collection.Length; i++)
            {
                int min = i;
                for (int j = i + 1; j < collection.Length; j++)
                {
                    if (collection[j] < collection[min])
                    {
                        min = j;
                    }
                }
                collection.Swap( i, min );
            }
        }

        /// <summary>
        ///     Public API: Sorts ascending
        /// </summary>
        public static void SelectionSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            int i;
            for (i = 0; i < collection.Count; i++)
            {
                int min = i;
                for (int j = i + 1; j < collection.Count; j++)
                {
                    if (comparer.Compare( collection[j], collection[min] ) < 0)
                    {
                        min = j;
                    }
                }
                collection.Swap( i, min );
            }
        }

        /// <summary>
        ///     Public API: Sorts ascending
        /// </summary>
        public static void SelectionSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            int i;
            for (i = collection.Count - 1; i > 0; i--)
            {
                int max = i;
                for (int j = 0; j <= i; j++)
                {
                    if (comparer.Compare( collection[j], collection[max] ) < 0)
                    {
                        max = j;
                    }
                }
                collection.Swap( i, max );
            }
        }
    }
}
