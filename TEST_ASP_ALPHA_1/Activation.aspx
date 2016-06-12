<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activation.aspx.cs" Inherits="TEST_ASP_ALPHA_1.Activation"  MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Activation</strong></li>
                </ul>
            </div>
        </div>
    </div>
    <!-- main-container -->
    <div class="main-container col2-right-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="row">
                <section class="col-main col-sm-9">
                    <div class="static-contain" runat="server" ID="successActMsgBox" visible="false">
                        <div class="page-title new_page_title">
                            <h2>SUCCESS!</h2>
                        </div>
                        <br />
                        <p>Thank you for choosing Saa Video Entertainments! Your account has been activated successfully.</p>
                        <p>You can now <a href="Login.aspx"><b>login</b></a> and continue enjoying our services!</p>
                        <br />
                        <p>Please feel free to talk to us whenever you want, we are always there to help.</p>
                        <p><b>Address &nbsp;&nbsp;&nbsp;: </b>387/E, Galle Road, Ratmalana.</p>
                        <p><b>Telephone : </b>0115929266 / 0115929267</p>
                        <p><b>Email &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: </b><a href="mailto:supprt.saavideos@gmail.com">supprt.saavideos@gmail.com</a></p>
                        <br />
                        <img src="images/smile_face.png" height="210" width="210" style="float:right;margin-right:-45px;margin-top:-215px;" />
                    </div>
                    <div class="static-contain" runat="server" ID="errorActMsgBox" visible="false">
                        <div class="page-title new_page_title">
                            <h2>ERROR!</h2>
                        </div>
                        <br />
                        <p style="color:red">An error occurred when trying to activate your account! Please try again later.</p>
                        <br />
                        <p>Please feel free to talk to us whenever you want, we are always there to help.</p>
                        <p><b>Address &nbsp;&nbsp;&nbsp;: </b>387/E, Galle Road, Ratmalana.</p>
                        <p><b>Telephone : </b>0115929266 / 0115929267</p>
                        <p><b>Email &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: </b><a href="mailto:supprt.saavideos@gmail.com">supprt.saavideos@gmail.com</a></p>
                        <br />
                        <img src="images/sad_face_1.png" height="190" width="190" style="float:right;margin-right:-45px;margin-top:-185px;" />
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-company">
                        <div class="block-title">Company </div>
                        <div class="block-content">
                            <ol id="recently-viewed-items">
                                <li class="item odd"><strong>About Us</strong></li>
                                <li class="item even"><a href="Contact.aspx">Contact Us</a></li>
                                <li class="item last"><a href="BrowseGamesGrid.aspx">Browse...</a></li>
                            </ol>
                        </div>
                    </div>
                    <div class="block block-subscribe">
                        <div class="block-title">Newsletter</div>
                        <form Id="newsLetterForm" method="post" runat="server">
                            <div class="block-content">
                                <div class="form-subscribe-header">Sign up for our newsletter:</div>
                                <input type="email" placeholder="Enter your email address" class="input-text required-entry validate-email" title="" Id="dfnEmailAdd" name="email" runat="server" style="z-index:10;" required>
                                <div class="success" runat="server" ID="successBox" style="width:210px;height:30px;margin-top:10px;padding:8px;" visible="false">
                                  <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                        Thank you!
                                </div>
                                <div class="error" runat="server" ID="errorBox" style="width:210px;height:30px;margin-top:10px;padding:8px;" visible="false">
                                  <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span> 
                                        Error Occurred!
                                </div>
                                <div class="actions">
                                    <asp:Button CssClass="button button-subscribe" Text="SUBSCRIBE" runat="server" ID="btnSubscribe" OnClick="btnSubscribe_Click"></asp:Button>
                                </div>
                            </div>
                        </form>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <!--End main-container -->
</asp:Content>

