<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchPaymentDetail.aspx.cs" Inherits="Pages_Admin_Prints_BranchPaymentDetail" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="pgc.Model.Patterns" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>

                             
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>جزئیات پرداخت شعبه</title>
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
                    BranchPaymentBusiness business = new BranchPaymentBusiness();
                    BranchPayment Pay = business.Retrieve(long.Parse(Request.QueryString["id"]));%>
                    
                         
                    <table class="lsttbl" cellpadding="0" cellspacing="0">
                        <tr class="thead" style="text-align:center;">
                            <th colspan="4">اطلاعات شعبه</th>
                        </tr>    
                        <tr class="row">
                            <td>نام : <%=Pay.Branch.Title%></td>
    
                            <td>تلفن : <%=Pay.Branch.PhoneNumbers.Replace("\n","-")%></td>
                        </tr>
                        <tr class="altrow">
                            <td>اعتبار کنونی شعبه : <%=UIUtil.GetCommaSeparatedOf(BranchCreditBusiness.GetBranchCredit(Pay.Branch_ID))%> ریال</td>        

                            <td>&nbsp;</td>
                        </tr>
                    </table>

                    <%if (Pay.Type==(int)BranchPaymentType.Offline){ %>
                        <table class="lsttbl" cellpadding="0" cellspacing="0">        
                            <tr class="thead" style="text-align:center;">
                                <th colspan="4">جزئیات پرداخت (بصورت آفلاین)</th>
                            </tr>
                            <tr class="row">
                                <td class="bold">مبلغ پرداختی : </td>
                                <td><%=UIUtil.GetCommaSeparatedOf(Pay.Amount) %> ریال</td>
        
                                <td class="bold">تاریخ واریز : </td>
                                <td><%=DateUtil.GetPersianDateWithTime(DateUtil.GetEnglishDateTime(Pay.OfflineReceiptPersianDate))%></td>
                            </tr>
                            <tr class="altrow">            
                                <td class="bold">وضعیت : </td>
                                <td><%=EnumUtil.GetEnumElementPersianTitle((BranchOfflinePaymentStatus)Pay.OfflinePaymentStatus)%></td>

                                <td class="bold">نحوه واریز : </td>                                                         
                                <td><%=EnumUtil.GetEnumElementPersianTitle((BranchOfflineReceiptType)Pay.OfflineReceiptType) %></td>
                            </tr>
                            <tr class="row">
                                <td class="bold">نام واریز کننده : </td>
                                <td><%=Pay.OfflineReceiptLiquidator %></td>
       
                                <td class="bold">شماره فیش : </td>
                                <td><%=Pay.OfflineReceiptNumber %></td>
                            </tr>
                            <tr class="altrow"> 
                                <td class="bold">حساب بانکی : </td>                                                          
                                <td><%=Pay.OfflineBankAccountTitle %></td>
                    
                                <td class="bold">توضیحات حساب بانکی : </td>                                                          
                                <td><%=Pay.OfflineBankAccountDescription %></td>
                            </tr>
                            <tr>
                                <td class="bold">توضیحات : </td>
                                <td><%=Pay.OfflineDescription %></td>

                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>    
                        </table>
                    <%} %>
                    <%if (Pay.Type==(int)BranchPaymentType.Online){ %>
                        <table class="lsttbl" cellpadding="0" cellspacing="0">
                            <tr class="thead" style="text-align:center;">
                                <th colspan="4">جزئیات پرداخت (بصورت آنلاین)</th>
                            </tr>
                            <tr class="row">
                                <td class="bold">مبلغ پرداختی : </td>
                                <td><%=UIUtil.GetCommaSeparatedOf(Pay.Amount)%> ریال</td>
       
                                <td class="bold">تاریخ تراکنش : </td>
                                <td><%=DateUtil.GetPersianDateWithTime( Pay.Date)%></td>
                            </tr>    
                            <tr class="altrow">
                                <td class="bold">رسید دیجیتالی : </td>
                                <td><%=Pay.OnlineRefNum%></td>
        
                                <%  string resultTran = Pay.OnlineTransactionState;
                                if (resultTran == "OK")
                                {
                                    if (Pay.OnlineResultTransaction <= 0)
                                        resultTran = (UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + Math.Abs(Pay.OnlineResultTransaction).ToString())));
                                    else
                                        resultTran = UserMessageKeyBusiness.GetUserMessageDescription(UserMessageKey.OK);
                                }
                                else
                                    resultTran = (EnumUtil.GetEnumElementPersianTitle((OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), (Pay.OnlineTransactionState))));
                                %>

                                <td class="bold">نتیجه تراکنش : </td>
                                <td><%=resultTran %></td>
                            </tr>
                        </table>
                    <%} 
                    
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
                    Response.Redirect(GetRouteUrl("admin-branchpayment", null));
                }%>
        </div>
    </form>
</body>
</html>