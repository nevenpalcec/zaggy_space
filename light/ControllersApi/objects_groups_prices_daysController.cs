 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_groups_prices_daysController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage save_price()
        {
            string body = Request.Content.ReadAsStringAsync().Result;
            var json = bl.sys.json.obj(body);
            var id = (string)json["id"];
            var date_from = (string)json["date_from"];
            var date_until = (string)json["date_until"];
            var price = (string)json["price"];
            bl.objects_groups_prices_days.save_price(id, date_from, date_until, price, "-1");
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }


        [HttpGet, HttpPost]
        public HttpResponseMessage save_enable(string id, string date_from, string date_until, string enable, string item_id = "-1")
        {
            bl.objects_groups_prices_days.save_enable(id, date_from, date_until, enable, item_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_min_stay(string id, string date_from, string date_until, string min_stay, string item_id = "-1")
        {
            bl.objects_groups_prices_days.save_min_stay(id, date_from, date_until, min_stay, item_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_check_in_out(string id, string date_from, string date_until, string check_in, string check_out, string item_id = "-1")
        {
            bl.objects_groups_prices_days.save_check_in_out(id, date_from, date_until, check_in, check_out, item_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_(string id, string date_from, string date_until, string stock, string item_id = "-1")
        {
            bl.objects_groups_prices_days.save_stock(id, date_from, date_until, stock, item_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete_price(string id, string date_from, string date_until, string item_id = "-1")
        {

            bl.objects_groups_prices_days.del(id, date_from, date_until, item_id, true);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }
    }
}
