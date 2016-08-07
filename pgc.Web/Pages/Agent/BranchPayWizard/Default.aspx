<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Agent_BranchPayWizard_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" ScriptMode="Release" runat="server" AsyncPostBackTimeout="0"></asp:ScriptManager>
    <asp:UpdatePanel ID="uplMain" runat="server">
        <ContentTemplate>
            <asp:MultiView ActiveViewIndex="0" runat="server" ID="mView">
                <asp:View runat="server" ID="vFirst">                                        
                    <fieldset>
                        <legend>افزایش اعتبار شعبه</legend>                                                                                                        
                        <div class="desc"><%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.BranchPayWizardDescription) %></div>
                        <div>اعتبار <% long credit=pgc.Business.BranchCreditBusiness.GetBranchCredit(kFrameWork.UI.UserSession.User.Branch_ID.Value);
                                        bool isNegative = (credit < 0);%>
                                        <%=kFrameWork.UI.UserSession.User.Branch.Title %> : <%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(Math.Abs(credit))%><%=isNegative?"<span style='color:red;'>-</span>":"" %> ریال</div>
                        <div class="commands">
                            <asp:Button ID="btnOnline1" Text="پرداخت آنلاین (از طریق درگاه الکترونیکی بانک سامان)" runat="server" onclick="online1click" />
                            <%if (kFrameWork.Business.OptionBusiness.GetBoolean(pgc.Model.Enums.OptionKey.BranchPaymentOfflineAvaileble)){ %>
                                <asp:Button ID="btnOffline1" Text="ثبت اطلاعات فیش وجه واریزی" runat="server" onclick="offline1click" />                    
                            <%} %>
                        </div>
                    </fieldset>
                </asp:View>
                <asp:View ID="vOnline" runat="server">
                    <fieldset>
                        <legend>افزایش اعتبار شعبه (پرداخت آنلاین)</legend>
                        <div class="desc"><%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.BranchPayWizardOnlineDescription) %></div>
                        <div>اعتبار <%=kFrameWork.UI.UserSession.User.Branch.Title %> : <%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(pgc.Business.BranchCreditBusiness.GetBranchCredit(kFrameWork.UI.UserSession.User.Branch_ID.Value))%> ریال</div>
                        <div>مبلغ <kfk:NumericTextBox ID="nmrPriceOnline" runat="server" Required="true" UnitText="ریال" SupportComma="true" /></td>
                        <div class="commands">                            
                            <asp:Button ID="Button3" Text="ارسال به درگاه پرداخت آنلاین" runat="server" onclick="online1go_Click" UseSubmitBehavior="true" />
                            <asp:Button ID="Button2" Text="بازگشت" runat="server" onclick="online1Back_Click" UseSubmitBehavior="false" CausesValidation="false" />
                        </div>
                    </fieldset>
                </asp:View>
                <asp:View ID="vOffline" runat="server">
                    <fieldset>
                    <legend>اطلاعات فیش وجه واریزی</legend>
                        <div class="desc"><%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.BranchPayWizardOfflineDescription) %></div>                    
                        <table class="Table">
                            <tr>
                                <td class="caption">به حساب </td>                                                  
                                <td class="control" colspan="3"><kfk:LookupCombo runat="server" ID="lkpBankAccount" BusinessTypeName="pgc.Business.Lookup.BankAccountLookupBusiness" CssClass="banklkp" /></td>
                            </tr>        
                            <tr>
                                <td class="caption">مبلغ </td>
                                <td class="control"><kfk:NumericTextBox ID="nmrOfflinePrice" runat="server" Required="true" UnitText="ریال" SupportComma="true" /></td>
                            
                                <td class="caption">واریز کننده </td>
                                <td class="control"><kfk:NormalTextBox runat="server" ID="txtReceiptLiquidator" Required="true" /></td>
                            </tr>                                                                               
                            <tr>        
                                <td class="caption">شماره رسید </td>
                                <td class="control"><kfk:NormalTextBox runat="server" ID="txtReceiptNumber" Required="true" /></td>
                            
                                <td class="caption">نحوه واریز</td>                                                                                                  
                                <td class="control"><kfk:LookupCombo runat="server" ID="lkpRecieptType" EnumParameterType="pgc.Model.Enums.BranchOfflineReceiptType" /></td>
                            </tr>
                            <tr>        
                                <td class="caption">تاریخ واریز</td>
                                <td class="control"><kfk:PersianDatePicker runat="server" ID="pdpReceiptPersianDate" Required="true" /></td>                                                                                                    

                                <td class="caption">توضیحات <br />(در صورت تمایل)</td>
                                <td class="control"><kfk:NormalTextBox runat="server" ID="txtOfflineDesc" TextMode="MultiLine" /></td>
                            </tr>                                                           
                        </table>
                        <div class="commands">
                            <asp:Button ID="btnSaveOffline" runat="server" Text="ثبت واریز" CausesValidation="true" onclick="btnSaveOffline_Click" UseSubmitBehavior="true" />
                            <asp:Button ID="btnofflineBack" runat="server" Text="بازگشت" CausesValidation="false" onclick="btnofflineBack_Click" UseSubmitBehavior="false" />
                        </div>
                    </fieldset>
                </asp:View>                
            </asp:MultiView>              
        </ContentTemplate>
    </asp:UpdatePanel> 
</asp:Content>

