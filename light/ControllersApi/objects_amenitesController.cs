 using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_amenitesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var list = bl.amenites.list_object("189", id);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_available(string id)
        {
            var list = bl.amenites.list_available("189", id);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id, string amenity_id)
        {
            var list = bl.objects_amenites.add(id, amenity_id);
            return Request.CreateResponse(HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete_amenities(string id)
        {
            bl.objects_amenites.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage copy_to_object(string from_object_id, string to_object_id)
        {
            bl.objects_amenites.copy_to_object(from_object_id, to_object_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete_object(string id)
        {
            bl.objects_amenites.del_object(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete_charges(string id)
        {
            bl.objects_amenites.del_charges(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage sync_room_amenities(string id)
        {
            bl.objects_amenites.sync_wtih_object_amenities(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }
    }

}
