using bl;
using System;
using System.Web.Mvc;


namespace wp2.Controllers
{
    public class objectsController : Controller
    {

        public ActionResult list(string date_from, string date_until, string adults = "1", string pets = "0", string children = "0", string amenity_id = "", string city_name = null, string object_type_name = null, string facilitie_name = null)
        {

            var user_id = bl.settings_current.user_id;
            adults = string.IsNullOrWhiteSpace(adults) == false ? adults : "1";
            children = string.IsNullOrWhiteSpace(children) == false ? children : "0";
            pets = string.IsNullOrWhiteSpace(pets) == false ? pets : "0";
            amenity_id = string.IsNullOrWhiteSpace(amenity_id) == false ? amenity_id : "";
            facilitie_name = string.IsNullOrWhiteSpace(facilitie_name) == false ? facilitie_name : "";
            city_name = string.IsNullOrWhiteSpace(city_name) == false ? city_name : "";
            object_type_name = string.IsNullOrWhiteSpace(object_type_name) == false ? object_type_name : "";

            ViewData["adults"] = string.IsNullOrWhiteSpace(adults) == false ? adults : "1";
            ViewData["date_from"] = date_from;
            ViewData["date_until"] = date_until;
            ViewData["children"] = string.IsNullOrWhiteSpace(children) == false ? children : "0";
            ViewData["pets"] = string.IsNullOrWhiteSpace(pets) == false ? pets : "0";
            ViewData["amenity_id"] = string.IsNullOrWhiteSpace(amenity_id) == false ? amenity_id : "";
            ViewData["facilitie_name"] = string.IsNullOrWhiteSpace(facilitie_name) == false ? facilitie_name : "";
            ViewData["city_name"] = string.IsNullOrWhiteSpace(city_name) == false ? city_name : "";
            ViewData["object_type_name"] = string.IsNullOrWhiteSpace(object_type_name) == false ? object_type_name : "";

            var json = new
            {
                user_id = user_id,
                b2b_id = bl.B2B.Web.shared.b2b_id,
                search_type = string.IsNullOrWhiteSpace(date_from) == true ? "all" : "free",
                from = date_from,
                to = date_until,
                can_sleep_max = adults,
                pets_number = pets,
                city_name = city_name,
                object_type_name = object_type_name,
                amenity_id = amenity_id,
                facilitie_name = facilitie_name
            };

            var json_object = bl.sys.json.to_object(json);
            var objects = bl.objects.free_stock(json_object);

            // if we have only one property, just show him
            if (bl.settings_current.registars.objects_active.Rows.Count == 1)
            {
                var object_id = bl.settings_current.registars.objects_active.Rows[0]["id"].ToString();
                ViewData["object_id"] = object_id;
                ViewData["objects"] = objects;

                return Redirect($"/objects/property/?id={object_id}&date_from={date_from}&date_until={date_until}&adults={adults}");

            }
            else
            {
                ViewData["objects"] = objects;
                return View();
            }
        }

        public ActionResult filter(string guid)
        {

            return View();
        }

        public ActionResult property(string id, string date_from, string date_until, string adults = "1", string pets = "0", string promo_code = "")
        {
            if (id.is_null())
            {
                return Redirect("/");
            }

            var object_exists = bl.objects.exist(id);

            if (object_exists == false)
            {
                return Redirect("/");
            }

            if (date_from.is_iso_date() && date_until.is_iso_date() && DateTime.Parse(date_until) < DateTime.Parse(date_from))
            {
                var date_temp = date_from;
                date_from = date_until;
                date_until = date_temp;
            }

            if (date_from == date_until)
            {
                date_until = null;
            }

            ViewData["object_id"] = id;
            ViewData["date_from"] = string.IsNullOrWhiteSpace(date_from) == false ? date_from : System.DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            ViewData["date_until"] = string.IsNullOrWhiteSpace(date_until) == false ? date_until : System.DateTime.Now.AddDays(3).ToString("yyyy-MM-dd");
            ViewData["adults"] = string.IsNullOrWhiteSpace(adults) == false ? adults : "1";
            ViewData["pets"] = string.IsNullOrWhiteSpace(pets) == false ? pets : "0";
            ViewData["promo_code"] = string.IsNullOrWhiteSpace(promo_code) == false ? promo_code : "";

            return View();
        }

        public ActionResult hotel() {
            var user_id = bl.settings_current.user_id;
            var groups = bl.objects_groups.get_groups_count(user_id);
            if(int.Parse(groups) > 0)
            {
                return View();
            }
            else
            {
                return RedirectToAction("index", "home");

            }
        }

        public ActionResult h(string id)
        {
            return View();
        }

        // /objects/payment
        public ActionResult payment(string id, string date_from, string date_until, string item_id = "-1", string persons = "1", string children = "0", string pets = "0", string promo_code = null)
        {
            ViewData["object_id"] = id;
            ViewData["date_from"] = date_from;
            ViewData["date_until"] = date_until;
            ViewData["persons"] = persons;
            ViewData["pets"] = pets;
            ViewData["item_id"] = item_id;
            ViewData["promo_code"] = promo_code;
            ViewData["children"] = children;
            return View();
        }

