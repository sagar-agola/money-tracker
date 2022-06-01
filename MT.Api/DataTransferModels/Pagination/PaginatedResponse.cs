using System.Collections.Generic;

namespace MT.Api.DataTransferModels.Pagination;

public class PaginatedResponse<T> where T : class
{
    public List<T> Data { get; set; }
    public int TotalRecords { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
