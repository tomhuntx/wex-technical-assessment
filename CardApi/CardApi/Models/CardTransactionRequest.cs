namespace CardApi.Models;

public class CardTransactionRequest
{
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset TransactionDate { get; set; }
    public decimal Amount { get; set; }
}
