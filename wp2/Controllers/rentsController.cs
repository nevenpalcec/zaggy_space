using System.Web.Mvc;

namespace wp2.Controllers
{
    public class rentsController : Controller
    {
        // GET: rents
        public ActionResult book()
        {
            return View();
        }

        public ActionResult confirm(string id, string error = "0")
        {
            ViewData["rent_id"] = id;
            ViewData["error"] = error;
            return View();
        }

        public void add(FormCollection f)
        {

            var contact_name_first = new System.Web.HttpCookie("contact_name_first", f.Get("contact_name_first"));
            contact_name_first.Expires.AddDays(365);
            HttpContext.Response.Cookies.Add(contact_name_first);

            var contact_email = new System.Web.HttpCookie("contact_email", f.Get("contact_email"));
            contact_email.Expires.AddDays(365);
            HttpContext.Response.Cookies.Add(contact_email);

            var contact_tel = new System.Web.HttpCookie("contact_tel", f.Get("contact_tel"));
            contact_tel.Expires.AddDays(365);
            HttpContext.Response.Cookies.Add(contact_tel);

            var contact_country_id = new System.Web.HttpCookie("contact_country_id", f.Get("contact_country_id"));
            contact_country_id.Expires.AddDays(365);
            HttpContext.Response.Cookies.Add(contact_country_id);

            f.Add("worker_id", bl.settings_current.worker_id);

            var rent_id = bl.rents.add(f, bl.rents_log.change_by.worker);

            bl.objects_items.add_to_rent_auto(rent_id);
            bl.rents.email_new_rent(rent_id);

            Response.Redirect("/rents/confirm");
        }
    }
}