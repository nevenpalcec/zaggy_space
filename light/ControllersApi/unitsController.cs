
using bl;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class unitsController : ApiController
    {

        [HttpGet, HttpPost]
        public HttpResponseMessage get_list(string id)
        {
            Auth.check_user(id);

            var unit_list = bl.units.unit_list(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, unit_list, Configuration.Formatters.JsonFormatter);
        }
        
        [HttpGet, HttpPost]
        public HttpResponseMessage get_objects(string id)
        {
            Auth.check("units", id);

            var unit_list = bl.units.list_all_objects(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, unit_list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_unit(string id)
        {
            Auth.check("units", id);

            var unit = bl.units.unit(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, unit, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_unit_pictures(string id)
        {
            Auth.check("units", id);

            var unit_pictures = bl.units_pictures.get_by_unit(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, unit_pictures, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_json()
        {

            string json_string = Request.Content.ReadAsStringAsync().Result;

            var b = json_string.obj();

            var unit_id = (string)b["unit_id"];
            Auth.check("units", unit_id);

            bl.units.save_json(json_string, Settings.app_id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage unit_del(string id)
        {
            Auth.check("units", id);

            bl.units.del(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_contact()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var unit_id = (string)b["unit_id"];

            Auth.check("units", unit_id);


            var f = new System.Collections.Specialized.NameValueCollection();
            f.Add("unit_id", unit_id);
            f.Add("contact_name", (string)b["contact_name"]);
            f.Add("contact_telephone_mobile", (string)b["contact_telephone_mobile"]);
            f.Add("contact_telephone", (string)b["contact_telephone"]);
            f.Add("contact_email", (string)b["contact_email"]);
            f.Add("contact_web", (string)b["contact_web"]);

            bl.units.save_contact(f);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id)
        {
            Auth.check_user(id);

            var unit_id = bl.units.add(id);
            return Request.CreateResponse(HttpStatusCode.OK, unit_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage save_coordinate_manual(string id, string latitude, string longitude)
        {
            Auth.check("units", id);

            bl.units.geo_save(id, latitude, longitude);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage predict_location(string address)
        {
            string lat, lng;
            bl.B2B.Google.geolocation.latlng(address, out lat, out lng, bl.B2B.Google.shared.geocode_api_key);
            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                latitude = lat,
                longitude = lng
            }, Configuration.Formatters.JsonFormatter);
        }

    }

}
