using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festival.Models
{
    public class Gebruiker
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string vnaam { get; set; }
        public string anaam { get; set; }
        public string email { get; set; }

    }
}