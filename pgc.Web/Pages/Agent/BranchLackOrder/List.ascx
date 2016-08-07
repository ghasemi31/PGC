<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Agent_BranchLackOrder_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchLackOrderListExcel.aspx")%>" target="_blank" class="excelbtn" >دریافت فایل اکسل نتایج</a>
        <a href="<%=ResolveUrl("~/Pages/Agent/Prints/BranchLackOrderList.aspx")%>" target="_blank" class="printbtn" >چاپ نتایج</a>
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف مرجوعات انتخاب شده" Visible="false" CssClass="dtButton" OnClick="OnBulkDelete" Width="190" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد کالای جدید" Visible="false" CssClass="dtButton" OnClick="OnAdd" Width="150" />
    </div>
</div>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource" onrowdatabound="grdList_RowDataBound" >
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate Visible="false" />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:NumberColumnTemplate DataField="TotalPrice" UnitText="ریال" HeaderText="مبلغ" CommaSeparated="true" />
        <kfk:PersianDateColumnTemplate DataField="RegPersianDate" OriginalDataField="RegDate" HeaderText="تاریخ درج" />
        <kfk:BaseBoundField DataField="ID" HeaderText="کد کسری" />
        <kfk:TextColumnTemplate DataField="AdminDescription" MaxLength="10" HeaderText="توضیحات مدیر سیستم"  />    
        <kfk:EnumColumnTemplate DataField="Status" HeaderText="وضعیت" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.BranchLackOrderStatus" ViewMode="Image" ImageMode="png" ImagesFolderPath="~/styles/images/" />        
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="مشاهده جزئیات" />
        <asp:TemplateField />        
        <asp:TemplateField />        
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>