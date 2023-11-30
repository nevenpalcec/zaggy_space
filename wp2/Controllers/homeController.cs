using System.Web.Mvc;

namespace wp2.Controllers
{
    public class homeController : Controller
    {
        public ActionResult index(string id = null)
        {
            return View();
        }

        public ActionResult index1(string id = null)
        {
            return View();
        }

    }
}