<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SiteMap.ascx.cs" Inherits="UserControls_Project_SiteMap" %>
<div id="sm-icon"></div>
<div id="sm">
    <%foreach(pgc.Model.SiteMapCat category in SiteMap.GetSiteMapCat())
      { %>
        <ul>
            <li class="heading"><a href="<%=category.NavigateUrl%>" <%=(category.IsBlank)?"target='_blank'":"target=''"%>><%=category.Title%></a></li>

             <%int count = 0;
               foreach(pgc.Model.SiteMapItem item in SiteMap.GetSiteMapItem(category.ID))
               { %>
                <%
                  count++;
                  if (count <=10)
                  {%>
                        <li><a href="<%=item.NavigateUrl %>" <%=(item.IsBlank)?"target='_blank'":"target=''"%>><%=item.Title%></a></li>
                        
                   <% 
                  }
                  else
                  {%>
                    <li class="see-all"><a href="<%=category.NavigateUrl%>">مشاهده همه</a></li>
                
                <%break;
                  } %>

             <%} %>
        </ul>
    <%} %>
    <div id="sm-social">
        <%--<a href="#"><img src="/userfiles/socials/social-gp.png" alt="pgcizi in Google Plus"/></a>
        <a href="#"><img src="/userfiles/socials/social-fb.png" alt="pgcizi Facebook"/></a>
        <a href="#"><img src="/userfiles/socials/social-tw.png" alt="pgcizi Twitter"/></a>
        <a href="#"><img src="/userfiles/socials/social-rss.png" alt="pgcizi RSS"/></a>--%>
        <%=kFrameWork.Business.OptionBusiness.GetHtml(pgc.Model.Enums.OptionKey.Master_SiteMap_Social) %>
    </div>
</div>
