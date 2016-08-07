/// <reference path="jquery-1.7.2.min.js" />
$(document).ready(function () {

    try { rightpanel_ready(); } catch (err) { }
    try { nested_master_ready(); } catch (err) { }
    try { page_ready(); } catch (err) { }
    try {
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            try {
                $.event.trigger({
                    type: "callback",
                });
            } catch (err) { console.log(err) }
        });
    } catch (err) { console.log(err) }

});