<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderList.aspx.cs" Inherits="Pages_Admin_Prints_OrderList" %>

<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لیست ثبت نام کنندگان</title>
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


            <%try
              {
                  GameOrdersPattern pattern = (GameOrdersPattern)Session["GameOrderPattern"];
                  GameOrdersBusiness business = new GameOrdersBusiness();
                  IQueryable<GameOrder> OrdersList = business.Search_SelectPrint(pattern);

                  bool isAltRow = true;


                  if (pattern != null)
                  {
            %>
            <table border="0px" class="lsttbl">
                <tr class="thead" style="text-align: center;">
                    <th colspan="4">نتایج جستجو بر روی فیلد های ذیل در لیست ثبت نام کنندگان بازی</th>
                </tr>
                <tr>
                    <td class="caption" style="border: none">کد ثبت نام : <%=(pattern.Numbers>0)?pattern.Numbers.ToString():"" %></td>
                    <td class="caption" style="border: none">نام ثبت نام کننده : <%=pattern.UserName %></td>
                </tr>
                <tr>
                    <td class="caption" style="border: none">وضعیت پرداخت : <%=EnumUtil.GetEnumElementPersianTitle((GameOrderPaymentStatus)pattern.GameOrderPaymentStatus) %></td>
                    <td class="caption" style="border: none">بازی : <%=pattern.GameTitle %></td>
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
                    <%if (pattern.OrderPersianDate.SearchMode != DateRangePattern.SearchType.Nothing)
                      { %>
                    <td class="caption" style="border: none">تاریخ ثبت نام : 
                        <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.OrderPersianDate.SearchMode)%>
                        <%= pattern.OrderPersianDate.HasDate ? pattern.OrderPersianDate.Date : ""%>
                        <%= pattern.OrderPersianDate.HasFromDate ? pattern.OrderPersianDate.FromDate : ""%>
                        <%= pattern.OrderPersianDate.HasToDate ? "و" + pattern.OrderPersianDate.ToDate : ""%>
                    </td>
                    <%} %>
                </tr>
            </table>
            <%  }
                  else
                  { %>
            <table border="0px" class="lsttbl">
                <tr class="thead" style="text-align: center;">
                    <th>لیست کامل ثبت نامی ها</th>
                </tr>
            </table>
            <%}
            %>


            <table cellpadding="0" cellspacing="0">
                <tr class="thead">
                    <th>کد ثبت نام</th>
                    <th>نام ثبت نام کننده</th>
                    <th>مبلغ</th>
                    <th>وضعیت پرداخت</th>
                    <th>تاریخ ثبت نام</th>
                    <th>بازی</th>
                </tr>
                <%foreach (GameOrder item in OrdersList)
                  {
                      isAltRow = !isAltRow;
                %>
                <tr class="<%=(isAltRow)?"altrow":"row" %>">
                    <td><%=item.ID %></td>
                    <td><%=item.Name %></td>
                    <td><%=item.PayableAmount %></td>
                    <td><%=(item.IsPaid)?"پرداخت شده":"پرداخت نشده" %></td>
                    <td><%=item.OrderPersianDate %></td>
                    <td><%=item.GameTitle%></td>
                </tr>
                <%}%>
            </table>


            <%}

              catch (Exception)
              {
                  Response.Redirect(GetRouteUrl("admin-orders", null));
              }%>
        </div>
    </form>
</body>
</html>
