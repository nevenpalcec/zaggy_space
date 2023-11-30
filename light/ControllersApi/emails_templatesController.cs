using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]
    public class emails_templatesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage emails(string id, string type, string lng_id = "1")
        {
            var email_templates = bl.emails_templates.emails(id, lng_id, type);
            return Request.CreateResponse(HttpStatusCode.OK, email_templates, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_languages(string id)
        {
            var languages = bl.emails_templates.languages(id);
            return Request.CreateResponse(HttpStatusCode.OK, languages, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {
            var template = bl.emails_templates.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, template, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_template_simple()
        {

            string json_string = Request.Content.ReadAsStringAsync().Result;
            bl.emails_templates.save_template_simple(json_string);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string user_id, string language_id)
        {

            var templ_id = bl.emails_templates.add(user_id, language_id);

            return Request.CreateResponse(HttpStatusCode.OK, templ_id, Configuration.Formatters.JsonFormatter);
        }

    }
}
