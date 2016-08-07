<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Report_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان گزارش</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">خلاصه گزارش</td>
        <td class="control"><kfk:NormalTextBox ID="txtSummary" runat="server" TextMode="MultiLine" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">کلمات کلیدی متا</td>
        <td class="control"><kfk:NormalTextBox ID="txtMetaKeyWords" runat="server" TextMode="MultiLine"/></td>
    </tr>
    <tr>
        <td class="caption">توضیحات متا</td>
        <td class="control"><kfk:NormalTextBox ID="txtMetaDesc" runat="server" TextMode="MultiLine"/></td>
    </tr>
    <tr>
        <td class="caption">نکات ویژه</td>
        <td class="control"><kfk:NormalTextBox ID="txtNote" runat="server" TextMode="MultiLine"/></td>
    </tr>
    <tr>
         <td class="caption">متن اطلاعیه</td>
        <td class="control"><kfk:HtmlEditor ID="txtBody" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">عکس</td>
        <td class="control">><kfk:FileUploader ID="fupReportPic" runat="server" SaveFolder="~/userfiles/report/" />
           <ul style="color:#bd0019">
                <li>فرمت مناسب: png/gif</li>
                <li>width مناسب: 162 px</li>
                <li>height مناسب: 102 px</li>
                <li><a href="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/PSDs/Frame.psd") %>" style=" text-decoration:underline; color:#bd0019">نمومه قالب(فایل psd)</a></li>
            </ul>
        </td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

