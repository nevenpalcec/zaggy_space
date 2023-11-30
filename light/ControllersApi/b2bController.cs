using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]
    public class b2bController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage list(string type) 
        {
            var b2b = bl.b2b.list_by_type_synced(type);
            return Request.CreateResponse(HttpStatusCode.OK, b2b, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_sort() 
        {
            var b2b = bl.b2b.b2b_list();
            return Request.CreateResponse(HttpStatusCode.OK, b2b, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage revolut_transaction_add(string id, string erp_id, string note)
        {
            Auth.check("users", id);
            var transaction_id = bl.B2B.Revolut.orders.transaction_add_by_erp_id(id, erp_id, note, true);
            return Request.CreateResponse(HttpStatusCode.OK, transaction_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage airbnb_listings_list(string id) 
        {
            Auth.check("objects", id);

            var response = bl.B2B.AirBNB.listings.list_by_default(id);
            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage airbnb_get_all_settings(string id) 
        {
            Auth.check("objects", id);

            bl.B2B.AirBNB.shared.get_all_settings(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage airbnb_set_listing_details(string id) 
        {
            Auth.check("objects", id);

            bl.B2B.AirBNB.listings.put(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage airbnb_availability_settings_set(string id) 
        {
            Auth.check("objects", id);

            var response = bl.B2B.AirBNB.availability_rules.put(id);
            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage airbnb_booking_settings_set(string id) 
        {
            Auth.check("objects", id);

            var response = bl.B2B.AirBNB.booking_settings.set(id);
            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage airbnb_price_settings_set(string id) 
        {            
            Auth.check("objects", id);

            var response = bl.B2B.AirBNB.pricing_settings.put(id);
            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }
    }
}
