using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class reviewsController : ApiController
    {
        [HttpPost, HttpGet]
        public HttpResponseMessage list(string date_from, string date_until, string object_id = null, string contact_name = null, string show_read = null) 
        {
            var reviews = bl.reviews.list(date_from, date_until, bl.settings_current.user_id, null, object_id, null, null, contact_name, show_read);
            return Request.CreateResponse(HttpStatusCode.OK, reviews, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_by_object(string id)
        {
            var review_list = bl.reviews.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, review_list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id, string object_id)
        {
            var review_id = bl.reviews.add(id, object_id);
            return Request.CreateResponse(HttpStatusCode.OK, review_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("rating_max", (string)b["rating_max"] ?? "5" );
            f.Add("rent_id", (string)b["rent_id"]);
            f.Add("object_id", (string)b["object_id"]);
            f.Add("rating_accuracy", (string)b["rating_accuracy"]);
            f.Add("rating_check_in", (string)b["rating_check_in"]);
            f.Add("rating_cleanliness", (string)b["rating_cleanliness"]);
            f.Add("rating_communication", (string)b["rating_communication"]);
            f.Add("rating_value_for_money", (string)b["rating_value_for_money"]);
            f.Add("rating_location", (string)b["rating_location"]);
            f.Add("contact_country_id", (string)b["contact_country_id"]);
            f.Add("review_date", (string)b["date"]);
            f.Add("rating", (string)b["rating"]);
            f.Add("rent_id", (string)b["rent_id"]);
            f.Add("review_id", (string)b["review_id"]);
            f.Add("active", (string)b["active"]); 
            f.Add("public", (string)b["public"]);
            f.Add("is_customer", (string)b["is_customer"]);
            f.Add("positive", (string)b["positive"]);
            f.Add("negative", (string)b["negative"]);
            f.Add("note_public", (string)b["note_public"]);
            f.Add("note_owner", (string)b["note_owner"]);
            f.Add("contact_name", (string)b["contact_name"]);
            f.Add("headline", (string)b["headline"]);

            bl.reviews.save(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpPost, HttpGet]
        public HttpResponseMessage set_open(string id) 
        {
            var user_id = bl.settings_current.user_id;
            bl.reviews.set_open(id);
            return Request.CreateResponse(HttpStatusCode.OK, user_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage mark_all_as_seen() 
        {
            var user_id = bl.settings_current.user_id;
            bl.reviews.mark_all_as_seen(user_id);
            return Request.CreateResponse(HttpStatusCode.OK, user_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet,HttpPost]
        public HttpResponseMessage get(string id)
        {
            var review = bl.reviews.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, review, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete(string id)
        {
            bl.reviews.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

    }
}
