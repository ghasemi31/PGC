<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="SoldCostPieChart.aspx.cs" Inherits="Pages_Admin_Chart_SoldCostPieChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">
    <asp:ScriptManager runat="server" ID="scmOrder">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <kfk:Loading runat="server" ID="Loading" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <fieldset id="search">
                <legend><%=this.Entity.Title %></legend>
                <table class="Table">
                    <tr>
                        <td colspan="1" class="caption">تاریخ </td>
                        <td colspan="3" class="control">
                            <kfk:PersianDateRange ID="pdrDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="commands">
                            <asp:Button ID="btnSearch" CssClass="large" runat="server" Text="تولید نمودار" OnClick="btnSearch_Click" CausesValidation="true" />
                        </td>
                    </tr>
                </table>
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

