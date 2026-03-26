using CardApi.Models.Cards;
using CardApi.Models.CardTransactions;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Data;

public class CardDbContext(DbContextOptions<CardDbContext> options) : DbContext(options)
{
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<CardTransaction> Transactions => Set<CardTransaction>();

    // Define database schema
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(builder =>
        {
            // Ensure correct decimal precision
            builder.Property(c => c.CreditLimit)
                   .HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<CardTransaction>(builder =>
        {
            // Ensure correct decimal precision
            builder.Property(t => t.Amount)
                   .HasColumnType("decimal(18,2)");

            // Define unique foreign key on CardId
            builder.HasOne<Card>()
                   .WithMany()
                   .HasForeignKey(t => t.CardId)
                   .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
