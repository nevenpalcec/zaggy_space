using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]
    public class cancellation_policiesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage get_cancellation_polices(string id)
        {
            var cancellation_policies = bl.cancellation_policies.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, cancellation_policies, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage cancellations_add(string user_id) 
        {
            
            var id = bl.cancellation_policies.add(user_id);
            return Request.CreateResponse(HttpStatusCode.OK, id, Configuration.Formatters.JsonFormatter);
        }        

        [HttpGet, HttpPost]
        public HttpResponseMessage cancellations_save()
        {

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);
            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("cancellation_id", (string) b["cancellation_id"]);
            f.Add("name", (string) b["name"]);
            f.Add("note", (string) b["note"]);            
            
            bl.cancellation_policies.save(f);
            return Request.CreateResponse(HttpStatusCode.OK, "OK", Configuration.Formatters.JsonFormatter);
        }
    }
}
