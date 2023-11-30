bl.rooms_types = {}

bl.rooms_types.load = async () => {

    var url = '/api/objects_rooms_types/list_room_types';
    var res = await ajax.get_json(url);

    localStorage.setItem("room_types", JSON.stringify(res));

}

bl.rooms_types.list = () => {

    var room_types = localStorage.getItem('room_types');
    return JSON.parse(room_types);

}