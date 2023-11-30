using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class languagesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list_real()
        {
            var languages = bl.languages.list_real();
            return Request.CreateResponse(HttpStatusCode.OK, languages, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list()
        {
            var languages = bl.languages.list();
            return Request.CreateResponse(HttpStatusCode.OK, languages, Configuration.Formatters.JsonFormatter);
        }
    }
}
