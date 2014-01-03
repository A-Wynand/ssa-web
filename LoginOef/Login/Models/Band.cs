using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festival.Models
{
    public class Band
    {
        public int ID { get; set; }
        public string naam { get; set; }
        public string omschrijving { get; set; }
        public string tijdslot { get; set; }
        public string fotoUri { get; set; }
        public string podium { get; set; }
    }
}