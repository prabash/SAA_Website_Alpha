using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEST_ASP_ALPHA_1.Common
{
    public enum ItemType
    {
        Games,
        TvSeries,
        Movies,
        Gifts,
        Electronics,
        All
    }

    public enum ItemSortBy
    {
        NameAsc,
        NameDesc,
        YearAsc,
        YearDesc,
        PriceAsc,
        PriceDesc,
        OnSaleAsc,
        OnSaleDesc,
        BestSellerAsc,
        BestSellerDesc
    }

    public enum CustomerGetType
    {
        id,
        email,
        name,
        telephone,
        registeredDate
    }
}