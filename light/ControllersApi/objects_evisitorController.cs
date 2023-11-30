 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_evisitorController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("eVizitor_object", (string)b["eVizitor_object"]);
            f.Add("checkin", (string)b["checkin"]);
            f.Add("checkout", (string)b["checkout"]);
            f.Add("enable", (string)b["enable"]);
            f.Add("object_id", (string)b["object_id"]);
            f.Add("eVizitor_id", (string)b["user_b2b_id"]);
           
            bl.objects_evisitor.save(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet]
        public HttpResponseMessage get(string id)
        {
            var evisitor = bl.objects_evisitor.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, evisitor, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage list(string id)
        {
            var evisitor = bl.objects_evisitor.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, evisitor, Configuration.Formatters.JsonFormatter);
        }
    }
}
