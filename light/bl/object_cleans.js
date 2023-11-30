bl.object_cleans = {};

//load cleaning options
bl.object_cleans.load = async () => {
    var user_id = bl.workers.current_user_id();
    var url = '/api/users/user_object_cleans/' + user_id;

    var registars = bl.registars.get();

    registars.cleaning_options = await ajax.get_json(url);

    localStorage.setItem('registars', JSON.stringify(registars));
}

//get cleaning options
bl.object_cleans.get_data = () => {

    var regsistars = localStorage.getItem('registars');
    return JSON.parse(regsistars).cleaning_options;
}