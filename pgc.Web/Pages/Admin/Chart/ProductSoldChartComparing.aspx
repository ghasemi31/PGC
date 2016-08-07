<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="ProductSoldChartComparing.aspx.cs" Inherits="Pages_Admin_Chart_ProductSoldChartComparing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
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
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td class="caption">گروه کالا</td>
                        <td class="control">
                            <kfk:LookupCombo ID="lkpProductGroup"
                                runat="server"
                                BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleGroupLookupBusiness"
                                AutoPostBack="true"
                                DependantControl="lkpProduct" />
                        </td>
                    </tr>
                    <tr>
                        <td class="caption">عنوان کالا</td>
                        <td class="control">
                            <kfk:LookupCombo ID="lkpProduct"
                                runat="server"
                                AddDefaultItem="true"
                                BusinessTypeName="pgc.Business.Lookup.BranchOrderTitleLookupBusiness"
                                DependOnParameterName="Group_ID"
                                DependOnParameterType="Int64"
                                Required="true" />
                        </td>

                    </tr>
                    <tr>
                        <td colspan="4" class="commands">
                            <asp:Button ID="btnSearch" CssClass="large" runat="server" Text="تولید نمودار"  CausesValidation="true" />
                        </td>
                    </tr>
                </table>
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

