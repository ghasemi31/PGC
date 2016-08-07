﻿(function($,len,createRange,duplicate){
	$.fn.caret=function(options,opt2){
		var start,end,t=this[0],browser=$.browser.msie;
		if(typeof options==="object" && typeof options.start==="number" && typeof options.end==="number") {
			start=options.start;
			end=options.end;
		} else if(typeof options==="number" && typeof opt2==="number"){
			start=options;
			end=opt2;
		} else if(typeof options==="string"){
			if((start=t.value.indexOf(options))>-1) end=start+options[len];
			else start=null;
		} else if(Object.prototype.toString.call(options)==="[object RegExp]"){
			var re=options.exec(t.value);
			if(re != null) {
				start=re.index;
				end=start+re[0][len];
			}
		}
		if(typeof start!="undefined"){
			if(browser){
				var selRange = this[0].createTextRange();
				selRange.collapse(true);
				selRange.moveStart('character', start);
				selRange.moveEnd('character', end-start);
				selRange.select();
			} else {
				this[0].selectionStart=start;
				this[0].selectionEnd=end;
			}
			this[0].focus();
			return this
		} else {
           if(browser){
				var selection=document.selection;
                if (this[0].tagName.toLowerCase() != "textarea") {
                    var val = this.val(),
                    range = selection[createRange]()[duplicate]();
                    range.moveEnd("character", val[len]);
                    var s = (range.text == "" ? val[len]:val.lastIndexOf(range.text));
                    range = selection[createRange]()[duplicate]();
                    range.moveStart("character", -val[len]);
                    var e = range.text[len];
                } else {
                    var range = selection[createRange](),
                    stored_range = range[duplicate]();
                    stored_range.moveToElementText(this[0]);
                    stored_range.setEndPoint('EndToEnd', range);
                    var s = stored_range.text[len] - range.text[len],
                    e = s + range.text[len]
                }
            } else {
				var s=t.selectionStart,
					e=t.selectionEnd;
			}
			var te=t.value.substring(s,e);
			return {start:s,end:e,text:te,replace:function(st){
				return t.value.substring(0,s)+st+t.value.substring(e,t.value[len])
			}}
		}
	}
})(jQuery,"length","createRange","duplicate");


jQuery.fn.ForceNumericOnly =
function () {
    return this.each(function () {
        $(this).keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;
            // allow backspace, tab, delete, arrows, numbers and keypad numbers ONLY
            return (
                key == 8 ||
                key == 9 ||
                key == 46 ||
                (key >= 37 && key <= 40) ||
                (key >= 48 && key <= 57) ||
                (key >= 96 && key <= 105));
        });
    });
};


$(document).ready(function () {

    SetAll();
    $('img').hover(function () {
        var imgSrc = $(this).attr('data-src');
        var html = '<img src="' + imgSrc + '?height=300&width=450&mode=cropandscale"/>';
        $('#item').empty();
        $('#item').append(html);
    },
    function () {
        $('#item').empty();
    })


    $("img").easyTooltip({
        useElement: "item"
    });
});



function SetAll() {

    $('.detailtbl input[type="text"]').each(function () {
        UpdatePrice(this);
        if (!$($($(this).parents('tr')[0]).find('input[type="checkbox"]')).attr('checked'))
            $(this).attr('disabled', 'disabled');
    });
    
    $('.dltabs li').click(function () {
        SetTabular($(this).index());
    })

    $('.detailtbl input[type="checkbox"]').click(function () {
        ToggleText(this);
        if ($(this).attr('checked')) {
            UpdatePrice($($($(this).parents('tr')[0]).find('input[type="text"]')));
        }
    });

    $('.detailtbl input[type="text"]').keyup(function () {
        UpdatePrice(this);
    });

    $('.detailtbl input[type="text"]').ForceNumericOnly();
  
}


function ToggleText(obj) {
    var textBox = $($($(obj).parents('tr')[0]).find('input[type="text"]'));
    var labelForPrice = $($($(obj).parents('tr')[0]).find('span[id*="lbl"]'));

    if ($(obj).attr('checked') == 'checked') {
        textBox.removeAttr('disabled');
    }
    else {
        textBox.attr('disabled', 'disabled');
        labelForPrice.text('0');
    }
}

function UpdatePrice(obj) {
    var quantity = parseInt($(obj).val().replace(',', ''));
    var fee = parseInt($($($(obj).parents('tr')[0]).find('td')[2]).text().replace(',', '').replace(',', '').replace(',', '').replace(',', ''));
    var labelForPrice = $($($(obj).parents('tr')[0]).find('span[id*="lbl"]'));

    if ($(obj).val() == "")
        labelForPrice.text('0');
    else {
        var totalprice = (quantity * fee).toString();
        labelForPrice.text(totalprice.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
    }
}

function SetTabular(obj) {

    $('.tabs > table').hide();
    $('.tabs > table').each(function(){

        $(this).removeClass('selectTab');
        if($(this).index()==obj)
        {
        
            $(this).addClass('selectTab');
            $(this).fadeIn();
        }
    });


    $('.dltabs li').each(function(){

        $(this).removeClass('selectli');
        if($(this).index()==obj)
        {
             $(this).addClass('selectli');
        }
    });
}


