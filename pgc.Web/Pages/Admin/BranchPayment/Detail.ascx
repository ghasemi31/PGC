<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchPayment_Detail" %>
<%@ Import Namespace="kFrameWork.Util" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%--<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>--%>
</fieldset>
<fieldset>
    <legend class="legend">اطلاعات شعبه</legend>
    <table class="wrapper">     
        <tr>
            <td class="caption">نام </td>
            <td class="control"><%=Pay.Branch.Title%></td>
    
            <td class="caption">تلفن</td>
            <td class="control"><%=Pay.Branch.PhoneNumbers.Replace("\n","-")%></td>
        </tr>
        <tr>
            <td class="caption">اعتبار</td>
            <td class="control"><%=UIUtil.GetCommaSeparatedOf(BranchCreditBusiness.GetBranchCredit(Pay.Branch_ID))%> ریال</td>        
        </tr>
    </table>
</fieldset>

<%if (Pay.Type == (int)BranchPaymentType.Offline)
  { %>
    <fieldset>
        <legend class="legend lwrapper">جزئیات پرداخت (بصورت آفلاین)</legend>
        <%if (Pay.OfflinePaymentStatus != (int)BranchOfflinePaymentStatus.Paid)
          { %>
            <table class="wrapper">        
                <tr>
                    <td class="caption">مبلغ پرداختی</td>
                    <td class="control"><kfk:NumericTextBox ID="nmrPrice" runat="server" Required="true" UnitText="ریال" /></td>
        
                    <td class="caption">تاریخ واریز</td>
                    <td class="control"><kfk:PersianDatePicker ID="pdpPayDate" runat="server" Required="true" /></td>
                </tr>
                <tr>            
                    <td class="caption">وضعیت</td>
                    <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchOfflinePaymentStatus)Pay.OfflinePaymentStatus)%></td>

                    <td class="caption">نحوه واریز</td>                                                         
                    <td class="control"><kfk:LookupCombo ID="lkpRcieptType" runat="server" EnumParameterType="pgc.Model.Enums.BranchOfflineReceiptType" /></td>
                </tr>
                <tr>
                    <td class="caption">نام واریز کننده</td>
                    <td class="control"><kfk:NormalTextBox ID="txtRecieptName" runat="server" Required="true" /></td>
       
                    <td class="caption">شماره فیش</td>
                    <td class="control"><kfk:NormalTextBox ID="txtRecieptNumber" runat="server" Required="true" /></td>
                </tr>
                <tr>
                    <td class="caption">حساب بانکی</td>                                                          
                    <td class="control" colspan="3"><kfk:LookupCombo CssClass="lkpbank" ID="lkpBankAccount" runat="server" BusinessTypeName="pgc.Business.Lookup.BankAccountLookupBusiness" /></td>
                </tr>
                <tr>
                    <td class="caption">توضیحات</td>
                    <td class="control"><kfk:NormalTextBox ID="txtOfflineDesc" runat="server" TextMode="MultiLine" /></td>
                </tr>    
            </table>
        <%}
          else
          { %>
            <table class="wrapper">        
                <tr>
                    <td class="caption">مبلغ پرداختی</td>
                    <td class="control"><%=UIUtil.GetCommaSeparatedOf(Pay.Amount)%> ریال</td>
        
                    <td class="caption">تاریخ واریز</td>
                    <td class="control"><%=DateUtil.GetPersianDateWithTime(DateUtil.GetEnglishDateTime(Pay.OfflineReceiptPersianDate))%></td>
                </tr>
                <tr>            
                    <td class="caption">وضعیت</td>
                    <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchOfflinePaymentStatus)Pay.OfflinePaymentStatus)%></td>

                    <td class="caption">نحوه واریز</td>                                                         
                    <td class="control"><%=EnumUtil.GetEnumElementPersianTitle((BranchOfflineReceiptType)Pay.OfflineReceiptType)%></td>
                </tr>
                <tr>
                    <td class="caption">نام واریز کننده</td>
                    <td class="control"><%=Pay.OfflineReceiptLiquidator%></td>
       
                    <td class="caption">شماره فیش</td>
                    <td class="control"><%=Pay.OfflineReceiptNumber%></td>
                </tr>
                <tr>
                    <td class="caption">حساب بانکی</td>                                                          
                    <td class="control"><%=Pay.OfflineBankAccountTitle%></td>
                    
                    <td class="caption">توضیحات حساب بانکی</td>                                                          
                    <td class="control"><%=Pay.OfflineBankAccountDescription%></td>
                </tr>
                <tr>
                    <td class="caption">توضیحات</td>
                    <td class="control"><%=Pay.OfflineDescription%></td>
                </tr>    
            </table>
        <%} %>
    </fieldset>
<%} %>
<%if (Pay.Type == (int)BranchPaymentType.Online)
  { %>
    <fieldset>
        <legend class="legend lwrapper">جزئیات پرداخت (بصورت آنلاین)</legend>
        <table class="wrapper">
            <tr>
                <td class="caption">مبلغ پرداختی</td>
                <td class="control"><%=UIUtil.GetCommaSeparatedOf(Pay.Amount)%> ریال</td>
       
                <td class="caption">تاریخ تراکنش</td>
                <td class="control"><%=DateUtil.GetPersianDateWithTime(Pay.Date)%></td>
            </tr>    
            <tr>
                <td class="caption">رسید دیجیتالی</td>
                <td class="control"><%=Pay.OnlineRefNum%></td>
        
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

                <td class="caption">نتیجه تراکنش</td>
                <td class="control"><%=resultTran%></td>
            </tr>
        </table>
    </fieldset>
<%} %>

<div class="commands">    
    
    <a href="<%=ResolveUrl("~/Pages/Admin/Prints/BranchPaymentDetail.aspx?id="+Pay.ID.ToString() )%>" target="_blank" class="printbtn btnfinance" >چاپ جزئیات</a>
    
    <%if (Pay.Type == (int)BranchPaymentType.Offline)
      { %>
        <%if (Pay.OfflinePaymentStatus != (int)BranchOfflinePaymentStatus.Paid)
          {%>
            <asp:Button runat="server" ID="Button1" Text="ذخیره و تایید پرداخت" CssClass="large" onclick="Con_Click" />
            <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
        <%} %>
        <%if (Pay.OfflinePaymentStatus != (int)BranchOfflinePaymentStatus.NotPaid)
          {%>
            <asp:Button runat="server" ID="Button2" Text="لغو پرداخت" CssClass="large" onclick="UnCon_Click" />
        <%} %>
        <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
    <%}
      else
      { %>
        <asp:Button runat="server" ID="Button3" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
    <%} %>
    
</div>

