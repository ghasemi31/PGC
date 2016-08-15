
/*************** COLORS TO BE ERASED WHEN INSTALLING THE THEME ***********/

$(document).ready(function () {

    var s = document.createElement("script");
    s.type = "text/javascript";
    s.src = "js-plugin/jquery-cookie/jquery.cookie.js";
    $("body").append(s);

    if ($.cookie("css")) {
        $("#colors").attr("href", $.cookie("css"));
    }
    $(".switcher li a").click(function () {

        $("#colors").attr("href", $(this).attr("href"));
        $.cookie("css", $(this).attr("href"));
        return false;
    });

});

/*************** COLORS TO BE ERASED WHEN INSTALLING THE THEME ***********/


/*************** REPLACE WITH YOUR OWN UA NUMBER ***********/
var UA = 'UA-36433263-5';
/*************** REPLACE WITH YOUR OWN UA NUMBER ***********/


/*
|--------------------------------------------------------------------------
| DOCUMENT READY
|--------------------------------------------------------------------------
*/

$(document).ready(function () {



    ////hover queckmenu
    $('ul.quickMenu > li').hover(
        function () {
            $('ul', this).css('display', 'block');
            //$(this).css('background-color', '#ed8d43')
        },
        function () {
            $('ul', this).css('display', 'none');
            //$(this).css('background-color', '#000')
        })
    

    /*
    |--------------------------------------------------------------------------
    | PRETTY PHOTOS
    |--------------------------------------------------------------------------
    */
    if ($("a.prettyPhoto").length) {
        $("a.prettyPhoto").prettyPhoto({
            animation_speed: 'fast',
            slideshow: 10000,
            hideflash: true
        });
    }


    /*
    |--------------------------------------------------------------------------
    | TOOLTIP
    |--------------------------------------------------------------------------
    */

    $('.tips').tooltip({ placement: 'bottom' });



    /*
    |--------------------------------------------------------------------------
    | COLLAPSE
    |--------------------------------------------------------------------------
    */

    $('.accordion').on('show hide', function (e) {
        $('.accordion-toggle').removeClass('active');
        $(e.target).siblings('.accordion-heading').find('.accordion-toggle').addClass('active');
        $(e.target).siblings('.accordion-heading').find('.accordion-toggle i').toggleClass('icon-plus icon-minus', 200);

    });

    /*
    |--------------------------------------------------------------------------
    | CONTACT
    |--------------------------------------------------------------------------
    */
    $('.slideContact').click(function (e) {

        if ($(window).width() >= 800) {

            $('#contact').slideToggle('normal', 'easeInQuad', function () {

                $('#contactinfoWrapper').css('margin-left', 0);
                $('#mapSlideWrapper').css('margin-left', 3000);
                $('#contactinfoWrapper').fadeToggle();


            });
            $('#closeContact').fadeToggle();
            return false;

        } else {

            return true;

        }
    });


    $('#closeContact').click(function (e) {


        $('#contactinfoWrapper').fadeOut('normal', 'easeInQuad', function () {
            $('#contactinfoWrapper').css('margin-left', 0);
            $('#mapSlideWrapper').css('margin-left', 3000);
        });

        $('#contact').slideUp('normal', 'easeOutQuad');

        $(this).fadeOut();

        e.preventDefault();

    });






    /* MAP */
    $('#mapTrigger').click(function (e) {


        $('#mapSlideWrapper').css('display', 'block');
        initialize('mapWrapper');

        $('#contactinfoWrapper, #contactinfoWrapperPage').animate({
            marginLeft: '-2000px'
        }, 400, function () { });


        $('#mapSlideWrapper').animate({
            marginLeft: '25px'
        }, 400, function () { });

        appendBootstrap();

        e.preventDefault();
    });


    $('#mapTriggerLoader').click(function (e) {


        $('#mapSlideWrapper, #contactinfoWrapperPage').css('display', 'block');

        $('#contactinfoWrapper, #contactinfoWrapperPage').animate({
            marginLeft: '-2000px'
        }, 400, function () { });


        $('#mapSlideWrapper').animate({
            marginLeft: '25px'
        }, 400, function () { });


        appendBootstrap();

        e.preventDefault();
    });


    $('#mapReturn').click(function (e) {
        //$('#mapWrapper').css('margin-bottom', '3em');


        $('#mapSlideWrapper').animate({
            marginLeft: '3000px'
        }, 400, function () { });


        $('#contactinfoWrapper, #contactinfoWrapperPage').animate({
            marginLeft: '0'
        }, 400, function () {
            $('#mapSlideWrapper').css('display', 'none');
        });

        e.preventDefault();
    });


    /*
    |--------------------------------------------------------------------------
    | SCROLL (portfolio horizontal)
    |--------------------------------------------------------------------------
    */
    if ($("div#makeMeScrollable").length) {
        $("div#makeMeScrollable").smoothDivScroll({

            touchScrolling: true,
            mousewheelScrolling: "vertical"
        });


        $('.showInfo').click(function (e) {

            $('.pInfo').find('h3').css('margin-top', '-400px');
            $('.pInfo').css('display', 'none');
            $(this).next('div').slideDown(300);
            $('.showInfo').not($(this)).animate({
                marginTop: 0
            });
            $(this).animate({
                marginTop: $(this).width()
            }, 300);
            $(this).next('div').slideDown(300);
            $(this).next('div').children('h3').animate({
                marginTop: -5
            }, 300);

            e.preventDefault();

        });

        $('.closeInfo').click(function (e) {

            $(this).parent().slideUp('normal');
            $(this).parent().children('h3').animate({
                marginTop: -400
            }, 200, function () {
                $(this).parent().css('display', 'none');
            });
            $(this).parent().parent().find('img').animate({
                marginTop: 0
            });

            e.preventDefault();

        });

    }

    /*
    |--------------------------------------------------------------------------
    | FLEXSLIDER
    |--------------------------------------------------------------------------
    */
    if ($('.flexFullScreen').length) {
        $('.flexFullScreen').flexslider({
            animation: "slide",
            controlNav: false,
            directionNav: true,
            slideshow: false,
            prevText: '<i class="icon-left-open"></i>',
            nextText: '<i class="icon-right-open"></i>',
            start: function (slider) {
                setTimeout("animateTxt(" + slider.currentSlide + ", 'in')", 100);
            },
            before: function (slider) {
                setTimeout("animateTxt(" + slider.currentSlide + ")", 100);
            },
            after: function (slider) {
                setTimeout("animateTxt(" + slider.currentSlide + ", 'in')", 100);
            }
        });
    }


    if ($('.flexScreenSlider').length) {

        $('.flexScreenSlider').flexslider({
            animation: "slide",
            controlNav: false,
            prevText: '<i class="icon-left-open"></i>',
            nextText: '<i class="icon-right-open"></i>'
        });
    }



    /*
    |--------------------------------------------------------------------------
    | DIRECTIONAL ROLLOVER AND HOVER EFFECTS
    |--------------------------------------------------------------------------
    */

    if ($('.da-thumbs').length) {
        $('.da-thumbs article a').hoverdir();
    }


    if ($('.da-thumbs').length) {

        $('article a').hover(
            function () {


                if (Modernizr.csstransitions) {
                    $(this).find('section img').css('-webkit-transform', 'rotateY(180deg)');
                    $(this).find('section img').css('-moz-transform', 'rotateY(180deg)');
                    $(this).find('section img').css('-o-transform', 'rotateY(180deg)');
                    $(this).find('section img').css('transform', 'rotateY(180deg)');
                } else {

                    $(this).find('section img').animate({ opacity: '0.3' }, { duration: 300 });
                }


                var fromTop = ($('.imgWrapper', this).height() / 2 - $('.iconLink', this).height() / 2) + 10;
                $('.iconLink', this).css('margin-top', fromTop);


            },
            function () {


                if (Modernizr.csstransitions) {
                    $(this).find('section img').css('-webkit-transform', 'rotateY(0deg)');
                    $(this).find('section img').css('-moz-transform', 'rotateY(0deg)');
                    $(this).find('section img').css('-o-transform', 'rotateY(0deg)');
                    $(this).find('section img').css('transform', 'rotateY(0deg)');
                } else {

                    $(this).find('section img').animate({ opacity: '1' }, { duration: 300 });
                }

            }
            );
    }



    /*
    |--------------------------------------------------------------------------
    | ROLLOVER BTN
    |--------------------------------------------------------------------------
    */

    $('.socialIcon').hover(
        function () {
            $(this).stop(true, true).addClass('socialHoverClass', 300);
        },
        function () {
            $(this).removeClass('socialHoverClass', 300);
        });





    $('.tabs li, .accordion h2').hover(
        function () {
            $(this).stop(true, true).addClass('speBtnHover', 300);
        },
        function () {
            $(this).stop(true, true).removeClass('speBtnHover', 100);
        });





    /*
    |--------------------------------------------------------------------------
    | ALERT
    |--------------------------------------------------------------------------
    */
    $('.alert').delegate('button', 'click', function () {
        $(this).parent().fadeOut('fast');
    });


    /*
    |--------------------------------------------------------------------------
    | CLIENT
    |--------------------------------------------------------------------------
    */

    if ($('.colorHover').length) {
        var array = [];
        $('.colorHover').hover(

            function () {

                array[0] = $(this).attr('src');
                $(this).attr('src', $(this).attr('src').replace('-off', ''));

            },

            function () {

                $(this).attr('src', array[0]);

            });
    }


    /*
    |--------------------------------------------------------------------------
    | UP AND DOWN & MENU BTNS PORTFOLIO STATIC
    |--------------------------------------------------------------------------
    */

    $('.goDown').click(function (e) {

        var offset = $(this).parents().next('article').offset();
        var variation = ($('.mainNav').length) ? $('.mainNav').outerHeight(true) + 20 : 111;
        var finalPos = offset.top - variation;

        scrollTo(finalPos, 500);
        e.preventDefault();

    });


    $('.goUp').click(function (e) {

        var offset = $(this).parents().prev('article').offset();
        var variation = ($('.mainNav').length) ? $('.mainNav').outerHeight(true) + 20 : 111;
        var finalPos = offset.top - variation;

        scrollTo(finalPos, 500);
        e.preventDefault();
    });


    $('.PortfolioStickyMenu ul li a').click(function (e) {


        var targetId = $(this).attr('href');
        var offset = $(targetId).offset();
        var variation = ($('.mainNav').length) ? $('.mainNav').outerHeight(true) + 20 : 111;
        var finalPos = offset.top - variation;

        scrollTo(finalPos, 500);
        e.preventDefault();


    });

    /*
    |--------------------------------------------------------------------------
    | CAMERA SLIDER
    |--------------------------------------------------------------------------
    */
    if ($('.camera_wrap').length) {

        jQuery('.camera_wrap').camera({
            thumbnails: true,
            pagination: true,
            height: '30%',
            loader: 'none',
            loaderPadding: 0,
            loaderColor: '#ffffff',
            loaderBgColor: '#eeeeee',
            navigationHover: true,

            fx: 'simpleFade'
        });

    }

    if ($('.camera_wrap_nonav').length) {

        jQuery('.camera_wrap_nonav').camera({
            pagination: false,
            thumbnails: true,
            height: '70%'
        });

    }
    if ($('.camera_wrap_nothumb').length) {

        jQuery('.camera_wrap_nothumb').camera({
            pagination: false,
            thumbnails: false,
            height: '70%'
        });

    }




    if ($('#sequence').length) {
        var options = {
            nextButton: true,
            prevButton: true,
            pagination: true,
            animateStartingFrameIn: true,
            autoPlayDelay: 3000,
            preloader: true,
            preloadTheseFrames: [1],
            preloadTheseImages: [
            "images/slider/sequence/tn-model1.png",
            "images/slider/sequence/tn-model2.png",
            "images/slider/sequence/tn-model3.png"
            ]
        };

        var mySequence = $("#sequence").sequence(options).data("sequence");
    }

    /*
    |--------------------------------------------------------------------------
    | Main header Chnage when page scrolled 
    |--------------------------------------------------------------------------
    */
    //$(window).bind('scroll', function() {
    //    if($(window).width() > 960 && !$('.headerStyleCenteredLogo').length && !$('.headerStyleBigLogo').length && !$('.headerStyleThin').length && !$('#boxedLayout').length){
    //         if($(window).scrollTop() > 250){
    //             $('.preHeader').css('display', 'none');
    //             $('.mainNav').css('margin-top', '0');




    //             $('#mainMenu>ul>li>a').css('padding', '0em 1em 0.3em 1em');

    //             $('#mainMenu ul ul').css('top', '55px');


    //             $('#mainHeader .brand').css('margin-top', '0'); 


    //         }else{
    //             $('.preHeader').css('display', 'block');

    //             $('.mainNav').css('margin-top', '44px');


    //             $('#mainMenu>ul>li>a').css('padding', '0.6em 1em 0.7em 1em');
    //             $('#mainMenu ul ul').css('top', '69px');
    //             $('#mainHeader .brand').css('margin-top', '0.2em'); 
    //         }
    //    }

    // });

    //END DOCUMENT READY   
});



