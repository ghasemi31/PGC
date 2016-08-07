<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Agent_OnlinePayment_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<%if(kFrameWork.Business.OptionBusiness.GetInt(pgc.Model.Enums.OptionKey.SecondOfRefreshOrderPage)>0){ %>
    <asp:Timer runat="server" id="Timer" ontick="Timer_Tick"  ></asp:Timer>
<%} %>
<div class="list-help">
    <div class="commands">
        <asp:Button Visible="false" ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button Visible="false" ID="btnAdd" runat="server" Text="+ ایجاد سطر جدید" CssClass="dtButton" OnClick="OnAdd" Width="150" />
        <div style="float:left;display:inline-block;" >
            <asp:Button visible="false" ID="btnEvent" runat="server" Text="بازگشت &raquo;" OnClick="btnEvent_Click" Width="70" />
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
        <asp:TemplateField HeaderText="مبلغ" />
        <asp:TemplateField HeaderText="کد سفارش" />
        <asp:TemplateField HeaderText="رسید دیجیتالی" />
        <asp:TemplateField HeaderText="تاریخ تراکنش" />
        <asp:TemplateField HeaderText="وضعیت تراکنش" />
        <%--<asp:TemplateField HeaderText="نتیجه تایید تراکنش" />--%>
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="ویرایش" Visible="false" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>