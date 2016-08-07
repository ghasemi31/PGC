// Settings
function filedropdown_Click() {
    if ($("#file-dropdown").is(".active")) {
        $("#file-dropdown").removeClass("active");
        $("#file-dropdown ul").hide();
    }
    else {
        $("#file-dropdown ul").fadeIn(function () { $("#file-dropdown").addClass("active") });
    }
}