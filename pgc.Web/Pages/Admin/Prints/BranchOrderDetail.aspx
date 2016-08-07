<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchOrderDetail.aspx.cs" Inherits="Pages_Admin_Prints_BranchOrderDetail" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>جزئیات درخواست شعبه</title>
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
                            <td style="border:none"><%=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutTitleHtml) %></td>
                        </tr>
                    </table>
              <%try
                {
                    BranchOrderBusiness business = new BranchOrderBusiness();
                    BranchOrder Order =business.Retrieve(long.Parse(Request.QueryString["id"]));
                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;
                    %>
                    
                    <table border="0px" class="lsttbl">
                        <tr class="thead">
                            <th colspan="2" style="text-align:center;">جزئیات سفارش</th>
                        </tr>
                        <tr>
                            <td class="caption">کد درخواست : <%=Order.ID %></td>

                            <td class="caption"><%=Order.Branch.Title %></td>
                        </tr>
                        <tr>
                            <td class="caption">وضعیت درخواست : <%=EnumUtil.GetEnumElementPersianTitle((BranchReturnOrderStatus)Order.Status) %></td>

                            <td class="caption">وضعیت ارسالی : <%=Order.ShipmentStatus_ID.HasValue?Order.BranchOrderShipmentState.Title:"----" %></td>
                        </tr>
                        <tr>        
                            <td class="caption">تاریخ تحویل : <%=Order.OrderedPersianDate %></td>
        
                            <td class="caption">تاریخ ثبت : <%=Util.GetPersianDateWithTime(Order.RegDate) %></td>
                        </tr>
                        <tr>        
                            <td class="caption">توضیحات شعبه : <%=string.IsNullOrEmpty(Order.BranchDescription) ? "----" : Order.BranchDescription%></td>
        
                            <td class="caption">توضیحات مدیر : <%=string.IsNullOrEmpty(Order.AdminDescription) ? "----" : Order.AdminDescription%></td>
                        </tr>                 
                        <tr>
                            <td class="caption" colspan="2" style="line-height:17px;">آدرس شعبه: <%= Order.Branch.Description%></td>
                        </tr>
                    </table>
                    <%foreach (BranchOrderDetail item in Order.BranchOrderDetails.ToList())
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.TotalPrice;
                        TotalNumber++;
                        if (RowNumber == 0 && (Order.BranchOrderDetails.Count() - TotalNumber) < RowPerPage-1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام کالا</th>
                                    <th>مبلغ واحد</th>
                                    <th>تعداد</th>
                                    <th>مبلغ کل</th>
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام کالا</th>
                                    <th>مبلغ واحد</th>
                                    <th>تعداد</th>
                                    <th>مبلغ کل</th>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=item.BranchOrderTitle_Title%></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.SinglePrice)%> ریال</td>
                            <td><%=item.Quantity + " عدد"%></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.TotalPrice)%> ریال</td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == Order.BranchOrderDetails.Count())
                        {%>
                                <tr class="ftr">
                                    <td colspan="3">&nbsp;</td>
                                    <td>جمع کل: </td>
                                    <td><%=UIUtil.GetCommaSeparatedOf(TotalAmount)%> ریال</td>
                                </tr>
                            </table>
                        <%}
                    else if (RowNumber == RowPerPage)
                    {
                        RowNumber = 0; %></table><%}    
                    }
                    
                    if (!string.IsNullOrEmpty(kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutFooterHtml))){%>
                    <table border="0px" class="lsttbl">
                        <tr>
                            <td style="border:none"><%=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutFooterHtml) %></td>
                        </tr>
                    </table>
                    <%}         
                }
                  
                catch (Exception)
                {
                    Response.Redirect(GetRouteUrl("admin-branchorder", null));
                }%>
        </div>
    </form>
</body>
</html>
