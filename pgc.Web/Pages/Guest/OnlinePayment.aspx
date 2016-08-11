<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OnlinePayment.aspx.cs" Inherits="Pages_Guest_OnlinePayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>مستردیزی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" Runat="Server">
    <%--https://sep.shaparak.ir/Payment.aspx--%>
     <form action="<%=kFrameWork.Business.OptionBusiness.GetText(pgc.Model.Enums.OptionKey.Mellat_BankURL) %>" method="post" name="payment">
         <input type="hidden" id="RefId" name="RefId" value="<%=RefId%>"/>
       

        <div class="container" style="text-align: center;margin-top: 2em;font-size: 1.2em;">
            کاربر گرامی، برای اتصال به درگاه بانک سامان لطفا منتظر بمانید.
        </div>

        <input type="submit" id="btnSave" style="visibility: hidden" name="btnSave" value="" />
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" Runat="Server">
    <script type="text/javascript">
        document.getElementById("btnSave").click();
    </script>
</asp:Content>

