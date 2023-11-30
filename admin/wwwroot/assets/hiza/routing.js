var routing = {};

routing.hash_logic = async () => {
    
    let hash = location.hash.slice(1).split('?')[0];
    await routing.load_content(hash);
}

routing.reload = routing.hash_logic;

window.addEventListener('hashchange', routing.hash_logic, false);

routing.is_development = () => window.location.host.includes('localhost');

routing.load_content = async function () {

    var hash = await window.location.hash;
    hash = hash.replace('#', '');

    //var u = localStorage.getItem("worker");
    //var url = location.href.split('/')[2];

    //if (!u == true && hash == '' && url == 'app.syncbeds.com') {
    //    hash = 'sign_in';
    //}

    //if (!u == true && hash == '') {
    //    hash = 'login';
    //}

    //if (!u == true && hash == 'sign_up') {

    //    //hash = 'login';
    //    hash = 'sign_up';
    //}

    if (hash == '') {
        hash = 'home';
    }

    //if (hash == '') {
    //    hash = 'login';

    //}

    $('html, body').animate({ scrollTop: 0 }, '600', 'swing');


    let args = '';

    if (hash.includes('?')) {
        args = hash.split('?')[1];
        hash = hash.split('?')[0];
    }

    var page = '/pages/' + hash + '.html?' + args;

    // Do not use cached
    if (window.location.href.includes('localhost')) {
        page += '&v=' + Math.floor(Math.random() * 100000);
    }

    // clear console 
    // console.clear();
    let start = new Date();

    // load page
    await $('#div_main_content').load(page, function() {
        main.load();
    }).promise();

    // translate page
    // tran_js.translate_page();

    let diff_ms = new Date() - start;
    console.log('Loaded ' + page.replace('/pages/', '') + ' in ' + diff_ms + ' ms.');

}

routing.default = async function (worker_guid) {

    if (worker_guid && worker_guid.length > 0) {
        await bl.workers.worker_login(worker_guid);
        routing.clear_url();
    }

    let hash = location.hash.slice(1);
    
    routing.load_content(hash);

}

routing.clear_url = function () {
    window.history.replaceState({}, document.title, '/');
}
