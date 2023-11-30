bl.payment_methods = {};

bl.payment_methods.load = async () => {
    var user_id = bl.workers.current_user_id();
    var url = '/api/registars/payment_methods/' + user_id;

    //$.get(url).done((data) => {


    //    var registars = bl.registars.get();
    //    registars.payment_methods = data;
    //    localStorage.setItem('registars', JSON.stringify(registars));

    //});

    var registars = bl.registars.get();
    let data = await ajax.get_json(url);
    registars.payment_methods = data;
    //await js.http.async.get(url).then(data_text => JSON.parse(data_text)).then(data => {
    //})
    localStorage.setItem("registars", JSON.stringify(registars));

}

bl.payment_methods.list = () => {

    var regsistars = localStorage.getItem('registars');
    if (regsistars == null) {
        return '';
    }
    return JSON.parse(regsistars).payment_methods;

}