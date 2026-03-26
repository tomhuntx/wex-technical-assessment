namespace CardApi.Models;

public class Card
{
    public Guid Id { get; set; }
    public decimal CreditLimit { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}
