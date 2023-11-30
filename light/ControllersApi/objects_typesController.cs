
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_typesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage get_type()
        {
            var list = bl.objects_types.list_for_realestate();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }
    }
}
