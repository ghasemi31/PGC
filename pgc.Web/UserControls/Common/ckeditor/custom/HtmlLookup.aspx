<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HtmlLookup.aspx.cs" Inherits="UserControls_Common_ckeditor_HtmlLookup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #apply , #cancel
        {
            background-color:#a81c1b;
            border:none;
            color:White;
            text-align:center;
            line-height:21px;
            height:21px;
            padding:0 10px 0 10px;
            cursor:pointer;
            font-family:Tahoma;
            font-size:11px;
            -moz-border-radius: 4px; 
            -khtml-border-radius: 4px; 
            -webkit-border-radius: 4px; 
            border-radius: 4px;
            margin:15px 5px;
        }
        #apply:hover , #cancel:hover
        {
            background-color:#d50e0c;
        }
        #apply{width:100px;}
        #cancel{width:60px;}
    </style>
    <script type="text/javascript" src="<%=ResolveClientUrl("~/pages/html/ui/js/jquery-1.7.2.min.js") %>"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <kfk:Ckeditor ID="html_editor" ClientIDMode="Static" runat="server" BaseHref="~/UserControls/Common/ckeditor" Width="99%" Height="360px"/>    
        <input type="button" id="apply" value="تایید" onclick="ok()"/>
        <input type="button" id="cancel" value="لغو" onclick="nok()"/>
    </div>
    <%string contID = "'" + Request.QueryString["cont"] + "'";%>
    <script language="javascript" type="text/javascript">
        CKEDitor_loaded = false;

        var textarea = window.parent.$('#' + <%=contID %>).children('textarea');
//        var framecontainer = window.parent.$('#' + <%=contID %> + '_cont');
//        var iframe = window.parent.$('#' + <%=contID %> + '_frame');

        CKEDITOR.on('instanceReady', function () {
            CKEditor_loaded = true;
            CKEDITOR.instances["html_editor"].setData(textarea.val());
        });

        function ok() {
            textarea.val(CKEDITOR.instances["html_editor"].getData());
            parent.closeHtmlLookup();
        }
        function nok() {
            parent.closeHtmlLookup();
        }
    </script>
    </form>
</body>
</html>
