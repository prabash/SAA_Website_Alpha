<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckoutOrderReview.aspx.cs" Inherits="TEST_ASP_ALPHA_1.CheckoutOrderReview" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%@ Import Namespace="TEST_ASP_ALPHA_1.Models" %>
    <%@ Import Namespace="TEST_ASP_ALPHA_1.Common" %>
    <!-- main-container -->
    <div class="main-container col2-right-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="row">
                <section class="col-main col-sm-12">
                    <div class="checkout-page">
                        <div class="page-title new_page_title">
                            <h1>Checkout</h1>
                        </div>
                        <ol class="one-page-checkout" id="checkoutSteps">
                            <li id="opc-login" class="section">
                                <div class="step-title">
                                    <span class="number">1</span>
                                    <h3 class="one_page_heading">Checkout Method</h3>
                                    <!--<a href="#">Edit</a>-->
                                </div>
                            </li>
                            <li id="opc-billing" class="section">
                                <div class="step-title">
                                    <span class="number">2</span>
                                    <h3 class="one_page_heading">Billing Information</h3>
                                </div>
                            </li>
                            <li id="opc-payment" class="section">
                                <div class="step-title">
                                    <span class="number">3</span>
                                    <h3 class="one_page_heading">Payment Information</h3>
                                </div>
                            </li>
                            <li id="opc-review" class="section allow active">
                                <div class="step-title">
                                    <span class="number">4</span>
                                    <h3 class="one_page_heading">Order Review</h3>
                                </div>
                                <div id="checkout-step-review" class="step a-item">
                                    <div class="order-review" id="checkout-review-load"></div>
                                    <div class="buttons-set13" id="review-buttons-container">
                                        <fieldset>
                                            <table class="data-table cart-table" id="shopping-cart-table">
                                                <thead>
                                                    <tr class="first last">
                                                        <th rowspan="1">&nbsp;</th>
                                                        <th rowspan="1"><span class="nobr">Product Name</span></th>
                                                        <th colspan="1" class="a-center"><span class="nobr">Unit Price</span></th>
                                                        <th class="a-center" rowspan="1">Qty</th>
                                                        <th colspan="1" class="a-center">Subtotal</th>
                                                    </tr>
                                                </thead>
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

                                                                  double discount = new ItemsModel().GetItemDiscount();
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
                                                            <h4 class="product-name"><a href="product_detail.html"><%=item.title%></a> </h4>
                                                        </td>
                                                        <td class="a-right"><span class="cart-price"><span class="price"><%=item.currentPrice.FormatCurrency("LKR").ToString()%></span> </span></td>
                                                        <td class="a-center movewishlist">1</td>
                                                        <td class="a-right movewishlist"><span class="cart-price"><span class="price" id="span<%=item.Id%>"><%=item.currentPrice.FormatCurrency("LKR").ToString()%></span> </span>
                                                            <input type="hidden" id="hidItemSubTot<%=item.Id%>" value="<%=item.currentPrice%>"></td>
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
                                        <table class="table shopping-cart-table-total" id="shopping-cart-totals-table" style="float:right;">
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
                                        <p class="f-left">Forgot an Item? <b><a href="Cart.aspx">Edit Your Cart</a></b> </p>
                                        <button type="submit" class="button" onclick="review.save();"><span>Place Order</span></button>
                                    </div>
                                </div>
                            </li>
                        </ol>
                    </div>
                </section>
            </div>
        </div>
    </div>
    <!--End main-container -->
</asp:Content>
