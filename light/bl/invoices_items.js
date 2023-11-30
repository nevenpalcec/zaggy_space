bl.invoices_items = {};

bl.invoices_items.list = async (invoice_id) => {

    var url = '/api/invoices_items/list/' + invoice_id;

    return await ajax.get_json(url);


}