<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="FaqList.aspx.cs" Inherits="Pages_Guest_FaqList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <title>لیست پرسش ها - رستوران مستردیزی</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">
    
    <div class="list-container">
        <asp:ObjectDataSource   ID="odsFaq" 
                                runat="server" 
                                EnablePaging="True"
                                SelectCountMethod="Faq_Count" 
                                SelectMethod="Faq_List"
                                TypeName="pgc.Business.General.CmsBusiness"
                                EnableViewState="false">
        </asp:ObjectDataSource>

        <asp:ListView ID="lsvFaq" runat="server" DataSourceID="odsFaq" EnableViewState="false">
              <ItemTemplate>
                  <div class="listItemHolder-singledata">
                    <div class="fontClass listItemHolder_text"><%#Eval("Summery")%>></div>           
                        <div class="gotoPost">
                            <a href="<%#GetRouteUrl("guest-faqdetail",new{id=Eval("ID")})%>">
                                <img src="<%=ResolveClientUrl("~/Pages/Guest/pgciziPix/listgoitem.png")%>"  alt="توصیح لینک هر آیتم" />
                            </a>
                        </div>
                        <div class="listItemHolderTitleHolder">
                          <div class="BMitra listItemHolderTitle">
                            <a href="<%#GetRouteUrl("guest-faqdetail",new { id = Eval("ID") })%>"><%#Eval("Title")%></a>
                          </div>
                        </div>
                    </div>
                </ItemTemplate>
         </asp:ListView>

         <!-- Pager -->
         <%if(dprFaq.TotalRowCount > dprFaq.MaximumRows) {%>
            <div class="fontClass pagination">
                <span class="pages-label">صفحات دیگر: </span> 
               <asp:DataPager ID="dprFaq" runat="server" PagedControlID="lsvFaq" PageSize="7" QueryStringField="page">
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

