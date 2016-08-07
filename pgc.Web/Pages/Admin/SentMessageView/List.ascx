<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_SentMessage_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="commands">
    <asp:Button ID="btnBulkDelete" Visible="false" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
    <div style="float:left;display:inline-block;" >
        <asp:Button visible="false" ID="btnBack" runat="server" Text="بازگشت &raquo;" OnClick="btnBack_Click" Width="70" />
    </div>
    <asp:Label visible="false" id="parentInfo" runat="server" style="float:left;margin-left:40px"></asp:Label>
</div>
<div class="list-help">
    <div class="itemRight">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Styles/images/unknown.png" Width="20" Height="20" AlternateText=" در حال ارسال "/>
        <span>در حال ارسال</span>
    </div>
    <div class="itemRight">
        <asp:Image ID="Image2" runat="server" ImageUrl="~/Styles/images/DeliveredToPhone.png" Width="20" Height="20" AlternateText=" رسیده به گوشی "/>
        <span>رسیده به گوشی</span>
    </div>
    <div class="itemRight">
        <asp:Image ID="Image3" runat="server" ImageUrl="~/Styles/images/NotDeliveredToPhone.png" Width="20" Height="20" AlternateText=" نرسیده به گوشی "/>
        <span>نرسیده به گوشی</span>
    </div>
    <div class="itemRight">
        <asp:Image ID="Image4" runat="server" ImageUrl="~/Styles/images/DeliveredToTelecommunication.png" Width="20" Height="20" AlternateText=" رسیده به مخابرات "/>
        <span>رسیده به مخابرات</span>
    </div>
    <div class="itemRight">
        <asp:Image ID="Image5" runat="server" ImageUrl="~/Styles/images/NotDeliveredToTelecommunication.png" Width="20" Height="20" AlternateText=" نرسیده به مخابرات "/>
        <span>نرسیده به مخابرات</span>
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
        <kfk:PersianDateColumnTemplate DataField="PersianDate" HeaderText="تاریخ ارسال" OriginalDataField="Date"  />
        <kfk:TextColumnTemplate DataField="RecipientNumber" HeaderText="گیرنده" MaxLength="20"/>
        <kfk:EnumColumnTemplate DataField="SendStatus" HeaderText="وضعیت" Enum_dllName="pgc.Model" ViewMode="Image" EnumPath="pgc.Model.Enums.SendSMSStatus" ImageMode="png" ImagesFolderPath="~/Styles/Images/" />
        <asp:TemplateField />
        <kfk:TextColumnTemplate DataField="Body" HeaderText="شرح" MaxLength="50"/>
        <asp:TemplateField HeaderText="تاریخ ارسال" />
        <kfk:TextColumnTemplate DataField="SMSCount" HeaderText="تعداد صفحه" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="جزئیات" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>