using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT.Api.Database.Models;

public class Transaction : BaseEntity
{
    [Column(TypeName = "DECIMAL(18,2)")]
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public int? CategoryId { get; set; }

    [MaxLength(500)]
    [Column(TypeName = "VARCHAR(500)")]
    public string Description { get; set; }

    public virtual Category Category { get; set; }
}
