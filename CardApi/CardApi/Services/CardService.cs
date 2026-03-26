using CardApi.Data;
using CardApi.Models.Cards;
using Microsoft.EntityFrameworkCore;

namespace CardApi.Services;

public class CardService(CardDbContext context)
{
    // Get ALL cards in database
    // Created for testing purposes and ease of use
    public async Task<List<Card>> GetAllCards()
    {
        return await context.Cards.ToListAsync();
    }

    /// <summary>
    /// Create a new card and save it to the db with a unique ID.
    /// </summary>
    /// <param name="request">Request body</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<Card> CreateCard(CardCreateRequest request)
    {
        if (request.CreditLimit <= 0)
            throw new ArgumentException("Credit limit must be greater than zero.");

        var card = new Card { Id = Guid.NewGuid(), CreditLimit = request.CreditLimit };
        context.Cards.Add(card);
        await context.SaveChangesAsync();
        return card;
    }

    /// <summary>
    /// Get card from database by its ID.
    /// </summary>
    /// <param name="cardId">Card GUID</param>
    /// <returns>Card object from database</returns>
    public async Task<Card?> GetCard(Guid cardId)
    {
        return await context.Cards.FindAsync(cardId);
    }

    /// <summary>
    /// Get the available balance of a card by its ID.
    /// </summary>
    /// <param name="card">Card object</param>
    /// <returns>Card balance details</returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public async Task<CardBalanceResponse> GetCardBalance(Card card)
    {
        // Sum all transactions for this card
        var totalTransactions = await context.Transactions
            .Where(t => t.CardId == card.Id)
            .SumAsync(t => t.Amount);

        // Calculate balance
        var availableBalance = card.CreditLimit - totalTransactions;

        return new CardBalanceResponse
        {
            CardId = card.Id,
            CreditLimit = card.CreditLimit,
            TotalOfTransactions = totalTransactions,
            AvailableBalance = availableBalance
        };
    }
}
