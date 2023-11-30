var units_details = {};

units_details.test = async () => {
    alert('Hello from units_details!');
}

units_details.map_int = async (lat, lng) => {

    //if (lat.indexOf(".") == -1 || lng.indexOf(".") == -1) return;

    var el = document.getElementById('unit_details_map');
    //alert(lat)
    if (!el == true) return;

    var map = L.map('unit_details_map');
    if (map == null) return;

    // set view
    map.setView([lat, lng], 8);
     
    var key = 'pk.eyJ1IjoibWdyYW92YWMiLCJhIjoiY2xmY2IwOGJ4Mzh1MjNvcjA5bTJ1MG5mYyJ9.i78C8kbNAlMwpR_7DbLWbw';
    var layer = L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=' + key, {
        id: 'mapbox/light-v11',
        attribution: '',
        tileSize: 512,
        style: '/assets/plugins/mapbox/mireo.json',
        zoom: 14,
        pitch: 0,
        bearing: 0,
        zoomOffset: -1
    });

    // add layer to map
    layer.addTo(map);

    // define icon
    var icon_img = L.icon({
        iconUrl: '',
        iconSize: [70, 70]
    });

    //if (!lat == true || !lng == true) continue;

    var marker = L.marker([lat, lng], { icon: icon_img });

    // add market to map
    marker.addTo(map);

}