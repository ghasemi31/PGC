<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="PollChoise.aspx.cs" Inherits="Pages_Guest_PollChoise" %>
<%@ Import Namespace="pgc.Model"%>
<%@ Import Namespace="pgc.Business.General" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/Mrform.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <title></title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">

    <div id="menuItemHolder">
    
        <div id="firstLine"></div>
	    <div id="mohtavaHead">
	      <div id="PageTitle">
	        <div class="itemName fontClass BMitra" id="title"><%=poll.Title %></div>
	      </div>
	    </div>
        <div class="fontClass" id="mohtavaBody">
          <div id="help-pgcizi">
            <div id="helpText"><%=poll.Description%></div>
           <div class="fontClass" id="help_title">نظر سنجی</div>
          </div>

        

          <div id="formElement">
            <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
              <tr>
                <td class="radio-container" >
                    <asp:Panel runat="server" ID="pnlRadioContainer">
                    
                    </asp:Panel>
                    <%--<%foreach (PollChoise choise in business.GetChoise(poll.ID))
                      { %>                      
                        <input type="radio" id="rbChoise"   clientidmode="Static" name="<%=choise.Poll.Title%>" value="<%=choise.ID %>" />
                        <label for="rbChoise"><%=choise.Description %></label>
                       <%} %>--%>
                    <%--<asp:RadioButton ID="RadioButton1" runat="server" Text="sample text" GroupName="rbChoise"/>
                    <asp:RadioButton ID="RadioButton2" runat="server" Text="sample text2" GroupName="rbChoise"/>--%>
                    <asp:RadioButtonList ID="rblChoises" runat="server">
                    
                    </asp:RadioButtonList>
                </td>
              </tr>

              <tr>
                <td height="50">
                <%--<input type="submit" runat="server" name="button" class="pgciziFrmBtn" id="btnSave" value="ثبت" onclick="return btnSave_onclick()" />--%>
                 <asp:Button ID="btnSave" runat="server" Text="ثبت" CssClass="pgciziFrmBtn" 
                        onclick="btnSave_Click"/>
                   
                <%--<input type="reset" name="Reset" class="pgciziFrmBtn" id="btnReset" value="فرم جدید" />--%>                  
                </td>
              </tr>
            </table>
          </div>
        </div>
    
        <!-- قسمت پایین صفحات محتوا محور -->
        <div id="mohtavaFoot">
           <div id="Footnavigation">
                <!-- لینک به صفحه دلخواه -->
               <div class="navPage">
                    <div class="itemName" id="navTitle"><a href="#"><%=(poll.Title.Count()>20)?poll.Title.Substring(0,20) + "...":poll.Title%></a></div>
               </div>

               <!-- لینک به صفحه نخست -->
               <div id="firstPage">
                   <div id="navPoint" >
                       <span class="detail">برای کمک به سئو اینجا متنی مرتبط با لینک نوشت این متن نمایش داده نمیوشد</span>
                   </div>
                   <div class="itemName" id="navTitle"><a href="<%=GetRouteUrl("guest-polllist",null)%>">نظرسنجی</a></div>
               </div>
           </div>
        </div>
    
    </div>

</asp:Content>

