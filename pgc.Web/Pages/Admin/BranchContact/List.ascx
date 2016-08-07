<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_BranchContact_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <%--<asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />--%>
    </div>
</div>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate Visible="false"/>
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:TextColumnTemplate DataField="FullName" HeaderText="نام" MaxLength="20"/>
        <asp:TemplateField HeaderText="وضعیت خوانده شده">
            <ItemTemplate>
                <img src="/Styles/Images/<%# ((bool)Eval("IsRead"))?"Enabled.png":"Disabled.png"	%>"/>              
            </ItemTemplate>
        </asp:TemplateField>
        <kfk:PersianDateColumnTemplate DataField="PersianDate" HeaderText="تاریخ"/>
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="مشاهده" />
        <%--<kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />--%>
    </Columns>
</kfk:HKGrid>

<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>