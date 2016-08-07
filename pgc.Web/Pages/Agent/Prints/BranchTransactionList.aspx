<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchTransactionList.aspx.cs" Inherits="Pages_Agent_Prints_BranchTransactionList" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لیست دریافتی پرداختی ها</title>
    <script src="<%=ResolveClientUrl("~/scripts/shared/jquery-1.7.2.min.js")%>" type="text/javascript" language="javascript"></script>
    <link href="<%=ResolveClientUrl("~/Styles/Shared/PrintPage.css")%>" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        $(document).ready(new function () {
            self.print();
        });
    </script>
    <style type="text/css">
        table{font-size:11px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
                     <table border="0px" class="lsttbl">
                        <tr>
                            <td  style="border:none"><%=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutTitleHtml) %></td>
                        </tr>
                    </table>
              <%try
                {
                    BranchTransactionPattern pattern = (BranchTransactionPattern)Session["BranchTransactionPrintPattern"];
                    BranchTransactionBusiness business = new BranchTransactionBusiness();
                    IQueryable<BranchTransaction> OrdersList = business.Search_SelectPrint(pattern);
                    pattern.Branch_ID = kFrameWork.UI.UserSession.User.Branch_ID.Value;
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;%>
                    
                    <%if (pattern.Branch_ID > 0 || 
                          pattern.PersianDate.SearchMode!=DateRangePattern.SearchType.Nothing || 
                          pattern.BranchCreditPrice.Type != RangeType.Nothing ||
                          pattern.BranchDebtPrice.Type != RangeType.Nothing ||
                          BasePattern.IsEnumAssigned(pattern.Type) ||
                          !string.IsNullOrEmpty( pattern.Title) ) {%>
                                    <table border="0px" class="lsttbl">
                                        <tr class="thead" style="text-align:center;">
                                            <th colspan="2">نتایج جستجو بر روی فیلد های ذیل در لیست دریافتی و پرداختی ها</th>
                                        </tr>
                                        <tr>
                                            <%if (pattern.Branch_ID>0){ %>
                                                <td class="caption" style="border:none">شعبه : <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title %></td>
                                            <%}else{%><td style="border:none">&nbsp;</td><%} %>
                            
                                            <%if (!string.IsNullOrEmpty(pattern.Title)){ %>
                                                <td class="caption" style="border:none">عبارت : <%=pattern.Title %></td>
                                            <%}else{%><td style="border:none">&nbsp;</td><%} %>

                                            <%--<td rowspan="3" width="120" style="border:none" class="logowrp"><img class="logo" src="http://pgcizi.com/UserFiles/images/112%20(1).gif"  alt="logo"/></td>--%>
                                        </tr>
                                        <tr>                            
                                            <%if (BasePattern.IsEnumAssigned(pattern.Type)){ %>
                                                <td class="caption"style="border:none">وضعیت درخواست : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Type) %></td>
                                            <%} %>
                       
                                            <%if (pattern.PersianDate.SearchMode!=DateRangePattern.SearchType.Nothing){ %>                  
                                                <td class="caption"style="border:none">تاریخ درخواست : 
                                                    <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.PersianDate.SearchMode) %>
                                                    <%= pattern.PersianDate.HasDate? pattern.PersianDate.Date:"" %>
                                                    <%= pattern.PersianDate.HasFromDate? pattern.PersianDate.FromDate:"" %>
                                                    <%= pattern.PersianDate.HasToDate? "و" + pattern.PersianDate.ToDate:"" %>
                                                </td>
                                            <%} %>
                                        </tr>
                                        <tr>
                                            <%if (pattern.BranchCreditPrice.Type!=RangeType.Nothing){ %>
                                                <td class="caption"style="border:none">مبلغ : 
                                                    <%= EnumUtil.GetEnumElementPersianTitle((RangeType)pattern.BranchCreditPrice.Type)%>
                                                    <%= pattern.BranchCreditPrice.HasFirstNumber ? UIUtil.GetCommaSeparatedOf(pattern.BranchCreditPrice.FirstNumber) + " ریال" : ""%>
                                                    <%= pattern.BranchCreditPrice.HasSecondNumber ? " و " + UIUtil.GetCommaSeparatedOf(pattern.BranchCreditPrice.SecondNumber) + " ریال" : ""%>
                                                </td>
                                            <%} %>

                                            <%if (pattern.BranchDebtPrice.Type!=RangeType.Nothing){ %>
                                                <td class="caption"style="border:none">مبلغ : 
                                                    <%= EnumUtil.GetEnumElementPersianTitle((RangeType)pattern.BranchDebtPrice.Type)%>
                                                    <%= pattern.BranchDebtPrice.HasFirstNumber ? UIUtil.GetCommaSeparatedOf(pattern.BranchDebtPrice.FirstNumber) + " ریال" : ""%>
                                                    <%= pattern.BranchDebtPrice.HasSecondNumber ? " و " + UIUtil.GetCommaSeparatedOf(pattern.BranchDebtPrice.SecondNumber) + " ریال" : ""%>
                                                </td>
                                            <%} %>
                                        </tr>
                                    </table>
                    <%}else{ %>
                        <table border="0px" class="lsttbl">
                            <tr class="thead" style="text-align:center;">
                                <th>لیست کامل دریافتی و پرداختی ها</th>

                                <%--<th width="120" class="logowrp"><img class="logo" src="http://pgcizi.com/UserFiles/images/112%20(1).gif"  alt="logo"/></th>--%>
                            </tr>
                        </table>
                    <%} %>


                    <%foreach (BranchTransaction item in OrdersList)
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.BranchCredit - item.BranchDebt;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نوع تراکنش</th>
                                    <th>بستانکار</th>
                                    <th>بدهکار</th>
                                    <th>تاریخ</th>
                                    <th>توضیحات</th>
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نوع تراکنش</th>
                                    <th>بستانکار</th>
                                    <th>بدهکار</th>
                                    <th>تاریخ</th>
                                    <th>توضیحات</th>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=EnumUtil.GetEnumElementPersianTitle((BranchTransactionType)item.TransactionType) %></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.BranchCredit)%> ریال</td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.BranchDebt)%> ریال</td>
                            <td><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(item.RegDate)%></td>
                            <td><%=item.Description%></td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == OrdersList.Count())
                        {%>
                                <tr class="ftr">
                                    <td colspan="2">جمع کل: </td>
                                    <td><%=(TotalAmount > 0) ? UIUtil.GetCommaSeparatedOf(Math.Abs(TotalAmount)) : "-----"%> ریال</td>
                                    <td><%=(TotalAmount < 0) ? UIUtil.GetCommaSeparatedOf(Math.Abs(TotalAmount)) : "-----"%> ریال</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
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
                    Response.Redirect(GetRouteUrl("agent-branchtransaction", null));
                }%>
        </div>
    </form>
</body>
</html>
