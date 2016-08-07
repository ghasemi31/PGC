/// <reference path="sample.js" />
function formatRange(cboID, andID , firstID , secondID) {
    var cbo = $('#' + cboID);
    var and = $('#' + andID);
    var first = $('#' + firstID);
    var second = $('#' + secondID);

    switch (cbo.val()) {
        case '0':
            first.hide();
            and.hide();
            second.hide();
            break;
        case '1':
        case '2':
        case '3':
            first.show();
            and.hide();
            second.hide();
            break;
        case '4':
            first.show();
            and.show();
            second.show();
            break;
    }
}
function currencyToLetters(number) {
	var worth = new Array("", "هزار", "ميليون", "ميليارد", "بليارد", "تليارد", "بيليون", "تليون", "", "", "")
	var output = "";
	var parts = number.split(",");
	for (i = 0; i < parts.length; i++) {
		if (parts[i] != "000") {
			if (i != 0) output += " و ";
			output += parToLetters(parts[i]) + " " + worth[parts.length - i - 1];
		}
	}
	return output;
}
function parToLetters(number) {
	number = parseInt(number, 10) + "";
	var first = new Array("صفر", "يک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه", "ده", "يازده", "دوازده", "سيزده", "چهارده", "پانزده", "شانزده", "هفده", "هجده", "نوزده");
	var second = new Array("", "", "بيست", "سي", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود");
	var third = new Array("", "صد", "دويست", "سيصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد");
	if (parseInt(number) >= 0 && parseInt(number) <= 19)
		return first[number];
	if (parseInt(number) >= 20 && parseInt(number) <= 99)
		return second[number.charAt(0)] + ((number.charAt(1) == '0') ? "" : " و " +  first[parseInt(number.charAt(1))]);
	if (parseInt(number) % 100 == 0)
		return third[parseInt(number) / 100];
	return third[number.charAt(0)] + " و " + parToLetters(number.substr(1));
}
function FormatCurrency(inp) {
	inp.value = 0 + inp.value;
	number = filterNum(inp.value);
	number = parseInt(number, 10) + "";
	if (number.length > 3)
	{
		var mod = number.length % 3;
		var output = (mod > 0 ? (number.substring(0, mod)) : '');
		for (i=0 ; i < Math.floor(number.length / 3); i++)
		{
			if ((mod == 0) && (i == 0))
				output += number.substring(mod+ 3 * i, mod + 3 * i + 3);
			else
				output += ',' + number.substring(mod + 3 * i, mod + 3 * i + 3);
		}
	}
	else 
		output = number;
	if(output == 0 && inp.value == "0")
	    inp.value = "";
	 else
	    inp.value = output;

	 //new
	 var letter = $('#' + inp.id + '_Letter');
	 if (letter) {
	     letter.html(currencyToLetters(output));
	     showLetter(inp,false);
	 }
}

function filterNum(inp){
	re = /^\$|[^0-9]|,/g;
    return inp.replace(re, "");
}

function showLetter(inp,isfocus) {
    var txt = $('#' + inp.id);
    var letter = $('#' + inp.id + '_Letter');
    if (txt.val() != '') {
        if (letter.css('display') == 'none') {
            letter.fadeIn();
        }
        if (isfocus) {
            txt.select();
        }
    }
    else {
        letter.fadeOut();
    }
}

function hideLetter(inp) {
    var letter = $('#' + inp.id + '_Letter');
    letter.fadeOut();
}

