/// <reference path="sample.js" />
/// jQueryBlockUI is needed
function hasMessage() {
    if ($("#usermessage .text").html() != null && $("#usermessage .text").html().length > 0) {
        return true;
    }
    else {
        return false;
    }
}

$(document).ready(function () {
    try {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            showMessage();
        });
    } catch (err) { }

    showMessage();
});

function showMessage() {
    if (hasMessage()) {
        $.blockUI({ overlayCSS: { backgroundColor: '#FFFFFF', cursor: 'default' }, message: $('#usermessage'), css: { border: 'none', background: 'transparent'} });
    }
}
