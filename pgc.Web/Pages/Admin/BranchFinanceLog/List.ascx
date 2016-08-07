<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_BranchFinanceLog_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchFinanceLogListExcel.aspx")%>" target="_blank" class="excelbtn" >دریافت فایل اکسل نتایج</a>
        <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchFinanceLog.aspx")%>" target="_blank" class="printbtn" >چاپ نتایج</a>
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" Visible="false" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد سطر جدید" Visible="false" CssClass="dtButton" OnClick="OnAdd" Width="150" />
        <%--<asp:Button  Text="بازگشت" Visible="false" CssClass="dtButton" OnClientClick="history.back()" Width="150" />--%>
        <%--<input type="button" value="بازگشت &raquo;" onClick="history.back()" CssClass="dtButton" Visible="false" ID="btnBackToOrderList" runat="server" />--%>
        <%--<asp:Button ID="btnBack" runat="server" Text="بازگشت &raquo;" Visible="false" CssClass="dtButton" OnClick="BackBtn_Click" />--%>

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
        <%--<kfk:BaseBoundField DataField="BranchTitle" HeaderText="نام شعبه" />--%>
        <asp:TemplateField HeaderText="نام شعبه" />
        <kfk:EnumColumnTemplate DataField="ActionType" HeaderText="نوع تغییر" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.BranchFinanceLogActionType" ViewMode="PersianTitle" />
        <kfk:BaseBoundField DataField="UserName" HeaderText="تغییر توسط" />
        <kfk:PersianDateColumnTemplate DataField="PersianDate" OriginalDataField="Date" HeaderText="تاریخ" />
        <kfk:TextColumnTemplate DataField="Description" HeaderText="توضیحات" MaxLength="50" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="جزئیات" Visible="false" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>