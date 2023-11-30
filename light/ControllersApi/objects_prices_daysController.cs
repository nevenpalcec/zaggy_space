using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_prices_daysController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage prices_period_objects_list(string id, string date_from, string days, string object_name, string item_id = "-1", string worker_id = null, string worker_object_count = null)
        {
            Auth.check_user(id);

            var prices = bl.objects_prices_days.prices_period_objects_list_all_days(id, object_name, date_from, days, item_id, worker_id, worker_object_count);
            return Request.CreateResponse(HttpStatusCode.OK, prices, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage prices_period_liste_by_object(string id, string date_from, string date_until, string item_id = "-1")
        {
            Auth.check("objects", id);

            var prices = bl.objects_prices_days.list(id, date_from, date_until, item_id);
            return Request.CreateResponse(HttpStatusCode.OK, prices, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage price_get(string day, string object_id)
        {
            Auth.check("objects", object_id);

            var prices = bl.objects_prices_days.price_get(day, object_id, "-1");
            return Request.CreateResponse(HttpStatusCode.OK, prices, Configuration.Formatters.JsonFormatter);
        }
       

        [HttpGet, HttpPost]
        public HttpResponseMessage save_price()
        {

            string body = Request.Content.ReadAsStringAsync().Result;
            var json = bl.sys.json.obj(body);

            var id = (string)json["id"];
            var date_from = (string)json["date_from"];
            var date_until = (string)json["date_until"];
            var price = (string)json["price"];

            Auth.check("objects", id);

            bl.objects_prices_days.add_price(id, date_from, date_until, price, "-1");
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_min_stay(string id, string date_from, string date_until, string min_stay, string item_id = "-1")
        {
            Auth.check("objects", id);
            
            bl.objects_prices_days.update_min_stay(id, date_from, date_until, min_stay, item_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_enable(string id, string date_from, string date_until, string enable, string item_id = "-1")
        {
            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("enable", enable);
            f.Add("object_id", id);
            f.Add("date_from", date_from);
            f.Add("date_until", date_until);
            f.Add("item_id", item_id);

            Auth.check("objects", id);

            bl.objects_prices_days.save_enable(f);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage prices_save_extra()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            var object_id = (string)b["object_id"];

            Auth.check("objects", object_id);

            f.Add("object_id", object_id);
            f.Add("price_extra", (string)b["price_extra"]);
            f.Add("extra_from", (string)b["extra_from"]);
            f.Add("date_from", (string)b["date_from"]);
            f.Add("date_until", (string)b["date_until"]);
            f.Add("item_id", (string)b["item_id"]);
            f.Add("who", (string)b["who"]);

            bl.objects_prices_days.save_extra_price(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete_price(string id, string date_from, string date_until, string item_id = "-1")
        {
            Auth.check("objects", id);

            bl.objects_prices_days.del(id, date_from, date_until, item_id, true);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_check_in_out(string id, string date_from, string date_until, string check_in, string check_out, string days, string item_id = "-1")
        {

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("check_out", check_out);
            f.Add("check_in", check_in);
            f.Add("days", days);
            f.Add("object_id", id);
            f.Add("date_from", date_from);
            f.Add("date_until", date_until);
            f.Add("item_id", item_id);

            Auth.check("objects", id);

            bl.objects_prices_days.save_check_light(f);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage get_days_array(string id, string free = "N")
        {
            Auth.check("objects", id);
            
            var days = bl.objects_days_stocks.object_days_array(id, free);
            return Request.CreateResponse(HttpStatusCode.OK, days, Configuration.Formatters.JsonFormatter);
        }

    }
}
