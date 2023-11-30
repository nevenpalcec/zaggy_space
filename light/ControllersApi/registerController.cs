using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    public class registerController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage add_user()
        {

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();
            //string name = b["name"];

            f.Add("name", (string) b["name"]);
            f.Add("username", (string) b["username"]);
            f.Add("email", (string) b["email"]);
            f.Add("tel", (string) b["tel"]);
            f.Add("reseller_id", "1");
            f.Add("syncbeds", "Y");

            //sql = sql.Replace("@reseller_id", db.format.db_string_null(f.Get("reseller_id") == "" ? "1" : f.Get("reseller_id")));

            var user_id = bl.users.add(f);

            return Request.CreateResponse(HttpStatusCode.OK, user_id, Configuration.Formatters.JsonFormatter);
        }
    }
}
