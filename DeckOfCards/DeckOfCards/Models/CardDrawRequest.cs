using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckOfCards.Models
{
    public class CardDrawRequest
    {
        public CardDrawRequest()
        {
            Count = 1;
        }

        public int? Count { get; set; }
    }
}