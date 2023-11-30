// Tran.js
// js script for direct translations of a web page
// deveoped by: zaggy
// https://my-rens.com
// Author: Neven Palcec

// usage
// change stirng with  <span class="tran_js" data-key="Departures"> Departures </span>
// data-key: primary key what will be translated

// for signle 
// tran_js.get('key to translate')


var tran_js = {};

tran_js.keys = '';
tran_js.lng = 'en';
tran_js.class_name = 'tran_js';

tran_js.load = async () => {
    tran_js.keys = await ajax.get_json("/assets/translate/keys.json");
}


tran_js.get = async (key, num = null) => {
    try {
        var rows = tran_js.keys['translations'];

        if (num == null) {
            let r = await rows.find(el => el.key === key);
            return r[tran_js.lng];
        }
        else {

            let r = await rows.find(el => el.key === key);
            var w = r.split('|');

            if (num == 1) {
                return w[0];
            }
            else {
                return w[1];
            }

        }
       
      
    }
    catch {
        return key;
    }
}

tran_js.translate_page = async (lng) => {

    if (!lng == false) {
        tran_js.lng = lng;
    }

    var spans = document.getElementsByClassName(tran_js.class_name)

    for (var s of spans) {
        var key = s.dataset.key;
        s.innerHTML = await tran_js.get(key);
    }

}

// load page on load
window.addEventListener("load", function () {
    tran_js.translate_page();
});