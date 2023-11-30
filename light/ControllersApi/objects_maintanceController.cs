using bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    public class objects_maintanceController : ApiController
    {
        public HttpResponseMessage add()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("object_id", (string)b["object_id"]);
            f.Add("clean_id", (string)b["clean_id"]);
            f.Add("user_id", (string)b["user_id"]);

            if (f.Get("clean_id") == "-1")
            {
                bl.objects_maintenance.delete_cleans(f);
            }
            else
            {
                bl.objects_maintenance.save(f);
            }



            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }
    }
}
