bl.countries = {};

bl.countries.load = async () => {
    
    var user_id = bl.workers.current_user_id();
    var url = '/api/registars/countries/' + user_id;

    var registars = bl.registars.get();


    registars.countries = await ajax.get_json(url);
    //await js.http.async.get(url).then(data_text => JSON.parse(data_text)).then(data => {
        
    //})
    localStorage.setItem("registars", JSON.stringify(registars));

}

bl.countries.list = () => {

    var regsistars = localStorage.getItem('registars');
    return JSON.parse(regsistars).countries;

}