/*
|--------------------------------------------------------------------------
| EVENTS TRIGGER AFTER ALL IMAGES ARE LOADED
|--------------------------------------------------------------------------
*/
$(window).load(function () {

    /*
    |--------------------------------------------------------------------------
    | RS SLIDER
    |--------------------------------------------------------------------------
    */
    if ($('.fullwidthbanner').length) {

        $('.fullwidthbanner').css('display', 'block');

        if (jQuery().revolution) {

            $('.fullwidthbanner').revolution(
            {
                delay: 9000,
                startwidth: 1170,
                startheight: 432,

                onHoverStop: "on",// Stop Banner Timet at Hover on Slide on/off

                thumbWidth: 100,// Thumb With and Height and Amount (only if navigation Type set to thumb !)
                thumbHeight: 50,
                thumbAmount: 3,

                hideThumbs: 200,
                navigationType: "both",//bullet, thumb, none, both	 (No Shadow in Fullwidth Version !)
                navigationArrows: "verticalcentered",//nexttobullets, verticalcentered, none
                navigationStyle: "round",//round,square,navbar

                touchenabled: "on",// Enable Swipe Function : on/off

                navOffsetHorizontal: 0,
                navOffsetVertical: 20,

                stopAtSlide: -1,// Stop Timer if Slide "x" has been Reached. If stopAfterLoops set to 0, then it stops already in the first Loop at slide X which defined. -1 means do not stop at any slide. stopAfterLoops has no sinn in this case.
                stopAfterLoops: 0,// Stop Timer if All slides has been played "x" times. IT will stop at THe slide which is defined via stopAtSlide:x, if set to -1 slide never stop automatic



                fullWidth: "on",

                shadow: 0, //0 = no Shadow, 1,2,3 = 3 Different Art of Shadows -  (No Shadow in Fullwidth Version !)


                wrapper: '#sliderWrapper',
                wrapperheight: 430

            });

        }

    }



    /*
    |--------------------------------------------------------------------------
    | ISOTOPE USAGE FILTERING
    |--------------------------------------------------------------------------
    */
    if ($('.isotopeWrapper').length) {

        var $container = $('.isotopeWrapper');
        var $resize = $('.isotopeWrapper').attr('id');
        // initialize isotope

        $container.isotope({
            itemSelector: '.isotopeItem',
            resizable: false, // disable normal resizing
            masonry: {
                columnWidth: $container.width() / $resize
            }



        });

        $('#filter a').click(function () {



            $('#filter a').removeClass('current');
            $(this).addClass('current');
            var selector = $(this).attr('data-filter');
            $container.isotope({
                filter: selector
            });
            return false;
        });


        $(window).smartresize(function () {
            $container.isotope({
                // update columnWidth to a percentage of container width
                masonry: {
                    columnWidth: $container.width() / $resize
                }
            });
        });

        $container.delegate('.masoneryBloc a.sizer', 'click', function () {
            var $this = $(this);
            var resizeElement = $(this).parent();
            $this.find('div').css('display', 'none');

            if (Modernizr.csstransitions) {
                $this.find('section img').css('-webkit-transform', 'rotateY(0deg)');
                $this.find('section img').css('-moz-transform', 'rotateY(0deg)');
                $this.find('section img').css('-o-transform', 'rotateY(0deg)');
                $this.find('section img').css('transform', 'rotateY(0deg)');
            } else {

                $this.find('section img').animate({ opacity: '1' }, { duration: 300 });
            }


            if (resizeElement.hasClass('span3')) {

                resizeElement.removeClass('span3');
                resizeElement.addClass('span6');
                $this.find('div>span>i').attr('class', 'icon-minus');

            } else {

                resizeElement.addClass('span3');
                resizeElement.removeClass('span6');
                $this.find('div>span>i').attr('class', 'icon-plus');

            }




            if ($(this).parent().children('.hiddenInfo').css('display') == 'block') {

                $(this).parent().children('.hiddenInfo').css('display', 'none');
                $(this).parent().find('.iconZoom').css('background-position', '217px 702px');
                $(this).parent().find('.mask span').html('Read More');

            } else {

                $(this).parent().children('.hiddenInfo').css('display', 'block');
                $(this).parent().find('.iconZoom').css('background-position', '164px 689px');
                $(this).parent().find('.mask span').html('Minimize');

            }

            $container.isotope('reLayout');
            return false;
        });
    }


    /**PROCESS ICONS**/
    $('.iconBoxV3 a').hover(function () {

        if (Modernizr.csstransitions) {

            $(this).stop(false, true).toggleClass('hover', 150);
            $('i', this).css('-webkit-transform', 'rotateZ(360deg)');
            $('i', this).css('-moz-transform', 'rotateZ(360deg)');
            $('i', this).css('-o-transform', 'rotateZ(360deg)');
            $('i', this).css('transform', 'rotateZ(360deg)');

        } else {

            $(this).stop(false, true).toggleClass('hover', 150);

        }

    }, function () {

        if (Modernizr.csstransitions) {
            $(this).stop(false, true).toggleClass('hover', 150);
            $('i', this).css('-webkit-transform', 'rotateZ(0deg)');
            $('i', this).css('-moz-transform', 'rotateZ(0deg)');
            $('i', this).css('-o-transform', 'rotateZ(0deg)');
            $('i', this).css('transform', 'rotateZ(0deg)');

        } else {

            $(this).stop(false, true).toggleClass('hover', 150);
        }

    });



    //END WINDOW LOAD
});


