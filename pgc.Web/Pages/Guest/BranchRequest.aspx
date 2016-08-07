<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="BranchRequest.aspx.cs" Inherits="Pages_Guest_BranchRequest" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="kfk" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = OptionBusiness.GetText(pgc.Model.Enums.OptionKey.request_title); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Description_BranchRequest) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Keywords_BranchRequest) %>" />
    <!-- BEGIN REQUEST PAGE STYLE -->
    <link href="/assets/css/Request/request.min.css?v=2.2" rel="stylesheet" />
    <!-- END REQUEST PAGE STYLE -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <!-- BEGIN CONTENT -->
    <section class="main-body">
        <div class="container-fluid padding0">
            <header>
                <div id="request-header-title">
                    <h1><%=OptionBusiness.GetText(pgc.Model.Enums.OptionKey.request_header_title) %></h1>
                    <h2><%=OptionBusiness.GetText(pgc.Model.Enums.OptionKey.request_header_text) %></h2>
                </div>
                <div id="request-header" class="container">
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <h1><i class="fa fa-user"></i><%=OptionBusiness.GetText(pgc.Model.Enums.OptionKey.request_subHeader_text) %></h1>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <h1><i class="fa fa-phone"></i><%=OptionBusiness.GetPhone(pgc.Model.Enums.OptionKey.request_subHeader_tel) %></h1>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                        <h1><i class="fa fa-envelope"></i><%=OptionBusiness.GetEmail(pgc.Model.Enums.OptionKey.request_subHeader_email) %></h1>
                    </div>
                </div>
            </header>
            <div id="request-section" class="row">
                <form runat="server">
                    <div id="request-form" class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="request-form-title">
                                <span>اطلاعات هویتی:</span>
                            </div>
                            <div>
                                <label><i class="fa fa-user"></i></label>
                                <asp:TextBox ID="txtFullName" CssClass="input-text" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <hr />
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server"
                                    ErrorMessage="لطفا نام و نام خانوادگی خود را وارد نمایید"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic" ControlToValidate="txtFullName">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div>
                                <label><i class="fa fa-building-o"></i></label>
                                <asp:TextBox ID="txtApplicatorName" CssClass="input-text" placeholder="نام شرکت" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <hr />
                            </div>
                            <div class="request-form-title">
                                <span>اطلاعات تماس:</span>
                            </div>
                            <div>
                                <label><i class="fa fa-envelope"></i></label>
                                <asp:TextBox ID="txtEmail" CssClass="input-text" placeholder="پست الکترونیک" ClientIDMode="Static" runat="server"></asp:TextBox>
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
                            <div>
                                <label><i class="fa fa-phone"></i></label>
                                <asp:TextBox ID="txtPhone" CssClass="input-text" placeholder="تلفن تماس" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <hr />
                            </div>
                            <div>
                                <label><i class="fa fa-mobile"></i></label>
                                <asp:TextBox ID="txtMobile" CssClass="input-text" placeholder="تلفن همراه" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <hr />
                                <asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator3" runat="server"
                                    ErrorMessage="لطفا شماره تلفن همراه خود را وارد نمایید" ControlToValidate="txtMobile"
                                    Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ErrorMessage="شماره تلفن همراه معتبر نمی باشد" ControlToValidate="txtMobile"
                                    runat="server" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" CssClass="rqurecontct" Font-Size="10px" />
                            </div>
                            <div class="request-form-textarea">
                                <label><i class="fa fa-map-marker"></i></label>
                                <textarea runat="server" clientidmode="Static" id="txtAddress" name="textarea" class="input-text" placeholder="آدرس" autocomplete="false"></textarea>
                                <hr />
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                            <div class="request-form-title">
                                <span>اطلاعات نمایندگی:</span>
                            </div>
                            <div>
                                <label><i class="fa fa-map-marker"></i></label>
                                <asp:TextBox ID="txtLocation" CssClass="input-text" placeholder="مکان مورد نظر" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <hr />
                                <asp:RequiredFieldValidator
                                    ID="RequiredLocation" runat="server"
                                    ErrorMessage="لطفا مکان موردنظر خود را وارد نمایید" ControlToValidate="txtLocation"
                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                </asp:RequiredFieldValidator>
                            </div>
                            <div id="request-form-place">
                                <span>نوع ملک</span>
                                <input type="radio" id="rbPersonal" runat="server" clientidmode="Static" name="radios" value="1" checked="true" />
                                <span>شخصی</span>
                                <input type="radio" id="rbLeased" runat="server" clientidmode="Static" name="radios" value="2" />
                                <span>استیجاری</span>
                                <hr />
                            </div>
                            <div class="request-form-textarea">
                                <label><i class="fa fa-sticky-note"></i></label>
                                <textarea runat="server" clientidmode="Static" id="txtDesc" name="textarea" class="input-text" placeholder="توضیحات" autocomplete="false"></textarea>
                                <hr />
                            </div>
                            <div id="request-form-history">
                                <input type="checkbox" id="chkBachground" runat="server" clientidmode="Static" name="checkboxsingle" value="admin" />
                                <span>سابقه فعالیت در زمینه مواد غذایی را دارم.</span>
                            </div>
                            <div>
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
                                                Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" Display="Dynamic">
                                            </asp:RequiredFieldValidator>
                                            <asp:CustomValidator ErrorMessage="کد امنیتی اشتباه است" OnServerValidate="ValidateCaptcha"
                                                runat="server" Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" Display="Dynamic" />
                                        </div>
                                        <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12 padding-left-right-0">
                                            <div id="captcha" class="display-center">
                                                <kfk:CaptchaControl ID="Captcha1" runat="server"
                                                    CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                    CaptchaHeight="40" CaptchaWidth="120" CssClass="col-md-8"
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
                        </div>

                    </div>
                    <asp:Button ID="btnSave" CssClass="btn-request" runat="server" Text="ثبت" OnClick="btnSave_Click" />
                    <div class="clear"></div>
                    <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
                </form>
            </div>
        </div>
        <div class="clear"></div>
    </section>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <!-- BEGIN REQUEST PAGE SCRIPT -->
    <script src="/assets/js/Request/request.min.js?v=2.2"></script>
    <!-- BEGIN REQUEST PAGE SCRIPT -->
</asp:Content>

