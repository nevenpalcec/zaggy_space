using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_rooms_typesController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage list_room_types() {
            var list = bl.objects_rooms_types.list();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

    }
}
