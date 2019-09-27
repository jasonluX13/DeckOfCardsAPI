using System;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace DeckOfCards.Data
{
    public class DeckRepository : IDeckRepository
    {
        async public Task<Deck> CreateNewShuffledDeckAsync(int deckCount)
        {
            var random = new Random();

            var suits = new[] { "HEARTS", "SPADES", "CLUBS", "DIAMONDS" };
            var values = new[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "JACK", "QUEEN", "KING" };
            var cards = new Card[52 * deckCount];
            var deck = new Deck { DeckId = random.Next().ToString("X") };

            int newCardIndex = 0;
            for (int _ = 0; _ < deckCount; _ += 1)
            {
                foreach (string suit in suits)
                {
                    foreach (string value in values)
                    {
                        string code = value.Substring(0, 1) + suit.Substring(0, 1);
                        if (value == "10")
                        {
                            code = "0" + suit.Substring(0, 1);
                        }
                        cards[newCardIndex] = new Card
                        {
                            Deck = deck,
                            Value = value,
                            Suit = suit,
                            Code = code,
                        };
                        newCardIndex += 1;
                    }
                }
            }

            // Fisher-Yates shuffle
            for (int cardIndex = cards.Length - 1; cardIndex >= 0; cardIndex -= 1)
            {
                int swapIndex = random.Next(0, cardIndex);
                Card card = cards[swapIndex];
                cards[swapIndex] = cards[cardIndex];
                cards[cardIndex] = card;
                cards[cardIndex].Order = cardIndex;
                cards[swapIndex].Order = swapIndex;
            }

            foreach (Card card in cards)
            {
                deck.Cards.Add(card);
            }

            using (var context = new DeckContext())
            {
                context.Decks.Add(deck);
                await context.SaveChangesAsync();
            }

            return deck;
        }
        async public Task<Deck> DrawCardsAsync(string deckId, int numberToDraw)
        {
            using (var context = new DeckContext())
            {
                Deck deck = await context.Decks
                  .Include(x => x.Cards)
                  .SingleAsync(x => x.DeckId == deckId);

                foreach (Card card in deck.Cards)
                {
                    if (!card.Drawn)
                    {
                        card.Drawn = true;
                        numberToDraw -= 1;
                    }
                    if (numberToDraw == 0)
                    {
                        break;
                    }
                }

                await context.SaveChangesAsync();

                return deck;
            }
        }

        async public Task<Deck> PutCardsInPile(string deckId, string pileName, List<string> cardCodes)
        {
            using (var context = new DeckContext())
            {
                Deck deck = await context.Decks
                  .Include(x => x.Cards)
                  .Include(x => x.Piles)
                  .SingleAsync(x => x.DeckId == deckId);

                Pile pile = null;

                foreach (Pile p in deck.Piles)
                {
                    if (p.Name == pileName)
                    {
                        pile = p;
                    }
                }
                if (pile == null)
                {
                    //create new pile
                    pile = new Pile()
                    {
                        Name = pileName,
                        DeckId = deck.Id,
                        Deck = deck
                    };
                    context.Piles.Add(pile);
                    context.SaveChanges();
                    pile = context.Piles
                        .Include(p => p.Cards)
                        .First(p => p.DeckId == deck.Id && p.Name == pileName);
                }
               

                foreach (Card card in deck.Cards)
                {
                   foreach (string cardCode in cardCodes)
                    {
                        if (card.Code == cardCode)
                        {
                            card.PileId = pile.Id;
                        }
                    }
                }

                await context.SaveChangesAsync();

                return deck;
            }
        }


        async public Task<Deck> GetDeck(string deckId)
        {
            using (var context = new DeckContext())
            {
                Deck deck = await context.Decks
                  .Include(x => x.Cards)
                  .Include(x => x.Piles)
                  .SingleAsync(x => x.DeckId == deckId);

                return deck;
            }
        }
    }
}