<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="LotteryDetail.aspx.cs" Inherits="Pages_Guest_LotteryDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/Lottery.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/Mrform.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <title>شرکت در قرعه کشی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">

<asp:ScriptManager ScriptMode="Release" ID="scmMain" runat="server" AsyncPostBackTimeout="0"></asp:ScriptManager>
<asp:UpdatePanel ID="uplMain" runat="server">
    <ContentTemplate>
        <div id="menuItemHolder">
            
            <div id="firstLine"></div>
	        <div id="mohtavaHead">
	          <div id="PageTitle">
	            <div class="itemName fontClass BMitra" id="title"> شرکت در  : <%=Lottery.Title %> </div>
	          </div>
	        </div>
            <div class="fontClass" id="mohtavaBody">
            <div class="help-hint" style="top:30px;">
                <div class="text"><%=Lottery.Description%></div>
                <div class="fontClass title">توضیحات</div>
            </div>
            <div id="formElement" style="margin-top:50px; margin-right:50px; width:700px;">
              <table>
                <tr>
                    <td class="caption">نام:</td> 
                    <td> 
                        <input type="text" runat="server" ID="txtFName" style="color:#000;" value="" />
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="لطفا نام خود را وارد نمایید" ControlToValidate="txtFName" 
                            Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td> 
                </tr>

                <tr>
                    <td class="caption">نام خانوادگی:</td>
                    <td> 
                        <input type="text" runat="server" id="txtLName" value="" style="color:#000;" />
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="لطفا نام خانوادگی خود را وارد نمایید" ControlToValidate="txtLName" 
                            Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Display="Dynamic">
                        </asp:RequiredFieldValidator>                        
                    </td>
                </tr>
                <tr>
                    <td class="caption">ایمیل:</td>
                    <td> 
                        <input type="text" runat="server" id="txtEmail" value="" style="color:#000;" />
                        <asp:RequiredFieldValidator
                            ID="RequiredEmail" runat="server" 
                            ErrorMessage="لطفا پست الکترونیک خود را وارد نمایید" ControlToValidate="txtEmail" 
                            Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Display="Dynamic">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ErrorMessage="پست الکترونیک معتبر نمی باشد"  
                            Font-Names="Tahoma" ForeColor="#CC0000" ControlToValidate="txtEmail" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                            Display="Dynamic">
                            </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="caption">کد قرعه کشی:</td>
                    <td> 
                        <%--<kfk:NumericTextBox ID="txtCode" runat="server" Required="true"/>--%>
                        <input type="text" runat="server" id="txtCode" value="" style="color:#000;" />
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator4" runat="server" 
                            ErrorMessage="لطفا کد را وارد نمایید" ControlToValidate="txtCode" 
                            Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="caption" style=" vertical-align:top;">توضیحات</td>
                    <td height="75">
                        <textarea id="txtComment" runat="server" name="textarea" style="color:#000;" cols="3" rows="5" class="pgciziFrmTextarea" autoComplete="false" ></textarea>
                    </td>
                </tr>
                <tr>
                    <td class="caption"></td>
                    <td height="50">
                       
                        <asp:Button ID="btnSave" runat="server" Text="ثبت" CssClass="pgciziFrmBtn" 
                            onclick="btnSave_Click"  />
                    </td>
                </tr>
              </table>
             </div>

        </div>
            <div id="mohtavaFoot">
                <div id="Footnavigation">
                    <!-- لینک به صفحه دلخواه -->
                    <div class="navPage">
                        <div class="itemName" id="navTitle"><a href="#"><%=Lottery.Title %></a></div>
                    </div>
                    <!-- لینک به صفحه دلخواه -->
                    <div class="navPage">
                        <div class="itemName" id="navTitle"><a href="<%=GetRouteUrl("guest-lotterylist",null) %>">قرعه کشی ها</a></div>
                        <div id="navPoint" >
                            <span class="detail">برای کمک به سئو اینجا متنی مرتبط با لینک نوشت این متن نمایش داده نمیوشد</span>
                        </div>    
                    </div>
                    <!-- لینک به صفحه نخست -->
                    <div id="firstPage">
                        <div id="navPoint" >
                            <span class="detail">برای کمک به سئو اینجا متنی مرتبط با لینک نوشت این متن نمایش داده نمیوشد</span>
                        </div>
                        <div class="itemName" id="navTitle"><a href="#"><%--<a href="<%=GetRouteUrl("guest-newslist",null)%>" />--%>مستر دیزی</a></div>
                    </div>
                </div>
            </div>
    </ContentTemplate>
</asp:UpdatePanel>



</asp:Content>

