<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_DynPage_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان صفحه</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" /></td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">هدینگ صفحه</td>
        <td class="control"><kfk:NormalTextBox ID="txtHeading" runat="server" Required="true" /></td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">کلمات کلیدی متا</td>
        <td class="control"><kfk:NormalTextBox ID="txtMetaKeyWords" runat="server" TextMode="MultiLine" />
            <br />
            <span style="color: #bd0019">هر کلمه را در یک سطر بنویسید.</span>
        </td>

        <td></td>
    </tr>
    <tr>
        <td class="caption">توضیحات متا</td>
        <td class="control"><kfk:NormalTextBox ID="txtMetaDesc" runat="server" TextMode="MultiLine" /></td>
        <td></td>
    </tr>
   
    <tr>
        <td class="caption">محتوای صفحه</td>
        <td class="control"><kfk:HtmlEditor ID="txtBody" runat="server" Required="true" />
            <a href="/assets/global/images/dyn-page-guide.jpg" target="_blank">
            <i class="fa fa-question-circle" aria-hidden="true" style="font-size:1.5em;color:#abc046"></i>
         
                                                         </a>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="caption">کلمه کلیدی Url </td>
        <td class="control" style=" width:280px"><kfk:NormalTextBox ID="txtUrlKey" runat="server" Required="true" />/www.pgcizi.com/portal</td>
        <td>
            <ul style="color:#bd0019">
                <li>ترجیحا از فاصله (space) استفاده نکنید</li>
                <li>فقط از حروف و اعداد استفاده شود</li>
                <li>ترجیحا از حروف لاتین استفاده نمایید</li>
                <li>برای جدا کردن کلمات از خط تیره (-) استفاده شود</li>
            </ul>
        </td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

