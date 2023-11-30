using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class rents_itemsController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var items = bl.rents_items.list(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, items, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage add(string id)
        {
            var item_id = bl.rents_items.add(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, item_id, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage del(string id)
        {
            bl.rents_items.del(id);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage save()
        {

            bl.sys.globalization.set();

            var body = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body);

            var f = new System.Collections.Specialized.NameValueCollection();
                       
            f.Add("price", (string)b["price"]);
            f.Add("quantity", (string)b["quantity"]);
            f.Add("vat", (string)b["vat"]);
            f.Add("name", (string)b["name"]);
            f.Add("rent_item_id", (string)b["rent_item_id"]);
            f.Add("rent_item_type", (string)b["rent_item_type"]);
            f.Add("rent_id", (string)b["rent_id"]);
            f.Add("currency_id", (string)b["currency_id"]);
            f.Add("exchange", (string)b["exchange"]);

            bl.rents_items.save(f);
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, "ok", Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public HttpResponseMessage update_row() 
        {

            var body = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("id", (string) b["id"]);
            f.Add("rent_id", (string) b["rent_id"]);
            f.Add("name", (string) b["name"]);
            f.Add("price", (string) b["price"]);
            f.Add("quantity", (string) b["quantity"]);
            f.Add("vat", (string) b["vat"]);
      
            //Save item
            bl.rents_items.save_updated_row(f);

            var a = "ok";

            return Request.CreateResponse(System.Net.HttpStatusCode.OK, a, Configuration.Formatters.JsonFormatter);
        }

    }
}
