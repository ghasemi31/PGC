function btndel(sender,path,page) {
    if (!confirm('Are you sure?')) {return false;}
    
    //send ajax request for file deletion
    $.ajax({
        url: page + '?FilePath=' + path,
        cache: false
    }).done(function (html) {
        var res;
        if ($(html).find('#result').text() == 'True') { res = true; } else { res = false; }
        if (res == false) { alert('File deletion failed!'); }
    });

    var loadedview = $(sender).parents('.loadedview');
    var newview = loadedview.prev();
    var hdf = loadedview.next('input');
    hdf.val('');
    loadedview.fadeOut(function () { newview.fadeIn(); });
}
//function path_changed(newpath, contId) {
//    //alert('path changed : ' + newpath);
//    alert('done in parent : ' + newpath);
//    var cont = document.getElementById(contId);
//    $(cont).find('.hdf_FilePath').val(newpath);
//}
//function hideValidator(contId) {
//    var cont = document.getElementById(contId);
////    $(cont).find('.validator-fileupload').hide();
//}
//function showValidator(contId) {
//    var cont = document.getElementById(contId);
////    $(cont).find('.validator-fileupload').show();
//}