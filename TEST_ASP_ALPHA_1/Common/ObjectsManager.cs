using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_ASP_ALPHA_1.Common
{

    public class SlideShowObj
    {
        public int id { get; set; }
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
        public bool bestSeller { get; set; }
        public string year { get; set; }
        public string type { get; set; }
        public DateTime dateAdded { get; set; }
        public string genre { get; set; }
    }

    public class CustomerObject
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string emailAddress { get; set; }
        public string telephoneNo { get; set; }
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public DateTime registeredDate { get; set; }
        public bool active { get; set; }
    }

    public class PurchaseItemObject
    {
        public int PurchId { get; set; }
        public int PurchCartId { get; set; }
        public int ItemId { get; set; }
        public string ItemTitle { get; set; }
        public int ItemQuantity { get; set; }
        public double Discount { get; set; }
        public double CostPerItem { get; set; }
        public double SubTotal { get; set; }
        public string Status { get; set; }
        public DateTime PurchDate { get; set; }
    }

    public class CartObject
    {
        public int CartId { get; set; }
        public List<PurchaseItemObject> Items { get; set; }
        public int CustId { get; set; }
        public string CustEmailAddress { get; set; }
        public DateTime PurchaseDate { get; set; }
        public double CartDiscount { get; set; }
        public double CartDelivery { get; set; }
        public double CartTotal { get; set; }
        public string CartStatus { get; set; }
    }
}