<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Products.aspx.cs" Inherits="Pages_Guest_Products" %>

<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="kfk" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="kFrameWork.Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = products.PageTitle; %>
    <meta name="description" content="<%=products.PageDescription %>" />
    <meta name="keywords" content="<%=products.PageKeywords %>" />

    <link href="/assets/global/plugins/social-master/assets/stylesheets/arthref.min.css" rel="stylesheet" />
    <!-- BEGIN PRODUCT PAGE STYLE -->
    <link href="/assets/css/Product/Product.min.css?v=2.2" rel="stylesheet" />
    <!-- END PRODUCT PAGE STYLE -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <!-- BEGIN CONTENT -->
    <section class="main-body">
        <!-- BEGIN PRODUCT PIC -->
        <section>
            <div class="container-fluid padding-left-right-0">
                <%if (products.ProductPicPath != null || products.ProductPicPath != string.Empty)
                  {%>
                <img class="width100" alt="<%=products.Title %>" src="<%=ResolveClientUrl(products.ProductPicPath) %>?height=600&width=1400&mode=cropandscale" />
                <% } %>
            </div>
        </section>
        <!-- END PRODUCT PIC -->
        <!-- BEGIN PRODUCT DETAILS -->
        <section id="product-details" style="direction: rtl !important">
            <div class="container">
                <div class="row">
                    <div class="col-md-offset-0 col-md-4 col-lg-offset-0 col-lg-4 col-sm-offset-0 col-sm-4 col-xs-offset-2 col-xs-8">
                        <div id="materials-slider">
                            <%foreach (Material item in material)
                              {%>
                            <div class="materials-item">
                                <img alt="<%=item.Title %>" src="<%=ResolveClientUrl(item.MaterialPicPath) %>?height=367&width=366&mode=cropandscale" />
                            </div>
                            <% } %>
                        </div>
                    </div>
                    <div class="col-md-8 col-lg-8 col-sm-8 col-xs-12">
                        <article>
                            <header>
                                <h1><%=products.Title %>  <span><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(products.Price/10) %>  تومان</span></h1>
                            </header>
                            <p>
                                <%=products.Body%>
                            </p>
                            <div class="row">
                                <div>
                                    <%if (products.AllowOnlineOrder && kFrameWork.Business.OptionBusiness.GetBoolean(pgc.Model.Enums.OptionKey.AllowOnlineOrdering))
                                      {%>
                                    <span>
                                        <span id="btn-plus"><i class="fa fa-plus"></i></span>
                                        <span id="counter">1</span>
                                        <span id="btn-minus"><i class="fa fa-minus"></i></span>
                                    </span>
                                    <a id="add-to-basket" href="<%=GetRouteUrl("guest-order",null) + "?id=" + products.ID+"&count="%>">اضافه  به سبد</a>
                                    <%} %>
                                    <%string pageUrl = HttpContext.Current.Request.Url.AbsoluteUri; %>
                                    <ul id="share-icon" class="list-inline float-left">
                                        <li><a href="http://www.facebook.com/sharer.php?u=<%=pageUrl %>&amp;t=<%=products.Title %>"><i class="fa fa-facebook-square" aria-hidden="true"></i></a></li>
                                        <li><a href="http://twitter.com/home?status=<%=products.Title %>%20<%=pageUrl %>"><i class="fa fa-twitter-square" aria-hidden="true"></i></a></li>
                                        <li><a href="https://plus.google.com/share?url=<%=pageUrl %>"><i class="fa fa-google-plus-square" aria-hidden="true"></i></a></li>
                                        <li><a href="https://telegram.me/share/url?url=<%=pageUrl %>"><i class="fa fa-paper-plane" aria-hidden="true"></i></a></li>
                                        <li><a class="shareSelector" href="javascript:;"><i class="fa fa-share-alt" aria-hidden="true"></i><span id="share-text">به اشتراک بگذارید</span></a></li>
                                    </ul>
                                </div>
                            </div>
                        </article>
                    </div>
                </div>
            </div>
        </section>
        <!-- END PRODUCT DETAILS -->
        <!-- BEGIN FOOD SLIDER -->
        <section class="container-fluid" style="direction: rtl !important">
            <div class="container" style="border-bottom: solid 1px #f2f0f0;">
                <div class="row">
                    <header id="food-slider-header">
                        <h6>شـاید از اینا هم خوشتون بیاد&nbsp;.&nbsp;.&nbsp;.</h6>
                    </header>
                    <div class="col-lg-offset-0 col-lg-12 col-md-offset-0 col-md-12 col-sm-offset-0 col-sm-12 col-xs-offset-1 col-xs-10">
                        <div id="food-slider">
                            <%foreach (Product item in foodSlider)
                              {%>
                            <div class="item">
                                <a class="disable-first-click" href="<%=GetRouteUrl("guest-products",new { id = item.ID ,title=item.Title.Replace(" ","-")})%>">
                                    <img alt="<%=item.Title %>" class="front-pic" src="<%=ResolveClientUrl(item.SliderProductPicPath) %>?height=355&width=280&mode=cropandscale" />
                                    <img alt="<%=item.Title %>" class="top-pic" src="<%=ResolveClientUrl(item.SliderHoverProductPicPath) %>?height=355&width=280&mode=cropandscale" />
                                </a>
                                <footer>
                                    <h1 style="direction: rtl"><%=item.Title %></h1>
                                    <h5><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(item.Price/10) %> تومان</h5>
                                    <%if (item.AllowOnlineOrder && kFrameWork.Business.OptionBusiness.GetBoolean(pgc.Model.Enums.OptionKey.AllowOnlineOrdering))
                                      {%>
                                    <a href="<%=GetRouteUrl("guest-order",null) + "?id=" + item.ID+"&count=1"%>"><i class="fa fa-shopping-basket"></i></a>
                                    <%} %>
                                </footer>
                            </div>
                            <%} %>
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <!-- END FOOD SLIDER -->
        <!-- BEGIN FEEDBACK SECTION -->
        <section id="feedback" style="direction: rtl !important">
            <div class="container">
                <div class="row">
                    <!-- BEGIN FEEDBACK FORM -->
                    <div class="<%=(comment.Count()!=0)? "col-md-4":"col-md-12"%>">
                        <header>
                            <h6>نظر شما ؟</h6>
                        </header>
                        <form runat="server">
                            <div id="feedback-form">
                                <div class="row">
                                    <div class="<%=(comment.Count()!=0)? "col-lg-12":"col-lg-4"%> form-items-wrap">
                                        <label class="form-label"><i class="fa fa-user"></i></label>
                                        <asp:TextBox ID="txtFullName" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server" autocomplete="off"></asp:TextBox>
                                        <hr>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator6" runat="server"
                                            ErrorMessage="لطفا نام و نام خانوادگی را وارد کنید." ControlToValidate="txtFullName"
                                            Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="<%=(comment.Count()!=0)? "col-lg-12":"col-lg-4"%> form-items-wrap">
                                        <label class="form-label"><i class="fa fa-envelope"></i></label>
                                        <asp:TextBox ID="txtEmail" placeholder="ایمیل " ClientIDMode="Static" runat="server" autocomplete="off"></asp:TextBox>
                                        <hr>
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator5" runat="server"
                                            ErrorMessage="لطفا پست الکترونیک را وارد کنید." ControlToValidate="txtEmail"
                                            Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                    <div class="<%=(comment.Count()!=0)? "col-lg-12":"col-lg-4"%> form-items-wrap">
                                        <asp:ScriptManager ID="sm" runat="server">
                                        </asp:ScriptManager>
                                        <asp:UpdatePanel ID="uplCaptcha" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12 padding-left-right-0">
                                                    <label><i class="fa fa-question-circle"></i></label>
                                                    <asp:TextBox ID="txtCaptcha" CssClass="input-text" placeholder="کد امنیتی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                    <hr />
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator1" runat="server"
                                                        ErrorMessage="لطفا کد امنیتی را وارد کنید." ControlToValidate="txtCaptcha"
                                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ErrorMessage="کد امنیتی اشتباه است" OnServerValidate="ValidateCaptcha"
                                                        runat="server" Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" Display="Dynamic" />
                                                </div>
                                                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12 padding-left-right-0">
                                                    <div id="captcha" class="display-center">
                                                        <kfk:CaptchaControl ID="Captcha2" runat="server"
                                                            CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                            CaptchaHeight="40" CaptchaWidth="120" CssClass="col-md-8"
                                                            CaptchaLineNoise="None" CaptchaMinTimeout="5"
                                                            CaptchaMaxTimeout="240" FontColor="#da251d" />
                                                        <input type="submit" runat="server" class="btn-captch" value="&#xf021;" onclick="" causesvalidation="false" />
                                                    </div>
                                                    <div class="guide-captcha">
                                                        <p>
                                                            CAPTCHA یا همان کپچا نرم افزاری آنلاین برای تولید سوالات و آزمون هایی است که انسان براحتی قادر به پاسخ گویی به آنها است ولی کامپیوترها در حال حاضر قادر به تشخیص و پاسخ به آنها نیستند.
                                                        </p>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12 form-items-wrap">
                                        <label class="form-label"><i class="fa fa-commenting"></i></label>
                                        <textarea runat="server" clientidmode="Static" id="txtBody" name="textarea" placeholder="متن پیام مورد نظر..."></textarea>
                                        <hr />
                                        <asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator2" runat="server"
                                            ErrorMessage="لطفا متن پیام را وارد کنید." ControlToValidate="txtBody"
                                            Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>
                            <div class="form-items-wrap">
                                <asp:Button ID="btnSave" CssClass="btn-save" runat="server" Text="ارسال کن" OnClick="btnSave_Click" />
                            </div>
                            <div class="clear"></div>
                            <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
                        </form>
                    </div>
                    <!-- END FEEDBACK FORM -->
                    <!-- BEGIN COMMENT SECTION -->
                    <%if (comment.Count() != 0)
                      {%>
                    <div id="comment-section" class="col-md-offset-0 col-md-8 col-lg-offset-0 col-lg-8 col-sm-offset-1 col-sm-11 col-xs-offset-1 col-xs-11">
                        <%foreach (Comment item in comment)
                          {
                        %>
                        <section class="comment">
                            <header>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-1 col-lg-1 col-sm-2 col-xs-2">
                                            <img alt="مستردیزی" src="/assets/global/images/user-avatar/avatar-default.png" />
                                        </div>
                                        <div class="col-md-4 col-lg-4 col-sm-5 col-xs-5 font-color paddin-top">
                                            <span><%=item.SenderName %></span>
                                        </div>
                                        <div class="col-md-3 col-lg-3 col-sm-5 col-xs-5 font-color paddin-top">
                                            <span><%=DateUtil.GetPersianDateWithTime(item.Date) %></span>
                                        </div>
                                        <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12 paddin-top direction-ltr">
                                            <button class="btn-dislike" data-id="<%=item.ID %>"><i class="fa fa-heart"></i></button>
                                            <button class="btn-like" data-id="<%=item.ID %>"><i class="fa fa-heart"></i></button>
                                            <span class="like-count" style="color: #da251d;"><%=(item.Like>0)?"+"+item.Like.ToString():item.Like.ToString() %></span>
                                        </div>
                                    </div>
                                </div>
                            </header>
                            <div class="comment-text">
                                <p>
                                    <%=item.Body %>
                                </p>
                                <hr />
                            </div>
                        </section>
                        <%
                          } %>
                    </div>
                    <%} %>
                    <!-- END COMMENT SECTION -->
                </div>
            </div>
        </section>
        <!-- END FEEDBACK SECTION -->
    </section>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">

    <script src="/assets/global/plugins/social-master/assets/javascripts/socialShare.js"></script>
    <!-- BEGIN PRODUCT PAGE SCRIPT -->
    <script src="/assets/js/Product/Product.min.js?v=2.2"></script>
    <!-- BEGIN PRODUCT PAGE SCRIPT -->

</asp:Content>

