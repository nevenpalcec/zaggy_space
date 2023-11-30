bl.objects = {};

// pull objects list
bl.objects.list_load = async () => {

    var id_list = '';
    var worker_id = bl.workers.current_worker_id();

    var url = '/api/objects/list/' + worker_id;

    // Fetch objects
    //var response = await ajax.get(url);

    // Fill with score
    //var objects = JSON.parse(response);
    var objects = await ajax.get_json(url);

    for (var ob of objects) {
        id_list = id_list + ob['id'] + ',';
    }

    id_list = id_list.substring(0, id_list.length - 1);

    //let obj_percentage = await fetch('/api/objects/list_status/', {
    //    method: "POST",

    //    // Adding body or contents to send
    //    body: id_list
    //})
    //    .then(res => res.json());

    // debugger;

    //let obj_percentage = await ajax.post('/api/objects/list_status/', id_list);


    //for (var ob of objects) {
    //    for (var p of obj_percentage) {
    //        if (ob['id'] == p['object_id']) {
    //            ob['score_percent'] = Math.round(p['score_percent']);
    //            break;
    //        }
    //    }
    //}

    localStorage.setItem("objects", JSON.stringify(objects));
}

// get list of objects
bl.objects.list_get_db = async(worker_id) => {

    var url = '/api/objects/list/' + worker_id;
    var o = await ajax.get_json(url);
    
    return o;

}

bl.objects.list_get = () => {

    var objects = localStorage.getItem('objects');
    var o = JSON.parse(objects);
    return o;
}


// get details for one property
bl.objects.object_load = async (object_id, callback) => {

    var url = '/api/objects/get_details/' + object_id;
    var o = await ajax.get_json(url);
    localStorage.setItem('object', JSON.stringify(o));

    callback();

}

// get details for one property
bl.objects.object_load = async (object_id) => {

    var url = '/api/objects/get_details/' + object_id;
    var data = await ajax.get_json(url);
    var o = JSON.stringify(data);
    localStorage.setItem('object', o);
    return data;

}

// get details for one property
bl.objects.object_get = () => {

    var object = localStorage.getItem('object');
    var o = JSON.parse(object);
    return o;

}

