<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchSoldList.aspx.cs" Inherits="Pages_Admin_Prints_BranchSoldList" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>گزارش کلان فروش</title>
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
                    IQueryable<BranchSold> OrdersList = business.Search_SelectPrint(pattern);
                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;%>
                    
                    <%if (pattern.Branch_ID > 0 ||
                          pattern.OrderTitle_ID > 0 ||
                          pattern.Price.Type != RangeType.Nothing ||
                          pattern.SoldPersianDate.SearchMode != DateRangePattern.SearchType.Nothing || 
                          BasePattern.IsEnumAssigned(pattern.Type)) {%>
                            <table border="0px" class="lsttbl">
                                <tr class="thead" style="text-align:center;">
                                    <th colspan="2">نتایج جستجو بر روی فیلد های ذیل در گزارش کلان فروش</th>
                                </tr>
                                <tr>
                                    <%if (BasePattern.IsEnumAssigned(pattern.Type)){ %>
                                        <td class="caption" style="border:none">نوع گزارش : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Type) %></td>
                                    <%} %>
                                </tr>
                                <tr>
                                    <%if (pattern.Branch_ID>0){ %>
                                        <td class="caption" style="border:none">شعبه : <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title %></td>
                                    <%} %>
                         
                                    <%if (pattern.OrderTitle_ID>0){ %>
                                        <td class="caption"style="border:none">عنوان کالای سفارشی : <%=new BranchOrderTitleBusiness().Retrieve(pattern.OrderTitle_ID).Title %></td>
                                    <%} %>
                                </tr>   
                                <tr>
                                     <%if (pattern.SoldPersianDate.SearchMode!=DateRangePattern.SearchType.Nothing){ %>
                                            <td class="caption"style="border:none">تاریخ تحویل : 
                                                <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.SoldPersianDate.SearchMode)%>
                                                <%= pattern.SoldPersianDate.HasDate ? pattern.SoldPersianDate.Date : ""%>
                                                <%= pattern.SoldPersianDate.HasFromDate ? pattern.SoldPersianDate.FromDate : ""%>
                                                <%= pattern.SoldPersianDate.HasToDate ? " و " + pattern.SoldPersianDate.ToDate : ""%>
                                            </td>
                                        <%} %>
                                        
                                        <%if (pattern.Price.Type!=RangeType.Nothing){ %>
                                            <td class="caption"style="border:none">مبلغ : 
                                                <%= EnumUtil.GetEnumElementPersianTitle((RangeType)pattern.Price.Type)%>
                                                <%= pattern.Price.HasFirstNumber ? UIUtil.GetCommaSeparatedOf( pattern.Price.FirstNumber ) + " ریال" : ""%>
                                                <%= pattern.Price.HasSecondNumber ? " و " + UIUtil.GetCommaSeparatedOf( pattern.Price.SecondNumber ) + " ریال" : ""%>
                                            </td>
                                        <%} %>
                                </tr>                     
                            </table>
                    <%}else{ %>
                        <table border="0px" class="lsttbl">
                            <tr class="thead" style="text-align:center;">
                                <th>لیست گزارش جامع فروش</th>
                            </tr>
                        </table>
                    <%} %>


                    <%foreach (BranchSold item in OrdersList)
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.Amount;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام شعبه</th>
                                    <th>مبلغ</th>
                                    <th>سقف حداقل اعتبار</th>                                    
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام شعبه</th>
                                    <th>مبلغ</th>
                                    <th>سقف حداقل اعتبار</th>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=item.Title%></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.Amount)%> ریال</td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.MinimumCredit)%> ریال</td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == OrdersList.Count())
                        {%>
                                <tr class="ftr">
                                    <td>&nbsp;</td>
                                    <td>جمع کل : </td>
                                    <td><%= UIUtil.GetCommaSeparatedOf(TotalAmount) %> ریال</td>                                    
                                    <td>&nbsp;</td>                                    
                                </tr>
                            </table>
                        <%}
                        else if (RowNumber == RowPerPage)
                        {
                            RowNumber = 0; %>
                            </table>
                        <%}    
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
                    Response.Redirect(GetRouteUrl("admin-branchsold", null));
                }%>
        </div>
    </form>
</body>
</html>
