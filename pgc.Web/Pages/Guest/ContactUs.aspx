<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="Pages_Guest_ContactUs" %>

<%@ Import Namespace="pgc.Model" %>
<%@ Import Namespace="pgc.Business" %>
<%@ Import Namespace="pgc.Model.Enums" %>
<%@ Import Namespace="kFrameWork.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%this.Title = OptionBusiness.GetText(OptionKey.contact_Title); %>
    <meta name="description" content="<%=OptionBusiness.GetLargeText(OptionKey.Description_Contact) %>" />
    <meta name="keywords" content="<%=OptionBusiness.GetLargeText(OptionKey.Keywords_Contact) %>" />
    <link href="/assets/Guest/css/Contact.css" rel="stylesheet" />
    <link href="/assets/User/UserProfile.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" runat="Server">

    <section id="page">
        <header id="contact-header">
        </header>

        <!-- main content -->
        <section id="main-content" class="main-body">
            <div class="container">
                <div class="row">
                    <!-- map -->
                    <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                        <div class="row">
                            <div id="map_canvas" class="col-md-12"></div>
                        </div>
                        <div id="address-info" class="row">
                            <span><i class="fa fa-phone" aria-hidden="true"></i><%=kFrameWork.Business.OptionBusiness.GetLargeText(OptionKey.PGC_Telegram) %></span>
                            <br />
                            <span><i class="fa fa-envelope" aria-hidden="true"></i><%=kFrameWork.Business.OptionBusiness.GetLargeText(OptionKey.PGC_Email) %></span>

                        </div>
                    </div>
                    <!-- map -->
                    <!-- contact form -->
                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">


                        <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="نام و نام خانوادگی" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="نام و نام خانوادگی"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator1" runat="server"
                            ErrorMessage="لطفا نام و نام خانوادگی خود را وارد نمایید" ControlToValidate="txtFullName"
                            Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                        </asp:RequiredFieldValidator>

                        <asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="form-control" placeholder="پست الکترونیک" runat="server" autocomplete="off" ToolTip="پست الکترونیک"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RequiredEmail" runat="server"
                            ErrorMessage="لطفا پست الکترونیک خود را وارد نمایید" ControlToValidate="txtEmail"
                            Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ErrorMessage="پست الکترونیک معتبر نمی باشد"
                            Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                            Display="Dynamic">
                        </asp:RegularExpressionValidator>

                        <asp:TextBox ID="txtMobile" CssClass="form-control" placeholder="تلفن همراه" ClientIDMode="Static" runat="server" autocomplete="off" ToolTip="تلفن همراه"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator5" runat="server"
                            ErrorMessage="لطفا تلفن همراه خود را وارد نمایید" ControlToValidate="txtMobile"
                            Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                        </asp:RequiredFieldValidator>

                        <textarea runat="server" name="txtBody" id="txtBody" cols="4" rows="5" placeholder="متن پیام..." class="width100"></textarea>
                        <asp:RequiredFieldValidator
                            ID="RequiredFieldValidator2" runat="server"
                            ErrorMessage="لطفا متن پیام را وارد کنید." ControlToValidate="txtBody"
                            Visible="True" Font-Names="Tahoma" Font-Size="10px" ForeColor="#CC0000" Display="Dynamic">
                        </asp:RequiredFieldValidator>
                        <asp:Button CssClass="btn btn-default btn-contact" runat="server" Text="ارسال" OnClick="btnSave_Click" />

                        <div class="result"></div>
                    </div>
                    <!-- contact form -->
                </div>
                <div class="row">
                    <div class="user-info" style="margin-top: 3em">
                        <header>
                            <i class="fa fa-list" aria-hidden="true"></i>
                            <span>لیست مدیران بازی</span>
                            <hr />
                        </header>
                        <div class="row margin0" style="padding: 0 20px;">
                            <table class="table">
                                <tr class="order-title table-header">
                                    <td>نام بازی</td>
                                    <td>نام مدیر بازی</td>
                                    <td>پست الکترونیک مدیر بازی</td>
                                    <td>شماره تماس مدیر بازی</td>
                                    <td>شماره همراه مدیر بازی</td>
                                    <td>آدرس تلگرام مدیر بازی</td>
                                </tr>
                                <tr class="order-tb-row">
                                    <%foreach (var item in games)
                                      {
                                          foreach (var useritem in item.Users)
                                          {%>
                                    <td><%=item.Title %></td>
                                    <td><%=useritem.FullName %></td>
                                    <td><%=useritem.Email %></td>
                                    <td><%=useritem.Tel %></td>
                                    <td><%=useritem.Mobile %></td>
                                    <td>telegram</td>
                                    <%}
                                      } %>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

            </div>
        </section>
        <!-- end main content -->
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC--heJUlVdZJY1OjwnwZd1aVSX-Lpeo00&callback=initMap" async defer></script>
    <script>
        function initMap() {
            var longitude = <%=OptionBusiness.GetDouble(OptionKey.Longitude)%>;
            var latitude = <%=OptionBusiness.GetDouble(OptionKey.Latitude)%>;
            var title = "Iran PGC";
            var position = new google.maps.LatLng(latitude,longitude);
            var myOptions = {
                zoom: 13,
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
</asp:Content>

