<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Material_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" /></td>
    </tr>

    <tr>
        <td class="caption">تصویر ماده اولیه</td>
        <td class="control"><kfk:FileUploader ID="fupProductPic" runat="server" SaveFolder="~/userfiles/Material/" />
           <ul style="color:#bd0019">
                <li>فرمت مناسب: png/gif</li>
                <li>width مناسب: 365 px</li>
                <li>height مناسب: 365 px</li>                
            </ul>
        </td>
    </tr>
    

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

