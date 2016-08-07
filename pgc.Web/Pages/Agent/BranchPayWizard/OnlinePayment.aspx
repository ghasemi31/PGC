<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="OnlinePayment.aspx.cs" Inherits="Pages_Guest_OnlinePayment" %>
 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1"  >
    <script src="<%=ResolveClientUrl("~/Scripts/Shared/jquery-1.7.2.min.js")%>" type="text/javascript"></script>
    <title></title>      
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="Amount" name="Amount" style="visibility:hidden"  value="<%=online.Amount.ToString() %>"/>
        <input type="hidden" id="ResNum" name="ResNum" style="visibility:hidden" value="<%=online.OnlineResNum %>"/>
        <input type="hidden" id="Wage" name="Wage" style="visibility:hidden" value="0" />
        <input type="hidden" id="MID" name="MID" style="visibility:hidden" value="<%=new pgc.Business.Core.SamanOnlinePayment().MerchantID%>"/>                     
        <input type="hidden" id="RedirectURL" name="RedirectURL" style="visibility:hidden" value="<%=new pgc.Business.Core.SamanOnlinePayment().BranchRedirectURL %>" />
        <asp:Button ID="btnSave" runat="server" Text="پرداخت آنلاین" CssClass="linkLong" UseSubmitBehavior="true" ClientIDMode="Static" style="display:none"/>
    </form>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSave').click();
        });

        //$('#btnSave').click();
    </script>    
</body>
</html>


