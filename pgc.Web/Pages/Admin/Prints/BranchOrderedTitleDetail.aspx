<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchOrderedTitleDetail.aspx.cs" Inherits="Pages_Admin_Prints_BranchOrderedTitleDetail" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>جزئیات سفارش کالای سفارش داده شده</title>
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
                    long orderTitle_ID=long.Parse(Request.QueryString["id"]);
                    
                    var branchDetailList = business.RetrieveOrderedTitle(orderTitle_ID, (BranchOrderedTitlePattern)Session["BranchOrderedTitlePrintPattern"]);
                    
                    bool isAltRow = true;
                    int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                    int RowNumber = 1;
                    int TotalNumber = 0;%>
                    
                    <table border="0px" class="lsttbl">
                        <tr class="thead" style="text-align:center">
                            <th colspan="2">جزئیات کالای سفارشی</th>
                        </tr>
                        <tr>        
                            <td style="width:50%;border:none">عنوان کالا : <%=new BranchOrderTitleBusiness().Retrieve(orderTitle_ID).Title %></td>
                       
                            <td style="border:none">مبلغ واحد : <%=UIUtil.GetCommaSeparatedOf(branchDetailList.First().SinglePrice) %> ریال</td>
                        </tr>                                       
                    </table>
                    
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr class="thead" >
                            <th style="border:none;">ردیف</th>
                            <th style="border:none;">نام شعبه</th>                            
                            <th style="border:none;">تعداد ارسال شده به شعبه</th>
                            <th style="border:none;">تعداد کسری</th>
                            <th style="border:none;">تعداد مرجوعی</th>
                            <th style="border:none;">تعداد کل فروخته شده</th>
                            <th style="border:none;">مبلغ کل فروش</th>
                        </tr>
                        <% 
                        foreach (BranchDetailTitle dtl in branchDetailList.OrderBy(f=>f.DisplayOrder)){
                            
                            isAltRow=!isAltRow;
                            TotalNumber++;%>

                            <tr class="<%=(isAltRow)?"altrow":"row" %>">
    
                                <td><%=RowNumber.ToString() %></td>

                                <td><%=dtl.Title %></td>
                            
                                <td><%=dtl.OrderQuantity %> عدد</td>
                            
                                <td><%=dtl.LackQuantity %> عدد</td>
                           
                                <td><%=dtl.ReturnQuantity %> عدد</td>

                                <td><%=dtl.OrderQuantity-dtl.LackQuantity %> عدد</td>

                                <td><%=UIUtil.GetCommaSeparatedOf((dtl.OrderQuantity - dtl.ReturnQuantity)* dtl.SinglePrice)%> ریال</td>

                            </tr>

                            
                            <%RowNumber++;
                        }%>

                            <tr class="ftr">
                                <td colspan="2" class="ftrtxt">مجموع کل : </td>
                                <td class="ftrtxt"><%=branchDetailList.Sum(f=>f.OrderQuantity) %> عدد</td>
                                <td class="ftrtxt"><%=branchDetailList.Sum(f => f.LackQuantity) %> عدد</td>
                                <td class="ftrtxt"><%=branchDetailList.Sum(f => f.ReturnQuantity) %> عدد</td>
                                <td class="ftrtxt"><%=branchDetailList.Sum(f => f.OrderQuantity - f.ReturnQuantity) %> عدد</td>
                                <td class="ftrtxt"><%=UIUtil.GetCommaSeparatedOf(branchDetailList.Sum(f=>f.OrderTotalPrice - f.ReturnTotalPrice)) %> ریال</td>
                            </tr>
                    </table>
                <%
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
                    Response.Redirect(GetRouteUrl("admin-branchorderedtitle", null));
                }%>
        </div>
    </form>
</body>
</html>
