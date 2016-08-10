<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="OnlinePayment.aspx.cs" Inherits="Pages_Guest_OnlinePayment" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>پرداخت اینترنتی</title>
</head>
<body>
    <form action="<%=kFrameWork.Business.OptionBusiness.GetText(pgc.Model.Enums.OptionKey.Mellat_BankURL) %>" method="post" name="payment">
         <input type="hidden" id="RefId" name="RefId" value="<%=RefId%>"/>
       
 
        <div style="clear: both;display:none;">
            <button type="submit" name="Save" class='t-button t-state-default'>اتصال به درگاه پرداخت بانک ملت</button>
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        document.payment.submit();
    </script>
</body>
</html>

