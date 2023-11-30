bl.objects_realestates_pictures = {};

bl.objects_realestates_pictures.delete_picture = async (id) => {

    var url = '/api/objects_realestates_pictures/delete_picture/' + id;

    await ajax.get(url);

    //$.post(url).done((data) => {
    //    console.log(data);
    //});
}

bl.objects_realestates_pictures.add_picture = async (picture) => {

    var url = '/api/objects_realestates_pictures/add_picture/' + picture;

    await ajax.get(url);

    console.log('slika je dodana' + picture);
    //$.get(url).done((data) => {
    //    console.log(data);
    //});
}