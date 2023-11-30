using System.Web.Mvc;

namespace light.Controllers
{
    public class contractsController : Controller
    {
        public ActionResult pdf_get(string id)
        {
            byte[] fileStream = bl.users_zaggy_contract.pdf_get(id);
            return File(fileStream, @"application/pdf", "Contract.pdf");

        }

    }
}