<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_SentEmailBlock_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <asp:Button Visible="false" ID="btnBulkDelete" runat="server" Text="حذف موارد انتخاب شده" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سوالهاي انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button Visible="false" ID="btnAdd" runat="server" Text="+ ایجاد سطر جدید" OnClick="OnAdd" Width="150" />
        <div style="float:left;display:inline-block;">
            <asp:Button visible="false" ID="btnBack" runat="server" Text="بازگشت &raquo;" OnClick="btnBack_Click" Width="70" />
        </div>
        <asp:Label visible="false" id="parentInfo" runat="server" style="float:left;margin-left:40px"></asp:Label>
    </div>
</div>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource" onrowdatabound="grdList_RowDataBound">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate Visible="false" />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:TextColumnTemplate DataField="Subject" HeaderText="عنوان" MaxLength="80"/>        
        <kfk:TextColumnTemplate DataField="RecipientMailAddress" HeaderText="دریافت کنندگان" MaxLength="15" />
        <asp:TemplateField HeaderText="تاریخ ارسال" />
        <asp:TemplateField HeaderText="وضعیت ارسال" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="جزئیات" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>