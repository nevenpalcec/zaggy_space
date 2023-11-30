
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace admin.Controllers.api
{
    public class users_zaggy_contractController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {
            try
            {
                var response = bl.users_zaggy_contract.get(id);
                return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message, Configuration.Formatters.JsonFormatter);
            }
        }

        [HttpPost]
        public HttpResponseMessage sign_contract()
        {
            var json = Request.Content.ReadAsStringAsync().Result;

            bl.users_zaggy_contract.sign_contract(json);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }


    }

}
