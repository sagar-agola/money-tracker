using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MT.Api.DataTransferModels.Pagination;

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
}
