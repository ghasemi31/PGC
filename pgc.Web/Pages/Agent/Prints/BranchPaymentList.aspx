<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchPaymentList.aspx.cs" Inherits="Pages_Agent_Prints_BranchPaymentList" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لیست پرداختی های شعبه</title>   
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
                    BranchPaymentPattern pattern = (BranchPaymentPattern)Session["BranchPaymentPrintPattern"];
                    BranchPaymentBusiness business = new BranchPaymentBusiness();
                    IQueryable<BranchPayment> OrdersList = business.Search_SelectPrint(pattern);
                    pattern.Branch_ID = kFrameWork.UI.UserSession.User.Branch_ID.Value;                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;%>
                    
                    <%if (pattern.Branch_ID > 0 || 
                          pattern.PayDate.SearchMode != DateRangePattern.SearchType.Nothing ||                           
                          pattern.DefaultSearch ||
                          pattern.Price.Type != RangeType.Nothing ||
                          BasePattern.IsEnumAssigned(pattern.Type) ||
                          !string.IsNullOrEmpty( pattern.Title) ) {%>
                                    <table border="0px" class="lsttbl">
                                        <tr class="thead" style="text-align:center;">
                                            <th colspan="2">نتایج جستجو بر روی فیلد های ذیل در لیست پرداختی های شعبه</th>
                                        </tr>
                                        <%if (pattern.DefaultSearch){ %>
                                            <tr>
                                                <td class="caption">نوع پرداخت ها : تایید شده یا در حال بررسی</td>

                                            </tr>
                                        <%}else{ %>
                                            <tr>
                                                <%if (pattern.Branch_ID>0){ %>
                                                    <td class="caption" style="border:none">شعبه : <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title %></td>
                                                <%}else{%><td style="border:none">&nbsp;</td><%} %>
                            
                                                <%if (!string.IsNullOrEmpty(pattern.Title)){ %>
                                                    <td class="caption"style="border:none">عبارت : <%=pattern.Title %></td>
                                                <%}else{%><td style="border:none">&nbsp;</td><%} %>

                                            </tr>
                                            <tr>
                                                <%if (BasePattern.IsEnumAssigned(pattern.Type)){ %>
                                                    <td class="caption"style="border:none">وضعیت درخواست : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Type)%></td>
                                                <%} %>
                                                <%if (pattern.PayDate.SearchMode!=DateRangePattern.SearchType.Nothing){ %>
                                                    <td class="caption"style="border:none">تاریخ پرداخت : 
                                                        <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.PayDate.SearchMode)%>
                                                        <%= pattern.PayDate.HasDate ? pattern.PayDate.Date : ""%>
                                                        <%= pattern.PayDate.HasFromDate ? pattern.PayDate.FromDate : ""%>
                                                        <%= pattern.PayDate.HasToDate ? " و " + pattern.PayDate.ToDate : ""%>
                                                    </td>
                                                <%} %>
                                            </tr>
                                            <tr>
                                                <%if (pattern.Price.Type!=RangeType.Nothing){ %>
                                                    <td class="caption"style="border:none">مبلغ : 
                                                        <%= EnumUtil.GetEnumElementPersianTitle((RangeType)pattern.Price.Type)%>
                                                        <%= pattern.Price.HasFirstNumber ? UIUtil.GetCommaSeparatedOf( pattern.Price.FirstNumber ) + " ریال" : ""%>
                                                        <%= pattern.Price.HasSecondNumber ? " و " + UIUtil.GetCommaSeparatedOf( pattern.Price.SecondNumber ) + " ریال" : ""%>
                                                    </td>
                                                <%} %>
                                            </tr>
                                        <%} %>
                                    </table>
                    <%}else{ %>
                        <table border="0px" class="lsttbl">
                            <tr class="thead" style="text-align:center;">
                                <th>لیست تمامی پرداخت های شعبه</th>
                            </tr>
                        </table>
                    <%} %>


                    <%foreach (BranchPayment item in OrdersList)
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.Amount;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>مبلغ</th>
                                    <th>تاریخ پرداخت</th>
                                    <th>وضعیت</th>
                                    <th>نحوه پرداخت وجه</th>
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>مبلغ</th>                                    
                                    <th>وضعیت</th>  
                                    <th>تاریخ پرداخت</th>
                                    <th>نحوه پرداخت</th>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=UIUtil.GetCommaSeparatedOf(item.Amount)%> ریال</td>
                            <%  string transactionState = "";

                                if (item.Type == (int)BranchPaymentType.Online)
                                {
                                    if (item.OnlineTransactionState == "OK")
                                    {
                                        if (item.OnlineResultTransaction <= 0)
                                            transactionState = (UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + Math.Abs(item.OnlineResultTransaction).ToString())));
                                        else
                                            transactionState = UserMessageKeyBusiness.GetUserMessageDescription(UserMessageKey.OK);
                                    }
                                    else
                                        transactionState = (EnumUtil.GetEnumElementPersianTitle((OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), (item.OnlineTransactionState))));
                                    
                                    %>
                                        <td><%=transactionState %></td>
                                        <td><%=item.PersianDate %></td>
                                    <%
                                }
                                else
                                {%>
                                    <td><%=EnumUtil.GetEnumElementPersianTitle((BranchOfflinePaymentStatus)item.OfflinePaymentStatus)%></td>    
                                    <td><%=item.OfflineReceiptPersianDate%></td>
                                <%}
                                
                           %>
                            <td><%=EnumUtil.GetEnumElementPersianTitle((BranchPaymentType)item.Type) %></td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == OrdersList.Count())
                        {%>
                                <tr class="ftr">
                                    <td>جمع کل: </td>
                                    <td><%=UIUtil.GetCommaSeparatedOf(TotalAmount)%> ریال</td>
                                    <td colspan="3">&nbsp;</td>
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
                    Response.Redirect(GetRouteUrl("agent-branchpayment", null));
                }%>
        </div>
    </form>
</body>
</html>
