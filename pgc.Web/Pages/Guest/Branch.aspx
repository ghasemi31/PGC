<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Branch.aspx.cs" Inherits="Pages_Guest_Branch" %>


<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="kfk" %>
<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="kFrameWork.Util" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">
    <%this.Title = branch.PageTitle; %>
    <meta name="description" content="<%=branch.PageDescription %>" />
    <meta name="keywords" content="<%=branch.PageKeywords %>" />
    <!-- BEGIN BRANCH-DETAILS PAGE STYLE -->
    <link href="/assets/css/Branch-details/Branch-Details.min.css?v=2.2" rel="stylesheet" />
    <!-- END BRANCH-DETAILS PAGE STYLE -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">

    <!-- BEGIN CONTENT -->
    <form runat="server">
        <asp:ScriptManager ID="sm" runat="server">
        </asp:ScriptManager>
        <section class="main-body">
            <!-- BEGIN HEADER (BRANCH NAME) -->
            <header>
                <div id="branch-name" class="col-md-offset-3 col-md-6 col-lg-offset-3 col-lg-6 col-sm-offset-0 col-sm-12 col-xs-offset-0 col-xs-12">
                    <img alt="مستردیزی" id="right-img" src="/assets/global/images/elemans.right.red.svg" />
                    <span><%=branch.Title %></span>
                    <img alt="مستردیزی" id="left-img" src="/assets/global/images/elemans.left.red.svg" />
                </div>
                <div class="clear"></div>
            </header>
            <!-- END HEADER (BRANCH NAME) -->
            <!-- BEGIN BRANCH PHOTO SLIDER -->
            <section>
                <div class="container">
                    <!--BEGIN SLIDER FOR SMALL SCREEN -->
                    <div class="row hidden-lg hidden-md">
                        <div id="small-screen">
                            <div id="branch-photo-slider-small-screen">
                                <%foreach (BranchPic item in business.GetThumbBranchPic(branch.ID))
                                  {%>
                                <div class="branch-item">
                                    <img alt="<%=item.Branch.Title %>" src="<%=ResolveClientUrl(item.ImagePath)%>?height=530&width=900&mode=cropandscale" />
                                </div>
                                <% } %>
                            </div>
                        </div>
                    </div>
                    <!--END SLIDER FOR SMALL SCREEN -->
                    <!--BEGIN SLIDER FOR LARGE SCREEN -->
                    <div class="row hidden-sm hidden-xs" style="margin: 0 50px;">
                        <div id="large-screen" style="margin-left: 130px;">
                            <div id="branch-photo-slider">
                                <%foreach (BranchPic item in business.GetThumbBranchPic(branch.ID))
                                  {%>
                                <div class="branch-item">
                                    <img alt="<%=item.Branch.Title %>" class="lazyOwl" data-src="<%=ResolveClientUrl(item.ImagePath)%>?height=530&width=900&mode=cropandscale" data-img-original="<%=ResolveClientUrl(item.ImagePath)%>?height=530&width=900&mode=cropandscale" />
                                </div>
                                <% } %>
                            </div>
                        </div>
                    </div>
                    <!--END SLIDER FOR LARGE SCREEN -->
                </div>
            </section>
            <!-- END BRANCH PHOTO SLIDER -->
            <!-- BEGIN BRANCH INFO -->
            <section>
                <div class="container">
                    <div id="branch-tab" class="row">
                        <ul class="nav nav-tabs">
                            <li><a data-toggle="tab" href="#branch-info"><i class="fa fa-info-circle"></i>اطلاعات شعبه</a></li>
                            <li class="active"><a data-toggle="tab" href="#branch-map"><i class="fa fa-map-marker"></i>آدرس شعبه</a></li>
                            <li><a data-toggle="tab" href="#user-comment"><i class="fa fa-comments vertical-middle"></i>نظرات شما</a></li>
                            <li><a data-toggle="tab" href="#branch-contact"><i class="fa fa-envelope vertical-middle"></i>ارتباط با شعبه</a></li>
                        </ul>
                        <div class="tab-content">
                            <div id="branch-info" class="tab-pane fade">
                                <ul>
                                    <li>
                                        <img alt="آدرس شعبه" src="/assets/Images/branch-details/tabs-content/cottage-orange.png" />
                                        <span class="title-bg-color">آدرس: </span>
                                        <span><%=branch.Address %></span>
                                    </li>

                                    <li>
                                        <img alt="تلفن شعبه" src="/assets/images/branch-details/tabs-content/phone-orange.png">
                                        <span class="title-bg-color">تلفن: </span>
                                        <span><%=(!string.IsNullOrEmpty(branch.PhoneNumbers)?branch.PhoneNumbers.Replace("\n","-"):"-") %></span>
                                    </li>

                                    <li>
                                        <img alt="ساعت سفارش گیری شعبه" src="/assets/images/branch-details/tabs-content/people-orange.png">
                                        <span class="title-bg-color">ساعات سفارش گیری: </span>
                                        <span><%=branch.HoursOrdering %></span>
                                    </li>

                                    <li>
                                        <img alt="ساعت سرو غذا شعبه" src="/assets/images/branch-details/tabs-content/serv-orange.png">
                                        <span class="title-bg-color">ساعات سرو غذا: </span>
                                        <span><%=branch.HoursServingFood %></span>
                                    </li>

                                    <li>
                                        <img alt="ظرفیت شعبه" src="/assets/images/branch-details/tabs-content/chairs-orange.png">
                                        <span class="title-bg-color">تعداد صندلی:</span>
                                        <span><%=branch.NumberOfChair%> عدد</span>
                                    </li>

                                    <li>
                                        <img alt="هزینه پیک شعبه" src="/assets/images/branch-details/tabs-content/truck-orange.png">
                                        <span class="title-bg-color">هزینه حمل و نقل:</span>
                                        <span><%=branch.TransportCost %></span>
                                    </li>

                                </ul>
                            </div>
                            <div id="branch-map" class="tab-pane fade in active">
                                <div id="branch-map-details">
                                    <input id="branchLongitude" type="hidden" value="<%=branch.Longitude %>" />
                                    <input id="branchLatitude" type="hidden" value="<%=branch.latitude %>" />
                                    <input id="branchTitle" type="hidden" value="<%=branch.Title %>" />
                                    <%--<div class="overlay">
                                        <span>برای فعال شدن قابلیت بزرگنمایی نقشه، بر روی نقشه کلیک کنید</span>
                                    </div>--%>
                                    <div id="map_canvas" class="col-lg-12"></div>
                                </div>
                            </div>
                            <div id="user-comment" class="tab-pane fade">
                                <!-- BEGIN FEEDBACK SECTION -->
                                <section id="feedback">
                                    <!-- BEGIN FEEDBACK FORM -->
                                    <div class="<%=(comment.Count()!=0)? "col-md-4":"col-md-12"%>">
                                        <header>
                                            <h6>نظر شما ؟</h6>
                                        </header>
                                        <%-- <form id="commentForm" runat="server">--%>
                                        <div id="feedback-form">
                                            <div class="row">
                                                <div class="<%=(comment.Count()!=0)? "col-lg-12":"col-lg-4"%> form-items-wrap">
                                                    <label class="form-label"><i class="fa fa-user"></i></label>
                                                    <asp:TextBox ID="txtFullName" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server" autocomplete="off"></asp:TextBox>
                                                    <hr>
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator6" runat="server"
                                                        ErrorMessage="لطفا نام و نام خانوادگی را وارد کنید." ValidationGroup="feedback" ControlToValidate="txtFullName"
                                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div class="<%=(comment.Count()!=0)? "col-lg-12":"col-lg-4"%> form-items-wrap">
                                                    <label class="form-label"><i class="fa fa-envelope"></i></label>
                                                    <asp:TextBox ID="txtEmail" placeholder="پست الکترونیک " ClientIDMode="Static" runat="server" autocomplete="off"></asp:TextBox>
                                                    <hr>
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator5" runat="server"
                                                        ErrorMessage="لطفا پست الکترونیک را وارد کنید." ValidationGroup="feedback" ControlToValidate="txtEmail"
                                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                        ErrorMessage="پست الکترونیک معتبر نمی باشد"
                                                        Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" ValidationGroup="feedback" ControlToValidate="txtEmail"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Display="Dynamic">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                                <div class="<%=(comment.Count()!=0)? "col-lg-12":"col-lg-4"%> form-items-wrap">
                                                    <div class="contact-form-item captch-item">
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12 padding-left-right-0">
                                                                    <label><i class="fa fa-question-circle"></i></label>
                                                                    <asp:TextBox ID="txtCaptcha" CssClass="input-text" placeholder="کد امنیتی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                                    <hr />
                                                                    <asp:RequiredFieldValidator
                                                                        ID="RequiredFieldValidator4" runat="server"
                                                                        ErrorMessage="لطفا کد امنیتی را وارد کنید." ValidationGroup="feedback" ControlToValidate="txtCaptcha"
                                                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                                    </asp:RequiredFieldValidator>
                                                                    <asp:CustomValidator ErrorMessage="کد امنیتی اشتباه است" OnServerValidate="ValidateCaptcha4"
                                                                        runat="server" Visible="True" Font-Names="Tahoma" ValidationGroup="feedback" ForeColor="#CC0000" Font-Size="10px" Display="Dynamic" />
                                                                </div>
                                                                <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12 padding-left-right-0">
                                                                    <div class="captcha display-center">
                                                                        <kfk:CaptchaControl ID="Captcha4" runat="server"
                                                                            CaptchaBackgroundNoise="High" CaptchaLength="5"
                                                                            CaptchaHeight="40" CssClass="col-md-6" CaptchaWidth="120"
                                                                            CaptchaLineNoise="None" CaptchaMinTimeout="5" ValidationGroup="feedback"
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
                                            </div>

                                            <div class="row">
                                                <div class="col-lg-12 form-items-wrap text-body">
                                                    <label class="form-label"><i class="fa fa-commenting"></i></label>
                                                    <textarea runat="server" clientidmode="Static" id="txtBody" name="textarea" placeholder="متن پیام مورد نظر..."></textarea>
                                                    <hr />
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator2" runat="server"
                                                        ErrorMessage="لطفا متن پیام را وارد کنید." ValidationGroup="feedback" ControlToValidate="txtBody"
                                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="form-items-wrap">
                                                <asp:Button ID="btnSave" runat="server" CssClass="btn-feedback-form" ValidationGroup="feedback" Text="ارسال کن" OnClick="btnSave_Click" />
                                            </div>
                                            <div class="clear"></div>
                                        </div>
                                        <%--</form>--%>
                                    </div>
                                    <!-- END FEEDBACK FORM -->
                                    <!-- BEGIN COMMENT SECTION -->
                                    <%if (comment.Count() != 0)
                                      {%>
                                    <div id="comment-section" class="col-md-offset-0 col-md-8 col-lg-offset-0 col-lg-8 col-sm-offset-1 col-sm-11 col-xs-offset-1 col-xs-11">
                                        <%foreach (Comment item in comment)
                                          {%>
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
                                                            <span><%=DateUtil.GetPersianDateWithTime(item.Date)%></span>
                                                        </div>
                                                        <div class="col-md-4 col-lg-4 col-sm-12 col-xs-12 paddin-top direction-ltr">
                                                            <span class="btn-dislike" data-id="<%=item.ID %>"><i class="fa fa-heart"></i></span>
                                                            <span class="btn-like" data-id="<%=item.ID %>"><i class="fa fa-heart"></i></span>
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
                                        <%}%>
                                    </div>
                                    <%} %>

                                    <!-- END COMMENT SECTION -->
                                </section>
                                <!-- END FEEDBACK SECTION -->
                            </div>
                            <div id="branch-contact" class="tab-pane fade">
                                <section>
                                    <div id="branch-contact-form" class="col-md-12">
                                        <%-- <form id="branchContactForm">--%>
                                        <div class="row">
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-items-wrap">
                                                    <label class="form-label"><i class="fa fa-user"></i></label>
                                                    <asp:TextBox ID="txtContactFullName" CssClass="width100" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server" autocomplete="off"></asp:TextBox>
                                                    <hr />
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator8" runat="server"
                                                        ErrorMessage="لطفا نام و نام خانوادگی را وارد کنید." ValidationGroup="contact" ControlToValidate="txtContactFullName"
                                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                                <div class="form-items-wrap">
                                                    <label class="form-label"><i class="fa fa-envelope"></i></label>
                                                    <asp:TextBox ID="txtContactEmail" CssClass="width100" placeholder="پست الکترونیک" ClientIDMode="Static" runat="server" autocomplete="off"></asp:TextBox>
                                                    <hr />
                                                    <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator7" runat="server"
                                                        ErrorMessage="لطفا پست الکترونیک را وارد کنید." ValidationGroup="contact" ControlToValidate="txtContactEmail"
                                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                    </asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                        ErrorMessage="پست الکترونیک معتبر نمی باشد"
                                                        Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" ValidationGroup="contact" ControlToValidate="txtEmail"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        Display="Dynamic">
                                                    </asp:RegularExpressionValidator>
                                                </div>
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 form-items-wrap captcha-item">
                                                <asp:UpdatePanel ID="uplCaptcha" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12 padding-left-right-0">
                                                            <label><i class="fa fa-question-circle"></i></label>
                                                            <asp:TextBox ID="txtCaptchaContact" CssClass="input-text" placeholder="کد امنیتی" ClientIDMode="Static" runat="server"></asp:TextBox>
                                                            <hr />
                                                            <asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator3" runat="server"
                                                                ErrorMessage="لطفا کد امنیتی را وارد کنید." ValidationGroup="contact" ControlToValidate="txtCaptchaContact"
                                                                Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                            </asp:RequiredFieldValidator>
                                                            <asp:CustomValidator ErrorMessage="کد امنیتی اشتباه است" OnServerValidate="ValidateCaptcha5"
                                                                runat="server" ValidationGroup="contact" Visible="True" Font-Names="Tahoma" ForeColor="#CC0000" Font-Size="10px" Display="Dynamic" />
                                                        </div>
                                                        <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12 padding-left-right-0">
                                                            <div class="captcha display-center">
                                                                <kfk:CaptchaControl ID="Captcha5" runat="server"
                                                                    CaptchaBackgroundNoise="High" CaptchaLength="5"
                                                                    CaptchaHeight="40" CssClass="col-md-6" CaptchaWidth="120"
                                                                    CaptchaLineNoise="None" CaptchaMinTimeout="5" ValidationGroup="contact"
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
                                        <div class="row margin-left-right-0">
                                            <div class="col-md-12 form-items-wrap text-body">
                                                <label for="msg" class="form-label"><i class="fa fa-commenting"></i></label>
                                                <textarea runat="server" clientidmode="Static" id="txtContactBody" name="textarea" placeholder="پیام شما به شعبه ..."></textarea>
                                                <hr />
                                                <asp:RequiredFieldValidator
                                                    ID="RequiredFieldValidator1" runat="server"
                                                    ErrorMessage="لطفا متن پیام را وارد کنید." ValidationGroup="contact" ControlToValidate="txtContactBody"
                                                    Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                        <div>
                                            <asp:Button ID="btnContactForm" CssClass="btn-contact-form" runat="server" ValidationGroup="contact" Text="ارسال پیام" OnClick="btnContactSave_Click" />
                                        </div>
                                        <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
                                        <%-- </form>--%>
                                    </div>
                                </section>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <!-- END BRANCH INFO -->
        </section>
    </form>
    <!-- END CONTENT -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <%-- BEGIN GLOBAL SCRIPT --%>
    <script src="/assets/global/plugins/bootstrap/js/bootstrap.v3.3.2.min.js"></script>
    <script>
        function initMap() {
            var longitude = $("#branchLongitude").val();
            var latitude = $("#branchLatitude").val();
            var title = $("#branchTitle").val();
            var position = new google.maps.LatLng(latitude, longitude);
            var myOptions = {
                zoom: 17,
                center: position,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            var map = new google.maps.Map(
                document.getElementById("map_canvas"),
                myOptions);

            var marker = new google.maps.Marker({
                position: position,
                map: map,
                title: title
            });
        }
    </script>

    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyDiyiZ5RoDBc82CVb5hCOoKuOEqYts_0Pg&callback=initMap" async defer></script>


    <%-- END  GLOBAL SCRIPT  --%>
    <!-- BEGIN Branch-Details PAGE SCRIPT -->
    <script src="/assets/js/Branch-Details/Branch-Details.min.js?v=2.2"></script>
    <!-- BEGIN Branch-Details PAGE SCRIPT -->

</asp:Content>

