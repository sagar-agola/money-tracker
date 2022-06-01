using System;

namespace MT.Api.DataTransferModels.Transaction;

public class TransactionModel
{
    public int Id { get; set; }
    public string HashId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string RelativeTransactionDate { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
}
