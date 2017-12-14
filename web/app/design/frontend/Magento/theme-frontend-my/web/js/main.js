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


});


