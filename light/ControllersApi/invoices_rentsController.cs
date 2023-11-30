using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class invoices_rentsController : ApiController {
        [HttpGet, HttpPost]
        public HttpResponseMessage get_by_invoice_id(string invoice_id) {

            //var items = bl.invoices_items.get_by_invoice_id(invoice_id);

            return Request.CreateResponse(HttpStatusCode.OK, "OK", Configuration.Formatters.JsonFormatter);
        }
    }
}