using System.Web.Mvc;

namespace wp2.Controllers
{
    public class picturesController : Controller
    {
        // GET: pictures
        public ActionResult gallery()
        {
            return View();
        }
    }
}