var units_map = {};

units_map.map = null;
units_map.properties = [];
units_map.markers = [];
units_map.marker_list = [];

units_map.map_int = async (data) => {

    var lat = 45.815399;
    var lng = 15.966568

    // console.log(units_map.properties)
    if (!units_map.map == false) {

        units_map.map.off();
        units_map.map.remove();

        units_map.map = null;
        units_map.properties = [];
        units_map.markers = [];
        units_map.marker_list = [];
        
    }

    // load properties
    units_map.properties = !data == false ? JSON.parse(data) : null;

    units_map.map = L.map('map_properites');
    if (units_map.map == null) return;

    // set view
    units_map.map.setView([lat, lng], 8);

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
    layer.addTo(units_map.map);

    // define icon
    var icon_img = L.icon({
        iconUrl: 'https://za.zaggys.com/assets/images/icons/zagreb-map-icon.svg',
        iconSize: [70, 70]
    });

    // marker layer
    markers = L.markerClusterGroup({
        spiderfyOnMaxZoom: true,
        showCoverageOnHover: false,
        zoomToBoundsOnClick: true,
        chunkedLoading: true
    });

    for (var o of units_map.properties) {

        lat = o.unit_latitude;
        lng = o.unit_longitude;

        if (!lat == true || !lng == true) continue;


        //var marker = L.marker([lat, lng], { title: o.id, icon: icon_img });

        var html = ` 
            <a href="/objects/details/${o.object_id_hash}" target = "_blank" class="text-dark">
                <div>
                    <strong> ${o.object_name} </strong>
                <div>
                <div>
                   ${o.object_id_hash}
                </div>
            </a>
            `;


        var marker = L.marker([lat, lng], { icon: icon_img });
        marker.bindPopup(html, { maxWidth: 200, maxHeight: 200 });

        // add marker to list
        units_map.marker_list.push(marker);

    }

    // add markers from list ot layer
    markers.addLayers(units_map.marker_list);

    // add to map
    units_map.map.addLayer(markers);

}
