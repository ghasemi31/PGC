<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchTransaction_Detail" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>

</fieldset>

<fieldset>
    <legend class="legend">توضیحات تراکنش</legend>
    <table class="Table">
        <tr>
            <td class="caption">نوع تراکنش</td>
            <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchTransactionType)branchTransaction.TransactionType) %></td>
        </tr>
        <tr>
            <td class="caption">تاریخ درج</td>
            <td class="control"><%=DateUtil.GetPersianDateWithTime(branchTransaction.RegDate) %></td>
        </tr>
        <tr>
            <td class="caption">بدهکاری شعبه</td>
            <td class="control"><%=UIUtil.GetCommaSeparatedOf(branchTransaction.BranchDebt)+" ریال" %></td>
        </tr>
        <tr>
            <td class="caption">بستانکاری شعبه</td>
            <td class="control"><%=UIUtil.GetCommaSeparatedOf(branchTransaction.BranchCredit)+" ریال" %></td>
        </tr>
        <tr>
            <td class="caption">توضیحات</td>
            <td class="control"><%=branchTransaction.Description %></td>
        </tr>
    </table>
</fieldset>

<%if (branchTransaction.TransactionType == (int)BranchTransactionType.BranchPayment && branchPay.Type == (int)BranchPaymentType.Offline){ %>
    <fieldset class="fieldsetDetail">
        <legend class="legend lwrapper">جزئیات پرداخت شعبه (بصورت آفلاین)</legend>        
            <table class="wrapper">        
                <tr>
                    <td class="caption">مبلغ پرداختی</td>
                    <td class="control"><%=UIUtil.GetCommaSeparatedOf(branchPay.Amount) %> ریال</td>
        
                    <td class="caption">تاریخ واریز</td>
                    <td class="control"><%=branchPay.OfflineReceiptPersianDate %></td>
                </tr>
                <tr>            
                    <td class="caption">وضعیت</td>
                    <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchOfflinePaymentStatus)branchPay.OfflinePaymentStatus)%></td>

                    <td class="caption">نحوه واریز</td>                                                         
                    <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchOfflineReceiptType)branchPay.OfflineReceiptType) %></td>
                </tr>
                <tr>
                    <td class="caption">نام واریز کننده</td>
                    <td class="control"><%=branchPay.OfflineReceiptLiquidator%></td>
       
                    <td class="caption">شماره فیش</td>
                    <td class="control"><%=branchPay.OfflineReceiptNumber%></td>
                </tr>
                <tr>
                    <td class="caption">حساب بانکی</td>                                                          
                    <td class="control"><%=branchPay.OfflineBankAccountTitle%></td>
                    
                    <td class="caption">توضیحات حساب بانکی</td>                                                          
                    <td class="control"><%=branchPay.OfflineBankAccountDescription%></td>
                </tr>
                <tr>
                    <td class="caption">توضیحات</td>
                    <td class="control"><%=branchPay.OfflineDescription%></td>
                </tr>    
            </table>        
    </fieldset>
<%} %>
<%if (branchTransaction.TransactionType == (int)BranchTransactionType.BranchPayment && branchPay.Type == (int)BranchPaymentType.Online){ %>
    <fieldset class="fieldsetDetail">
        <legend class="legend lwrapper">جزئیات پرداخت شعبه (بصورت آنلاین)</legend>
        <table class="wrapper">
            <tr>
                <td class="caption">مبلغ پرداختی</td>
                <td class="control"><%=UIUtil.GetCommaSeparatedOf(branchPay.Amount)%> ریال</td>
       
                <td class="caption">تاریخ تراکنش</td>
                <td class="control"><%=DateUtil.GetPersianDateFull(branchPay.Date)%></td>
            </tr>    
            <tr>
                <td class="caption">رسید دیجیتالی</td>
                <td class="control"><%=branchPay.OnlineRefNum%></td>
        
                <%  string resultTran = branchPay.OnlineTransactionState;
                if (resultTran == "OK")
                {
                    if (branchPay.OnlineResultTransaction <= 0)
                        resultTran = (UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + Math.Abs(branchPay.OnlineResultTransaction).ToString())));
                    else
                        resultTran = UserMessageKeyBusiness.GetUserMessageDescription(UserMessageKey.OK);
                }
                else
                    resultTran = (EnumUtil.GetEnumElementPersianTitle((OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), (branchPay.OnlineTransactionState))));
                %>

                <td class="caption">نتیجه تراکنش</td>
                <td class="control"><%=resultTran %></td>
            </tr>
        </table>
    </fieldset>
