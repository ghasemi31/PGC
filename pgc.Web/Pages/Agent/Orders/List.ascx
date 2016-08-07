<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Agent_Order_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<%if(kFrameWork.Business.OptionBusiness.GetInt(pgc.Model.Enums.OptionKey.SecondOfRefreshOrderPage)>0){ %>
<asp:Timer runat="server" id="Timer"  ontick="Timer_Tick"  ></asp:Timer>
<%} %>
<div class="list-help">
    <div class="commands">
        <a href="<%=GetRouteUrl("agent-orderprint",null)%>" target="_blank" class="hbtn" >چاپ سفارشات</a>
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
    </div>
</div>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource" 
            onrowdatabound="grdList_RowDataBound1"
            ShowFooter="true">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="True" HeaderText="کد" />
        <kfk:TextColumnTemplate DataField="FullName" HeaderText="سفارش دهنده" MaxLength="20"/>
        <kfk:NumberColumnTemplate DataField="PayableAmount" HeaderText="مبلغ"  CommaSeparated="true" UnitText="ریال"/>
        <asp:TemplateField HeaderText="پرداخت"  />
        <kfk:PersianDateColumnTemplate DataField="OrderPersianDate" HeaderText="تاریخ سفارش" OriginalDataField="OrderDate"  />
        <asp:TemplateField ItemStyle-Width="75px"/>              
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="مشاهده" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />        
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>