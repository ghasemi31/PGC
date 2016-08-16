<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Guest_Default" %>

<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="kFrameWork.Business" %>

<asp:Content ID="c1" ContentPlaceHolderID="head" runat="Server">
    <%this.Title = OptionBusiness.GetText(OptionKey.HomeTitle); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Description_Default) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Keyword_Default) %>" />
    <link href="/assets/Plugin/OwlCarousel2-master/dist/assets/owl.carousel.min.css" rel="stylesheet" />
    <link href="/assets/Plugin/OwlCarousel2-master/dist/assets/owl.theme.default.min.css" rel="stylesheet" />
    <link href="/assets/Plugin/animate.css-master/animate.min.css" rel="stylesheet" />
    <link href="/assets/Guest/css/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <section>
        <!-- slider -->
        <input type="hidden" id="slider-time" value="<%=OptionBusiness.GetDouble(OptionKey.FullPageSliderTimer)%>" />
        <input type="hidden" id="speed-slider-time" value="<%=OptionBusiness.GetDouble(OptionKey.FullPageSpeedSlider)%>" />
        <input type="hidden" id="slider-transaction" value="<%=OptionBusiness.GetText(OptionKey.Slider_transitionStyle) %>" />
        <section style="direction: ltr">

            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding-left-right-0">
                        <div class="col-md-12 adv">
                            <img class="img-thumbnail" src="<%=ResolveClientUrl(OptionBusiness.GetFilePath(OptionKey.Adv_Img_1))%>?width=335&height=120&mode=cropandscale" />
                        </div>
                        <div class="col-md-12 adv">
                            <img class="img-thumbnail" src="<%=ResolveClientUrl(OptionBusiness.GetFilePath(OptionKey.Adv_Img_2))%>?width=335&height=120&mode=cropandscale" />
                        </div>
                        <div class="col-md-12 adv">
                            <img class="img-thumbnail" src="<%=ResolveClientUrl(OptionBusiness.GetFilePath(OptionKey.Adv_Img_3))%>?width=335&height=120&mode=cropandscale" />
                        </div>
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding-left-right-0">
                        <%--<div>
                            <img src="/assets/Guest/Image/2222.jpg" />
                        </div>--%>
                        <div id="mainSlider" class="owl-carousel">
                            <%foreach (var item in DBusiness.GetMainSlider())
                              {
                                  var imgPath = ResolveClientUrl(item.ImgPath);
                            %>
                            <%-- <div class="owl-slide" style="background-image: url(<%=imgPath%>)"></div>--%>
                            <div class="mainSlider-item">
                                <img class="img-thumbnail" src="<%=ResolveClientUrl(item.ImgPath)%>?width=663&height=368&mode=cropandscale" />
                            </div>
                            <%} %>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 padding-left-right-0">
                        <div class="col-md-12 adv">
                            <img class="img-thumbnail" src="<%=ResolveClientUrl(OptionBusiness.GetFilePath(OptionKey.Adv_Img_4))%>?width=335&height=120&mode=cropandscale" />
                        </div>
                        <div class="col-md-12 adv">
                            <img class="img-thumbnail" src="<%=ResolveClientUrl(OptionBusiness.GetFilePath(OptionKey.Adv_Img_5))%>?width=335&height=120&mode=cropandscale" />
                        </div>
                        <div class="col-md-12 adv">
                            <img class="img-thumbnail" src="<%=ResolveClientUrl(OptionBusiness.GetFilePath(OptionKey.Adv_Img_6))%>?width=335&height=120&mode=cropandscale" />
                        </div>
                    </div>
                </div>
            </div>


        </section>
        <!-- slider -->
        <section id="content" class="home clearfix">
            <!--begin game logo slider -->
            <section class="topBox">
                <div class="container">
                    <div class="row margin-left-right-0">
                        <div id="game-slider" class="owl-carousel">
                            <%foreach (var item in DBusiness.GetGameLogos())
                              {%>
                            <div class="game-logo-item">
                                <a href="<%=GetRouteUrl("guest-gamedetail", new { urlkey=item.UrlKey})%>">
                                    <img src="<%=ResolveClientUrl(item.LogoPath) %>" alt="<%=item.Title %>" />
                                </a>
                            </div>


                            <% } %>
                        </div>
                    </div>
                </div>
            </section>
            <!--end game logo slider -->


            <!-- tabs -->
            <section id="pic-fixed" class="slice color3">
                <div class="container">
                    <div class="row x">
                        <div class="col-sm-5 col-md-5">
                            <img class="pgc-poster" src="<%=OptionBusiness.GetText(OptionKey.Hamrah_Dizi_Pic).Replace("~","") %>" />
                        </div>
                        <div class="col-sm-7 col-md-7">
                            <div class="row">
                                <img src="/assets/Guest/Image/IranPGC/05.png" />
                            </div>
                            <div id="quality" class="row x">
                                <p><%=OptionBusiness.GetText(OptionKey.Quality_Charter) %></p>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- tabs -->
            <!--bubbles--->
            <section class="slice color2  roundedShadow">
                <div class="container">
                    <div class="row">
                        <div class="col-md-7 pgc-title">
                            <div class="row margin-left-right-0">
                                <h1>حامیان معنوی</h1>
                                <hr />
                                <div id="supporter-slider" class="owl-carousel">
                                    <%foreach (var item in new pgc.Business.SupportBusiness().getAllSupport())
                                      { %>
                                    <div class="supporter-item">
                                        <a href="<%=item.Link %>">
                                            <img src="<%=item.ImagePath.Replace("~","") %>" />
                                        </a>
                                    </div>
                                    <%} %>
                                </div>
                            </div>
                            <div class="row margin-left-right-0" style="margin-top: 2em;">
                                <h1>اسپانسرها</h1>
                                <hr />
                                <div id="sponsor-slider" class="owl-carousel">
                                    <%foreach (var item in new pgc.Business.SponsorBusiness().getAllSponsors())
                                      { %>
                                    <div class="sponsor-item">
                                        <a href="<%=item.Link %>">
                                            <img src="<%=item.ImagePath.Replace("~","") %>" />
                                        </a>
                                    </div>
                                    <%} %>
                                </div>
                            </div>

                        </div>
                        <div id="news" class="col-md-5 pgc-title">
                            <h1>آخرین اخبار</h1>
                            <hr />
                            <div id="news-slider" class="owl-carousel">
                                <%foreach (var item in DBusiness.GetLastNews())
                                  {%>
                                <div class="news-item">
                                    <img alt="<%=item.Title %>" src="<%=(!string.IsNullOrEmpty(item.NewsPicPath))?ResolveClientUrl(item.NewsPicPath):"/assets/global/images/branch-default.jpg" %>?height=410&width=436&mode=cropandscale" />
                                    <a href="<%=GetRouteUrl("guest-newsdetail",new { id = item.ID,title=item.Title.Replace(" ","-") })%>">
                                        <article class="news-detail">
                                            <h4><%=item.Title %></h4>
                                            <p><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(item.NewsDate) %></p>
                                            <p><%=item.Summary %></p>
                                        </article>
                                    </a>
                                </div>
                                <%} %>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!--bubbles--->
        </section>
        <!-- content -->
        <div class="clear"></div>
    </section>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
    <script src="/assets/Plugin/OwlCarousel2-master/dist/owl.carousel.min.js"></script>
    <script src="/assets/Guest/js/default.js?v=1"></script>
</asp:Content>

