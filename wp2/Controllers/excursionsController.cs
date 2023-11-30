using System.Web.Mvc;

namespace wp2.Controllers
{
    public class excursionsController : Controller
    {
        // GET: excursions
        public ActionResult list()
        {
            return View();
        }

        // GET: excursions
        public ActionResult details(string id)
        {
            ViewData["excursion_id"] = id;
            return View();
        }

        public ActionResult pay(string id)
        {
            ViewData["id_hash"] = id;
            return View();
        }

        public ActionResult pay_success(string id)
        {
            ViewData["id_hash"] = id;
            return View();
        }

        public ActionResult pay_error(string id)
        {
            ViewData["id_hash"] = id;
            return View();
        }

        public ActionResult pay_cancel(string id)
        {
            ViewData["id_hash"] = id;
            return View();
        }

    }
}