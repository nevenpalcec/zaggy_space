 
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class units_picturesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage main_picture(string id)
        {
            var main_pictures = bl.units_pictures.main_picture_get(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, main_pictures, Configuration.Formatters.JsonFormatter);
        }
    }
}
