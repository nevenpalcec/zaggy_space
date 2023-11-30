using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class registarsController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage rents_sources(string id)
        {
            var sources = bl.rents_sources.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, sources, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage rents_status(string id)
        {
            var statues = bl.rents_status.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, statues, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage payment_methods(string id)
        {
            var payment_methods = bl.payment_methods.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, payment_methods, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage countries(string id)
        {
            var countries = bl.countries.contires_list();
            return Request.CreateResponse(HttpStatusCode.OK, countries, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage currency() {
            var currencies = bl.currency.get_all();
            return Request.CreateResponse(HttpStatusCode.OK, currencies, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage rents_prices_day(string id)
        {
            var prices_day = bl.rents_prices_days.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, prices_day, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage qod()
        {
            var qod = bl.quotes_famous.get_last();
            return Request.CreateResponse(HttpStatusCode.OK, qod, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public string qod_like(string id) {
            bl.quotes_famous.like_add(id);
            var quote = bl.quotes_famous.get_last();
            return quote.Rows[0]["like"].ToString();
        }

    }
}
