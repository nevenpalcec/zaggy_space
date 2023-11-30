 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class units_ibanController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();
            
            f.Add("id", (string)b["id"]);
            f.Add("name", (string)b["name"]);
            f.Add("recipient", (string)b["recipient"]);
            f.Add("iban", (string)b["iban"]);
            f.Add("swift", (string)b["swift"]);
            f.Add("adress", (string)b["adress"]);
            f.Add("country", (string)b["country"]);
            f.Add("city", (string)b["city"]);
            
            bl.units_iban.save(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        public HttpResponseMessage get(string id)
        {
            var units_iban = bl.units_iban.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, units_iban, Configuration.Formatters.JsonFormatter);
        }
    }
}
