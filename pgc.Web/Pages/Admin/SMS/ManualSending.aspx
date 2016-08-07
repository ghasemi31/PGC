<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="ManualSending.aspx.cs" Inherits="Pages_Admin_SMS_ManualSending" %>

<%@ Register src="../../../UserControls/Common/MessageControl.ascx" tagname="MessageControl" tagprefix="uc1" %>

<%@ Register src="../../../UserControls/Project/SendResult.ascx" tagname="SendResult" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">    
    <asp:ScriptManager ScriptMode="Release" ID="scmMain" runat="server" AsyncPostBackTimeout="0"></asp:ScriptManager>
    <asp:UpdatePanel ID="uplMain" runat="server">
        <ContentTemplate>            
            <asp:Panel ID="pnlSend" runat="server">
                </fieldset>

                <fieldset id="template">
                    <legend>متن ارسالی</legend>
                    <table class="Table">
                        <tr>
                            <td class="caption">شماره اختصاصی برای ارسال پیامک</td>
                            <td class="control"><kfk:LookupCombo ID="lkcPrivateNo" 
                                                                BusinessTypeName="pgc.Business.Lookup.PrivateNoLookupBusiness"
                                                                runat="server" 
                                                                Required="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="caption">متن پیامک ارسالی</td>
                            <td class="control"><uc1:MessageControl ID="txtSMS" runat="server" /></td>
                        </tr>
                    </table>
                    <div class="commands">    
                        <asp:Button ID="btnSend" runat="server" Text="ارسال" CssClass="large" onclick="btnSend_Click" UseSubmitBehavior="False"/>                
                    </div>
                </fieldset>


                <fieldset id="searchtbl">
                <legend>اضافه کردن دریافت کنندگان</legend>
                    <asp:RadioButtonList runat="server" ID="rdb" AutoPostBack="true" CssClass="radiobtn">
                        <asp:ListItem Selected="True" Value="User">انتخاب از میان کاربران سیستم</asp:ListItem>
                        <asp:ListItem Value="Manual">درج شماره تلفن همراه به صورت دستی</asp:ListItem>
                    </asp:RadioButtonList>    
                    <% if (rdb.SelectedIndex==0){ %>
                        <table class="Table searchtbl">
                            <tr>
                                <td class="caption">نام</td>
                                <td class="control"><kfk:NormalTextBox runat="server" ID="txtName"/></td>
        
                                <td class="caption">نام کاربری</td>
                                <td class="control"><kfk:NormalTextBox runat="server" ID="txtUsername"/></td>
                            </tr>
                            <tr>
                                <td class="caption">نمایندگی</td>
                                <td class="control"><kfk:LookupCombo ID="lkpBranch" 
                                                            runat="server" 
                                                            BusinessTypeName="pgc.Business.Lookup.BranchLookupBusiness"
                                                            DefaultItemText="--"
                                                            AddDefaultItem="True"/>
                                </td>
                                
                                <td class="caption">نقش</td>
                                <td class="control"><kfk:LookupCombo AddDefaultItem="true" EnumParameterType="pgc.Model.Enums.Role" runat="server" ID="lkpRole" /> </td>
                            </tr>   
                            <tr>
                                <td class="caption">استان</td>
                                <td class="control"><kfk:LookupCombo ID="lkcProvince" 
                                                                    runat="server" 
                                                                    BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness"
                                                                    AutoPostBack="true"
                                                                    AddDefaultItem="true"                                                                    
                                                                    DependantControl="lkcCity"/>
                                </td>

                                <td class="caption">شهرستان</td>
                                <td class="control"><kfk:LookupCombo ID="lkcCity" 
                                                                    runat="server" 
                                                                    BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness"
                                                                    DependOnParameterName="Province_ID" 
                                                                    DependOnParameterType="Int64"
                                                                    AddDefaultItem="true"/>
                                </td>          
                            </tr>       
                            <tr>
                                <td class="caption">وضعیت</td>
                                <td class="control"><kfk:LookupCombo ID="lkcActivityStatus" runat="server" EnumParameterType="pgc.Model.Enums.UserActivityStatus" AddDefaultItem="true"/></td>        

                                <td class="caption">جنسیت</td>
                                <td class="control"><kfk:LookupCombo runat="server" ID="lkpGender" EnumParameterType="pgc.Model.Enums.Gender" AddDefaultItem="true" /></td>
                            </tr>
                        </table>
                        <div class="commands">
                            <asp:Button runat="server" ID="btnSearch" Text="جستجو" class="dtButton" OnClick="OnSearch" />
                            <asp:Button runat="server" ID="btnShowAll" Text="نمایش همه" CssClass="dtButton" OnClick="OnSearchAll" />
                        </div>         
                        <kfk:HKGrid ID="Grid"
                                    runat="server"
                                    CssClass="Table"
                                    AllowPaging="True"
                                    DataSourceID="GridObjectDataSource" xmlns:kfk="kframework.webcontrols">
                                <PagerSettings Mode="NumericFirstLast" /> 
                            <Columns> 
                                <kfk:SelectableColumnTemplate /> 
                                <kfk:RowNumberColumnTemplate HeaderText="ردیف" /> 
                                <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" /> 
                                <kfk:BaseBoundField DataField="Name" HeaderText="نام و نام خانوادگی" /> 
                                <kfk:BaseBoundField DataField="Username" HeaderText="نام کاربری" /> 
                                <kfk:BaseBoundField DataField="Mobile" HeaderText="تلفن همراه" />
                                <kfk:BaseBoundField DataField="City" HeaderText="موقعیت" /> 
                                <kfk:BaseBoundField DataField="AccessLevel" HeaderText="سطح دسترسی" /> 
                                <kfk:EnumColumnTemplate DataField="ActivityStatus" 
                                                    HeaderText="وضعیت" 
                                                    Enum_dllName="pgc.Model" 
                                                    EnumPath="pgc.Model.Enums.UserActivityStatus" 
                                                    ViewMode="Image" 
                                                    ImageMode="png" 
                                                    ImagesFolderPath="~/Styles/Images/" />    
                            </Columns> 
                        </kfk:HKGrid>        
                        <asp:ObjectDataSource   ID="GridObjectDataSource"
                                                runat="server"
                                                EnablePaging="true"
                                                SelectMethod="Search_Select"
                                                SelectCountMethod="Search_Count"
                                                TypeName="pgc.Business.UserBusiness" onselecting="GridObjectDataSource_Selecting">
                        </asp:ObjectDataSource>    
                    <%}else{ %>
                        <div class="manualinsert">
                            <p>لیست شماره تلفن همراه کنندگان: (هر شماره در یک خط وارد شود)</p>
                            <kfk:NormalTextBox ID="txtList" runat="server" TextMode="MultiLine" />
                        </div>
                    <%} %>
                    <div class="sendcommand">
                        <% if (rdb.SelectedIndex==0){ %>
                            <p>از لیست بالا کاربران مورد نظر خود را برای ارسال انتخاب نمایید</p>        
                        <%} %>                
                        <asp:Button runat="server" ID="btnAdd" Text="اضافه به لیست دریافت کنندگان" CssClass="xxlarge"  CausesValidation="true" onclick="btnAdd_Click" />                    
                    </div>
                </fieldset>

                <fieldset id="rcpnttbl">
                <legend>لیست دریافت کنندگان</legend>
                    <div>
                        <kfk:HKGrid ID="RecipientGrid"  runat="server" onrowcommand="RecipientGrid_RowCommand" CssClass="Table" AllowPaging="true" xmlns:kfk="kframework.webcontrols" onrowdatabound="RecipientGrid_RowDataBound" onpageindexchanging="RecipientGrid_PageIndexChanging" >
                        <%--DataSourceID="RecipientObjectDataSource"--%>
                            <Columns> 
                                    <kfk:RowNumberColumnTemplate HeaderText="ردیف" /> 
                                    <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
                                    <kfk:BaseBoundField DataField="FullName" HeaderText="نام و نام خانوادگی" />
                                    <kfk:BaseBoundField DataField="UserName" HeaderText="نام کاربری" />
                                    <kfk:BaseBoundField DataField="Role" HeaderText="نقش" />
                                    <kfk:BaseBoundField DataField="Mobile" HeaderText="شماره تلفن همراه" />
                                    <kfk:BaseButtonField ButtonType="Button" CommandName="DeleteRow" Text="حذف" />    
                            </Columns> 
                        </kfk:HKGrid>                             
                    </div>
                    <div class="commands">
                        <asp:Button ID="Button2" runat="server" Text="ارسال" CssClass="large" onclick="btnSend_Click" />
                    </div>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="pnlResult" runat="server" Visible="false">
                <fieldset>
                    <legend>نتایج ارسال</legend>
                    <div class="fields">
                        <table cellpadding="1" cellspacing="0">
                            <tr>
                                <td>
                                    <uc2:SendResult ID="snrResult" runat="server" />                                
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="commands">
                        <asp:Button ID="cmdSentMessages" runat="server" Text="لیست پیامک های ارسالی" CssClass="dtButton"  Width="200" onclick="cmdSentMessages_Click"/>
                        <asp:Button ID="cmdNewSend" runat="server" Text="ارسال پیامک جدید" CssClass="dtButton" onclick="cmdNewSend_Click" Width="150" />
                    </div>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

