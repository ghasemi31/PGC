<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OnlinePayment.aspx.cs" Inherits="Pages_Guest_OnlinePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" Runat="Server">
    <title>مستردیزی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" Runat="Server">
    <%--https://sep.shaparak.ir/Payment.aspx--%>
    <form action="<%=new pgc.Business.Core.SamanOnlinePayment().SamanUrl %>" method="post" name="onlinePaymentForm">
        <input type="hidden" id="Amount" name="Amount" style="visibility: hidden" value="<%=online.Amount.ToString() %>" />
        <input type="hidden" id="ResNum" name="ResNum" style="visibility: hidden" value="<%=online.ResNum %>" />
        <input type="hidden" id="Wage" name="Wage" style="visibility: hidden" value="0" />
        <input type="hidden" id="MID" name="MID" style="visibility: hidden" value="<%=new pgc.Business.Core.SamanOnlinePayment().MerchantID%>" />
        <input type="hidden" id="RedirectURL" name="RedirectURL" style="visibility: hidden" value="<%=new pgc.Business.Core.SamanOnlinePayment().RedirectURL %>" />

        <div class="container" style="text-align: center;margin-top: 2em;font-size: 1.2em;">
            کاربر گرامی، برای اتصال به درگاه بانک سامان لطفا منتظر بمانید.
        </div>

        <input type="submit" id="btnSave" style="visibility: hidden" name="btnSave" value="" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" Runat="Server">
    <script type="text/javascript">
        document.getElementById("btnSave").click();
    </script>
</asp:Content>

