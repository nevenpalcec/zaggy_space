bl.guests = {};

bl.guests.load = async () => {
    
    var user_id = bl.workers.current_user_id();
    var url = '/api/guests/get_guest_by_user_id/' + user_id;

    var guests = await ajax.get_json(url);
    localStorage.setItem('guests', JSON.stringify(guests));

    //$.get(url).done((data) => {

    //    var guests = data;
    //    localStorage.setItem('guests', JSON.stringify(guests));

    //});    
}

bl.guests.add_guest = async (string_json) => {

    var user_id = bl.workers.current_user_id();
    var url = '/api/guests/add_guest/' + string_json;

    let data = await ajax.get(url);


}

bl.guests.list = () => {

    var guests = localStorage.getItem('guests');
    return JSON.parse(guests);

}