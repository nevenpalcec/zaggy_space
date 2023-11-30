 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class workers_objectsController : ApiController
    {
        [HttpGet,HttpPost]
        public HttpResponseMessage list(string id)
        {
            var list = bl.workers_objects.full_list(id);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

    }
}
