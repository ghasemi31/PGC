<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Loading.ascx.cs" Inherits="UserControls_Common_Loading" %>
<div style="display:none" id="divBlock" class="loading">
    <asp:Image runat="server" ID="Image1" ImageUrl="~/Styles/UserControls/Common/images/loading_ajax.gif"
        AlternateText="در حال بارگذاری ..." CssClass="loading" />        
    <div class="loadingtext">
        در حال بارگذاری ...</div>        
</div>