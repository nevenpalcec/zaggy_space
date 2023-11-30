 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_rooms_bedsController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var beds = bl.objects_rooms_beds.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, beds, Configuration.Formatters.JsonFormatter);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("bed_type_id", (string)b["bed_type_id"]);
            f.Add("dimenzion_x", (string)b["dimenzion_x"]);
            f.Add("dimenzion_y", (string)b["dimenzion_y"]);
            f.Add("bed_id", (string)b["bed_id"]);

            bl.objects_rooms_beds.save(f);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add()
        {

            var bed_string = Request.Content.ReadAsStringAsync().Result;
            var bed = bl.sys.json.obj(bed_string);

            var room_id = (string) bed["room_id"];
            var bed_type_id = (string) bed["bed_type_id"];
            var dimenzion_x = (string) bed["dimenzion_x"];
            var dimenzion_y = (string) bed["dimenzion_y"];

            var bed_id = bl.objects_rooms_beds.add(room_id, bed_type_id, dimenzion_x, dimenzion_y);
            return Request.CreateResponse(HttpStatusCode.OK, bed_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete(string id) {
            bl.objects_rooms_beds.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage beds_type_get(string id) {
            var response = bl.objects_rooms_beds_type.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, response, Configuration.Formatters.JsonFormatter);
        }

    }
}
