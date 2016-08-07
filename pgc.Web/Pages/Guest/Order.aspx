<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Pages_Guest_Order" %>

<%@ Import Namespace="kFrameWork.UI" %>
<%@ Import Namespace="kFrameWork.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Register Src="~/UserControls/Project/LoginControl.ascx" TagName="LoginControl" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/Common/UserMessageViewer.ascx" TagName="UserMessageViewer" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" runat="Server">

    <%this.Title = OptionBusiness.GetText(pgc.Model.Enums.OptionKey.OrderTitle); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Description_Order) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Keywords_Order) %>" />
    <link href="/assets/css/Order/order.min.css?v=2.2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" runat="Server">
    <section class="main-body">
        <div class="container">
            <%if (OptionBusiness.GetBoolean(OptionKey.AllowOnlineOrdering))
              {%>
            <div id="rootwizard" style="margin: 3em 0;">
                <div class="row">
                    <div>
                        <ul>
                            <li><a href="#step1" data-toggle="tab">انتخاب غذا</a></li>
                            <li><a href="#step2" data-toggle="tab">انتخاب مخلفات</a></li>
                            <li><a href="#step3" data-toggle="tab">احراز هویت کاربر</a></li>
                            <li><a href="#step4" data-toggle="tab">آدرس و شرایط تحویل</a></li>
                        </ul>
                    </div>
                </div>
                <div class="progress">
                    <div id="progressBar" class="progress-bar progress-bar-striped">
                        <div class="bar">
                            <span></span>
                        </div>
                    </div>
                </div>
                <div>
                    <ul class="pager wizard">
                        <li class="previous"><a href="javascript:;" class="disable-select" accesskey="p"><i class="fa fa-chevron-right" aria-hidden="true" style="padding-left: 10px"></i>مرحله قبلی</a></li>
                        <li class="last float-left" style="display: none;"><a href="javascript:;" class="disable-select">ثبت نهایی</a></li>
                        <li class="next"><a href="javascript:;" class="disable-select" accesskey="n" style="padding-left: 25px; padding-right: 25px;">مرحله بعدی <i class="fa fa-chevron-left" aria-hidden="true" style="padding-right: 10px;"></i></a></li>
                    </ul>
                </div>
                <input id="userId" type="hidden" value="<%=kFrameWork.UI.UserSession.IsUserLogined ? kFrameWork.UI.UserSession.UserID:0 %>" />
                <input id="productId" type="hidden" value="<%=id %>" />
                <input id="productCount" type="hidden" value="<%=count %>" />
                <div class="tab-content">
                    <div class="tab-pane active" id="step1">
                        <%-- BEGIN PRODUCT LIST --%>
                        <div class="row order-content">

                            <%foreach (var item in product.Where(p => p.Accessories == false))
                              {%>
                            <div class="product col-lg-5ths col-md-5ths col-sm-5ths col-xsl-5ths col-xs-5ths thumbnail">
                                <div class="product-detail product-select">
                                    <span class="product-add"><i class="fa fa-plus" aria-hidden="true"></i></span>
                                    <span class="product-count disable-select">0</span>
                                    <span class="product-remove"><i class="fa fa-minus" aria-hidden="true"></i></span>
                                    <div class="clear"></div>
                                </div>
                                <img class="disable-select" alt="<%=item.Title %>" src="<%=(!string.IsNullOrEmpty(item.SliderProductPicPath))?ResolveClientUrl(item.SliderProductPicPath):"/assets/global/images/branch-default.jpg" %>?height=200&width=260&mode=cropandscale" />
                                <span class="item-count-selected"></span>
                                <div class="product-detail">
                                    <span class="product-info"><i class="fa fa-info-circle"></i></span>
                                    <span data-pid="<%=item.ID %>" class="product-title"><%=item.Title %></span>
                                    <span data-pid="<%=item.ID %>" class="product-price"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(item.Price/10) %> تومان</span>
                                    <div class="clear"></div>
                                </div>
                                <div class="product-tip">
                                    <p>
                                        <%=item.Body %>
                                    </p>
                                </div>
                            </div>
                            <% } %>
                        </div>
                        <%-- END PRODUCT LIST --%>
                    </div>
                    <div class="tab-pane" id="step2">
                        <%-- BEGIN PRODUCT INGREDIENTS LIST --%>
                        <div class="row order-content">
                            <%foreach (var item in product.Where(p => p.Accessories == true))
                              {%>
                            <div class="product col-lg-5ths col-md-5ths col-sm-5ths col-xsl-5ths col-xs-5ths thumbnail">
                                <div class="product-detail product-select">
                                    <span class="product-add"><i class="fa fa-plus" aria-hidden="true"></i></span>
                                    <span class="product-count disable-select">0</span>
                                    <span class="product-remove"><i class="fa fa-minus" aria-hidden="true"></i></span>
                                    <div class="clear"></div>
                                </div>
                                <img class="disable-select" alt="<%=item.Title %>" src="<%=(!string.IsNullOrEmpty(item.SliderProductPicPath))?ResolveClientUrl(item.SliderProductPicPath):"/assets/global/images/branch-default.jpg" %>?height=200&width=260&mode=cropandscale" />
                                <span class="item-count-selected"></span>
                                <div class="product-detail">
                                    <span class="product-info"><i class="fa fa-info-circle"></i></span>
                                    <span data-pid="<%=item.ID %>" class="product-title"><%=item.Title %></span>
                                    <span data-pid="<%=item.ID %>" class="product-price"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(item.Price/10) %> تومان</span>
                                    <div class="clear"></div>
                                </div>
                                <div class="product-tip">
                                    <p>
                                        <%=item.Body %>
                                    </p>
                                </div>
                            </div>
                            <% } %>
                        </div>
                        <%-- END PRODUCT INGREDIENTS LIST --%>
                    </div>
                    <div class="tab-pane" id="step3">
                        <div class="row">
                            <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                                <div class="row" id="tip">
                                    لطفا وارد سایت شوید و چنانچه عضو مستردیزی نیستید برای ادامه درخواست، عضو شوید.
                                </div>
                                <div class="row">
                                    <div class="order-form col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                        <header>
                                            <h1>ورود به مستردیزی</h1>
                                            <hr />
                                        </header>
                                        <div>
                                            <div class="order-form-item">
                                                <label><i class="fa fa-envelope"></i></label>
                                                <input type="text" id="txtEmail" class="input-text" placeholder="پست الکترونیک" />
                                                <hr />
                                                <span class="validation-error">لطفا پست الکترونیک خود را وارد کنید.</span>
                                                <%--<span class="validation-error-email">پست الکترونیکی وارد شده معتبر نمیباشد.</span>--%>
                                            </div>
                                            <div class="order-form-item">
                                                <label><i class="fa fa-key" aria-hidden="true"></i></label>
                                                <input type="password" id="txtPass" class="input-text" placeholder="کلمه عبور" />
                                                <hr />
                                                <span class="validation-error">لطفا کلمه عبور خود را وارد کنید.</span>
                                            </div>
                                            <div class="order-form-item display-center">
                                                <input type="button" id="btnLogin" class="btn-order-form" value="ورود" />
                                            </div>
                                        </div>
                                        <div class="clear"></div>

                                    </div>
                                    <div class="order-form col-lg-offset-2 col-md-offset-2 col-sm-offset-0 col-xs-offset-0 col-lg-4 col-md-4 col-sm-6 col-xs-12">
                                        <header>
                                            <h1>ثبت نام در مستردیزی</h1>
                                            <hr />
                                        </header>
                                        <div>
                                            <div class="order-form-item">
                                                <label><i class="fa fa-user"></i></label>
                                                <input id="txtRegisterName" type="text" class="input-text" placeholder="نام و نام خانوادگی" />
                                                <hr />
                                                <span class="validation-error">لطفا نام و نام خانوادگی خود را وارد کنید.</span>
                                            </div>
                                            <div class="order-form-item">
                                                <label><i class="fa fa-envelope"></i></label>
                                                <input id="txtRegisterEmail" type="text" class="input-text" placeholder="پست الکترونیک" />
                                                <hr />
                                                <span class="validation-error">لطفا پست الکترونیک خود را وارد کنید.</span>
                                                <span class="validation-error-email">پست الکترونیکی وارد شده معتبر نمیباشد.</span>
                                            </div>
                                            <div class="order-form-item">
                                                <label><i class="fa fa-key" aria-hidden="true"></i></label>
                                                <input id="txtRegisterPass" type="password" class="input-text" placeholder="کلمه عبور" />
                                                <hr />
                                                <span class="validation-error">لطفا کلمه عبور خود را وارد کنید.</span>
                                            </div>
                                            <div class="order-form-item">
                                                <label><i class="fa fa-key" aria-hidden="true"></i></label>
                                                <input id="txtRegisterConfirmPass" type="password" class="input-text" placeholder="تکرار کلمه عبور" />
                                                <hr />
                                                <span class="validation-error">لطفا تکرار کلمه عبور خود را وارد کنید.</span>
                                                <span class="validation-error-rePass">کلمه عبور با تکرار آن مغایرت دارد.</span>
                                            </div>
                                            <div class="order-form-item display-center">
                                                <input id="btnRegister" type="button" class="btn-order-form" value="ثبت نام" />
                                            </div>
                                        </div>
                                        <div class="clear"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="step4">
                        <div class="row">
                            <form action="<%=GetRouteUrl("guest-order",null) %>" id="orderForm" method="post">
                                <input type="hidden" name="action" value="orderForm" />
                                <input type="hidden" id="item-selected" name="item-selected" value="" />
                                <input type="hidden" id="total-amount" name="total-amount" value="" />
                                <div class="order-form col-lg-offset-1 col-md-offset-1 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-10 col-sm-12 col-xs-12">
                                    <header>
                                        <h1>اطلاعات تحویل غذا</h1>
                                        <hr />
                                    </header>
                                    <div class="col-lg-offset-1 col-md-offset-1 col-sm-offset-2 col-xs-offset-0 col-lg-5 col-md-5 col-sm-8 col-xs-12">
                                        <div class="order-form-item">
                                            <label><i class="fa fa-building-o" aria-hidden="true"></i></label>
                                            <select id="branchID" name="branchID" class="input-text">
                                                <%foreach (var item in branch)
                                                  {%>
                                                <option value="<%=item.ID %>"><%=item.Title %></option>
                                                <%} %>
                                            </select>
                                            <span class="float-left" id="branchInfo"><i class="fa fa-info-circle" aria-hidden="true"></i></span>
                                            <div id="branch-tip">
                                                <p><%=kFrameWork.Business.OptionBusiness.GetText(pgc.Model.Enums.OptionKey.OnlineOrderInformation) %></p>
                                            </div>
                                            <hr />
                                            <span class="validation-error">لطفا شعبه مورد نظر را انتخاب کنید.</span>
                                        </div>
                                        <div class="order-form-item">
                                            <div class="order-form-caption">
                                                <span>نام تحویل گیرنده</span>
                                            </div>
                                            <label><i class="fa fa-user"></i></label>
                                            <input id="txtTransferee" name="txtTransferee" type="text" class="input-text" />
                                            <hr />
                                            <span class="validation-error">لطفا نام تحویل گیرنده را وارد کنید.</span>
                                        </div>
                                        <div class="order-form-item">
                                            <div class="order-form-caption">
                                                <span>تلفن ثابت</span>
                                            </div>
                                            <label><i class="fa fa-phone"></i></label>
                                            <input id="txtPhone" name="txtPhone" type="text" class="input-text" />
                                            <hr />
                                            <span class="validation-error">لطفا شماره تلفن ثابت را وارد کنید.</span>
                                        </div>
                                        <div class="order-form-item">
                                            <div class="order-form-caption">
                                                <span>تلفن همراه</span>
                                            </div>
                                            <label><i class="fa fa-mobile"></i></label>
                                            <input id="txtMobile" name="txtMobile" type="text" class="input-text" />
                                            <hr />
                                            <span class="validation-error">لطفا شماره همراه خود را وارد کنید.</span>
                                        </div>
                                    </div>
                                    <div class="col-lg-offset-0 col-md-offset-0 col-sm-offset-0 col-xs-offset-0 col-lg-5 col-md-5 col-sm-8 col-xs-12">
                                        <div class="order-form-item">
                                            <label><i class="fa fa-money" aria-hidden="true"></i>روش پرداخت</label>
                                            <input type="radio" name="peymentType" value="2" checked="checked" style="margin-right: 25px">
                                            پرداخت آنلاین
                                        <input type="radio" name="peymentType" value="1" style="margin-right: 10px">
                                            پرداخت در محل
                                        <hr />
                                        </div>
                                        <div id="address-item" class="order-form-item">
                                            <div class="order-form-caption">
                                                <span>آدرس تحویل گیرنده</span>
                                            </div>
                                            <label><i class="fa fa-map-marker"></i></label>
                                            <textarea id="txtAddress" name="txtAddress" class="input-text"></textarea>
                                            <hr />
                                            <span class="validation-error">لطفا آدرس تحویل گیرنده را وارد کنید.</span>
                                        </div>
                                    </div>
                                    <div id="description-item" class="col-lg-offset-1 col-md-offset-1 col-sm-offset-2 col-xs-offset-0 col-lg-10 col-md-10 col-sm-8 col-xs-12">
                                        <div class="order-form-item">
                                            <div class="order-form-caption">
                                                <span>توضیحات</span>
                                            </div>
                                            <label><i class="fa fa-sticky-note"></i></label>
                                            <textarea id="txtDescription" name="txtDescription" class="input-text"></textarea>
                                            <hr />
                                        </div>
                                    </div>

                                </div>
                            </form>
                        </div>
                    </div>

                    <ul class="pager wizard">
                        <li class="previous"><a href="javascript:;" class="disable-select" accesskey="p"><i class="fa fa-chevron-right" aria-hidden="true" style="padding-left: 10px"></i>مرحله قبلی</a></li>
                        <li class="last float-left" style="display: none;"><a href="javascript:;" class="disable-select">ثبت نهایی</a></li>
                        <li class="next"><a href="javascript:;" class="disable-select" accesskey="n" style="padding-left: 25px; padding-right: 25px;">مرحله بعدی <i class="fa fa-chevron-left" aria-hidden="true" style="padding-right: 10px;"></i></a></li>
                    </ul>
                </div>
            </div>
            <%}
              else
              {%>
            <div id="order-suspended">
                <%=OptionBusiness.GetLargeText(OptionKey.MessageForOnlineOrderIsSuspended) %>
            </div>
            <% } %>
        </div>
        <kfk:UserMessageViewer runat="server" ID="UserMessageViewer" />
    </section>


    <%-- BEGIN BILL --%>
    <%if (OptionBusiness.GetBoolean(OptionKey.AllowOnlineOrdering))
      {%>
    <section>
        <div id="bill">
            <div id="bill-header">
                <span>صورت حساب</span>
                <span id="price-header">(0 تومان)</span>
                <span class="float-left"><i class="fa fa-chevron-up" aria-hidden="true"></i></span>
            </div>
            <div id="selected-food" class="bill-deactive">
                <div id="foods">
                </div>
                <div>
                    <span>مبلغ قابل پرداخت</span>
                    <span class="price-item"><span class="price">0</span> تومان</span>
                </div>
            </div>
        </div>
    </section>
    <%} %>
    <%-- END BILL --%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" runat="Server">
    <script src="/assets/global/plugins/bootstrap/js/bootstrap.v3.3.2.min.js"></script>
    <script src="/assets/global/plugins/bootstrap/jquery.bootstrap.wizard.min.js"></script>
    <script src="/assets/js/Order/order.min.js?v=2.2"></script>
</asp:Content>

