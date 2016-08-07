<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="BranchList.aspx.cs" Inherits="Pages_Guest_BranchList" %>

<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="kFrameWork.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = OptionBusiness.GetText(pgc.Model.Enums.OptionKey.BranchListTitle); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Description_BranchList) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Keywords_BranchList) %>" />
    <!-- BEGIN BRANCHES PAGE STYLE -->
    <link href="/assets/css/Branches/branches.min.css?v=2.2" rel="stylesheet" />
    <!-- END BRANCHES PAGE STYLE -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <!-- BEGIN CONTENT -->
    <section class="main-body">
        <!-- BEGIN HEADER (BRANCH LIST) -->
        <header>
            <div id="branch-list" class="col-md-offset-3 col-md-6 col-lg-offset-3 col-lg-6 col-sm-offset-0 col-sm-12 col-xs-offset-0 col-xs-12">
                <img alt="مستردیزی" id="right-img" class="hidden-xs" src="/assets/global/images/elemans.right.red.svg" />
                <span>شعب مستردیزی</span>
                <img alt="مستردیزی" id="left-img" class="hidden-xs" src="/assets/global/images/elemans.left.red.svg" />
            </div>
            <div class="clear"></div>
        </header>
        <!-- END HEADER (BRANCH LIST) -->
        <!-- BEGIN BRANCH LIST SLIDER -->
        <section id="bl-slider">
            <div class="container">
                <div class="row" style="margin: 0 1em;">
                    <div id="branch-list-slider">
                        <!--tehran-->

                        <%if (tehranBranch.Count() > 0)
                          {%>
                        <div class="branch-list-item">
                            <div class="overlay">
                                <span>برای فعال شدن قابلیت بزرگنمایی نقشه، بر روی نقشه کلیک کنید</span>
                            </div>
                            <iframe id="teh-map" class="width100" frameborder="0" src="<%=OptionBusiness.GetHtml(OptionKey.TehranMap) %>" height="400"></iframe>
                            <div class="row margin-left-right-0">
                                <%foreach (Branch item in tehranBranch)
                                  {%>
                                <div class="col-lg-5ths col-md-5ths col-sm-5ths col-xs-5ths">
                                    <a href="<%=GetRouteUrl("guest-branch", new { urlkey=item.UrlKey,title=item.Title.Replace(" ","-")})%>">
                                        <img class="width100" alt="<%=item.Title %>" src="<%=(!string.IsNullOrEmpty(item.ThumbListPath))?ResolveClientUrl(item.ThumbListPath):"/assets/global/images/branch-default.jpg" %>?height=216&width=330&mode=cropandscale" />
                                        <article>
                                            <header>
                                                <h1><%=item.Title %></h1>
                                            </header>
                                            <div class="branch-contact">
                                                <h5><i class="fa fa-phone"></i><%=item.PhoneNumbers.Replace("\n","-") %></h5>
                                                <span>
                                                    <%=item.Address %>
                                                </span>
                                            </div>
                                        </article>

                                    </a>
                                </div>
                                <%} %>
                                <div class="clear"></div>
                            </div>

                        </div>

                        <%} %>

                        <!--iran-->
                        <%if (iranBranch.Count() > 0)
                          {%>
                        <div class="branch-list-item">
                            <div class="overlay">
                                <span>برای فعال شدن قابلیت بزرگنمایی نقشه، بر روی نقشه کلیک کنید</span>
                            </div>
                            <iframe class="width100" frameborder="0" src="<%=OptionBusiness.GetHtml(OptionKey.IranMap) %>" height="400"></iframe>
                            <div class="row margin-left-right-0">
                                <%foreach (Branch item in iranBranch)
                                  {%>
                                <div class="col-lg-5ths col-md-5ths col-sm-5ths col-xs-5ths">
                                    <a href="<%=GetRouteUrl("guest-branch", new { urlkey=item.UrlKey,title=item.Title.Replace(" ","-")})%>">
                                        <img class="width100" alt="<%=item.Title %>" src="<%=(!string.IsNullOrEmpty(item.ThumbListPath))?ResolveClientUrl(item.ThumbListPath):"/assets/global/images/branch-default.jpg" %>?height=216&width=330&mode=cropandscale" />
                                        <article>
                                            <header>
                                                <h1><%=item.Title %></h1>
                                            </header>
                                            <div class="branch-contact">
                                                <h5><i class="fa fa-phone"></i><%=item.PhoneNumbers.Replace("\n","-") %></h5>
                                                <span>
                                                    <%=item.Address %>
                                                </span>
                                            </div>
                                        </article>
                                    </a>
                                </div>
                                <%} %>
                                <div class="clear"></div>
                            </div>
                        </div>
                        <%} %>

                        <!--world-->
                        <%if (worldBranch.Count() > 0)
                          {%>
                        <div class="branch-list-item">
                            rtghwrthr
                            <div class="overlay">
                                <span>برای فعال شدن قابلیت بزرگنمایی نقشه، بر روی نقشه کلیک کنید</span>
                            </div>
                            <iframe class="width100" frameborder="0" src="<%=OptionBusiness.GetHtml(OptionKey.WorldMap) %>" height="400"></iframe>
                            <div class="row margin-left-right-0">
                                <%foreach (Branch item in worldBranch)
                                  {%>
                                <div class="col-lg-5ths col-md-5ths col-sm-5ths col-xs-5ths">
                                    <a href="<%=GetRouteUrl("guest-branch", new { urlkey=item.UrlKey,title=item.Title.Replace(" ","-")})%>">
                                        <img class="width100" alt="<%=item.Title %>" src="<%=(!string.IsNullOrEmpty(item.ThumbListPath))?ResolveClientUrl(item.ThumbListPath):"/assets/global/images/branch-default.jpg" %>?height=216&width=330&mode=cropandscale" />
                                        <article>
                                            <header>
                                                <h1><%=item.Title %></h1>
                                            </header>
                                            <div class="branch-contact">
                                                <h1><i class="fa fa-phone"></i><%=item.PhoneNumbers.Replace("\n","-") %></h1>
                                                <span>
                                                    <%=item.Address %>
                                                </span>
                                            </div>
                                        </article>
                                    </a>
                                </div>
                                <%} %>
                                <div class="clear"></div>
                            </div>
                        </div>
                        <%} %>
                    </div>
                </div>
            </div>
            <div class="clear"></div>
        </section>
        <!-- END BRANCH LIST SLIDER -->
    </section>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <!-- BEGIN BRANCHES PAGE SCRIPT -->
    <script src="/assets/js/Branches/branches.min.js?v=2.2"></script>
    <!-- BEGIN BRANCHES PAGE SCRIPT -->
</asp:Content>

