<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_SystemEvent_Detail" %>
<%@ Register Src="../../../UserControls/Common/MessageControl.ascx" TagName="MessageControl" TagPrefix="uc1" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table class="dtl">
    <tr class="wrng">
        <td colspan="4">
            <p>
                کاربر گرامی قبل از تعیین تنظیمات به تعداد بسته های ارسال ایمیل که با توجه به تعداد کاربران انتخابی، نقش کاربری و سطح دسترسی (که منجر به انتخاب بازه ای از کاربران می شوند)،دقت نمایید تا از وارد آمدن بار شبکه ای به سرور خود جلوگیری نمایید .
                    <br />
                همچنین به تعداد دفعات رویداد این رخداد توجه داشته باشید
            </p>
        </td>
    </tr>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control">
            <asp:Label ID="txtTitle" runat="server"></asp:Label></td>

        <td></td>
        <td></td>
    </tr>
    <%if (!string.IsNullOrEmpty(txtDescription.Text))
      { %>
    <tr>
        <td class="caption">توضیحات</td>
        <td class="control">
            <asp:Label ID="txtDescription" runat="server"></asp:Label></td>

        <td></td>
        <td></td>
    </tr>
    <%} %>
</table>


<table>
    <tr>
        <td>
            <ul class="tabs">
                <li class="selected" onclick="selectTab(this, 'tab-1')">تنظیمات ایمیل</li>
                <li onclick="selectTab(this, 'tab-2')">تنظیمات پیامک</li>
            </ul>
        </td>
    </tr>
    <tr>
        <td id="tab-contents">
            <div id="tab-1" style="display: block;">
                <table>
                    <%if (SystemEvent.Support_Custom_AccessLevel_Email ||
              SystemEvent.Support_Custom_Role_Email ||
              SystemEvent.Support_Custom_User_Email)
                      { %>

                    <%if (SystemEvent.Support_Custom_AccessLevel_Email || SystemEvent.Support_Custom_AccessLevel_SMS)
                      { %>
                    <tr>
                        <%if (SystemEvent.Support_Custom_AccessLevel_Email)
                          { %>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbCustom_Access_Email" runat="server" CssClass="item-selected" Text="ارسال ایمیل به سطح دسترسی خاص" /></td>
                        <%}
                        %>
                        <td class="caption visibility-hidden">سطح دسترسی </td>
                        <td class="control visibility-hidden">
                            <kfk:LookupCombo ID="lkpCustomAccess" runat="server" AddDefaultItem="true" DefaultItemValue="-1" BusinessTypeName="pgc.Business.Lookup.AccessLevelLookupBusiness" DependOnParameterName="ForEventAction" DependOnParameterType="Boolean" DefaultItemText="" />
                        </td>


                    </tr>
                    <%} %>

                    <tr class="border">
                        <td colspan="4"></td>
                    </tr>

                    <%if (SystemEvent.Support_Custom_Role_Email)
                      { %>
                    <tr>
                        <%if (SystemEvent.Support_Custom_Role_Email)
                          { %>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbCustom_Role_Email" runat="server" CssClass="item-selected" Text="ارسال ایمیل به نقش خاص" /></td>

                        <%} %>
                        <td class="caption visibility-hidden">نقش </td>
                        <td class="control visibility-hidden">
                            <kfk:LookupCombo ID="lkpCustomRole" runat="server" DefaultItemValue="-1" EnumParameterType="pgc.Model.Enums.Role" AddDefaultItem="true" DefaultItemText="" />
                        </td>


                    </tr>
                    <%} %>

                    <tr class="border">
                        <td colspan="4"></td>
                    </tr>

                    <%if (SystemEvent.Support_Custom_User_Email)
                      { %>
                    <tr>
                        <%if (SystemEvent.Support_Custom_User_Email)
                          { %>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbCustom_User_Email" runat="server" CssClass="item-selected" Text="ارسال ایمیل به کاربر خاص" /></td>

                        <%} %>
                        <td class="caption visibility-hidden">کاربر </td>
                        <td class="control visibility-hidden">
                            <kfk:LookupCombo ID="lkpCustomUser" runat="server" DefaultItemValue="-1" AddDefaultItem="true" BusinessTypeName="pgc.Business.Lookup.UserLookupBusiness" AddUserIDParameter="true" DefaultItemText="" />
                        </td>

                    </tr>
                    <%} %>
                    <%} %>


                    <tr class="border">
                        <td colspan="4"></td>
                    </tr>

                    <tr>
                        <%if (SystemEvent.Support_Related_Guest_Email)
                          { %>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbRelated_Guest_Email" runat="server" Text="ارسال ایمیل به مهمان مربوطه" /></td>
                        <%} %>
                        <%if (SystemEvent.Support_Related_User_Email)
                          { %>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbRelated_User_Email" runat="server" Text="ارسال ایمیل به کاربر مربوطه" /></td>
                        <%} %>
                    </tr>

                    <tr>
                    <%if (SystemEvent.Support_Related_Branch_Email)
                      {%>                    
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbRelated_Branch_Email" runat="server" Text="ارسال ایمیل به نمایندگی مربوطه" /></td>                    
                    <%} %>
                    <%if (SystemEvent.Support_Related_Doer_Email)
                      {%>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbRelated_Doer_Email" runat="server" Text="ارسال ایمیل به انجام دهنده مربوطه" /></td>
                    <%} %>
                    </tr>



                    <%if (SystemEvent.Support_Manual_Email)
                      {%>
                     <tr class="border">
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td class="caption">ارسال ایمیل به آدرس های<br />
                            (هر آدرس در یک خط)</td>
                        <td class="control" colspan="3">
                            <kfk:NormalTextBox TextMode="MultiLine" ID="txtManual_Email" runat="server" />
                        </td>
                    </tr>
                    <%} %>

                    <%if (SystemEvent.Support_Related_Branch_Email ||
                SystemEvent.Support_Related_Doer_Email ||
                SystemEvent.Support_Custom_User_Email ||
                SystemEvent.Support_Custom_AccessLevel_Email ||
                SystemEvent.Support_Custom_Role_Email ||
                SystemEvent.Support_Manual_Email)
                      {%>
                    <tr>
                        <td class="caption">متن ایمیل ارسالی به مدیر</td>
                        <td class="control">
                          
                            <kfk:CkHtmlEditor ID="htmlEmailAdmin" runat="server"  /> 
                        </td>

                        <td class="control">
                            <input type="button" value="<<" onclick="javascript: moveAlias(this)" /></td>
                        <td class="control">
                            <asp:ListBox ID="lsb3" runat="server"></asp:ListBox></td>
                    </tr>
                    <%} %>
                    <%if (SystemEvent.Support_Related_User_Email ||
                SystemEvent.Support_Related_Guest_Email ||
                SystemEvent.Support_Related_Guest_Email ||
                SystemEvent.Support_Custom_Role_Email ||
                SystemEvent.Support_Related_Branch_Email ||
                SystemEvent.Support_Related_User_Email)
                      {%>
                    <tr>
                        <td class="caption">متن ایمیل ارسالی به کاربر</td>
                        <td class="control">
                      
                             <kfk:CkHtmlEditor ID="htmlEmailUser" runat="server"  />
                        </td>

                        <td class="control">
                            <input type="button" value="<<" onclick="javascript: moveAlias(this)" /></td>
                        <td class="control">
                            <asp:ListBox ID="lsb1" runat="server"></asp:ListBox></td>

                    </tr>
                    <%} %>
                </table>
            </div>

            <%-- ////////////////////////////////// --%>
            <div id="tab-2">
                <table>
                    <%if (SystemEvent.Support_Custom_AccessLevel_SMS ||
              SystemEvent.Support_Custom_Role_SMS ||
              SystemEvent.Support_Custom_User_SMS)
                      { %>

                        <%if (SystemEvent.Support_Manual_SMS ||
                SystemEvent.Support_Custom_User_SMS ||
                SystemEvent.Support_Custom_Role_SMS ||
                SystemEvent.Support_Custom_AccessLevel_SMS ||
                SystemEvent.Support_Related_Doer_SMS ||
                SystemEvent.Support_Related_Guest_SMS ||
                SystemEvent.Support_Related_Branch_SMS ||
                SystemEvent.Support_Related_User_SMS)
                      {%>
                    <tr>
                        <td class="caption" colspan="2">شماره اختصاصی ارسال پیامک</td>
                        <td class="control" colspan="3">
                            <kfk:LookupCombo ID="lkcPrivateNo"
                                BusinessTypeName="pgc.Business.Lookup.PrivateNoLookupBusiness"
                                runat="server" />
                        </td>
                    </tr>
                     <tr class="border">
                        <td colspan="4"></td>
                    </tr>
                    <%} %>

                    <%if (SystemEvent.Support_Custom_AccessLevel_SMS)
                      { %>
                    <tr>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbCustom_Access_SMS" runat="server" CssClass="item-selected" Text="ارسال پیامک به سطح دسترسی خاص" /></td>
                        <td class="caption visibility-hidden">سطح دسترسی </td>
                        <td class="control visibility-hidden">
                            <kfk:LookupCombo ID="lkpCustomAccess_SMS" runat="server" AddDefaultItem="true" DefaultItemValue="-1" BusinessTypeName="pgc.Business.Lookup.AccessLevelLookupBusiness" DependOnParameterName="ForEventAction" DependOnParameterType="Boolean" DefaultItemText="" />
                        </td>
                    </tr>
                   
                    <%} %>

                    <tr class="border">
                        <td colspan="4"></td>
                    </tr>

                    <%if (SystemEvent.Support_Custom_Role_SMS)
                      { %>
                    <tr>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbCustom_Role_SMS" CssClass="item-selected" runat="server" Text="ارسال پیامک به نقش خاص" /></td>

                        <td class="caption visibility-hidden"> نقش </td>
                        <td class="control visibility-hidden">
                            <kfk:LookupCombo ID="lkpCustomRole_SMS" runat="server" DefaultItemValue="-1" EnumParameterType="pgc.Model.Enums.Role" AddDefaultItem="true" DefaultItemText="" />
                        </td>
                    </tr>                 
                    <%} %>

                    <tr class="border">
                        <td colspan="4"></td>
                    </tr>

                    <%if (SystemEvent.Support_Custom_User_SMS)
                      { %>
                    <tr>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbCustom_User_SMS" CssClass="item-selected" runat="server" Text="ارسال پیامک به کاربر خاص" /></td>
                        <td class="caption visibility-hidden"> کاربر </td>
                        <td class="control visibility-hidden">
                            <kfk:LookupCombo ID="lkpCustomUser_SMS" runat="server" DefaultItemValue="-1" AddDefaultItem="true" BusinessTypeName="pgc.Business.Lookup.UserLookupBusiness" AddUserIDParameter="true" DefaultItemText="" />
                        </td>

                    </tr>
                    <tr class="border">
                        <td colspan="4"></td>
                    </tr>
                  
                    <%} %>
                    <%} %>


                
                     <tr>
                    <%if (SystemEvent.Support_Related_Guest_SMS)
                      { %>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbRelated_Guest_SMS" runat="server" Text="ارسال پیامک به مهمان مربوطه" /></td>                    
                    <%} %>
                    <%if (SystemEvent.Support_Related_User_SMS)
                      { %>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbRelated_User_SMS" runat="server" Text="ارسال پیامک به کاربر مربوطه" /></td>
                    <%} %>
                    </tr>

                     <tr>
                    <%if (SystemEvent.Support_Related_Branch_SMS)
                      {%>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbRelated_Branch_SMS" runat="server" Text="ارسال پیامک به نمایندگی مربوطه" /></td>                    
                    <%} %>
                    <%if (SystemEvent.Support_Related_Doer_SMS)
                      {%>
                        <td class="control" colspan="2">
                            <asp:CheckBox ID="chbRelated_Doer_SMS" runat="server" Text="ارسال پیامک به انجام دهنده مربوطه" /></td>
                    <%} %>
                    </tr>

                    <%if (SystemEvent.Support_Manual_SMS)
                      {%>
                    <tr class="border">
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td class="caption">ارسال پیامک به شماره های<br />
                            (هر شماره تلفن در یک خط)</td>
                        <td class="control" colspan="3">
                            <kfk:NormalTextBox TextMode="MultiLine" ID="txtManual_SMS" runat="server" />
                        </td>
                    </tr>
                    <%} %>
                    <%if (SystemEvent.Support_Related_Branch_SMS ||
                SystemEvent.Support_Related_Doer_SMS ||
                SystemEvent.Support_Custom_User_SMS ||
                SystemEvent.Support_Custom_AccessLevel_SMS ||
                SystemEvent.Support_Custom_Role_SMS ||
                SystemEvent.Support_Manual_SMS)
                      {%>
                    <tr>
                        <td class="caption">متن پیامک ارسالی به مدیر</td>
                        <td class="control">
                            <uc1:MessageControl ID="txtSMSAdmin" runat="server" />
                        </td>

                        <td class="control">
                            <input type="button" value="<<" onclick="javascript: moveAlias(this)" /></td>
                        <td class="control">
                            <asp:ListBox ID="lsb4" runat="server"></asp:ListBox></td>
                    </tr>
                    <%} %>
                    <%if (SystemEvent.Support_Related_User_SMS ||
                SystemEvent.Support_Related_Guest_SMS ||
                SystemEvent.Support_Related_Guest_SMS ||
                SystemEvent.Support_Custom_Role_SMS ||
                SystemEvent.Support_Related_Branch_SMS ||
                SystemEvent.Support_Related_User_SMS)
                      {%>
                    <tr>
                        <td class="caption">متن پیامک ارسالی به کاربر</td>
                        <td class="control">
                            <uc1:MessageControl ID="txtSMSUser" runat="server" />
                        </td>

                        <td class="control">
                            <input type="button" value="<<" onclick="javascript: moveAlias(this)" /></td>
                        <td class="control">
                            <asp:ListBox ID="lsb2" runat="server"></asp:ListBox></td>
                    </tr>
                    <%} %>
                </table>
            </div>
        </td>
    </tr>

