using CardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Data;

public class CardDbContext(DbContextOptions<CardDbContext> options) : DbContext(options)
{
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<CardTransaction> Transactions => Set<CardTransaction>();

    // Define database schema
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CardDbContext).Assembly);
    }
}
