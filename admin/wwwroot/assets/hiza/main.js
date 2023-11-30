// Label main 
var main = {};

main.image_lazy_loading_src = 'https://zaggypictures.s3.eu-central-1.amazonaws.com/no_picture/loading.gif';

// main load function
window.addEventListener("load", function() {
    
});

main.load = function() {

    // Initialize templates
    hiza.engine.init();

    // tool tips
    // $('[data-toggle="tooltip"]').tooltip();

}

// Change title
main.title = function(title) {
    document.title = title;
}