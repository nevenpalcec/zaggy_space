// fornt ent javascript class
var fr = new function () {

    // test function to see if all it's working
    this.test = () => {
        alert('fr tets: OK!');
    }

    // dropdow menu => handle
    this.dropdown = function (dropdown_id) {

        //debugger;

        var dropdown_container = document.getElementById(dropdown_id);

        var dropdown_input = dropdown_container.getElementsByClassName('dropdown-toggle')[0];
        var dropdown_list = dropdown_container.getElementsByClassName('dropdown-menu')[0]
        var dropdown_selected_option = dropdown_input.dataset.id;

        for (let i = 0; i < dropdown_list.children.length; i++) {

            // add on clicke even
            dropdown_list.children[i].onclick = function (e) {
                dropdown_input.innerHTML = e.currentTarget.innerHTML;
                dropdown_input.dataset.id = e.currentTarget.dataset.id;
            }

        }

        // set 
        if (!dropdown_selected_option == false) {
            var items = document.querySelectorAll('#dropdown_amenity .dropdown-item');
            for (var i of items) {
                if (i.dataset.id == dropdown_selected_option) {
                    dropdown_input.innerHTML = i.innerHTML;
                    break;
                }
            }
        }

        dropdown_input.onclick = function () {

        };

        dropdown_input.onkeyup = function (e) {

        };

        dropdown_input.onfocus = function (e) {

        }

        $(document).mouseup(function (e) {

        });

    }

    this.dropdown_select = function (dropdown_id) {

        debugger;

        var dropdown_container = document.getElementById(dropdown_id)
        var dropdown_list = dropdown_container.getElementsByClassName("dropdown-item");
        var dropdown_input = dropdown_container.getElementsByClassName('dropdown-toggle')[0];

        for (let i = 0; i < dropdown_list.length; i++) {

            dropdown_list[i].onclick = function (e) {
                debugger;
                dropdown_input.innerHTML = e.currentTarget.innerHTML;
            }

        }

    }

    // img lazy load
    this.img_lazy_load = function () {

        // how to use
        // put class lazy in img
        // example: <img src="blank.gif" data-src="my_image.png" class="lazy" width="600" height="400" >`

        // run after window.load  
        // window.onload = function () { fr.img_lazy_load(); }

        var $q = function (q, res) {
            if (document.querySelectorAll) {
                res = document.querySelectorAll(q);
            } else {
                var d = document
                    , a = d.styleSheets[0] || d.createStyleSheet();
                a.addRule(q, 'f:b');
                for (var l = d.all, b = 0, c = [], f = l.length; b < f; b++)
                    l[b].currentStyle.f && c.push(l[b]);

                a.removeRule(0);
                res = c;
            }
            return res;
        }
            , addEventListener = function (evt, fn) {
                window.addEventListener
                    ? this.addEventListener(evt, fn, false)
                    : (window.attachEvent)
                        ? this.attachEvent('on' + evt, fn)
                        : this['on' + evt] = fn;
            }
            , _has = function (obj, key) {
                return Object.prototype.hasOwnProperty.call(obj, key);
            }
            ;

        function loadImage(el, fn) {

            // NEW
            if (el.getAttribute('data-src') === el.getAttribute('src')) {
                return;
            }

            let img = new Image();
            let src = el.getAttribute('data-src');
            let delay_ms = 150;

            img.onload = async function () {

                // OLD
                //if (!!el.parent)
                //    el.parent.replaceChild(img, el)
                //else
                //    el.src = src;

                // NEW
                if (!!el.parent) {

                    // Slowly hide image
                    await $(el).animate({ opacity: '0' }, delay_ms).promise();
                    img.style.opacity = '0';

                    // Change image
                    el.parent.replaceChild(img, el);

                    // Slowly show image
                    await $(img).animate({ opacity: '1' }, delay_ms + 100).promise();
                }
                else {

                    // Slowly hide image
                    await $(el).animate({ opacity: '0' }, delay_ms).promise();

                    // Change image
                    el.src = src;

                    // Slowly show image
                    await $(el).animate({ opacity: '1' }, delay_ms + 100).promise();
                }

                fn ? fn() : null;
            }
            img.src = src;
        }

        function elementInViewport(el) {
            var rect = el.getBoundingClientRect()

            return (
                rect.top >= 0
                && rect.left >= 0
                && rect.top <= (window.innerHeight || document.documentElement.clientHeight)
            )
        }

        var images = new Array()
            , query = $q('img.lazy')
            , processScroll = function () {
                for (var i = 0; i < images.length; i++) {
                    if (elementInViewport(images[i])) {
                        loadImage(images[i], function () {
                            images.splice(i, i);
                        });
                    }
                };
            };

        // Array.prototype.slice.call is not callable under our lovely IE8 
        for (var i = 0; i < query.length; i++) {
            images.push(query[i]);
        };

        processScroll();
        addEventListener('scroll', processScroll);


    }

};