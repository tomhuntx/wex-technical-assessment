namespace CardApi.Models.Cards;

public class CardBalanceResponse
{
    public Guid CardId { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal TotalOfTransactions { get; set; }
    public decimal AvailableBalance { get; set; }
    public string? Currency { get; set; }
    public decimal? AvailableBalanceConverted { get; set; }
    public decimal? ExchangeRate { get; set; }
}