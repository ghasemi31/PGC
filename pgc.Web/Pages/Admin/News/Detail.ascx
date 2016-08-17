<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_News_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان خبر</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtTitle" runat="server" TextBoxWidth="212" Required="true"/>
        </td>
    </tr>
    <tr>
        <td class="caption">خلاصه خبر</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtSummary" runat="server" TextMode="MultiLine" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">متن خبر</td>
        <td class="control">
            <kfk:HtmlEditor ID="txtBody" runat="server" Required="true" />
              
        </td>
    </tr>
    <tr>
        <td class="caption">عنوان صفحه</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPageTitle" runat="server" TextBoxWidth="212" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">توضیحات صفحه</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPageDescription" runat="server" TextMode="MultiLine" Required="true" />
        </td>
    </tr>
    <tr>
        <td class="caption">کلید واژه های صفحه</td>
        <td class="control">
            <kfk:NormalTextBox ID="txtPageKeywords" runat="server" TextMode="MultiLine" CssClass="xxlarge" Required="true" />
            <br />
            <span style="color: #bd0019">هر کلمه را با کلید Enter از هم جدا کنید</span>
        </td>
    </tr>

    <tr>
        <td class="caption">عکس</td>
        <td class="control">
            <kfk:FileUploader ID="fupNewsPic" runat="server" Required="true" SaveFolder="~/userfiles/News/" />
            <ul style="color: #bd0019">
                <li>فرمت مناسب: png/jpg</li>
                <li>width مناسب: 435 px</li>
                <li>height مناسب: 410 px</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td class="caption">وضعیت نمایش خبر</td>
         <td class="control"><kfk:LookupCombo ID="lkpStatus" runat="server" CssClass="state" EnumParameterType="pgc.Model.Enums.NewsStatus" /></td>

         <td class="caption">تاریخ</td>
        <td class="control">
           <kfk:PersianDatePicker runat="server" ID="pdpDate" Required="true"/>
        </td>
    </tr>
    

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />

    
</div>

