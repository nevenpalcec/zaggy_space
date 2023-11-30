bl.rents_sources = {};


bl.rents_sources.load = async () => {

    var user_id = bl.workers.current_user_id();
    var url = '/api/registars/rents_sources/' + user_id;
 
    var registars = bl.registars.get();
    registars.rent_sources = await ajax.get_json(url);;
    localStorage.setItem("registars", JSON.stringify(registars));

}

bl.rents_sources.list = () => {
    
    var regsistars = localStorage.getItem('registars');
    if (regsistars == null) {
        return '';
    }
    return JSON.parse(regsistars).rent_sources;

}