<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SendEmailResult.ascx.cs" Inherits="UserControls_Project_SendEmailResult" %>
<div class="sendresult">
    <div class="countresult">
        <div class="title">نتیجه تعدادی ارسال</div>
        <div class="resultdetail"  style="display:<%=(lblRes_SentEmailCount.Text =="" || lblRes_SentEmailCount.Text =="0")?"none":"" %>" title="با موفقیت ارسال و در بانک داده ذخیره شدند.">
            <div class="succeed"></div>
            <asp:Label runat="server" ID="lblRes_SentEmailCount" Font-Bold="True" ></asp:Label>
            <label>ایمیل ارسال شد </label>
        </div>
        <div class="resultdetail"  style="display:<%=(lblRes_SentBlockCount.Text =="" || lblRes_SentBlockCount.Text =="0")?"none":"" %>" title="با موفقیت ارسال و در بانک داده ذخیره شدند.">
            <div class="succeed"></div>
            <asp:Label runat="server" ID="lblRes_SentBlockCount" Font-Bold="True" ></asp:Label>
            <label>بسته ارسال شد </label>
        </div>
        <div class="resultdetail"  style="display:<%=(lblRes_FailedEmailCount.Text =="" || lblRes_FailedEmailCount.Text =="0")?"none":"" %>" title="ارسال نشده ولی در بانک داده ذخیره شدند.">
            <div class="fail"></div>
            <asp:Label runat="server" ID="lblRes_FailedEmailCount" Font-Bold="True" ></asp:Label>
            <label>ایمیل ارسال نشد </label>
        </div>
        <div class="resultdetail"  style="display:<%=(lblRes_FailedBlockCount.Text =="" || lblRes_FailedBlockCount.Text =="0")?"none":"" %>" title="ارسال نشده ولی در بانک داده ذخیره شدند.">
            <div class="fail"></div>
            <asp:Label runat="server" ID="lblRes_FailedBlockCount" Font-Bold="True" ></asp:Label>
            <label>بسته ارسال نشد </label>
        </div>        
        <div class="resultdetail"  style="display:<%=(lblRes_InvalidMailCount.Text =="" || lblRes_InvalidMailCount.Text =="0")?"none":"" %>" title="آدرس های نا معتبر بوده و هیچ ارسالی برای آنها صورت نپذیرفته است.">
            <div class="unknown"></div>
            <asp:Label runat="server" ID="lblRes_InvalidMailCount" Font-Bold="True" ></asp:Label>
            <label>آدرس های نامعتبر</label>
        </div>
        <div class="sum">
            مجموع ایمیل های ارسالی : 
            <asp:Label runat="server" ID="lblRes_TotalMailCount" Font-Bold="True"></asp:Label>
        </div>
        <div class="sum" style="display:<%=(lblRes_TotalBlockMailCount.Text =="" || lblRes_TotalBlockMailCount.Text =="0")?"none":"" %>">
            مجموع بسته های ارسالی : 
            <asp:Label runat="server" ID="lblRes_TotalBlockMailCount" Font-Bold="True"></asp:Label>
        </div>
        <div class="sum" style="display:<%=(lblRes_BlockSize.Text =="" || lblRes_BlockSize.Text =="0")?"none":"" %>">
            حداکثر سایز هر بسته ارسالی : 
            <asp:Label runat="server" ID="lblRes_BlockSize" Font-Bold="True"></asp:Label>
        </div>
    </div>
</div>