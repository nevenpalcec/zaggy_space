var region_list = new function () {

    var region_id = "1";
    var map = null;
    var polygons = [];
    var circles = [];
    var points = [];
    var pointsArray = [];
    var regies_selected = new Map();
    var token = 'pk.eyJ1IjoibWdyYW92YWMiLCJhIjoiY2xmY2IwOGJ4Mzh1MjNvcjA5bTJ1MG5mYyJ9.i78C8kbNAlMwpR_7DbLWbw';

    this.on_load = function () {

        //region_id = x;
        region_list.bl.setup_checkbox();
        region_list.ui.setup();
        region_list.bl.load_data();

    }

    this.on_load2 = function (x) {

        region_id = x;
        region_list.bl.setup_checkbox();
        region_list.ui.setup();
        region_list.bl.load_data();

    }

    this.clear_checked = async function () {
        var cbarray = document.getElementsByName('cb_regije');
        for (var i = 0; i < cbarray.length; i++) {
            cbarray[i].checked = false;
        }
    }

    this.deselected_all = async function () {
        pointsArray = [];
        var cbarray = document.getElementsByName('cb_regije');
        for (var i = 0; i < cbarray.length; i++) {

            cbarray[i].checked = false;
        }
    }

    this.edit = async function (id) {
        region_list.ui.clear_all();
        var cbarray = document.getElementsByName('cb_regije');
        for (var i = 0; i < cbarray.length; i++) {

            cbarray[i].checked = false;
        }
        regies_selected.clear();
        this.bl.load_data_add(id);
    }

    this.bl = new function () {

        // Setup caching and initial value from cache
        this.setup_checkbox = function () {


        }

        // Load data from server and show it
        this.load_data = async function (region_id) {
            console.log(region_id);

            if (!region_id == true) return;

            var leaf_map = document.getElementById('leaf_map');
            if (!leaf_map == true) return;

            leaf_map.style.opacity = '0.5';

            let region = await ajax.get_json('/api/b2b_regions/get/' + region_id);

            if (region.length == 0) return;

            let polygon_str = region[0]['polygon_str'];

            let zoom_level = region[0]['zoom_level'] ?? 6;

            //alert(polygon_str)
            if (!polygon_str == true) {
                console.log('Polygon points not found.');
                region_list.ui.clear_all();
            }

            else {

                let data = polygon_str.split(',').map(latlng => {
                    let arr = latlng.trim().split(' ');
                    return [parseFloat(arr[0]), parseFloat(arr[1])];
                }).slice(0, -1);

                points = data;
                region_list.ui.draw();

                // centar map 
                var center = polygons[0].getBounds().getCenter();
                var lat = center['lat'];
                var lng = center['lng'];
                map.flyTo([lat, lng], zoom_level);

            }

            leaf_map.style.opacity = '';
            var cbarray = document.getElementsByName('cb_regije');
            for (var i = 0; i < cbarray.length; i++) {

                cbarray[i].checked = false;
            }   

            //setCheckboxTrueAll();
        }
        this.ClearAll = async function () {
            region_list.ui.clear_all();
            var cbarray = document.getElementsByName('cb_regije');
            for (var i = 0; i < cbarray.length; i++) {

                cbarray[i].checked = false;
            }
            regies_selected.clear();
        }
        this.setCheckboxTrueAll = async function () {

            region_list.ui.clear_all();

            var cbarray = document.getElementsByName('cb_regije');
            for (var i = 0; i < cbarray.length; i++) {

                if (regies_selected.get(cbarray[i].value) == true) {
                    cbarray[i].checked = true;
                }
                else {
                    cbarray[i].checked = false;
                }
            }
            
            for (let [key, value] of regies_selected) {
                
                    if (value == true) {
                        //console.log(key + " - " + value);
                        this.load_data_add(key)
                }
            }
        }

        this.setCheckboxTrue = async function () {

            pointsArray = [];

            var cbarray = document.getElementsByName('cb_regije');
            for (var i = 0; i < cbarray.length; i++) {

                cbarray[i].checked = true;
            }
            
        }

        this.setCheckboxFalse = async function () {

            pointsArray = [];

            var cbarray = document.getElementsByName('cb_regije');
            for (var i = 0; i < cbarray.length; i++) {

                cbarray[i].checked = false;
            }
            region_list.ui.clear_all();
        }


        this.getSelectCheckbox = async function (region_id) {

            pointsArray = [];

            var cbarray = document.getElementsByName('cb_regije');
            for (var i = 0; i < cbarray.length; i++) {

                if (cbarray[i].checked == true) {
                    //console.log(cbarray[i].value)
                }
            }   
        }

        //on inputbox click
        this.getSelectCheckbox = async function (region_id, value) {
            if (value == true) {
                regies_selected.set(region_id, value);
            }
            else {
                regies_selected.delete(region_id);
            }

            if (value == true) {
                //this.load_data_add(region_id)
            }

            //this.setCheckboxTrueAll();
            //console.log(regies_selected);
        }

        this.get_map_data = async function () {

            var s = "";

            for (let [key, value] of regies_selected) {

                if (value == true) {
                    s = s + key + ",";
                }
            }
            return s;
        }

        this.load_data_add = async function (region_id) {
            pointsArray = [];
            //console.log('-> ' + region_id)
            if (!region_id == true) return;

            var leaf_map = document.getElementById('leaf_map');
            if (!leaf_map == true) return;

            leaf_map.style.opacity = '0.5';

            let region = await ajax.get_json('/api/b2b_regions/get/' + region_id);

            if (region.length == 0) return;

            let polygon_str = region[0]['polygon_str'];

            let zoom_level = 12; // region[0]['zoom_level'] ?? 18;

            //alert(polygon_str)
            if (!polygon_str == true) {
                console.log('Polygon points not found.');
                region_list.ui.clear_all();
            }

            else {

                let data = polygon_str.split(',').map(latlng => {
                    let arr = latlng.trim().split(' ');
                    return [parseFloat(arr[0]), parseFloat(arr[1])];
                }).slice(0, -1);

                points = data;
                //region_list.ui.draw_add();

                pointsArray.push(points)
                region_list.ui.clear_all();

                pointsArray.forEach(element => {
                    // ...use `element`...
                    points = element;
                    //console.log(points);
                    region_list.ui.draw_add()
                });

                // centar map 
                /*var center = data[0].getBounds().getCenter();
                var lat = center['lat'];
                var lng = center['lng'];
                map.flyTo([lat, lng], zoom_level);
                */

            }

            leaf_map.style.opacity = '';

        }

        // Save map details
        this.send = async function (id) {

            leaf_map.style.opacity = '0.5';

            let polygon_str = '';

            // At least 3 points to make a polygon
            if (points.length > 2) {

                let poly_points = points;
                poly_points.push(points[0]); // Connect last point to first

                polygon_str = points.map(p => p[0] + ' ' + p[1]).join(',');
            }

            let url = '/api/b2b_regions/save/' + id;

            var name = document.getElementById("b2b_regions_name").value;
            var type = document.getElementById("regions_types").value;
            var country_id = document.getElementById("region_contires_select").value;

            var json = {
                polygon_str: polygon_str,
                name: name,
                type: type,
                country_id: country_id,
                zoom_level: map.getZoom()
            }

            // save data
            await ajax.post(url, json);

            //console.log(url, j);
            leaf_map.style.opacity = '';

        }

    }

    this.ui = new function () {

        let clat = 45.297;
        let clng = 14.545;

        function map_onclick(e) {

            let l = e.latlng;
            points.push([l['lat'], l['lng']]);

            region_list.ui.draw();
        }

        this.clear = function () {

            polygons.forEach(p => p.remove());
            polygons = [];
            circles.forEach(c => c.remove());
            circles = [];
        }

        this.clear_all = function () {
            region_list.ui.clear();
            points = [];
        }

        this.setup = function () {

            map = L.map('leaf_map');

            if (map == null) return;

            map.setView([clat, clng], 8);

            var layer = L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=' + token, {
                id: 'mapbox/outdoors-v12',
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

            map.on('click', map_onclick);
            // Assumes your Leaflet map variable is 'map'..
            L.DomUtil.addClass(map._container, 'crosshair-cursor-enabled');
        }

        this.mireo_setup = function () {


            $('<link>').attr('rel', 'stylesheet')
                .attr('type', 'text/css')
                .attr('href', "{{ '/assets/mireo/mapbox.css'| theme }}")
                .appendTo('head');

            $.getScript("{{ '/assets/mireo/mapbox.js'| theme }}", function () {

                // mapboxgl.accessToken = 'pk.eyJ1Ijoic29jcmF0YSIsImEiOiJjaXJxc2wzam0waGU5ZmZtODhqd2ttamdxIn0.1ZQEByXoDD7fGIa9lUHIqg'; // temp

                var map = new mapboxgl.Map({
                    container: 'map',
                    // style: 'mapbox://styles/mapbox/streets-v11', // temp
                    style: '/assets/mireo/mireo.json',
                    center: {
                        lng: clng,
                        lat: clat
                    },
                    zoom: 12,
                    pitch: 0,
                    bearing: 0
                });

                const el = document.createElement('div');
                el.className = 'marker';

            });

        }

        this.draw = function () {

            region_list.ui.clear();

            if (points.length > 1) {
                region_list.ui.add_poly(points);
            }

            // Show last point
            if (points.length >= 1) {
                region_list.ui.add_circle(points[points.length - 1], 10);
            }
        }

        this.draw_add = function () {

            if (points.length > 1) {
                region_list.ui.add_poly(points);
            }

            // Show last point
            if (points.length >= 1) {
                region_list.ui.add_circle(points[points.length - 1], 10);
            }
        }

        this.delete_last = function () {

            if (points.length > 0) {
                points.splice(points.length - 1, 1);
            }
            region_list.ui.draw();
        }

        this.add_circle = function (point, radius = 500) {

            var circle = L.circle(point, {
                color: 'red',
                fillColor: '#f03',
                fillOpacity: 0.5,
                radius: radius
            })
                .bindPopup('Last point.')
                .addTo(map);

            circles.push(circle);

                map.panTo(point);
            
        }

        this.add_poly = function (latlng_list) {
            var polygon = L.polygon(latlng_list).addTo(map);
            polygons.push(polygon);
        }

        this.map_set_zoom = function (x, y, zoom_level) {
            map.flyTo([x, y], zoom_level);
            setTimeout(function () { map.invalidateSize(); console.log('map resize') }, 0);
            //map.setZoom(zoom_level);
        }

        this.map_mapbox = async (id, lat, lng) => {

            $('<link>').attr('rel', 'stylesheet')
                .attr('type', 'text/css')
                .attr('href', 'assets/css/mapbox.css')
                .appendTo('head');

            $.getScript('assets/plugins/mapbox/mapbox.js', function () {

                var map = new mapboxgl.Map({
                    container: id,
                    style: '/assets/plugins/mapbox/mireo.json',
                    center: {
                        lng: lng,
                        lat: lat
                    },
                    zoom: 12,
                    pitch: 0,
                    bearing: 0
                });

                const el = document.createElement('div');
                el.className = 'marker';

                var marker = new mapboxgl.Marker(el)
                    .setLngLat([lng, lat])
                    .addTo(map);

            });
        }
    }
}

