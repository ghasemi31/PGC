// JavaScript Document	

$(document).ready(function(){

    /////////////////////////////////////////////////////////////// menu style changing
    $(".mnu").hover(
      function () {
          var id = $(this).attr("src");
          imgSrc = id.replace("Other.", "OtherOver.");
          $(this).attr("src", imgSrc);

      },
      function () {
          var id = $(this).attr("src");
          imgSrc = id.replace("Over", "");
          $(this).attr("src", imgSrc);
      }

    );

});//end doc ready