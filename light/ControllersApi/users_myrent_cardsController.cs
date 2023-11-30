
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace admin.Controllers.api
{
    public class users_zaggy_cardsController : ApiController
    {

        [HttpPost, HttpGet]
        public HttpResponseMessage get(string user_id, string b2b_id)
        {
            var response = bl.users_zaggy_cards.get(user_id, b2b_id);

            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }


    }

}
