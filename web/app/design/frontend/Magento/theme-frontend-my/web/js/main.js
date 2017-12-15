define([
  "jquery"
], 
function($) {
  "use strict";


    ga('require', 'ec');
    var st=new Date();
    var end=new Date();


$("button,input,img").on("mouseenter", function(event) {
st=new Date().getTime();

});

$("button,input,img").on("mouseleave", function(event) {
end=new Date().getTime();
var text="searchString";

	if(event.target.name!=undefined)
		text=event.target.name;
	else
		text=event.target.title;
	
	if(text=="q")
		text="searchString";


    	//ga('send', 'event', 'UserTiming', text+" in "+(end-st)/1000+" sec");

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-109686091-1']);
  _gaq.push(['_trackPageview']);
	ga('send', 'timing', 'Action', text, end-st);
_gaq.push(['_trackTiming', 'Action', text, end-st]);


});


$(".magestore-bannerslider").find("img").each(function (i) {
    $(this).attr("alt", "alt"+i);
});

$(".magestore-bannerslider").find("img").on("click", function (event) {
  //  event.preventDefault();
    var text = $(this).attr("alt");
    if (text === "alt0") {
        ga('send', 'event', 'banner1', 'banners');
    } else if (text === "alt1") {
        ga('send', 'event', 'banner2', 'banners');

    }

})

});


