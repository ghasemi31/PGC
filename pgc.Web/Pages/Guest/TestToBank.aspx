<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestToBank.aspx.cs" Inherits="Pages_Guest_TestToBank" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>        
   <form action="<%=ResolveUrl("http://localhost:8030/pages/guest/callbackurl.aspx") %>" method="post" ><%--<%=ResolveClientUrl(new pgc.Business.Core.SamanOnlinePayment().RedirectURL) --%>
  وضعیت:<input type="text" name="State" /><br />
  رسید دیجیتالی<input type="text" name="RefNum" /><br />
  شماره رزروی خودمان<input type="text" name="ResNum" /><br />
  
  
 
 <div style="clear: both;">
            <button id="btn" type="submit" runat="server" clientidmode="Static" name="Save" class='t-button t-state-default'>خرید اینترنتی</button>
     
    </div>
    </form>
    
</body>
</html>
