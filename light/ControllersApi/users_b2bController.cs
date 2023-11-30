 
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class users_b2bController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage list(string id)
        {
            var list = bl.users_b2b.get(id, "1");
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        public HttpResponseMessage save()
        {

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("username", (string)b["username"]);
            f.Add("password", (string)b["password"]);
            f.Add("link1", (string)b["link1"]);
            f.Add("user_b2b_id", (string)b["user_b2b_id"]);
            f.Add("enable", (string)b["enable"]);

            bl.users_b2b.save_with_enable(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage delete(string id)
        {
            bl.user_b2b.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage delete_tan_file(string id)
        {
            bl.user_b2b.delete_evistor_amazon_file(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public void upload_tan_file(string user_b2b_id)
        {
            var file = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[0]);

            bl.user_b2b.upload_evisitor_file(file, user_b2b_id, bl.settings_current.user_id);

        }

        [HttpGet]
        public HttpResponseMessage add_evisitor(string id)
        {
            bl.user_b2b.add_b2b_user(id, bl.user_b2b.b2b_type.eVizitor);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

    }

}
