
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace admin.Controllers.api
{
    public class users_zaggy_contract_itemsController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            try
            {
                var response = bl.users_zaggy_contract_items.list(id);
                return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message, Configuration.Formatters.JsonFormatter);
            }
        }


        [HttpPost]
        public HttpResponseMessage add()
        {
            try
            {
                var body = this.Request.Content.ReadAsStringAsync().Result;

                bl.users_zaggy_contract_items.item_add(body);

                return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message, Configuration.Formatters.JsonFormatter);
            }
        }

        [HttpGet]
        public HttpResponseMessage del(string id)
        {
            try
            {
                bl.users_zaggy_contract_items.item_delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message, Configuration.Formatters.JsonFormatter);
            }
        }

    }

}
