using CardApi.Data;
using CardApi.Models;

namespace CardApi.Services;

public class CardService(CardDbContext context)
{
    /// <summary>
    /// Create a new card and save it to the db with a unique ID.
    /// </summary>
    /// <param name="creditLimit">Given credit limit</param>
    /// <returns></returns>
    public async Task<Card> CreateCard(decimal creditLimit)
    {
        if (creditLimit <= 0)
            throw new ArgumentException("Credit limit must be greater than zero.", nameof(creditLimit));

        var card = new Card { Id = Guid.NewGuid(), CreditLimit = creditLimit };
        context.Cards.Add(card);
        await context.SaveChangesAsync();
        return card;
    }
}
