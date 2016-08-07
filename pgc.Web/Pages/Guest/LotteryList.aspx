<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="LotteryList.aspx.cs" Inherits="Pages_Guest_LotteryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    
    <title>قرعه کشی ها - رستوران مستردیزی</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">
<div class="htmlcontent" style="width:170px; float:left; margin-top:70px;"><%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.LotteryConfig)%></div>
<div class="list-container" style="width:727px; float:right">
    <div class="LotteryTitle">قرعه کشی های در حال اجرا</div>
       <asp:ObjectDataSource   ID="odsLottery" 
                                runat="server" 
                                EnablePaging="True"
                                SelectCountMethod="Lottery_Count" 
                                SelectMethod="Lottery_List"
                                TypeName="pgc.Business.General.LotteryBusiness"
                                EnableViewState="false">
        </asp:ObjectDataSource>

    <!-- آیتم نمونه در لیست -->

        <asp:ListView ID="lsvLottery" runat="server" DataSourceID="odsLottery" EnableViewState="false">
          <ItemTemplate>
           <%-- <%#if(Eval("Status")== (int)pgc.Model.Enums.LotteryStatus.flow){%>--%>
                <div class="listItemHolder-singlerow">
                    <div class="listItemHolderTitleHolder">
                      <div class="BMitra listItemHolderTitle">
                        <a href="<%#GetRouteUrl("guest-lotterydetail",new{id=Eval("ID")})%>"><%#Eval("Title")%></a>
                      </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>

    <!-- Pager -->
          <%if(dprLottery.TotalRowCount > dprLottery.MaximumRows) {%>
            <div class="fontClass pagination">
                <span class="pages-label">صفحات دیگر: </span> 
               <asp:DataPager ID="dprLottery" runat="server" PagedControlID="lsvLottery" PageSize="10" QueryStringField="page">
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
          <div class="LotteryTitle">آرشیو قرعه کشی ها</div>
          <asp:ObjectDataSource   ID="odsLotteryComplate" 
                                runat="server" 
                                EnablePaging="True"
                                SelectCountMethod="LotteryCompate_Count" 
                                SelectMethod="LotteryCompate_List"
                                TypeName="pgc.Business.General.LotteryBusiness"
                                EnableViewState="false">
        </asp:ObjectDataSource>

    <!-- آیتم نمونه در لیست -->

       <asp:ListView ID="lsvLotteryComplate" runat="server" DataSourceID="odsLotteryComplate" EnableViewState="false">
          <ItemTemplate>
           <%-- <%#if(Eval("Status")== (int)pgc.Model.Enums.LotteryStatus.flow){%>--%>
                <div class="listItemHolder-singlerow">
                    <div class="listItemHolderTitleHolder">
                      <div class="BMitra listItemHolderTitle">
                        <a style="float:right; display:inline-block;" href="<%#GetRouteUrl("guest-lotterywiner",new{id=Eval("ID")})%>">نتایج <%#Eval("Title")%></a>
                        <%--<div style=" float:left;width:100px;"><a href=""  style="width:50px; color:#0E8D37;">برندگان</a></div>--%>
                      </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:ListView>

    <!-- Pager -->
          <%if (dprLotteryCompate.TotalRowCount > dprLotteryCompate.MaximumRows)
            {%>
            <div class="fontClass pagination">
                <span class="pages-label">صفحات دیگر: </span> 
               <asp:DataPager ID="dprLotteryCompate" runat="server" PagedControlID="lsvLotteryComplate" PageSize="10" QueryStringField="page">
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

