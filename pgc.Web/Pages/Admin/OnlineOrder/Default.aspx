<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_OnlineOrder_Default" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
    <fieldset>
        <legend>پذیرش سفارش بصورت آنلاین</legend>
        <table class="tblmain">
            <tr>
                <td class="caption" >وضعیت پذیرش:</td>
                <td class="control" colspan="3">
                    <asp:RadioButtonList runat="server" ID="rdbList">
                        <asp:ListItem Text="تعلیق کامل سفارش آنلاین" Value="false" />
                        <asp:ListItem Text="مجاز به دریافت سفارش آنلاین" Value="true" />
                    </asp:RadioButtonList>
                </td>                                
            </tr>
            <tr class="msg">
                <td class="caption" colspan="2">پیام نمایشی در حالت تعلیق:</td>
                <td class="control" colspan="2"><kfk:NormalTextBox ID="txtMsg" runat="server" TextMode="MultiLine" Required="true" /></td>
            </tr>
            <tr class="msg">
                <td colspan="4">
                    <div class="commands">
                        <asp:Button id="Button1" runat="server" onclick="btnSave_Click" UseSubmitBehavior="true" Text="ذخیره" CssClass="large" />
                    </div>
                </td>
            </tr>
            
            <tr class="border">
                <td colspan="4"></td>
            </tr>            
            <tr>
                <td class="caption" colspan="4">لیست محصولات:</td>
            </tr>        
            <tr>
                <td class="control" colspan="4">
                    <asp:CheckBoxList runat="server" ID="chbProductList" CssClass="chblst"></asp:CheckBoxList>
                </td>
            </tr>

            <tr class="border">
                <td colspan="4"></td>
            </tr>    
            <tr>
                <td class="caption" colspan="4">لیست شعب:</td>
            </tr>        
            <tr>
                <td class="control brnchlst" colspan="4">
                    <asp:ListView runat="server" ID="lstBranch">
                        <ItemTemplate>
                            <div>
                                <asp:CheckBox Text="text" runat="server" ID="chbAllow" />                                
                                <span>از ساعت:</span>
                                <kfk:TimePicker runat="server" ID="timeFrom" />
                                <span>تا ساعت:</span>
                                <kfk:TimePicker runat="server" ID="timeTo" />
                            </div>        
                        </ItemTemplate>
                    </asp:ListView>
                    <%--<asp:CheckBoxList runat="server" ID="chbBranchList" CssClass="chblst"></asp:CheckBoxList>--%>
                </td>
            </tr>
        </table>
        <div class="commands">
            <asp:Button id="btnSave" runat="server" onclick="btnSave_Click" UseSubmitBehavior="true" Text="ذخیره" CssClass="large" />
        </div>
    </fieldset>
</asp:Content>

