bl.user = {};

bl.user.load = async () => {

    var user_id = bl.workers.current_user_id();
    var url = '/api/users/details/' + user_id;

    var user = await ajax.get_json(url);
    localStorage.setItem('users', JSON.stringify(user));    
}

bl.user.list = () => {
   
    var users = localStorage.getItem('users');
    return JSON.parse(users).users;
}