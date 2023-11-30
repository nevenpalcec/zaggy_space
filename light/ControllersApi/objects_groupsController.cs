using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_groupsController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list_active(string id)
        {
            var objects_groups = bl.objects_groups.list_active(id);
            return Request.CreateResponse(HttpStatusCode.OK, objects_groups, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var objects_groups = bl.objects_groups.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, objects_groups, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_with_prices(string id, string date, string days)
        {
            var prices = bl.objects_groups.list_with_prices(id, date, days);
            return Request.CreateResponse(HttpStatusCode.OK, prices, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage rents_count(string from, string until, string id)
        {
            var list = bl.objects_groups.rents_count_day(from, until, id);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

      
    }
}
