<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test.aspx.cs" Inherits="Pages_Guest_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

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
    <CKEditor:CKEditorControl ID="CKEditor1" BasePath="/ckeditor/" runat="server" />
    </form>
</body>
</html>
