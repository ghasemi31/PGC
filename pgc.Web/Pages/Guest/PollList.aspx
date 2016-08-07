<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="PollList.aspx.cs" Inherits="Pages_Guest_PollList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <title>نظرسنجی - رستوران مستردیزی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">

<div class="list-container">
       <asp:ObjectDataSource   ID="odsPoll" 
                                runat="server" 
                                EnablePaging="True"
                                SelectCountMethod="Poll_Count" 
                                SelectMethod="Poll_List"
                                TypeName="pgc.Business.General.PollBusiness"
                                EnableViewState="false">
        </asp:ObjectDataSource>

    <!-- آیتم نمونه در لیست -->
   <asp:ListView ID="lsvPoll" runat="server" DataSourceID="odsPoll" EnableViewState="false">
      <ItemTemplate>
            <div class="listItemHolder-singlerow">
                <div class="listItemHolderTitleHolder">
                  <div class="BMitra listItemHolderTitle">
                    <a href="<%#GetRouteUrl("guest-pollchoise",new{id=Eval("ID")})%>"><%#Eval("Title")%></a>
                  </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>

    <!-- Pager -->
          <%if(dprPoll.TotalRowCount > dprPoll.MaximumRows) {%>
            <div class="fontClass pagination">
                <span class="pages-label">صفحات دیگر: </span> 
               <asp:DataPager ID="dprPoll" runat="server" PagedControlID="lsvPoll" PageSize="10" QueryStringField="page">
                   <Fields>
                      <asp:NextPreviousPagerField PreviousPageText="قبلی" ButtonCssClass="button prev" ShowNextPageButton="false"/>
                      <asp:TemplatePagerField><PagerTemplate><span class="pages"></PagerTemplate></asp:TemplatePagerField>
                          <asp:NumericPagerField ButtonCount="6" NumericButtonCssClass="page" NextPreviousButtonCssClass="page" CurrentPageLabelCssClass="current" />
                      <asp:TemplatePagerField><PagerTemplate></span></PagerTemplate></asp:TemplatePagerField>
                      <asp:NextPreviousPagerField NextPageText="بعدی" ButtonCssClass="button next" ShowPreviousPageButton="false"/>
                   </Fields>
               </asp:DataPager>
           </div>    
          <%} %>
</div>

</asp:Content>

