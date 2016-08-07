<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="ReportDetail.aspx.cs" Inherits="Pages_Guest_ReportDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="<%#ResolveClientUrl("~/Pages/Guest/pgciziCss/content-common.css")%>" rel="stylesheet" type="text/css" media="screen" />
     <meta name="Keywords" content="<%#report.MetaKeyWords%>"/>
     <meta name="Description" content="<%#report.MetaDescription %>" />

    <title><%#report.Title%></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph" Runat="Server">

    <div id="menuItemHolder">
    
           <div id="firstLine"></div>
	       <div id="mohtavaHead">
	         <div id="PageTitle">
	           <div class="itemName fontClass BMitra" id="title"><%=report.Title%></div>
	         </div>
	       </div>

         <div class="fontClass" id="mohtavaBody">
         <%if (report.Note != string.Empty)
           { %>
            <div id="help" class="help-hint" style="top:30px;">
                <div id="helpText" class="text"><%=report.Note%></div>
                <div id="help_title">خلاصه</div>
            </div>
            <%} %>
            <div class="detail-content"><%=report.Body%></div>

         </div>
    
        <!-- قسمت پایین صفحات محتوا محور -->
        <div id="mohtavaFoot">
           <div id="Footnavigation">
                <!-- لینک به صفحه دلخواه -->
               <div class="navPage">
                    <div class="itemName" id="navTitle"><a href="#"><%=(report.Title.Count()>10)?report.Title.Substring(0,10) + "...":report.Title%></a></div>
               </div>
               <!-- لینک به صفحه دلخواه -->
               <div class="navPage">
                    <div class="itemName" id="navTitle"><a href="<%=GetRouteUrl("guest-reportlist",null)%>">گزارشات</a></div>
                    <div id="navPoint" >
                        <span class="detail">برای کمک به سئو اینجا متنی مرتبط با لینک نوشت این متن نمایش داده نمیوشد</span>
                    </div>    
               </div>
               <!-- لینک به صفحه نخست -->
               <div id="firstPage">
                   <div id="navPoint" >
                       <span class="detail">برای کمک به سئو اینجا متنی مرتبط با لینک نوشت این متن نمایش داده نمیوشد</span>
                   </div>
                   <div class="itemName" id="navTitle"><a href="#"><%--<a href="<%=GetRouteUrl("guest-Reportlist",null)%>" />--%>مستر دیزی</a></div>
               </div>
           </div>
        </div>
    </div>
</asp:Content>

