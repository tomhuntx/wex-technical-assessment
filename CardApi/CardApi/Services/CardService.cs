using CardApi.Data;
using CardApi.Models;

namespace CardApi.Services;

public class CardService(CardDbContext context)
{
    /// <summary>
    /// Create a new card and save it to the db with a unique ID.
    /// </summary>
    /// <param name="request">Request body</param>
    /// <returns></returns>
    public async Task<Card> CreateCard(CardCreateRequest request)
    {
        if (request.CreditLimit <= 0)
            throw new ArgumentException("Credit limit must be greater than zero.");

        var card = new Card { Id = Guid.NewGuid(), CreditLimit = request.CreditLimit };
        context.Cards.Add(card);
        await context.SaveChangesAsync();
        return card;
    }
}
