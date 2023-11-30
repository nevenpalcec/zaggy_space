using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class rents_prices_daysController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage generate_days(string id)
        {

            Auth.check("rents", id);

            bl.rents_prices_days.generate_days(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_day(string id, string day, string price)
        {
            Auth.check("rents", id);

            bl.rents_prices_days.save_day(id, day, price);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_day(string id)
        {
            Auth.check("rents", id);

            var lista = bl.rents_prices_days.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, lista, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage set_total_price(string id)
        {
            Auth.check("rents", id);

            bl.rents_prices_days.set_total_price(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage price_calculation(string id, string date_from, string date_until, string adults = "1", string children = "0", string pets = "0", string item_id = "-1", string b2b_id = null, string customer_id = null)
        {
            Auth.check("objects", id);

            var price_calculation = bl.objects_prices_days.price_calculation(id, date_from, date_until, adults, children, pets, item_id, b2b_id, customer_id);
            return Request.CreateResponse(HttpStatusCode.OK, price_calculation, Configuration.Formatters.JsonFormatter);
        }


    }
}
