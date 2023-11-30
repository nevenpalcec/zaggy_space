bl.cancellation_policies = {};

bl.cancellation_policies.load = async () => {

    var user_id = bl.workers.current_user_id();
    var url = '/api/cancellation_policies/get_cancellation_polices/' + user_id;

    let policies = await ajax.get(url);
    localStorage.setItem('cancellation_policies', JSON.stringify(policies));
}

bl.cancellation_policies.list = () => {

    var policies = localStorage.getItem('cancellation_policies');
    return JSON.parse(policies);
}