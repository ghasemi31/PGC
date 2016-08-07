///////////////product///////////////

$(document).ready(function () {

    $(window).load(function () { nameLineShow(); });

    /////////////////////////////////////////////////////////////// createRawMaterial
//    $("#bean").mouseover(function () { overlay(0, '#beanOutline'); });
//    $("#spices").mouseover(function () { overlay(1, '#spicesOutline'); });
//    $("#potato").mouseover(function () { overlay(2, '#potatoOutline'); });
//    $("#meat").mouseover(function () { overlay(3, '#meatOutline'); });
//    $("#onion").mouseover(function () { overlay(4, '#onionOutline'); });
//    $("#tomato").mouseover(function () { overlay(5, '#tomatoOutline'); });

    /////////////////////////////////////////////////////////////// createRawMaterial

    function createRawMaterial(which) {
        //alert(which);
        switch (which) {
            case 'o':
                var rawMaterials = new Array();
                rawMaterials[0] = "#obeanNameLine";
                rawMaterials[1] = "#ospicesNameLine";
                rawMaterials[2] = "#opotatoNameLine";
                rawMaterials[3] = "#omeatNameLine";
                rawMaterials[4] = "#oonionNameLine";
                rawMaterials[5] = "#otomatoNameLine";
                break;
            default:
                var rawMaterials = new Array();
                rawMaterials[0] = "#beanNameLine";
                rawMaterials[1] = "#spicesNameLine";
                rawMaterials[2] = "#potatoNameLine";
                rawMaterials[3] = "#meatNameLine";
                rawMaterials[4] = "#onionNameLine";
                rawMaterials[5] = "#tomatoNameLine";
        }

        return rawMaterials;
    }

    /////////////////////////////////////////////////////////////// everyTime


    function nameLineShow() {

        $(document).stopTime('raw');
        $(document).stopTime('oraw');

        rawMaterials = createRawMaterial('');
        $(document).everyTime(3000, 'raw', function (i) {

            turn = turnChanger(turn);
            check_Visibility(rawMaterials, turn);

        });
    }

    /////////////////////////////////////////////////////////////// check_Visibility
    function check_Visibility(rawMaterials, turn) {

        //alert(turn);
        RMlen = rawMaterials.length;
        for (i = 0; i <= RMlen; i++) {
            $(rawMaterials[i]).hide('slow');
            //if(visibility != 'none') { $(rawMaterials[i]).show(); }
        }

        $(rawMaterials[turn]).show('slow');


    }

    /////////////////////////////////////////////////////////////// hideEveryRaw
    function hideEveryRaw(rawMaterials) {
        
        RMlen = rawMaterials.length;
        for (i = 0; i <= RMlen; i++) {
            $(rawMaterials[i]).hide('fast');
        }

    }

    /////////////////////////////////////////////////////////////// hideEveryOutline
    function hideOutline() {

        var OutlineMaterials = new Array();
        OutlineMaterials[0] = "#beanOutline";
        OutlineMaterials[1] = "#spicesOutline";
        OutlineMaterials[2] = "#potatoOutline";
        OutlineMaterials[3] = "#meatOutline";
        OutlineMaterials[4] = "#onionOutline";
        OutlineMaterials[5] = "#tomatoOutline";


        RMlen = OutlineMaterials.length;
        for (i = 0; i <= RMlen; i++) {
            $(OutlineMaterials[i]).hide('fast');
        }

    }

    /////////////////////////////////////////////////////////////// turnChanger
    var turn = -1;
    function turnChanger(turn, deny) {

        if (turn == 5) { turn = 0; }
        else { turn++; }
        if (turn == deny) { turn++ } /* for denying of the over */
        return (turn % 6);

    }

    /////////////////////////////////////////////////////////////// overlay
    function overlay(deny, rawMFocus) {

        //old
        //        CRM = createRawMaterial();
        //        $('#RawMaterials').hide('fast');
        //        var heightWin = $(document).height(); // get window height
        //        $('#overlay').css('height', heightWin);
        //        $(document).stopTime('raw');
        //        $('#overlay').fadeIn('slow');
        //        $(rawMFocus).show('fast');

        //new
        CRM = createRawMaterial();
        $('#RawMaterials').hide('fast');
        $('#whitedoc').css('height', getDocumentHeight());
        $('#whitedoc').css('width', $(document).width());
        $('#whitedoc').fadeIn('slow');
        $(document).stopTime('raw');
        $('#overlay').fadeIn('slow');
        $(rawMFocus).show('fast');

    }
//    $("#beanOutline, #spicesOutline, #potatoOutline, #meatOutline, #onionOutline, #tomatoOutline ").mouseout(function () {
//        //turn = -1;
//        //alert('exiting ...');
//        $('#whitedoc').fadeOut('slow');


//        hideOutline();
//        $(document).stopTime('oraw');
//        OCRM = createRawMaterial('o');
//        hideEveryRaw(OCRM);
//        nameLineShow();
//        $('#RawMaterials').show('fast');
//        $('#overlay').fadeOut('slow');


//    });

//    $("#overlay").click(function () {
//        //turn = -1;
//        $('#whitedoc').fadeOut('slow');
//        hideOutline();
//        $("#overlay").fadeOut('slow');
//        $(document).stopTime('oraw');
//        hideEveryRaw(OCRM);
//        nameLineShow();
//        $('#RawMaterials').show('fast');

//    });

});

function getDocumentHeight() {
    var body = document.body,
    html = document.documentElement;
    var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);
    return height;
}