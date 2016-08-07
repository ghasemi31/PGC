<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchesOrderReport.aspx.cs" Inherits="Pages_Admin_Prints_BranchesOrderReport" %>

<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>گزارش سفارشات شعب</title>
    <link href="<%=ResolveClientUrl("~/Styles/Shared/PrintPage.css")%>" rel="stylesheet" type="text/css" />
    <link href="/assets/css/BranchesOrderReport/Default.css?v=2" rel="stylesheet" />
    <style type="text/css">
        td {
            border-top: 1px solid gray !important;
        }
    </style>
    <script src="<%=ResolveClientUrl("~/scripts/shared/jquery-1.7.2.min.js")%>" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(new function () {
            self.print();
        });
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>

            <table border="0px" class="lsttbl">
                <tr>
                    <td style="border: none"><%=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutTitleHtml) %></td>
                </tr>
            </table>
            <%try
              {
                  BranchesOrderReportPattern pattern = (BranchesOrderReportPattern)Session["BranchesOrderReportPattern"];
                  pgc.Business.General.BranchesOrderReportBusiness business = new pgc.Business.General.BranchesOrderReportBusiness();
                  IQueryable<BranchOrderDetail> order = business.RetriveOrder(pattern);

                  long TotalAmount = 0;
                  bool isAltRow = true;
                  int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                  int RowNumber = 0;
                  int TotalNumber = 0;%>

            <%if (pattern.Branch_ID > 0 ||
                          pattern.PersianDate.SearchMode != DateRangePattern.SearchType.Nothing ||
                          !string.IsNullOrEmpty(pattern.Title))
              {%>
            <table border="0px" class="lsttbl">
                <tr class="thead" style="text-align: center;">
                    <th colspan="2">نتایج جستجو بر روی فیلد های ذیل</th>
                </tr>
                <tr>
                    <%if (pattern.Branch_ID > 0)
                      { %>
                    <td class="caption" style="border: none">شعبه : <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title %></td>
                    <%} %>
                </tr>
                <tr>


                    <%if (!string.IsNullOrEmpty(pattern.Title))
                      { %>
                    <td class="caption" style="border: none">عبارت : <%=pattern.Title %></td>
                    <%} %>
                    <tr>
                    </tr>


                    <%if (pattern.PersianDate.SearchMode != DateRangePattern.SearchType.Nothing)
                      { %>
                    <td class="caption" style="border: none">تاریخ سفارش : 
                                                    <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.PersianDate.SearchMode) %>
                        <%= pattern.PersianDate.HasDate? pattern.PersianDate.Date:"" %>
                        <%= pattern.PersianDate.HasFromDate? pattern.PersianDate.FromDate:"" %>
                        <%= pattern.PersianDate.HasToDate? "و" + pattern.PersianDate.ToDate:"" %>
                    </td>
                    <%} %>
                </tr>
            </table>
            <%}
              else
              { %>
            <table border="0px" class="lsttbl">
                <tr class="thead" style="text-align: center;">
                    <th>لیست کامل گزارش ها</th>
                </tr>
            </table>
            <%} %>
            <%if (order.Count() == 0)
              {%>
            <div id="empty-list">
                <span>هیچ سطری برای نمایش وجود ندارد.</span>
            </div>
            <%}
              else
              {%>

            <%//List<string> headerList = order.Select(m => m.BranchOrderTitle_Title).Distinct().ToList();
                  var headerList = order.Select(m => new { m.BranchOrderTitle_Title, m.BranchOrderTitle.BranchOrderTitleGroup.DisplayOrder, orderTitle = m.BranchOrderTitle.DisplayOrder }).Distinct().OrderBy(m => m.DisplayOrder).ThenBy(m => m.orderTitle);
                  List<Branch> branchList = order.Select(b => b.BranchOrder.Branch).Distinct().ToList();
            %>


            <br />
            <br />


            <div>
                <table class="Table table-print" cellpadding="0" cellspacing="0">
                    <tr class="Header">
                        <td>محصولات مستردیزی</td>
                        <%foreach (var item in branchList)
                          {%>
                        <td class="rotate" style="height: 280px !important">
                            <div><span><%=item.Title %></span></div>
                        </td>
                        <%} %>
                        <td class="rotate" style="height: 280px !important">
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
                        <%foreach (var branchItem in branchList)
                          {
                              long count = 0;
                              foreach (var itemCount in query.Where(q => q.BranchOrder.Branch_ID == branchItem.ID))
                              {
                                  count += (itemCount.Quantity != null) ? itemCount.Quantity : 0;
                              }
                        %>
                        <td><%=((count>0))?kFrameWork.Util.UIUtil.GetCommaSeparatedOf(count):"-"  %></td>
                        <% } %>
                        <td style="font-weight: bold"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(query.Sum(s=>s.Quantity)) %></td>
                    </tr>
                    <%} %>
                    <%--<tr>
                        <td>جمع</td>
                        <%foreach (var branchItem in branchList)
                          {%>
                        <td style="padding-left: 8px; font-weight: bold"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(order.Where(o=>o.BranchOrder.Branch_ID==branchItem.ID).Sum(q=>q.Quantity)) %></td>
                        <% } %>
                        <td style="padding-left: 8px; font-weight: bold"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(order.ToList().Sum(q=>q.Quantity)) %></td>
                    </tr>--%>
                </table>
            </div>
            <%}
            %>
            <% }
              catch (Exception)
              {
                  Response.Redirect(GetRouteUrl("admin-branchesorderreport", null));
              }%>
        </div>
    </form>
</body>
</html>
