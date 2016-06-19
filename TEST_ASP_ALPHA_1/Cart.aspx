<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="TEST_ASP_ALPHA_1.Cart" MasterPageFile="~/Site.Master" %>

<%@ Import Namespace="TEST_ASP_ALPHA_1.Common" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <section class="main-container col1-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="col-main">
                <div class="cart">
                    <div class="page-title">
                        <h2>Shopping Cart</h2>
                    </div>
                    <div class="table-responsive">
                        <form method="post" action="#updatePost/">
                            <input type="hidden" value="Vwww7itR3zQFe86m" name="form_key">
                            <fieldset>
                                <table class="data-table cart-table" id="shopping-cart-table">

                                    <thead>
                                        <tr class="first last">
                                            <th rowspan="1">&nbsp;</th>
                                            <th rowspan="1"><span class="nobr">Product Name</span></th>
                                            <th colspan="1" class="a-center"><span class="nobr">Unit Price</span></th>
                                            <th class="a-center" rowspan="1">Qty</th>
                                            <th colspan="1" class="a-center">Subtotal</th>
                                            <th class="a-center" rowspan="1">&nbsp;</th>
                                        </tr>
                                    </thead>
                                    <tfoot>
                                        <tr class="first last">
                                            <td class="a-right last" colspan="50">
                                                <button onclick="setLocation('#')" class="button btn-continue" title="Continue Shopping" type="button"><span><span>Continue Shopping</span></span></button>
                                                <button id="empty_cart_button" class="button btn-empty" title="Clear Cart" value="empty_cart" name="update_cart_action" type="submit"><span><span>Clear Cart</span></span></button>
                                                <button class="button btn-update" title="Update Cart" value="update_qty" name="update_cart_action" type="submit"><span><span>Update Cart</span></span></button></td>
                                        </tr>
                                    </tfoot>
                                    <tbody>
                                        <%var cartSession = Session[CommonManager.GetCartItemsSessionName()];
                                          if (cartSession != null)
                                          {
                                              var subTotal = 0.0;
                                              var cart = (List<int>)cartSession;
                                              if (cart.Count > 0)
                                              {
                                                  var searchCriteria = new Dictionary<string, string>();
                                                  searchCriteria.Add(CommonManager.GetIdCriterionName(), CommonManager.GetSearchByIdCriterion(cart));
                                                  var itemDetails = new TEST_ASP_ALPHA_1.Models.ItemsModel().GetItemDetails(ItemType.All, ItemSortBy.NameAsc, 5, 0, searchCriteria);
                                                  foreach (var item in itemDetails)
                                                  {
                                                      subTotal = itemDetails.Sum(rec => rec.currentPrice);
                                                      dfSubTotal.InnerText = subTotal.FormatCurrency("LKR").ToString();
                                                      dfHidSubTotal.Value = subTotal.ToString();

                                                      double discount = 250.00;
                                                      dfDiscount.InnerText = discount.FormatCurrency("LKR").ToString();
                                                      dfHidDiscount.Value = discount.ToString();

                                                      dfGrandTotal.InnerText = (subTotal - discount).FormatCurrency("LKR").ToString();
                                                      dfHidGrandTotal.Value = (subTotal - discount).ToString();
                                        %>
                                        <tr class="first odd">
                                            <td class="image"><a class="product-image" title="<%=item.title%>" href="ProductDetails.aspx?itemId=<%=item.Id%>">
                                                <img width="75" alt="Sample Product" src="<%=item.defaultLocation%>"></a>

                                            </td>
                                            <td>
                                                <h2 class="product-name"><a href="product_detail.html"><%=item.title%></a> </h2>
                                            </td>
                                            <td class="a-right"><span class="cart-price"><span class="price"><%=item.currentPrice.FormatCurrency("LKR").ToString()%></span> </span></td>
                                            <td class="a-center movewishlist">
                                                <input maxlength="12" class="input-text qty" title="Qty" size="4" value="1" name="qty" onchange="calcItemSubQty('span<%=item.Id%>', 'hidItemSubTot<%=item.Id%>', 'item<%=item.Id%>Qty', <%=item.currentPrice%>)" id="item<%=item.Id%>Qty"></td>
                                            <td class="a-right movewishlist"><span class="cart-price"><span class="price" id="span<%=item.Id%>"><%=item.currentPrice.FormatCurrency("LKR").ToString()%></span> </span>
                                                <input type="hidden" id="hidItemSubTot<%=item.Id%>" value="<%=item.currentPrice%>"></td>
                                            <td class="a-center last"><a class="button remove-item" title="Remove item" onclick="RemoveFromCart(<%=item.Id%>);"><span><span>Remove item</span></span></a></td>
                                        </tr>
                                        <%
                                                  }
                                        %>

                                        <%
                                              }
                                          }
                                        %>
                                    </tbody>
                                </table>
                            </fieldset>
                        </form>
                    </div>
                    <!-- BEGIN CART COLLATERALS -->
                    <div class="cart-collaterals row">
                        <div class="col-sm-4">
                            <div class="shipping">
                                <h3>Delivery Address</h3>
                                <div class="shipping-form">
                                    <form id="shipping-zip-form" method="post" action="#estimatePost/">
                                        <p>Please check if your Delivery Address is accurate</p>
                                        <ul class="form-list">
                                            <li>
                                                <div class="input-box">
                                                    <input type="text" value="Address Line 1" name="Address line 1" id="addLine1" runat="server" class="input-text validate-postcode" disabled="disabled">
                                                </div>
                                                <div class="input-box">
                                                    <input type="text" value="Address Line 2" name="Address line 2" id="addLine2" runat="server" class="input-text validate-postcode" disabled="disabled">
                                                </div>
                                                <div class="input-box">
                                                    <input type="text" value="City" name="City" id="city" runat="server" class="input-text validate-postcode" disabled="disabled">
                                                </div>
                                            </li>
                                        </ul>
                                        <div class="buttons-set11">
                                            <button class="button get-quote" onclick="window.location.href='MyAccountInformation.aspx';" title="Get a Quote" type="button"><span>Update Address</span></button>
                                        </div>
                                        <!--buttons-set11-->
                                    </form>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">

                            <div class="discount">
                                <h3>Discount Codes</h3>
                                <form method="post" action="#couponPost/">
                                    <label for="coupon_code">Enter your coupon code if you have one.</label>
                                    <input type="hidden" value="0" id="remove-coupone" name="remove">
                                    <input type="text" value="" name="coupon_code" id="coupon_code" class="input-text fullwidth" disabled="disabled">
                                    <button value="Apply Coupon" onclick="discountForm.submit(false)" class="button btn-update" title="Apply Coupon" type="button" disabled="disabled"><span>Apply Coupon</span></button>
                                </form>
                            </div>

                        </div>
                        <div class="totals col-sm-4">
                            <h3>Shopping Cart Total</h3>
                            <div class="inner">
                                <table class="table shopping-cart-table-total" id="shopping-cart-totals-table">

                                    <tfoot>
                                        <tr>
                                            <td colspan="1" class="a-left" style=""><strong>Grand Total</strong></td>
                                            <td class="a-right" style=""><strong><span class="dfGrandTotal" id="dfGrandTotal" runat="server">00.00</span></strong><input type="hidden" id="dfHidGrandTotal" class="dfHidGrandTotal" runat="server"></td>
                                        </tr>
                                    </tfoot>
                                    <tbody>
                                        <tr>
                                            <td colspan="1" class="a-left" style="">Subtotal </td>
                                            <td class="a-right" style=""><span class="dfSubTotal" id="dfSubTotal" runat="server">00.00</span><input type="hidden" id="dfHidSubTotal" class="dfHidSubTotal" runat="server"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="1" class="a-left" style="">Discount </td>
                                            <td class="a-right" style=""><span class="dfDiscount" id="dfDiscount" runat="server">00.00</span><input type="hidden" id="dfHidDiscount" class="dfHidDiscount" runat="server"></td>
                                        </tr>
                                    </tbody>
                                </table>
                                <ul class="checkout">
                                    <li>
                                        <button onclick="" class="button btn-proceed-checkout" title="Proceed to Checkout" type="button"><span>Proceed to Checkout</span></button>
                                    </li>
                                    <li>
                                        <br>
                                    </li>
                                    <li>
                                        <br>
                                    </li>
                                </ul>
                            </div>
                            <!--inner-->

                        </div>
                    </div>
                </div>
            </div>
        </div>

    </section>
    <div class="col-main">
        <div class="box-additional">
            <div class="related-pro wow bounceInUp animated">
                <div class="slider-items-products">
                    <div class="new_title center">
                        <h2>Related Products</h2>
                    </div>
                    <div id="related-products-slider" class="product-flexslider hidden-buttons">
                        <div class="slider-items slider-width-col4" id="relatedItemsGridList" runat="server">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function calcItemSubQty(spanId, hidItemSubTotId, txtBoxId, currentPrice) {
            var itemQty = document.getElementById(txtBoxId);
            var span = document.getElementById(spanId);
            var oldSubTot = document.getElementById(hidItemSubTotId);
            $.ajax({
                type: "POST",
                url: "Cart.aspx/CalcItemSubQty",
                data: '{itemQty: "' + itemQty.value + '", itemPrice: "' + currentPrice + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var newVal = itemQty.value * currentPrice;
                    calcTotalSubQty(oldSubTot.value, newVal);
                    span.textContent = response.d;
                    oldSubTot.value = newVal;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        };

        function calcTotalSubQty(oldItemSubQtyPrice, newItemSubQtyPrice) {
            var hiddenSubTotal = $('.dfHidSubTotal').val();
            var newSubQty = hiddenSubTotal - oldItemSubQtyPrice + newItemSubQtyPrice;
            $.ajax({
                type: "POST",
                url: "Cart.aspx/FormatCurrency",
                data: '{price: "' + newSubQty + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $('.dfSubTotal').text(response.d);
                    $('.dfHidSubTotal').val(newSubQty);
                    calcGrandTotal(newSubQty);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        };

        function calcGrandTotal(newSubQty) {
            var hiddenDiscount = $('.dfHidDiscount').val();
            var newGrandTotal = newSubQty - hiddenDiscount;
            $.ajax({
                type: "POST",
                url: "Cart.aspx/FormatCurrency",
                data: '{price: "' + newGrandTotal + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    $('.dfGrandTotal').text(response.d);
                    $('.dfHidGrandTotal').val(newGrandTotal);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        };
    </script>
</asp:Content>

