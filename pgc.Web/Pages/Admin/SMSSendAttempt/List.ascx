<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_SMSSendAttempt_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <asp:Button Visible="false" ID="btnBulkDelete" runat="server" Text="حذف پیامک های انتخاب شده" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button Visible="false" ID="btnAdd" runat="server" Text="+ " OnClick="OnAdd" Width="150" />
        <div style="float:left;display:inline-block;" >
            <asp:Button visible="false" ID="btnEvent" runat="server" Text="بازگشت &raquo;" OnClick="btnEvent_Click" Width="70" />
        </div>
        <asp:Label visible="false" id="parentInfo" runat="server" style="float:left;margin-left:40px"></asp:Label>
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
        
        <kfk:EnumColumnTemplate DataField="EventType" HeaderText="نوع رخداد" Enum_dllName="pgc.Model" EnumPath="pgc.Model.Enums.EventType" />
        <asp:TemplateField HeaderText="عنوان رخداد"/>
        <kfk:TextColumnTemplate DataField="Body" HeaderText="متن پیامک" MaxLength="20"/>
        <kfk:TextColumnTemplate DataField="Total_SucceedCount" HeaderText="تعداد ارسالی" />
        <kfk:TextColumnTemplate DataField="Total_SumCount" HeaderText="تعداد کل" />
        <asp:TemplateField HeaderText="تاریخ ارسال" />
        
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="جزئیات" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" Visible="false" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>