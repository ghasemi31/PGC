<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Agent_BranchTransaction_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchTransactionListExcel.aspx")%>" target="_blank" class="excelbtn" >دریافت فایل اکسل نتایج</a>
        <a href="<%=ResolveUrl("~/Pages/Agent/Prints/BranchTransactionList.aspx")%>" target="_blank" class="printbtn" >چاپ نتایج</a>
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" Visible="false" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد سطر جدید" Visible="false" CssClass="dtButton" OnClick="OnAdd" Width="150" />                                          
    </div>
</div>
<kfk:HKGrid ID="grdList"                                                                                                                                                             
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource" 
            ShowFooter="true"
            onrowdatabound="grdList_RowDataBound">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate Visible="false" />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:EnumColumnTemplate DataField="TransactionType" HeaderText="نوع تراکنش" ViewMode="PersianTitle" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.BranchTransactionType" />
        <kfk:NumberColumnTemplate DataField="BranchCredit" HeaderText="بستانکار" CommaSeparated="true" UnitText="ریال" />
        <kfk:NumberColumnTemplate DataField="BranchDebt" HeaderText="بدهکار" CommaSeparated="true" UnitText="ریال" />
        <kfk:PersianDateColumnTemplate DataField="RegPersianDate" OriginalDataField="RegDate" HeaderText="تاریخ" />
        <kfk:TextColumnTemplate DataField="Description" HeaderText="توضیحات" MaxLength="20"/>
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="جزئیات" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>