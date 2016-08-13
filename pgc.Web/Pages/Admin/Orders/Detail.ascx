<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_GameOrder_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr runat="server" id="UserID">
        <td class="caption">کد ثبت نام</td>
        <td class="control"><asp:Label ID="lblGameOrderID" runat="server"/></td>

    </tr>

    <tr>
        <td class="caption">تاریخ</td>
        <td class="control"><asp:Label ID="lblGameOrderPersianDate" runat="server"/></td>
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
        <td class="caption">کد ملی </td>
        <td class="control"><asp:Label ID="lblNationalCode" runat="server"/></td>
    </tr>

    <tr>
        <td class="caption">آدرس</td>
        <td class="control"><asp:Label ID="lblAddress" runat="server"/></td>
    </tr>
    
    <tr>
        <td class="caption">تلفن </td>
        <td class="control"><asp:Label ID="lblUserTel" runat="server"/></td>
    </tr>
   
 
       <tr>
        <td class="caption">مبلغ قابل پرداخت</td>
        <td class="control"><asp:Label ID="lblPayableAmount" runat="server"/></td>
    </tr>
    
    <tr>
        <td class="caption">وضعیت پرداخت</td>
        <td class="control"><asp:Label ID="lblGameOrderPaymentStatus" runat="server" /></td>
    </tr>


    

         <tr>
        <td class="caption">نام بازی</td>
        <td class="control"><asp:Label ID="lblGameTitle" runat="server"/></td>
    </tr>

         <tr>
        <td class="caption">نوع بازی</td>
        <td class="control"><asp:Label ID="lblGameType" runat="server"/></td>
    </tr>
    <%if(GameOrder.Group!=null){ %>
     <tr>
        <td class="caption">نام تیم</td>
        <td class="control"><asp:Label ID="lblTeamName" runat="server" /></td>
    </tr>
     <tr>

        <td class="caption">اعضای تیم</td>
        <td>
            <table class="Table" style=" width:630px;">
                <tr class="Header" style="background-color:#AD6868; color:#fff;">
                    <th scope="col">نام بازیکن</th>
                    <th scope="col">ایمیل</th>
                    <th scope="col">کد ملی</th>
                </tr>
                <asp:ListView ID="lsvGroup" runat="server"  DataKeyNames="ID" EnableViewState="false" >
                    <ItemTemplate>
                <tr class="Row">
                    <td class="Cell"><%#Eval("FullName") %></td>
                    <td class="Cell"><%#Eval("Email") %></td>
                    <td class="Cell"><%#Eval("NationalCode")%></td>
                </tr>
                   </ItemTemplate>
                </asp:ListView>
               
              
            </table>
        </td>
    </tr>
    
    <%} %>


    
   
</table>

<div class="commands">
    <asp:Button runat="server" ID="btnOnline" Text="لیست تراکنش های آنلاین" CssClass="xlarge" CausesValidation="false" onclick="btnOnline_Click" />
   
    <asp:Button runat="server" ID="btnCancel" Text="بازگشت" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>



