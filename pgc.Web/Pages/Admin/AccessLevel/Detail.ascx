<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_AccessLevel_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption" colspan="1">نقش</td>
        <td class="control"><kfk:LookupCombo ID="lkcRole" 
                                            runat="server" 
                                            EnumParameterType="pgc.Model.Enums.Role"
                                            EnumParameterTypeAssembly="pgc.Model"
                                            Required="true" 
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="Role_Changed"/></td>
    </tr>
    <tr>
        <td class="caption" colspan="1">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true"/></td>
    </tr>
    <tr>
        <td class="caption" colspan="1">مجوز های دسترسی</td>
        <td class="control" colspan="3" style="width:85%"><asp:CheckBoxList ID="chlPermissions" RepeatColumns="3" runat="server"></asp:CheckBoxList></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

