 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_roomsController : ApiController
    {
        [HttpGet,HttpPost]
        public HttpResponseMessage list(string id)
        {
            var list = bl.objects_rooms.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, "", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet,HttpPost]
        public HttpResponseMessage add_room()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("user_id", (string)b["user_id"]);
            f.Add("object_id", (string)b["object_id"]);
            f.Add("space", (string)b["space"]);
            f.Add("objects_rooms_types", (string)b["type_id"]);
            f.Add("floor_id", (string)b["floor_id"]);
            f.Add("quantity", (string)b["quantity"]);
            f.Add("name", (string)b["name"]);

            var list = bl.objects_rooms.add(f);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get(string id)
        {
            var room = bl.objects_rooms.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, room, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_room()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("space", (string)b["space"]);
            f.Add("quantity", (string)b["quantity"]);
            f.Add("toilets", (string)b["toilets"]);
            f.Add("floor_id", (string)b["floor_id"]);
            f.Add("floor", (string)b["floor"]);
            f.Add("code", (string)b["code"]);
            f.Add("objects_rooms_types", (string)b["type_id"]);
            f.Add("name", (string)b["name"]);
            f.Add("en_suite_bathroom_type", (string)b["en_suite_bathroom_type"]);
            f.Add("en_suite_bathroom", (string)b["en_suite_bathroom"]);
            f.Add("room_id", (string)b["room_id"]);
            f.Add("description", (string)b["description"]);

            bl.objects_rooms.save(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage del_room(string id)
        {
            bl.objects_rooms.del(id);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage auto_add_rooms(string id)
        {
           var room_id = bl.objects_rooms.add_by_realstate(id);
            return Request.CreateResponse(HttpStatusCode.OK, room_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_bedrooms(string id)
        {
            var bedrooms = bl.objects_rooms.bedrooms_no_livingroom(id);
            return Request.CreateResponse(HttpStatusCode.OK, bedrooms, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_livingrooms(string id)
        {
            var livingrooms = bl.objects_rooms.livingrooms_beds(id);
            return Request.CreateResponse(HttpStatusCode.OK, livingrooms, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_bathrooms(string id)
        {
            var bathrooms = bl.objects_rooms.bathrooms(id);
            return Request.CreateResponse(HttpStatusCode.OK, bathrooms, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage clone(string id)
        {
            var room = bl.objects_rooms.clone(id);
            return Request.CreateResponse(HttpStatusCode.OK, room, Configuration.Formatters.JsonFormatter);
        }
    }
}
