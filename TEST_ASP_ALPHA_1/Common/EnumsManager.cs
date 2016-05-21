using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_ASP_ALPHA_1.Common
{
    public class EnumsManager
    {
        public static ItemSortBy GetSortByOption(string sortBy, bool asc)
        {
            if (sortBy == "Name" && asc)
                return ItemSortBy.NameAsc;
            else if (sortBy == "Name" && !asc)
                return ItemSortBy.NameDesc;
            else if (sortBy == "Best Seller" && asc)
                return ItemSortBy.BestSellerAsc;
            else if (sortBy == "Best Seller" && !asc)
                return ItemSortBy.BestSellerDesc;
            else if (sortBy == "On Sale" && asc)
                return ItemSortBy.OnSaleAsc;
            else if (sortBy == "On Sale" && !asc)
                return ItemSortBy.OnSaleDesc;
            else if (sortBy == "Price" && asc)
                return ItemSortBy.PriceAsc;
            else if (sortBy == "Price" && !asc)
                return ItemSortBy.PriceDesc;
            else if (sortBy == "Year" && asc)
                return ItemSortBy.YearAsc;
            else if (sortBy == "Year" && !asc)
                return ItemSortBy.YearDesc;
            else
                return ItemSortBy.NameAsc;

        }
    }
}