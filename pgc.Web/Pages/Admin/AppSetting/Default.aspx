<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_Option_Default" ValidateRequest="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
    <asp:ScriptManager  ScriptMode="Release" ID="scmMain" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="uplMain" runat="server">
        <ContentTemplate>
            <fieldset>
                <legend>تنظیمات اپلیکیشن اندروید</legend>
                <div class="params">
                   <table>
    <tr>
        <td class="caption">متن منشور کیفیت</td>
       <td class="control" colspan="2"><kfk:NormalTextBox ID="txtQualityCharter" runat="server" TextMode="MultiLine" CssClass="large" TextBoxWidth="400" /></td>      
    </tr>
    <tr>    
        <td class="caption">تصویر NavigationDrawer</td>
         <td class="control" colspan="2">
         <kfk:FileUploader ID="fupNavImage" runat="server" SaveFolder="~/UserFiles/MobileApplication/Android/" />
             </td>
    </tr>   
    <tr>
        <td class="caption">تعداد اخبار</td>
        <td class="control" colspan="2">
          <kfk:NumericTextBox ID="txtNewsCnt" runat="server" SupportComma="false" SupportLetter="false" TextBoxWidth="50" />
            </td>
    </tr>
                           <tr>    
        <td class="caption">تصویر شرایط نمایندگی</td>
         <td class="control" colspan="2">
         <kfk:FileUploader ID="fupBranchImg" runat="server" SaveFolder="~/UserFiles/MobileApplication/Android/" />
             </td>
    </tr> 

                       <tr>
        <td class="caption">محتوای شرایط نمایندگی</td>
        <td class="control" colspan="2"><kfk:HtmlEditor ID="txtBranchContent" runat="server" Required="true" TextBoxWidth="400"/></td>
        <td></td>
    </tr>
  <tr>
        <td class="caption">عرض و طول جغرافیایی مستر دیزی<a href="/assets/global/images/locationsHelp.png" target="_blank">
            <i class="fa fa-question-circle" aria-hidden="true" style="font-size:1.5em;color:#2196F3"></i>
         
                                                         </a></td>
       <td class="control" colspan="2"><kfk:NormalTextBox ID="txtpgciziLatLng" runat="server" TextMode="MultiLine" CssClass="xxlarge" TextBoxWidth="400"/></td>      
    </tr>
</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
</div>
                   
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    

</asp:Content>

