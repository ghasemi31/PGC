<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="GameDetail.aspx.cs" Inherits="Pages_Guest_GameDetail" %>

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
                        <img class="width100" src="<%=ResolveClientUrl(game.ImagePath) %>" alt="<%=game.Title %>" />
                        <div class="display-center">
                            <a href="#" class="btn-game">ثبت نام در بازی</a>
                        </div>
                    </div>
                    <div class="col-lg-9 col=md-9 col-sm-6 col-xs-12">
                        <div class="row" id="game-info">
                            <div class="col-md-12">
                                <span id="game-title"><%=game.Title %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">مدیر بازی:</span>
                                <span class="game-detail-info"><%=game.User.FullName %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">نوع بازی:</span>
                                <span class="game-detail-info"><%=(game.GamerCount>1)?"بازی انفرادی":"بازی تیمی" %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">پلتفرم بازی:</span>
                                <span class="game-detail-info"><%=kFrameWork.Util.EnumUtil.GetEnumElementPersianTitle((pgc.Model.Enums.GameType)game.Type_Enum) %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">جایزه نفر اول:</span>
                                <span class="game-detail-info"><%=game.FirstPresent %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">جایزه نفر دوم:</span>
                                <span class="game-detail-info"><%=game.SecondPresent %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">جایزه نفر سوم:</span>
                                <span class="game-detail-info"><%=game.ThirdPresent %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">هزینه ثبت نام:</span>
                                <span class="game-detail-info"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(game.Cost) %></span>
                            </div>
                            <div class="col-md-12">
                                <span class="game-detail-title">قوانین و مقررات بازی:</span>
                                <hr />
                                <div>
                                    <%=game.Laws %>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </section>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
</asp:Content>

