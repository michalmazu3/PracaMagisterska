$(window).load(function(){

    $("#banner").easybg({

        images: ["~/image/banner.jpg", "~/image/banner1.jpg"],

    interval: 10000,

    speed : 1000, // 1 minute

    ignoreError : false,

    changeMode : "normal", // normal or random
 
    initIndex : 0,
    cloneClassId : null,

    cloneClassName : "easybgClone",
  
    debug : false

});

});
