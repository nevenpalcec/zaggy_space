 var ui = {};
 
 // Filter dropdown to get text on main dropdown
ui.dropdown_list = function (id) {

    let dropdown_id = document.getElementById(id);

    // if we can find then exit
    if (dropdown_id == true) return;

    let dropdown_menu = dropdown_id.getElementsByClassName("dropdown_menu")[0];
    let dropdown_ul = dropdown_id.getElementsByClassName("dropdown_ul")[0];
    let dropdown_name = dropdown_id.getElementsByClassName("dropdown_name")[0];
    let button = dropdown_id.getElementsByClassName("btn")[0];

    button.addEventListener("click", function () {
        if (!button.classList.contains("open")) {
            dropdown_menu.classList.remove("hidden");
            button.classList.add("open");
        } else {
            dropdown_menu.classList.add("hidden");
            button.classList.remove("open");
        }
    });

    document.addEventListener("mouseup", function (e) {
        if (dropdown_id.outerHTML.includes(e.target.outerHTML) == false) {
            dropdown_menu.classList.add("hidden");
            button.classList.remove("open");
        }
    });

    //dropdown_close.onclick = function () {
    //    if (!dropdown_menu.classList.contains("open")) {
    //        dropdown_menu.classList.add("hidden");
    //        button.classList.remove("open");
    //    }
    //};

    // handle li click
    for (let i = 0; i < dropdown_ul.children.length; i++) {
        const element = dropdown_ul.children[i];
        element.onclick = function (e) {

            dropdown_name.innerHTML = e.currentTarget.innerHTML;
            if (id == 'modal_filter_country') {
                document.querySelector('#' + id).dataset.id = e.currentTarget.dataset.id;
                document.querySelector('#' + id).dataset.name = e.currentTarget.innerText;

            }
            else {
                document.querySelector('#' + id).dataset.name = e.currentTarget.innerText;
                document.querySelector('#' + id).dataset.id = e.currentTarget.dataset.id;
            }

            dropdown_menu.classList.add("hidden");
            button.classList.remove("open");
        }
    }

}

// Filter dropdown - only click
ui.dropdown_text = function (id) {

    let dropdown_id = document.getElementById(id);
    let btn = dropdown_id.getElementsByClassName("btn")[0];
    let dropdown_ul = dropdown_id.getElementsByClassName("dropdown_ul")[0];
    let dropdown_menu = dropdown_id.getElementsByClassName("dropdown_menu")[0];

    btn.onclick = function () {

        if (!btn.classList.contains("open")) {
            dropdown_menu.classList.remove("hidden");
            btn.classList.add("open");
        } else {
            dropdown_menu.classList.add("hidden");
            btn.classList.remove("open");
        }
    }

    $(document).mouseup(function (e) {
        if (dropdown_id.outerHTML.includes(e.target.outerHTML) == false) {
            dropdown_menu.classList.add("hidden");
            btn.classList.remove("open");
        }
    });

    //dropdown_close.onclick = function () {

    //    if (!dropdown_menu.classList.contains("open")) {
    //        dropdown_menu.classList.add("hidden");
    //        btn.classList.remove("open");
    //    }
    //}

    //for (let i = 0; i < dropdown_ul.children.length; i++) {
    //    const element = dropdown_ul.children[i];
    //    element.onclick = function (e) {
    //        dropdown_menu.classList.add("hidden");
    //        btn.classList.remove("open");
    //    }
    //}


}

// Filter dropdown - date
ui.dropdown_date = function (id) {
    let dropdown_id = document.getElementById(id);
    let btn = dropdown_id.children[0];
    let dropdown = dropdown_id.lastElementChild;

    btn.addEventListener("click", function () {
        dropdownToggle();
    });

    $(document).mouseup(function (e) {
        if (dropdown_id.outerHTML.includes(e.target.outerHTML) == false) {
            dropdown.classList.add("hidden");
            btn.classList.remove("open");
        }
    });
    function dropdownToggle() {
        if (!btn.classList.contains("open")) {
            dropdown.classList.remove("hidden");
            btn.classList.add("open");
        } else {
            dropdown.classList.add("hidden");
            btn.classList.remove("open");
        }
    }
}

// Filter dropdown - check
ui.dropdown_check = function (id) {

    let filter_id = document.getElementById(id);
    let btn = filter_id.getElementsByClassName("btn")[0];
    let dropdown = filter_id.lastElementChild;
    let close = filter_id.lastElementChild.children[0].lastElementChild;

    btn.addEventListener("click", function () {
        dropdownToggle();
    });

    close.addEventListener("click", function () {
        dropdownToggle();
    });

    $(document).mouseup(function (e) {
        if (!$(dropdown).is(e.target) && $(dropdown).has(e.target).length === 0) {
            dropdown.classList.add("hidden");
            btn.classList.remove("open");
        }
    });

    function dropdownToggle() {
        if (!btn.classList.contains("open")) {
            dropdown.classList.remove("hidden");
            btn.classList.add("open");
        } else {
            dropdown.classList.add("hidden");
            btn.classList.remove("open");
        }
    }

    if (filter_id.classList.contains("toggle")) {

        var dropdown_li = filter_id.querySelectorAll(".dropdown_li");


        for (let i = 0; i < dropdown_li.length; i++) {

            dropdown_li[i].addEventListener("click", function () {

                var checks = this.parentElement.querySelectorAll('.icon_check.check_dropdown');
                for (var c of checks) {
                    c.classList.remove('check_dropdown');
                }

                this.children[0].children[0].lastElementChild.classList.add("check_dropdown");
                dropdown.classList.add("hidden");
                btn.classList.remove("open");
            });

        }
    }
}

