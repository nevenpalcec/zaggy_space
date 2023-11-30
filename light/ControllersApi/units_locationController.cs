 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class units_locationController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {

            Auth.check("users", id);

            var list = bl.units.unit_list(id);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);

        }

    }

}
