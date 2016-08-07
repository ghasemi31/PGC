<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OnlinePaymentList.aspx.cs" Inherits="Pages_Admin_Prints_OnlinePaymentList" %>

<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لیست پرداخت های آنلاین</title>
    <link href="<%=ResolveClientUrl("~/Styles/Shared/PrintPage.css")%>" rel="stylesheet" type="text/css" />
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
                  OnlinePaymentListPattern pattern = (OnlinePaymentListPattern)Session["OnlinePaymentListPattern"];
                  OnlinePaymentListBusiness business = new OnlinePaymentListBusiness();
                  IQueryable<OnlinePaymentList> paymentList = business.Search_Select(pattern);

                  long TotalAmount = 0;
                  bool isAltRow = true;
                  int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                  int RowNumber = 0;
                  int TotalNumber = 0;%>

            <%if (pattern.Order_ID > 0 ||
                          pattern.PersianDate.SearchMode != DateRangePattern.SearchType.Nothing ||
                          pattern.Amount.Type != RangeType.Nothing || (int)pattern.Status>0)
              {%>
            <table border="0px" class="lsttbl">
                <tr class="thead" style="text-align: center;">
                    <th colspan="2">نتایج جستجو بر روی فیلد های ذیل</th>
                </tr>
                <tr>
                    <%if (pattern.Order_ID > 0)
                      { %>
                    <td class="caption" style="border: none">کد سفارش : <%=pattern.Order_ID %></td>
                    <%} %>
                </tr>
                <tr>
                    <%if (pattern.PersianDate.SearchMode != DateRangePattern.SearchType.Nothing)
                      { %>
                    <td class="caption" style="border: none">تاریخ پرداخت : 
                                                        <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.PersianDate.SearchMode)%>
                        <%= pattern.PersianDate.HasDate ? pattern.PersianDate.Date : ""%>
                        <%= pattern.PersianDate.HasFromDate ? pattern.PersianDate.FromDate : ""%>
                        <%= pattern.PersianDate.HasToDate ? " و " + pattern.PersianDate.ToDate : ""%>
                    </td>
                    <%} %>
                </tr>
                <tr>
                    <%if (pattern.Amount.Type != RangeType.Nothing)
                      { %>
                    <td class="caption" style="border: none">مبلغ : 
                                                        <%= EnumUtil.GetEnumElementPersianTitle((RangeType)pattern.Amount.Type)%>
                        <%= pattern.Amount.HasFirstNumber ? UIUtil.GetCommaSeparatedOf( pattern.Amount.FirstNumber ) + " ریال" : ""%>
                        <%= pattern.Amount.HasSecondNumber ? " و " + UIUtil.GetCommaSeparatedOf( pattern.Amount.SecondNumber ) + " ریال" : ""%>
                    </td>
                    <%} %>
                </tr>
                <tr>
                    <%if ((int)pattern.Status > 0)
                      {%>
                    <td class="caption" style="border: none">پرداخت کننده : 
                                                        <%= EnumUtil.GetEnumElementPersianTitle(pattern.Status)%>
                    </td>
                    <%} %>
                </tr>
            </table>
            <%}
              else
              { %>
            <table border="0px" class="lsttbl">
                <tr class="thead" style="text-align: center;">
                    <th>لیست پرداخت های آنلاین</th>
                </tr>
            </table>
            <%} %>
            <table>
                <tr>
                    <td>مبلغ</td>
                    <td>کد سفارش</td>
                    <td>رسید دیجیتالی</td>
                    <td>تاریخ</td>
                    <td>وضعیت</td>
                    <td>پرداخت کننده</td>
                </tr>
                <%foreach (var item in paymentList)
                  {%>
                <tr>
                    <td><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(item.Amount) %> ریال</td>
                    <td><%=!(item.ResNum.Contains("b"))?item.Order_ID.ToString():"--" %></td>
                    <td><%=item.RefNum %></td>
                    <td><%=item.PersianDate %></td>
                    <%
                      //STATE
                      long result = 0;
                      string stateStr = "";
                      OnlineTransactionStatus status = (OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), item.TransactionState.ToString());
                      if (status != OnlineTransactionStatus.OK)
                          stateStr = EnumUtil.GetEnumElementPersianTitle(status);
                      else
                      {
                          result = long.Parse(item.ResultTransaction.ToString());
                          if (result > 0)
                              stateStr = "پرداخت شده";
                          else if (result == 0 || result < 20)
                              stateStr = "توضیحات موجود نیست";
                          else
                              stateStr = pgc.Business.UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + result.ToString().Substring(1)));
                      }
                    %>
                    <td><%=stateStr %></td>
                    <%
                      string payment = "";
                      if (!item.ResNum.ToString().Contains("b"))
                      {
                          payment = "کاربر";
                      }
                      else
                      {
                          payment = paymentBusiness.RetriveBranchName(item.ResNum);
                      }
                    %>
                    <td><%=payment %></td>
                </tr>
                <% } %>
            </table>
            <%}

              catch (Exception)
              {
                  Response.Redirect(GetRouteUrl("admin-onlinepaymentlist", null));
              }%>
        </div>
    </form>
</body>
</html>
