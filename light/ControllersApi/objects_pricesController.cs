
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_pricesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage add(string id)
        {
            Auth.check("objects", id);

            bl.objects_prices.add(id, "-1");
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }
    }
}