</table>

<div class="commands" id="detailDiv" runat="server">
    <asp:Button runat="server" ID="btnSave" Text="ذخیره رخداد" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="btnCancel" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>






<%--<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Pages_Admin_SystemEvent_Detail" %>
<%@ Register Src="../../../UserControls/Common/MessageControl.ascx" TagName="MessageControl" TagPrefix="uc1" %>
<legend><%=(this.Page as kFrameWork.UI.BasePage).Entity.UITitle %></legend>
<table class="dtl">
    <tr class="wrng">
        <td colspan="4">
            <p>
                کاربر گرامی قبل از تعیین تنظیمات به تعداد بسته های ارسال ایمیل که با توجه به تعداد کاربران انتخابی، نقش کاربری و سطح دسترسی (که منجر به انتخاب بازه ای از کاربران می شوند)،دقت نمایید تا از وارد آمدن بار شبکه ای به سرور خود جلوگیری نمایید .
                    <br />
                همچنین به تعداد دفعات رویداد این رخداد توجه داشته باشید
            </p>
        </td>
    </tr>
    <tr>
        <td class="caption">عنوان</td>
        <td class="control">
            <asp:Label ID="Label1" runat="server"></asp:Label></td>

        <td></td>
        <td></td>
    </tr>
    <%if (!string.IsNullOrEmpty(txtDescription.Text))
      { %>
    <tr>
        <td class="caption">توضیحات</td>
        <td class="control">
            <asp:Label ID="Label2" runat="server"></asp:Label></td>

        <td></td>
        <td></td>
    </tr>
    <%} %>
