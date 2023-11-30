using bl;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]
    public class invoices_itemsController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {

            Auth.check("invoices_header", id);

            var items = bl.invoices_items.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, items, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        public HttpResponseMessage save()
        {

            var body = Request.Content.ReadAsStringAsync().Result;

            var j = body.obj();
            var id = (string)j["id"];

            Auth.check("invoices_items", id);

            bl.invoices_items.save_json(body);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

    }

}
