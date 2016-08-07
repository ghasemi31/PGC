<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="Pages_Guest_404" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.PreHead)%>
    <title></title>
    <%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.PostHead)%>
</head>
<body>
    <%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.PreBody)%>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    <%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.PostBody)%>
</body>
</html>
