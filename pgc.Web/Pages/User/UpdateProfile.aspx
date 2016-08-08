<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs" Inherits="Pages_User_UpdateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%this.Title = (!string.IsNullOrEmpty(kFrameWork.UI.UserSession.User.FullName)) ? "ویرایش اطلاعات-" + kFrameWork.UI.UserSession.User.FullName : ""; %>
    <link href="/assets/User/UserProfile.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <section id="page">
        <section id="main-content">
            <div class="container">
                <div class="row">
                <div class="col-lg-offset-2 col-lg-8 col-md-offset-1 col-md-10 col-sm-offset-0 col-sm-12 col-xs-offset-0 col-xs-12">
                    <div class="order-code">
                        <ul class="list-inline">
                            <li>حساب کاربری من  <i class="fa fa-angle-left" aria-hidden="true"></i></li>
                           <li><a href="<%=GetRouteUrl("user-userprofile",null) %>">پروفایل من</a><i class="fa fa-angle-left" aria-hidden="true"></i></li>
                            <li>ویرایش اطلاعات من</li>
                        </ul>
                    </div>
                    <form runat="server">
                        <div class="user-info">
                            <header>
                                <i class="fa fa-pencil-square-o" aria-hidden="true"></i>
                                <span>ویرایش اطلاعات</span>
                                <hr />
                            </header>
                            <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <kfk:LookupCombo ID="lkpSexStatus" runat="server" CssClass="form-control" EnumParameterType="pgc.Model.Enums.Gender" />
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="نام و نام خانوادگی"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server"
                                    ErrorMessage="لطفا نام و نام خانوادگی خود را وارد نمایید" ControlToValidate="txtFullName"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtNationalCode" CssClass="form-control" placeholder="کد ملی" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="کد ملی"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator2" runat="server"
                                    ErrorMessage="لطفا کد ملی خود را وارد نمایید" ControlToValidate="txtNationalCode"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtFatherName" CssClass="form-control" placeholder="نام پدر" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="نام پدر"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server"
                                    ErrorMessage="لطفا نام پدر خود را وارد نمایید" ControlToValidate="txtFatherName"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <kfk:LookupCombo ID="lkcProvince" class="form-control width100"
                                    runat="server"
                                    BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness"
                                    AutoPostBack="true"
                                    DependantControl="lkcCity"
                                    Required="true" />
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <kfk:LookupCombo ID="lkcCity" class="form-control width100"
                                    runat="server"
                                    BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness"
                                    DependOnParameterName="Province_ID"
                                    DependOnParameterType="Int64"
                                    Required="true" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtTel" CssClass="form-control" placeholder="تلفن ثابت" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="تلفن ثابت"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator4" runat="server"
                                    ErrorMessage="لطفا تلفن ثابت خود را وارد نمایید" ControlToValidate="txtTel"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtMobile" CssClass="form-control" placeholder="تلفن همراه" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="تلفن همراه"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator5" runat="server"
                                    ErrorMessage="لطفا تلفن همراه خود را وارد نمایید" ControlToValidate="txtMobile"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtAddress" CssClass="form-control" placeholder="آدرس" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="آدرس"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator6" runat="server"
                                    ErrorMessage="لطفا آدرس خود را وارد نمایید" ControlToValidate="txtAddress"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                <asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="form-control" placeholder="پست الکترونیک" runat="server" autocomplete="off" ToolTip="پست الکترونیک"></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="RequiredEmail" runat="server"
                                    ErrorMessage="لطفا پست الکترونیک خود را وارد نمایید" ControlToValidate="txtEmail"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="پست الکترونیک معتبر نمی باشد"
                                    Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" ControlToValidate="txtEmail"
                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                            </div>
                        </div>
                            <div class="float-left">
                                <a class="btn-profile" href="<%=GetRouteUrl("user-userprofile",null) %>">انصراف</a>
                                <asp:Button ID="btnSave" CssClass="btn-profile btn-margin" runat="server" Text="ثبت اطلاعات" OnClick="btnSave_Click" />
                            </div>
                            <div class="clear"></div>
                        </div>                     
                    </form>
                </div>
            </div>
            <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
            </div>
        </section>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
</asp:Content>