// Custom select
ui.custom_select = function (id) {

    let select_id = document.getElementById(id);
    let btn_list = select_id.getElementsByTagName("button")

    // we didn't find it, so letes quit
    if (btn_list.length <= 0) return;

    let btn = btn_list[0];
    let ul = select_id.getElementsByTagName("ul")[0];
    let li = select_id.getElementsByTagName("li")[0];

    btn.onclick = function () {
        if (!btn.classList.contains("open")) {
            btn.classList.add("open");
            ul.classList.remove("hidden");
            let ulOffset = $(ul).offset();
            let spaceUp = (ulOffset.top - $(btn).height() - $(ul).height()) - $(window).scrollTop();
            let spaceDown = $(window).scrollTop() + $(window).height() - (ulOffset.top + $(ul).height());
            if (spaceDown < 0 && (spaceUp >= 0 || spaceUp > spaceDown)) {
                ul.classList.add("reverse");
            } else {
                ul.classList.remove("reverse");
            }
        } else {
            btn.classList.remove("open");
            ul.classList.add("hidden");
        }
    };

    document.addEventListener("mouseup", function (e) {
        if (!$(btn).is(e.target) && $(ul).has(e.target).length === 0) {
            if (!$(ul).is(e.target) && $(ul).has(e.target).length === 0) {
                ul.classList.add("hidden");
                btn.classList.remove("open");
            }
        }
    });

    
    for (let i = 0; i < ul.children.length; i++) {
        ul.children[i].onclick = function (e) {
            //if (btn.children[0].classList.contains("label") == true) {
            //    btn.children[0].textContent = e.currentTarget.textContent;
            //} else {
            //    btn.innerHTML = e.currentTarget.innerHTML;
            //}
            btn.innerHTML = e.currentTarget.innerHTML
            ul.classList.add("hidden");
            btn.classList.remove("open");
            btn.setAttribute('data-id', e.currentTarget.dataset.id);
            btn.setAttribute('data-code1', e.currentTarget.dataset.code1);
        }
    }

}

// Custom select search
ui.select_search_custom = function (id) {

    var select_id = document.getElementById(id);
    var input = select_id.getElementsByTagName("input")[0];
    var ul = select_id.getElementsByTagName("ul")[0];
    var li = ul.querySelectorAll(".select_li");
    var span = select_id.getElementsByTagName("span")[0];
    var clean = select_id.getElementsByClassName("clean")[0];

    input.addEventListener("click", function () {
        ul.classList.remove("hidden");
        if (input.value.length <= 0) {
            input.style.backgroundColor = "rgba(255,255,255,.5)";
        }
    });
    // menu close by click out botder's menu
    document.addEventListener("mouseup", function (e) {
        if (!$(ul).is(e.target) && $(ul).has(e.target).length === 0) {
            ul.classList.add("hidden");
            if (input.value.length <= 0) {
                input.style.backgroundColor = "";
            }
        }
        if (!$(input).is(e.target) && $(input).has(e.target).length === 0) {
            clean.classList.add("hidden");
        }
    });
    // focus on input to show clean when contais is true
    input.addEventListener("focus", function () {
        clean.classList.remove("hidden");
    });
    for (let i = 0; i < ul.children.length; i++) {
        ul.children[i].onclick = function (e) {
            span.innerHTML = e.currentTarget.innerHTML;
            ul.classList.add("hidden");
            input.style.backgroundColor = "";
            input.value = "";
            for (let j = 0; j < ul.children.length; j++) {
                const element = ul.children[j];
                ul.children[j].style.display = "";
            }
        }
    }
    input.onclick = function () {
        var ulOffset = $(ul).offset();
        var spaceUp = (ulOffset.top - $(input).height() - $(ul).height()) - $(window).scrollTop();
        var spaceDown = $(window).scrollTop() + $(window).height() - (ulOffset.top + $(ul).height());
        if (spaceDown < 0 && (spaceUp >= 0 || spaceUp > spaceDown)) {
            ul.classList.add("reverse");
        } else {
            ul.classList.remove("reverse");
        }
        clean.classList.remove("hidden");
    }
    input.addEventListener("keyup", function () {
        filter = input.value.toUpperCase();
        for (let i = 0; i < li.length; i++) {
            var list = li[i];
            textValue = list.textContent || list.innerText;
            if (textValue.toUpperCase().indexOf(filter) > -1) {
                li[i].style.display = "";
            } else {
                li[i].style.display = "none";
            }
        }
    })
    document.addEventListener("keyup", function () {
        if (input.value.length > 0) {
            input.style.backgroundColor = "white";
        } else {
            input.style.backgroundColor = "rgba(255,255,255,.5)";
        }
    })
    clean.addEventListener("click", function () {
        span.innerHTML = "";
    });
}

