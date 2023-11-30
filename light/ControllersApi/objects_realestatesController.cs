
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_realestatesController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage save_json() 
        {
            string json_string = Request.Content.ReadAsStringAsync().Result;
            bl.objects_relstate.save_json(json_string, Settings.app_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

    }
}
