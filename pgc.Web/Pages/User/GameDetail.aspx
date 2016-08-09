<%@ Page title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="GameDetail.aspx.cs" Inherits="Pages_User_GameDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="/assets/Guest/css/GameDetail.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">
    <section id="page">
        <header id="game-detail-header">
        </header>
        <section id="main-content">
            <div class="container">
                <div class="row game-detail">
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <img class="width100" src="/assets/Guest/Image/GamePoster/images.jpg" alt="pgc" />
                    </div>
                    <div class="col-lg-9 col=md-9 col-sm-6 col-xs-12">
                        <div class="row" id="game-info">
                            <div class="col-md-12">
                                <span id="game-title">نام بازی</span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">مدیر بازی:</span>
                                <span class="game-detail-info">نام مدیر بازی</span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">نوع بازی:</span>
                                <span class="game-detail-info"><%=(true)?"بازی انفرادی":"بازی تیمی" %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">نام تیم:</span>
                                <span class="game-detail-info">اگه بازی تیمی بود</span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">پلتفرم بازی:</span>
                                <span class="game-detail-info"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.GameType)1) %></span>
                            </div>

                            <div class="col-md-6">
                                <span class="game-detail-title">تاریخ ثبت نام:</span>
                                <span class="game-detail-info">فردا</span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">وضعیت پرداخت:</span>
                                <span class="game-detail-info">پس فردا</span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">تاریخ پرداخت:</span>
                                <span class="game-detail-info">شلغم</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row game-detail">
                    <div class="col-lg-3 col=md-3 col-sm-4 col-xs-12">
                        <span>افزودن هم تیمی</span>
                        <hr />
                        <div>
                            <asp:TextBox ID="txtNationalCode" CssClass="form-control" placeholder="کد ملی هم تیمی" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="کد ملی هم تیمی"></asp:TextBox>
                            <asp:RequiredFieldValidator
                                ID="RequiredFieldValidator2" runat="server"
                                ErrorMessage="لطفا کد ملی هم تیمی را وارد نمایید" ControlToValidate="txtNationalCode"
                                Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                            <asp:Button CssClass="btn-game btn-add-player" runat="server" Text="افزودن هم تیمی" OnClick="Unnamed_Click" />
                        </div>
                         
                    </div>
                    <div class="col-lg-9 col=md-9 col-sm-8 col-xs-12">
                        <span>لیست اعضای تیم</span>
                        <hr />
                    </div>
                </div>

            </div>
        </section>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
</asp:Content>

