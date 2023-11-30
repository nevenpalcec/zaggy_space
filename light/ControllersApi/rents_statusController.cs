 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class rents_statusController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var status_list = bl.rents_status.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, status_list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id)
        {
            var status_id = bl.rents_status.add(id);
            return Request.CreateResponse(HttpStatusCode.OK, status_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("name", (string)b["name"]);
            f.Add("color", (string)b["color"]);
            f.Add("password", (string)b["password"]);
            f.Add("payment_method_id", (string)b["payment_method_id"]);
            f.Add("id", (string)b["id"]);

            bl.rents_status.save(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {
            var status = bl.rents_status.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, status, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet,HttpPost]
        public HttpResponseMessage delete(string id)
        {
            bl.rents_status.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }
    }
}
