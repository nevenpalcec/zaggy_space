 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class payments_reciveController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list_of_payment_recive(string id)
        {
            var payment_recive = bl.payments_recive.list_rent_paid(id);
            return Request.CreateResponse(HttpStatusCode.OK, payment_recive, Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_all_payments(string user_id, string rent_id) {
            
            var payments = bl.payments_recive.list_rent(user_id,rent_id);

            return Request.CreateResponse(HttpStatusCode.OK, payments, Configuration.Formatters.JsonFormatter);

        }
    }
}
