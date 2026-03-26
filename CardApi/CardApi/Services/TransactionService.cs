using CardApi.Data;
using CardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Services;

public class TransactionService(CardDbContext context)
{
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
