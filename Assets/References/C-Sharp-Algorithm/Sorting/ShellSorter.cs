using System.Collections.Generic;
using Algorithms.Common;
namespace Algorithms.Sorting
{
    public static class ShellSorter
    {
        public static void ShellSort<T>(this IList<T> collection, Comparer<T> comparer = null)
        {
            comparer ??= Comparer<T>.Default;
            collection.ShellSortAscending( comparer );
        }

        public static void ShellSort(this float[] collection)
        {
            collection.ShellSortAscending();
        }

        /// <summary>
        ///     Public API: Sorts ascending
        /// </summary>
        public static void ShellSortAscending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            bool flag = true;
            int d = collection.Count;
            while (flag || d > 1)
            {
                flag = false;
                d = (d + 1) / 2;
                for (int i = 0; i < collection.Count - d; i++)
                {
                    if (comparer.Compare( collection[i + d], collection[i] ) < 0)
                    {
                        collection.Swap( i + d, i );
                        flag = true;
                    }
                }
            }
        }


        public static void ShellSortAscending(this float[] collection)
        {
            bool flag = true;
            int d = collection.Length;
            while (flag || d > 1)
            {
                flag = false;
                d = (d + 1) / 2;
                for (int i = 0; i < collection.Length - d; i++)
                {
                    if (collection[i + d] < collection[i])
                    {
                        collection.Swap( i + d, i );
                        flag = true;
                    }
                }
            }
        }

        /// <summary>
        ///     Public API: Sorts descending
        /// </summary>
        public static void ShellSortDescending<T>(this IList<T> collection, Comparer<T> comparer)
        {
            bool flag = true;
            int d = collection.Count;
            while (flag || d > 1)
            {
                flag = false;
                d = (d + 1) / 2;
                for (int i = 0; i < collection.Count - d; i++)
                {
                    if (comparer.Compare( collection[i + d], collection[i] ) > 0)
                    {
                        collection.Swap( i + d, i );
                        flag = true;
                    }
                }
            }
        }
    }
}
