
function closeHtmlLookup() {
    $($Dlg).dialog('close');
}
var $Dlg;
function openHtmlEditorLookup(contID, width, height, url) {
    var textarea = $('#' + contID).children('textarea');
    var framecontainer = $('#' + contID + '_cont');
    var iframe = $('#' + contID + '_frame');
    try {
        iframe.attr('src', url + '?cont=' + contID);
        $(iframe).attr('onload', (function () {
            $Dlg = $(framecontainer).dialog({
                modal: true,
                autoOpen: false,
                closeOnEscape: true,
                title: 'Html ویرایشگر',
                width: width,
                height: height,
                show: 'fade',
                hide: 'fade',
                open: function (event, ui) {
                    iframe.attr('style', 'display:block;');
                },
                close: function (event, ui) {
                    try {
                        iframe.attr('style', 'display:none;');
                        iframe.attr('src', '');
                        $($Dlg).dialog('destroy');
                        $Dlg = null;
                    }
                    catch (err) { alert(err); }
                }
            });
            $($Dlg).dialog('open').html(iframe);
            
        }));
    } catch (err) { alert( err);}

    return false;
}

