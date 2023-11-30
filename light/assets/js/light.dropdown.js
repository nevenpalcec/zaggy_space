// New JS (old js is inside light-dropdown.js)

if (typeof light === 'undefined') {
    var light = {};
}

light.dropdown = new function () {


    this.clearInput = (inputElement) => {

        try {
            inputElement.value = null;
        } catch (error) {
            console.log(error);
        }
       
    }

    function setup_ui(dropdown) {

        console.log(dropdown);


        try {

            // Ako želiš nešto odabrati, pozovi .querySelector
            // Npr


            //debugger;

            let btn = dropdown.querySelector(".light_dropdown-btn");
            let menu = dropdown.querySelector(".light_dropdown-menu");
            let btn_box = dropdown.querySelector(".light_dropdown-box");
            let no_result_el = dropdown.querySelector(".no_result");
            let input_el = dropdown.querySelector('.light_dropdown-input');
            let items = dropdown.querySelectorAll('.light_dropdown-item');



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

                    /*no_result_el.classList.add('hidden');*/

                    let li_shown = 0;
                    let value = input_el.value.toLowerCase();

                    items.forEach(li => {
                        if (li.getElementsByClassName("light_dropdown-label")[0].innerText.toLowerCase().includes(value)) {

                            li.classList.remove('hidden');
                            ++li_shown;

                        } else {

                            li.classList.add('hidden');
                        }
                    });

                    // Show 'No results...'

                    /*if (li_shown == 0) {
                       no_result_el.classlist.remove('hidden');
                    }*/

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
                document.addEventListener('click', function (e) {

                    //let dropdown = document.getElementById("modal_add_guest_details_country");
                    let dropdown_input = dropdown.querySelector("input");
                    let dropdown_ul = dropdown.querySelector("ul");

                    if (dropdown != document.activeElement && dropdown_input != document.activeElement && dropdown_ul != document.activeElement) {

                        menu.classList.remove("show");
                        menu.classList.add("hidden");
                        input_el.classList.remove("opacity_50");

                        light.dropdown.clearInput(input_el);


                        //let dropdown_items_list = document.getElementById("modal_add_guest_details_countries_ul");
                        //let remaining_item_in_dropdown = dropdown_items_list.querySelector('.light_dropdown-item.select_li:not(.hidden)');

                        //if (remaining_item_in_dropdown) {

                        //    if (remaining_item_in_dropdown.children[1].innerText.toLowerCase() != input_el.value.toLowerCase()) {

                        //        light.dropdown.clearInput();

                        //    } else {

                        //        //input_el.value = remaining_item_in_dropdown.children[1].innerText;

                        //        light.dropdown.clearInput();
                        //    }

                        //} else {

                        //    light.dropdown.clearInput();
                        //}   
                    }
                });


                // Click on select will close dropdown menu

                for (let i = 0; i < items.length; i++) {
                    const element = items[i];
                    element.onclick = function (e) {

                        //debugger;

                        //btn_box.innerHTML = e.target.innerHTML;
                        btn_box.innerHTML = e.target.outerHTML;
                        input_el.setAttribute("data-value", e.target.dataset.value);
                        menu.classList.remove("show");
                        menu.classList.add("hidden");
                        input_el.classList.remove("opacity_50");
                        input_el.classList.remove("opacity_100");
                        input_el.value = "";
                    }
                }

            } else {

                // Toggle btn
                btn.addEventListener("click", function (e) {

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

        } catch (error) {

            console.log(error);
        }
        

    }

    this.init = function (selector) {


        try {

            var ddmenu = document.querySelector(selector);

            if (!ddmenu) {
                console.warn('light.dropdown: Cannot find element "' + selector + '"');
            }


            // Setup dropdown UI
            setup_ui(ddmenu);


            // Get <input> element
            var input_el = ddmenu.querySelector('.light_dropdown-input');
            // Get menu items that do not have .hidden
            var items = ddmenu.querySelectorAll('.light_dropdown-menu .light_dropdown-item:not(.hidden)');

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
                console.warn('light.dropdown: Cannot find .light_dropdown-input inside "' + selector + '".');
            }
            // Check if shown items exist
            if (items.length == 0) {
                console.warn('light.dropdown: There is a possible error. Cannot find any shown ".light_dropdown-item" inside "' + selector + '.light_dropdown-menu"');
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
                    console.warn('light.dropdown: Cannot find "' + val +
                        '" inside "' + selector +
                        '".\n\tAvailable values: ' + all_vals);
                }
                else {
                    // Click on item
                    search[0].click();
                }
            }

            // Set "light" to HTML element if not exist
            if (!ddmenu?.light) {
                ddmenu.light = {};
            }
            ddmenu.light.dropdown = {
                get_all_values,
                get_value,
                set_value
            };

            // Return ELEMENT.light.dropdown
            return ddmenu.light.dropdown;

        } catch (error) {

            console.log(error);
            return null;
        }
    }
}
