
using bl;
using bl.sys.conversion;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objectsController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list_short(string id)
        {
            // this call is: /api/objects/list/
            var objects = bl.objects.list_short(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, objects, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id, string unit_id, string object_type_id)
        {
            Auth.check_user(id);
            var object_id = bl.objects.add(unit_id, object_type_id, id);
            return Request.CreateResponse(HttpStatusCode.OK, object_id, Configuration.Formatters.JsonFormatter);
        }


        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            
            Auth.check("workers", id);

            var objects = bl.objects.objects_list_by_worker_sort(id, null, null, null,null, "Y");
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, objects, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage count_active(string id)
        {
            Auth.check_user(id);

            var objects = bl.objects.count_active(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, objects, Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage days_blocked(string id) {

            Auth.check("objects", id);

            string days_blocked = bl.rents.rents_dates(id, true);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, days_blocked, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_cancellations_list(string id) 
        {
            Auth.check("objects", id);
            var list = bl.objects_cancellations.list(id);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }        

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_cancellations_save(string id, string from, string until, string percent)
        {
            var f = new System.Collections.Specialized.NameValueCollection(); 
            f.Add("objects_cancellations_id", id);
            f.Add("from", from);
            f.Add("until", until);
            f.Add("percent", percent);

            bl.objects_cancellations.save(f);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_cancellations_add(string id) 
        {
            Auth.check("objects", id);
            var objects_cancellation_id = bl.objects_cancellations.add(id);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, objects_cancellation_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_cancellations_del(string id) 
        {
            bl.objects_cancellations.del(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_cancellations_copy_to_all(string id) 
        {
            Auth.check("objects", id);
            bl.objects_cancellations.copy_to_all(id);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage objects_cancellations_nju_save()
        {
            var json = Request.Content.ReadAsStringAsync().Result;
            var j = bl.sys.json.obj(json);
            var object_id = (string) j["object_id"];

            Auth.check("objects", object_id);

            bl.objects_cancellations_free.save(json);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage objects_cancellations_nju_copy_to_all(string id)
        {
            Auth.check("objects", id);
            bl.objects_cancellations_free.copy_to_all(id);

            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage objects_cancellations_nju_list(string id) 
        {
            Auth.check("objects", id);
            var cp = bl.objects_cancellations_free.get(id);
            return Request.CreateResponse(HttpStatusCode.OK, cp, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_payment_terms_list(string id) 
        {
            Auth.check("objects", id);
            var list = bl.objects_payment_terms.list(id);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, list, Configuration.Formatters.JsonFormatter);
        }
        
        [HttpGet, HttpPost]
        public HttpResponseMessage objects_payment_terms_save(string id, string when, string how, string days, string amount) 
        {
            var f = new System.Collections.Specialized.NameValueCollection();
            f.Add("objects_payment_terms_id", id);
            f.Add("payment_terms_when", when);
            f.Add("payment_terms_how", how);
            f.Add("days", days);
            f.Add("amount", amount);

            bl.objects_payment_terms.save(f);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_payment_terms_add(string id) 
        {
            Auth.check("objects", id);
            var objects_payment_terms_id = bl.objects_payment_terms.add(id);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, objects_payment_terms_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_payment_terms_del(string id)
        {
            bl.objects_payment_terms.del(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage objects_payment_terms_copy_to_all(string id, string user_id) 
        {
            Auth.check("objects", id);
            bl.objects_payment_terms.copy_all(user_id, id);

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage get_details(string id)
        {
            Auth.check("objects", id);
            var objects = bl.objects.get_all(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, objects, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage picture_upload(string id, string user_id)
        {
       
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
          
                foreach (string f in httpRequest.Files)
                {
                    var file = httpRequest.Files[f];
                    file.InputStream.Position = 0;
                    
                    var ms = new System.IO.MemoryStream();
                    file.InputStream.CopyTo(ms);

                    bl.objects_relstate_pictures.SaveUploadedFile_bytes(ms, file.FileName, user_id, id);
                }
                 
            }
            else
            {
                throw new System.Exception("Error");
            }

             
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage main_picture_upload(string id, string user_id)
        {

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {

                foreach (string f in httpRequest.Files)
                {
                    var file = httpRequest.Files[f];
                    bl.objects.main_picture_save(file.InputStream, user_id, id, file.FileName);
                }

            }
            else
            {
                throw new System.Exception("Error");
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage status(string id)
        {
            var status = bl.objects.status(id);
            return Request.CreateResponse(HttpStatusCode.OK, status, Configuration.Formatters.JsonFormatter);
        }


        [HttpPost]
        public HttpResponseMessage list_status()
        {
            string object_list = Request.Content.ReadAsStringAsync().Result;
            var status = bl.objects.list_status(object_list.Split(','));
            return Request.CreateResponse(HttpStatusCode.OK, status, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save()
        {

            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var object_id = (string)b["object_id"];

            Auth.check("objects", object_id);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("price_calculation", (string)b["price_calculation"]);
            f.Add("object_id", object_id);
            f.Add("tourist_membership_fee", (string)b["tourist_membership_fee"]);
            f.Add("vat_neto", (string)b["vat_neto"]);
            f.Add("vat_advance", (string)b["vat_advance"]);
            f.Add("vat_profit", (string)b["vat_profit"]);
            f.Add("vat", (string)b["vat"]);
            f.Add("note_important", (string)b["note_important"]);
            f.Add("note_email", (string)b["note_email"]);
            f.Add("note_invoice", (string)b["note_invoice"]);
            f.Add("note_long", (string)b["note_long"]);
            f.Add("note", (string)b["note"]);
            f.Add("name", (string)b["name"]);
            f.Add("color", (string)b["color"]);
            f.Add("object_vat_type", (string)b["object_vat_type"]);
            f.Add("profit_calculation", (string)b["profit_calculation"]);
            f.Add("object_type_id", (string)b["object_type_id"]);
            f.Add("calculation_type", (string)b["calculation_type"]);
            f.Add("advance_percent", (string)b["advance_percent"]);
            f.Add("provision_calculation", (string)b["provision_calculation"]);
            f.Add("provision_with_vat", (string)b["provision_with_vat"]);
            f.Add("city_tax", (string)b["city_tax"]);
            f.Add("price_with_city_tax", (string)b["price_with_city_tax"]);
            f.Add("price_with_vat", (string)b["price_with_vat"]);
            f.Add("price", (string)b["price"]);
            //f.Add("worker_id", (string)b["worker_id"]);
            //f.Add("min_stay", (string)b["min_stay"]);

            bl.objects.save(f);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save_json()
        {
            string json_string = Request.Content.ReadAsStringAsync().Result;

            var j = json_string.obj();
            var object_id = (string)j["object_id"];

            Auth.check("objects", object_id);


            bl.objects.save_json(json_string, Settings.app_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);

        }

        [HttpGet, HttpPost]
        public HttpResponseMessage main_picture_del(string id)
        {
            bl.objects.picture_main_del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage sync_all(string id)
        {
            bl.objects.sync(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage sync_b2b(string id, string b2b_id) 
        {
            bl.objects.sync_b2b(id, b2b_id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage delete(string id)
        {
            Auth.check("objects", id);

            bl.objects.del(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage deactivate(string id)
        {
            Auth.check("objects", id);
            
            bl.objects.deactivate(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet]
        public HttpResponseMessage activate(string id) {

            Auth.check("objects", id);

            bl.objects.activate(id);
            return Request.CreateResponse(HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }
    }
}
