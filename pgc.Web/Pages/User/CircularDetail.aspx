<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="CircularDetail.aspx.cs" Inherits="Pages_User_CircularDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphStylePege" Runat="Server">
    <title></title>
    <%this.Title ="جزئیات بخش نامه-"+circular.Title; %>
    <link href="/assets/css/UserProfile/UserProfile.min.css?v=2.2" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphContentPage" Runat="Server">
    <section class="main-body">
        <div class="container">
            <div class="row">
                <div class="col-lg-offset-1 col-md-offset-0 col-sm-offset-0 col-xs-offset-0 col-lg-10 col-md-12 col-sm-12 col-xs-12">
                    <div id="circular-detail">
                        <div id="circular-header">
                            <span id="circular-title"><%=circular.Title %></span>
                            <span class="float-left"><%=kFrameWork.Util.DateUtil.GetPersianDateWithTime(circular.Date) %></span>
                        </div>
                        <div id="circular-body">
                            <%=circular.Body %>
                        </div>
                        <div class="float-left">
                            <a class="btn-profile" href="<%=GetRouteUrl("user-circularlist",null) %>">بخشنامه ها</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphScriptPage" Runat="Server">
</asp:Content>

