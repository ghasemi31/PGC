<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="BranchSummaryChart.aspx.cs" Inherits="Pages_Admin_Chart_BranchSummaryChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <script src="/assets/global/plugins/Highcharts-4.2.5/js/highcharts.js"></script>--%>
    <%--<script src="/assets/global/plugins/Highstock-4.2.5/js/highstock.js"></script>
    <script src="/assets/global/plugins/Highstock-4.2.5/js/modules/exporting.js"></script>--%>

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
                        <td class="caption chbMinValue"><asp:CheckBox ID="chbMinPrice" Checked="true" runat="server" /> عدم نمایش مقادیر کمتر از:
                            
                            
                        </td>

                        <td class="control txtprice">
                               <kfk:NumericTextBox ID="txtPrice" SupportComma="true" runat="server" UnitText="ریال" />  
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

