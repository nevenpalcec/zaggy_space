using System.Web.Mvc;

namespace light.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class homeController : Controller
    {
        // GET: home

        public ActionResult index(string id=null, string hash = null)
        {
            ViewData["guid"] = id;
            ViewData["hash"] = hash;
            return View();
        }

        public ActionResult user_picture_add(string id)
        {
            foreach (string file2 in Request.Files)
            {
                var fileContent = Request.Files[file2];
                bl.users.picture_add(fileContent, id);
            }
            Response.Redirect("/?hash=settings");
            return null;
        }

    }
}