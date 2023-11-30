 
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class itemsController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage list_of_items(string id)
        {
            var items = bl.items.list(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, items, Configuration.Formatters.JsonFormatter);
        }

    }

}
