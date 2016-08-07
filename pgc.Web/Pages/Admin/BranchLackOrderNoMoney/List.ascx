<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_BranchLackOrderNoMoney_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchLackOrderNoMoneyListExcel.aspx")%>" target="_blank" class="excelbtn" >دریافت فایل اکسل نتایج</a>
        <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchLackOrderNoMoneyList.aspx")%>" target="_blank" class="printbtn" >چاپ نتایج</a>
        <asp:Button ID="btnBulkDelete" runat="server" Visible="false" Text="حذف مرجوعات انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="190" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد کالای جدید" Visible="false" CssClass="dtButton" OnClick="OnAdd" Width="150" />
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
        <kfk:TextColumnTemplate DataField="BranchTitle" HeaderText="نام شعبه" MaxLength="90"/>
        <%--<kfk:NumberColumnTemplate DataField="TotalPrice" UnitText="ریال" HeaderText="مبلغ" CommaSeparated="true" />--%>
        <asp:TemplateField HeaderText="تاریخ کسری"/>
<%--        <kfk:PersianDateColumnTemplate DataField="OrderedPersianDate" HeaderText="تاریخ کسری" />--%>
        <kfk:BaseBoundField DataField="ID" HeaderText="کد کسری" />
        <kfk:EnumColumnTemplate DataField="Status" HeaderText="وضعیت" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.BranchLackOrderStatus" ViewMode="Image" ImageMode="png" ImagesFolderPath="~/styles/images/" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="مشاهده جزئیات" />
        <asp:TemplateField />
        <asp:TemplateField />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
  <asp:ObjectDataSource     ID="obdSource"
                            runat="server"
                            EnablePaging="true"
                            SelectMethod="Search_Select"
                            SelectCountMethod="Search_Count"
                            TypeName="pgc.Business.UserBusiness" >
</asp:ObjectDataSource>