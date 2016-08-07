<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchOrderedTitleList.aspx.cs" Inherits="Pages_Admin_Prints_BranchOrderedTitleList" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لیست کالاهای سفارش شده</title>
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
                    BranchOrderedTitlePattern pattern = (BranchOrderedTitlePattern)Session["BranchOrderedTitlePrintPattern"];
                    BranchOrderedTitleBusiness business = new BranchOrderedTitleBusiness();
                    IQueryable<BranchOrderedTitle> OrdersList = business.Search_SelectPrint(pattern);
                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;%>
                    
                    <%if (pattern.Branch_ID > 0 || 
                          pattern.PersianDate.SearchMode!=DateRangePattern.SearchType.Nothing || 
                          pattern.OrderTitle_ID > 0 ||
                          BasePattern.IsEnumAssigned(pattern.Status)) {%>
                                <table border="0px" class="lsttbl">
                                    <tr class="thead" style="text-align:center;">
                                        <th colspan="2">نتایج جستجو بر روی فیلد های ذیل در لیست کالاهای سفارش شده</th>
                                    </tr>
                                    <tr>
                                        <%if (pattern.Branch_ID>0){ %>
                                            <td class="caption"style="border:none">شعبه : <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title %></td>
                                        <%} %>                            
                                    
                                        <%if (pattern.OrderTitle_ID>0){ %>
                                            <td class="caption"style="border:none">عنوان کالای سفارشی : <%=new BranchOrderTitleBusiness().Retrieve(pattern.OrderTitle_ID).Title %></td>
                                        <%} %>
                                    </tr>
                                    <tr>
                                        <%if (BasePattern.IsEnumAssigned(pattern.Status)){ %>
                                            <td class="caption"style="border:none">وضعیت درخواست : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Status) %></td>
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
                                </table>
                    <%}else{ %>
                        <table border="0px" class="lsttbl">
                            <tr class="thead" style="text-align:center;">
                                <th>لیست کامل کالاهای سفارش شده</th>
                            </tr>
                        </table>
                    <%} %>


                    <%foreach (BranchOrderedTitle item in OrdersList)
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.TotalPrice;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
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
                            <td><%=item.Title%></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.SinglePrice)%> ریال</td>
                            <td><%=item.Quantity%></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.TotalPrice)%> ریال</td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == OrdersList.Count())
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
                    Response.Redirect(GetRouteUrl("admin-branchorderedtitle",null));
                }%>
        </div>
    </form>
</body>
</html>
