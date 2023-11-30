const ajax = {};

ajax.debug_mode = false;

ajax.headers = {
    'user_id': '',
    'guid': '',
    'worker_id': '',
    'id_hash': '',
    'session_key': ''
};

if (location.hostname == 'localhost') {
    ajax.debug_mode = true;
}

ajax.log = function () {

    if (ajax.debug_mode) {
        let print_out = Array.from(arguments).join(' ');
        console.error('%c' + print_out, 'background: yellow; color: red; font-size: 1.5rem;');
    }
}

ajax.post = async (url = '', data = {}) => {

    const p_start = performance.now();

    ajax.headers = {
        'Content-Type': 'application/json',
        'X-Custom-Header': 'This is my town',
        'zaggy_user_key': bl.workers.current_user_guid(),
        'zaggy_user_id': bl.workers.current_user_id(),
        'zaggy_worker_key': bl.workers.current_worker_guid(),
        'session_id': bl.workers.current_worker.session_id
    };

    var settings = {
        method: 'POST',
        mode: 'same-origin',
        cache: 'no-cache',
        credentials: ajax.headers,
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    }

    try {

        const response = await fetch(url, settings);

        const p_end = performance.now();
        const p_was_runing = Math.round(p_end - p_start);

        if (location.hostname === "localhost" || location.hostname === "127.0.0.1" || ajax.debug_mode == true) {
            console.log(settings.method, url, p_was_runing + ' ms', 'post');
        }

        var text = await response.text();

        return text;

    }
    catch (ex) {

        ajax.log(ex);

        console.log('Yippee-ki-yay, motherfucker!');
        //location.href = bl.varables.return_url;
        return;
    }

}

ajax.post_json = async (url = '', data = {}) => {
    const p_start = performance.now();

    ajax.headers = {
        'Content-Type': 'application/json',
        'X-Custom-Header': 'Too hot for you to handle',
        'zaggy_user_key': bl.workers.current_user_guid(),
        'zaggy_user_id': bl.workers.current_user_id(),
        'zaggy_worker_key': bl.workers.current_worker_guid(),
        'session_id': bl.workers.current_worker.session_id
    };

    var settings = {
        method: 'POST',
        mode: 'same-origin',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: ajax.headers,
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: JSON.stringify(data)
    }

    try {

        const response = await fetch(url, settings);

        const p_end = performance.now();
        const p_was_runing = Math.round(p_end - p_start);

        if (location.hostname === "localhost" || location.hostname === "127.0.0.1" || ajax.debug_mode == true) {
            console.log(settings.method, url, p_was_runing + ' ms', 'post_json');
        }

        var text = await response.text();
        var json = JSON.parse(text);
        return json;

    }
    catch (ex) {

        ajax.log(ex, 'URL=' + url, 'DATA=' + JSON.stringify(data));

        console.log('Yippee-ki-yay, motherfucker!');
        //location.href = bl.varables.return_url;
        return;
    }

}

ajax.get_json = async (url = '') => {

    const p_start = performance.now();

    ajax.headers = {
        'Content-Type': 'application/json',
        'X-Custom-Header': 'Use my attention wisely',
        'zaggy_user_key': bl.workers.current_user_guid(),
        'zaggy_user_id': bl.workers.current_user_id(),
        'zaggy_worker_key': bl.workers.current_worker_guid(),
        'session_id': bl.workers.current_worker.session_id
    };

    var settings = {
        method: 'GET',
        mode: 'same-origin',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: ajax.headers,
        redirect: 'follow',
        referrerPolicy: 'no-referrer'
    }

    try {

        const response = await fetch(url, settings);

        const p_end = performance.now();
        const p_was_runing = Math.round(p_end - p_start);

        if (location.hostname === "localhost" || location.hostname === "127.0.0.1" || ajax.debug_mode == true) {
            console.log(settings.method, url, p_was_runing + ' ms', 'get_json');
        }

        var text = await response.text();
        var json = JSON.parse(text);
        return json;

    }
    catch (ex) {

        ajax.log(ex);

        console.log('Hasta la vista, baby...');
        //location.href = bl.varables.return_url;
        return;
    }

}

ajax.get = async (url = '') => {

    const p_start = performance.now();

    ajax.headers = {
        'Content-Type': 'application/json',
        'X-Custom-Header': 'Your worst nightmare',
        'zaggy_user_key': bl.workers.current_user_guid(),
        'zaggy_user_id': bl.workers.current_user_id(),
        'zaggy_worker_key': bl.workers.current_worker_guid(),
        'session_id': bl.workers.current_worker.session_id
    };

    var settings = {
        method: 'GET',
        mode: 'same-origin',
        cache: 'no-cache',
        credentials: 'same-origin',
        headers: ajax.headers,
        redirect: 'follow',
        referrerPolicy: 'no-referrer'
    }

    try {

        const response = await fetch(url,settings);

        const p_end = performance.now();
        const p_was_runing = Math.round(p_end - p_start);

        if (location.hostname === "localhost" || location.hostname === "127.0.0.1" || ajax.debug_mode == true) {
            console.log(settings.method, url, p_was_runing + ' ms', 'get');
        }

        var text = await response.text();
        //var json = JSON.parse(text);
        return text;

    }
    catch (ex) {

        ajax.log(ex);

        console.log('Hasta la vista, baby...');
        //location.href = bl.varables.return_url;
        return;
    }

}
 