<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_BranchPayment_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchPaymentList.aspx")%>" target="_blank" class="printbtn" >چاپ نتایج</a>
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف پرداخت های انتخاب شده" Visible="false" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" runat="server" Text="+برای شعبه ایجاد پرداخت جدید" CssClass="dtButton" OnClick="OnAdd" Width="150" Visible="false" />
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
        <asp:TemplateField HeaderText="نام شعبه" />
        <kfk:NumberColumnTemplate DataField="Amount" HeaderText="مبلغ" CommaSeparated="true" UnitText="ریال" />
        <%--<kfk:PersianDateColumnTemplate DataField="PersianDate" OriginalDataField="Date" HeaderText="تاریخ پرداخت" />--%>
        <asp:TemplateField HeaderText="تاریخ پرداخت" />
        <asp:TemplateField HeaderText="وضعیت" />
        <kfk:EnumColumnTemplate DataField="Type" HeaderText="پرداخت" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.BranchPaymentType" ViewMode="Image" ImageMode="png" ImagesFolderPath="~/styles/images/" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="ConfirmRow" Text="تایید پرداخت" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="UnconfirmRow" Text="لغو پرداخت" />        
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="مشاهده جزئیات" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />
    </Columns>                                                                  
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>