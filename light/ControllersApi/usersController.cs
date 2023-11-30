 
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class usersController : ApiController
    {
        [light.CacheFilter(TimeDuration = 600000)]
        [HttpPost, HttpGet, HttpHead]
        public HttpResponseMessage picture(string id)
        {
            var result = new HttpResponseMessage(HttpStatusCode.OK);
            var b = bl.pictures.picture_user(id);
            var ms = new System.IO.MemoryStream(b);
            result.Content = new StreamContent(ms);
            result.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            return result;
        }


        [zaggyAuth]
        [HttpGet, HttpPost]
        public HttpResponseMessage dashboard_statistics_simple(string id)
        {
            Auth.check_user(id);

            var statistics = bl.users.dashboard_statistics_simple(id);
            return Request.CreateResponse(HttpStatusCode.OK, statistics, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage details(string id)
        {
            var user = bl.users.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, user, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_currency()
        {
            var currency = bl.currency.get_all();
            return Request.CreateResponse(HttpStatusCode.OK, currency, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_languages()
        {
            var currency = bl.languages.list();
            return Request.CreateResponse(HttpStatusCode.OK, currency, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage user_settings()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            //f.Add("link_web", (string)b["link_web"]);
            //f.Add("link_facebook", (string)b["link_facebook"]);
            //f.Add("link_tweeter", (string)b["link_tweeter"]);
            //f.Add("link_instagram", (string)b["link_instagram"]);
            f.Add("name", (string)b["name"]);
            f.Add("email", (string)b["email"]);
            f.Add("note", (string)b["note"]);
            f.Add("tel", (string)b["tel"]);
            //f.Add("skype", (string)b["skype"]);
            f.Add("user_id", (string)b["user_id"]);
            f.Add("company_adress", (string)b["company_adress"]);
            f.Add("company_name", (string)b["company_name"]);
            f.Add("company_country_id", (string)b["company_country_id"]);
            f.Add("company_city_name", (string)b["company_city_name"]);
            f.Add("company_city_zip", (string)b["company_city_zip"]);
            f.Add("company_id", (string)b["company_id"]);
            f.Add("latitude", (string)b["latitude"]);
            f.Add("longitude", (string)b["longitude"]);
            //f.Add("tax_id", (string)b["tax_id"]);
            f.Add("oib", (string)b["oib"]);
            //f.Add("code", (string)b["code"]);
            //f.Add("vat_obligation", (string)b["vat_obligation"]);
            //f.Add("vat_inclusive", (string)b["vat_inclusive"]);
            //f.Add("vat", (string)b["vat"]);
            //f.Add("link_general_terms", (string)b["link_general_terms"]);
            //f.Add("link_privacy_policy", (string)b["link_privacy_policy"]);
            //f.Add("primary_contant_name", (string)b["primary_contant_name"]);
            f.Add("invoice_text", (string)b["invoice_text"]);
            f.Add("invoice_footer", (string)b["invoice_footer"]);

            bl.users.save_extended(f);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage zaggy_invoices_list()
        {
            var invoices = bl.users_zaggy_invoices_headers.invoices(bl.settings_current.user_id);
            return Request.CreateResponse(HttpStatusCode.OK, invoices, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage zaggy_invoices_files_list(string invoice_id)
        {
            var files = bl.users_zaggy_invoices_headers_files.list(invoice_id);
            return Request.CreateResponse(HttpStatusCode.OK, files, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage revolut_take_card(string id, string erp_id)
        {
            Auth.check("users", id);
            var order_id = bl.B2B.Revolut.orders.user_card_order_id(id, erp_id);
            return Request.CreateResponse(HttpStatusCode.OK, order_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage user_picture_del(string id)
        {
            bl.users.picture_del();
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);


        }

        [HttpGet, HttpPost]
        public HttpResponseMessage user_object_cleans(string id)
        {
            var object_cleans_data = bl.objects_cleans.list(id);
            return Request.CreateResponse(HttpStatusCode.OK, object_cleans_data, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage metricool(string id)
        {
            var url = bl.user_b2b.get(bl.user_b2b.b2b_type.metricool, id).Rows[0]["link1"].ToString();
            return Request.CreateResponse(url);
        }

    }
}
