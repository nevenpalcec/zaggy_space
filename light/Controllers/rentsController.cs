using System.Web.Mvc;

namespace light.Controllers
{
    [OutputCache(NoStore = true, Duration = 0)]
    public class rentsController : Controller
    {

        public ActionResult email(string id)
        {
            return Content(bl.rents.mail_generate(id, Server.MapPath("~"), bl.rents.mail_type.confirmation));
        }

        public ActionResult email_voucher(string id)
        {
            return Content(bl.rents.mail_generate(id, Server.MapPath("~"), bl.rents.mail_type.vaucher));
        }

        public ActionResult email_offer(string id)
        {
            return Content(bl.rents.mail_generate(id, Server.MapPath("~"), bl.rents.mail_type.offer));
        }

        public FileStreamResult excel() 
        {
            var rents = (System.Data.DataTable) Session["sb_rent_list"];
            rents = bl.sys.excell.generate.rents(rents);

            var memStream = bl.sys.excell.generate.from_datatable(rents);
            memStream.Position = 0;

            return File(memStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}