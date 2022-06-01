using System;
using MT.Api.DataTransferModels.Pagination;

namespace MT.Api.DataTransferModels.Transaction;

public class GetAllTransactionsRequestModel : PaginationBaseRequestModel
{
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }

    public DateTime? MinTransactionDate { get; set; }
    public DateTime? MaxTransactionDate { get; set; }

    public int? CategoryId { get; set; }
}
