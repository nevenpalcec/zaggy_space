using System.Web.Mvc;

namespace wp2.Controllers
{
    public class blogsController : Controller
    {
        // GET: blogs
        public ActionResult index()
        {
            return View();
        }

        public ActionResult blogs()
        {
            var page = (string)(Request.QueryString["page"] ?? "0");
            ViewData["page"] = page;
            return View();
        }

        public ActionResult details(string id)
        {
            ViewData["blog_id"] = id;
            return View();
        }
    }
}