bl.item = {};

bl.item.list = async () => {

    var user_id = bl.workers.current_user_id();
    var url = '/api/items/list_of_items/' + user_id;

    let data = await ajax.get_json(url);
    localStorage.setItem("items", JSON.stringify(data));
    
    //await js.http.async.get(url).then(data_text => JSON.parse(data_text)).then(data => {
    //});

}

bl.item.get = () => {

    var items = localStorage.getItem('items');
    if (items == null) {
        return '';
    }
    return JSON.parse(items);

}