using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using System.Web.SessionState;

namespace light
{
    public class Global : HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            bl.conn.set_user("light");

            GlobalFilters.Filters.Add(new GlobalActionFilterAttribute());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }

        protected void Application_Error(object sender, EventArgs e)
        {

            System.Console.WriteLine("Enter - Application_Error");

            var ex = Server.GetLastError();
        
            Response.StatusCode = 500;
            Response.Write(ex.Message);


        }

        [zaggyAuth_Local]
        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
        }

    }

    public class GlobalActionFilterAttribute : IActionFilter
    {
        
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // System.Console.WriteLine("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // System.Console.WriteLine("OnActionExecuting");
            //if (filterContext.RequestContext.HttpContext.Request.IsLocal == false)
            //{
            //    filterContext.HttpContext.Response.StatusCode = 401;
            //    filterContext.HttpContext.Response.Headers.Clear();
            //}
        }

        
    }
}