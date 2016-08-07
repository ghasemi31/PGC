<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="Pages_Admin_News_Confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="<%#ResolveClientUrl("~/Scripts/Shared/SetSelectedMenuItem.js") %>" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
    <fieldset>
        <legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
        <input type="hidden" id="SelectedMenuItem" value="<%= GetRouteUrl("admin-news",null) %>" />
        <table>

            <tr>
                <td class="caption">عنوان خبر</td>
                <td class="control"><%= news.Title %></td>
            </tr>
            <tr>
                <td class="caption">خلاصه خبر</td>
                <td class="control"><%= news.Summary %></td>
            </tr>
            <tr>
                 <td class="caption">متن خبر</td>
                <td class="control"><%=news.Body %></td>
            </tr>
            
        </table>
        <div class="commands">
            <%--<asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />--%>
            <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
        </div>
    </fieldset>
</asp:Content>

