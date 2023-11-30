using bl;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace light.ControllersApi
{
    public class workersController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage check(string username = null, string password = null, string guid = null)
        {
            var c = HttpContext.Current;

            if ((username == null || password == null) && guid == null)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            }

            // Login by username and password or login by guid
            var w = guid == null ? bl.workers.worker(username, password) : bl.workers.worker_by_guid(guid);

            if (w.Rows.Count > 0)
            {
                var worker_id = w.Rows[0]["id"].ToString();
                var worker_row = bl.workers.get(worker_id);
                var user_id = w.Rows[0]["user_id"].ToString();

                bl.workers.workers_settings_registry(worker_row);

                var worker = new
                {
                    id = worker_id,
                    user_id = w.Rows[0]["user_id"].ToString(),
                    user_guid = w.Rows[0]["user_guid"].ToString(),
                    username = w.Rows[0]["username"].ToString(),
                    name = w.Rows[0]["name"].ToString(),
                    guid = w.Rows[0]["guid"].ToString(),
                    email = w.Rows[0]["email"].ToString(),
                    active = w.Rows[0]["active"].ToString(),
                    picture_background = w.Rows[0]["picture_background"].ToString(),
                    count = w.Rows[0]["count"],
                    zaggy_company_id = w.Rows[0]["zaggy_company_id"],
                    reseller_worker_id = w.Rows[0]["reseller_worker_id"],
                    session_id = c.Session.SessionID
                };

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, worker, Configuration.Formatters.JsonFormatter);

            } else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "wrong login data", Configuration.Formatters.JsonFormatter);
            }

        }

        [zaggyAuth]

        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {
            var w = bl.workers.get(id);

            if (w.Rows.Count > 0)
            {
                var worker = new
                {
                    id = w.Rows[0]["id"].ToString(),
                    user_id = w.Rows[0]["user_id"].ToString(),
                    username = w.Rows[0]["username"].ToString(),
                    name = w.Rows[0]["name"].ToString(),
                    guid = w.Rows[0]["guid"].ToString(),
                    email = w.Rows[0]["email"].ToString(),
                    active = w.Rows[0]["active"].ToString(),
                    picture_background = w.Rows[0]["picture_background"].ToString()
                };

                return Request.CreateResponse(System.Net.HttpStatusCode.OK, worker, Configuration.Formatters.JsonFormatter);

            }
            else
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "wrong login data", Configuration.Formatters.JsonFormatter);
            }

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_notifications(string user_id)
        {
            var notification = bl.users.notifications_new(user_id).Rows[0].Table;

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, notification, Configuration.Formatters.JsonFormatter);

        }

        [zaggyAuth]

        [CacheFilter(TimeDuration = 600000)]
        [HttpPost, HttpGet, HttpHead]
        public HttpResponseMessage picture(string id)
        {
            HttpResponseMessage result;
            result = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var b = bl.pictures.picture_worker(id);
            var ms = new System.IO.MemoryStream(b);
            result.Content = new StreamContent(ms);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            return result;
        }

        [zaggyAuth]

        [HttpGet, HttpPost]
        public HttpResponseMessage picture_background_set(string id, string img)
        {

            bl.workers.picture_background_set(id, img);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [zaggyAuth]

        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var workers_list = bl.workers.list_simple(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, workers_list, Configuration.Formatters.JsonFormatter);

        }
        
    }
}
