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



$(document).mousemove(function () {

    if (!$('.dltabs').attr('data-isbind')) {

        $('.dltabs').attr('data-isbind', 'true');

        $('.dltabs li').click(function () {
            SetTabular($(this).index());
        })

        SetTextBoxDisabling();

         $('.tabBody input[type="text"]').ForceNumericOnly();

        $('.tabBody input:checkbox').click(function(){
            ToggleTextBox(this);
        });

         $('.inputAllBox').keyup(function () {
                $($($(this).parents('table')[0]).find('input[id*="txtNum"]')).val($(this).val());
            });

        $('.checkAllBox input').click(function(){ 
            
            var IsSelected= ($(this).attr('checked'))?true:false;
            
            $($(this).parents('table')[0]).find('input[id*="chk-"]').each(function(){                
                if (!$(this).attr('checked') && IsSelected){
                    $(this).attr('checked','checked');
                    $($($(this).parents('tr')[0]).find('input[type="text"]')).removeAttr('disabled');
                }
                else if ($(this).attr('checked') && !IsSelected){
                    $(this).removeAttr('checked');
                    $($($(this).parents('tr')[0]).find('input[type="text"]')).attr('disabled','disabled');
                }
            });
        });

    }
});

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

function ToggleTextBox(obj)
{
    if ($(obj).attr('checked'))
        $($($(obj).parents('tr')[0]).find('input[type="text"]')).removeAttr('disabled');
    else
        $($($(obj).parents('tr')[0]).find('input[type="text"]')).attr('disabled','disabled');
}

function SetTextBoxDisabling()
{
    //$('.tabBody input[type="text"]').attr('disabled','disabled')
    $('.tabBody input[type="text"]').each(function(){
        if (!$($($(this).parents('tr')[0]).find('input[type="checkbox"]')).attr('checked'))
            $(this).attr('disabled','disabled');
    });
}


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