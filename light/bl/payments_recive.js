bl.payment_recive = {};

bl.payment_recive.list_of_payment_recive = async (rent_id) => {

    var url = '/api/payments_recive/list_of_payment_recive/' + rent_id;
    return await ajax.get(url);


}  