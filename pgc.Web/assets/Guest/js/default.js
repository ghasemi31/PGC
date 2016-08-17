$(document).ready(function () {

    //main slider
    //SetScreenHeight();
    //$(window).resize(function () {
    //    SetScreenHeight();
    //})



    var time = parseFloat($('#slider-time').val(), 10) * 1000;
    var speedTime = parseFloat($('#speed-slider-time').val(), 10) * 1000;
    var transaction = $('#slider-transaction').val();
    var transactionStyle;
    if (transaction == "false") {
        transactionStyle = false;
    }
    else {
        transactionStyle = transaction;
    }

    $("#mainSlider").owlCarousel({
        autoPlay: 3000,
      
        singleItem: true,
        autoplay: true,
        autoplayTimeout: speedTime,
        autoplaySpeed: time,
        autoplayHoverPause: true,
        loop: true,
        transitionStyle: transactionStyle,
   
        autoHeight: false,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            400: {
                items: 1
            },
            700: {
                items: 1,
            },
            1000: {
                items: 1,
            }
        }
    });
    //function afterOWLinit() {
    //    $('#main-slider .mainSlider-item').css("display", "block")
    //}



    //game logo slider
    var owl = $("#game-slider");
    owl.owlCarousel({
        nav: true,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplaySpeed: 4000,
        autoplayHoverPause: true,
        loop: true,

        navText: [
          "<i class='fa fa-chevron-left'></i>",
          "<i class='fa fa-chevron-right'></i>"
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            400: {
                items: 2
            },
            700: {
                items: 3,
            },
            1000: {
                items: 4,
            }
        }
    });

    //news slider
    $("#news-slider").owlCarousel({
        nav: true,
        autoplay: true,
        autoplayTimeout: 5000,
        autoplaySpeed: 4000,
        autoplayHoverPause: true,
        margin: 5,
       
        addClassActive: true,
        loop: true,
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            320: {
                items: 1
            },
            480: {
                items: 2
            },
            
            1024: {
                items: 3,
            },
            3000: {
                items: 3,
            },
        },
        nav: false,
    });

    //supporter slider
    $("#supporter-slider").owlCarousel({
        nav: true,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplaySpeed: 4000,
        autoplayHoverPause: true,
        loop: true,

        navText: [
          "<i class='fa fa-chevron-left'></i>",
          "<i class='fa fa-chevron-right'></i>"
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            400: {
                items: 3,
            },
            700: {
                items: 5,
            },
            3000: {
                items: 5,
            }
        }
    });

    //sponsor slider
    $("#sponsor-slider").owlCarousel({
        nav: true,
        autoplay: true,
        autoplayTimeout: 3000,
        autoplaySpeed: 4000,
        autoplayHoverPause: true,
        loop: true,

        navText: [
          "<i class='fa fa-chevron-left'></i>",
          "<i class='fa fa-chevron-right'></i>"
        ],
        responsiveClass: true,
        responsive: {
            0: {
                items: 1,
            },
            400: {
                items: 3,
            },
            700: {
                items: 5,
            },
            3000: {
                items: 5,
            }
        }
    });
})

//var SetScreenHeight = function () {
//    var screenHeight = $(window).height();
//    var screenWidth = $(window).width();
//    if (screenHeight > screenWidth) {
//        screenHeight = screenHeight / 2;
//    }
//    $('#main-slider').css("height", screenHeight-150);
//}