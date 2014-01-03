using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace Festival.Models
{
    public partial class Ticket
    {
        public int ID { get; set; }
        public string soort { get; set; }
        public int aantal { get; set; }
        public float prijs { get; set; }
    }

    public class DagBestelling
    {
        public string soort { get; set; }
        public int aantal { get; set; }
    }
}