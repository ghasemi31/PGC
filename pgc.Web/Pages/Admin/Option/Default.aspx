<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Admin_Option_Default" ValidateRequest="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
    <asp:ScriptManager  ScriptMode="Release" ID="scmMain" runat="server" AsyncPostBackTimeout="0">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="uplMain" runat="server">
        <ContentTemplate>
            <fieldset>
                <legend>پارامتر ها</legend>
                <div class="params">
                    <table cellpadding="0" cellspacing="0" class="main-tbl">
                        <tr>
                            <td class="rightcol" runat="server" id="rightcol">
                                <table cellpadding="1" cellspacing="0" class="optiontreeview">
                                    <tr>
                                        <td>
                                            <div class="tv_container">
                                                <asp:TreeView ID="trvCat" runat="server" ExpandDepth="0" onselectednodechanged="trvCat_SelectedNodeChanged" >
                                                    <LeafNodeStyle ImageUrl="~/Styles/Pages/Admin/Option/images/tv_item.png" CssClass="lns" />
                                                    <NodeStyle ImageUrl="~/Styles/Pages/Admin/Option/images/tv_cat.png" CssClass="ns" />
                                                    <SelectedNodeStyle ImageUrl="~/Styles/Pages/Admin/Option/images/tv_edit.png" CssClass="sns" />
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="leftcol"  runat="server" id="leftcol">
                                <table class="framed" runat="server" id="tblControl" visible="false">
                                    <tr>
                                        <td class="caption">
                                            <asp:Label runat="server" ID="lblParameterName" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="control">
                                            <asp:MultiView runat="server" ID="mlvControl">
                                                <asp:View runat="server" ID="vs_SmallText">
                                                    <kfk:NormalTextBox runat="server" ID="txt_SmallText" MaxLength="100" />
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Text">
                                                    <kfk:NormalTextBox runat="server" ID="txt_Text"  MaxLength="500" TextBoxWidth="250" />
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_LargeText">
                                                    <kfk:NormalTextBox runat="server" ID="txt_LargeText" TextMode="MultiLine"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_NText">
                                                    <kfk:NormalTextBox runat="server" ID="txt_NText" TextMode="MultiLine"  Direction="rtl" TextBoxWidth="370" TextBoxHeight="300"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Html">
                                                    <%--<kfk:NormalTextBox runat="server" ID="txt_Html" TextMode="MultiLine"  Direction="ltr"  TextBoxWidth="370" TextBoxHeight="300"/>--%>
                                                    <%--<kfk:Ckeditor ID="txt_Html" runat="server" BaseHref="~/UserControls/Common/ckeditor" Height="300" />--%>
                                                   
                                                    <kfk:CkHtmlEditor ID="ckHtml" runat="server"  Required="true" /> 
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_SmallInt">
                                                    <kfk:NumericTextBox runat="server" ID="nurSmallInt" SupportComma="true" SupportLetter="true" TextBoxMaxLen="6"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Int">
                                                    <kfk:NumericTextBox runat="server" ID="nur_Int" SupportComma="true" SupportLetter="true" TextBoxMaxLen="12"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_BigInt">
                                                    <kfk:NumericTextBox runat="server" ID="nur_BigInt" SupportComma="true" SupportLetter="true"  TextBoxMaxLen="18"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_TinyInt">
                                                    <kfk:NumericTextBox runat="server" ID="nur_TinyInt" SupportComma="true" SupportLetter="true" TextBoxMaxLen="6"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Double">
                                                    <kfk:NormalTextBox runat="server" ID="hkt_Double" Direction="ltr"  Mode="Numeric_Decimal" MaxLength="12"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Phone">
                                                    <kfk:NormalTextBox runat="server" ID="hkt_Phone" Direction="ltr"  TextBoxWidth="250" Mode="Phone"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Email">
                                                    <kfk:NormalTextBox runat="server" ID="hkt_Email" Direction="ltr"  TextBoxWidth="250" Mode="Email"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Money">
                                                    <kfk:NumericTextBox runat="server" ID="nur_Money" SupportComma="true" SupportLetter="true"  UnitText="ریال"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Boolean">
                                                    <asp:RadioButton ID="rdb_Boolean_True" runat="server" Text="بلی" GroupName="vs_Boolean"/>
                                                    <asp:RadioButton ID="rdb_Boolean_False" runat="server" Text="خیر" GroupName="vs_Boolean"/>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Date">
                                                    <asp:Calendar ID="cln_Date" runat="server"></asp:Calendar>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_PersianDate">
                                                    <kfk:PersianDatePicker runat="server" ID="pdp_PersianDate" />
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Lookup">
                                                    <asp:DropDownList runat="server" ID="cbo_Lookup"></asp:DropDownList>
                                                </asp:View>
                                                <asp:View runat="server" ID="vs_Time">
                                                    <kfk:TimePicker runat="server" ID="tp_time" />
                                                </asp:View>
                                                 <asp:View runat="server" ID="vs_FilePath">
                                                    <kfk:FileUploader runat="server" ID="fup_Path" SaveFolder="~/Userfiles/" />
                                                </asp:View>
                                            </asp:MultiView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="commands">
                                                <asp:Button ID="btnSave" runat="server" CausesValidation="true" CssClass="dtButton" OnClick="OnSave" Text="ذخیره" Width="100"/>
                                                <%--<asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="dtButton" OnClick="OnCancel" Text="لغو" Width="100"/>--%>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    

    <div style="display:none">
        <kfk:NumericTextBox runat="server" ID="NumericTextBox1" SupportComma="true" SupportLetter="true" />
        <kfk:PersianDatePicker runat="server" ID="PersianDatePicker1" />
        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
    </div>
</asp:Content>