function validate_region_form() {

    let inputs = [input_region_code, input_region_country, input_region_name];

    for (let i of inputs) {
        if (i.validity.valid === false || i.checkValidity() || !i.value) {
            remove_all_waitings();
            return false;
        }
    }
    return true;
}

function edit_area(row, button) {

    let elements = row.querySelectorAll('.area_name_input, .area_name_span, .area_code_input, .area_code_span');

    elements.forEach(e => {
        if (e.tagName.toLowerCase() == 'input') {
            e.classList.remove('d-none');
        }
        else {
            e.classList.add('d-none');
        }
    })

    // Show save and cancel buttons
    let buttons = button.parentElement.children;
    buttons[0].classList.add('d-none');
    buttons[1].classList.remove('d-none');
    buttons[2].classList.remove('d-none');
}

function cancel_area(row, button) {

    let elements = row.querySelectorAll('.area_name_input, .area_name_span, .area_code_input, .area_code_span');

    elements.forEach(e => {
        if (e.tagName.toLowerCase() == 'input') {
            e.value = e.dataset.init_val;
            e.classList.add('d-none');
        }
        else {
            e.classList.remove('d-none');
        }
    })

    // Show edit button
    let buttons = button.parentElement.children;
    buttons[0].classList.remove('d-none');
    buttons[1].classList.add('d-none');
    buttons[2].classList.add('d-none');
}

