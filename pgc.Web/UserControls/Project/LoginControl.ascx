<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LoginControl.ascx.cs" Inherits="UserControls_Project_LoginControl" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="kfk" %>
<div class="login">
    <asp:MultiView runat="server" ID="mlv">
        <asp:View runat="server">
            <div class="login-form-content">
                <div class="login-content">
                    <input runat="server" id="txtEmail" type="text" class="form-control" placeholder="پست الکترونیک" />                   
                </div>
                <div class="login-content">
                    <input runat="server" id="txtPass" type="password" class="form-control" placeholder="کلمه عبور" />
                </div>
                <div id="loginCaptcha" runat="server" class="login-content" style="display:none">
                    <asp:ScriptManager ID="sm" runat="server">
                    </asp:ScriptManager>
                    <asp:UpdatePanel ID="uplCaptcha" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12 padding-left-right-0">
                                <asp:TextBox ID="txtCaptcha" CssClass="form-control" placeholder="کد امنیتی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                <span runat="server" id="captchavalidation" style="font-size:10px; color:#CC0000;font-family:Tahoma;display:none;">لطفا کد امنیتی را وارد نمایید</span>
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
                                    <input type="submit" runat="server" class="btn-captch" value="&#xf021;" onclick="" causesvalidation="false" />
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="clear"></div>


                <div id="login-form-link">
                    <span><a href="<%=GetRouteUrl("guest-signup",null) %>">ثبت نام</a></span>
                </div>
                <div class=" display-center">
                    <asp:Button runat="server" ID="btnLogin" CssClass="btnLoginForm btnEndLoginForm" ClientIDMode="Static" Text="ورود به سامانه" OnClick="Login" />
                </div>
            </div>
        </asp:View>
        <asp:View runat="server">
            <div class="login-form-content">
                <div class="row">
                    <%=(!string.IsNullOrEmpty(kFrameWork.UI.UserSession.User.FullName)?kFrameWork.UI.UserSession.User.FullName:"کاربر")  + "  عزیز،"%>  خوش آمدید
                </div>
                <div class="row display-center">
                    <%if (kFrameWork.UI.UserSession.User.AccessLevel_ID == 2)
                      {%>
                    <asp:Button runat="server" Text="پروفایل من" CssClass="btnLoginForm btnEndLoginForm" OnClick="CPanelClick" autocomplete="off" />
                    <%}
                      else
                      {%>
                    <asp:Button runat="server" Text="کنترل پنل" CssClass="btnLoginForm btnEndLoginForm" OnClick="CPanelClick" autocomplete="off" />
                    <% }%>
                    <asp:Button runat="server" ID="t" Text="خروج" CssClass="btnLoginForm btnEndLoginForm" OnClick="LogOut" autocomplete="off" />
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
      
</div>
<script type="text/javascript" language="javascript">

    function controlEnter(obj, event) {
        var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
        if (keyCode == 13) {
            __doPostBack(obj, '');
            return false;
        }
        else {
            return true;
        }
    }
</script>