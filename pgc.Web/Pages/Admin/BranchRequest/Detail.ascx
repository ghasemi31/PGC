<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_BranchRequest_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
    <tr>
        <td class="caption">وضعیت</td>
        <td class="control"><kfk:LookupCombo ID="lkcStatus" 
                                    runat="server" 
                                    EnumParameterType="pgc.Model.Enums.UserCommentStatus"
                                    Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">تاریخ</td>
        <td class="control"><asp:Label ID="lblBRPersianDate" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">نام و نام خانوادگی</td>
        <td class="control"><asp:Label ID="lblFullname" runat="server"></asp:Label></td>
    </tr>
    <%--<tr>
        <td class="caption">نام</td>
        <td class="control"><asp:Label ID="lblFname" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">نام خانوادگی</td>
        <td class="control"><asp:Label ID="lblLname" runat="server"></asp:Label></td>

    </tr>--%>
    <tr>
        <td class="caption">نام شرکت</td>
        <td class="control"><asp:Label ID="lblAplicatorName" runat="server"></asp:Label></td>

    </tr>
    <tr>
        <td class="caption">سابقه</td>
        <td class="control"><asp:Label ID="lblBackground" runat="server"></asp:Label></td>
    </tr>

    <tr>
        <td class="caption">محل مورد نظر برای تاسیس نمایندگی</td>
        <td class="control"><asp:Label ID="lblLocation" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">نوع ملک</td>
        <td class="control"><asp:Label ID="lblLocationType" runat="server"></asp:Label></td>
    </tr>

    <tr>
        <td class="caption">تلفن</td>
        <td class="control"><asp:Label ID="lblPhone" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">تلفن همراه</td>
        <td class="control"><asp:Label ID="lblMobile" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">ایمیل</td>
        <td class="control"><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">آدرس</td>
        <td class="control"><asp:Label ID="lblAddress" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">توضیحات</td>
        <td class="control"><asp:Label ID="lblDesc" runat="server"></asp:Label></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

