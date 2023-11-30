
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace admin.Controllers.api
{
    public class users_zaggy_contract_typesController : ApiController
    {

        [HttpPost, HttpGet]
        public HttpResponseMessage get_contract_body()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var zaggy_company_id = (string)b["zaggy_company_id"];
            var user_id = (string)b["user_id"];
            var language_id = (string)b["language_id"];

            var response = bl.users_zaggy_contract_types.get_contract_body(zaggy_company_id, user_id, language_id);

            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }


    }

}
