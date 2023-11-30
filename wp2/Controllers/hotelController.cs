using System.Web.Mvc;

namespace wp2.Controllers
{
    public class hotelController : Controller
    {

        public ActionResult details(string id)
        {
            ViewData["id"] = id;
            return View();
        }

        public ActionResult room(string id)
        {
            ViewData["id"] = id;
            return View();
        }

    }

}