/*
|--------------------------------------------------------------------------
| FUNCTIONS
|--------------------------------------------------------------------------
*/

/* CONTACT FROM */

jQuery(function () {
    if (jQuery("#contactfrm").length) {

        /*$('#name').prop('minlength', 2);
        $('#comments').prop('minlength', 10);*/

        // show a simple loading indicator
        var loader = jQuery('<div id="loader"><img src="images/loading.gif" alt="loading..." /></div>')
        .css({
            position: "relative",
            top: "1em",
            left: "25em",
            display: "inline"
        })
        .appendTo("body")
        .hide();
        jQuery().ajaxStart(function () {
            loader.show();
        }).ajaxStop(function () {
            loader.hide();
        }).ajaxError(function (a, b, e) {
            throw e;
        });

        var v = jQuery("#contactfrm").validate({
            // debug: true,
            errorPlacement: function (error, element) {
                error.insertBefore(element);
            },
            submitHandler: function (form) {
                jQuery(form).ajaxSubmit({
                    target: ".result"
                });
            },
            rules: {
                name: {
                    required: true,
                    minlength: 3
                },
                email: {
                    required: true,
                    email: true
                },
                phone: {
                    required: true,
                    minlength: 10,
                    digits: true
                },
                comment: {
                    required: true,
                    minlength: 10,
                    maxlength: 350
                }
            }
        });
    }

});

