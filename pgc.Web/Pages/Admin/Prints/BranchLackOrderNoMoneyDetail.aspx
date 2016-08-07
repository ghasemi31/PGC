<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchLackOrderNoMoneyDetail.aspx.cs" Inherits="Pages_Admin_Prints_BranchLackOrderNoMoneyDetail" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>جزئیات کسری</title>
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
                    BranchLackOrderBusiness business = new BranchLackOrderBusiness();
                    BranchLackOrder lackOrder =business.Retrieve(long.Parse(Request.QueryString["id"]));
                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;
                    %>
                    
                    <table border="0px" class="lsttbl">
                        <tr class="thead">
                            <th colspan="2" style="text-align:center;">جزئیات کسری</th>
                        </tr>
                        <tr>
                            <td>شعبه : <%=lackOrder.BranchOrder.Branch.Title %></td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>کد کسری : <%=lackOrder.ID%></td>

                            <td>کد درخواست : <%=lackOrder.BranchOrder_ID %></td>
                        </tr>
                        <tr>        
                            <td style="width:50%;">تاریخ کسری : <%=Util.GetPersianDateWithTime(lackOrder.RegDate) %></td>
        
                            <td>وضعیت کسری : <%=EnumUtil.GetEnumElementPersianTitle((BranchLackOrderStatus)lackOrder.Status)%></td>
                        </tr>
                        <tr>        
                            <td>توضیحات شعبه : <%=string.IsNullOrEmpty(lackOrder.BranchDescription) ? "----" : lackOrder.BranchDescription%></td>
        
                            <td>توضیحات مدیر : <%=string.IsNullOrEmpty(lackOrder.AdminDescription) ? "----" : lackOrder.AdminDescription%></td>
                        </tr>                 
                    </table>
                    <%foreach (BranchLackOrderDetail item in lackOrder.BranchLackOrderDetails.ToList())
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.TotalPrice;
                        TotalNumber++;
                        if (RowNumber == 0 && (lackOrder.BranchLackOrderDetails.Count() - TotalNumber) < RowPerPage - 1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام کالا</th>
                                   <%-- <th>مبلغ واحد</th>--%>
                                    <th>تعداد کسری</th>
                                    <th>تعداد درخواستی</th>
                                    <%--<th>مبلغ کل</th>--%>
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام کالا</th>
                                    <%--<th>مبلغ واحد</th>--%>
                                    <th>تعداد کسری</th>
                                    <th>تعداد درخواستی</th>
                                    <%--<th>مبلغ کل</th>--%>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=item.BranchOrderTitle_Title%></td>
                            <%--<td><%=UIUtil.GetCommaSeparatedOf(item.SinglePrice)%> ریال</td>--%>
                            <td><%=item.Quantity + " عدد"%></td>
                            <td><%=item.BranchLackOrder.BranchOrder.BranchOrderDetails.Where(f=>f.BranchOrderTitle_ID==item.BranchOrderTitle_ID).FirstOrDefault().Quantity + " عدد"%></td>
                            <%--<td><%=UIUtil.GetCommaSeparatedOf(item.TotalPrice)%> ریال</td>--%>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == lackOrder.BranchLackOrderDetails.Count())
                        {%>
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
                    Response.Redirect(GetRouteUrl("admin-branchlackorder", null));
                }%>
        </div>
    </form>
</body>
</html>
