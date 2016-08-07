<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="User.aspx.cs" Inherits="Pages_Guest_User" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="kfk" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">

    <%this.Title = OptionBusiness.GetText(OptionKey.User_Title); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.User_Description) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.User_Keywords) %>" />
    <!-- BEGIN REGISTER PAGE STYLE -->
    <link href="/assets/css/Login/login.min.css?v=2.2" rel="stylesheet" />
    <!-- END REGISTER PAGE STYLE -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <!-- BEGIN CONTENT -->
    <section class="main-body">
        <div class="container-fluid">
            <div id="login-section" class="row">
                <div id="login" class="col-lg-offset-4 col-lg-4 col-md-offset-4 col-md-4 col-sm-offset-3 col-sm-6 col-xs-offset-0 col-xs-12">
                    <header>
                        <h1>ثبت نام در مستردیزی</h1>
                        <hr />
                    </header>
                    <div id="login-form">
                        <form runat="server">
                            <div class="login-form-content">
                                <div class="login-content">
                                    <label class="login-form-label"><i class="fa fa-user"></i></label>
                                    <asp:TextBox ID="txtFullName" CssClass="input-text" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server" autocomplete="off"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server"
                                        ErrorMessage="لطفا نام و نام خانوادگی خود را وارد نمایید" ControlToValidate="txtFullName"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="login-content">
                                    <label class="login-form-label"><i class="fa fa-envelope"></i></label>
                                    <asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="input-text" placeholder="پست الکترونیک" runat="server" autocomplete="off"></asp:TextBox>
                                    <hr />
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
                                <div class="login-content">
                                    <label class="login-form-label"><i class="fa fa-key"></i></label>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" ClientIDMode="Static" placeholder="کلمه عبور" CssClass="input-text" runat="server" autocomplete="off"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredPassword" runat="server"
                                        ErrorMessage="لطفا کلمه عبور خود را وارد نمایید" ControlToValidate="txtPassword"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </div>
                                <div class="login-content">
                                    <label class="login-form-label"><i class="fa fa-key"></i></label>
                                    <asp:TextBox ID="txtRePassword" TextMode="Password" ClientIDMode="Static" placeholder="تکرار کلمه عبور" CssClass="input-text" runat="server" autocomplete="off"></asp:TextBox>
                                    <hr />
                                    <asp:RequiredFieldValidator
                                        ID="RequiredRePassword" runat="server"
                                        ErrorMessage="لطفا کلمه عبور خود را تکرار نمایید" ControlToValidate="txtRePassword"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                                        ErrorMessage="کلمه عبور یکسان نمی باشد" ControlToCompare="txtPassword"
                                        ControlToValidate="txtRePassword" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:CompareValidator>
                                </div>
                                <div class="login-content">
                                    <asp:ScriptManager ID="sm" runat="server">
                                    </asp:ScriptManager>
                                    <asp:UpdatePanel ID="uplCaptcha" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12 padding-left-right-0">
                                                <label><i class="fa fa-question-circle"></i></label>
                                                <asp:TextBox ID="txtCaptcha" CssClass="input-text" placeholder="کد امنیتی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                <hr />
                                                <asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator2" runat="server"
                                                    ErrorMessage="لطفا کد امنیتی را وارد نمایید" ControlToValidate="txtCaptcha"
                                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                                <asp:CustomValidator ErrorMessage="کد امنیتی اشتباه است" OnServerValidate="ValidateCaptcha"
                                                    runat="server" Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" Display="Dynamic" />
                                            </div>
                                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12 padding-left-right-0">
                                                <div id="captcha" class="display-center">
                                                    <kfk:CaptchaControl ID="Captcha6" runat="server"
                                                        CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                        CaptchaHeight="40" CaptchaWidth="120" CssClass="col-md-12"
                                                        CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                                        CaptchaMaxTimeout="240" FontColor="#da251d" />
                                                   <input type="submit" runat="server"  class="btn-captch" value="&#xf021;" onclick="" causesvalidation="false" />
                                                </div>
                                                <div class="guide-captcha">
                                                    <p>
                                                        CAPTCHA یا همان کپچا نرم افزاری آنلاین برای تولید سوالات و آزمون هایی است که انسان براحتی قادر به پاسخ گویی به آنها است ولی کامپیوترها در حال حاضر قادر به تشخیص و پاسخ به آنها نیستند.
                                                    </p>
                                                </div>
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="login-content" style="margin-top: 6em;">
                                    <asp:Button CssClass="btnRegisterForm" runat="server" Text="ثبت نام" OnClick="btnSave_Click" />
                                </div>
                            </div>
                            <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <div class="clear"></div>
    </section>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <!-- BEGIN REGISTER PAGE SCRIPT -->
    <script src="/assets/js/Login/login.min.js?v=2.2"></script>
    <!-- BEGIN REGISTER PAGE SCRIPT -->
</asp:Content>

