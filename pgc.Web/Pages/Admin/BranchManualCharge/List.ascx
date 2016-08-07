<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_BranchManualCharge_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <%--<asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />--%>
        <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchManualCharge.aspx")%>" target="_blank" class="excelbtn" >دریافت فایل اکسل نتایج</a>
        <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchManualCharge.aspx")%>" target="_blank" class="printbtn" >چاپ نتایج</a>
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
        <%--<kfk:SelectableColumnTemplate />--%>
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:TextColumnTemplate DataField="Branch" HeaderText="شعبه" MaxLength="50"/>
        <kfk:NumberColumnTemplate DataField="BranchCredit" HeaderText="بستانکار" CommaSeparated="true" UnitText="ریال" />
        <kfk:NumberColumnTemplate DataField="BranchDebt" HeaderText="بدهکار" CommaSeparated="true" UnitText="ریال" />
        <kfk:TextColumnTemplate DataField="Description" HeaderText="توضیحات"/>
        <kfk:PersianDateColumnTemplate DataField="RegPersianDate" HeaderText="تاریخ" OriginalDataField="RegDate" />
        <%--<kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="ویرایش" />--%>
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>