<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Pages_Guest_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
<%--    <script runat="server" type="text/c#">
      protected void Timer1_Tick(object sender, EventArgs e)
        {
            Label1.Text = "Panel refreshed at: " +
    DateTime.Now.ToLongTimeString();
     }
</script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager runat="server" id="ScriptManager1">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" id="UpdatePanel1">
          <ContentTemplate>
            <%--<asp:Timer runat="server" id="Timer1" Interval="10000" OnTick="Timer1_Tick"></asp:Timer>--%>
            <asp:Timer runat="server" id="Timer1" Interval="10000" ontick="Timer1_Tick" ></asp:Timer>
            <asp:Label runat="server" Text="Page not refreshed yet." id="Label1">
            </asp:Label>
          </ContentTemplate>
        </asp:UpdatePanel>
            <asp:Label runat="server" Text="Label" id="Label2"></asp:Label>
    </div>
    </form>
</body>
</html>
