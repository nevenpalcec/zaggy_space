
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace admin.Controllers.api
{
    public class zaggy_companiesController : ApiController
    {

        [HttpPost, HttpGet]
        public HttpResponseMessage get(string id)
        {
            var response = bl.zaggy_companies.get_by_user(id);

            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }


    }

}
