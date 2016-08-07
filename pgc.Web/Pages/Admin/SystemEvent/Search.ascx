<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="Pages_Admin_SystemEvent_Search" %>
<legend>جستجو</legend>
<table>
   <tr>
        <td class="caption">عنوان</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server"  CssClass="long" /></td>        
    </tr>
    <%--<tr>
        <td class="caption">درج ایمیل</td>
        <td class="caption">درج تلفن همراه</td>
    </tr>
    <tr>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Manual_Email" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Manual_SMS" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
    </tr>
    <tr>
        <td class="caption">ارسال ایمیل به تمامی مدیران</td>
        <td class="caption">ارسال پیامک به تمامی مدیران</td>
    </tr>
    <tr>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Every_Admin_Email" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Every_Admin_SMS" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
    </tr>
    <tr>
        <td class="caption">ارسال ایمیل به تمامی کاربران</td>
        <td class="caption">ارسال پیامک به تمامی کاربران</td>
    </tr>
    <tr>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Every_User_Email" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Every_User_SMS" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
    </tr>
    <tr>
        <td class="caption">ارسال ایمیل به مهمان مربوطه</td>
        <td class="caption">ارسال پیامک به مهمان مربوطه</td>
    </tr>
    <tr>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Related_Guest_Email" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Related_Guest_SMS" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
    </tr>
    <tr>
        <td class="caption">ارسال ایمیل به کاربر مربوطه</td>
        <td class="caption">ارسال پیامک به کاربر مربوطه</td>
    </tr>
    <tr>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Related_User_Email" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Related_User_SMS" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
    </tr>
    <tr>
        <td class="caption">ارسال ایمیل به مرکز تصویربرداری مربوطه</td>
        <td class="caption">ارسال پیامک به مرکز تصویربرداری مربوطه</td>
    </tr>
    <tr>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Related_ImagingCenter_Email" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Related_ImagingCenter_SMS" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
    </tr>
    <tr>
        <td class="caption">ارسال ایمیل به واحد مدیریتی مربوطه</td>
        <td class="caption">ارسال پیامک به واحد مدیریتی مربوطه</td>
    </tr>
    <tr>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Related_Department_Email" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
        <td class="control"><kfk:LookupCombo DefaultItemValue="-1" AddDefaultItem="true" ID="lkpSupport_Related_Department_SMS" runat="server" EnumParameterType="pgc.Model.Enums.BooleanStatus" /></td>
    </tr>--%>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSearch" Text="جستجوي رخداد" CssClass="" OnClick="OnSearch" />
    <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه رخدادها" CssClass="" OnClick="OnSearchAll" />
</div>
