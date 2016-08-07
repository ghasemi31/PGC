<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="DisplayCircular.aspx.cs" Inherits="Pages_Agent_Circular_DisplayCircular" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">
    <fieldset>
        <legend></legend>
        <div id="circular">
            <div id="title"><%=circular.Title %></div>
            <div id="date"><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(circular.Date) %></div>
            <div id="body"><%=circular.Body %></div>
        </div>
    </fieldset>

</asp:Content>

