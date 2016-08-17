<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="GameList.aspx.cs" Inherits="Pages_Guest_GameList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/assets/Guest/css/GameList.css?v=2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <section id="page">
        <header id="gamelist-header"></header>
        <section id="main-content">
            <div class="container">
                <div class="row">
                    <%foreach (var item in games)
                      {%>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12 game">
                        <a href="<%=GetRouteUrl("guest-gamedetail", new { urlkey=item.UrlKey})%>">
                            <img class="img-thumbnail" src="<%=ResolveClientUrl(item.ImagePath) %>?width=260&height=220&mode=cropandscale" />
                            <h1><%=item.Title %></h1>
                        </a>
                    </div>
                    <%} %>                    
                </div>
            </div>
        </section>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
</asp:Content>

