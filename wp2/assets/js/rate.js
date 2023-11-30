/* 
 *  js class for making rates stars
 *  Design by: Adi
 *  When: 26.05.2020. g.
 *  Company: zaggy
 * 
 */


var rate = {};

rate.set = function (id) {

    console.log("Clicked");

    //var r = document.getElementById(id);
    //return '5';

    document.getElementById(id).addEventListener('click', function (e) {
        let action = 'add';
        for (const span of this.children) {
            span.classList[action]('active');
            if (span === e.target) action = 'remove';
        }
    });

}

rate.get = function (id) {

}

 