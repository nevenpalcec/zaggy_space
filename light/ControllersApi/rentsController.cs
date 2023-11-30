
using bl;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class rentsController : ApiController
    {


        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {
            Auth.check("rents", id);

            var rent = bl.rents.get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rent, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_by_object(string id, string date_from, string date_until)
        {

            Auth.check("objects", id);

            var rent = bl.rents.list_object(id, date_from, date_until);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rent, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage rent_to_invoice() {

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var rent_id = (string) b["rent_id"];
            var user_id = (string) b["user_id"];
            var worker_id = (string) b["worker_id"];
            var type = (string) b["type"];

            Auth.check("rents", rent_id);

            bl.invoices.type invoice_type = bl.invoices.type.invoice;

            if (b["invoice_type"] == 0) {
                invoice_type = bl.invoices.type.invoice;
            } else if (b["invoice_type"] == 1) {
                invoice_type = bl.invoices.type.inv_advance;
            }


            object invoice_id = bl.rents.toInvoice_full(rent_id, user_id, invoice_type, worker_id, type);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, invoice_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage price_save()
        {
            var json_string = Request.Content.ReadAsStringAsync().Result;

            var j = json_string.obj();

            var rent_id = (string)j["rents"]["rent_id"];

            Auth.check("rents", rent_id);

            bl.rents.price_save_json(json_string);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        public HttpResponseMessage save()
        {
            var json_string = Request.Content.ReadAsStringAsync().Result;

            var j = json_string.obj();

            var rent_id = (string)j["rent_id"];

            Auth.check("rents", rent_id);

            bl.rents.save_json(json_string);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        public HttpResponseMessage add()
        {            
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var object_id = (string)b["object_id"];

            Auth.check("objects", object_id);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("from_date", (string)b["date_from"]);
            f.Add("until_date", (string)b["date_until"]);
            f.Add("rent_source_id", (string)b["rent_source_id"]);
            f.Add("rent_status_id", (string)b["rent_status_id"]);
            f.Add("object_id", object_id);
            f.Add("currency_id", (string)b["currency_id"]);

            f.Add("price", (string) b["price"]);
            f.Add("discount", (string) b["discount"]);

            f.Add("contact_name", (string)b["contact_name"]);
            f.Add("contact_email", (string)b["contact_email"]);
            f.Add("contact_tel", (string)b["contact_tel"]);
            f.Add("contact_country_id", (string)b["contact_country_id"]);

            f.Add("note_owner", (string)b["note_owner"]);
            //price note
            f.Add("note", (string)b["note"]);
            f.Add("note_short", (string) b["note_short"]);

            f.Add("note_user", (string) b["note_user"]);

            f.Add("children", (string)b["children"]);
            f.Add("pets", (string)b["pets"]);
            f.Add("adults", (string)b["adults"]);

            var rent_id = bl.rents.add(f, bl.rents_log.change_by.worker);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, int.Parse(rent_id), Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage del(string id, string worker_id)
        {
            Auth.check("rents", id);

            bl.rents.del(id, worker_id, bl.rents_log.change_by.worker, true, false);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            Auth.check_user(id);

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var date_from = (string)b["date_from"];
            var days = (string)b["days"];  
            var filter_id = (string)b["filter_id"];
            var objs = (string)b["objs"]; ;
            var rent_statuses = (string)b["rent_statuses"];
            var rent_soruces = (string)b["rent_soruces"];
            var active = (string)b["active"];
            var search = (string)b["search"];

            var rents = bl.rents.rents_list_active_simple2(id, date_from, days, objs, filter_id, rent_statuses, rent_soruces, active, search, "order by rents.from_date asc");
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_filtered() {

            string body_string = Request.Content.ReadAsStringAsync().Result;

            var b = body_string.json();

            var user_id = (string)b["user_id"];

            Auth.check_user(user_id);

            var rents = bl.rents.get_json(body_string);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_characters(string user_id)
        {
            Auth.check_user(user_id);

            var characters = bl.rents.special_characters();
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, characters, Configuration.Formatters.JsonFormatter);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage search(string id, string search, string active = "Y")
        {

            Auth.check_user(id);

            var rents = bl.rents.search(search, id, active);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_calendar(string id, string date_from, string days, string objs)
        {
            Auth.check_user(id);

            var rents = bl.rents.list_all(id, date_from, days, objs);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_arrivals(string day, string user_id)
        {
            Auth.check_user(user_id);

            var rents = bl.rents.rents_comming(user_id,  day);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_departures(string user_id, string day)
        {
            Auth.check_user(user_id);

            var rents = bl.rents.rents_leaving(user_id, day);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage statistics(int year, string user_id, string object_id, string unit_id)
        {
            Auth.check_user(user_id);

            var rents = bl.rents.rents_statistics_in_year(year, user_id, object_id, unit_id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage statistics_by_countries(string year, string user_id, string object_id)
        {
            Auth.check_user(user_id);

            var rents = bl.rents.rents_statistics_rents_by_countries(year, user_id, object_id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage statistics_by_channels(string year, string user_id, string object_id, string unit_id)
        {
            Auth.check_user(user_id);

            var rents = bl.rents.rents_statistics_by_chanells(year, user_id, object_id, unit_id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage statistics_by_month(int year, int month, string user_id, string object_id, string unit_id)
        {
            Auth.check_user(user_id);

            var rents = bl.rents.rents_statistics_in_month(year, month, user_id, object_id, unit_id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage prices_save_json()
        {
            var json_string = Request.Content.ReadAsStringAsync().Result;

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage rents_by_object(string id)
        {
            Auth.check("objects", id);

            var rents = bl.rents.rents_calendar_list(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, rents, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage send_email_confirmation(string id)
        {
            Auth.check("rents", id);

            bl.rents.mail_send(id, "", bl.rents.mail_type.confirmation);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage send_email_voucher(string id)
        {
            Auth.check("rents", id);

            bl.rents.mail_send(id, "", bl.rents.mail_type.vaucher);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage send_email_offer(string id)
        {
            Auth.check("rents", id);

            bl.rents.mail_send(id, "", bl.rents.mail_type.offer);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage generate_confirmation(string id)
        {
            Auth.check("rents", id);

            var eamil = bl.rents.mail_generate(id, "" , bl.rents.mail_type.confirmation);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, eamil, Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage generate_offer(string id)
        {
            Auth.check("rents", id);

            var email = bl.rents.mail_generate(id, "", bl.rents.mail_type.offer);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, email, Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage generate_vaucher(string id)
        {
            Auth.check("rents", id);

            bl.rents.mail_generate(id, "", bl.rents.mail_type.vaucher);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet]
        public HttpResponseMessage rents_move(string id, string date_from, string object_id)
        {
            Auth.check("rents", id);

            bl.rents.rent_move(id, date_from, object_id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }
    }
}
