<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_GameOrder_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<%if(kFrameWork.Business.OptionBusiness.GetInt(pgc.Model.Enums.OptionKey.SecondOfRefreshOrderPage)>0){ %>
<asp:Timer runat="server" id="Timer" ontick="Timer_Tick"  ></asp:Timer>
<%} %>
<div class="list-help">
    <div class="commands">
        <a href="<%=GetRouteUrl("admin-orderprint",null)%>" target="_blank" class="hbtn" >چاپ سفارشات</a>
       
    </div>
</div>
<%--<asp:Label runat="server" Text="Page not refreshed yet." id="Label1">
            </asp:Label>--%>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource" onrowdatabound="grdList_RowDataBound"
            ShowFooter="true">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="True" HeaderText="کد" />
        <kfk:TextColumnTemplate DataField="Name" HeaderText="سفارش دهنده" MaxLength="20"/>
        <kfk:NumberColumnTemplate DataField="PayableAmount" HeaderText="مبلغ"  CommaSeparated="true" UnitText="ریال"/>
        <asp:TemplateField HeaderText="پرداخت" >
            <ItemTemplate>
                <img src="/Styles/Images/<%# (bool)Eval("IsPaid")?"Enabled":"Disabled" %>.png" alt="">
            </ItemTemplate>
        </asp:TemplateField>

        <kfk:PersianDateColumnTemplate DataField="OrderPersianDate" HeaderText="تاریخ سفارش" OriginalDataField="OrderDate"  />
        <kfk:BaseBoundField DataField="GameTitle" HeaderText="بازی" />
        <asp:TemplateField ItemStyle-Width="75px" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="مشاهده" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>