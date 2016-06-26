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
                                    <form id="formCompleteCheckout" runat="server" method="post"> 
                                        <div class="buttons-set13" id="review-buttons-container">
                                            <fieldset>
                                                <table class="data-table cart-table" id="shopping-cart-table">
                                                    <thead>
                                                        <tr class="first last">
                                                            <th rowspan="1">&nbsp;</th>
                                                            <th rowspan="1"><span class="nobr">Product Name</span></th>
                                                            <th colspan="1" class="a-center"><span class="nobr">Unit Price</span></th>
                                                            <th class="a-center" rowspan="1">Qty</th>
                                                            <th colspan="1" class="a-center">Unit total</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <%var cartSession = Session[CommonManager.GetCartItemsWithQtySessionName()];
                                                          if (cartSession != null)
                                                          {
                                                              var subTotal = 0.0;
                                                              var cart = (CartObject)cartSession;
                                                              if (cart != null)
                                                              {
                                                                  var cartIds = new List<int>();
                                                                  foreach (var item in cart.Items)
                                                                  {
                                                                      cartIds.Add(item.ItemId);
                                                                  }

                                                                  var searchCriteria = new Dictionary<string, string>();
                                                                  searchCriteria.Add(CommonManager.GetIdCriterionName(), CommonManager.GetSearchByIdCriterion(cartIds));
                                                                  var itemDetails = new TEST_ASP_ALPHA_1.Models.ItemsModel().GetItemDetails(ItemType.All, ItemSortBy.NameAsc, 0, 0, searchCriteria);
                                                                  foreach (var item in itemDetails)
                                                                  {
                                                                      var itemQty = cart.Items.First(rec => rec.ItemId == item.Id).ItemQuantity;
                                                                      var unitTotal = (item.currentPrice * itemQty);
                                                                      subTotal += unitTotal;
                                                                 
                                                        %>
                                                        <tr class="first odd">
                                                            <td class="image"><a class="product-image" title="<%=item.title%>" href="ProductDetails.aspx?itemId=<%=item.Id%>">
                                                                <img width="75" alt="Sample Product" src="<%=item.defaultLocation%>"></a>

                                                            </td>
                                                            <td>
                                                                <h4 class="product-name"><a href="product_detail.html"><%=item.title%></a> </h4>
                                                            </td>
                                                            <td class="a-right"><span class="cart-price"><span class="price"><%=item.currentPrice.FormatCurrency("LKR").ToString()%></span> </span></td>
                                                            <td class="a-center movewishlist"><%=itemQty%></td>
                                                            <td class="a-right movewishlist"><span class="cart-price"><span class="price" id="span<%=item.Id%>"><%=unitTotal.FormatCurrency("LKR").ToString()%></span> </span>
                                                                <input type="hidden" id="hidItemSubTot<%=item.Id%>" value="<%=item.currentPrice%>"></td>
                                                        </tr>
                                                        <%
                                                              }

                                                              dfSubTotal.InnerText = subTotal.FormatCurrency("LKR").ToString();
                                                              dfDiscount.InnerText = cart.CartDiscount.FormatCurrency("LKR").ToString();
                                                              dfGrandTotal.InnerText = (subTotal - cart.CartDiscount + cart.CartDelivery).FormatCurrency("LKR").ToString();

                                                        %>
                                                        <tr class="first odd">
                                                            <td>&nbsp; </td>
                                                            <td>&nbsp; </td>
                                                            <td>&nbsp; </td>
                                                            <td colspan="1" class="a-left" style="">Subtotal </td>
                                                            <td class="a-right" style=""><span class="dfSubTotal" id="dfSubTotal" runat="server">00.00</span></td>
                                                        </tr>
                                                        <tr class="first odd">
                                                            <td>&nbsp; </td>
                                                            <td>&nbsp; </td>
                                                            <td>&nbsp; </td>
                                                            <td colspan="1" class="a-left" style="">Discount </td>
                                                            <td class="a-right" style=""><span class="dfDiscount" id="dfDiscount" runat="server">00.00</span></td>
                                                        </tr>
                                                        <tr class="first odd">
                                                            <td>&nbsp; </td>
                                                            <td>&nbsp; </td>
                                                            <td>&nbsp; </td>
                                                            <td colspan="1" class="a-left" style=""><strong>Grand Total</strong></td>
                                                            <td class="a-right" style=""><strong><span class="dfGrandTotal" id="dfGrandTotal" runat="server">00.00</span></strong></td>
                                                        </tr>
                                                        <%
                                                            
                                                          }
                                                      }
                                                        %>
                                                    </tbody>
                                                </table>
                                            </fieldset>
                                            <br />
                                            <p class="f-left">Forgot an Item? <b><a href="Cart.aspx">Edit Your Cart</a></b> </p>
                                            <asp:Button type="submit" CssClass="button" runat="server" ID="btnCompleteCheckout" Text="COMPLETE CHECKOUT" OnClick="btnCompleteCheckout_Click"></asp:Button>
                                        </div>
                                    </form>
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
