<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Lottery_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">وضعیت</td>
        <td class="control"><kfk:LookupCombo ID="lkcStatus" 
                                    runat="server" 
                                    EnumParameterType="pgc.Model.Enums.LotteryStatus"
                                    Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">توضیحات</td>
        <td class="control"><kfk:NormalTextBox ID="txtDesc" runat="server" TextMode="MultiLine"/></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

