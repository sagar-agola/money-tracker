using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MT.Shared.DataTransferModels.Pagination;

namespace MT.Api.Helpers;

public static class CommonHelpers
{
    public static async Task<PaginatedResponse<T>> GetPaginatedResponse<T, Q>(
        IQueryable<T> baseQuery,
        Expression<Func<T, Q>> orderBy,
        bool isOrderByAsc,
        int pageSize,
        int pageNumber
    ) where T : class
    {
        baseQuery = isOrderByAsc ? baseQuery.OrderBy(orderBy) : baseQuery.OrderByDescending(orderBy);

        return new PaginatedResponse<T>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Data = await baseQuery
                .AsNoTracking()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(),
            TotalRecords = await baseQuery.CountAsync()
        };
    }

    public static string ToRelativeTime(this DateTime date)
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);
        double delta = Math.Abs(ts.TotalSeconds);

        if (delta < 1 * MINUTE)
        {
            return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
        }

        if (delta < 2 * MINUTE)
        {
            return "a minute ago";
        }

        if (delta < 45 * MINUTE)
        {
            return ts.Minutes + " minutes ago";
        }

        if (delta < 90 * MINUTE)
        {
            return "an hour ago";
        }

        if (delta < 24 * HOUR)
        {
            return ts.Hours + " hours ago";
        }

        if (delta < 48 * HOUR)
        {
            return "yesterday";
        }

        if (delta < 30 * DAY)
        {
            return ts.Days + " days ago";
        }

        if (delta < 12 * MONTH)
        {
            int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
            return months <= 1 ? "one month ago" : months + " months ago";
        }
        else
        {
            int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
            return years <= 1 ? "one year ago" : years + " years ago";
        }
    }
}
