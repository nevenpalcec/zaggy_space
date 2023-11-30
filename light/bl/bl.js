'use strict';

const bl = {};

bl.copy_to_clipboard = function(txt) {

    let copyText = document.createElement('input');
    copyText.value = txt;

    /* Select the text field */
    copyText.select();
    copyText.setSelectionRange(0, 99999); /* For mobile devices */

    /* Copy the text inside the text field */
    navigator.clipboard.writeText(copyText.value);
    copyText.remove();
}

// ui definition
bl.ui = new function () {

    this.main_div_show = (div_id) => {

        var div = document.getElementById(div_id);
        var div_html = template_div_loading.innerHTML;
        div.insertAdjacentHTML("afterbegin", div_html);
    }

    this.main_div_hide = () => {

        var divs = document.getElementsByClassName('loading');

        if (divs.length > 0) {
            for (var d of divs) {
                d.style.display = 'none';
            }
        }

    }

}

// variables
bl.varables = new function () {

    this.set_default = () => {

        var today = new Date();
        var today_iso = js.date.toIso(today);

        var variables = {

            calendar_date_from: today_iso,
            calendar_days: 31,
            rents_date_from: today_iso,
            rents_days: 30,
            price_date_from: today_iso,
            price_days: 30

        };

        localStorage.setItem("variables", JSON.stringify(variables));
    }

    // get a list (all) of varables
    this.list = () => {

        var v = localStorage.getItem('variables');

        // if variables dont exist or empty then relaod with defailt settinss
        if (!v == true) {
            this.set_default();
            v = localStorage.getItem('variables');
        }

        var variables = JSON.parse(v);
        return variables;
    }

    this.get = (name) => {

        var variables = this.list();
        var v = variables[name];
        return v;

    }

    this.set = (name, value) => {

        var variables = this.list();
        variables[name] = value;
        localStorage.setItem("variables", JSON.stringify(variables));

    }

    this.objects = new function () {

        this.get_array = () => {

            var objs = [];
            var objects = bl.varables.list();

            for (var o of objects) {
                objs.push(o['id']);
            }

        }

    }

}


bl.rents_local = new function () {
   
    // load default settings for worker
    this.load_int = async () => {

        // create registars
        var rents = {};
        localStorage.setItem('rents', JSON.stringify(rents));

        // set variables
        bl.varables.set_default();

        //  set logo
        main_menu_left.load();

        // load objects
      
        await bl.objects.list_load();

        await bl.rents.list_arrivals_today();

        await bl.rents.list_departures_today();

        await bl.rents.list_arrivals_tomorrow();

        await bl.rents.list_departures_tomorrow();

    }

    this.get = () => {

        var rents_string = localStorage.getItem('rents');
        return JSON.parse(rents_string);

    }

}

bl.users = new function () {

    this.load_int = async () => {
       
        var users = {};
        localStorage.setItem('users', JSON.stringify(users));

        // set variables
        bl.varables.set_default();


        //  set logo
        main_menu_left.load();

        bl.user.load();

    }

    this.get = () => {

        var users_string = localStorage.getItem('users');
        return JSON.parse(users_string);

    }
}

// registars
bl.registars = new function () {

    // load default settings for worker
    this.load_int = async () => {
        // create registars
        var registars = {};
        localStorage.setItem('registars', JSON.stringify(registars));

        // set variables
        bl.varables.set_default();

        //  set logo
        main_menu_left.load();
        // load objects
        await bl.objects.list_load();

        // load sources
        await bl.rents_sources.load();

        // load statuses
        await bl.rents_status.load();

        // load payments methods
        await bl.payment_methods.load();

        //load countries
        await bl.countries.load();

        //load cleaning options
        await bl.object_cleans.load();

        //load currencies
        await bl.currency.load();

        //load b2b
        await bl.b2b.load();

        // load b2b list
        await bl.b2b.list_sort();

        //load room types
        await bl.rooms_types.load
            
    }

    this.get = () => {
        
        var registars_string = localStorage.getItem('registars');
        return JSON.parse(registars_string);

    }
}
