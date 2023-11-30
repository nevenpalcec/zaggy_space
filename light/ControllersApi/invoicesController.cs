using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class invoicesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list_by_rent(string id)
        {
            var invoices = bl.invoices.list_by_rent(id);
            return Request.CreateResponse(HttpStatusCode.OK, invoices, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_single_invoice(string id) {
            var invoice = bl.invoices.invoice(id);
            return Request.CreateResponse(HttpStatusCode.OK, invoice, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_by_user(string id)
        {
            var invoices = bl.invoices.list_by_user(id);
            return Request.CreateResponse(HttpStatusCode.OK, invoices, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage list_by_user_filtered() {

            string body_string = Request.Content.ReadAsStringAsync().Result;

            var invoices = bl.invoices.list_by_user_filtered(body_string);
            return Request.CreateResponse(HttpStatusCode.OK, invoices, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage invoice_status_save(string id, string paid)
        {
            bl.invoices.invoice_status_save(id, paid);
            return Request.CreateResponse(HttpStatusCode.OK, "ok" ,Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage fiscalise(string id) {

            try {
                bl.invoices.fiskalizacija(id, true);
                return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
            } catch (System.Exception ex) {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message, Configuration.Formatters.JsonFormatter);
            }
            
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage issue_invoice(string id) {
            bl.invoices.lock_invoice(id, bl.settings_current.worker_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage invoice_storno(string id) {
            var new_invoice = bl.invoices.add_storno(id);
            return Request.CreateResponse(HttpStatusCode.OK, new_invoice, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet,HttpPost]
        public HttpResponseMessage invoice_details_save() {

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("payment_method_id", (string) b["payment_method_id"]);
            f.Add("number", (string) b["number"]);
            f.Add("inv_date", (string) b["date"]);
            f.Add("object_id", (string) b["object"]);
            f.Add("currency_id", (string) b["currency"]);
            //TODO: reservation and worker
            f.Add("invoice_id", (string) b["invoice_id"]); 
            f.Add("private_note", (string) b["private_note"]); 
            f.Add("note_short", (string) b["note_short"]); 

            bl.invoices.invoice_details_save(f);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage invoice_customer_save() {

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("customer_name", (string) b["name"]);
            f.Add("company_number", (string) b["company_number"]);
            f.Add("customer_adress", (string) b["address"]);
            f.Add("customer_city_name", (string) b["city"]);
            f.Add("customer_city_zip", (string) b["zip_code"]);
            f.Add("customer_country_id", (string) b["country"]);
            f.Add("customer_tel", (string) b["telephone"]);
            f.Add("customer_email", (string) b["email"]);
            f.Add("invoice_id", (string) b["invoice_id"]);

            bl.invoices.invoice_customer_save(f);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage invoice_item_save() {

            string body_string = Request.Content.ReadAsStringAsync().Result;

            //var b = bl.sys.json.obj(body_string);
            //var f = new System.Collections.Specialized.NameValueCollection();

            //f.Add("invoice_id", (string) b["invoice_id"]);
            //f.Add("user_id", (string) b["user_id"]);
            //f.Add("worker_id", (string) b["worker_id"]);
            //f.Add("item_id", (string) b["item_id"]);
            //f.Add("item_code", (string) b["item_code"]);
            //f.Add("item_name", (string) b["item_name"]);
            //f.Add("price", (string) b["price"]);
            //f.Add("exchange", (string) b["exchange"]);
            //f.Add("quantity", (string) b["quantity"]);
            //f.Add("vat", (string) b["vat"]);
            //f.Add("discount", (string) b["discount"]);

            //bl.invoices.invoice_item_save(f);

            bl.invoices_items.add_json(body_string);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage invoice_item_del(string id) {
            bl.invoices_items.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage delete_invoice(string id)
        {
            bl.invoices.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

    }
}
