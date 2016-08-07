
$(document).on("callback", function () {
    $('#chkAllBranches').on("click", function () {        
        if ($(this).is(":checked")) {
            $('.branch input[type=checkbox]').attr('checked', true);
        }
        else {
            $('.branch input[type=checkbox]').attr('checked', false);
        }       
    })
})