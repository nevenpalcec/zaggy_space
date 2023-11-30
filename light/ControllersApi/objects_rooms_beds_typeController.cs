 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_rooms_beds_typeController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list_basic()
        {
            var list = bl.objects_rooms_beds_type.list();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }
    }
}
