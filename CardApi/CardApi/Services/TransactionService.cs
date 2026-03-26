using CardApi.Data;
using CardApi.Models;

namespace CardApi.Services;

public class TransactionService(CardDbContext context)
{
    /// <summary>
    /// Create a new transaction for a given card Id
    /// </summary>
    /// <param name="cardId">GUID of card used in transaction</param>
    /// <param name="request">Details of transaction in request body</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<CardTransaction> CreateTransaction(Guid cardId, CardTransactionRequest request)
    {
        if (request.Amount <= 0)
            throw new ArgumentException("Transaction amount must be greater than zero.");

        var transaction = new CardTransaction
        {
            Id = Guid.NewGuid(),
            CardId = cardId,
            Description = request.Description,
            TransactionDate = request.TransactionDate,
            Amount = request.Amount
        };

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        return transaction;
    }
}
