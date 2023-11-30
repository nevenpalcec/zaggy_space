using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace light.ControllersApi
{
    [zaggyAuth]

    public class objects_distancesController : ApiController
    {
        [HttpGet, HttpPost]
        public HttpResponseMessage list(string id)
        {
            var distances = bl.objects_distances.get_by_object(id);
            return Request.CreateResponse(HttpStatusCode.OK, distances, Configuration.Formatters.JsonFormatter);
        }

        [HttpGet, HttpPost]
        public void save()
        {
            string body_string = Request.Content.ReadAsStringAsync().Result;
            var b = bl.sys.json.obj(body_string);

            var f = new System.Collections.Specialized.NameValueCollection();

            f.Add("object_id", (string)b["object_id"]);
            f.Add("user_id", (string)b["user_id"]);

            
            f.Add("dis_" + (string) b["airport"], (string) b["airport_value"]);
            f.Add((string) b["airport"] + "_unit", (string) b["airport_unit"]);
            
            f.Add("dis_" + (string) b["bank_atm"], (string)b["bank_atm_value"]);
            f.Add((string) b["bank_atm"] + "_unit", (string)b["bank_atm_unit"]);
            
            f.Add("dis_" + (string) b["bus_stop"], (string) b["bus_stop_value"]);
            f.Add((string) b["bus_stop"] + "_unit", (string) b["bus_stop_unit"]);
            
            f.Add("dis_" + (string) b["caffe_bar"], (string)b["caffe_bar_value"]);
            f.Add((string) b["caffe_bar"] + "_unit", (string)b["caffe_bar_unit"]);
            
            f.Add("dis_" + (string) b["downtown"], (string)b["downtown_value"]);
            f.Add((string) b["downtown"] + "_unit", (string)b["downtown_unit"]);
            
            f.Add("dis_" + (string) b["ferry"], (string)b["ferry_value"]);
            f.Add((string) b["ferry"] + "_unit", (string)b["ferry_unit"]);
            
            f.Add("dis_" + (string) b["gas_station"], (string)b["gas_station_value"]);
            f.Add((string) b["gas_station"] + "_unit", (string)b["gas_station_unit"]);
            
            f.Add("dis_" + (string) b["highway"], (string)b["highway_value"]);
            f.Add((string) b["highway"] + "_unit", (string)b["highway_unit"]);
            
            f.Add("dis_" + (string) b["marina"], (string)b["marina_value"]);
            f.Add((string) b["marina"] + "_unit", (string)b["marina_unit"]);
            
            f.Add("dis_" + (string) b["market"], (string)b["market_value"]);
            f.Add((string) b["market"] + "_unit", (string)b["market_unit"]);
            
            f.Add("dis_" + (string) b["night_life"], (string)b["night_life_value"]);
            f.Add((string) b["night_life"] + "_unit", (string)b["night_life_unit"]);
            
            f.Add("dis_" + (string) b["pharmacy"], (string)b["pharmacy_value"]);
            f.Add((string) b["pharmacy"] + "_unit", (string)b["pharmacy_unit"]);
            
            f.Add("dis_" + (string) b["railway_station"], (string)b["railway_station_value"]);
            f.Add((string) b["railway_station"] + "_unit", (string)b["railway_station_unit"]);
            
            f.Add("dis_" + (string) b["restaurant"], (string)b["restaurant_value"]);
            f.Add((string) b["restaurant"] + "_unit", (string)b["restaurant_unit"]);
            
            f.Add("dis_" + (string) b["shops"], (string)b["shops_value"]);
            f.Add((string) b["shops"] + "_unit", (string)b["shops_unit"]);
            
            f.Add("dis_" + (string) b["ski_lift"], (string)b["ski_lift_value"]);
            f.Add((string) b["ski_lift"] + "_unit", (string)b["ski_lift_unit"]);
            
            f.Add("dis_" + (string) b["sports"], (string)b["sports_value"]);
            f.Add((string) b["sports"] + "_unit", (string)b["sports_unit"]);
            
            f.Add("dis_" + (string) b["tourist_info_center"], (string)b["tourist_info_center_value"]);
            f.Add((string) b["tourist_info_center"] + "_unit", (string)b["tourist_info_center_unit"]);
            
            f.Add("dis_" + (string) b["water"], (string)b["water_value"]);
            f.Add((string) b["water"] + "_unit", (string)b["water_unit"]);

            //distances not in syncbeds
            f.Add("dis_" + (string) b["bank"], (string) b["bank_value"]);
            f.Add((string) b["bank"] + "_unit", (string) b["bank_unit"]);

            f.Add("dis_" + (string) b["beach"], (string) b["beach_value"]);
            f.Add((string) b["beach"] + "_unit", (string) b["beach_unit"]);

            f.Add("dis_" + (string) b["city_center"], (string) b["city_center_value"]);
            f.Add((string) b["city_center"] + "_unit", (string) b["city_center_unit"]);

            f.Add("dis_" + (string) b["dentist"], (string) b["dentist_value"]);
            f.Add((string) b["dentist"] + "_unit", (string) b["dentist_unit"]);

            f.Add("dis_" + (string) b["doctor"], (string) b["doctor_value"]);
            f.Add((string) b["doctor"] + "_unit", (string) b["doctor_unit"]);

            f.Add("dis_" + (string) b["golf"], (string) b["golf_value"]);
            f.Add((string) b["golf"] + "_unit", (string) b["golf_unit"]);

            f.Add("dis_" + (string) b["katamaran"], (string) b["katamaran_value"]);
            f.Add((string) b["katamaran"] + "_unit", (string) b["katamaran_unit"]);

            f.Add("dis_" + (string) b["lake"], (string) b["lake_value"]);
            f.Add((string) b["lake"] + "_unit", (string) b["lake_unit"]);



            bl.objects_distances.save(f);



        }




    }
}
