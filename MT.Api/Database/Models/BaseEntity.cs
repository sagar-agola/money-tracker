using System;
using Microsoft.EntityFrameworkCore;

namespace MT.Api.Database.Models;

[Index(nameof(DeletedAt))]
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
