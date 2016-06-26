<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckoutBillingInfoUpdate.aspx.cs" Inherits="TEST_ASP_ALPHA_1.CheckoutMethodUpdate" MasterPageFile="~/Site.Master" %>

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
                                </div>
                            </li>
                            <li id="opc-billing" class="section allow active">
                                <div class="step-title">
                                    <span class="number">2</span>
                                    <h3 class="one_page_heading">Billing Information : Update Information</h3>
                                    <!--<a href="#">Edit</a>-->
                                </div>
                                <div id="checkout-step-billing" class="step a-item" style="">
                                    <form class="content" runat="server" id="updateInfoForm" method="post">
                                        <fieldset class="group-select">
                                            <ul>
                                                <li id="shipping-new-address-form">
                                                    <fieldset>
                                                        <input type="hidden" name="shipping[address_id]" value="9681" id="shipping:address_id" />
                                                        <ul>
                                                            <li>
                                                                <div class="customer-name">
                                                                    <div class="input-box name-firstname">
                                                                        <label for="billing:firstname">Full Name<span class="required">*</span></label>
                                                                        <br />
                                                                        <input type="text" class="input-text required-entry" title="Full Name" runat="server" id="dfName" required />
                                                                    </div>
                                                                </div>
                                                            </li>
                                                            <li>
                                                                <label>Address  <span class="required">*</span> &nbsp; <b>[IMPORTANT : This is the address we will use to deliver your items]</b></label>
                                                                <br />
                                                                <input type="text" title="Street Address" id="dfAddLine1" value="" class="input-text required-entry" placeholder="Address Line 1" runat="server" required />
                                                            </li>
                                                            <li>
                                                                <input type="text" title="Street Address 2" id="dfAddLine2" value="" class="input-text" placeholder="Address Line 2" runat="server" required />
                                                            </li>
                                                            <li>
                                                                <div class="input-box">
                                                                    <label for="shipping:city">City <span class="required">*</span></label>
                                                                    <br />
                                                                    <select id="cmbCity" title="City" class="validate-select" style="" runat="server" required>
                                                                        <option value="">Please select the city</option>
                                                                        <option value="Colombo 1">Colombo 1</option>
                                                                        <option value="Colombo 2">Colombo 2</option>
                                                                        <option value="Colombo 3">Colombo 3</option>
                                                                        <option value="Colombo 4">Colombo 4</option>
                                                                        <option value="Colombo 5">Colombo 5</option>
                                                                        <option value="Colombo 6">Colombo 6</option>
                                                                        <option value="Colombo 7">Colombo 7</option>
                                                                        <option value="Colombo 8">Colombo 8</option>
                                                                        <option value="Colombo 9">Colombo 9</option>
                                                                        <option value="Colombo 10">Colombo 10</option>
                                                                        <option value="Colombo 11">Colombo 11</option>
                                                                        <option value="Colombo 12">Colombo 12</option>
                                                                        <option value="Colombo 13">Colombo 13</option>
                                                                        <option value="Colombo 14">Colombo 14</option>
                                                                        <option value="Colombo 15">Colombo 15</option>
                                                                        <option value="Dehiwala">Dehiwala</option>
                                                                        <option value="Katubedda">Katubedda</option>
                                                                        <option value="Kottawa">Kottawa</option>
                                                                        <option value="Moratuwa">Moratuwa</option>
                                                                        <option value="Mt. Lavinia">Mt. Lavinia</option>
                                                                        <option value="Panadura">Panadura</option>
                                                                        <option value="Piliyandala">Piliyandala</option>
                                                                        <option value="Ratmalana">Ratmalana</option>
                                                                    </select>
                                                                </div>
                                                                <div class="input-box">
                                                                    <label for="billing:telephone">Telephone <span class="required">*</span></label>
                                                                    <br />
                                                                    <input type="text" value="" title="Telephone" class="input-text required-entry" runat="server" id="dfTelephone" required />
                                                                </div>
                                                            </li>
                                                        </ul>
                                                    </fieldset>
                                                </li>
                                            </ul>
                                            <div class="error" runat="server" id="regErrorBox" visible="false" style="height: 30px; margin-top: 10px; padding: 8px;">
                                                <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>
                                                <p runat="server" id="regErrMsg"></p>
                                            </div>
                                            <p class="require"><em class="required">* </em>Required Fields</p>
                                            <div class="buttons-set1" id="shipping-buttons-container">
                                                 <asp:Button CssClass="button continue" runat="server" ID="btnUpdate" Text="UPDATE DETAILS" OnClick="btnUpdate_Click" />
                                            </div>
                                        </fieldset>
                                    </form>
                                </div>
                            </li>
                            <li id="opc-payment" class="section">
                                <div class="step-title">
                                    <span class="number">3</span>
                                    <h3 class="one_page_heading">Payment Information</h3>
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