/* CONTACT FROM */

/* FLEXSLIDER INNER INFO CUSTOM ANIMATION */
function animateTxt(curSlide, action) {

    if (action == 'in') {
        var i = 0;
        var animaDelay = 0;

        $('.slideN' + curSlide + ':not([class*=clone])>.caption').css('display', 'block');

        $('.slideN' + curSlide + ':not([class*=clone])>.caption>div').each(function () {
            if (Modernizr.csstransitions) {

                $(this).css('-webkit-animation-delay', animaDelay + 's');
                $(this).css('-moz-animation-delay', animaDelay + 's');
                $(this).css('-0-animation-delay', animaDelay + 's');
                $(this).css('-ms-animation-delay', animaDelay + 's');
                $(this).css('animation-delay-delay', animaDelay + 's');

                $(this).show().addClass('animated').addClass($(this).attr('data-animation'));

            } else {

                $('.slideN' + curSlide + ':not([class*=clone])>.caption>div').hide();
                if (i == 0) { timing = 0 } else if (i == 1) { timing = 300 } else { timing = 300 * i }
                $(this).delay(timing).fadeIn('fast');
            }
            i++;
            animaDelay = animaDelay + 0.2;


        });

    } else {
        var j = 0;
        $('.slideN' + curSlide + ':not([class*=clone])>.caption').css('display', 'none');

        $('.slideN' + curSlide + ':not([class*=clone])>.caption>div').each(function () {
            if (Modernizr.csstransitions) {

                $(this).removeClass($(this).attr('data-animation')).removeClass('animated').hide();

            } else {
                $(this).hide();
            }
            j++;
        });
    }

}





