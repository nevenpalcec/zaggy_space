using System.Web.Mvc;

namespace wp2.Controllers
{
    public class itemsController : Controller
    {
        // GET: items
        public ActionResult items_add(string id, string date_from, string date_until, string item_id, string adults, string pets)
        {
            ViewData["object_id"] = id;
            ViewData["date_from"] = date_from;
            ViewData["date_until"] = date_until;
            ViewData["item_id"] = item_id;
            ViewData["adults"] = adults;
            ViewData["pets"] = pets;
            return View();
        }
    }
}