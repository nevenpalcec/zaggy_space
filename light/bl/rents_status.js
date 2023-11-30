bl.rents_status = {};

bl.rents_status.load = async () => {

    var user_id = bl.workers.current_user_id();

    var url = '/api/registars/rents_status/' + user_id;

    var registars = bl.registars.get();
    registars.rent_status = await ajax.get_json(url);;
    localStorage.setItem("registars", JSON.stringify(registars));

}

bl.rents_status.list = () => {
    var regsistars = localStorage.getItem('registars');
    if (regsistars == null) {
        return '';
    }
    return JSON.parse(regsistars).rent_status;

}