/* MAIN MENU (submenu slide and setting up of a select box on small screen)*/
(function () {

    var $mainMenu = $('#mainMenu').children('ul');

    $mainMenu.on('mouseenter', 'li', function () {


        var $this = $(this),

        $subMenu = $this.children('ul');


        if ($subMenu.length) $this.addClass('hover').stop();
        //else {
        //    if ($this.parent().is($(':gt(1)', $mainMenu))) {
        //        $this.stop(false, true).hide().fadeIn('slow');
        //    } else {
        //        $this.stop(false, true);
        //    }
        //}


        if ($this.parent().is($(':gt(1)', $mainMenu))) {

            $subMenu.css('display', 'block');
            $subMenu.stop(false, true).animate({
                left: 150,
                opacity: 1
            }, 100, 'easeOutQuad');




        } else {

            $subMenu.stop(false, true).slideDown(100, 'easeInOutQuad');

        }


    }).on('mouseleave', 'li', function () {


        var $nthis = $(this);
        if ($nthis.parent().is($(':gt(1)', $mainMenu))) {

            $nthis.children('ul').stop(false, true).css('left', 130).css('opacity', 0).css('display', 'none');

        } else {

            $nthis.removeClass('hover').removeClass('Shover').children('ul').stop(false, true).hide();
        }

        $subMenu = $nthis.children('ul');

        if ($subMenu.length) $nthis.removeClass('hover');
        else $nthis.removeClass('Shover');


    }).on('touchend', 'li ul li a', function (e) {

        var el = $(this);
        var link = el.attr('href');
        window.location = link;

    });


    // ul to select
    var optionsList = '<option value="" selected>انتخاب منو ...</option>';
    $mainMenu.find('li').each(function () {
        var $this = $(this),
        $anchor = $this.children('a'),
        depth = $this.parents('ul').length - 1,
        indent = '';

        if (depth) {
            while (depth > 0) {
                indent += ' - ';
                depth--;
            }
        }

        optionsList += '<option value="' + $anchor.attr('href') + '">' + indent + ' ' + $anchor.text() + '</option>';
    }).end().after('<select class="responsive-nav">' + optionsList + '</select>');

    $('.responsive-nav').on('change', function () {
        window.location = $(this).val();
    });

})();


