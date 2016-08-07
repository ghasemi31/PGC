<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_UserComment_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table>
   
    <tr>
        <td class="caption">تاریخ</td>
        <td class="control"><asp:Label ID="lblUCPersianDate" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">شعبه مورد نظر</td>
        <td class="control"><kfk:LookupCombo ID="lkcBranch" runat="server"
                                             BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness" 
                                             AddDefaultItem="true"
                                             DefaultItemText="مدیریت"/>
         </td>
    </tr> 
    <tr>    
        <% if (userComment!=null&& userComment.ID>0&& !userComment.Branch_ID.HasValue && !string.IsNullOrEmpty(userComment.BranchTitle)) {%>
            <td class="caption">عنوان شعبه درج شده در پایگاه داده</td>
            <td class="control"><%=userComment.BranchTitle %></td>        
        <% } %>
    </tr> 
    <tr>
        <td class="caption">نوع نظر</td>
        <td class="control"><kfk:LookupCombo ID="lkcType" runat="server" DependOnParameterType="int" DependOnParameterName="Type" EnumParameterType="pgc.Model.Enums.UserCommentType" /></td>
    </tr>
    <tr>
        <td class="caption">وضعیت</td>
        <td class="control"><kfk:LookupCombo ID="lkcStatus" runat="server" DependOnParameterType="int" DependOnParameterName="Status" EnumParameterType="pgc.Model.Enums.UserCommentStatus"  /></td>
    </tr>
     <tr>
        <td class="caption">موضوع</td>
        <td class="control"><asp:Label ID="lblTopic" runat="server"></asp:Label></td>
    </tr>
        
    <tr>
        <td class="caption">نام</td>
        <td class="control"><asp:Label ID="lblName" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td class="caption">ایمیل</td>
        <td class="control"><asp:Label ID="lblEmail" runat="server"></asp:Label></td>
    </tr>
    <tr>
         <td class="caption">متن</td>
        <td class="control"><asp:Label ID="lblBody" runat="server"></asp:Label></td>
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

