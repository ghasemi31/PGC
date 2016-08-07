<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_LotteryWiner_Winer" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">قرعه کشی</td>
        <td class="control"><kfk:LookupCombo ID="lkpLottery" 
                                            BusinessTypeName="pgc.Business.Lookup.LotteryLookupBusiness"
                                            SelectMethod="GetCompateLotteryList"
                                            runat="server" 
                                            Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">نام</td>
        <td class="control" ><kfk:NormalTextBox ID="txtFName" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">نام خانوادگی</td>
        <td class="control" ><kfk:NormalTextBox ID="txtLName" runat="server" Required="true"/></td>
    </tr>
    <tr>
        <td class="caption">رتبه</td>
        <td class="control" ><kfk:NumericTextBox ID="txtRank" runat="server" Required="true"/></td>
    </tr>
        <tr>
        <td class="caption">توضیحات</td>
        <td class="control" ><kfk:NormalTextBox ID="txtDesc" runat="server" TextMode="MultiLine"/></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

