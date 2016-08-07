<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="HelpDetail.aspx.cs" Inherits="Pages_Guest_HelpDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
    <title><%#help.Title%></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">

    <div id="menuItemHolder">
    
            <div id="firstLine"></div>
	        <div id="mohtavaHead">
	          <div id="PageTitle">
	            <div class="itemName fontClass BMitra" id="title"><%=help.Title%></div>
	          </div>
	        </div>

         <div class="fontClass" id="mohtavaBody">

            <div class="detail-content"><%=help.Body%></div>

         </div>
    
        <!-- قسمت پایین صفحات محتوا محور -->
        <div id="mohtavaFoot">
           <div id="Footnavigation">
                <!-- لینک به صفحه دلخواه -->
               <div class="navPage">
                    <div class="itemName" id="navTitle"><a href="#"><%=(help.Title.Count()>10)?help.Title.Substring(0,10) + "...":help.Title%></a></div>
               </div>
          
               <!-- لینک به صفحه نخست -->
               <div id="firstPage">
                   <div id="navPoint" >
                       <span class="detail">برای کمک به سئو اینجا متنی مرتبط با لینک نوشت این متن نمایش داده نمیوشد</span>
                   </div>
                   <div class="itemName" id="navTitle"><a href="<%=GetRouteUrl("guest-Helplist",null)%>">راهنما</a></div>
               </div>
           </div>
        </div>
    </div>
</asp:Content>

