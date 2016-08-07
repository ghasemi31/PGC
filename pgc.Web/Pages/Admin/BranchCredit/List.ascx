<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_BranchCredit_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchCreditListExcel.aspx")%>" target="_blank" class="excelbtn" >دریافت فایل اکسل نتایج</a>
        <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchCreditList.aspx")%>" target="_blank" class="printbtn" >چاپ نتایج</a>
        <asp:Button ID="btnBulkDelete" Visible="false" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" Visible="false" runat="server" Text="+ ایجاد سطر جدید" CssClass="dtButton" OnClick="OnAdd" Width="150" />
    </div>
</div>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            ShowFooter="true"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource" onrowdatabound="grdList_RowDataBound">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate Visible="false" />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:TextColumnTemplate DataField="Title" HeaderText="نام شعبه" MaxLength="40"/>
        <kfk:NumberColumnTemplate DataField="CurrentCredit" HeaderText="بستانکار" CommaSeparated="true" UnitText="ریال"/>
        <kfk:NumberColumnTemplate DataField="CurrentDebt" HeaderText="بدهکار" CommaSeparated="true" UnitText="ریال"/>
        <%--<kfk:NumberColumnTemplate DataField="MinimumCredit" HeaderText="سقف حداقل اعتبار" CommaSeparated="true" UnitText="ریال"/>--%>
        <asp:TemplateField HeaderText="سقف حداقل اعتبار" />
        <kfk:EnumColumnTemplate DataField="Status" HeaderText="وضعیت" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.BranchCreditStatus" ViewMode="Image" ImageMode="png" ImagesFolderPath="~/styles/images/" />
        <asp:TemplateField />
        <kfk:BaseButtonField ButtonType="Button" CommandName="ChangeRow" Text="سقف اعتبار" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>