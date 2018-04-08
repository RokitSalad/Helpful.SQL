using System;
using System.Collections.Generic;
using System.Linq;

namespace Helpful.SQL
{
    internal static class ListExtensions
    {
        internal static IEnumerable<IList<T>> ToPagedList<T>(this IEnumerable<T> originalList, int pageSize)
        {
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "The parameter 'pageSize' cannot be less than 1.");
            }
            int count = 0;
            IList<T> currentPage = new List<T>();
            foreach (T item in originalList)
            {
                count++;
                if (count > pageSize)
                {
                    yield return currentPage;
                    count = 0;
                    currentPage = new List<T> { item };
                    continue;
                }
                currentPage.Add(item);
            }
            if (currentPage.Any())
            {
                yield return currentPage;
            }
        }
    }
}
