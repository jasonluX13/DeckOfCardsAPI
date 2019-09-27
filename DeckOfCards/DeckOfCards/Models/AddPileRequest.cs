using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeckOfCards.Models
{
    public class AddPileRequest
    {
        public AddPileRequest(List<string> value)
        {
            op = "add";
            path = "/cards";
            this.value = value;
        }
        public string op { get; set; }
        public string path { get; set; }
        public List<string> value { get; set; }
    }
}