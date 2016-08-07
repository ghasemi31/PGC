function toggleLang(id) {
    if ($(id).val().match(/^[^a-z]*[^\\x00-\\x7E]/ig)) {
        $(id).css({ 'direction': 'rtl', 'text-align': 'right' });
    }
    else {
        $(id).css({ 'direction': 'ltr', 'text-align': 'left' });
    }
}
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