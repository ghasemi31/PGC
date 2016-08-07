// JavaScript Document	

$(document).ready(function(){
	
$(".mnu").hover(
      function () {
		var id = $(this).attr("src");
		imgSrc = id.replace(".","Over.");
		$(this).attr("src",imgSrc);
		
      },
      function () {		
        var id = $(this).attr("src");
		imgSrc = id.replace("Over","");
 		$(this).attr("src",imgSrc);
      } 

);
	
$(function () {
    $('#js-news').ticker({
	    titleText: 'اخبار:',
        direction: 'rtl',
		speed: 0.20, 
		displayType: 'reveal'        
   
    });
});	
	



	
});//end doc ready