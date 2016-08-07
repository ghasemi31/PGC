<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="ProductSoldChart.aspx.cs" Inherits="Pages_Admin_Chart_ProductSoldChart" %>

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
                <legend>نمودار روند فروش کل</legend>

                <table class="Table">
                    <tr>
                        <td class="caption">تاریخ</td>
                        <td class="control">
                            <kfk:PersianDateRange ID="pdrDate" runat="server" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>


                </table>

                <div class="commands">
                     <asp:Button runat="server" ID="btnSoldAll" Text="تولید نمودار" OnClick="btnSoldAll_Click"/>          
                </div>

            </fieldset>
            <fieldset>
                <legend>نمودار روند فروش شعب</legend>
                <table class="Table">
                    <tr>
                        <td class="caption">تاریخ</td>
                        <td class="control">
                            <kfk:PersianDateRange ID="pdrBranchDate" runat="server" />
                        </td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr>
                        <td class="caption">شعبه</td>
                        <td class="control">
                            <kfk:LookupCombo ID="lkpBranch" runat="server" BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness" AddDefaultItem="true" />
                        </td>
                    </tr>
                </table>
                <div class="commands">
                    <asp:Button runat="server" ID="btnBranchSold" Text="تولید نمودار" OnClick="btnBranchSold_Click" />
                </div>
            </fieldset>
            <fieldset>
                <legend>نمودار روند فروش کالا</legend>
                <table class="Table">
                    <tr>
                        <td class="caption">تاریخ</td>
                        <td class="control">
                            <kfk:PersianDateRange ID="pdrProductCase" runat="server" />
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

                </table>
                <div class="commands">
                    <asp:Button runat="server" ID="btnProductSold" Text="تولید نمودار" OnClick="btnProductSold_Click" />
                </div>
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

