<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="GameDetail.aspx.cs"  Inherits="Pages_Guest_GameDetail" %>

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
                        <div class="game-detail right-section">
                            <asp:MultiView ID="mlvGame" runat="server">
                                <asp:View runat="server">
                                    <span>لطفا برای ثبت نام در بازی وارد سایت شوید و یا ثبت نام کنید</span>
                                    <br />
                                    <div class="display-center">
                                        <a href="<%=GetRouteUrl("guest-signup",null)+redirectUrl %>" class="btn-game btn-login-game">ثبت نام</a>
                                        <a href="<%=GetRouteUrl("guest-login",null)+redirectUrl %>" class="btn-game btn-login-game">ورود به سایت</a>
                                    </div>
                                </asp:View>
                                <asp:View runat="server">
                                   
                                    <%if(game.HowType_Enum==(int) pgc.Model.Enums.GameHowType.Offline){ %>


                                     <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                                    <span class="description">لطفا قبل از ثبت نام با توجه به استان و شهر محل سکونت خود یک مرکز بازی را انتخاب کنید</span>
                                    
                                    <br />
                                    <br />
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        استان:
                                <kfk:LookupCombo ID="lkcProvince" class="form-control width100"
                                    runat="server"
                                    BusinessTypeName="pgc.Business.Lookup.ProvinceLookupBusiness"
                                    AutoPostBack="true"
                                    DependantControl="lkcCity"
                                 
                                    Required="true" />
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                شهر:
                                <kfk:LookupCombo ID="lkcCity" class="form-control width100"
                                    runat="server"
                                    BusinessTypeName="pgc.Business.Lookup.CityLookupBusiness"
                                    DependOnParameterName="Province_ID"
                                    DependOnParameterType="Int64"
                                    OnSelectedIndexChanged="lkcGameCenetr_SelectedIndexChanged"
                                     AutoPostBack="true"
                               DependantControl="lkcGameCenetr"
                                    Required="true" />
                            </div>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                مرکز بازی:
                                <kfk:LookupCombo ID="lkcGameCenetr" class="form-control width100"
                                    runat="server"
                                    BusinessTypeName="pgc.Business.Lookup.GameCenterLookupBusiness"
                                    DependOnParameterName="City_ID"
                                    DependOnParameterType="Int64"
                               RequireText="لطفا مرکز بازی را انتخاب کنید"
                                   AutoPostBack="true"
                                    OnSelectedIndexChanged="lkcGameCenetr_SelectedIndexChanged"
                                    Required="true" />
                            </div>


            </ContentTemplate>
        </asp:UpdatePanel>
                                    <asp:Label runat="server" ID="lblDesc" Text=""></asp:Label>
                                  
                                    <%} %>
                                    <br />
                                   
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">

                                     <%if (game.GamerCount > 1)
                                      {%>
                                   

                                    <asp:TextBox ID="txtTeamName" CssClass="form-control" placeholder="نام تیم" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="نام تیم"></asp:TextBox>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1" runat="server"
                                        ErrorMessage="لطفا نام تیم خود را وارد نمایید" ControlToValidate="txtTeamName"
                                        Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                    <%} %>
                                     <br />
                                     <div class="<%=(game.GamerCount > 1)?"":"display-center" %>">
                                        <asp:Button CssClass="btn-game btn-add-player" runat="server" Text="ثبت نام در بازی" OnClick="Unnamed_Click" />
                                    </div>
                                        </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </div>
                    <div class="col-lg-9 col=md-9 col-sm-6 col-xs-12">
                        <div class="row" id="game-info">
                            <div class="col-md-12">
                                <span id="game-title"><%=game.Title %></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">مدیر بازی:</span>
                                <span class="game-detail-info"><%--<%=game.User.FullName %>--%></span>
                            </div>
                            <div class="col-md-6">
                                <span class="game-detail-title">نوع بازی:</span>
                                <span class="game-detail-info"><%=(game.GamerCount>1)?"بازی تیمی":"بازی انفرادی" %></span>
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
                                <span class="game-detail-info"><%=kFrameWork.Util.UIUtil.GetCommaSeparatedOf(game.Cost) %> ریال</span>
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
    <asp:HiddenField runat="server" ID="hfPosition" Value="" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
    <script type = "text/javascript">
        $(function () {
            var f = $("#<%=hfPosition.ClientID%>");
            window.onload = function () {
                var position = parseInt(f.val());
                $(document).scrollTop(500);
                if (!isNaN(position)) {
                   // alert(position);
                    $(window).scrollTop(position);
                    //$("html, body").animate({ scrollTop: position }, "slow");
                    
                }
            };
            window.onscroll = function () {
                var position = $(window).scrollTop();
                f.val(position);
            };
        });
</script>
</asp:Content>

