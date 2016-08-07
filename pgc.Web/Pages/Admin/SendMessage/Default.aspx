<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_TestSendSms_Default" %>

<%@ Register src="../../../UserControls/Project/SendResult.ascx" tagname="SendResult" tagprefix="uc2" %>

<%@ Register src="../../../UserControls/Common/MessageControl.ascx" tagname="MessageControl" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
    <fieldset>
      <legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
        <asp:MultiView runat="server" ID="mlvQS" ActiveViewIndex="0">
        <asp:View runat="server" ID="vSend">
            <div class="fields">
                <table id="SendMessage">
                    <tr>
                        <td class="caption">شماره اختصاصی</td>
                        <td class="control"><kfk:LookupCombo ID="lkcPrivateNo" 
                                                            BusinessTypeName="pgc.Business.Lookup.PrivateNoLookupBusiness"
                                                            runat="server" 
                                                            Required="true" 
                                                            ValidationGroup="QS"/></td>
                    </tr>
                    <tr>
                        <td class="caption">متن</td>
                        <td class="control">
                            <uc3:MessageControl ID="mscBody" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="caption">دریافت کنندگان</td>
                        <td class="control">
                            <kfk:NormalTextBox ID="ntbRecipients" Mode="Phone" CssClass="Msg-tarea" runat="server" TextMode="MultiLine" ValidationGroup="QS"/>
                            <div><span style="padding-right:230px;" >تعداد شماره ها <strong><span id="numCount">0</span></strong></span></div>    
                        </td>
                    </tr>
                    </table>
                </div>
                <div class="commands">
                    <asp:Button ID="btnSend" runat="server" Text="ارسال" CssClass="large" onclick="btnSend_Click" UseSubmitBehavior="False"/>
                    <asp:Button ID="BtnReload" runat="server" Text="انصراف" CssClass="large" 
                        CausesValidation="false" onclick="BtnReload_Click" />    
                </div>
        </asp:View>
        <asp:View runat="server" ID="vRes">

            <div class="fields">
                <table cellpadding="1" cellspacing="0">
                    <tr>
                        <td>
                            <uc2:SendResult ID="snrResult" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="commands">
                <asp:Button ID="cmdNewSend" runat="server" Text="ارسال پیامک جدید" CssClass="dtButton" onclick="cmdNewSend_Click" Width="150" />
                <asp:Button ID="cmdSentMessages" runat="server" Text="لیست پیامک های ارسالی" 
                    CssClass="dtButton"  Width="200" onclick="cmdSentMessages_Click"/>
            </div>
            <div class="dtspacer"></div>

        </asp:View>
    </asp:MultiView>
</fieldset>


<script language="javascript" type="text/javascript">
    function rel(text) {
        text = text.replace(/ /g, '').trim();
        text = text.replace(/\r/gi, '');
        text = text.split('\n');
        count1 = text.length;
        for (x = 0; x < count1; x++) {
            if (text[x].match(/[\S]/g) == null) text[x] = ''; else text[x] = text[x];
        }
        text = text.join('\n').replace(/\n{2,}/g, '\n');
        return text;
    }
    function showLineCount() {
        str = rel($('#<%=ntbRecipients.TextBoxControl.ClientID%>').val());

        if (str == '') { $('#numCount').html('0'); }
        else { $('#numCount').html(str.split('\n').length); }
    }
    function OnContentDocumentsReady() {
        showLineCount();
        $('#<%=ntbRecipients.TextBoxControl.ClientID%>').keyup(function () { showLineCount(); });
        $('#<%=ntbRecipients.TextBoxControl.ClientID%>').change(function () { showLineCount(); });
    }
//    function OnContentPageLoad() {
//        showLineCount();
//        $('#<%=ntbRecipients.TextBoxControl.ClientID%>').keyup(function () { showLineCount(); });
//        $('#<%=ntbRecipients.TextBoxControl.ClientID%>').change(function () { showLineCount(); });
//    }
    OnContentDocumentsReady();

//    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
//        OnContentDocumentsReady();
//    });



</script>
</asp:Content>

