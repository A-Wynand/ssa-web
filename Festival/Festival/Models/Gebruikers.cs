using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festival.Models
{
    public class Gebruikers
    {
        public int ID { get; set; }
        public string vnaam { get; set; }
        public string anaam { get; set; }
        public string email { get; set; }
        public string wachtwoord { get; set; }
        public Boolean isAdmin { get; set; }
    }


}