<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OldLoginControl.ascx.cs" Inherits="UserControls_Project_LoginControl" %>
<div class="login">
<asp:MultiView runat="server" ID="mlv">
    <asp:View runat="server">
        <input runat="server" id="txtUserName" type="text" class="fontClass username" value="نام کاربری را وارد کنید" onfocus="this.value ='';"/>
        <input runat="server" id="txtPass" type="password" class="fontClass pass" onfocus="pass_focus()"/>
        <span class="fontClass passlabel" onclick="pass_click()">رمز عبور را وارد کنید</span>
        <asp:Button runat="server" ID="btnLogin" clientidmode="Static" Text="ورود" CssClass="btn signin" OnClick="Login"  />
        <input type="hidden" id="user_state" value="1"/>

        <div class="fontClass links">
            » <a href="<%=GetRouteUrl("guest-signup",null) %>">ثبت نام در مستردیزی</a><br />
            <%--» <a href="#">فراموش کردن رمز</a><br />--%>
        </div>
    </asp:View>
    <asp:View runat="server">
        <div class="loginedview fontClass">
            <%=((pgc.Model.Enums.Gender)kFrameWork.UI.UserSession.User.Gender == pgc.Model.Enums.Gender.Female) ? "خانم" : "آقای" %> <%=kFrameWork.UI.UserSession.User.Fname + " " + kFrameWork.UI.UserSession.User.Lname%> به مستر دیزی خوش آمدید
            <asp:Button runat="server" ID="btnConsole" Text="کنترل پنل" CssClass="cpanel btn fontClass" OnClick="CPanelClick" autocomplete="off"/>
            <asp:Button runat="server" ID="btnLogOut" Text="خروج" CssClass="exit btn fontClass" OnClick="LogOut" autocomplete="off"/>
            <%if (kFrameWork.UI.UserSession.User.AccessLevel_ID == 2 && kFrameWork.UI.UserSession.User.Orders.Count != 0)
              { %>
              <div>
                <asp:Button runat="server" ID="btnOrderList" Text="سفارشات" CssClass="orderlist btn fontClass" OnClick="OrderListClick"/>
              </div>
              <%} %>
        </div>
    </asp:View>
</asp:MultiView>
</div>