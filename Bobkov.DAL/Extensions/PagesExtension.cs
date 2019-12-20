using System.Collections.Generic;
using System.Linq;

namespace Bobkov.DAL.Extensions
{
    public static class PagesExtension
    {
        public static IEnumerable<TSource> GetPage<TSource>(this IEnumerable<TSource> source, int pageNumber, int pageSize)
        {
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        public static IEnumerable<IEnumerable<T>> PagesOf<T>(this IEnumerable<T> source, int pageSize)
        {
            var list = source.ToList();
            var pageCount = (list.Count + pageSize - 1) / pageSize;

            var pageRange = Enumerable.Range(1, pageCount);
            return pageRange.Select((page, index) => list.Skip(index * pageCount).Take(pageCount));
        }
    }
}
