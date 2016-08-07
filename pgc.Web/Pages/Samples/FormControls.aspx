<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="FormControls.aspx.cs" Inherits="Pages_Samples_FormControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">
    <fieldset><legend>TextBoxes</legend>
    <table>
        <tr>
            <td class="caption">FK.HKTextbox.cs Mode = Default(Text)</td>
            <td class="control"><kfk:HKTextBox ID="HKTextBox1" runat="server"></kfk:HKTextBox></td>
            <td class="caption">FK.HKTextbox.cs Mode = Phone</td>
            <td class="control"><kfk:HKTextBox ID="HKTextBox2" runat="server" Mode="Phone"></kfk:HKTextBox></td>
        </tr>
        <tr>
            <td class="caption">FK.HKTextbox.cs InputMode = Numeric</td>
            <td class="control"><kfk:HKTextBox ID="HKTextBox3" runat="server" Mode="Numeric" CssClass="large"></kfk:HKTextBox></td>
            <td class="caption">FK.HKTextbox.cs InputMode = Numeric_Decimal</td>
            <td class="control"><kfk:HKTextBox ID="HKTextBox4" runat="server" Mode="Numeric_Decimal" CssClass="small"></kfk:HKTextBox></td>
        </tr>
        <tr>
            <td class="caption">UCs.NormalTextBox Mode = Default (Text) (all above modes available)</td>
            <td class="control"><kfk:NormalTextBox ID="NormalTextBox1" runat="server" /></td>
            <td class="caption">UCs.NormalTextBox Mode (Required)</td>
            <td class="control"><kfk:NormalTextBox ID="NormalTextBox2" runat="server" Required="true"/></td>
        </tr>
        <tr>
            <td class="caption">UCs.NumericTextBox Default (comma & letter)</td>
            <td class="control"><kfk:NumericTextBox ID="NumericTextBox1" runat="server" /></td>
            <td class="caption">UCs.NumericTextBox Support only Comma</td>
            <td class="control"><kfk:NumericTextBox ID="NumericTextBox2" runat="server" SupportComma="true" SupportLetter="false"/></td>
        </tr>
        <tr>
            <td class="caption">UCs.NumericTextBox no comma no letter</td>
            <td class="control"><kfk:NumericTextBox ID="NumericTextBox3" runat="server" SupportComma="false" SupportLetter="false"/></td>
            <td class="caption">UCs.NumericRange Default</td>
            <td class="control"><kfk:NumericRange ID="NumericRange1" runat="server" /></td>
        </tr>
    </table>
    </fieldset>
    <fieldset><legend>Date & Time</legend>
    <table>
        <tr>
            <td class="caption">UCs.PersianDatePicker</td>
            <td class="control"><kfk:PersianDatePicker ID="PersianDatePicker1" runat="server" /></td>
            <td class="caption">UCs.PersianDateRange</td>
            <td class="control"><kfk:PersianDateRange ID="PersianDateRange1" runat="server" /></td>
        </tr>
        <tr>
            <td class="caption">UCs.PersianDateRange</td>
            <td class="control"><kfk:TimePicker ID="TimePicker1" runat="server" /></td>
            <td class="caption"></td>
            <td class="control"></td>
        </tr>
    </table>
    </fieldset>
    <fieldset><legend>Lookup Combo</legend>
    <table>
        <tr>
            <td class="caption">  کمبو استان</td>
            <td class="control"><kfk:LookupCombo ID="lkcSample" runat="server" BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness" /></td>
            <td class="caption">  کمبو Enum</td>
            <td class="control"><kfk:LookupCombo ID="LookupCombo1" runat="server" EnumParameterType="pgc.Model.Enums.EnumSample"/></td>
        </tr>
        <tr>
            <td class="caption"> Default Item</td>
            <td class="control"><kfk:LookupCombo ID="LookupCombo2" runat="server" BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness" AddDefaultItem="true"/></td>
            <td class="caption">  Combo + Plus Runtime Parameter (BranchDemandSession.BranchDemandID)</td>
            <td class="control"><kfk:LookupCombo ID="LookupCombo4" runat="server" BusinessTypeName="pgc.Business.Lookup.SampleLookupBusiness" AddBranchDemandIDParameter="true"/></td>
        </tr>
        <tr>
            <td class="caption"> Pronvince / Satate + CallBack</td>
            <td class="control">
                <kfk:LookupCombo    ID="LookupCombo6" 
                                    runat="server" 
                                    BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness" 
                                    OnSelectedIndexChanged="Province_Changed"
                                    AutoPostBack="true" 
                                    DependantControl="LookupCombo7" />
            </td>
            <td class="caption">  Province / State + Required</td>
            <td class="control">
                <kfk:LookupCombo    ID="LookupCombo7" 
                                    runat="server" 
                                    BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness" 
                                    DependOnParameterName="Province_ID" 
                                    DependOnParameterType="Int64" 
                                    Required="true"/>
            </td>
        </tr>
        <tr>
            <!-- Samplelock A -->
            <td class="caption">  پارامتر اضافی برای یک فانگشن جدید(Event)</td>
            <td class="control">
                <asp:CheckBox runat="server" ID="chkBool" />
                <kfk:LookupCombo    ID="lkcDynamicParam" 
                                    runat="server" 
                                    BusinessTypeName="pgc.Business.Lookup.SampleLookupBusiness" 
                                    SelectMethod="GetLookupList_RunTimeParam" 
                                    OnSelecting="LookupCombo_Selecting"/>
                <asp:Button runat="server" ID="btnRebind" Text="ReBind" onclick="btnRebind_Click" CausesValidation="false"/>
            </td>
            <!-- Sample Block B -->
            <td class="caption">نمونه Get</td>
            <td class="control">
                <kfk:LookupCombo ID="LookupCombo9" 
                                runat="server" 
                                EnumParameterType="pgc.Model.Enums.EnumSample" 
                                AddDefaultItem="true" 
                                DefaultItemText="Sample Default Text"/>
                <asp:Button runat="server" ID="btnSampleGet" Text="Get Enum" CausesValidation="false" onclick="btnSampleGet_Click" /><asp:Label runat="server" ID="lblEnum" ></asp:Label>
            </td>
        </tr>
        <tr>
            <!-- Sample Block C-->
            <td class="caption"> نمونه Get عدد</td>
            <td class="control">
                <kfk:LookupCombo ID="LookupCombo10" 
                                runat="server" 
                                EnumParameterType="pgc.Model.Enums.EnumSample" 
                                AddDefaultItem="true" 
                                DefaultItemText="Sample Default Text"/>
                <asp:Button runat="server" ID="btnSampleGetNum" Text="Get Number Value" CausesValidation="false" onclick="btnSampleGetNum_Click" /><asp:Label runat="server" ID="lblEnumNum" ></asp:Label>
            </td>
            <!-- Sample Block D -->
            <td class="caption">نمونه Set کردن</td>
            <td class="control">
                <kfk:LookupCombo ID="LookupCombo11" 
                                runat="server" 
                                BusinessTypeName="pgc.Business.Lookup.SampleLookupBusiness" />
                <asp:Button ID="btnSet" runat="server" Text="Set Value" CausesValidation="False" onclick="btnSet_Click" />
            </td>
        </tr>
        <tr>
            <!-- Sample Block E -->
            <td class="caption">نمونهEnum  Set کردن</td>
            <td class="control">
                <kfk:LookupCombo    ID="LookupCombo12" 
                                    runat="server" 
                                    EnumParameterType="pgc.Model.Enums.EnumSample" 
                                    AddDefaultItem="true"/>
                <asp:Button ID="btnSetEnum" runat="server" Text="Set Enum" CausesValidation="False" onclick="btnSetEnum_Click" />
            </td>
            <td></td>
            <td></td>
        </tr>
    </table>
    </fieldset>
    <fieldset><legend>Lookup</legend>
    <table>
        <tr>
            <td class="caption">Html Editor</td>
            <td class="control"><kfk:HtmlEditor ID="hte" runat="Server"/></td>
            <td class="caption">Another Html Editor</td>
            <td class="control"><kfk:HtmlEditor ID="hte2" runat="Server" Required="true"/></td>
        </tr>
        <tr>
            <td class="caption">FileUploader</td>
            <td class="control">
                    <kfk:FileUploader ID="fup1" runat="server" SaveFolder="~/BranchDemandfiles/test/" FilePath="~/BranchDemandfiles/test/Koala.jpg"/>
            </td>
            <td class="caption">Set and Get</td>
            <td class="control">
                <!-- Sample Block G -->
                <div dir="ltr">
                    <asp:TextBox runat="server" ID="txtFup1" Text="~/BranchDemandfiles/test/Koala.jpg" Width="150"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSetFup1" Text="Set" OnClick="SetFup1" CausesValidation="false"/>
                    <br />
                    <asp:Button runat="server" ID="btnGetFup1" Text="Get" OnClick="GetFup1" CausesValidation="false"/>
                    <asp:Label runat="server" ID="lblFup1" ></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td class="caption">
                FUP with 'FilePath'
            </td>
            <td class="control">
                <kfk:FileUploader ID="fup2" runat="server" SaveFolder="~/BranchDemandfiles/test/" />
            </td>
            <td class="caption">Set and Get</td>
            <td class="control">
                <!-- Sample Block H -->
                <div dir="ltr">
                    <asp:TextBox runat="server" ID="txtFup2" Text="~/BranchDemandfiles/test/Koala.jpg" Width="150"></asp:TextBox>
                    <asp:Button runat="server" ID="btnSetFup2" Text="Set" OnClick="SetFup2" CausesValidation="false"/>
                    <br />
                    <asp:Button runat="server" ID="btnGetFup2" Text="Get" OnClick="GetFup2" CausesValidation="false"/>
                    <asp:Label runat="server" ID="lblFup2" ></asp:Label>
                </div>
            </td>
        </tr>        
        <tr>
        <td class="caption">FileUploader +Required</td>
            <td class="control">
                    <kfk:FileUploader ID="fup3" runat="server" Required=true SaveFolder="~/BranchDemandfiles/test/"/>
            </td>
            <td class="caption">FileUploader +ReadOnly</td>
            <td class="control">
                    <kfk:FileUploader ID="fup4" runat="server" ReadOnly="true" FilePath="~/BranchDemandfiles/test/Koala.jpg"/>
            </td>
        </tr>
        <%--<tr>
            <td class="caption">lookup</td>
            <td class="control">
                <kfk:Lookup ID="lkpBranchDemand1" 
                            runat="server" 
                            URL="~/Pages/Admin/Lookups/BranchDemand.aspx"
                            ColumnIndex="1"/>
            </td>
            <td class="caption">Another Lookup</td>
            <td class="control">
                <kfk:Lookup ID="lkpBranchDemand2" 
                            runat="server" 
                            URL="~/Pages/Admin/Lookups/BranchDemand.aspx"
                            ColumnIndex="1"/>
            </td>
        </tr>
        <tr>
            <td class="caption">Sample Get</td>
            <td class="control">
                <asp:Button ID="btnGetLookupGridSample" runat="server" OnClick="btnGetLookupGridSample_Click" CausesValidation="false"
                    Text="Get Value" />
                <asp:Label ID="lblGetLookupGridSample" runat="server" Text=""></asp:Label>
            </td>
            <td class="caption">Sample Set</td>
            <td class="control">
                <asp:Button ID="btnSetLookupGridSample" runat="server" OnClick="btnSetLookupGridSample_Click" CausesValidation="false"
                    Text="Set Value" />
                <asp:Label ID="lblSetLookupGridSample" runat="server" Text=""></asp:Label>
            </td>
        </tr>--%>
    </table>
    </fieldset>
    <fieldset><legend>Other Controls</legend>
    <table>
        <tr>
            <td class="caption">Sample Text Area</td>
            <td class="control"><kfk:NormalTextBox TextMode="MultiLine" runat="server" ID="textarea1" /></td>
            <td class="caption">Sample Check Box</td>
            <td class="control">
                <asp:CheckBox runat="server" ID="CheckBox1" Text="sample check box 1" CssClass="checkbox" />
                <asp:CheckBox runat="server" ID="CheckBox2" Text="sample check box 2" CssClass="checkbox" />
                <asp:CheckBox runat="server" ID="CheckBox3" Text="sample check box 3" CssClass="checkbox" />
            </td>
        </tr>
        <tr>
            <td class="caption">Sample Radio</td>
            <td class="control">
                <asp:RadioButton ID="RadioButton1" runat="server" Text="saple radio 1" CssClass="radio" />
                <asp:RadioButton ID="RadioButton2" runat="server" Text="saple radio 2" CssClass="radio" />
                <asp:RadioButton ID="RadioButton3" runat="server" Text="saple radio 3" CssClass="radio" />
            </td>
            <td class="caption">Sample ListBox</td>
            <td class="control">
                <asp:ListBox runat="server" ID="lsb1">
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                </asp:ListBox>
            </td>
        </tr>
        <%--<tr>
            <td class="caption">test required</td>
            <td class="control" style="display:none">
                <input runat="server" id="txtTest" />
            </td>
            <td>
                <asp:RequiredFieldValidator runat="server" ID="rfvTest" ControlToValidate="txtTest" Text="*" ToolTip="tooltip sample" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>--%>
    </table>
    </fieldset>
    <fieldset><legend>Grid</legend>
        <div class="list-BranchDemandTitle">
            <div class="commands">
                <asp:Button ID="btnBulkDelete" runat="server" Text="حذف سطر های انتخاب شده" CssClass="dtButton" Width="160" OnClientClick="if (!confirm('آیا در حذف کلیه سطرهای انتخاب شده ، اطمینان دارید؟')){return false;}" />
                <asp:Button ID="btnAdd" runat="server" Text="+ ایجاد سطر جدید" CssClass="dtButton" Width="150" />
            </div>
        </div>
        <kfk:HKGrid ID="grdList"
                    runat="server"
                    AllowPaging="True"
                    PageSize="3"
                    DataSourceID="obdSource">
            <PagerSettings Mode="NumericFirstLast" />
            <Columns>
                <kfk:SelectableColumnTemplate />
                <kfk:RowNumberColumnTemplate HeaderText="ردیف" />
                <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
                <kfk:TextColumnTemplate DataField="Title" HeaderText="عنوان جشنواره" MaxLength="20"/>
                <kfk:BaseBoundField DataField="StartPersianDate" HeaderText="تاریخ شروع" />
                <kfk:BaseBoundField DataField="EndPersianDate" HeaderText="تاریخ پایان" />
                <kfk:BaseBoundField DataField="IsDoneState" HeaderText="وضعیت جشنواره"/>
                <kfk:TextColumnTemplate DataField="Description" HeaderText="توضیحات" MaxLength="20"/>
                <kfk:BaseButtonField ButtonType="Button" CommandName="EditRow" Text="ویرایش" />
                <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />
            </Columns>
        </kfk:HKGrid>
        <asp:ObjectDataSource   ID="obdSource"
                                runat="server"
                                EnablePaging="true"
                                ViewStateMode="Disabled"
                                TypeName="pgc.Business.SampleBusiness"
                                SelectMethod="Search_Select"
                                SelectCountMethod="Search_Count" 
                                onselecting="obdSource_Selecting">
        </asp:ObjectDataSource>
    </fieldset>
    <div class="commands">
        <asp:Button ID="Button1" runat="server" CausesValidation="true" UseSubmitBehavior="true" Text="Submit Button" />
        <asp:Button ID="Button2" runat="server" CausesValidation="false" Text="normal button" />
        <asp:Button ID="Button3" runat="server" CausesValidation="false" 
            Text="large normal button" CssClass="large" onclick="Button3_Click" />
        <asp:Button ID="Button4" runat="server" CausesValidation="false" Text="xlarge normal button" CssClass="xlarge" />
    </div>
    
</asp:Content>

