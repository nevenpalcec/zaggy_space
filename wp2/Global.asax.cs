using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace wp2
{
    public class Global : System.Web.HttpApplication
    {

        string[] languages_codes =  { "en", "de", "fr", "pl", "hr" };

        void Application_Start(object sender, EventArgs e)
        {

            // enable sql logging
            bl.conn.set_user(Properties.Settings.Default.db_conn);

            // Code that runs on application startup
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // set global application variables
            Application["countries"] = bl.countries.contires_list();
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {

            string user_id = "6";
            var request = ((System.Web.HttpApplication)sender).Request;
            var path = request.Path;

            // log user_id
            bl.log_api_flow.add(null, "web: path", path);


            if (path.Contains("/api") == true)
            {
                // we are goin to api => we dont have thre sessions so lets skip it
                return;
            }

            // get domain
            var domain = request.Url.Host;
            domain = domain.ToLower().Replace("www.", "");

            if (domain.Contains("localhost") == false)
            {
                if (string.IsNullOrWhiteSpace(bl.settings_current.user_id) == true)
                {

                    user_id = bl.booking_engine.user_id_by_web_domain(domain);

                    // log user_id
                    bl.log_api_flow.add(null, "web DB: user_id", user_id);
                }
                else
                {
                    user_id = bl.settings_current.user_id;

                    // log user_id
                    bl.log_api_flow.add(null, "web Session: user_id", user_id);
                }
                
            }

            // we are on development
            else
            {
                if (string.IsNullOrWhiteSpace(bl.settings_current.user_id) == true)
                {
                    var guid = request.Path.Replace("/", string.Empty);
                    user_id = bl.users.user_id_by_guid(guid);

                    if (string.IsNullOrWhiteSpace(user_id) == true)
                    {
                        // set here if you want to test on a specific user

                         user_id = "6";       // Demo

                    }

                }
                else
                {
                    user_id = bl.settings_current.user_id;
                }
            }           

            // if session variable is null
            if (Session["user"] == null || Session["booking_engine"] == null || System.Web.HttpContext.Current.Session["domain"].ToString() != domain || bl.settings_current.user_id != user_id)
            {

                user_id = "6";

                // ok lets create all the stuff
                var app_path = request.ApplicationPath;

                System.Web.HttpContext.Current.Session["domain"] = domain;

                var user_db = bl.users.user_get(user_id).Rows[0];
                System.Web.HttpContext.Current.Session["user_db"] = user_db;

                bl.settings_current.user_id = user_id;
                bl.settings_current.user_guid = user_db["guid"].ToString();
                bl.settings_current.user_company_name = user_db["company_name"].ToString();

                bl.settings_current.registars.booking_engine = bl.booking_engine.get_by_user_id(user_id);

                // get code
                var language_code = request.Url.Segments.Length > 3 
                    ? request.Url.Segments[1].Replace("/", "") 
                    : bl.settings_current.registars.booking_engine.Rows[0]["language_id"].ToString();

                // set default language
                bl.settings_current.language_id = language_code;

                bl.settings_current.registars.booking_engine_user = bl.booking_engine_user.get(user_id);

                bl.settings_current.domain = domain;
                bl.settings_current.registars.objects_active = bl.objects.list_with_object_b2b(user_id, "297");
                bl.settings_current.registars.user = bl.users.user_get(user_id);

                var user = (System.Data.DataRow)System.Web.HttpContext.Current.Session["user_db"];
                var local_culture_info_web = user["local_culture_info_web"].ToString();
                var language_id = bl.settings_current.language_id;

                if (Request.Cookies["lng"] == null)
                {

                    var language = bl.languages.get(bl.settings_current.language_id).Rows[0];
                    var nameCookie = new System.Web.HttpCookie("lng");
                    nameCookie.Values["lng"] = language["code"].ToString();
                    nameCookie.Values["language_id"] = bl.settings_current.language_id;
                    nameCookie.Expires = System.DateTime.Now.AddDays(30);
                    Response.Cookies.Add(nameCookie);

                    bl.settings_current.lng = language["code"].ToString();

                    var ci = new System.Globalization.CultureInfo(local_culture_info_web);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

                }

                // read settngs every time => enalbe this for testing to see changes
                var booking_engine_db = bl.booking_engine.get_by_user_id(user_id).Rows[0];
                System.Web.HttpContext.Current.Session["booking_engine"] = booking_engine_db;

                // get settings
                var booking_engine_settings = booking_engine_db["settings"].ToString();
                var booking_engine_settings_data = JObject.Parse(booking_engine_settings);
                var wp2_search_menu_type = (booking_engine_settings_data.GetValue("wp2_search_menu_type") ?? "").ToString();
                var show_map_user = booking_engine_settings_data.GetValue("show_map_user").ToString();

                var objects_filters_pets = bl.booking_engine_settings.get("objects_filters_pets", bl.settings_current.user_id);
                var objects_filters_amenities = bl.booking_engine_settings.get("objects_filters_amenities", bl.settings_current.user_id);

                var settings = new
                {
                    objects_filters_pets,
                    objects_filters_amenities,
                    wp2_search_menu_type,
                    show_map_user
                };

                System.Web.HttpContext.Current.Session["settings"] = bl.sys.json.str(settings);

            }

            else
            {

                if (Request.Cookies["lng"] != null)
                {

                    string lng = "en";

                    // get code
                    try
                    {
                        lng = request.Url.Segments[1].Replace("/", "");

                        if (languages_codes.Contains(lng) == false) {
                            lng = Request.Cookies["lng"].Values[0].ToString();
                        }
                        
                    }
                    catch
                    {
                        lng = "en";
                    }                   

                    // var lng = Request.Cookies["lng"].Values[0].ToString();

                    var ci = new System.Globalization.CultureInfo(lng);
                    System.Threading.Thread.CurrentThread.CurrentUICulture = ci;

                    //set session time out
                    Session.Timeout = 1440;
                }
            }

        }

        protected void Application_BeginRequest()
        {
            if (!Context.Request.IsSecureConnection)
                Response.Redirect(Context.Request.Url.ToString().Replace("http:", "https:"));

            var ip = HttpContext.Current.Request.UserHostAddress;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");

            HttpContext.Current.Request.Headers.Add("start_reqest", System.DateTime.Now.Ticks.ToString());
        }

        public void Application_EndRequest(object sender, EventArgs e)
        {
            //var stopwatch = (System.Diagnostics.Stopwatch)HttpContext.Current.Items["stopwatch"];
            //stopwatch.Stop();

            try
            {
                if (this.Request != null && this.Response != null)
                {
                    bl.log_api_all.add(this, "wp2");
                }

            }
            catch (Exception ex)
            {
                bl.log_errors.add_log(ex, "api", "Application_EndRequest", "wp2", "Global");
            }

            //stopwatch = n

        }

        protected void Application_Error(object sender, EventArgs e)
        {

            System.Console.WriteLine("Enter - Application_Error");

            var httpContext = ((Global)sender).Context;

            var currentRouteData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(httpContext));
            var currentController = "";
            var currentAction = "";

            if (currentRouteData != null)
            {
                if (currentRouteData.Values["controller"] != null && !String.IsNullOrEmpty(currentRouteData.Values["controller"].ToString()))
                {
                    currentController = currentRouteData.Values["controller"].ToString();
                }

                if (currentRouteData.Values["action"] != null && !String.IsNullOrEmpty(currentRouteData.Values["action"].ToString()))
                {
                    currentAction = currentRouteData.Values["action"].ToString();
                }
            }

            // save log
            var user_id = bl.settings_current.user_id;
            var worker_id = bl.settings_current.worker_id;

            var ex = Server.GetLastError();
            bl.log_errors.add_log(ex, currentController, currentAction, "global error", "global.asax", "wp2", worker_id, user_id);

            //redirect to error page
            // Response.Redirect("/error/not_found");

        }
    }
}