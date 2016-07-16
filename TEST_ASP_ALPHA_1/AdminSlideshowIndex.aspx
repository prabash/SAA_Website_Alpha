<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminSlideshowIndex.aspx.cs" Inherits="TEST_ASP_ALPHA_1.AdminSlideshowIndex" MasterPageFile="~/Admin.Master" %>

<%@ Import Namespace="TEST_ASP_ALPHA_1.Common" %>
<%@ Import Namespace="TEST_ASP_ALPHA_1.Models" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Breadcrumbs -->
    <div class="breadcrumbs" style="margin-left: -15px">
        <div class="container">
            <div class="row">
                <ul>
                    <li class="home"><a title="Go to Home Page" href="Default.aspx">Home</a><span>&raquo;</span></li>
                    <li class="category13"><strong>Index Slideshow</strong></li>
                </ul>
            </div>
        </div>
    </div>
    <!-- main-container -->
    <div class="main-container col2-right-layout" style="margin-left: -15px">
        <div class="main container">
            <div class="row">
                <section class="col-main col-sm-9 wow bounceInUp animated">
                    <div class="my-account">
                        <div class="page-title new_page_title">
                            <h2>Index Slideshow</h2>
                        </div>
                        <div class="dashboard">
                            <div class="welcome-msg">
                                <strong>Hello there, Admin!</strong>
                                <p>You can use this window to add or edit the items that will be shown in the slideshow of the homepage.</p>
                                <br />
                                <p><b>INSTRUCTIONS :</b></p>
                                <ul style="margin-left: 15px; list-style-type: square">
                                    <li style="line-height: 0.5em;">
                                        <p>Please make sure the item dimension is = <b>860px * 320px</b></p>
                                    </li>
                                    <li style="line-height: 0.5em;">
                                        <p>Always use the same folder for "location" and <b>only change the file name</b></p>
                                    </li>
                                </ul>
                                <br />
                            </div>
                            <div class="recent-orders">
                                <form runat="server">
                                    <div class="table-responsive">
                                        <input type="text" id="txtSearch" onkeyup="Search(this.value)" placeholder="Search"/>
                                        <br />
                                        <asp:GridView ID="IndexSlideshowGrid" runat="server" CssClass="data-table" AutoGenerateColumns="false" ShowFooter="true" AllowSorting="true" AllowPaging="true" PageSize="5"
                                            OnRowEditing="lnkEdit_Click" OnRowCancelingEdit="IndexSlideshowGrid_RowCancelingEdit" OnRowUpdating="IndexSlideshowGrid_RowUpdating">
                                            <HeaderStyle BackColor="#80c94c" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' ID="lblId" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TITLE">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditTitle" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "title")%>' class="input-text required-entry"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewTitle" runat="server" class="input-text required-entry"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LOCATION">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "location")%>' />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtEditLocation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "location")%>' class="input-text required-entry"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewLocation" runat="server" class="input-text required-entry"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" OnClick="lnkDelete_Click" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Button runat="server" Text="ADD" ID="btnAdd" OnClick="btnAdd_Click" CssClass="button"></asp:Button>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField EditText="Edit" CancelText="Cancel" ShowEditButton="true" />
                                            </Columns>
                                        </asp:GridView>
                                        <div class="error" runat="server" id="regErrorBox" visible="false" style="height: 30px; margin-top: 10px; padding: 8px;">
                                            <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>
                                            <p runat="server" id="regErrMsg"></p>
                                        </div>
                                        <div class="success" runat="server" id="regSuccessBox" visible="false" style="height: 30px; margin-top: 10px; padding: 8px;">
                                            <span class="closebtn" onclick="this.parentElement.style.display='none';">&times;</span>
                                            <p runat="server" id="regSuccessMsg"></p>
                                        </div>
                                    </div>
                                </form>
                            </div>
                            <br />
                        </div>
                    </div>
                </section>
                <aside class="col-right sidebar col-sm-3 wow bounceInUp animated">
                    <div class="block block-account">
                        <div class="block-title">My Account</div>
                        <div class="block-content">
                            <ul>
                                <li class="current"><a href="#">Index Slideshow</a></li>
                                <li><a href="MyAccountInformation.aspx">Items Slideshow</a></li>
                            </ul>
                        </div>
                    </div>
                </aside>
            </div>
        </div>
    </div>
    <!--End main-container -->
    <script type="text/javascript">
        function Search(searchCriteria) {
            $.ajax({
                type: "POST",
                url: "AdminSlideshowIndex.aspx/Search",
                data: '{criteria: "' + searchCriteria + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {}
    </script>
</asp:Content>
