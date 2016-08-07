<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Agent_BranchOrderedTitle_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchOrderedTitleListExcel.aspx")%>" target="_blank" class="excelbtn" >دریافت فایل اکسل نتایج</a>
        <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchOrderedTitleList.aspx")%>" target="_blank" class="printbtn" >چاپ نتایج</a>
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف کالاهای انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Visible="false" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد کالای جدید" CssClass="dtButton" OnClick="OnAdd" Width="150" Visible="false" />
    </div>
</div>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            ShowFooter="true"
            DataSourceID="obdSource" onrowdatabound="grdList_RowDataBound">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate Visible="false" />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:TextColumnTemplate DataField="Title" HeaderText="عنوان" MaxLength="90"/>
        <kfk:TextColumnTemplate DataField="GroupTitle" HeaderText="عنوان گروه" MaxLength="90"/>
        <kfk:NumberColumnTemplate DataField="Quantity" HeaderText="تعداد" />
        <kfk:NumberColumnTemplate DataField="SinglePrice" UnitText="ریال" HeaderText="آخرین مبلغ تعیین شده" CommaSeparated="true" />
        <kfk:NumberColumnTemplate DataField="TotalPrice" UnitText="ریال" HeaderText="مبلغ کل" CommaSeparated="true" />
       <%-- <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="جزئیات" />--%>
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>