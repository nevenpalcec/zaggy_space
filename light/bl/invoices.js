bl.invoices = {};

bl.invoices.list_by_rent = async (rent_id) => {

    var url = '/api/invoices/list_by_rent/' + rent_id;

    return await ajax.get(url);


}

bl.invoices.list_by_user = async () => {

    var user_id = bl.workers.current_user_id();
    var url = '/api/invoices/list_by_user/' + user_id;

    var invoices = await ajax.get(url);
    localStorage.setItem('invoices', JSON.stringify(invoices));

}

bl.invoices.list = () => {

    var invoices = localStorage.getItem('invoices');
    return JSON.parse(invoices);

}


bl.invoices.delete_invoice = (id) => {

    var url = '/api/invoices/delete_invoice/' + id;

    console.log('obrisi' + id);
    $.get(url).done((data) => {

        console.log(data);
    });

}


bl.invoices.invoice_item_delete = async (id) => {

    var url = '/api/invoices/invoice_item_del/' + id;
    await ajax.get(url);

}