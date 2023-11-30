using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    //[MyRentAuth]

    public class objects_foodController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list()
        {
            var list = bl.objects_food.list();
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }
    }
}