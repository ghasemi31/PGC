<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OldUpdateProfile.aspx.cs" Inherits="Pages_User_UpdateProfile" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="kfk" %>
<%@ Import Namespace="kFrameWork.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = (!string.IsNullOrEmpty(kFrameWork.UI.UserSession.User.FullName))?"ویرایش اطلاعات-"+kFrameWork.UI.UserSession.User.FullName:""; %>
    <link href="/assets/css/UserProfile/UserProfile.min.css?v=2.2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <section class="main-body content">
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

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 update-form-group">
                                <div class="update-form-item">
                                    <label><i class="fa fa-user"></i></label>
                                    <asp:TextBox ID="txtFullName" CssClass="input-text" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator3" runat="server"
                                        ErrorMessage="لطفا نام و نام خانوادگی خود را وارد کنید." ControlToValidate="txtFullName"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="update-form-item">
                                    <label><i class="fa fa-credit-card-alt" aria-hidden="true"></i></label>
                                    <asp:TextBox ID="txtNationalCode" CssClass="input-text" placeholder="کد ملی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server"
                                        ErrorMessage="لطفا کد ملی خود را وارد کنید." ControlToValidate="txtNationalCode"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="update-form-item">
                                    <label><i class="fa fa-phone" aria-hidden="true"></i></label>
                                    <asp:TextBox ID="txtTel" CssClass="input-text" placeholder="تلفن" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator4" runat="server"
                                        ErrorMessage="لطفا تلفن خود را وارد کنید." ControlToValidate="txtTel"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 update-form-group">
                                <div class="update-form-item">
                                    <label><i class="fa fa-envelope"></i></label>
                                    <asp:TextBox ID="txtEmail" CssClass="input-text" placeholder="پست الکترونیک" ClientIDMode="Static" ReadOnly="true" runat="server"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredEmail" runat="server"
                                        ErrorMessage="لطفا پست الکترونیک خود را وارد نمایید" ControlToValidate="txtEmail"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ErrorMessage="پست الکترونیک معتبر نمی باشد"
                                        Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" ControlToValidate="txtEmail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Display="Dynamic">
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="update-form-item">
                                    <label><i class="fa fa-credit-card-alt" aria-hidden="true"></i></label>
                                    <asp:TextBox ID="txtPostalCode" CssClass="input-text" placeholder="کد پستی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="لطفا کد پستی خود را وارد کنید." ControlToValidate="txtPostalCode"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="update-form-item">
                                    <label><i class="fa fa-mobile" aria-hidden="true"></i></label>
                                    <asp:TextBox ID="txtMobile" CssClass="input-text" placeholder="تلفن همراه" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator5" runat="server"
                                        ErrorMessage="لطفا تلفن همراه خود را وارد کنید." ControlToValidate="txtMobile"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-12" style="padding: 0 2em;">
                                <div class="update-form-item">
                                    <label><i class="fa fa-map-marker" aria-hidden="true"></i></label>
                                    <asp:TextBox ID="txtAddress" CssClass="input-text" placeholder="آدرس" ClientIDMode="Static" runat="server"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator6" runat="server"
                                        ErrorMessage="لطفا آدرس را وارد کنید." ControlToValidate="txtAddress"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //login form effect
            $('.user-info input , .user-info textarea').focus(function () {
                $(this).siblings("hr").css({ "border-color": "#da251d" });
                $(this).siblings("label").css({ "color": "#da251d" });
                $(this).addClass("red-placeholder");
            });
            $('.user-info input , .user-info textarea').focusout(function () {
                $(this).siblings("hr").css({ "border-color": "#eeeeee" });
                $(this).siblings("label").css({ "color": "#423F3F" });
                $(this).removeClass("red-placeholder");
            });
        })
    </script>
</asp:Content>

