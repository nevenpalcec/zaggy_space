bl.workers = {};

// variables
bl.workers.current_worker = {};

// test
bl.workers.test = () => {

}

// login workier
bl.workers.worker_login = async (guid) => {

    ui.content_loading('login_content_loading');

    var url = '';

    // By default do not login
    var response = 'wrong login data';

    // Try login by guid
    if (guid) {

        // If user logged in already, do not do it
        if (guid == bl.workers.current_worker['guid']) {
            //return; --> if this is uncommented, the button for refreshing local storage wont work
        }

        // If logged in by different user, first clear data
        if (guid !== bl.workers.current_worker['guid']) {
            localStorage.clear();
        }

        url = '/api/workers/check/?guid=' + guid;
        
        var response = await ajax.get_json(url);
    }

    // Try login by username and password
    if (response == 'wrong login data' && login_username && login_password) {

        var u = login_username.value;
        var p = login_password.value;
        url = '/api/workers/check/?username=' + encodeURIComponent(u) + '&password=' + encodeURIComponent(p);
        var response = await ajax.get_json(url);
    }

    if (response != 'wrong login data')
    {

        bl.workers.current_worker = response;

        // store worker
        localStorage.setItem("worker", JSON.stringify(response));

        ajax.headers = {
            'Content-Type': 'application/json',
            'X-Custom-Header': 'Thank you, next.',
            'zaggy_user_key': bl.workers.current_worker.user_guid,
            'zaggy_worker_key': bl.workers.current_worker.guid,
            'zaggy_user_id' : bl.workers.current_worker.user_id,
            'session_id': bl.workers.current_worker.session_id
        };

        // load int settings
        await bl.registars.load_int();

        // load int settings
        await bl.rents_local.load_int();

        //await bl.guests.load();

        //await bl.invoices.list_by_user();

        await bl.item.list();

        await bl.cancellation_policies.load();

        await bl.objects_b2b.load();

        await bl.rents.list_on_start();

        await bl.workers.load_all_workers();

        ui.modal_load();

        //await bl.users.load_int();

        ui.content_loading_hide('login_content_loading');

        main_menu_left.show();
        
        routing.clear_url();

        window.location.href = '#home';

        location.reload();
    }

    else {
        ui.content_loading_hide('login_content_loading');

        if (page.get_current_url() == 'app.syncbeds.com') {
            sign_in_username_message.classList.remove('hidden');
            sign_in_password_message.classList.remove('hidden');
        } else {
            alert('Wrong username or password');
        }
        login_username.value = '';
        login_password.value = '';
    }    

}

// get worker
bl.workers.get = async () => {
 
    var worker_id = bl.workers.current_worker_id();
    var url = '/api/workers/get/' + worker_id;

    let data = await ajax.get_json(url);    
    localStorage.setItem("worker", JSON.stringify(data));  
}

// get notification
bl.workers.get_notifications = async () => {

    var user_id = bl.workers.current_user_id();

    var url = `/api/workers/get_notifications/?user_id=${user_id}`;
    var data = await ajax.get_json(url);

    return data;
}

// get current worker
bl.workers.get_current_worker = async () => {

    var w = JSON.parse(localStorage.getItem("worker"));
    bl.workers.current_worker = w;
    return w;
}

// log out from application
bl.workers.worker_logout = () => {

    // hide menu
    main_menu_left.hide();

    // clear db
    localStorage.setItem("worker", null);
    localStorage.setItem("objects", null);
    localStorage.setItem("rents", null);

    //clear all from local storage
    localStorage.clear();

    // redirect to login
    if (page.get_current_url() == 'app.syncbeds.com') {
        location.href = '#sign_in';
    }
    else if (page.get_current_url() == '1klik.zaggys.com') {
        location.href = '#login_1klik';
    }
    else if (page.get_current_url() == 'a1.zaggys.com') {
        location.href = '#login_a1';
    }
    else if (page.get_current_url() == 'ht.zaggys.com') {
        location.href = '#login_ht';
    }
    else if (page.get_current_url().includes("staypicker")) {
        location.href = '#sign_in_spa';
    }
    else {
        //location.href = '#login'; -> old, now uses syncbeds sign_in
        location.href = '#sign_in';
    }

}

bl.workers.current_worker_get = () => {
    return JSON.parse(localStorage.getItem("worker"));
}

// get current token
bl.workers.current_token = () => {
    var w = bl.workers.current_worker_get();
    return !w == true || !w["token"] == true ? '-1' : w["token"];
}

// get current guid
bl.workers.current_guid = () => {
    var w = bl.workers.current_worker_get();
    return !w == true || !w["guid"] == true ? '-1' : w["guid"];
}

// get current user_id
bl.workers.current_user_id = () => {
 
    var w = JSON.parse(localStorage.getItem("worker"));
    return !w == true || !w["user_id"] == true ? '-1' : w["user_id"];
}

// get current worker
bl.workers.current_user_guid = () => {

    var w = JSON.parse(localStorage.getItem("worker"));
    return !w == true || !w["user_guid"] == true ? '-1' : w["user_guid"];
}

// get user's reseller_worker_id
bl.workers.reseller_worker_id = () => {
    var w = JSON.parse(localStorage.getItem("worker"));
    return !w == true || !w["reseller_worker_id"] == true ? '-1' : w["reseller_worker_id"];
}

// get user's zaggy_company_id
bl.workers.current_user_zaggy_company_id = () => {
    var w = JSON.parse(localStorage.getItem("worker"));
    return !w == true || !w["zaggy_company_id"] == true ? '-1' : w["zaggy_company_id"];
}

// get current worker
bl.workers.current_worker_id = () => {
    var w = JSON.parse(localStorage.getItem("worker"));
    return w == false || w["id"] == false ? '-1' : w["id"];
}

// get current worker name
bl.workers.current_worker_name = () => {
    var w = JSON.parse(localStorage.getItem("worker"));
    return w == false || w["name"] == false ? '-1' : w["name"];
}

// get current worker
bl.workers.current_worker_guid = () => {
    var w = JSON.parse(localStorage.getItem("worker"));
    return !w == true || !w["guid"] == true ? '-1' : w["guid"];
}
 
// get worker background picture
bl.workers.picture_background = () => {

    var w = JSON.parse(localStorage.getItem("worker"));
    var img = !w == true ? 'https://zaggypictures.s3.eu-central-1.amazonaws.com/background/4.jpg' : w['picture_background'];
    return !img == false ? img : 'https://zaggypictures.s3.eu-central-1.amazonaws.com/background/4.jpg';
    
}

// get or set current rent_id
bl.workers.current_rent_id = (rent_id) => {

    if (!rent_id == true) {
        var w = JSON.parse(localStorage.getItem('worker'));
        return !w == true || !w['rent_id'] == true ? '-1' : w['rent_id'];
    }

    else {

        var w = JSON.parse(localStorage.getItem('worker'));
        w['rent_id'] = rent_id;
        var json = JSON.stringify(w);
        localStorage.setItem('worker', json);

    }

   
}

bl.workers.load_all_workers = async () => {

    var url = '/api/workers/list/' + bl.workers.current_user_id();
    var workers = await ajax.get(url);

    localStorage.setItem("workers", workers);

}

bl.workers.list_all = async () => {

    var workers = localStorage.getItem('workers');
    var w = JSON.parse(workers);

    return w;

}
