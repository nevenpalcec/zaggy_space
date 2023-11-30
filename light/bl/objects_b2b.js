bl.objects_b2b = {};

bl.objects_b2b.load = async () => {

    var id = bl.workers.current_user_id();

    var url = 'api/objects_b2b/list/' + id; // it must be object_id, doesnt work

    var objects_b2b = await ajax.get_json(url);
    localStorage.setItem('objects_b2b', JSON.stringify(objects_b2b));
}

bl.objects_b2b.list = async () => {

    var objects_b2b = localStorage.getItem('objects_b2b');
    return JSON.parse(objects_b2b);
}
