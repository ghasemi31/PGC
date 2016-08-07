<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_User_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد سطر جدید" CssClass="dtButton" OnClick="OnAdd" Width="150" />
    </div>
</div>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:TextColumnTemplate DataField="FUllName" HeaderText="نام و نام خانوادگی" />
        <kfk:TextColumnTemplate DataField="Email" HeaderText="پست الکترونیک" />
<%--        <kfk:TextColumnTemplate DataField="City" HeaderText="شهرستان" MaxLength="30"/>--%>
        <kfk:EnumColumnTemplate DataField="Role" HeaderText="نقش" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.Role" ViewMode="PersianTitle"/>
        <kfk:TextColumnTemplate DataField="AccessLevel" HeaderText="سطح دسترسی" MaxLength="20"/>
        <kfk:TextColumnTemplate DataField="SignUpPersianDate" HeaderText="تاریخ عضویت" />
        <kfk:EnumColumnTemplate DataField="ActivityStatus" HeaderText="وضعیت" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.UserActivityStatus" ViewMode="Image" ImageMode="png" ImagesFolderPath="~/Styles/Images/" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="ویرایش" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>