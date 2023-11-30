using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class rent_doors_locksController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list_doors(string id)
        {
            var rent_doors_lock = bl.rents_doors_locks.list_by_rent(id);
            return Request.CreateResponse(HttpStatusCode.OK, rent_doors_lock, Configuration.Formatters.JsonFormatter);
        }
    }
}