/* BACK TO TOP (BTN back to top by Matt Varone)*/
(function () {

    var defaults = {
        text: '<i class="icon-up-open"></i>',
        min: 50,
        inDelay: 600,
        outDelay: 400,
        containerID: 'to-top',
        containerCLASS: 'iconWrapper',
        containerHoverID: 'to-top',
        scrollSpeed: 300,
        easingType: 'linear'
    },

    settings = $.extend(defaults);

    $('#backToTop a').click(function () {
        $('html, body').animate({
            scrollTop: 0
        }, settings.scrollSpeed, settings.easingType);

        return false;
    });


})();


/*
|--------------------------------------------------------------------------
| SIDEBAR MENU FOLLOWING WINDOW SCROLL
|--------------------------------------------------------------------------
*/

function scrollTo($position, $animationTime) {

    $('html,body').animate({
        scrollTop: $position
    }, $animationTime);

}

/*
|--------------------------------------------------------------------------
| STICKY MENU
|--------------------------------------------------------------------------
*/

$(function () {
    $window = $(window);
    if ($(".PortfolioStickyMenu").length && $window.width() >= 754) {
        var $sidebar = $(".PortfolioStickyMenu"),

        offset = $sidebar.offset(),
        topPadding = $('.mainNav').outerHeight(true) + 21;


        $window.scroll(function () {
            if ($window.scrollTop() > offset.top) {
                if (($window.scrollTop() - offset.top + topPadding) <= $('.lastArticle').position().top + 100) {
                    $sidebar.stop().animate({
                        marginTop: $window.scrollTop() - offset.top + topPadding
                    });
                }
            } else {
                $sidebar.stop().animate({
                    marginTop: 0
                });
            }
        });
    }

});


