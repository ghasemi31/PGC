<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvCollection.ascx.cs" Inherits="UserControls_Project_AdvCollection" %>
<%@ Register src="AdvViewer.ascx" tagname="AdvViewer" tagprefix="uc1" %>

<asp:ObjectDataSource   ID="odsAdv" 
                        runat="server" 
                        EnablePaging="False"
                        SelectMethod="Adv_List"
                        TypeName="pgc.Business.General.AdvBusiness"
                        EnableViewState="false" 
                        onselecting="odsAdv_Selecting">
</asp:ObjectDataSource>
                
<asp:ListView ID="lsvAdv" runat="server" DataSourceID="odsAdv" DataKeyNames="ID"
        EnableViewState="false" onitemdatabound="lsvAdv_ItemDataBound"  >
    <ItemTemplate>
        <uc1:AdvViewer ID="viewer" runat="server" />
    </ItemTemplate>
</asp:ListView>
            
