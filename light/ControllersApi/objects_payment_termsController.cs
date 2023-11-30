 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_payment_termsController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage list(string id)
        {
            var terms = bl.objects_payment_terms.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, terms, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage add(string id)
        {
            var terms_id = bl.objects_payment_terms.add(id);
            return Request.CreateResponse(HttpStatusCode.OK, terms_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        public HttpResponseMessage save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("objects_payment_terms_id", (string)b["objects_payment_terms_id"]);
            f.Add("payment_terms_when", (string)b["when"]);
            f.Add("payment_terms_how", (string)b["how"]);
            f.Add("amount", (string)b["amount"]);
            f.Add("days", (string)b["days"]);
            
            bl.objects_payment_terms.save(f);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet]
        public HttpResponseMessage delete(string id)
        {
            bl.objects_payment_terms.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }
    }
}
