using System;
using System.ComponentModel.DataAnnotations;

namespace MT.Shared.DataTransferModels.Transaction;

public class SaveTransactionModel
{
    public string Id { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    public decimal Amount { get; set; }

    public DateTime? TransactionDate { get; set; }

    public string CategoryId { get; set; }

    [MaxLength(500, ErrorMessage = "Description cannot be more than 500 characters")]
    public string Description { get; set; }
}
