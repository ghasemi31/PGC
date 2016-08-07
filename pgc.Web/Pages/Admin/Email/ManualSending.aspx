<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="ManualSending.aspx.cs" Inherits="Pages_Admin_Email_ManualSending" ValidateRequest="false" %>

<%@ Register src="../../../UserControls/Project/SendEmailResult.ascx" tagname="SendEmailResult" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../../../Scripts/Shared/FileUploaderHide.js" type="text/javascript"></script>    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" Runat="Server">    
    <asp:ScriptManager ScriptMode="Release" ID="scmMain" runat="server" AsyncPostBackTimeout="0"></asp:ScriptManager>
    <asp:UpdatePanel ID="uplMain" runat="server">
        <ContentTemplate>            
            <asp:Panel ID="pnlSend" runat="server">
                </fieldset>

                <fieldset id="template">
                    <legend>متن ارسالی</legend>
                    <table class="Table">    
                        <tr class="wrng">
                            <td colspan="4"><p>در صورت تمایل به الصاق عکس در ایمیل حتما در قسمت Source، آدرس درج شده در src را بصورت کامل وارد نمایید <br /> مثال:(http://www.pgcizi.com/Userfiles/picture.jpeg)</p></td>
                        </tr>
                        <tr>
                            <td class="caption">نام نمایشی ارسال کننده</td>
                            <td class="control"><kfk:NormalTextBox ID="txtDisplayAddressName" runat="server" /></td> 
                            <td class="caption" colspan="2"><asp:CheckBox ID="chUseTemplate" runat="server" Text="استفاده از قالب ایمیل" /></td>
                            
                        </tr>                    
                        <tr>
                            <td class="caption">عنوان</td>
                            <td class="control" colspan="3"><kfk:NormalTextBox ID="txtSubject" runat="server" CssClass="xxxlarge" /></td> 
                        </tr>                    
                        <tr>
                            <td class="caption">متن ایمیل ارسالی</td>
                            <td class="control" colspan="3"><kfk:HtmlEditor ID="htmlBody" runat="server" /></td>
                        </tr>                        
                    </table>
                    <ul class="fup insert">
                        <li>
                            <h1>ضمائم:</h1>
                        </li>
                        <li>                    
                            <a href="#">الصاق فایل ضمیمه</a>                    
                            <kfk:FileUploader ID="fup1" runat="server" SaveFolder="~/UserFiles/emailtemp/" />
                        </li>
                        <li>
                            <a href="#">الصاق فایل ضمیمه</a>                    
                            <kfk:FileUploader ID="fup2" runat="server" SaveFolder="~/UserFiles/emailtemp/" />
                        </li>
                        <li>
                            <a href="#">الصاق فایل ضمیمه</a>                    
                            <kfk:FileUploader ID="fup3" runat="server" SaveFolder="~/UserFiles/emailtemp/" />
                        </li>
                        <li>
                            <a href="#">الصاق فایل ضمیمه</a>                    
                            <kfk:FileUploader ID="fup4" runat="server" SaveFolder="~/UserFiles/emailtemp/" />
                        </li>
                        <li>
                            <a href="#">الصاق فایل ضمیمه</a>                    
                            <kfk:FileUploader ID="fup5" runat="server" SaveFolder="~/UserFiles/emailtemp/" />
                        </li>                
                    </ul>
                    <div class="commands">    
                        <asp:Button ID="btnSend" runat="server" Text="ارسال" CssClass="large" onclick="btnSend_Click" />
                    </div>
                </fieldset>


                <fieldset id="searchtbl">
                <legend>اضافه کردن دریافت کنندگان</legend>
                    <asp:RadioButtonList runat="server" ID="rdb" AutoPostBack="true" CssClass="radiobtn">
                        <asp:ListItem Selected="True" Value="User">انتخاب از میان کاربران سیستم</asp:ListItem>
                        <asp:ListItem Value="Manual">درج آدرس ایمیل به صورت دستی</asp:ListItem>
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
                                    DataSourceID="GridObjectDataSource">
                                <PagerSettings Mode="NumericFirstLast" /> 
                            <Columns> 
                                <kfk:SelectableColumnTemplate /> 
                                <kfk:RowNumberColumnTemplate HeaderText="ردیف" /> 
                                <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" /> 
                                <kfk:BaseBoundField DataField="Name" HeaderText="نام و نام خانوادگی" /> 
                                <kfk:BaseBoundField DataField="Username" HeaderText="نام کاربری" /> 
                                <kfk:BaseBoundField DataField="Email" HeaderText="آدرس ایمیل" /> 
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
                            <p>لیست آدرس ایمیل دریافت کنندگان (هر آدرس در یک خط)</p>
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
                        <kfk:HKGrid ID="RecipientGrid"  runat="server" onrowcommand="RecipientGrid_RowCommand" CssClass="Table" AllowPaging="true" onrowdatabound="RecipientGrid_RowDataBound" onpageindexchanging="RecipientGrid_PageIndexChanging" >
                        <%--DataSourceID="RecipientObjectDataSource"--%>
                            <Columns> 
                                    <kfk:RowNumberColumnTemplate HeaderText="ردیف" /> 
                                    <kfk:BaseBoundField DataField="ID" Visible="false" HeaderText="" />
                                    <kfk:BaseBoundField DataField="FullName" HeaderText="نام و نام خانوادگی" />
                                    <kfk:BaseBoundField DataField="UserName" HeaderText="نام کاربری" />
                                    <kfk:BaseBoundField DataField="Role" HeaderText="نقش" />
                                    <kfk:BaseBoundField DataField="Email" HeaderText="آدرس ایمیل" />
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
                                     <uc1:SendEmailResult ID="serResult" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="commands">
                        <asp:Button ID="cmdSentMessages" runat="server" Text="لیست ایمیل های ارسالی" CssClass="dtButton"  Width="200" onclick="cmdSentMessages_Click"/>
                        <asp:Button ID="cmdNewSend" runat="server" Text="ارسال ایمیل جدید" CssClass="dtButton" onclick="cmdNewSend_Click" Width="150" />
                    </div>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>   
</asp:Content>

