using Microsoft.EntityFrameworkCore;
using MT.Api.Database.Models;

namespace MT.Api.Database;

public class MoneyTrackerDbContext : DbContext
{
    public MoneyTrackerDbContext(DbContextOptions<MoneyTrackerDbContext> options) : base(options)
    { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
