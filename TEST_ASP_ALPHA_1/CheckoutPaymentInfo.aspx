<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckoutPaymentInfo.aspx.cs" Inherits="TEST_ASP_ALPHA_1.CheckoutPaymentInfo" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
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
                            <li id="opc-payment" class="section allow active">
                                <div class="step-title">
                                    <span class="number">3</span>
                                    <h3 class="one_page_heading">Payment Information</h3>
                                </div>
                                <div id="checkout-step-payment" class="step a-item">
                                    <form action="#" id="co-payment-form">
                                        <dl id="checkout-payment-method-load">
                                            <dt>
                                                <input type="radio" id="p_method_checkmo" value="checkmo" name="payment[method]" title="Check / Money order" onclick="payment.switchMethod('checkmo')" class="radio" checked="checked"/>
                                                <label for="p_method_checkmo">Pay on Delivery</label>
                                            </dt>
                                            <dd>
                                                <fieldset class="form-list">
                                                </fieldset>
                                            </dd>
                                        </dl>
                                        <div class="tool-tip" id="payment-tool-tip" style="display: none;">
                                            <div class="btn-close"><a href="#" id="payment-tool-tip-close" class="btn-close">
                                                <img src="#btn_window_close.gif" alt="Close" title="Close" /></a></div>
                                            <div class="tool-tip-content">
                                                <img src="#cvv.gif" alt="Card Verification Number Visual Reference" title="Card Verification Number Visual Reference" /></div>
                                        </div>
                                    </form>
                                    <p class="require"><em class="required">* </em>Required Fields</p>
                                    <div class="buttons-set1" id="payment-buttons-container">
                                        <button type="button" class="button continue" onclick="window.location.href='CheckoutOrderReview.aspx';"><span>Continue</span></button>
                                    </div>
                                    <div style="clear: both;"></div>
                                </div>
                            </li>
                            <li id="opc-review" class="section">
                                <div class="step-title">
                                    <span class="number">4</span>
                                    <h3 class="one_page_heading">Order Review</h3>
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

