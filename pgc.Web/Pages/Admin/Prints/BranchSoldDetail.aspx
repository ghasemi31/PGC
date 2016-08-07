<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchSoldDetail.aspx.cs" Inherits="Pages_Admin_Prints_BranchSoldDetail" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ریز کالاهای فروخته شده</title>
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
                    BranchSoldPattern pattern = (BranchSoldPattern)Session["BranchSoldPrintPattern"];
                    BranchSoldBusiness business = new BranchSoldBusiness();
                    pattern.Branch_ID = long.Parse(Request.QueryString["id"]);
                    pattern.OrderTitle_ID = 0;
                    pgcEntities Context = business.Context;

                    

                    //1.Split Return & Order
                    var OrderTransactionList = business.SearchDuringBranchTransactions(pattern);

                    var trOrderID=OrderTransactionList.Where(d => d.TransactionType == (int)BranchTransactionType.BranchOrder).Select(f=>f.TransactionType_ID).ToList();
                    var trReturnID = OrderTransactionList.Where(d => d.TransactionType == (int)BranchTransactionType.BranchReturnOrder).Select(f => f.TransactionType_ID).ToList();

                    IQueryable<BranchOrder> OrderList = Context.BranchOrders.Where(f => trOrderID.Contains(f.ID));

                    IQueryable<BranchReturnOrder> ReturnList = Context.BranchReturnOrders.Where(f => trReturnID.Contains(f.ID));


                    //2.Create List Of Order Title
                    List<long> orderTitleIDs = new List<long>();

                    if (OrderList.Count() > 0)
                        orderTitleIDs.AddRange(OrderList.SelectMany(f => f.BranchOrderDetails).Select(g => g.BranchOrderTitle_ID.Value));

                    if (ReturnList.Count() > 0)
                        orderTitleIDs.AddRange(ReturnList.SelectMany(f => f.BranchReturnOrderDetails).Select(g => g.BranchOrderTitle_ID.Value));

                    orderTitleIDs = orderTitleIDs.Distinct().ToList();


                    
                    
                    //Header 
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;
                    %>
                    
                    <table border="0px" class="lsttbl">
                        <tr class="thead">
                            <th colspan="2" style="text-align:center;">ریز کالاهای فروخته شده توسط <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title%></th>
                        </tr>
                        <tr>
                           <%if (pattern.SoldPersianDate.SearchMode!=DateRangePattern.SearchType.Nothing){ %>
                                <td class="caption"style="border:none">تاریخ : 
                                    <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.SoldPersianDate.SearchMode)%>
                                    <%= pattern.SoldPersianDate.HasDate ? pattern.SoldPersianDate.Date : ""%>
                                    <%= pattern.SoldPersianDate.HasFromDate ? pattern.SoldPersianDate.FromDate : ""%>
                                    <%= pattern.SoldPersianDate.HasToDate ? " و " + pattern.SoldPersianDate.ToDate : ""%>
                                </td>
                            <%} %>
                          
                            <%if (BasePattern.IsEnumAssigned(pattern.Type)){ %>
                                <td class="caption" style="border:none">نوع گزارش : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Type) %></td>
                            <%} %>
                        </tr>   
                    </table>

                    <table cellpadding="0" cellspacing="0" class="lsttbl" >
                        <tr class="thead">
                            <th>ردیف</th>
                            <th>نام کالا</th>
                            <th><%=(pattern.Type == BranchSoldType.Rejected) ? "تعداد مرجوعی" : "تعداد فروخته شده" %></th>
                            <th>مبلغ کل</th>
                        </tr>

                    <%
                    
                    long quantityOFAll = 0;
                    long totalPriceOFAll = 0;
                    
                    foreach (long orderTitle_ID in orderTitleIDs)
                    {
                        isAltRow=!isAltRow;                       
                        TotalNumber++;
                        
                        RowNumber++;
                        long Quantity = 0;
                           
                        if (pattern.Type == BranchSoldType.Sold && OrderList.Count() > 0)
                        {
                            var _temp = OrderList.SelectMany(f => f.BranchOrderDetails).Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                            if (_temp.Count() > 0)
                                Quantity += _temp.Sum(f => f.Quantity);
                        }


                        if (ReturnList.Count() > 0)
                        {
                            var _temp = ReturnList.SelectMany(f => f.BranchReturnOrderDetails).Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                            if (_temp.Count() > 0)
                                Quantity = (pattern.Type == BranchSoldType.Rejected) ?
                                                    Quantity + _temp.Sum(f => f.Quantity)
                                                    :
                                                    Quantity - _temp.Sum(f => f.Quantity);
                        }


                        long TotalAmount = 0;
                        if (pattern.Type == BranchSoldType.Sold && OrderList.Count() > 0)
                        {
                            var _temp = OrderList.SelectMany(f => f.BranchOrderDetails).Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                            if (_temp.Count() > 0)
                                TotalAmount += _temp.Sum(f => f.TotalPrice);
                        }


                        if (ReturnList.Count() > 0)
                        {
                            var _temp = ReturnList.SelectMany(f => f.BranchReturnOrderDetails).Where(g => g.BranchOrderTitle_ID == orderTitle_ID);

                            if (_temp.Count() > 0)
                                TotalAmount = (pattern.Type == BranchSoldType.Rejected) ?
                                                    TotalAmount + _temp.Sum(f => f.TotalPrice)
                                                    :
                                                    TotalAmount - _temp.Sum(f => f.TotalPrice);
                        }

                        totalPriceOFAll += TotalAmount;
                        quantityOFAll += Quantity;%>

                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            
                            <td><%=TotalNumber%></td> 
                            
                            <td><%= new BranchOrderTitleBusiness().Retrieve(orderTitle_ID).Title %></td>

                            <td><%= UIUtil.GetCommaSeparatedOf(Quantity) + " عدد" %></td>

                            <td><%= UIUtil.GetCommaSeparatedOf(TotalAmount) + " ریال" %></td>
                        </tr>
                        
                    <%}%>

                        <tr class="ftr">
                            <td>&nbsp;</td>
                            <td>جمع کل : </td>
                            <td><%=UIUtil.GetCommaSeparatedOf(quantityOFAll)%> عدد</td>
                            <td><%=UIUtil.GetCommaSeparatedOf(totalPriceOFAll)%> ریال</td> 
                        </tr>
                    </table><%
                    
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
                    Response.Redirect(GetRouteUrl("admin-branchsold", null));
                }%>
        </div>
    </form>
</body>
</html>
