using DeckOfCards.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckOfCards.Models
{
    public class PileListResponse
    {
        public string DeckId { get; set; }
        public int Remaining { get; set; }
        public IList<Card> Cards { get; set; }
    }
}