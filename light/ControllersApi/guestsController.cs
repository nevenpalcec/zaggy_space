using bl;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class guestsController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage get_guest_by_user_id(string id)
        {
            
            Auth.check_user(id);

            var guests = bl.guests.get_by_user_id(id);
            return Request.CreateResponse(HttpStatusCode.OK, guests, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add_guest(string json)
        {

            var r = json.obj();

            var rent_id = (string)r["rent_id"];

            Auth.check("rents", rent_id);

            bl.guests.add_guest(json, null);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_guests_filtered() {

            string body_string = Request.Content.ReadAsStringAsync().Result;

            var b = body_string.json();

            var user_id = (string)b["user_id"];

            Auth.check_user(user_id);
           
            var guests = bl.guests.get_json(body_string);

            return Request.CreateResponse(HttpStatusCode.OK, guests, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_guest()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f_evisitor = new System.Collections.Specialized.NameValueCollection();
            var f_guest= new System.Collections.Specialized.NameValueCollection();

            var guest_id = (string)b["guest_id"];

            Auth.check("guests", guest_id);

            f_evisitor.Add("guest_id", guest_id);
            f_evisitor.Add("document_type", (string)b["document_type"]);
            f_evisitor.Add("document_number", (string)b["document_number"]);
            f_evisitor.Add("gender", (string)b["gender"]);
            f_evisitor.Add("residence_country", (string)b["residence_country"]);
            f_evisitor.Add("residence_city", (string)b["residence_city"]);
            f_evisitor.Add("birth_country", (string)b["birth_country"]);
            f_evisitor.Add("citizenship", (string)b["citizenship"]);
            f_evisitor.Add("birth_city", (string)b["birth_city"]);
            f_evisitor.Add("arrival_organisation", (string)b["arrival_organisation"]);
            f_evisitor.Add("offered_service_type", (string)b["offered_service_type"]);
            f_evisitor.Add("birth_date_date", (string)b["birth_date"]);
            f_evisitor.Add("birth_date", (string)b["birth_date"]);
            f_evisitor.Add("tt_payment_category", (string)b["tt_payment_category"]);

            f_guest.Add(("id"), (string)b["guest_id"]);
            f_guest.Add("name_last", (string)b["name_last"]);
            f_guest.Add("name_first", (string)b["name_first"]);
            f_guest.Add("note", (string)b["note"]);
            f_guest.Add("date_from", (string)b["date_from"]);
            f_guest.Add("date_until", (string)b["date_until"]);
            f_guest.Add("visible_on_invoice", (string)b["visible_on_invoice"]);
            f_guest.Add("city_tax", (string)b["city_tax"]);

            bl.guest_evisitor.save(f_evisitor);
            bl.guests.save(f_guest);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id)
        {
            Auth.check("rents", id);

            var guest_id = bl.guests.add(id);
            return Request.CreateResponse(HttpStatusCode.OK, guest_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add_evisitor(string id)
        {
            var evisitor_id = bl.guest_evisitor.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, evisitor_id, Configuration.Formatters.JsonFormatter);
        }

        [System.Obsolete]
        public HttpResponseMessage save_checkin(string id)
        {
            bl.guest_evisitor.check_in(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_guest_by_rent_id(string id) 
        {
            Auth.check("rents", id);

            var guest_list = bl.guests.list_rent(id);
            return Request.CreateResponse(HttpStatusCode.OK, guest_list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_guest_by_guest_id(string id)
        {
            Auth.check("guests", id);

            var guest = bl.guests.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, guest, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete(string id) {
            
            var rent_id = bl.guests.get_rent_id(id);

            Auth.check("rents", rent_id);

            bl.guests.del(id);
            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage search_limit_10_json()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;

            var b = bl.sys.json.obj(body_string);

            var id = (string)b["id"];
            var name_first = (string)b["name_first"];
            var name_last = (string)b["name_last"];
            var document_number = (string)b["document_number"];

            Auth.check_user(id);

            var guests_search = bl.guests.search_limit_10(id, name_first, name_last, document_number);
            return Request.CreateResponse(HttpStatusCode.OK, guests_search, Configuration.Formatters.JsonFormatter);
        }
    }
}
