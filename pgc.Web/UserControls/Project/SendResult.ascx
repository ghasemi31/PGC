<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SendResult.ascx.cs" Inherits="UserControls_Project_SendResult" %>
<div class="sendresult">
    <div class="countresult">
        <div class="title">نتیجه تعدادی ارسال</div>
        <div class="resultdetail"  style="display:<%=(lblRes_SentMessagesCount.Text =="" || lblRes_SentMessagesCount.Text =="0")?"none":"" %>" title="با موفقیت ارسال شدند ، در بانک داده ذخیره شدند.">
            <div class="succeed"></div>
            <asp:Label runat="server" ID="lblRes_SentMessagesCount" Font-Bold="True" ></asp:Label>
            <label>پیامک موفق </label>
        </div>
        <div class="resultdetail" style="display:<%=(lblRes_FailedMessagesCount.Text =="" || lblRes_FailedMessagesCount.Text =="0")?"none":"" %>" title="ارسال پیامک ها از جانب مگفا با مشکل مواجه شده اند ، در بانک داده ذخیره شدند.">
            <div class="fail"></div>
            <asp:Label runat="server" ID="lblRes_FailedMessagesCount" Font-Bold="True"></asp:Label>
                <label> پیامک نا موفق </label>
        </div>
        <div class="resultdetail" style="display:<%=(lblRes_UnknownMessagesCount.Text =="" || lblRes_UnknownMessagesCount.Text =="0")?"none":"" %>" title="ارسال یا عدم ارسال آنها مشخص نیست ، در بانک داده ذخیره شدند.">
            <div class="unknown"></div>
            <asp:Label runat="server" ID="lblRes_UnknownMessagesCount" Font-Bold="True"></asp:Label>
                <label> پیامک نا مشخص </label>
        </div>
                <div class="resultdetail" style="display:<%=(lblRes_ErrorMessagesCount.Text =="" || lblRes_ErrorMessagesCount.Text =="0")?"none":"" %>" title="ارسال پیامک ها با خطا مواجه شده است ، در بانک داده ذخیره نشده اند.">
                <div class="error"></div>
                <asp:Label runat="server" ID="lblRes_ErrorMessagesCount" Font-Bold="True"></asp:Label>
                <label> پیامک خطا دار </label>
        </div>
        <div class="sum">
            مجموع پیامک های درخواست شده : 
            <asp:Label runat="server" ID="lblRes_TotalMessagesCount" Font-Bold="True"></asp:Label>
        </div>
    </div>
<%--    <div class="financeresult">
        <div class="title">نتیجه مالی ارسال</div>
        <div class="resultdetail">
            <label>مجموع شارژ کسر شده : </label>
            <asp:Label runat="server" ID="lblRes_ChargeAmount" ForeColor="#bf7a78" 
                    Font-Bold="True"></asp:Label>
        </div>
        <div class="resultdetail">
            <label>موجودی فعلی تعرفه تعیین شده : </label>
            <asp:Label runat="server" ID="lblRes_CurrentBalance" Font-Bold="True"></asp:Label>
        </div>
        <div class="sum">&nbsp;</div>
    </div>--%>
</div>