
function init_dropdown_select(dropdown_id) {

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
        } else {
            $(".select_ul").parents(".select_wrap").removeClass("active");
        }
    });
}