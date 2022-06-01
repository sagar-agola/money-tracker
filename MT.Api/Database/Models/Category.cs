using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MT.Api.Database.Models;

public class Category : BaseEntity
{
    [Required]
    [MaxLength(100)]
    [Column(TypeName = "VARCHAR(100)")]
    public string Title { get; set; }
}
