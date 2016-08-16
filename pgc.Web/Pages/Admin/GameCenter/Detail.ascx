<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_GameCenter_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
   
    <tr runat="server" >
        <td class="caption">عنوان</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtTitle"  Required="true"/>
        </td>
        <td class="caption">توضیحات</td>
        <td class="control">
            <kfk:NormalTextBox runat="server" ID="txtDesc"  Required="true" TextMode="MultiLine" />
        </td>
      

    </tr>
    <tr>
        <td class="caption">استان</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcProvince"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness"
                AutoPostBack="true"
                DependantControl="lkcCity"
                Required="true" />
        </td>
        <td class="caption">شهرستان</td>
        <td class="control">
            <kfk:LookupCombo ID="lkcCity"
                runat="server"
                BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness"
                DependOnParameterName="Province_ID"
                DependOnParameterType="Int64"
                Required="true" />
        </td>
    </tr>
   
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

