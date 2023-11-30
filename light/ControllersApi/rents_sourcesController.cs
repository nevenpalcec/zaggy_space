 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class rents_sourcesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var sources = bl.rents_sources.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, sources, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id)
        {
            var source_id = bl.rents_sources.add(id);
            return Request.CreateResponse(HttpStatusCode.OK, source_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("id", (string) b["id"]);
            f.Add("email", (string) b["email"]);
            f.Add("name", (string)b["name"]);
            f.Add("b2b_id", (string)b["b2b_id"]);
            f.Add("icon", (string)b["icon"]);
            f.Add("color", (string)b["color"]);
            f.Add("password", (string)b["password"]);
            f.Add("note", (string)b["note"]);
            f.Add("payment_method_id", (string)b["payment_method_id"]);
            f.Add("rent_status_id", (string)b["rent_status_id"]);            

            bl.rents_sources.save(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage del(string id)
        {
            bl.rents_sources.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {
            var source = bl.rents_sources.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, source, Configuration.Formatters.JsonFormatter);
        }
    }
}
