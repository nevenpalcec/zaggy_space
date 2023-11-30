using System.Web.Mvc;

namespace light.Controllers
{
    public class usersController : Controller
    {
        public void user_invoce_charge(string id)
        {
            bl.users_zaggy_invoices_headers.charge(id, "merchant");
            Response.Redirect(Request.UrlReferrer.ToString());
        }

    }
}