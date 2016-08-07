<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Agent_Order_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr runat="server" id="UserID">
        <td class="caption">کد سفارش</td>
        <td class="control"><asp:Label ID="lblOrderID" runat="server"/></td>

    </tr>
    <tr>
        <td class="caption">تاریخ</td>
        <td class="control"><asp:Label ID="lblOrderPersianDate" runat="server"/></td>
    </tr>
    <tr>
        <td class="caption">نام و نام خانوادگی</td>
        <td class="control"><asp:Label ID="lblFullName" runat="server"/></td>
    </tr>
    <tr>
        <td class="caption">پست الکترونیک</td>
        <td class="control"><asp:Label ID="lblEmail" runat="server"/></td>
    </tr>


    <tr>
        <td class="caption">آدرس</td>
        <td class="control"><asp:Label ID="lblAddress" runat="server"/></td>
    </tr>
    <tr>
        <td class="caption">تلفن درج در سفارش</td>
        <td class="control"><asp:Label ID="lblTel" runat="server"/></td>
    </tr>
    <tr>
        <td class="caption">تلفن سفارش دهنده</td>
        <td class="control"><asp:Label ID="lblUserTel" runat="server"/></td>
    </tr>
    <tr>
        <td class="caption">توضیحات</td>
        <td class="control"><asp:Label ID="lblComment" runat="server"/></td>
    </tr>

    <tr>
        <td></td>
        <td>
            <table class="Table" style=" width:630px;">
                <tr class="Header" style="background-color:#AD6868; color:#fff;">
                    <th scope="col">محصول</th>
                    <th scope="col">تعداد</th>
                    <th scope="col">مبلغ</th>
                </tr>
                <asp:ListView ID="lsvOrderDetail" runat="server"  DataKeyNames="ID" EnableViewState="false" >
                    <ItemTemplate>
                <tr class="Row">
                    <td class="Cell"><%#Eval("ProductTitle") %></td>
                    <td class="Cell"><%#Eval("Quantity") %></td>
                    <td class="Cell"><%#Eval("SumPrice")%></td>
                </tr>
                   </ItemTemplate>
                </asp:ListView>
                <tr>
                    <td></td>
                    <td class="caption">مبلغ کل</td>
                    <td class="control"><asp:Label ID="lblTotalAmount" runat="server"/></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="caption">مبلغ قابل پرداخت</td>
                    <td class="control"><asp:Label ID="lblPayableAmount" runat="server"/></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="caption">نحوه پرداخت</td>
        <td class="control"><asp:Label ID="lblPaymentType" runat="server" /></td>
    </tr>
    <%--<tr>--%>
        <%--<td class="caption">وضعیت پرداخت</td>--%>
        <%--<td class="control"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((order.OnlinePayments.Any(g => g.TransactionState == "OK" && g.ResultTransaction > 0 && g.RefNum != "")) ? pgc.Model.Enums.OrderPaymentStatus.Paid : pgc.Model.Enums.OrderPaymentStatus.NotPaid)%></td>--%>
    <%--</tr>--%>
    <tr>

        <td class="caption">وضعیت سفارش</td>
        <td class="control"><kfk:LookupCombo ID="lkcOrderStatus" 
                                    runat="server" 
                                    EnumParameterType="pgc.Model.Enums.OrderStatus"
                                    Required="true" /></td>
    </tr>    
</table>

<div class="commands">
    <asp:Button runat="server" ID="btnOnline" Text="لیست تراکنش های آنلاین" CssClass="xlarge" CausesValidation="false" onclick="btnOnline_Click" />
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

