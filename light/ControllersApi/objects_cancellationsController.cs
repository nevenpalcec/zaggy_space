 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_cancellationsController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {

            Auth.check("objects", id);

            var cancellation_list = bl.objects_cancellations.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, cancellation_list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id)
        {
            Auth.check("objects", id);

            var canc_id = bl.objects_cancellations.add(id);
            return Request.CreateResponse(HttpStatusCode.OK, canc_id, Configuration.Formatters.JsonFormatter);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("from", (string)b["from"]);
            f.Add("until", (string)b["until"]);
            f.Add("percent", (string)b["percent"]);
            f.Add("objects_cancellations_id", (string)b["objects_cancellations_id"]);

            bl.objects_cancellations.save(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet]
        public HttpResponseMessage delete(string id)
        {
            bl.objects_cancellations.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }
    }
}
