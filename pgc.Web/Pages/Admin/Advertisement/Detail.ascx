<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_Advertisement_Detail" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
 <table cellpadding="1" cellspacing="0" >
    <tr>
        <td class="caption">نوع آگهی</td>
        <td class="control">
            <asp:DropDownList ID="ddrAdvType" runat="server" style=" width:155px; padding:2px;"
                OnSelectedIndexChanged="ddrAdvType_IndexChanged" AutoPostBack="true">
                <%-- <asp:ListItem Value="0">-- نوع آگهی --</asp:ListItem>--%>
                <asp:ListItem Value="1">عکس</asp:ListItem>
                <asp:ListItem Value="2">فلش</asp:ListItem>
                <asp:ListItem Value="3">html</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="caption">عنوان آگهی</td>
        <td class="control"><kfk:NormalTextBox ID="txtTitle" runat="server" Required="true" /></td>
    </tr>
    <tr>                
        <td class="caption">تاریخ انقضا</td>
        <td class="control"><kfk:PersianDatePicker ID="dpExpirePersianDate" runat="server" Required="true" /></td>
    </tr>
    <tr>
        <td class="caption">اولویت نمایش</td>
        <td class="control"><kfk:NumericTextBox ID="txtDispOrder" runat="server" Required="true"/></td>
    </tr>
    <tr runat="server" id="rowFup">
        <td class="caption" >آدرس فایل</td>
        <td class="control" >
                    
            <asp:FileUpload ID="fupImage" runat="server" />
            <asp:Label ID="lblRequired" runat="server" Text="*" style="color:Red;"></asp:Label>
            <asp:HyperLink ID="hplFile" runat="server"  Visible="false" Target="_blank" style=" text-Decoration:underline"></asp:HyperLink>
            <asp:Button ID="btnDeleteFile" runat="server" Text="حذف فایل" Visible="false" 
                onclick="btnDeleteFile_Click" OnClientClick="if (!confirm('آیا از حذف این فایل اطمینان دارید؟')){return false;}" CausesValidation="False" />
        </td>
    </tr>
    
</table>
<asp:MultiView ID="mlvControls" runat="server">
    <asp:View ID="Image" runat="server">
        <table>
                    
            <tr>
                <td class="caption">عرض</td>
                <td class="control"><kfk:NumericTextBox ID="txtWidth" runat="server"/></td>
            </tr>
            <tr>
                <td class="caption">ارتفاع</td>
                <td class="control"><kfk:NumericTextBox ID="txtHeight" runat="server"/></td>
            </tr>
            <tr>
                <td class="caption">آدرس لینک</td>
                <td class="control"><kfk:NormalTextBox ID="txtNavigateUrl" runat="server" Required="true" /></td>
            </tr>
            <tr>
                <td class="caption">توضیحات عکس</td>
                <td class="control"><kfk:NormalTextBox ID="txtAlt" runat="server"/></td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="Flash" runat="server">
            <table cellpadding="1" cellspacing="0" id="tblFormControl" runat="server">

            <tr>
                <td class="caption">عرض</td>
                <td class="control"><kfk:NumericTextBox ID="txtFlashWidth" runat="server" Required="true"/></td>
            </tr>
            <tr>
                <td class="caption">ارتفاع</td>
                <td class="control"><kfk:NumericTextBox ID="txtFlashHeight" runat="server" Required="true"/></td>
            </tr>
            <tr>
                <td class="caption">توضیحات عکس</td>
                <td class="control"><kfk:NormalTextBox ID="txtFlashAlt" runat="server"/></td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="Html" runat="server">
        <table>
            <tr id="HtmlText" runat="server">
                <td class="caption">کد html</td>
                <td class="control"><kfk:HtmlEditor ID="txtHtml" runat="Server" Required="true"/></td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="empty" runat="server">
    </asp:View>
</asp:MultiView>

<table>
    <tr>
        <td class="caption">
            <label>صفحات</label>
        </td>
        <td style="width:600px;">
            <asp:CheckBoxList ID="cblPages" runat="server" RepeatColumns="4" CellSpacing="3">
            </asp:CheckBoxList>
        </td>
    </tr>

    <tr>
        <td class="caption">تعیین فواصل</td>
        <td>
          <table id="tblmargin">
            <tr>
                <td colspan="3" style="padding-bottom:20px;" >
                    <asp:Label ID="Label5" runat="server" Text="عرض قابل نمایش 900px"></asp:Label>
                    <br />
                    <img src="../../../Styles/images/FleshPic.png" width=500px; />
                    <%--<hr style=" border:2px #a81c1b dashed; width:600px;" />--%>
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="control">
                    <asp:Label ID="Label1" runat="server" Text="فاصله از بالا"></asp:Label><br />
                    px<kfk:NumericTextBox ID="txtMarginTop" runat="server"/>
                </td>
                <td></td>
            </tr>
            <tr>
                <td class="control" style="width:150px;">
                    <asp:Label ID="Label2" runat="server" Text="فاصله از راست"></asp:Label><br />
                    px<kfk:NumericTextBox ID="txtMarginRight" runat="server"/>
                </td>
                <td class="advtd">آگهی</td>
                <td class="control" style="width:150px;">
                    <asp:Label ID="Label3" runat="server" Text="فاصله از چپ"></asp:Label><br />
                    px<kfk:NumericTextBox ID="txtMarginLeft" runat="server"/>
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="control">
                    px<kfk:NumericTextBox ID="txtMarginBottom" runat="server"/><br />
                    <asp:Label ID="Label4" runat="server" Text="فاصله از پایین"></asp:Label>
                </td>
                <td></td>
            </tr>
        </table>
        </td>
    </tr>

</table>
<div class="commands">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>

