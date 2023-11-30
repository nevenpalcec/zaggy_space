bl.email = {};

bl.email.list_of_emails = async (rent_id) => {

    var url = '/api/log_email/list_of_emails/' + rent_id;

    return await ajax.get(url);


}  