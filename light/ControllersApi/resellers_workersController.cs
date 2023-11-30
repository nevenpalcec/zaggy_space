
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class resellers_workersController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {
            var res = bl.resellers_workers.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, res, Configuration.Formatters.JsonFormatter);
        }

    }
}