        /// <summary>
        /// add reservation to zaggy
        /// </summary>
        /// <param name="f"></param>
        public void rent_add(FormCollection f)
        {

            var object_id = f.Get("object_id");
            var user_id = bl.objects.get_user_id(object_id);
            var date_from = f.Get("from_date");
            var date_until = f.Get("until_date");
            var item_id = f.Get("item_id");
            var adults = f.Get("adults");
            var childern = string.IsNullOrWhiteSpace(f.Get("childern")) == true ? "0" : f.Get("childern");
            var pets = string.IsNullOrWhiteSpace(f.Get("childern")) == true ? "0" : f.Get("childern");
            var b2b_id = f.Get("b2b_id");
            var rent_status_id = f.Get("rent_status_id");
            var item_name = f.Get("item_name");
            var promo_code = f.Get("promo_code");
            var insurance_price_enable = f.Get("insurance_price_enable");
            var insurance_price = f.Get("insurance_price");

            if (insurance_price_enable == "N")
            {
                insurance_price = "0";
            }

            var date_from_d = System.DateTime.Parse(date_from);
            var date_until_d = System.DateTime.Parse(date_until);
            var days = (date_until_d - date_from_d).TotalDays;

            var contact_name = f.Get("contact_name_first") + " " + f.Get("contact_name_last");
            var contact_tel = f.Get("contact_tel");
            var contact_email = f.Get("contact_email");
            var contact_country_id = f.Get("contact_country_id");
            var contact_city_zip = f.Get("contact_city_zip");
            var contact_city = f.Get("contact_city");
            var contact_adress = f.Get("contact_adress");
            var note = f.Get("note");

            var payment_method_id = f.Get("payment_method_type") == "card" ? bl.payment_methods.get_by_type_id(user_id, bl.payment_methods.type.card) : bl.payment_methods.get_by_type_id(user_id, bl.payment_methods.type.bank);
            var rent_source_id = bl.rents_sources.get_id_web(user_id);

            // If any parameter missing, do not proceed
            if (contact_name.is_null())
            {
                Response.Redirect(Request.UrlReferrer.ToString());
                return;
            }

            // generate json
            var j = new
            {
                date_from = date_from,
                object_id = object_id,
                user_id = user_id,
                date_until = date_until,
                item_id = item_id,
                adults = adults,
                childern = childern,
                pets = pets,
                b2b_id = b2b_id,
                payment_method_id = payment_method_id,
                rent_status_id = rent_status_id,
                rent_source_id = rent_source_id,
                contact_name = contact_name,
                contact_tel = contact_tel,
                contact_email = contact_email,
                contact_country_id = contact_country_id,
                contact_city_zip = contact_city_zip,
                contact_city = contact_city,
                contact_adress = contact_adress,
                note = note,
                promo_code = promo_code,
                item_name = item_name,
                insurance_price = insurance_price,
                sync_wait = "Y"
            };

            var json = bl.sys.json.to_string(j);

            var rent_id = bl.rents.add_auto(json, bl.rents_log.change_by.guest);

            rent_source_id = "112036";
            bl.rents.save_rent_soruce_id(rent_id, rent_source_id);

            rent_status_id = "101072";
            bl.B2B.Zagreb.rents.save_rent_status_id(rent_id, rent_status_id);

            // set price to 0
            bl.rents.save_rent_price(rent_id, "0");

            // send email to the users
            // bl.rents.email_new_rent_async(rent_id);

            if (f.Get("payment_method_type") == "card")
            {

                string pg = bl.settings.get(bl.settings.set.payment_getway, bl.settings_current.user_id);

                if (pg.is_null() == true)
                {
                    Response.Redirect("/rents/confirm/" + rent_id);

                }
                else if (pg == "wspay")
                {

                    var url = Request.UrlReferrer.ToString();
                    var uri = new Uri(url);
                    string requested = uri.Scheme + Uri.SchemeDelimiter + uri.Host + ":" + uri.Port;

                    var url_success = requested + "/rents/confirm/" + rent_id;
                    var url_error = requested + "/rents/confirm/" + rent_id + "&error=1";
                    var url_new = $@"https://api.zaggy.net/rents/pay/{rent_id}?what=advance&url_success=" + url_success + "&url_error=" + url_error;

                    Response.Redirect(url_new);

                }
                else if (pg == "stripe")
                {

                    var url_success = Request.Url.Scheme + "://" + Request.Url.Authority + "/rents/confirm/" + rent_id;
                    var url_cancel = Request.UrlReferrer.ToString();
                    var url = bl.B2B.Stripe.payment.url_rent_advance(rent_id, url_success, url_cancel);
                    Response.Redirect(url);

                }
                else if (pg == "revolut")
                {
                    var url = bl.B2B.Revolut.rents.url(rent_id, "advance", null, null);
                    Response.Redirect(url);

                }
                else
                {
                    Response.Redirect("/rents/confirm/" + rent_id);
                }

            }

            else
            {
                Response.Redirect("/rents/confirm/" + rent_id);
            }

        }

    }

}