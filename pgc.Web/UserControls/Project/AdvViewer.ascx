<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdvViewer.ascx.cs" Inherits="UserControls_Project_AdvViewer" %>
<%if(adv.AdvType==1){ %>
    <div class="adv-image" <% %>style="<%=(adv.MarginBottom>0)?"margin-bottom:"+adv.MarginBottom+"px;":""%><%=(adv.MarginLeft>0)?"margin-left:"+adv.MarginLeft+"px;":"" %><%=(adv.MarginRight>0)?"margin-right:"+adv.MarginRight+"px;":"" %><%=(adv.MarginTop>0)?"margin-top:"+adv.MarginTop+"px;":"" %>">

        <%if(adv.NavigateUrl!=""){ %>
            <a href="<%=adv.NavigateUrl%>" target="_blank">
        <%} %>
          
                <img src="<%=ResolveClientUrl(adv.FileAddress)%>"  alt="<%=adv.AltText%>" <%=(adv.Width>0)?"width=\""+adv.Width+"\"": ""%>  <%=(adv.Height>0)?"Height=\""+adv.Height+"\"": ""%> style="border:0;"/>
            
        <%if(adv.NavigateUrl!=""){ %>
        </a>
        <%} %>
    </div>
<%} else if(adv.AdvType==2){ %>
    <div  class="adv-flash" <% %>style="<%=(adv.MarginBottom>0)?"margin-bottom:"+adv.MarginBottom+"px;":""%><%=(adv.MarginLeft>0)?"margin-left:"+adv.MarginLeft+"px;":"" %><%=(adv.MarginRight>0)?"margin-right:"+adv.MarginRight+"px;":"" %><%=(adv.MarginTop>0)?"margin-top:"+adv.MarginTop+"px;":"" %>" >
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" 
                codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0" 
                width="<%=adv.Width%>" 
                height="<%=adv.Height%>"> 
            <param name="movie" value="<%=ResolveClientUrl(adv.FileAddress)%>" /> 
            <param name="quality" value="high" /> 
            <param name="wmode" value="transparent" /> 
            <!--[if !IE]> <--> 
                <object data="<%=ResolveClientUrl(adv.FileAddress)%>" width="<%=adv.Width%>" height="<%=adv.Height%>" type="application/x-shockwave-flash">
                    <param name="quality" value="high" /> 
                    <param name="wmode" value="transparent" />
                    <param name="pluginurl" value="http://adobe.com/go/getflashplayer" />
                    <%-- <a href="http://www.lta.ir/searchresult.aspx?qs=5755g" target="_blank">Acer 5755G Laptops</a> --%>
                    <%=adv.AltText%>
                </object> 
            <!--> <![endif]--> 
        </object>
    </div>
 <%} else if(adv.AdvType==3){ %>
    <div class="adv-html" <% %>style="<%=(adv.MarginBottom>0)?"margin-bottom:"+adv.MarginBottom+"px;":""%><%=(adv.MarginLeft>0)?"margin-left:"+adv.MarginLeft+"px;":"" %><%=(adv.MarginRight>0)?"margin-right:"+adv.MarginRight+"px;":"" %><%=(adv.MarginTop>0)?"margin-top:"+adv.MarginTop+"px;":"" %>">
        <%=adv.Html%>
    </div>
 <%} %>