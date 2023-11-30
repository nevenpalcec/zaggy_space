using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class messagesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage conversation_messages(string id)
        {
            var messages = bl.messages.msg(id);
            return Request.CreateResponse(HttpStatusCode.OK, messages, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        public HttpResponseMessage save()
        {
            var json_string = Request.Content.ReadAsStringAsync().Result;
            bl.messages.add_db_json(json_string);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage new_messages_count_not_user(string id)
        {
            var messages_count = bl.messages.new_messages_count_not_user(id);

            return Request.CreateResponse(HttpStatusCode.OK, messages_count, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage message_seen_not_user(string id)
        {
            var json_string = Request.Content.ReadAsStringAsync().Result;
            bl.messages.message_seen_not_user(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_by_dates()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var messages = bl.messages.list_advance(body_string);

            return Request.CreateResponse(HttpStatusCode.OK, messages, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_filtered()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;

            var messages = bl.messages.list_filtered(body_string);

            return Request.CreateResponse(HttpStatusCode.OK, messages, Configuration.Formatters.JsonFormatter);

        }
    }
}
