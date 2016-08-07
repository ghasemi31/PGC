<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_BranchesOrderReportKitchen_Default" %>
<%@ Import Namespace="pgc.Model" %>
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
                <legend>جستجو</legend>
                <table>
                    <tr>
                        <td class="caption">عنوان</td>
                        <td class="control">
                            <kfk:NormalTextBox ID="txtTitle" runat="server" />
                        </td>

                        <td class="caption">شعبه</td>
                        <td class="control">
                            <kfk:LookupCombo ID="lkpBranch" runat="server" BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness" AddDefaultItem="true" />
                        </td>

                    </tr>
                    <tr>

                        <td class="caption">تاریخ ثبت سفارش</td>
                        <td class="control">
                            <kfk:PersianDateRange ID="pdrDeliverDate" runat="server" />
                        </td>

                        <td class="caption">وضعیت درخواست</td>
                        <td class="control">
                            <kfk:LookupCombo ID="lkpOrderStatus" runat="server" EnumParameterType="pgc.Model.Enums.BranchOrderStatus" AddDefaultItem="true" />
                        </td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <div class="commands">
                    <asp:Button runat="server" ID="btnSearch" Text="جستجو" CssClass="" OnClick="btnSearch_Click" />
                    <asp:Button runat="server" ID="btnShowAll" Text="نمایش سفارش امروز" CssClass="" OnClick="btnShowAll_Click" />
                </div>
            </fieldset>

            <fieldset id="list">

                <legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.Title %></legend>
                <%if (order.Count() == 0)
                  {%>
                <div id="empty-list">
                    <span>هیچ سطری برای نمایش وجود ندارد.</span>
                </div>
                <%}
                  else
                  {%>

                <%var headerList = order.Select(m => new { m.BranchOrderTitle_Title, m.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder, orderTitle = m.BranchOrderTitle.DisplayOrder }).Distinct().OrderBy(m => m.DisplayOrder).ThenBy(m => m.orderTitle);
                  List<Branch> branchList = order.Select(b => b.BranchOrder.Branch).Distinct().ToList();
                %>


                <div style="padding: 0 0 20px 20px; float: left">
                    <a href="<%=ResolveUrl("~/Pages/Admin/ExcelReport/BranchesOrderReportKitchenExcel.aspx")%>" target="_blank" class="excelbtn">دریافت فایل اکسل نتایج</a>
                    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchesOrderReportKitchen.aspx")%>" target="_blank" class="printbtn ocommands">چاپ جزئیات</a>
                </div>
                <br />
                <br />


                <div id="table-wrapper">
                    <table class="Table" cellpadding="0" cellspacing="0">
                        <tr class="Header">
                            <td>محصولات مستردیزی</td>
                           <%-- <%foreach (var item in branchList)
                              {%>
                            <td class="rotate">
                                <div><span><%=item.Title %></span></div>
                            </td>
                            <%} %>--%>
                            <td class="rotate">
                                <div><span>جمع کل</span></div>
                            </td>
                        </tr>

                        <%foreach (var item in headerList)
                          {%>
                        <tr id="order-row">
                            <td style="font-weight: bold"><%=item.BranchOrderTitle_Title %></td>

                            <%
                              var query = order.Where(o => o.BranchOrderTitle_Title == item.BranchOrderTitle_Title).ToList();
                            %>
                          <%--  <%foreach (var branchItem in branchList)
                              {
                                  long count = 0;
                                  foreach (var itemCount in query.Where(q => q.BranchOrder.Branch_ID == branchItem.ID))
                                  {
                                      count += (itemCount.Quantity != null) ? itemCount.Quantity : 0;
                                  }
                            %>
                            <td><%=(count>0)?kFrameWork.Util.UIUtil.GetCommaSeparatedOf(count):"-"  %></td>
                            <% } %>--%>
                            <td style="font-weight: bold"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(query.Sum(s=>s.Quantity)) %></td>
                        </tr>
                        <%} %>
                       <%-- <tr>
                            <td>جمع</td>
                            <%foreach (var branchItem in branchList)
                              {%>
                            <td style="padding-left: 8px;"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(order.Where(o=>o.BranchOrder.Branch_ID==branchItem.ID).Sum(q=>q.Quantity)) %></td>
                            <% } %>
                            <td style="padding-left: 8px;"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(order.ToList().Sum(q=>q.Quantity)) %></td>
                        </tr>--%>
                    </table>
                </div>
                <%}
                %>
            </fieldset>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

