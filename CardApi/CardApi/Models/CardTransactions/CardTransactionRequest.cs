namespace CardApi.Models.CardTransactions;

public class CardTransactionRequest
{
    public Guid CardId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset TransactionDate { get; set; }
    public decimal Amount { get; set; }
}
