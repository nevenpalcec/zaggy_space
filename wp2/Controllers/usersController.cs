using System.Web.Mvc;

namespace wp2.Controllers
{
    public class usersController : Controller
    {
        // GET: users
        public ActionResult about()
        {
            return View();
        }

        public ActionResult general_terms()
        {
            return View();
        }

        public ActionResult privacy_policy()
        {
            return View();
        }

        public void language_set(string id)
        {
            var language = bl.languages.get(id).Rows[0];

            var nameCookie = new System.Web.HttpCookie("lng");

            nameCookie.Values["lng"] = language["code"].ToString();
            nameCookie.Values["language_id"] = id;

            bl.settings_current.language_id = id;

            nameCookie.Expires = System.DateTime.Now.AddDays(30);
            Response.Cookies.Add(nameCookie);
            Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
}