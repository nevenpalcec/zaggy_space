let page = {};

page.home = function() {
    //$(".main_page").load("/pages/home.html");
}

page.get_current_url = () => {

    var url = location.href;
    return url.split('/')[2];

}

page.get_current_page = () => {

    let hash = location.hash.slice(1);
    var page = hash.split('?')[0];
    return page;

}

page.get_id = () => {

    var id = location.hash.split('?')[1];
    return id;
}