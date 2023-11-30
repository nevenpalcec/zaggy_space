bl.currency = {};

bl.currency.load = async () => {

    var user_id = bl.workers.current_user_id();
    var url = '/api/registars/currency/';

    var registars = bl.registars.get();

    registars.currency = await ajax.get_json(url);

    localStorage.setItem("registars", JSON.stringify(registars));

}

bl.currency.list = () => {

    var regsistars = localStorage.getItem('registars');
    return JSON.parse(regsistars).currency;

}