using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wp2.Controllers
{
    public class errorController : Controller
    {
        // GET: Error
        public ActionResult not_found()
        {
            return View();
        }
    }
}