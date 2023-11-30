var ajax = {};

ajax.debug_mode = false;

ajax.post = async (url = '', data = {}) => {

    const p_start = performance.now();

    var guid = localStorage.getItem('guid');
    var reseller_worker_perm = localStorage.getItem('reseller_worker_perm');
    var reseller_worker_type_id = localStorage.getItem('reseller_worker_type_id');
    var name = localStorage.getItem('name');
    var reseller_worker_id = localStorage.getItem('reseller_worker_id');
    var reseller_worker_guid = localStorage.getItem('reseller_worker_guid');

    var settings = {
        method: 'POST',
        mode: 'same-origin',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
            'reseller_worker_guid': reseller_worker_guid,
            'name': name,
            'reseller_worker_id': reseller_worker_id,
            'reseller_worker_type_id': reseller_worker_type_id,
            'reseller_worker_perm': reseller_worker_perm,
            'guid': guid
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    }

    try {

        const response = await fetch(url, settings);

        const p_end = performance.now();
        const p_was_runing = Math.round(p_end - p_start);

        if (location.hostname === "localhost" || location.hostname === "127.0.0.1" || ajax.debug_mode == true) {
            //console.log(settings.method, url, p_was_runing + ' ms');
        }

        var text = await response.text();
        return text;

    }
    catch (error) {
        debugger;
        console.error(error);
        console.log('Yippee-ki-yay, motherfucker!');

        // Logout
        bl.workers.worker_logout();

        // Return to url
        location.href = bl.varables.return_url;
        return;
    }

}

ajax.post_json = async (url = '', data = {}) => {

    const p_start = performance.now();

    var guid = localStorage.getItem('guid');
    var reseller_worker_perm = localStorage.getItem('reseller_worker_perm');
    var reseller_worker_type_id = localStorage.getItem('reseller_worker_type_id');
    var name = localStorage.getItem('name');
    var reseller_worker_id = localStorage.getItem('reseller_worker_id');
    var reseller_worker_guid = localStorage.getItem('reseller_worker_guid');

    var settings = {
        method: 'POST',
        mode: 'same-origin',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
            'reseller_worker_guid': reseller_worker_guid,
            'name': name,
            'reseller_worker_id': reseller_worker_id,
            'reseller_worker_type_id': reseller_worker_type_id,
            'reseller_worker_perm': reseller_worker_perm,
            'guid': guid
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    }

    try {

        debugger;

        const response = await fetch(url, settings);

        const p_end = performance.now();
        const p_was_runing = Math.round(p_end - p_start);

        if (location.hostname === "localhost" || location.hostname === "127.0.0.1" || ajax.debug_mode == true) {
            //console.log(settings.method, url, p_was_runing + ' ms');
        }

        var text = await response.text();
        var json = JSON.parse(text);
        return json;

    }
    catch (error)
    {
        debugger;
        console.error(error);
        console.log('Yippee-ki-yay, motherfucker!');

        // Logout
        bl.workers.worker_logout();

        // Return to url
        location.href = bl.varables.return_url;
        return;
    }

}

ajax.get_json = async (url = '') => {

    const p_start = performance.now();

    var guid = localStorage.getItem('guid');
    var reseller_worker_perm = localStorage.getItem('reseller_worker_perm');
    var reseller_worker_type_id = localStorage.getItem('reseller_worker_type_id');
    var name = localStorage.getItem('name');
    var reseller_worker_id = localStorage.getItem('reseller_worker_id');
    var reseller_worker_guid = localStorage.getItem('reseller_worker_guid');

    var settings = {
        method: 'GET',
        mode: 'same-origin',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: {
            'Content-Type': 'application/json',
            'reseller_worker_guid': reseller_worker_guid,
            'name': name,
            'reseller_worker_id': reseller_worker_id,
            'reseller_worker_type_id': reseller_worker_type_id,
            'reseller_worker_perm': reseller_worker_perm,
            'guid': guid
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer'
    }

    try {

        // debugger;

        var a = 'asdasdsa';
        const response = await fetch(url, settings);

        const p_end = performance.now();
        const p_was_runing = Math.round(p_end - p_start);

        if (location.hostname === "localhost" || location.hostname === "127.0.0.1" || ajax.debug_mode == true) {
            //console.log(settings.method, url, p_was_runing + ' ms');
        }

        var text = await response.text();
        var json = JSON.parse(text);
        return json;

    }
    catch (ex) {

        // Clear all and logout
        console.log('Hasta la vista, baby...');
 
        return;
    }

}