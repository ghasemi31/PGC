<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_User_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<br />
<br />
<b>به پنل کاربری Iran PGC خوش آمدید ، </b>
<br />
<br />
<br />
<b style="color:#A81C1B;">کد اشتراک: <%=kFrameWork.UI.UserSession.UserID%></b>
<br />
<br />
<b style="color:#A81C1B;">نام و نام خانوادگی: <%=kFrameWork.UI.UserSession.User.FullName %></b>
<br />
<br />
<b style="color:#A81C1B;"> پست الکترونیک: <%=kFrameWork.UI.UserSession.User.Email%></b>
<br />
<%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.User_GreetingMessage) %>
<br />
<br />
شما می توانید از جعبه ابزار راست صفحه برای رجوع به صفحات کاربری استفاده نمائید.
</asp:Content>

