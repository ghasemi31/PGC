function rightpanel_ready() {

    $('#right-panel .cat-open .title').css("color", "#e6b705");
    $('.title').hover(
        function () {
            if (!$(this).parent().hasClass('cat-open')) {
                $(this).css("color", "#E0E0E0");
            }          
        },
        function () {
            if (!$(this).parent().hasClass('cat-open')) {
                $(this).css("color", "#ffffff");
            }
        })
    $('#right-panel .cat .title').click(function () {        
        if ($(this).parent().hasClass('cat-open')) {
            $(this).parent().removeClass('cat-open');
            //$('i', this).toggleClass('fa-caret-down fa-caret-left');
            $('i', this).removeClass('fa-caret-down');
            $('i', this).addClass('fa-caret-left');
            $(this).css("color", "#FFF");
            $(this).next().slideUp();
        }
        else {
            $('#right-panel .cat').each(function () {
                $(this).removeClass('cat-open');
                //$('.title i', this).toggleClass('fa-caret-left fa-caret-down');
                $('.title i', this).removeClass('fa-caret-down');
                $('.title i', this).addClass('fa-caret-left');
                $('.title',this).css("color", "#FFF");
                $(this).children('.sub').slideUp();
            });

            $(this).parent().addClass('cat-open');
            $('i', this).removeClass('fa-caret-left');
            $('i', this).addClass('fa-caret-down');
            $(this).css("color", "#e6b705");
            $(this).next().slideDown();
        }
       
    });

    $("#right-panel .wrapper .section .freeweb .default-text").click(function () {
        $(this).hide();
        $(this).siblings("input").focus();
    });

    // Right Pane Toggle
    $("#right-panel .toggle").click(function () {
        if ($(this).is(".opened")) {
            $("#right-panel .wrapper").animate({ "top": "-630px" }, function () { $("#right-panel .toggle").removeClass("opened") });
        }
        else {
            $("#right-panel .wrapper").animate({ "top": "-50px" }, function () { $("#right-panel .toggle").addClass("opened") });
        }
        $("#ajax-modal").fadeOut();
        $("#ajax-modal .wrapper .dynamic-content").html('');
        $("#ajax-modal .wrapper .head").html('');

        $("#ajax-modal2").fadeOut();
        $("#ajax-modal2 .wrapper .dynamic-content").html('');
        $("#ajax-modal2 .wrapper .head").html('');
    });

    // Right Panel Sections Buttons
    $("#right-panel .wrapper .section .btn, #right-panel .wrapper .msg .btn, #right-panel .wrapper .msg a, #right-panel .wrapper .section .item").click(function () {
        var a = $(this);
        if (!a.is(".logout")) {
            $("#ajax-modal2").fadeOut();
            $("#ajax-modal2 .wrapper .dynamic-content").html('');
            $("#ajax-modal2 .wrapper .head").html('');
            $("#ajax-modal").css({ "top": a.position().top - 70 + "px" });
            $("#ajax-modal .wrapper .head").html(a.text());
            $("#ajax-modal .wrapper .dynamic-content").load(a.attr("page") + ".aspx",
				function () {
				    $(".modal-option .close").unbind("click");
				    $(".modal-option .close").click(function () {
				        $("#ajax-modal").fadeOut();
				        $("#ajax-modal .wrapper .dynamic-content").html('');
				        $("#ajax-modal .wrapper .head").html('');
				    });

				    // Text Change
				    $(".modal-form p span.inputlbl").unbind("click");
				    $(".modal-form p span.inputlbl").click(function () {
				        $(this).hide();
				        $(this).siblings("input").focus();
				    });

				    $(".modal-form p input").unbind("blur");
				    $(".modal-form p input").blur(function () {
				        if ($(this).val() == "") {
				            $(this).siblings("span.inputlbl").html($(this).siblings("span.inputlbl").attr("dtxt")).show();
				        }
				    });

				    // Help Tooltip
				    $(".help").easyTooltip({ tooltipId: "easyTooltip2" });
				});
            $("#ajax-modal").fadeIn();

            return false;
        }
        else {
            return true;
        }
    });

}
