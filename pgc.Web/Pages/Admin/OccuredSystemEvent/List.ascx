<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_OccuredSystemEvent_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <asp:Button Visible="false" ID="btnBulkDelete" runat="server" Text="حذف رخدادهای انتخاب شده" OnClick="OnBulkDelete" Width="230" OnClientClick="if (!confirm('آیا در حذف کلیه خبرهاي انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button Visible="false" ID="btnAdd" runat="server" Text="+ " OnClick="OnAdd" Width="150" />
    </div>
</div>
<kfk:HKGrid ID="grdList"
    runat="server"
    AllowPaging="True"
    OnRowCommand="Grid_RowCommand"
    DataSourceID="obdSource"
    OnRowDataBound="grdList_RowDataBound">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate Visible="false" />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <asp:TemplateField HeaderText="عنوان رخداد"/>
       <%-- <asp:TemplateField HeaderText="کاربر/شعبه" />--%>
         <kfk:TextColumnTemplate DataField="Actor" HeaderText="کاربر/شعبه" MaxLength="13" />
        <asp:TemplateField HeaderText="تاریخ اجرا" />
        <kfk:TextColumnTemplate DataField="Description" HeaderText="خلاصه اقدامات" MaxLength="19" />
        <asp:TemplateField HeaderText="نوع دستگاه" />
        <asp:TemplateField HeaderText="اقدامات ایمیلی" />
        <asp:TemplateField HeaderText="اقدامات پیامکی" />

        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="جزئیات" Visible="false" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource ID="obdSource"
    runat="server"
    EnablePaging="true"
    ViewStateMode="Disabled"></asp:ObjectDataSource>
