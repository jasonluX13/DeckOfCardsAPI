using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckOfCards.Models
{
    public class DeckCreate
    {
        public DeckCreate()
        {
            Count = 1;
        }
        public int? Count { get; set; }
    }
}