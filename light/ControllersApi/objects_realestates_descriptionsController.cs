using bl;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_realestates_descriptionsController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage add()
        {
            string json_string = Request.Content.ReadAsStringAsync().Result;
            var j = json_string.obj();

            var object_id = (string)j["object_id"];
            var user_id = (string)j["user_id"];
            var language_id = (string)j["language_id"];
            var description = (string)j["description"];

            Auth.check("objects", object_id);

            bl.objects_relstate_descriptions.add(user_id, object_id, language_id, description);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save(string id)
        {
            var description = Request.Content.ReadAsStringAsync().Result;

            bl.objects_relstate_descriptions.save(id, description);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_all() {

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("title", (string)b["title"]);
            f.Add("motto", (string)b["motto"]);
            f.Add("short", (string) b["short"]);
            f.Add("welcome", (string) b["welcome"]);
            f.Add("arrival", (string) b["arrival"]);
            f.Add("landlord", (string) b["landlord"]);
            f.Add("environment", (string) b["environment"]);
            f.Add("vacation_area", (string) b["vacation_area"]);
            f.Add("bathroom", (string) b["bathroom"]);
            f.Add("bedroom", (string) b["bedroom"]);
            f.Add("description_lng", (string) b["description"]);
            f.Add("id", (string) b["id"]);
            f.Add("house_rules", (string) b["house_rules"]);

            bl.objects_relstate_descriptions.save(f, Settings.app_id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id, string lng_id = "1")
        {
            var list = bl.objects_relstate_descriptions.get(id, lng_id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_languages(string id)
        {
            var languages = bl.objects_relstate_descriptions.languages(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, languages, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public void translate_desc(string id, string lng_id_to)
        {
            bl.objects_relstate_descriptions.translate(id, lng_id_to);
        }
    }
}
