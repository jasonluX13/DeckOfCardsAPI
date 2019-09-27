using DeckOfCards.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckOfCards.Models
{
    public class PileInfo
    {
        public string DeckId { get; set; }
        public int Remaining { get; set; }
        public IList<Pile> Piles { get; set;}
    }
}