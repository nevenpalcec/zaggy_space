
// Select with dropdown
function init_dropdown_select(id) {

    var dropdown_container = document.getElementById(id);
    var dropdown_list = dropdown_container.getElementsByClassName("dropdown-menu")[0];
    var dropdown_selected = dropdown_container.getElementsByClassName("btn")[0];

    for (let i = 0; i < dropdown_list.children.length; i++) {
        dropdown_list.children[i].onclick = function (e) {
            dropdown_selected.innerHTML = e.currentTarget.innerHTML;
            dropdown_selected.dataset.select_id = e.currentTarget.innerHTML;
            dropdown_selected.value = e.currentTarget.innerHTML;
        }
    }

};

function init_dropdown_get(id) {
    var id = document.getElementById("dropdown_pets").getElementsByClassName("btn")[0].dataset.select_id
    return id;
}

function dropdown_select(dropdown_id) {

    var dropdown_container = document.getElementById(dropdown_id);
    var dropdown_input = dropdown_container.getElementsByClassName("select-input")[0];
    var dropdown_list = dropdown_container.getElementsByClassName("select_ul")[0];
    var dropdown_selected_option = dropdown_container.getElementsByClassName("selected-option")[0];

    dropdown_input.onclick = function () {
        dropdown_container.classList.add("active");
    };

    for (let i = 0; i < dropdown_list.children.length; i++) {
        dropdown_list.children[i].onclick = function (e) {
            dropdown_selected_option.innerHTML = "";
            dropdown_selected_option.innerHTML = e.currentTarget.innerHTML;
            dropdown_container.classList.remove("active");
            dropdown_input.style.opacity = "0";
        }
    }

    dropdown_input.onkeyup = function (e) {
        if (e.target.value !== "") {
            e.target.style.opacity = "1";
        }
        else {
            e.target.style.opacity = "0.5";
        }
    };

    dropdown_input.onfocus = function (e) {
        e.target.style.opacity = "0.5";
    }

    $(document).mouseup(function (e) {
        if ($(".select-input").is(e.target)) {
        }
        else
        {
            $(".select_ul").parents(".select_wrap").removeClass("active");
        }
    });
}

if (typeof hiza === 'undefined') {
    var hiza = {};
}

