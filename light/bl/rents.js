bl.rents = {};
bl.rents.arrivals = {};
bl.rents.departures = {};
bl.rents.table = {};

bl.rents.rent_sources = '';

// get rents from db

bl.rents.list_arrivals_today = async () => {
    
    var date = new Date();
    date = js.date.toIsoFormat(date); 

    var user_id = bl.workers.current_user_id();

    var url = '/api/rents/list_arrivals/' + `?day=${date}&user_id=${user_id}`;
    let data = await ajax.get_json(url);

    bl.rents.arrivals.today = data;
    localStorage.setItem('rents', JSON.stringify(bl.rents));

    return data;

}

bl.rents.list_arrivals_tomorrow = async () => {

    var date = new Date();
    date.setDate(date.getDate() + 1);
    date = js.date.toIsoFormat(date);

    var user_id = bl.workers.current_user_id();
    var url = '/api/rents/list_arrivals/' + `?day=${date}&user_id=${user_id}`;

    let data = await ajax.get_json(url);
    bl.rents.arrivals.tomorrow = data;
    localStorage.setItem('rents', JSON.stringify(bl.rents));
   
}

bl.rents.list_departures_today = async () => {

    var date = new Date();
    date = js.date.toIsoFormat(date);

    var user_id = bl.workers.current_user_id();

    var url = '/api/rents/list_departures/' + `?day=${date}&user_id=${user_id}`;
    let data = await ajax.get_json(url);

    bl.rents.departures.today = data;
    localStorage.setItem('rents', JSON.stringify(bl.rents));

    return data;

}

bl.rents.list_departures_tomorrow = async () => {

    var date = new Date();
    date.setDate(date.getDate() + 1);
    date = js.date.toIsoFormat(date);
    var user_id = bl.workers.current_user_id();

    var url = '/api/rents/list_departures/' + `?day=${date}&user_id=${user_id}`;
    let data = await ajax.get_json(url);

    bl.rents.departures.tomorrow = data;
    localStorage.setItem('rents', JSON.stringify(bl.rents));
   
}

bl.rents.list = async () => {
    
    let active = 'Y';
    var date_from = bl.varables.get('rents_date_from');
    var days = bl.varables.get('rents_days');
    var user_id = bl.workers.current_user_id();
    var search = rents_search.value;
    var objs = rent_details_object_dropdown.dataset.id;
    //var object_id = rents_object_id.value;

    var p = {
        user_id: user_id,
        date_from: date_from,
        days: days,
        rent_soruces: bl.rents.rent_sources,
        search: search,
        active,
        objs
        /*rent_statuses: rents_statuses,*/
        //object_id
    };

    var url = '/api/rents/list/' + user_id;

    let data = await ajax.post_json(url, p);

    bl.rents.table = data;
    localStorage.setItem('rents', JSON.stringify(bl.rents));
}

//get all rents current year
bl.rents.get_all_rents_new = async () => {

    var date_from = bl.varables.get('rents_date_from');
    var date_from = moment().startOf('year').format('YYYYY-MM-DD');

    //var days = bl.varables.get('rents_days');
    var days = 365;
    var user_id = bl.workers.current_user_id();
    //console.log(days);
    //var objs = '';
    //var rents_soruces = '';
    //var rents_statuses = '';
    var search = '';

    var p = {
        user_id: user_id,
        date_from: date_from,
        days: days,
        //active: active,
        rent_soruces: '',
        /*rent_statuses: rents_statuses,*/
        /*objs: objs,*/
        search: search
    };

    var url = '/api/rents/list/' + user_id;
    var rents_list = await ajax.post_json(url, p);

    return rents_list;
}

bl.rents.get_all_rents = async () => {

    var user_id = bl.workers.current_user_id();
    var date_from = bl.varables.get('rents_date_from');
    var days = bl.varables.get('rents_days');
    var objs = '';
    var rents_soruces = '';
    var rents_statuses = '';
    var search = '';

    var p = {
        user_id: user_id,
        date_from: date_from,
        days: days,
        active: 'Y',
        rent_soruces: rents_soruces,
        rent_statuses: rents_statuses,
        objs: objs,
        search: search
    };

    var user_id = bl.workers.current_user_id();
    var url = '/api/rents/list/' + user_id;
    var rents_list = await ajax.post_json(url, p);

    return rents_list;
}

bl.rents.list_on_start = async () => {

    bl.rents.table = bl.rents.get_all_rents();
    localStorage.setItem('rents', JSON.stringify(bl.rents));
    
}

bl.rents.get = () => {

    var rents = localStorage.getItem('rents');
    return JSON.parse(rents).rents;

}

bl.rents.statistics = async (year, user_id, object_id, unit_id) => {

    var url = 'api/rents/statistics/' + `?year=${year}&user_id=${user_id}&object_id=${object_id}&unit_id=${unit_id}`

    let response = await ajax.get_json(url); 
    return response;
}

bl.rents.statistics_countries = async (year, user_id, object_id) => {

    var url = 'api/rents/statistics_by_countries/' + `?year=${year}&user_id=${user_id}&object_id=${object_id}`

    let response = await ajax.get_json(url); 
    return response;
}

bl.rents.statistics_by_channels = async (year, user_id, object_id, unit_id) => {

    var url = 'api/rents/statistics_by_channels/' + `?year=${year}&user_id=${user_id}&object_id=${object_id}&unit_id=${unit_id}`

    let response = await ajax.get_json(url);
    return response;
}

bl.rents.statistics_by_month = async (year, month, user_id, object_id, unit_id) => {

    var url = 'api/rents/statistics_by_month/' + `?year=${year}&month=${month}&user_id=${user_id}&object_id=${object_id}&unit_id=${unit_id}`

    let response = await ajax.get_json(url);
    return response;
}
