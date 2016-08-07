<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
<br />
<br />
<b>به پنل مدیریتی مستر دیزی خوش آمدید ، </b>
<br />
<br />
<%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.Admin_GreetingMessage) %>
<br />
<br />
شما می توانید از جعبه ابزار راست صفحه برای رجوع به صفحات مدیریتی استفاده نمائید.
</asp:Content>

