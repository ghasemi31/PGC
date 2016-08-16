<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchCreditList.aspx.cs" Inherits="Pages_Admin_Prints_BranchCreditList" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>گزارش اعتبار شعب</title>
    <link href="<%=ResolveClientUrl("~/Styles/Shared/PrintPage.css")%>" rel="stylesheet" type="text/css" />
    <script src="<%=ResolveClientUrl("~/scripts/shared/jquery-1.7.2.min.js")%>" type="text/javascript" language="javascript"></script>
    <script language="javascript" type="text/javascript">
        $(document).ready(new function () {
            self.print();
        });
</script>
</head>
<body>
    <%--<form id="form1" runat="server">
        <div>
                     <table border="0px" class="lsttbl">
                        <tr>
                            <td style="border:none"><%=kFrameWork.Business.OptionBusiness.GetHtml(OptionKey.BranchFinancePrintLayoutTitleHtml) %></td>
                        </tr>
                    </table>
              <%try
                {
                    BranchCreditPattern pattern = (BranchCreditPattern)Session["BranchCreditPrintPattern"];
                    BranchCreditBusiness business = new BranchCreditBusiness();
                    IQueryable<BranchCredit> OrdersList = business.Search_SelectPrint(pattern);
                    
                    long TotalAmount = 0;
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 0;
                    int TotalNumber = 0;%>
                    
                    <%if (pattern.Branch_ID > 0 || 
                          BasePattern.IsEnumAssigned(pattern.Status)) {%>
                            <table border="0px" class="lsttbl">
                                <tr class="thead" style="text-align:center;">
                                    <th colspan="2">نتایج جستجو بر روی فیلد های ذیل</th>
                                </tr>
                                <tr>
                                    <%if (pattern.Branch_ID>0){ %>
                                        <td class="caption" style="border:none">شعبه : <%=new BranchBusiness().Retrieve(pattern.Branch_ID).Title %></td>
                                    <%} %>
                         
                                    <%if (BasePattern.IsEnumAssigned(pattern.Status)){ %>
                                        <td class="caption" style="border:none">وضعیت درخواست : <%=EnumUtil.GetEnumElementPersianTitle(pattern.Status) %></td>
                                    <%} %>
                                </tr>                        
                            </table>
                    <%}else{ %>
                        <table border="0px" class="lsttbl">
                            <tr class="thead" style="text-align:center;">
                                <th>لیست اعتبار تمامی شعب</th>
                            </tr>
                        </table>
                    <%} %>


                    <%foreach (BranchCredit item in OrdersList)
                    {
                        isAltRow=!isAltRow;
                        TotalAmount += item.CurrentCredit - item.CurrentDebt;
                        TotalNumber++;
                        if (RowNumber == 0 && (OrdersList.Count() - TotalNumber) < RowPerPage-1)
                        {%>                      
                            <table cellpadding="0" cellspacing="0" class="lsttbl" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام شعبه</th>
                                    <th>بدهکار</th>
                                    <th>بستانکار</th>
                                    <th>سقف حداقل اعتبار</th>
                                    <th>وضعیت</th>
                                </tr>
                        <%}
                        else if (RowNumber == 0)
                        {%>
                            <table cellpadding="0" cellspacing="0" >
                                <tr class="thead">
                                    <th>ردیف</th>
                                    <th>نام شعبه</th>
                                    <th>بدهکار</th>
                                    <th>بستانکار</th>
                                    <th>سقف حداقل اعتبار</th>
                                    <th>وضعیت</th>
                                </tr>
                        <%} %>                                                    
                        <tr class="<%=(isAltRow)?"altrow":"row" %>">
                            <td><%=TotalNumber%></td>                            
                            <td><%=item.Title%></td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.CurrentDebt)%> ریال</td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.CurrentCredit)%> ریال</td>
                            <td><%=UIUtil.GetCommaSeparatedOf(item.MinimumCredit)%> ریال</td>
                            <td><%=EnumUtil.GetEnumElementPersianTitle((BranchCreditStatus)item.Status) %></td>
                        </tr>
                    <%
                        RowNumber++;
                        if (TotalNumber == OrdersList.Count())
                        {%>
                                <tr class="ftr">
                                    <td colspan="2">جمع کل : </td>
                                    <td><%=(TotalAmount < 0) ? UIUtil.GetCommaSeparatedOf(Math.Abs(TotalAmount)) : "-----"%> ریال</td>
                                    <td><%=(TotalAmount > 0) ? UIUtil.GetCommaSeparatedOf(Math.Abs(TotalAmount)) : "-----"%> ریال</td>
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
                    Response.Redirect(GetRouteUrl("admin-branchcredit", null));
                }%>
        </div>
    </form>--%>
</body>
</html>
