using bl;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace light
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class CacheFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        public int TimeDuration { get; set; }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response.Headers.CacheControl = new CacheControlHeaderValue
            {
                MaxAge = System.TimeSpan.FromSeconds(TimeDuration),
                MustRevalidate = true,
                Public = true
            };
        }
    }

    public class zaggyAuth : System.Web.Http.Filters.ActionFilterAttribute
    {
        public zaggyAuth()
        {

        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {

            var url = actionContext.Request.RequestUri.AbsolutePath;
            if (url.Contains("/api/users/picture") == true || url.Contains("/api/objects/picture_upload") == true || url.Contains("/api/objects/main_picture_upload") == true)
            {
                return;
            }

            try
            {
                var zaggy_user_key = actionContext.Request.Headers.GetValues("zaggy_user_key")?.FirstOrDefault();
                var zaggy_user_id = actionContext.Request.Headers.GetValues("zaggy_user_id")?.FirstOrDefault();
                var custom_header = actionContext.Request.Headers.GetValues("X-Custom-Header")?.FirstOrDefault();
                var zaggy_worker_key = actionContext.Request.Headers.GetValues("zaggy_worker_key")?.FirstOrDefault();

                if (zaggy_user_key.is_null() || zaggy_user_id.is_null() || custom_header.is_null() || zaggy_worker_key.is_null() || zaggy_user_key == "-1" || zaggy_user_id == "-1" || custom_header == "-1" || zaggy_worker_key == "-1")
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new System.Net.Http.StringContent("Being both soft and strong is a combination very few have mastered");
                    return;
                }

                var worker_user_id = bl.workers.get_user_id_by_guid(zaggy_worker_key);
                var user_id = bl.users.get_id_by_guid(zaggy_user_key);

                if (worker_user_id != user_id || user_id != zaggy_user_id || worker_user_id != zaggy_user_id)
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new System.Net.Http.StringContent("Now that you’ve proven your mettle, it’s time to kick some ass!");
                    return;
                }

            }
            catch
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                actionContext.Response.Content = new System.Net.Http.StringContent("Underestimate me. That’ll be fun");
                return;
            }


        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }

    public class zaggyAuth_Local : System.Web.Http.Filters.ActionFilterAttribute
    {
        public zaggyAuth_Local()
        {

        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.IsLocal == false)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                actionContext.Response.Content = new System.Net.Http.StringContent("Sorry: not local");
                return;
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

        }
    }



}
