<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Pages_User_ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = (!string.IsNullOrEmpty(kFrameWork.UI.UserSession.User.FullName)) ? "تغییر کلمه عبور-" + kFrameWork.UI.UserSession.User.FullName : ""; %>
    <link href="/assets/css/UserProfile/UserProfile.min.css?v=2.2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <section class="main-body content">
        <div class="container">
            <div class="row">
                <div class="col-lg-offset-4 col-lg-4 col-md-offset-4 col-md-4 col-sm-offset-2 col-sm-8 col-xs-offset-0 col-xs-12">
                    <div class="order-code">
                        <ul class="list-inline">
                            <li>حساب کاربری من  <i class="fa fa-angle-left" aria-hidden="true"></i></li>
                            <li><a href="<%=GetRouteUrl("user-userprofile",null) %>">پروفایل من</a><i class="fa fa-angle-left" aria-hidden="true"></i></li>
                            <li>تغییر کلمه عبور </li>
                        </ul>
                    </div>
                    <form runat="server">
                        <div class="user-info">
                            <header>
                                <i class="fa fa-unlock-alt" aria-hidden="true"></i>
                                <span>تغییر کلمه عبور</span>
                                <hr />
                            </header>
                            <div class="update-form-group">
                                <div class="update-form-item changepass">
                                    <label><i class="fa fa-key"></i></label>
                                    <asp:TextBox ID="txtPass" TextMode="Password" ClientIDMode="Static" placeholder="کلمه عبور فعلی" CssClass="input-text" runat="server" autocomplete="off"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredPassword" runat="server"
                                        ErrorMessage="لطفا کلمه عبور خود را وارد نمایید" ControlToValidate="txtPass"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="update-form-item changepass">
                                    <label><i class="fa fa-key"></i></label>
                                    <asp:TextBox ID="txtNewPass" TextMode="Password" ClientIDMode="Static" placeholder="کلمه عبور جدید" CssClass="input-text" runat="server" autocomplete="off"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server"
                                        ErrorMessage="لطفا کلمه عبور جدید را وارد نمایید" ControlToValidate="txtNewPass"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="update-form-item changepass">
                                    <label><i class="fa fa-key"></i></label>
                                    <asp:TextBox ID="txtRePass" TextMode="Password" ClientIDMode="Static" placeholder="تکرار کلمه عبور جدید" CssClass="input-text" runat="server" autocomplete="off"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredRePassword" runat="server"
                                        ErrorMessage="لطفا کلمه عبور جدید را تکرار نمایید" ControlToValidate="txtRePass"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                        ErrorMessage="کلمه عبور یکسان نمی باشد" ControlToCompare="txtNewPass"
                                        ControlToValidate="txtRePass" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:CompareValidator>
                                </div>
                                <div class="display-center">
                                    <a class="btn-profile" href="<%=GetRouteUrl("user-userprofile",null) %>">انصراف</a>
                                    <asp:Button ID="btnSave" CssClass="btn-profile" runat="server" Text="ثبت اطلاعات" OnClick="btnSave_Click" />
                                </div>
                                <div class="clear"></div>
                            </div>
                        </div>
                    </form>
                    <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

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

