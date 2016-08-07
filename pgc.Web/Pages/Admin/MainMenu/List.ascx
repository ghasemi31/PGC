<%@ Control Language="C#" AutoEventWireup="true" CodeFile="List.ascx.cs" Inherits="Pages_Admin_MainMenu_List" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
<div class="list-help">
    <div class="commands">
        <asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" OnClick="OnBulkDelete" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
        <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد سطر جدید" CssClass="dtButton" OnClick="OnAdd" Width="150" />
    </div>
</div>
<kfk:HKGrid ID="grdList"
            runat="server"
            AllowPaging="True"
            OnRowCommand="Grid_RowCommand"
            DataSourceID="obdSource">
    <PagerSettings Mode="NumericFirstLast" />
    <Columns>
        <kfk:SelectableColumnTemplate />
        <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
        <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
        <kfk:TextColumnTemplate DataField="Title" HeaderText="عنوان"/>
        <kfk:TextColumnTemplate DataField="NavigationUrl" HeaderText="لینک" />     
        
        <%--<asp:TemplateField HeaderText="وضعیت کد Html">
            <ItemTemplate>
                <img src="/Styles/Images/<%#((bool)Eval("IsHtml"))?"Enabled.png":"Disabled.png"%>"/>              
            </ItemTemplate>
        </asp:TemplateField>--%>
        <%--<asp:TemplateField HeaderText="نمایش در صفحه اصلی">
            <ItemTemplate>
                <img src="/Styles/Images/<%# ((bool)Eval("DispInHome"))?"Enabled.png":"Disabled.png"	%>"/>              
            </ItemTemplate>
        </asp:TemplateField>   
         <asp:TemplateField HeaderText="نمایش در سایر صفحات">
            <ItemTemplate>
                <img src="/Styles/Images/<%# ((bool)Eval("DispInOtherPage"))?"Enabled.png":"Disabled.png"	%>"/>              
            </ItemTemplate>
        </asp:TemplateField> 
        <asp:TemplateField HeaderText="باز شدن صفحه در تب جدید">
            <ItemTemplate>
                <img src="/Styles/Images/<%# ((bool)Eval("IsBlank"))?"Enabled.png":"Disabled.png"	%>"/>              
            </ItemTemplate>
        </asp:TemplateField>  --%>
        <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="ویرایش" />
        <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />
    </Columns>
</kfk:HKGrid>
<asp:ObjectDataSource   ID="obdSource"
                        runat="server"
                        EnablePaging="true"
                        ViewStateMode="Disabled">
</asp:ObjectDataSource>