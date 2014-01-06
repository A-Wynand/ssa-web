using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Festival.RSS
{
    public class MyFeedItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string CreatedBy { get; set; }
    }
}