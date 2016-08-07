<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Pages_Admin_Orders_Print" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table
        {
            width:795px;
            direction:rtl;
            font-family: tahoma;
            font-size: 12px;
            border: solid 1px gray;
            margin:auto;
        }
        
        .thead
        {
            -webkit-print-color-adjust:exact;
            background-color: #ededed;
            height: 25px;
            height:30px;
        }
        
        td { height:30px; border-top:1px solid gray;}
        
        .row  { }
        
        .altrow {-webkit-print-color-adjust:exact; background-color:#ededed; }
        
        .ftr
        {
            -webkit-print-color-adjust:exact;
            font-weight:bold;
            background-color: #dedede;
            text-align:center;
        }

        .ftrtxt { }    
            
        .trcode { padding-left:8px; }

        .ordsts { font-weight:bold; }
        
        table { page-break-after: always; margin-top:30px; }            

        body form div table.lsttbl { page-break-after: avoid; }            
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
            
              <%try
                {
                    OrdersPattern pattern = (OrdersPattern)Session["OrderPattern"];
                    OrdersBusiness business = new OrdersBusiness();
                    IQueryable<Order> OrdersList = business.Search_Select(pattern);
                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;
                    
                    foreach (var item in OrdersList)
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.PayableAmount;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
                        {%>
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th style="width:40px;">سفارش</th>
                                    <th>سفارش دهنده</th>
                                    <th>شعبه</th>
                                    <th>مبلغ</th>
                                    <th>وضعیت پرداخت</th>
                                    <th>تاریخ</th>                                
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th style="width:40px;">سفارش</th>
                                    <th>سفارش دهنده(کد اشتراک)</th>
                                    <th>شعبه</th>
                                    <th>مبلغ</th>
                                    <th>وضعیت پرداخت</th>
                                    <th>تاریخ</th>                                
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td style="text-align:center;"><%=TotalNumber%></td>
                            <td class="trcode" style="text-align:center;"><%=item.ID %></td>
                            <td><%=item.User.Fname + " " + item.User.Lname +" ("+item.User_ID+")"%></td>
                            <td><%=item.BranchTitle%></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.PayableAmount)%> ریال</td>
                            <td><%
                                    OrderPaymentStatus opStatus= business.GetPaymentStatus(item.ID);
                                    if (opStatus == OrderPaymentStatus.OnlineSucced)
                                        {%><span class="ordsts"><%=EnumUtil.GetEnumElementPersianTitle(opStatus)%></span><% }
                                    else
                                        {%><%=EnumUtil.GetEnumElementPersianTitle(opStatus)%><% }
                                %>
                            </td>
                            <td><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(item.OrderDate)%></td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == OrdersList.Count())
                        {%>
                                <tr class="ftr">
                                    <td colspan="7">جمع کل: <%=UIUtil.GetCommaSeparatedOf(TotalAmount)%> ریال</td>
                                </tr>
                            </table>
                        <%}
else if (RowNumber == RowPerPage)
{
    RowNumber = 0; %></table><%}    
                    }
                }
                catch (Exception)
                {
                    Response.Redirect(GetRouteUrl("admin-orders"));
                }%>
        </div>
    </form>
</body>
</html>