/*
|--------------------------------------------------------------------------
| GOOGLE MAP
|--------------------------------------------------------------------------
*/

function appendBootstrap() {
    var script = document.createElement("script");
    script.type = "text/javascript";
    script.src = "http://maps.google.com/maps/api/js?sensor=false&callback=initialize";
    document.body.appendChild(script);
}




function initialize(id) {

    var image = 'images/icon-map.png';

    var overlayTitle = 'Agencies';

    var locations = [
        //point number 1
        ['Madison Square Garden', '4 Pennsylvania Plaza, New York, NY'],

        //point number 2
        ['Best town ever', 'Santa Cruz', 36.986021, -122.02216399999998],

        //point number 3 
        ['Located in the Midwestern United States', 'Kansas'],

        //point number 4
        ['I\'ll definitly be there one day', 'Chicago', 41.8781136, -87.62979819999998]


    ];

    /*** DON'T CHANGE ANYTHING PASSED THIS LINE ***/
    id = (id == undefined) ? 'mapWrapper' : id;

    var map = new google.maps.Map(document.getElementById(id), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        scrollwheel: false,
        zoomControl: true,
        zoomControlOptions: {
            style: google.maps.ZoomControlStyle.LARGE,
            position: google.maps.ControlPosition.LEFT_CENTER
        },
        streetViewControl: true,
        scaleControl: false,
        zoom: 14

    });

    var myLatlng;
    var marker, i;
    var bounds = new google.maps.LatLngBounds();
    var infowindow = new google.maps.InfoWindow({ content: "loading..." });

    for (i = 0; i < locations.length; i++) {


        if (locations[i][2] != undefined && locations[i][3] != undefined) {
            var content = '<div class="infoWindow">' + locations[i][0] + '<br>' + locations[i][1] + '</div>';
            (function (content) {
                myLatlng = new google.maps.LatLng(locations[i][2], locations[i][3]);

                marker = new google.maps.Marker({
                    position: myLatlng,
                    icon: image,
                    title: overlayTitle,
                    map: map
                });

                google.maps.event.addListener(marker, 'click', (function (marker, i) {
                    return function () {
                        infowindow.setContent(content);
                        infowindow.open(map, this);
                    }

                })(this, i));

                if (locations.length > 1) {
                    bounds.extend(myLatlng);
                    map.fitBounds(bounds);
                } else {
                    map.setCenter(myLatlng);
                }

            })(content);
        } else {

            geocoder = new google.maps.Geocoder();
            var info = locations[i][0];
            var addr = locations[i][1];
            var latLng = locations[i][1];

            (function (info, addr) {

                geocoder.geocode({

                    'address': latLng

                }, function (results, status) {

                    myLatlng = results[0].geometry.location;

                    marker = new google.maps.Marker({
                        position: myLatlng,
                        icon: image,
                        title: overlayTitle,
                        map: map
                    });
                    var $content = '<div class="infoWindow">' + info + '<br>' + addr + '</div>';
                    google.maps.event.addListener(marker, 'click', (function (marker, i) {
                        return function () {
                            infowindow.setContent($content);
                            infowindow.open(map, this);
                        }
                    })(this, i));

                    if (locations.length > 1) {
                        bounds.extend(myLatlng);
                        map.fitBounds(bounds);
                    } else {
                        map.setCenter(myLatlng);
                    }
                });
            })(info, addr);

        }
    }
}





/*
|--------------------------------------------------------------------------
| SHARRRE
|--------------------------------------------------------------------------
*/
if ($('#shareme').length) {
    $('#shareme').sharrre({
        share: {
            googlePlus: true,
            facebook: true,
            twitter: true,
        },
        buttons: {
            googlePlus: { size: 'tall' },
            facebook: { layout: 'box_count' },
            twitter: { count: 'vertical' },
        },
        enableHover: false,
        enableCounter: false,
        enableTracking: true,
        //url:'document.location.href'
    });
}




