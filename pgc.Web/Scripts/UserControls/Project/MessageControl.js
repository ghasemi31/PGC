
(function () {

    var global = {};

    global.vars = {};

    global.fn = {

        // Language Toggle
        toggle_lang: function (elem) {
            elem.keypress(function () {
                $g.fn.check_sms(elem);
            });

            elem.keyup(function () {
                $g.fn.check_sms(elem);
            });
        },

        // Check SMS
        check_sms: function (elem) {
            var text = elem.val();
            var ucs2 = text.search(/[^\x00-\x7E]/) != -1
            if (!ucs2) {
                text = text.replace(/([[\]{}~^|\\])/g, "\\$1");
            }
            var maxchars = ucs2 ? 70 : 160;

            $("#sms-lang").html(ucs2 ? 'فارسی' : 'انگلیسی');
            $("#hdn-lang").val(ucs2 ? 1 : 2);
            if (text.match(/^[^a-z]*[^\x00-\x7E]/ig)) {
                elem.css({ "direction": "rtl", "text-align": "right" });
            }
            else {
                elem.css({ "direction": "ltr", "text-align": "left" });
            }

            if (text.length > maxchars) {
                if (ucs2) {
                    maxchars = maxchars - 3;
                }
                else {
                    maxchars = maxchars - 7;
                }
            }

            var count = Math.max(Math.ceil(text.length / maxchars), 1);
            $("#remaining-characters").html(' تعداد کاراکتر باقیمانده  <strong>' + (maxchars * count - text.length) + '</strong> ( <strong>' + count + '</strong> صفحه ) ');
            $("#hdn-smspc").val(count);
        }

    }

    $g = global;


    /********** BINDS **********/
    // Feedback
    $("#feedback .btn").click(function () {
        if ($("#feedback").is(".active")) {
            $("#feedback").animate({ "left": "-330px" }, function () {
                $("#feedback").removeClass("active");
            });
        }
        else {
            $("#feedback").animate({ "left": "0" }, function () {
                $("#feedback").addClass("active");
            });
        }
        return false;
    });

    // Quick Send
    $("#quicksend .btn").click(function () {
        if ($("#quicksend").is(".active")) {
            $("#quicksend").animate({ "right": "-330px" }, function () {
                $("#quicksend").removeClass("active");
            });
        }
        else {
            $("#quicksend").animate({ "right": "0" }, function () {
                $("#quicksend").addClass("active");
            });
        }
        return false;
    });

    // Hide Panels
    $('#main-wrapper').click(function () {
        if ($("#feedback").is(".active")) {
            $("#feedback").animate({ "left": "-330px" }, function () {
                $("#feedback").removeClass("active");
            });
        }
        if ($("#quicksend").is(".active")) {
            $("#quicksend").animate({ "right": "-330px" }, function () {
                $("#quicksend").removeClass("active");
            });
        }
        if ($("#user-dropdown").is(".active")) {
            $("#user-dropdown").removeClass("active");
            $("#user-dropdown ul").fadeOut();
        }
        if ($("#control_panel").is(".active")) {
            $("#control_panel").removeClass("active");
            $("#panel-wrapper").fadeOut(function () { $("#panel-wrapper").addClass("hide") });
        }
        try {
            if ($("#file-dropdown").is(".active")) {
                $("#file-dropdown").removeClass("active");
                $("#file-dropdown ul").fadeOut();
            }
        } catch (err) { }
    });

    // Add ToNumber
    $("#add-tonumver").click(function () {
        if ($(".frm-row.added").length < 9) {
            var row = '<div class="frm-row added"><label>&nbsp;</label><input type="text" name="tonumber" class="public-input" style="direction:ltr; text-align:left; width:125px; float:right;" /><div class="remove-tonumver" title="حذف شماره"></div></div>';
            if ($(".frm-row.added").length > 0) {
                $(".frm-row.added:last").after(row);
            }
            else {
                $("#tonumber-wrapper").after(row);
            }
            remove_tonumber();
        }
    });

    // Remove ToNumber
    function remove_tonumber() {
        $(".remove-tonumver").click(function () {
            $(this).parent(".frm-row").remove();
        });
    }

})();

function restrictNum(e, hasDot, hasSpace, hasBrace, hasPlus) {

    var key = (window.event) ? event.keyCode : e.which;
    if (key == 0 || key == 13) return;
    var bl = (key > 47 && key < 58);
    bl = (hasDot) ? (bl || key == 46) : bl;
    bl = (hasSpace) ? (bl || key == 32) : bl;
    bl = (hasBrace) ? (bl || key == 40 || key == 41) : bl;
    bl = (hasPlus) ? (bl || key == 43) : bl;

    if (bl)
        return;
    else {
        if (window.event) { window.event.returnValue = null; e.preventDefault(); }
        else { e.preventDefault(); }
    }
}
function lockkeypress(e) {
    var key = (window.event) ? event.keyCode : e.which;
    if (key == 0) return;
    if (window.event) { window.event.returnValue = null; e.preventDefault(); }
    else { e.preventDefault(); }
}
function onTogglePanel() {
    try { closeDP(); } catch (e) { }
}
function SMSKeyPress(elem) {
    var text = $(elem).val();
    var ucs2 = text.search(/[^\x00-\x7E]/) != -1
    if (!ucs2) {
        text = text.replace(/([[\]{}~^|\\])/g, "\\$1");
    }
    var maxchars = ucs2 ? 70 : 160;

    $(elem).parent().next('*').find('.sms_lang').html(ucs2 ? 'فارسی' : 'انگلیسی');
    if (text.match(/^[^a-z]*[^\x00-\x7E]/ig)) {
        $(elem).css({ "direction": "rtl", "text-align": "right" });
    }
    else {
        $(elem).css({ "direction": "ltr", "text-align": "left" });
    }

    if (text.length > maxchars) {
        if (ucs2) {
            maxchars = maxchars - 3;
        }
        else {
            maxchars = maxchars - 7;
        }
    }

    var count = Math.max(Math.ceil(text.length / maxchars), 1);
    //alert(' تعداد کاراکتر باقیمانده  <strong>' + (maxchars * count - text.length) + '</strong> ( <strong>' + count + '</strong> صفحه ) ');
    $(elem).parent().next('*').find('.remaining-characters').html(' تعداد کاراکتر باقیمانده  <strong>' + (maxchars * count - text.length) + '</strong> ( <strong>' + count + '</strong> صفحه ) ');
    //$("#remaining-characters").html(' تعداد کاراکتر باقیمانده  <strong>' + (maxchars * count - text.length) + '</strong> ( <strong>' + count + '</strong> صفحه ) ');
    //    $("#remaining-characters").html('testttttt');

}