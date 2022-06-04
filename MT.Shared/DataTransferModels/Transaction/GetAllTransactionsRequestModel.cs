using System;
using MT.Shared.DataTransferModels.Pagination;

namespace MT.Shared.DataTransferModels.Transaction;

public class GetAllTransactionsRequestModel : PaginationBaseRequestModel
{
    public decimal? MinAmount { get; set; }
    public decimal? MaxAmount { get; set; }

    public DateTime? MinTransactionDate { get; set; }
    public DateTime? MaxTransactionDate { get; set; }

    public int? CategoryId { get; set; }
}
