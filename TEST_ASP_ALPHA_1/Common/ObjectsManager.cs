using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_ASP_ALPHA_1.Common
{

    public class SlideShowObj
    {
        public string location { get; set; }
        public string title { get; set; }
    }

    public class ItemObject
    {
        public int Id { get; set; }
        public string title { get; set; }
        public string defaultLocation { get; set; }
        public string altLocation { get; set; }
        public double regularPrice { get; set; }
        public double currentPrice { get; set; }
        public double rating { get; set; }
        public bool onSale { get; set; }
        public string description { get; set; }
        public string overview { get; set; }
        public string year { get; set; }
        public string type { get; set; }
        public DateTime dateAdded { get; set; }
        public string genre { get; set; }
    }
}