using CardApi.Data;
using CardApi.Models.CardTransactions;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Services;

public class TransactionService(CardDbContext context)
{
    public async Task<List<CardTransaction>> GetAllTransactions()
    {
        return await context.Transactions.ToListAsync();
    }

    /// <summary>
    /// Create a new transaction for a given card Id
    /// </summary>
    /// <param name="cardId">GUID of card used in transaction</param>
    /// <param name="request">Details of transaction in request body</param>
    /// <returns>Transaction on success</returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<CardTransaction> CreateTransaction(CardTransactionRequest request)
    {
        if (request.Amount <= 0)
            throw new ArgumentException("Transaction amount must be greater than zero.");

        var transaction = new CardTransaction
        {
            Id = Guid.NewGuid(),
            CardId = request.CardId,
            Description = request.Description,
            TransactionDate = request.TransactionDate,
            Amount = request.Amount
        };

        context.Transactions.Add(transaction);
        await context.SaveChangesAsync();

        return transaction;
    }

    /// <summary>
    /// Get a transaction by its ID
    /// </summary>
    /// <param name="transactionId">GUID of transaction</param>
    /// <returns>Transaction or null if not found</returns>
    public async Task<CardTransaction?> GetTransaction(Guid transactionId)
    {
        return await context.Transactions
                             .FirstOrDefaultAsync(t => t.Id == transactionId);
    }

    /// <summary>
    /// Convert a transaction by its given exchange rate
    /// </summary>
    /// <param name="transaction">Transaction object</param>
    /// <param name="exchangeRate">Exchange rate</param>
    /// <param name="currency"></param>
    /// <returns></returns>
    public async Task<CardTransactionResponse> ConvertTransactionByExchangeRate(CardTransaction transaction, decimal exchangeRate, string currency)
    {
        var convertedAmount = transaction.Amount * exchangeRate;

        var result = new CardTransactionResponse
        {
            Id = transaction.Id,
            Description = transaction.Description,
            TransactionDate = transaction.TransactionDate,
            OriginalAmount = transaction.Amount,
            ExchangeRate = exchangeRate,
            ConvertedAmount = Math.Round(convertedAmount, 2),
            Currency = currency
        };

        return result;
    }
}
