<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="TEST_ASP_ALPHA_1.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&mdash;›</span></li>
                    <li class="category13"><strong>Contact Us</strong></li>
                </ul>
            </div>
        </div>
    </div>
    <!-- main-container -->
    <div class="main-container col2-right-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="row">
                <section class="col-main col-sm-8 wow">

                    <div class="static-contain">
                        <div class="page-title new_page_title">
                            <h2>Contact Us</h2>
                        </div>
                        <form runat="server" Id="contactForm" class="group-select">
                            <ul>
                                <li id="billing-new-address-form">
                                    <fieldset>
                                        <legend>New Address</legend>
                                        <input type="hidden" name="billing[address_id]" value="" id="billing:address_id">
                                        <ul>
                                            <li>
                                                <div class="customer-name">
                                                    <div class="input-box name-firstname">
                                                        <label for="firstname">Name<span class="required">*</span></label>
                                                        <br>
                                                        <input type="text" Id="dfnFirstname" name="billing[firstname]" value="" title="First Name" class="input-text" runat="server" required>
                                                    </div>
                                                    <div class="input-box name-lastname">
                                                        <label for="lastname">Email Address <span class="required">*</span> </label>
                                                        <br>
                                                        <input type="email" Id="dfnEmail" name="billing[email]" value="" title="Email" class="input-text" runat="server" required>
                                                    </div>
                                                </div>
                                            </li>
                                            <li>
                                                <div class="input-box">
                                                    <label for="company">Company <span class="required" style="visibility:hidden">*</span> </label>
                                                    <br>
                                                    <input type="text" Id="dfnCompany" name="billing[company]" value="" title="Company" class="input-text" runat="server" >
                                                </div>
                                                <div class="input-box">
                                                    <label for="email">Telephone <span class="required">*</span></label>
                                                    <br>
                                                    <input type="number" name="billing[telephone]" Id="dfnTelephone" value="" title="Telephone" class="input-text" runat="server" required>
                                                </div>
                                            </li>
                                            <li>
                                                <label for="address">Address <span class="required">*</span></label>
                                                <br>
                                                <input type="text" title="Street Address" name="billing[address]" Id="dfnAddress" value="" class="input-text required-entry" runat="server" required>
                                            </li>
                                            <li class="">
                                                <label>Comment<em class="required">*</em></label>
                                                <br>
                                                <div style="float: none" class="">
                                                    <textarea name="comment" title="Comment" class="required-entry input-text" cols="5" rows="3" runat="server" id="dfnComment" required></textarea>
                                                </div>
                                            </li>
                                        </ul>
                                    </fieldset>
                                </li>
                                <li>
                                    <p class="require"><em class="required">* </em>Required Fields</p>
                                    <input type="text" name="hideit" value="" style="display: none !important;">
                                    <div class="buttons-set">
                                        <asp:Button runat="server" ID="SubmitButton" CssClass="button submit" Text="SUBMIT" OnClick="SubmitButton_Click"></asp:Button>
                                    </div>
                                </li>
                            </ul>
                            <div class="error" runat="server" ID="errorBox" visible="false">
                              <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                    An error occurred! Please try again later or give us a call! We are always happy to help you out :)
                            </div>
                            <div class="success" runat="server" ID="successBox" visible="false">
                              <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                    Thank you for contacting us! Please be patient as we'll get back to you ASAP :)
                            </div>
                        </form>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-4 wow">
                    <div class="block block-list block-viewed">
                        <div class="block-title">Address </div>
                        <div class="block-content">
                            <ol id="recently-viewed-items">
                                <li class="item odd">
                                    <p class="product-name" runat="server" id="shopAddress"></p>
                                </li>
                                <li class="item last even">
                                    <p class="product-name" runat="server" id="shopTelephone"></p>
                                </li>
                            </ol>
                        </div>
                    </div>
                    <div class="block block-list block-viewed">
                        <div class="block-title">Map </div>
                        <div class="block-content">
                            <script src='https://maps.googleapis.com/maps/api/js?v=3.exp'></script>
                            <div style='overflow: inherit; height: 250px; width: 335px;'>
                                <div id='gmap_canvas' style='height: 250px; width: 335px;'></div>
                                <div>
                                    <small><a href="http://embedgooglemaps.com">embed google maps </a></small>
                                </div>
                                <div>
                                    <small><a href="https://termsandcondiitionssample.com">terms and conditions sample</a></small>
                                </div>
                                <style>
                                    #gmap_canvas img {
                                        max-width: none !important;
                                        background: none !important;
                                    }
                                </style>
                            </div>
                            <script type='text/javascript'>
                                function init_map() { var myOptions = { zoom: 15, center: new google.maps.LatLng(6.8209580, 79.8727630), mapTypeId: google.maps.MapTypeId.ROADMAP }; map = new google.maps.Map(document.getElementById('gmap_canvas'), myOptions); marker = new google.maps.Marker({ map: map, position: new google.maps.LatLng(6.8209690, 79.8727850) }); infowindow = new google.maps.InfoWindow({ content: '<strong>SAA Video</strong><br>387/E, Galle Road, Ratmalana.<br>' }); google.maps.event.addListener(marker, 'click', function () { infowindow.open(map, marker); }); infowindow.open(map, marker); } google.maps.event.addDomListener(window, 'load', init_map);
                            </script>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>

    <!--End main-container -->
</asp:Content>
