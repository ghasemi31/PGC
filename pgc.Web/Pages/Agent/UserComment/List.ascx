<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Agent_UserComment_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
       <!-- <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد سطر جدید" CssClass="dtButton" OnClick="OnAdd" Width="150" /> -->
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
        <kfk:TextColumnTemplate DataField="Name" HeaderText="نام" MaxLength="20"/>
        <kfk:TextColumnTemplate DataField="Topic" HeaderText="موضوع" MaxLength="50"/>
        <kfk:EnumColumnTemplate DataField="Status" Enum_dllName="pgc.model" EnumPath="pgc.Model.Enums.UserCommentStatus" HeaderText="وضعیت"/>
        <kfk:PersianDateColumnTemplate DataField="UCPersianDate" HeaderText="تاریخ"/>
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="مشاهده" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />
    </Columns>
</kfk:HKGrid>

<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>