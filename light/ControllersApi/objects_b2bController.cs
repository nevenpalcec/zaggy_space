using bl;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_b2bController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var obj = bl.objects_b2b.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, obj, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save() 
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            var object_id = (string)b["object_id"];

            Auth.check("objects", object_id);

            f.Add("id", (string) b["id"]);

            f.Add("object_id", object_id);

            f.Add("b2b_id", (string)b["b2b_id"]);
            f.Add("item_id", (string) b["item_id"]);
            f.Add("user_id", (string) b["user_id"]);

            f.Add("value", (string)b["value"]);
            f.Add("price_percent", (string) b["price_percent"]);
            f.Add("price_extra", (string) b["price_extra"]);
            f.Add("price_id", (string) b["price_id"]);
            f.Add("price_type", (string) b["price_type"]);
            f.Add("commision", (string) b["commision"]);

            f.Add("tolken", (string)b["tolken"]);
            f.Add("web", (string)b["web"]);
            f.Add("exchange", (string)b["exchange"]);
            f.Add("round", (string)b["round"]);     
            f.Add("name", (string)b["name"]);

            f.Add("option1", (string)b["option1"]);
            f.Add("option2", (string)b["option2"]);
            f.Add("option3", (string)b["option3"]);

            f.Add("save_commision", (string)b["save_commision"]);
            f.Add("add_items", (string)b["add_items"]);
            f.Add("share_content", (string)b["share_content"]);
            f.Add("active", (string)b["active"]);
            f.Add("save_payout_price", (string)b["save_payout_price"]);
            
            bl.objects_b2b.save_json(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add() 
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            var object_id = (string)b["object_id"];

            f.Add("b2b_id", (string) b["b2b_id"]);
            f.Add("b2b_value", (string) b["b2b_value"]);
            f.Add("item_id", (string) b["item_id"]);
            f.Add("object_id", object_id);
            f.Add("price_id", (string) b["price_id"]);
            f.Add("user_id", (string) b["user_id"]);

            Auth.check("objects", object_id);

            bl.objects_b2b.add_json(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add_booking() 
        {

            string body_string = Request.Content.ReadAsStringAsync().Result;

            var j = body_string.obj();

            var user_id = (string)j["user_id"];

            Auth.check_user(user_id);

            bl.objects_b2b.add_json_booking(body_string);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add_b2b() 
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;

            var j = body_string.obj();

            var user_id = (string) j["user_id"];
            var b2b_id = (string) j["b2b_id"];

            Auth.check_user(user_id);

            if (b2b_id == bl.B2B.Expedia.shared.b2b_id) 
            {
                bl.objects_b2b.add_json_expedia(body_string);
            } else {
                bl.objects_b2b.add_json_b2b(body_string);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage airbnb_connect()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var body = bl.sys.json.obj(body_string);

            var user_id = (string) body["user_id"];
            var object_id = (string) body["object_id"];

            Auth.check_user(user_id);

            // Get old value
            var airbnb_old = bl.objects_b2b.get_object_b2b(object_id, bl.B2B.AirBNB.shared.b2b_id).Rows[0];

            bl.B2B.AirBNB.listings.connect(body_string);

            // Get new value, compare with old value, save changes into log
            var airbnb_new = bl.objects_b2b.get_object_b2b(object_id, bl.B2B.AirBNB.shared.b2b_id).Rows[0];
            var changes = bl.sys.data_table.compare_dt(airbnb_old, airbnb_new);
            bl.log_changes.add(Settings.app_id, "Airbnb connect object", "objects_b2b", object_id, changes, bl.log_changes.who.worker, bl.log_changes.type_action.update);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete(string id)
        {
            Auth.check("objects_b2b", id);

            bl.objects_b2b.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_grp_by_type(string id)
        {
            var response = bl.objects_b2b.list_grp_by_type(id);
            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage list_grp_by_type_count(string id)
        {

            var response = bl.objects_b2b.list_grp_by_type_count(id);
            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }



    }

}
