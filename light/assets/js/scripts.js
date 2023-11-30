// Global open mobile navigation
//function nav_mobile() {
//    var btn = document.querySelector(".view_mobile_btn");
//    btn.addEventListener("click", function () {
//        document.querySelector(".view_nav_mobile").classList.remove("hidden");
//        setTimeout(function() {
//            document.querySelector(".view_nav_mobile").classList.remove('visuallyhidden');
//        },20)
//    });
    
//    var btn_close = document.querySelector(".mobile_close");
//    btn_close.addEventListener("click", function () {
//        document.querySelector(".view_nav_mobile").classList.add("visuallyhidden");
//        document.querySelector(".view_nav_mobile").addEventListener("transitionend", function(){
//            document.querySelector(".view_nav_mobile").classList.add("hidden");
//        }, {
//            capture: false, 
//            once: true,
//            passive: false
//        });
//    });
    
//    window.addEventListener("resize", function () {
//        debugger;
//        var w = window.innerWidth;
//        if (w > 767) {
//            document.querySelector(".view_nav_mobile").classList.add("visuallyhidden");
//            document.querySelector(".view_nav_mobile").classList.add("hidden");
//        }
//    });
//}
//nav_mobile();

// Load bg image
function load_bg_img() {
    //window.addEventListener("load", function() {
    //    var img = localStorage.getItem("bg_img");
    //    if(img !== null) {
    //        document.querySelector(".bg_screen_img > img").src = img;
    //        document.querySelector(".bg_screen_img_blur > img").src = img;    
    //    }
    //});
}

load_bg_img();

// Load color card
function load_color_card() {
    var check = localStorage.getItem("color_card");
    var cards = document.querySelectorAll(".color_card");
    if(check == "black") {
        cards.forEach(element => {
            element.classList.remove("white");
        });
    } else {
        cards.forEach(element => {
            element.classList.add("white");
        });
    }
}
load_color_card();

// Clean all notification
clean_all_notification = function(event) {
    var notification_list = document.querySelector(".notification_list");
    var notification_day = document.querySelector(".notification_day");
    notification_list.remove();
    notification_day.remove();
}

// Tooltip
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

var weather_id = document.getElementById("weatherwidget-io");
fetch('https://ipapi.co/json/')
    .then(function(response) {
      return response.json();
    })
    .then(function(data) {
      console.log(data);
      console.log(data.city);
    });

// Accordion
$('.collapse').collapse();

