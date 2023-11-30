 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{


    [zaggyAuth]
    public class countries_settlmentsController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list()
        {
            string json_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(json_string);
            var country_id = (string)b["country_id"];
            var place_zip = (string)b["place_zip"];
            var settlments_list = bl.countries_settlments.list(country_id, place_zip);
            return Request.CreateResponse(HttpStatusCode.OK, settlments_list, Configuration.Formatters.JsonFormatter);
        }

    }
}
