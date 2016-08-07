<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="ReportList.aspx.cs" Inherits="Pages_Guest_ReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <title>گزارشات - رستوران مستردیزی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">

<div class="list-container">
       <asp:ObjectDataSource   ID="odsReport" 
                                runat="server" 
                                EnablePaging="True"
                                SelectCountMethod="Report_Count" 
                                SelectMethod="Report_List"
                                TypeName="pgc.Business.General.ReportBusiness"
                                EnableViewState="false">
        </asp:ObjectDataSource>

    <!-- آیتم نمونه در لیست -->
   <asp:ListView ID="lsvReport" runat="server" DataSourceID="odsReport" EnableViewState="false">
      <ItemTemplate>
            <div class="listItemHolder">
                <div class="fontClass listItemHolder_text"><%#Eval("Summary")%></div>
                <div class="postImage"><img src="<%#ResolveClientUrl(Eval("ThumbImageUrl").ToString())%>" alt="<%#Eval("Title")%>"/></div>
                <div class="gotoPost">
                    <a href="<%#GetRouteUrl("guest-reportdetail",new{id=Eval("ID")})%>">
                        <img src="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/listgoitem.png")%>"  alt="توصیح لینک هر آیتم" />
                    </a>
                </div>
                <div class="listItemHolderTitleHolder">
                  <div class="BMitra listItemHolderTitle">
                    <a href="<%#GetRouteUrl("guest-reportdetail",new { id = Eval("ID") })%>"><%#Eval("Title")%></a>
                  </div>
                </div>
            </div>
        </ItemTemplate>
    </asp:ListView>

    <!-- Pager -->
          <%if(dprReport.TotalRowCount > dprReport.MaximumRows) {%>
            <div class="fontClass pagination">
                <span class="pages-label">صفحات دیگر: </span> 
               <asp:DataPager ID="dprReport" runat="server" PagedControlID="lsvReport" PageSize="7" QueryStringField="page">
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

