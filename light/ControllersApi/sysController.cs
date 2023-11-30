 
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class sysController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage test()
        {
            var a = this.RequestContext.IsLocal;
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }
    }
}
