function initIconPicker(picker) {
   

    var ValueHolder = $(picker).find("input[id$=hfValue]:hidden");

    var icon_set = $(picker).find(".IcnSet").val();

    var value = $(ValueHolder).val();
   

    $(picker).find('.icn-picker').iconpicker({
            align: 'center', // Only in div tag
            arrowClass: 'btn-danger',
            arrowPrevIconClass: 'fa fa-chevron-circle-right',
            arrowNextIconClass: 'fa fa-chevron-circle-left',
            cols: 7,
            footer: true,
            header: true,
            icon: value,
            iconset: icon_set,
            labelHeader: 'صفحه {0} از {1}',
            labelFooter: '{0} - {1}  از {2}  آیکون',
            placement: 'bottom', // Only in button tag
            rows: 3,
            search: true,
            searchText: 'جستجو...',
            selectedClass: 'btn-success',
            unselectedClass: ''
        });
 
        //$(".search-control").val(value.replace("fa-", ""));

    $(picker).find('.icn-picker').on('change', function (e) {
        $(ValueHolder).val(e.icon);
        //$(".search-control").val(e.icon.replace("fa-", ""));
    });
};

$(document).on("callback", function () {
    $(".icon-picker").each(function () {
        
        initIconPicker(this);
    })
    
});


//$(document).ready(function () {
//    $(".icon-picker").each(function () {
//        initIconPicker(this);
//    })
//});