// Filter calendar
ui.filter_calendar = function (id) {

    let calendar_id = document.getElementById(id);
    let dropdown_menu = calendar_id.getElementsByClassName("dropdown_menu")[0];
    let btn = calendar_id.getElementsByClassName("btn")[0];

    $("#" + calendar_id).datepicker({
        firstDay: 1,
        minDate: 0,
        numberOfMonths: 1,
        onSelect: function (dateText, inst) {
            $("." + dropdown_menu).addClass("hidden");
            btn.classList.remove("open");
            custom_calendar.innerHTML = dateText;
        }
    });

}

// Name document title
ui.display_title = function (label) {
    document.title = label;
}

// Content loading show
ui.content_loading = async function (id) {
    document.body.style.cursor = 'wait';
    let loading_id = document.getElementById(id);

    await $(loading_id).fadeIn(200).promise();

    setTimeout(function () {
        //$(loading_id).fadeOut(200);
        document.body.style.cursor = 'default';
    }, 5000);
}

// hide loading
ui.content_loading_hide = async function (id) {
    
    let loading_id = document.getElementById(id);
    //$(loading_id).hide();
    await $(loading_id).fadeOut(200).promise();
    document.body.style.cursor = 'default';
}

// Mobile open dropdown by click btn
ui.filter_mobile_btn = function (id, dropdown_id) {
    var mobile_btn = document.getElementById(id);
    var dropdown = document.getElementById(dropdown_id);
    var dropdown_btn = dropdown.getElementsByTagName("button")[0];
    var dropdown_menu = dropdown.getElementsByClassName("dropdown_menu")[0];
    mobile_btn.addEventListener("click", function () {
        if (!dropdown_btn.classList.contains("open")) {
            dropdown.classList.remove("d-none");
            dropdown_menu.classList.remove("hidden");
            dropdown_btn.classList.add("open");
        } else {
            dropdown.classList.add("d-none");
            dropdown_menu.classList.add("hidden");
            dropdown_btn.classList.remove("open");
        }
    })
}

// input counter
ui.input_counter = function (id) {
    var value = 0;
    var input_id = document.getElementById(id);
    var minus = input_id.getElementsByClassName("minus")[0];
    var plus = input_id.getElementsByClassName("plus")[0];
    var input = input_id.getElementsByClassName("form-control")[0];
    var counter = document.querySelector('#' + id + ' .input-group > input').value;
    minus.addEventListener("click", function () {
        if (value < counter) {
            counter--;
            input.value = counter;
        }
    });
    plus.addEventListener("click", function () {
        counter++;
        input.value = counter;
    });
}


// load modals
ui.modal_load = () => {

    // set up modals
    if (!localStorage.getItem('worker') == false) {
        $(".modal_new_rent").load("/pages/rents/modal_rent_new.html");
        $(".modal_rent_add_guest_details").load("/pages/guest/modal_add_guest_details.html");
        $(".modal_rent_edit_guest_details").load("/pages/guest/modal_edit_guest_details.html");
        // New
        $(".modal_rent_details_v1").load("/pages/rents/modal_rent_details.html");
        // Old
        //$(".modal_rent_details").load("/pages/rents/modal_rent_details-old.html");

        // loading modal
        $(".modal_search").load("/pages/components/modal_search.html");
        $(".modal_rent_details").load("/pages/rents/modal_rent_details.html");

    }

}

ui.notify_load = () => {
    $(notifications).load('/pages/partals/notify.html');
}


// ************************************************************
// ck edi
ui.ck_edit = {};
ui.ck_edit.toolbar_all = [
    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
    { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
    { name: 'colors', items: ['TextColor', 'BGColor'] },
    { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
    { name: 'about', items: ['Find', 'Replace', 'Smiley', 'SpecialChar'] },
    { name: 'document', items: ['Source'] },
    '/',
    { name: 'editing', items: ['SelectAll', 'Scayt'] },
    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
    { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', 'CopyFormatting', 'RemoveFormat'] },
    { name: 'paragraph', items: ['NumberedList', 'BulletedList', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl', 'Language'] },
    { name: 'insert', items: ['Image', 'Table', 'HorizontalRule',] },
];

ui.ck_edit.toolbar_small = [
    ['Paste', 'PasteText', 'PasteFromWord', 'Undo', 'Redo', '-', 'Link', 'Unlink', 'Image', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'TextColor', 'BGColor', 'Source', 'ShowBlocks', 'Preview']
    , ['Bold', 'Italic', 'Underline', 'Font', 'FontSize', 'Styles', 'NumberedList', 'BulletedList', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock']
];
