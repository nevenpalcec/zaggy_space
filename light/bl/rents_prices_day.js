bl.rents_prices_day = {};

//bl.rents_prices_day.list_day = async (rent_id) => {

//    var url = '/api/rents_prices_days/list_day/' + rent_id;

//    return await ajax.post(url);

//}

bl.rents_prices_day.save_day = async (rent_id, day, price) => {

    var url = 'api/rents_prices_days/save_day' + rent_id;

    return await ajax.get(url);
}

bl.rents_prices_day.set_total_price = async (rent_id) => {

    var url = '/api/rents_prices_days/set_total_price/' + rent_id;

    return await ajax.get(url);

}


bl.rents_prices_day.load = async (rent_id) => {
  
    var url = '/api/registars/rents_prices_day/' + rent_id;


    var registars = bl.registars.get();
    registars.rents_prices_day = await ajax.get_json(url);
    localStorage.setItem('registars', JSON.stringify(registars));


}

bl.rents_prices_day.list = () => {

    var regsistars = localStorage.getItem('registars');
    return JSON.parse(regsistars).rents_prices_day;;

}
