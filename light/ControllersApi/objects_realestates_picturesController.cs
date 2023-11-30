 
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_realestates_picturesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage delete_picture(string id)
        {
            bl.objects_relstate_pictures.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok" , Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete_all_picture(string id)
        {
            bl.objects_relstate_pictures.del_all(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add_picture(HttpPostedFileBase picture)
        {
            bl.pictures.add(picture);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost]
        public HttpResponseMessage save_picture_sort(string user_id)
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            bl.objects_relstate_pictures.save_sort(body_string, user_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet]
        public HttpResponseMessage set_as_main(string id, string user_id) {
            
             bl.objects_relstate_pictures.set_to_main(id);
            
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

    }
}
