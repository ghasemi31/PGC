$(document).on("callback", function () {
    tableRow();
})
$(document).ready(function () {
    tableRow();
})


var tableRow = function () {
    $("tr:odd").addClass("Row");
    $("tr:even").addClass("AltRow");
}