bl.b2b = {}

bl.b2b.load = async () => {

    var url = '/api/b2b/list?type=other';
    var res = await ajax.get_json(url);

    localStorage.setItem("b2b", JSON.stringify(res));

}

bl.b2b.list_sort = async () => {

    var url = '/api/b2b/list_sort';
    var res = await ajax.get_json(url);

    localStorage.setItem("list_sort", JSON.stringify(res));

}