async function getIpAddress() {

    url = '/api/users/get_ip';

    const p_start = performance.now();

    var guid = localStorage.getItem('guid');
    var reseller_worker_perm = localStorage.getItem('reseller_worker_perm');
    var reseller_worker_type_id = localStorage.getItem('reseller_worker_type_id');
    var name = localStorage.getItem('name');
    var reseller_worker_id = localStorage.getItem('reseller_worker_id');
    var reseller_worker_guid = localStorage.getItem('reseller_worker_guid');

    var settings = {
        method: 'GET',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/json',
            'reseller_worker_guid': reseller_worker_guid,
            'name': name,
            'reseller_worker_id': reseller_worker_id,
            'reseller_worker_type_id': reseller_worker_type_id,
            'reseller_worker_perm': reseller_worker_perm,
            'guid': guid
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer'
    }

    try {

        const response = await fetch(url, settings);
        var text = await response.text();
        return text;

    }
    catch (ex) {

        // Clear all and logout
        console.log('Hasta la vista, baby...');

        return "error " + ex;
    }
}

async function get_pdf(id_hash) {

    url = '/api/invoces/pdf/' + id_hash;

    const p_start = performance.now();

    var guid = localStorage.getItem('guid');
    var reseller_worker_perm = localStorage.getItem('reseller_worker_perm');
    var reseller_worker_type_id = localStorage.getItem('reseller_worker_type_id');
    var name = localStorage.getItem('name');
    var reseller_worker_id = localStorage.getItem('reseller_worker_id');
    var reseller_worker_guid = localStorage.getItem('reseller_worker_guid');

    var settings = {
        method: 'GET',
        cache: 'no-cache',
        headers: {
            'Content-Type': 'application/pdf',
            'reseller_worker_guid': reseller_worker_guid,
            'name': name,
            'reseller_worker_id': reseller_worker_id,
            'reseller_worker_type_id': reseller_worker_type_id,
            'reseller_worker_perm': reseller_worker_perm,
            'guid': guid
        },
        redirect: 'follow',
        referrerPolicy: 'no-referrer'
    }

    try {


        var a = 'asdasdsa';
        const response = await fetch(url, settings);

        var text = await response.text();
        return text;

    }
    catch (ex) {

        // Clear all and logout
        console.log('Hasta la vista, baby...');

        return "error " + ex;
    }
}

function get_map(lat, long) {

    setTimeout(function () {
        get_map2(lat, long);
    }, 1000);

}

function set_map2() {

}

function get_map2(lat, long) {

    var map = L.map('map').setView([lat, long], 17);

    map.setView(new L.LatLng(lat, long), 17);

    L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=' + token, {
        id: 'mapbox/light-v8',
        attribution: ''
    }).addTo(map);

    // add marker 
    L.marker([lat, long])
        .addTo(map)
        .openPopup();
}