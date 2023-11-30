 
using System.Web.Mvc;

namespace wp2.Controllers
{
    public class wbcController : Controller
    {
        
        public ActionResult test()
        {
            return View();
        }
      
        public ActionResult book1(string id, string mode = "", string inquery_enable = "", string booking_enable = "", string lng = "en")
        {
            ViewData["id_hash"] = id;
            ViewData["mode"] = mode;
            ViewData["inquery_enable"] = inquery_enable;
            ViewData["booking_enable"] = booking_enable;
            ViewData["lng"] = lng;

            var cultureInfo = new System.Globalization.CultureInfo(lng);
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return View();
        }

        public ActionResult book2(string id, string mode = "", string inquery_enable = "", string booking_enable = "", string lng = "en")
        {
            ViewData["id_hash"] = id;
            ViewData["mode"] = mode;
            ViewData["inquery_enable"] = inquery_enable;
            ViewData["booking_enable"] = booking_enable;
            ViewData["lng"] = lng;

            var cultureInfo = new System.Globalization.CultureInfo(lng);
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return View();
        }

        public ActionResult book3(string id, string inquery_enable = "", string booking_enable = "", string lng = "en")
        {
            ViewData["id_hash"] = id;
            ViewData["inquery_enable"] = inquery_enable;
            ViewData["booking_enable"] = booking_enable;
            ViewData["lng"] = lng;

            var cultureInfo = new System.Globalization.CultureInfo(lng);
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return View();
        }

        public ActionResult book4(string id, string mode = "", string inquery_enable = "", string booking_enable = "", string lng = "en")
        {
            ViewData["id_hash"] = id;
            ViewData["mode"] = mode;
            ViewData["inquery_enable"] = inquery_enable;
            ViewData["booking_enable"] = booking_enable;
            ViewData["lng"] = lng;

            var cultureInfo = new System.Globalization.CultureInfo(lng);
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return View();
        }

        public ActionResult book5(string id, string mode = "", string inquery_enable = "", string booking_enable = "", string lng = "en")
        {
            ViewData["id_hash"] = id;
            ViewData["mode"] = mode;
            ViewData["inquery_enable"] = inquery_enable;
            ViewData["booking_enable"] = booking_enable;
            ViewData["lng"] = lng;

            var cultureInfo = new System.Globalization.CultureInfo(lng);
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
            return View();
        }


    }
}