hiza.dropdown = new function () {

    function setup_ui(dropdown) {

        //console.log(dropdown);

        // Ako želiš nešto odabrati, pozovi .querySelector
        // Npr

        let btn = dropdown.querySelector(".hiza_dropdown-btn");
        let menu = dropdown.querySelector(".hiza_dropdown-menu");
        let btn_box = dropdown.querySelector(".hiza_dropdown-box");
        let no_result_el = dropdown.querySelector(".no_result");
        let input_el = dropdown.querySelector('.hiza_dropdown-input');
        let items = dropdown.querySelectorAll('.hiza_dropdown-item');

        if (dropdown.classList.contains('search', 'icon')) {

            // Open dropdown
            btn.addEventListener("click", function () {

                menu.classList.add("show");
                menu.classList.remove("hidden");
                input_el.classList.add("opacity_50");
                items.forEach(li => {
                    li.classList.remove('hidden');
                });

            });

            // Setup search
            input_el.addEventListener("input", function (e) {

                no_result_el.classList.add('hidden');

                let li_shown = 0;
                let value = input_el.value.toLowerCase();

                items.forEach(li => {
                    if (li.getElementsByClassName("hiza_dropdown-label")[0].innerText.toLowerCase().includes(value)) {
                        li.classList.remove('hidden');
                        ++li_shown;
                    } else {
                        li.classList.add('hidden');
                    }
                });

                // Show 'No results...'
                if (li_shown == 0) {
                    no_result_el.classList.remove('hidden');
                }

                // Value is empty input will show when is value not empty and input will hide
                if (value.length == 0) {
                    input_el.classList.add("opacity_50");
                    input_el.classList.remove("opacity_100");
                } else {
                    input_el.classList.add("opacity_100");
                    input_el.classList.remove("opacity_50");
                    menu.classList.add("show");
                    menu.classList.remove("hidden");
                }

            });

            // Out of the dropdown and dropdown-menu will close
            document.addEventListener('mouseup', function (e) {
                if (dropdown.outerHTML.includes(e.target.outerHTML) == false) {
                    menu.classList.remove("show");
                    menu.classList.add("hidden");
                    input_el.classList.remove("opacity_50");
                }
            });

            // Click on select will close dropdown menu
            for (let i = 0; i < items.length; i++) {
                const element = items[i];
                element.onclick = function (e) {
                    btn_box.innerHTML = e.target.innerHTML;
                    input_el.setAttribute("data-value", e.target.dataset.value);
                    menu.classList.remove("show");
                    menu.classList.add("hidden");
                    input_el.classList.remove("opacity_50");
                    input_el.classList.remove("opacity_100");
                    input_el.value = "";
                }
            }

        }

        else
        {

            // Toggle btn
            btn.addEventListener("click", function () {

                if (menu.classList.contains("show")) {
                    menu.classList.remove("show");
                    menu.classList.add("hidden");
                } else {
                    menu.classList.add("show");
                    menu.classList.remove("hidden");
                }

            });

            // Out of the dropdown and dropdown-menu will close
            document.addEventListener('mouseup', function (e) {
                if (dropdown.outerHTML.includes(e.target.outerHTML) == false) {
                    menu.classList.remove("show");
                    menu.classList.add("hidden");
                }
            });

            // Click on select will close dropdown menu
            for (let i = 0; i < items.length; i++) {
                const element = items[i];
                element.onclick = function (e) {
                    btn.innerHTML = e.target.innerHTML;
                    btn.setAttribute("data-value", e.target.dataset.value);
                    menu.classList.remove("show");
                    menu.classList.add("hidden");
                }
            }

        }

    }

    this.init = function (selector) {

        var ddmenu = document.querySelector(selector);

        if (!ddmenu) {
            console.warn('hiza.dropdown: Cannot find element "' + selector + '"');
        }

        // Setup dropdown UI
        setup_ui(ddmenu);

        // Get <input> element
        var input_el = ddmenu.querySelector('.hiza_dropdown-input');
        // Get menu items that do not have .hidden
        var items = ddmenu.querySelectorAll('.hiza_dropdown-menu .hiza_dropdown-item:not(.hidden)');

        // Convert to normal array and filter empty values
        items = Array.from(items).filter(function (item) {

            let val = item.dataset.value;

            if (val || val === '') {
                // Valid values
                return true;
            }
            else {
                return false;
            }
        });

        // Check if input exists
        if (!input_el) {
            console.warn('hiza.dropdown: Cannot find .hiza_dropdown-input inside "' + selector + '".');
        }

        // Check if shown items exist
        if (items.length == 0) {
            console.warn('hiza.dropdown: There is a possible error. Cannot find any shown ".hiza_dropdown-item" inside "' + selector + '.hiza_dropdown-menu"');
        }

        // Prepare functions for return value
        function get_all_values() {
            return items.map(it => it.dataset.value);
        }

        function get_value() {
            return input_el.value;
        }

        function set_value(val) {

            // Find value in dropdown
            let search = items.filter(it => {
                if (it.dataset.value == val) {
                    return true;
                }
                else {
                    return false;
                }
            });

            // If not found
            if (search.length == 0) {

                let all_vals = JSON.stringify(items.map(i => i.dataset.value));
                console.warn('hiza.dropdown: Cannot find "' + val +
                    '" inside "' + selector +
                    '".\n\tAvailable values: ' + all_vals);
            }
            else {
                // Click on item
                search[0].click();
            }
        }

        // Set "hiza" to HTML element if not exist
        if (!ddmenu?.hiza) {
            ddmenu.hiza = {};
        }

        ddmenu.hiza.dropdown = {
            get_all_values,
            get_value,
            set_value
        };

        // Return ELEMENT.hiza.dropdown
        return ddmenu.hiza.dropdown;
    }

}
