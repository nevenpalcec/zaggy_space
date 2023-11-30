using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class payment_methodsController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage lists(string id)
        {
            var payment_methods = bl.payment_methods.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, payment_methods, Configuration.Formatters.JsonFormatter);
        }
    }
}
