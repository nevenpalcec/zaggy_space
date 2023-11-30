bl.rent_doors_locks = {};

bl.rent_doors_locks.list_doors = async (rent_id) => {

    var url = '/api/rent_doors_locks/list_doors/' + rent_id;
    return await ajax.get(url);


}  