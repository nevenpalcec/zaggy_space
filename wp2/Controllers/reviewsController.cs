using System.Web.Mvc;

namespace wp2.Controllers
{
    public class reviewsController : Controller
    {
        // GET: reviews
        public ActionResult list()
        {
            return View();
        }
    }
}