<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchManualCharge.aspx.cs" Inherits="Pages_Agent_Prints_BranchManualCharge" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>لیست شارژهای دستی</title>
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
                        BranchManualChargePattern pattern = (BranchManualChargePattern)Session["BranchManualChargePattern"];
                        BranchManualChargeBusiness business = new BranchManualChargeBusiness();
                        IQueryable<BranchTransaction> ManualChargeList = business.Search_SelectPrint(pattern);
                    
                        bool isAltRow = true;
                        int RowPerPage = kFrameWork.Business.OptionBusiness.GetInt(OptionKey.OrderListPrintLayoutRowNumber);
                        int RowNumber = 1;
                        int TotalNumber = 0;%>
                    
                    <table border="0px" class="lsttbl">
                        <tr class="thead" style="text-align:center">
                            <th>لیست شارژهای دستی</th>
                        </tr>
                        <tr>        
                            <%--<td style="width:50%;border:none">عنوان کالا : <%=new BranchOrderTitleBusiness().Retrieve(orderTitle_ID).Title %></td>--%>
                        </tr>                                       
                    </table>
                    
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr class="thead" >
                            <th style="border:none;">ردیف</th>
                            <th style="border:none;">نام شعبه</th>                            
                            <th style="border:none;">تاریخ شارژ</th>
                            <th style="border:none;">بستانکار</th>
                            <th style="border:none;">بدهکار</th>
                            <th style="border:none;">توضیحات</th>
                        </tr>
                        <% 
                        foreach (BranchTransaction tr in ManualChargeList.OrderBy(f=>f.ID)){
                            
                            isAltRow=!isAltRow;
                            TotalNumber++;%>

                            <tr class="<%=(isAltRow)?"altrow":"row" %>">
    
                                <td><%=RowNumber.ToString() %></td>

                                <td><%=tr.Branch.Title%></td>
                            
                                <td><%=Util.GetPersianDateWithTime(tr.RegDate)%></td>
                            
                                <td><%=UIUtil.GetCommaSeparatedOf(tr.BranchCredit)%> ریال</td>
                           
                                <td><%=UIUtil.GetCommaSeparatedOf(tr.BranchDebt)%> ریال</td>

                                <td><%=tr.Description%></td>

                            </tr>

                            
                            <%RowNumber++;
                        }%>
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
                    Response.Redirect(GetRouteUrl("admin-manualcharge", null));
                }%>
        </div>
    </form>
</body>
</html>
