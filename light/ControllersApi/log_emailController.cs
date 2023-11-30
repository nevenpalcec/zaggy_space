using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class log_emailController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list_of_emails(string id)
        {
            var email = bl.log_email.log_email_get(id);
            return Request.CreateResponse(HttpStatusCode.OK, email, Configuration.Formatters.JsonFormatter);
        }

    }
}
