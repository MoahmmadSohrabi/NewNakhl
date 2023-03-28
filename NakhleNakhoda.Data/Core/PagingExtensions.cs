using Microsoft.EntityFrameworkCore;
using NakhleNakhoda.Common;

namespace NakhleNakhoda.Data.Core
{
    public static class PagingExtensions
    {
        #region IQueryable<T> extensionss

        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
        }

        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int? page)
        {
            int currentPageIndex = page.HasValue ? page.Value : 1;
            return await ToPagedListAsync(source, currentPageIndex - 1, Constants.DefaultPageSize);
        }

        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
                throw new ArgumentOutOfRangeException("index", "Value can not be below 0.");

            if (pageSize < 1)
                throw new ArgumentOutOfRangeException("pageSize", "Value can not be less than 1.");

            int realTotalCount = source == null ? 0 : await source.CountAsync();

            var result = new PagedList<T>(source!, pageIndex, pageSize, realTotalCount);

            if (realTotalCount > 0)
                result.AddRange(await source!.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync());

            return result;
        }

        public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> source)
        {
            int realTotalCount = source == null ? 0 : await source.CountAsync();

            var result = new PagedList<T>(source!, 0, realTotalCount, realTotalCount);

            if (realTotalCount > 0)
                result.AddRange(await source!.ToListAsync());

            return result;
        }


        #endregion

        #region IEnumerable<T> extensions

        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source.AsQueryable(), pageIndex, pageSize);
        }


        #endregion
    }
}
