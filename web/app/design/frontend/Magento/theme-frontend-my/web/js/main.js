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
var text;
	if(event.target.title!=undefined)
		text=event.target.title;
	else
		text=event.target.name;

    	ga('send', 'event', 'UserTiming', text+" in "+(end-st)/1000+" sec");
	ga('send', 'timing', 'UserAction', 'hovering', (end-st));
});


});


