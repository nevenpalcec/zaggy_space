

using Microsoft.AspNetCore.Mvc.Filters;


namespace Auth
{


    public class zaggyAuth : IActionFilter
    {
        public zaggyAuth()
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            var headers = context.HttpContext.Request.Headers;

            var reseller_worker_guid = AesOperation.DecryptString(bl.B2B.zaggy.shared.key, headers["reseller_worker_guid"].ToString());
            var reseller_worker_id = AesOperation.DecryptString(bl.B2B.zaggy.shared.key, headers["reseller_worker_id"].ToString());
            var guid = headers["guid"].ToString();

            //var reseller_worker_perm = AesOperation.DecryptString(bl.B2B.zaggy.shared.key, headers["reseller_worker_perm"].ToString());
            //var reseller_worker_type_id = AesOperation.DecryptString(bl.B2B.zaggy.shared.key, headers["reseller_worker_type_id"].ToString());
            //var name = AesOperation.DecryptString(bl.B2B.zaggy.shared.key, headers["name"].ToString());

            if (reseller_worker_guid != guid)
            {
                throw new Exception("This is not you!");
            }

            var resller_id = bl.resellers_workers.get_reseller_id_by_id(reseller_worker_id);

            if ("12" != resller_id)
            {
                throw new Exception("Looking for something !?");
            }

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
        }

    }

}
