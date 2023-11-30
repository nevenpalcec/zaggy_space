var routing = {};

//$(window).on('hashchange', function (e) {

//    let hash = location.hash.slice(1);
//    routing.load_content(hash);
//});

routing.hash_logic = async () => {
    
    let hash = location.hash.slice(1).split('?')[0];
    await routing.load_content(hash);
}

routing.reload = routing.hash_logic;

window.addEventListener('hashchange', routing.hash_logic, false);

routing.is_development = () => window.location.host.includes('localhost');

routing.load_content = async function (hash) {

    // disable casing of jquery
    $.ajaxSetup({ cache: false });

    var u = localStorage.getItem("worker");
    var url = location.href.split('/')[2];

    if (!u == true && (hash == '' || hash == 'sign_in') && url == '1klik.zaggys.com') {
        hash = 'login_1klik';
    }
    else if (!u == true && (hash == '' || hash == 'sign_in') && url == 'livmark.zaggys.com') {
        hash = 'login_livmark';
    }
    else if (!u == true && (hash == '' || hash == 'sign_in') && url == 'app.sync-hotel.com') {
        hash = 'login_sync_hotel';
    }
    else if (!u == true && (hash == '' || hash == 'sign_in') && url == 'totaltv.zaggys.com') {
        hash = 'login_totaltv';
    }
    else if (!u == true && (hash == '' || hash == 'sign_in') && url == 'a1.zaggys.com') {
        hash = 'login_a1';
    }
    else if (!u == true && (hash == '' || hash == 'sign_in') && url == 'ht.zaggys.com') {
        hash = 'login_ht';
    }
    else if (!u == true && (hash == '' || hash == 'sign_in') && url == 'apartmanija.zaggys.com') {
        hash = 'login_apartmanija';
    }
    else if (!u == true && (hash == '' || hash == 'sign_in') && url == 'app.syncbeds.com') {
        hash = 'sign_in';
    }
    else if (!u == true && (hash == '' || hash == 'sign_in')) {
        //hash = 'login'; -> old, "light" login page
        hash = 'sign_in'; //redirects to the same syncbeds login page
    }
    else if (!u == true && hash == 'sign_up') {

        //hash = 'login';
        hash = 'sign_up';
    }
    else if (hash == '') {
        hash = 'home';
    }
 
    // scroll to top nicely
    $('html, body').animate({ scrollTop: 0 }, '600', 'swing');

    let args = '';

    if (hash.includes('?')) {
        args = hash.split('?')[1];
        hash = hash.split('?')[0];
    }

    var page = '/pages/' + hash + '.html?' + args;

    // Do not use cached
    //if (window.location.href.includes('localhost')) {
    //    page += '&v=' + Math.floor(Math.random() * 100000);
    //}
    page += '&v=' + Math.floor(Math.random() * 100000);

    // Clear console if this is production
    if (location.hostname !== 'localhost') {
        console.clear();
    }

    let start = new Date();

    // get page
    var html = await ajax.get(page);

    await $('.main_page').html(html, function () { main.load();  }).promise();

    //await $('.main_page').load(page, function() {
    //    main.load();
    //}).promise();

    // translate page
    tran_js.translate_page();

    let diff_ms = new Date() - start;
    console.log('Loaded ' + page.replace('/pages/', '') + ' in ' + diff_ms + ' ms.');

}

routing.default = async function (worker_guid)
{

    if (worker_guid && worker_guid.length > 0) {

        // Login
        await bl.workers.worker_login(worker_guid);
        routing.clear_url();
    }

    let hash = location.hash.slice(1);
    
    routing.load_content(hash);

}

routing.clear_url = function () {
    window.history.replaceState({}, document.title, '/');
}
