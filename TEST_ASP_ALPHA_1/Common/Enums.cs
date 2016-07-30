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
        registeredDate,
        all
    }

    public enum PurchItemGetType
    {
        customerEmail,
        customerId,
        purchCartId,
        purchDate,
        purchId
    }

    public enum PurchItemOrderBy
    {
        purchDateAsc,
        purchDateDesc,
        OrderIdAsc,
        OrderIdDesc
    }

    public enum CartGetType
    {
        customerEmail,
        customerId,
        purchDate,
        totalValue,
        cartId
    }

    public enum CartOrderBy
    {
        purchDateAsc,
        purchDateDesc,
        OrderIdAsc,
        OrderIdDesc
    }

    public enum ItemStatus
    {
        New,
        Delivered,
        Cancelled,
        Closed,
        All
    }
}