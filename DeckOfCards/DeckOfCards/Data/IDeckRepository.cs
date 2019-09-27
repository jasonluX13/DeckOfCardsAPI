using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeckOfCards.Data
{
    public interface IDeckRepository
    {
        Task<Deck> CreateNewShuffledDeckAsync(int deckCount);
        Task<Deck> DrawCardsAsync(string deckId, int numberToDraw);
        Task<Deck> PutCardsInPile(string deckId, string pileName, List<string> cardCodes);
        Task<Deck> GetDeck(string deckId);
    }
}