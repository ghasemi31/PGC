<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchOrderList.aspx.cs" Inherits="Pages_Admin_Prints_BranchOrderList" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لیست درخواست های شعب</title>
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
                    BranchOrderPattern pattern = (BranchOrderPattern)Session["BranchOrderPrintPattern"];
                    BranchOrderBusiness business = new BranchOrderBusiness();
                    IQueryable<BranchOrder> OrdersList = business.Search_SelectPrint(pattern);
                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;%>
                    
                    <%if (pattern.Branch_ID > 0 || 
                          pattern.OrderedPersianDate.SearchMode != DateRangePattern.SearchType.Nothing || 
                          pattern.ID > 0 ||
                          pattern.OrderTitle_ID > 0 ||
                          pattern.Price.Type != RangeType.Nothing ||
                          BasePattern.IsEnumAssigned(pattern.Status) ||
                          !string.IsNullOrEmpty( pattern.Title) ) {%>
                                <table border="0px" class="lsttbl">
                                    <tr class="thead">
                                        <th style="text-align:center;" colspan="2">نتایج جستجو بر روی فیلد های ذیل در درخواست های شعب</th>
                                    </tr>
                                    <tr>
                                        <%if (pattern.Branch_ID>0){ %>
                                            <td class="caption"style="border:none">شعبه : <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title %></td>
                                        <%} %>

                                         <%if (pattern.ID>0){ %>
                                            <td class="caption"style="border:none">کد درخواست : <%=pattern.ID%></td>
                                        <%} %>
                                    </tr>
                                    <tr>
                                        <%if (pattern.OrderTitle_ID>0){ %>
                                            <td class="caption"style="border:none">عنوان کالای سفارشی : <%=new BranchOrderTitleBusiness().Retrieve(pattern.OrderTitle_ID).Title %></td>
                                        <%} %>

                                        <%if (BasePattern.IsEnumAssigned(pattern.Status)){ %>
                                            <td class="caption"style="border:none">وضعیت درخواست : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Status) %></td>
                                        <%} %>
                                    </tr>
                                    <tr>
                                        <%if (pattern.OrderedPersianDate.SearchMode!=DateRangePattern.SearchType.Nothing){ %>
                                            <td class="caption"style="border:none">تاریخ تحویل : 
                                                <%= EnumUtil.GetEnumElementPersianTitle((DateRangePattern.SearchType)pattern.OrderedPersianDate.SearchMode)%>
                                                <%= pattern.OrderedPersianDate.HasDate ? pattern.OrderedPersianDate.Date : ""%>
                                                <%= pattern.OrderedPersianDate.HasFromDate ? pattern.OrderedPersianDate.FromDate : ""%>
                                                <%= pattern.OrderedPersianDate.HasToDate ? " و " + pattern.OrderedPersianDate.ToDate : ""%>
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
                                <th>لیست کامل درخواست ها</th>
                            </tr>
                        </table>
                    <%} %>


                    <%foreach (BranchOrder item in OrdersList)
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.TotalPrice;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام شعبه</th>
                                    <th>کد درخواست</th>
                                    <th>مبلغ</th>
                                    <th>تاریخ درج</th>
                                    <th>تاریخ تحویل</th>
                                    <th>وضعیت</th>
                                    <th>وضعیت ارسالی</th>
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام شعبه</th>
                                    <th>کد درخواست</th>
                                    <th>مبلغ</th>
                                    <th>تاریخ درج</th>
                                    <th>تاریخ تحویل</th>
                                    <th>وضعیت</th>
                                    <th>وضعیت ارسالی</th>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=item.Branch.Title%></td>
                            <td><%=item.ID %></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.TotalPrice)%> ریال</td>
                            <td><%=Util.GetPersianDateWithTime(item.RegDate)%></td>
                            <td><%=item.OrderedPersianDate%></td>
                            <td><%=EnumUtil.GetEnumElementPersianTitle((BranchOrderStatus)item.Status) %></td>
                            <td><%=item.ShipmentStatus_ID.HasValue?item.BranchOrderShipmentState.Title:"----" %></td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == OrdersList.Count())
                        {%>
                                <tr class="ftr">
                                    <td colspan="2">&nbsp;</td>
                                    <td>جمع کل: </td>
                                    <td><%=UIUtil.GetCommaSeparatedOf(TotalAmount)%> ریال</td>
                                    <td colspan="4">&nbsp;</td>
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