</table>


<table>

    <%if (SystemEvent.Support_Custom_AccessLevel_Email ||
              SystemEvent.Support_Custom_AccessLevel_SMS ||
              SystemEvent.Support_Custom_Role_Email ||
              SystemEvent.Support_Custom_Role_SMS ||
              SystemEvent.Support_Custom_User_Email ||
              SystemEvent.Support_Custom_User_SMS)
      { %>
    <tr class="border">
        <td colspan="4"></td>
    </tr>

    <%if (SystemEvent.Support_Custom_AccessLevel_Email || SystemEvent.Support_Custom_AccessLevel_SMS)
      { %>
    <tr>
        <td class="caption">ارسال به سطح دسترسی خاص</td>
        <td class="control">
            <kfk:LookupCombo ID="LookupCombo1" runat="server" AddDefaultItem="true" DefaultItemValue="-1" BusinessTypeName="pgc.Business.Lookup.AccessLevelLookupBusiness" DependOnParameterName="ForEventAction" DependOnParameterType="Boolean" DefaultItemText="" />
        </td>

        <%if (SystemEvent.Support_Custom_AccessLevel_Email)
          { %>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox1" runat="server" Text="ارسال ایمیل به سطح دسترسی خاص" /></td>
        <%}
          else
          { %>
        <td></td>
        <td></td>
        <%} %>
    </tr>
    <%if (SystemEvent.Support_Custom_AccessLevel_SMS)
      { %>
    <tr>
        <td></td>
        <td></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox2" runat="server" Text="ارسال پیامک به سطح دسترسی خاص" /></td>
    </tr>
    <%} %>
    <%} %>

    <tr class="border">
        <td colspan="4"></td>
    </tr>

    <%if (SystemEvent.Support_Custom_Role_Email || SystemEvent.Support_Custom_Role_SMS)
      { %>
    <tr>
        <td class="caption">ارسال به نقش خاص</td>
        <td class="control">
            <kfk:LookupCombo ID="LookupCombo2" runat="server" DefaultItemValue="-1" EnumParameterType="pgc.Model.Enums.Role" AddDefaultItem="true" DefaultItemText="" />
        </td>

        <%if (SystemEvent.Support_Custom_Role_Email)
          { %>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox3" runat="server" Text="ارسال ایمیل به نقش خاص" /></td>
        <%}
          else
          { %>
        <td></td>
        <td></td>
        <%} %>
    </tr>
    <%if (SystemEvent.Support_Custom_Role_SMS)
      { %>
    <tr>
        <td></td>
        <td></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox4" runat="server" Text="ارسال پیامک به نقش خاص" /></td>
    </tr>
    <%} %>
    <%} %>

    <tr class="border">
        <td colspan="4"></td>
    </tr>

    <%if (SystemEvent.Support_Custom_User_Email || SystemEvent.Support_Custom_User_SMS)
      { %>
    <tr>
        <td class="caption">ارسال به کاربر خاص</td>
        <td class="control">
            <kfk:LookupCombo ID="LookupCombo3" runat="server" DefaultItemValue="-1" AddDefaultItem="true" BusinessTypeName="pgc.Business.Lookup.UserLookupBusiness" AddUserIDParameter="true" DefaultItemText="" />
        </td>
        <%if (SystemEvent.Support_Custom_User_Email)
          { %>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox5" runat="server" Text="ارسال ایمیل به کاربر خاص" /></td>
        <td colspan="2"></td>
        <%}
          else
          { %>
        <td></td>
        <td></td>
        <%} %>
    </tr>
    <%if (SystemEvent.Support_Custom_User_SMS)
      { %>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox6" runat="server" Text="ارسال پیامک به کاربر خاص" /></td>
    </tr>
    <%} %>
    <%} %>
    <%} %>


    <tr class="border">
        <td colspan="4"></td>
    </tr>

    <%if (SystemEvent.Support_Related_Guest_Email)
      { %>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox7" runat="server" Text="ارسال ایمیل به مهمان مربوطه" /></td>
    </tr>
    <%} %>
    <%if (SystemEvent.Support_Related_User_Email)
      { %>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox8" runat="server" Text="ارسال ایمیل به کاربر مربوطه" /></td>
    </tr>
    <%} %>

    <%if (SystemEvent.Support_Related_Branch_Email)
      {%>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox9" runat="server" Text="ارسال ایمیل به نمایندگی مربوطه" /></td>
    </tr>
    <%} %>
    <%if (SystemEvent.Support_Related_Doer_Email)
      {%>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox10" runat="server" Text="ارسال ایمیل به انجام دهنده مربوطه" /></td>
    </tr>
    <%} %>

    <%if (SystemEvent.Support_Manual_Email)
      {%>
    <tr>
        <td class="caption">ارسال ایمیل به آدرس های<br />
            (هر آدرس در یک خط)</td>
        <td class="control" colspan="3">
            <kfk:NormalTextBox TextMode="MultiLine" ID="NormalTextBox1" runat="server" />
        </td>
    </tr>
    <%} %>

    <%if (SystemEvent.Support_Related_Branch_Email ||
                SystemEvent.Support_Related_Doer_Email ||
                SystemEvent.Support_Custom_User_Email ||
                SystemEvent.Support_Custom_AccessLevel_Email ||
                SystemEvent.Support_Custom_Role_Email ||
                SystemEvent.Support_Manual_Email)
      {%>
    <tr>
        <td class="caption">متن ایمیل ارسالی به مدیر</td>
        <td class="control">
            <kfk:HtmlEditor ID="HtmlEditor1" runat="server" />
        </td>

        <td class="control">
            <input type="button" value="<<" onclick="javascript: moveAlias(this)" /></td>
        <td class="control">
            <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox></td>
    </tr>
    <%} %>
    <%if (SystemEvent.Support_Related_User_Email ||
                SystemEvent.Support_Related_Guest_Email ||
                SystemEvent.Support_Related_Guest_Email ||
                SystemEvent.Support_Custom_Role_Email ||
                SystemEvent.Support_Related_Branch_Email ||
                SystemEvent.Support_Related_User_Email)
      {%>
    <tr>
        <td class="caption">متن ایمیل ارسالی به کاربر</td>
        <td class="control">
            <kfk:HtmlEditor ID="HtmlEditor2" runat="server" />
        </td>

        <td class="control">
            <input type="button" value="<<" onclick="javascript: moveAlias(this)" /></td>
        <td class="control">
            <asp:ListBox ID="ListBox2" runat="server"></asp:ListBox></td>

    </tr>
    <%} %>



    <tr class="border">
        <td colspan="4"></td>
    </tr>



    <%if (SystemEvent.Support_Manual_SMS ||
                SystemEvent.Support_Custom_User_SMS ||
                SystemEvent.Support_Custom_Role_SMS ||
                SystemEvent.Support_Custom_AccessLevel_SMS ||
                SystemEvent.Support_Related_Doer_SMS ||
                SystemEvent.Support_Related_Guest_SMS ||
                SystemEvent.Support_Related_Branch_SMS ||
                SystemEvent.Support_Related_User_SMS)
      {%>
    <tr>
        <td class="caption">شماره اختصاصی ارسال پیامک</td>
        <td class="control" colspan="3">
            <kfk:LookupCombo ID="LookupCombo4"
                BusinessTypeName="pgc.Business.Lookup.PrivateNoLookupBusiness"
                runat="server" />
        </td>
    </tr>
    <%} %>

    <%if (SystemEvent.Support_Related_Guest_SMS)
      { %>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox11" runat="server" Text="ارسال پیامک به مهمان مربوطه" /></td>
    </tr>
    <%} %>
    <%if (SystemEvent.Support_Related_User_SMS)
      { %>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox12" runat="server" Text="ارسال پیامک به کاربر مربوطه" /></td>
    </tr>
    <%} %>

    <%if (SystemEvent.Support_Related_Branch_SMS)
      {%>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox13" runat="server" Text="ارسال پیامک به نمایندگی مربوطه" /></td>
    </tr>
    <%} %>
    <%if (SystemEvent.Support_Related_Doer_SMS)
      {%>
    <tr>
        <td colspan="2"></td>
        <td class="control" colspan="2">
            <asp:CheckBox ID="CheckBox14" runat="server" Text="ارسال پیامک به انجام دهنده مربوطه" /></td>
    </tr>
    <%} %>

    <%if (SystemEvent.Support_Manual_SMS)
      {%>
    <tr>
        <td class="caption">ارسال پیامک به شماره های<br />
            (هر شماره تلفن در یک خط)</td>
        <td class="control" colspan="3">
            <kfk:NormalTextBox TextMode="MultiLine" ID="NormalTextBox2" runat="server" />
        </td>
    </tr>
    <%} %>
    <%if (SystemEvent.Support_Related_Branch_SMS ||
                SystemEvent.Support_Related_Doer_SMS ||
                SystemEvent.Support_Custom_User_SMS ||
                SystemEvent.Support_Custom_AccessLevel_SMS ||
                SystemEvent.Support_Custom_Role_SMS ||
                SystemEvent.Support_Manual_SMS)
      {%>
    <tr>
        <td class="caption">متن پیامک ارسالی به مدیر</td>
        <td class="control">
            <uc1:MessageControl ID="MessageControl1" runat="server" />
        </td>

        <td class="control">
            <input type="button" value="<<" onclick="javascript: moveAlias(this)" /></td>
        <td class="control">
            <asp:ListBox ID="ListBox3" runat="server"></asp:ListBox></td>
    </tr>
    <%} %>
    <%if (SystemEvent.Support_Related_User_SMS ||
                SystemEvent.Support_Related_Guest_SMS ||
                SystemEvent.Support_Related_Guest_SMS ||
                SystemEvent.Support_Custom_Role_SMS ||
                SystemEvent.Support_Related_Branch_SMS ||
                SystemEvent.Support_Related_User_SMS)
      {%>
    <tr>
        <td class="caption">متن پیامک ارسالی به کاربر</td>
        <td class="control">
            <uc1:MessageControl ID="MessageControl2" runat="server" />
        </td>

        <td class="control">
            <input type="button" value="<<" onclick="javascript: moveAlias(this)" /></td>
        <td class="control">
            <asp:ListBox ID="ListBox4" runat="server"></asp:ListBox></td>
    </tr>
    <%} %>
</table>

<div class="commands" id="Div1" runat="server">
    <asp:Button runat="server" ID="Button1" Text="ذخیره رخداد" CssClass="large" OnClick="OnSave" CausesValidation="true" />
    <asp:Button runat="server" ID="Button2" Text="انصراف" CssClass="large" OnClick="OnCancel" CausesValidation="false" />
</div>
--%>