<%} %>
<%if ( branchTransaction.TransactionType == (int)BranchTransactionType.CustomerOnline ){ %>
    <fieldset class="fieldsetDetail">
        <legend class="legend lwrapper">جزئیات پرداخت سفارش مشتری (بصورت آنلاین)</legend>
        <table class="wrapper">
            <tr>
                <td class="caption">مبلغ پرداختی</td>
                <td class="control"><%=UIUtil.GetCommaSeparatedOf(customerPay.Amount)%> ریال</td>
       
                <td class="caption">تاریخ تراکنش</td>
                <td class="control"><%=DateUtil.GetPersianDateFull(customerPay.Date)%></td>
            </tr>    
            <tr>
                <td class="caption">رسید دیجیتالی</td>
                <td class="control"><%=customerPay.RefNum%></td>
        
                <%  string resultTran = customerPay.TransactionState;
                if (resultTran == "OK")
                {
                    if (customerPay.ResultTransaction <= 0)
                        resultTran = (UserMessageKeyBusiness.GetUserMessageDescription((UserMessageKey)Enum.Parse(typeof(UserMessageKey), "Err_" + Math.Abs(customerPay.ResultTransaction).ToString())));
                    else
                        resultTran = UserMessageKeyBusiness.GetUserMessageDescription(UserMessageKey.OK);
                }
                else
                    resultTran = (EnumUtil.GetEnumElementPersianTitle((OnlineTransactionStatus)Enum.Parse(typeof(OnlineTransactionStatus), (customerPay.TransactionState))));
                %>

                <td class="caption">نتیجه تراکنش</td>
                <td class="control"><%=resultTran %></td>
            </tr>
        </table>
        <div class="detailtbl">            
            <span class="detailistttl">لیست سفارشات</span>
            <table>
                <thead>
                    <th>ردیف</th>
                    <th>نام غذا</th>
                    <th>تعداد</th>
                    <th>مبلغ واحد</th>
                    <th>جمع کل</th>
                </thead>
                <%int rowNumber = 0; %>
                <% foreach (var item in customerPay.Order.OrderDetails){%>
                    <%rowNumber++; %>
                    <tr>
                        <td style="text-align:center;min-width:20px;"><%=rowNumber %></td>
                        <td><%=item.ProductTitle %></td>
                        <td><%=item.Quantity %></td>
                        <td><%=UIUtil.GetCommaSeparatedOf(item.UnitPrice) %> ریال</td>
                        <td><%=UIUtil.GetCommaSeparatedOf(item.SumPrice) %> ریال</td>
                    </tr>
		 
	            <%}%>
                <tr class="footerRow">
                    <td colspan="4">مبلغ سفارش</td>
                    <td colspan="1"><%=UIUtil.GetCommaSeparatedOf(customerPay.Order.TotalAmount)%> ریال</td>
                </tr>
            </table>
        </div>
    </fieldset>
<%} %>
<%if ( branchTransaction.TransactionType == (int)BranchTransactionType.BranchOrder ){ %>
    <fieldset class="fieldsetDetail">
        <legend class="legend lwrapper">فاکتور درخواست شعبه <%=branchTransaction.Branch.Title %></legend>
        <table>
            <tr>        
                <td class="caption">کد درخواست</td>
                <td class="control"><%=branchOrder.ID %></td>

                <td class="caption">تاریخ درخواست</td>
                <td class="control"><%=DateUtil.GetPersianDate(DateUtil.GetEnglishDateTime(branchOrder.OrderedPersianDate)) %></td>
            </tr>
            <tr>        
                <td class="caption">توضیحات شعبه</td>
                <td class="control"><%=branchOrder.BranchDescription%></td>
        
                <td class="caption">توضیحات مدیر</td>
                <td class="control"><%=branchOrder.AdminDescription%></td>
            </tr>                       
        </table>
        <div runat="server" id="detailListOrder" class="detailtbl">            
            <span class="detailistttl">لیست کالاهای سفارشی</span>
        </div>
    </fieldset>
<%} %>
<%if ( branchTransaction.TransactionType == (int)BranchTransactionType.BranchLackOrder ){ %>
    <fieldset class="fieldsetDetail">
        <legend class="legend lwrapper">فاکتور کسری درخواست <%=branchTransaction.Branch.Title %></legend>
        <table>
            <tr>
                <td class="caption">کد کسری</td>
                <td class="control"><%=branchLackOrder.ID %></td>

                <td class="caption">کد درخواست</td>
                <td class="control"><%=branchLackOrder.BranchOrder_ID %></td>
            </tr>
            <tr>        
                <td class="caption">وضعیت کسری</td>
                <td class="control"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.BranchReturnOrderStatus)branchReturn.Status)%></td>

                <td class="caption">تاریخ درخواست</td>
                <td class="control"><%=DateUtil.GetPersianDate(branchLackOrder.RegDate)%></td>
            </tr>
            <tr>        
                <td class="caption">توضیحات شعبه</td>
                <td class="control"><%=branchReturn.BranchDescription%></td>
        
                <td class="caption">توضیحات مدیر</td>
                <td class="control"><%=branchReturn.AdminDescription%></td>
            </tr>                       
        </table>
        <div runat="server" id="detailListLackOrder" class="detailtbl big">            
            <span class="detailistttl">لیست کسورات کالاها</span>
        </div>
    </fieldset>
<%} %>
<%if ( branchTransaction.TransactionType == (int)BranchTransactionType.BranchReturnOrder ){ %>
    <fieldset class="fieldsetDetail">
        <legend class="legend lwrapper">فاکتور مرجوعی <%=branchTransaction.Branch.Title %></legend>
        <table>
            <tr>
                <td class="caption">کد مرجوعی</td>
                <td class="control"><%=branchReturn.ID %></td>

                <td class="caption">تاریخ مرجوعی</td>
                <td class="control"><%=DateUtil.GetPersianDate(branchReturn.RegDate)%></td>
            </tr>
            <tr>        
                <td class="caption">وضعیت درخواست</td>
                <td class="control"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.BranchReturnOrderStatus)branchReturn.Status)%></td>
            </tr>
            <tr>        
                <td class="caption">توضیحات شعبه</td>
                <td class="control"><%=branchReturn.BranchDescription%></td>
        
                <td class="caption">توضیحات مدیر</td>
                <td class="control"><%=branchReturn.AdminDescription%></td>
            </tr>                       
        </table>
        <div runat="server" id="detailListReturnOrder" class="detailtbl big">            
            <span class="detailistttl">کالاهای مرجوعی</span>
        </div>
    </fieldset>
<%} %>

<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" Visible="false" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" />    
</div>

