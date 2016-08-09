<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/ControlPanel.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Agent_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_content" runat="Server">
    <br />
    <br />
    <b>به پنل نمایندگان Iran PGC خوش آمدید ، </b>
    <br />
    <br />
    <b style="color: #A81C1B;">نام و نام خانوادگی: <%=kFrameWork.UI.UserSession.User.FullName%></b>
    <br />
    <br />
    <b style="color: #A81C1B;">پست الکترونیک: <%=kFrameWork.UI.UserSession.User.Email%></b>
    <br />
    <br />
    <b style="color: #A81C1B;">نام شعبه: <%=kFrameWork.UI.UserSession.User.Branch.Title%></b>
    <br />
    <br />
    <br />
    <%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.Agent_GreetingMessage) %>
    <br />
    <br />
    شما می توانید از جعبه ابزار راست صفحه برای رجوع به صفحات کاربری استفاده نمائید.
    <%if (business.RetriveLastCircular().Count()>0)
      {%>
           <div id="last-circular">
        <div id="last-circular-header">
            <i class="fa fa-list-alt"></i>
            <span>آخرین بخشنامه های مستردیزی</span>
        </div>
        <div>
            <table class="Table" cellpadding="0" cellspacing="0">
                <%foreach (var item in business.RetriveLastCircular())
                  {%>
            
                <tr class="circular-row">                  
                    <td><a href="<%=GetRouteUrl("agent-displaycircular",null)+"?id="+item.ID %>"><%=item.Title %></a></td>
                    <td><a href="<%=GetRouteUrl("agent-displaycircular",null)+"?id="+item.ID %>"><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(item.Date) %></a></td>
                    <td class="circular-text"><a href="<%=GetRouteUrl("agent-displaycircular",null)+"?id="+item.ID %>"><%=item.Body %></a></td>
                </tr>
                <%} %>
            </table>
        </div>
    </div>
     <% } %>
   
</asp:Content>

