using System.Data.Entity;

namespace DeckOfCards.Data
{
    public class DeckContext : DbContext
    {
        public DeckContext() : base("name=DeckOfCardsConnection")
        {
            Database.SetInitializer<DeckContext>(null);
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Pile> Piles { get; set; }
    }
}