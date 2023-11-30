using System.Web.Mvc;

namespace wp2.Controllers
{
    public class sysController : Controller
    {
       
        /// <summary>
        /// on start, set start page 
        /// </summary>
        /// <param name="id"></param>
        public void start(string id)
        {


            var booking_engine = (System.Data.DataRow)System.Web.HttpContext.Current.Session["booking_engine"];

            var first_page_display = booking_engine["first_page_display"].ToString();

            if (first_page_display == "objects")
            {
                Response.Redirect("/objects/list", false);
            }

            else if (first_page_display == "blogs") 
            {
                Response.Redirect("/blogs/blogs", false);
            }

            else if (first_page_display == "excursions")
            {
                Response.Redirect("/excursions/list", false);
            }

            else
            {
                Response.Redirect("/home/index", false);
            }
            
        }

        public void language_set(string id)
        {
            bl.settings_current.language_id = id;
            Response.Redirect(Request.UrlReferrer.ToString());
        }

    }
}
