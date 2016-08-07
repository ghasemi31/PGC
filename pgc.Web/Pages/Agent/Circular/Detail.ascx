<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Agent_Circular_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>

    <tr>
        <td class="caption">عنوان</td>
        <td class="control">
            <%=circular.Title %>
        </td>
    </tr>
    <tr>
        <td class="caption">تاریخ دریافت</td>
        <td class="control">
            <%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(circular.Date) %>
        </td>
    </tr>
    <tr>
        <td class="caption">متن بخشنامه</td>
       
    </tr>
    <tr>

         <td colspan="4">
           <%=circular.Body %>
        </td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>
