/// <reference path="sample.js" />
/// jQueryBlockUI is needed
$(document).ready(function () {
    try {
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function () {
            $.blockUI({ overlayCSS: { backgroundColor: '#FFFFFF' }, message: $('#divBlock'), css: { border: 'none', background: 'transparent'} });
        });
    } catch (err) { }

    try {

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            var hasmsg = null;
            try { hasmsg = hasMessage() } catch (err) { }
            if (hasmsg == null || hasmsg == false) { $.unblockUI(); }
        });
    } catch (err) { }
});
