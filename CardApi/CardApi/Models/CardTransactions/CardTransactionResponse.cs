namespace CardApi.Models.CardTransactions;

public class CardTransactionResponse
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset TransactionDate { get; set; }

    public decimal OriginalAmount { get; set; }
    public decimal ExchangeRate { get; set; }
    public decimal ConvertedAmount { get; set; }

    public string Currency { get; set; } = string.Empty;
}
