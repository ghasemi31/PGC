<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchFinanceLog.aspx.cs" Inherits="Pages_Admin_Prints_BranchFinanceLog" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>تاریخچه عملیات</title>
    <link href="<%=ResolveClientUrl("~/Styles/Shared/PrintPage.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveClientUrl("~/scripts/shared/jquery-1.7.2.min.js")%>" type="text/javascript" language="javascript"></script>
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
                            <td style="border:none"><%=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutTitleHtml) %></td>
                        </tr>
                    </table>
              <%try
                {
                    BranchFinanceLogPattern pattern = (BranchFinanceLogPattern)Session["BranchFinanceLogPrintPattern"];
                    BranchFinanceLogBusiness business = new BranchFinanceLogBusiness();
                    IQueryable<BranchFinanceLog> OrdersList = business.Search_SelectPrint(pattern);
                    
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;%>
                    
                    <%if (pattern.Branch_ID > 0 || 
                          pattern.PersianDate.SearchMode!=DateRangePattern.SearchType.Nothing || 
                          pattern.LogType_ID > 0 ||
                          BasePattern.IsEnumAssigned(pattern.ActionType) ||
                          !string.IsNullOrEmpty( pattern.Title) ) {%>
                                    <table border="0px" class="lsttbl">
                                        <tr class="thead" style="text-align:center;">
                                            <th colspan="2">نتایج جستجو بر روی فیلد های ذیل</th>
                                        </tr>
                                        <tr>
                                            <%if (pattern.Branch_ID > 0){ %>
                                                <td class="caption"style="border:none">شعبه : <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title %></td>
                                            <%} %>                            
                                       
                                            <%if (pattern.LogType_ID > 0){ %>
                                                <td class="caption"style="border:none">کد فاکتور : <%=pattern.LogType_ID %></td>
                                            <%} %>
                                          </tr>
                                        <tr>
                                            <%if (BasePattern.IsEnumAssigned(pattern.LogType)){ %>
                                                <td class="caption"style="border:none">نوع فاکتور : <%=EnumUtil.GetEnumElementPersianTitle(pattern.LogType) %></td>
                                            <%} %>                            
                                       
                                            <%if (!string.IsNullOrEmpty(pattern.Title)){ %>
                                                <td class="caption"style="border:none">عبارت : <%=pattern.Title %></td>
                                            <%} %>
                                        <tr>
                                        </tr>     
                                            <%if (BasePattern.IsEnumAssigned(pattern.ActionType)){ %>
                                                <td class="caption"style="border:none">نوع تغییر : <%=EnumUtil.GetEnumElementPersianTitle(pattern.ActionType) %></td>
                                            <%} %>
                                           
                                            <%if (pattern.PersianDate.SearchMode!=DateRangePattern.SearchType.Nothing){ %>                  
                                                <td class="caption"style="border:none">تاریخ وقوع : 
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
                                <th>لیست کامل گزارش ها</th>
                            </tr>
                        </table>
                    <%} %>


                    <%foreach (BranchFinanceLog item in OrdersList)
                    {
                        isAltRow=!isAltRow;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام شعبه</th>
                                    <th>کد فاکتور</th>
                                    <th>نوع تغییر</th>
                                    <th>انجام دهنده</th>
                                    <th>تاریخ</th>
                                    <th>توضیحات</th>
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام شعبه</th>
                                    <th>کد فاکتور</th>
                                    <th>نوع تغییر</th>
                                    <th>انجام دهنده</th>
                                    <th>تاریخ</th>
                                    <th>توضیحات</th>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=item.BranchTitle%></td>
                            <td><%=item.LogType_ID %></td>
                            <td><%=EnumUtil.GetEnumElementPersianTitle((BranchFinanceLogActionType)item.ActionType) %></td>
                            <td><%=item.UserName%></td>
                            <td><%=DateUtil.GetPersianDateWithTime(item.Date)%></td>
                            <td><%=item.Description%></td>
                        </tr>
                    <%RowNumber++;
                    if (TotalNumber == OrdersList.Count())
                        {%></table><%}
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
                    Response.Redirect(GetRouteUrl("admin-branchfinancelog", null));
                }%>
        </div>
    </form>
</body>
</html>
