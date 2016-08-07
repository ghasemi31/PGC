<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Master/Guest.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="Pages_Guest_ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="/assets/Guest/css/Contact.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphbdy" Runat="Server">

        <section id="page">
            <header id="contact-header">
                <!--<div class="container">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h1>Contact us</h1>
                                        </div>
                                    </div>
                                </div>-->
            </header>

            <!-- main content -->
            <section id="main-content">
                <div class="container">
                    <div class="row">
                        <!-- map -->
                        <div class="col-lg-7 col-md-7 col-sm-12 col-xs-12">
                            <div class="row">
                                <div id="map_canvas" class="col-md-12"></div>
                            </div>
                            <div id="address-info" class="row">
                                <span><i class="fa fa-map-marker" aria-hidden="true"></i>تهران آریاشهر</span>
                                <br />
                                <span><i class="fa fa-phone" aria-hidden="true"></i>09132541924</span>
                                <br />
                                <span><i class="fa fa-envelope" aria-hidden="true"></i>s.ghasem365@gmail.com</span>

                            </div>
                        </div>
                        <!-- map -->
                        <!-- contact form -->
                        <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12">

                            <form id="contactfrm">
                                <label for="name"></label>
                                <input type="text" name="name" id="name" placeholder="نام و نام خانوادگی" title="sdasd" class="form-control" />
                                <label for="email"></label>
                                <input type="text" name="email" id="email" placeholder="پست الکترونیک" class="form-control" />
                                <label for="phone"></label>
                                <input name="phone" type="text" id="phone" size="30" value="" placeholder="تلفن همراه" class="required digits form-control" />
                                <label for="comments"></label>
                                <label for="game-admin"></label>                                
                                <select name="game-admin" id="game-admin" class="form-control">
                                    <option value="0">مدیر سایت</option>
                                    <option value="1">مسئول بازی 1</option>
                                    <option value="2">مسئول بازی 2</option>
                                    <option value="3">مسئول بازی 3</option>
                                </select>
                                <textarea name="comment" id="comments" cols="4" rows="5" placeholder="متن پیام..." class="width100"></textarea>
                                
                                <button name="submit" type="submit" class="btn btn-default" id="submit">ارسال</button>
                            </form>
                            <div class="result"></div>
                        </div>
                        <!-- contact form -->
                    </div>

                </div>
            </section>
            <!-- end main content -->
        </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphfoot" Runat="Server">
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyC--heJUlVdZJY1OjwnwZd1aVSX-Lpeo00&callback=initMap" async defer></script>
    <script>
        function initMap() {
            var longitude = 35.73;
            var latitude = 51.33;
            var title = "Iran PGC";
            var position = new google.maps.LatLng(longitude, latitude);
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

