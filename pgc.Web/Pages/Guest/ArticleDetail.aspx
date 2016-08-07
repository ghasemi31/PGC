<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="ArticleDetail.aspx.cs" Inherits="Pages_Guest_ArticleDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
     <meta name="keywords" content="<%#article.MetaKeyWords%>"/>
     <meta name="description" content="<%#article.MetaDescription %>" />

    <title><%#article.Title%></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">

    <div id="menuItemHolder">
    
           <div id="firstLine"></div>
	       <div id="mohtavaHead">
	         <div id="PageTitle">
	           <div class="itemName fontClass BMitra" id="title"><%=article.Title%></div>
	         </div>
	       </div>

         <div class="fontClass" id="mohtavaBody">
            <%if (article.Note != string.Empty)
              { %>
            <div id="help" class="help-hint" style="top:30px;">
                <div id="helpText" class="text"><%=article.Note%></div>
                <div id="help_title">خلاصه</div>
            </div>
            <%} %>
            <div class="detail-content"><%=article.Body%></div>

         </div>
    
        <!-- قسمت پایین صفحات محتوا محور -->
        <div id="mohtavaFoot">
           <div id="Footnavigation">
                <!-- لینک به صفحه دلخواه -->
               <div class="navPage">
                    <div class="itemName" id="navTitle"><a href="#"><%=(article.Title.Count()>10)?article.Title.Substring(0,10) + "...":article.Title%></a></div>
               </div>
               <!-- لینک به صفحه دلخواه -->
               <div class="navPage">
                    <div class="itemName" id="navTitle"><a href="<%=GetRouteUrl("guest-articlelist",null)%>">مطالب</a></div>
                    <div id="navPoint" >
                        <span class="detail">برای کمک به سئو اینجا متنی مرتبط با لینک نوشت این متن نمایش داده نمیوشد</span>
                    </div>    
               </div>
               <!-- لینک به صفحه نخست -->
               <div id="firstPage">
                   <div id="navPoint" >
                       <span class="detail">برای کمک به سئو اینجا متنی مرتبط با لینک نوشت این متن نمایش داده نمیوشد</span>
                   </div>
                   <div class="itemName" id="navTitle"><a href="#"><%--<a href="<%=GetRouteUrl("guest-Articlelist",null)%>" />--%>مستر دیزی</a></div>
               </div>
           </div>
        </div>
    </div>
</asp:Content>

