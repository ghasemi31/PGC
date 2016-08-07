<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="LotteryWiner.aspx.cs" Inherits="Pages_Guest_LotteryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    
    <title>برندگان - رستوران مستردیزی</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">
<div id="menuItemHolder">
 <div id="firstLine"></div>
	        <div id="mohtavaHead">
	          <div id="PageTitle">
	            <div class="itemName fontClass BMitra" id="title"> برندگان : <%=Lottery.Title%>  </div>
	          </div>
	        </div>
            <div class="fontClass" id="mohtavaBody">
                <div class="help-hint" style="top:30px;">
                    <div class="text"><%=Lottery.Description %></div>
                    <div class="fontClass title">توضیحات</div>
                </div>
                <div class="list-container" style="margin-top:50px; width:777px;">

<%--                    <asp:ObjectDataSource   ID="odsLotteryWiner" 
                                runat="server" 
                                EnablePaging="false"
                                SelectCountMethod="LotteryWiner_Count" 
                                SelectMethod="LotteryWiner_List"
                                TypeName="pgc.Business.General.LotteryBusiness"
                                EnableViewState="false">
                    </asp:ObjectDataSource>--%>

    <!-- آیتم نمونه در لیست -->
<%--
                    <asp:ListView ID="lsvLotteryWiner" runat="server" DataSourceID="odsLotteryWiner" EnableViewState="false">
                      <ItemTemplate>--%>
                       <%-- <%#if(Eval("Status")== (int)pgc.Model.Enums.LotteryStatus.flow){%>--%>
                       <%foreach (pgc.Model.LotteryWiner winer in business.RetriveLotteryWiners(Lottery.ID))
                         { %>
                            <div class="listItemHolder-singlerow">
                                <div class="listItemHolderTitleHolder">
                                  <div class="BMitra listItemHolderTitle">
                                    <div class="rank" >رتبه :<%=winer.Rank %></div>                                 
                                    <div class="winers"><%=winer.FName + " " + winer.LName %></div>
                                    <div class="winers"><%=winer.Description %></div>
                                  </div>
                                </div>
                            </div>
                            <%} %>
<%--                        </ItemTemplate>
                    </asp:ListView>--%>


            </div>
          </div>
    <div id="mohtavaFoot">
                <div id="Footnavigation">
                    <!-- لینک به صفحه دلخواه -->
                    <div class="navPage">
                        <div class="itemName" id="navTitle"><a href="#"> <%=Lottery.Title %> </a></div>
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
</div>
</asp:Content>

