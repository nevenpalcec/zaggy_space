using admin;
using Blazored.LocalStorage;

public class perm
{

    private ILocalStorageService _localStorageService;

    public perm(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task reseller_worker_perm()
    {
        try
        {
            // permisions
            var permisions_string = await reseller_worker_perm_json_string();
            var perm = bl.sys.json.obj(permisions_string);

            var menus = (Newtonsoft.Json.Linq.JArray)perm["menus"];
            string[] menus_arr = menus.ToObject<string[]>();

            var edit = (Newtonsoft.Json.Linq.JArray)perm["edit"];
            string[] edit_arr = edit.ToObject<string[]>();

            b2b = (string)(perm["b2b"] ?? "384, 385");
            admin = (string)(perm["admin"] ?? "N");
            agent = (string)(perm["agent"] ?? "Y");

            var _reseller_worker_type_id = await reseller_worker_type_id();
            super_admin = _reseller_worker_type_id == "-1" ? "Y" : "N";

            property_login = (string)(perm["property_login"] ?? "N");
            invoce_unlock = (string)(perm["invoce_unlock"] ?? "N");
            countries = (string)(perm["countries"] ?? "-1");
            url_app_zaggy = (string)(perm["url_app_zaggy"] ?? "https://appt.zaggys.com");

            dashboard = menus_arr.Contains("dashboard/general");
            faq = menus_arr.Contains("faq");
            email_templ = menus_arr.Contains("email-templ");
            users_list = menus_arr.Contains("users/list");
            users_deleted = menus_arr.Contains("users/deleted");
            functions = menus_arr.Contains("functions");
            log_prices = menus_arr.Contains("log/prices");
            units_list = menus_arr.Contains("units/list");

            objects_list = menus_arr.Contains("objects/list");
          
            objects_status = menus_arr.Contains("objects/status");
            objects_pictures = menus_arr.Contains("objects/pictures");

            rents_list = menus_arr.Contains("rents/list");
           
            rents_no_show_list = menus_arr.Contains("rents/no-show/list");

            reviews_list = menus_arr.Contains("reviews/list");
            regions_list = menus_arr.Contains("regions/list");

            reports = menus_arr.Contains("users/reports");
            invoces_list = menus_arr.Contains("invoces/list");

            workers_list = menus_arr.Contains("workers/list");
            workers_log = menus_arr.Contains("workers/log");
            settings = menus_arr.Contains("workers/settings");
            inquires_list =  menus_arr.Contains("inquires/list");

            contract_list = menus_arr.Contains("contract/list");

            //edit
            edit_rents = edit_arr.Contains("rents");
            edit_units = edit_arr.Contains("units");
            edit_objects = edit_arr.Contains("objects");
            edit_users = edit_arr.Contains("users");

            edit_reginos = edit_arr.Contains("reginos");
            edit_invoces = edit_arr.Contains("invoces");
        }
        catch (System.Exception ex)
        {
            var err = ex.Message;
        }
        

    }

    public async Task<string> reseller_worker_perm_json_string()
    {
        var crypt = await _localStorageService.GetItemAsync<string>("reseller_worker_perm");
        var json_string = AesOperation.DecryptString(Settings.key, crypt);
        return json_string;
    }

    public async Task<string> permisions_object_del()
    {
        var crypt = await _localStorageService.GetItemAsync<string>("reseller_worker_perm");
        var json_string = AesOperation.DecryptString(Settings.key, crypt);
        var j = bl.sys.json.obj(json_string);
        var object_del = (string)(j["object_del"] ?? "N");
        return object_del;
    }

    public async Task<string> permisions_unit_del()
    {
        var crypt = await _localStorageService.GetItemAsync<string>("reseller_worker_perm");
        var json_string = AesOperation.DecryptString(Settings.key, crypt);
        var j = bl.sys.json.obj(json_string);
        var unit_del = (string)(j["unit_del"] ?? "N");
        return unit_del;
    }

    public async Task<string> permisions_b2b()
    {
        var crypt = await _localStorageService.GetItemAsync<string>("reseller_worker_perm");
        var json_string = AesOperation.DecryptString(Settings.key, crypt);
        var j = bl.sys.json.obj(json_string);
        var b2b = (string)(j["b2b"] ?? "384, 385");
        return b2b;
    }

    public async Task<string> reseller_worker_id()
    {
        return AesOperation.DecryptString(Settings.key, await _localStorageService.GetItemAsync<string>("reseller_worker_id"));
    }

    public async Task<string> reseller_worker_guid()
    {
        return await _localStorageService.GetItemAsync<string>("reseller_worker_guid");
    }

    public async Task<string> reseller_worker_type_id()
    {
        var user_type = "";

        var user_type_crypt = await _localStorageService.GetItemAsync<string>("reseller_worker_type_id");

        try
        {
            user_type = AesOperation.DecryptString(Settings.key, user_type_crypt);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return user_type;
    }

    public async Task<string> resellers_workers_types_name()
    {
        var user_type = "";

        var user_type_crypt = await _localStorageService.GetItemAsync<string>("resellers_workers_types_name");

        try
        {
            user_type = AesOperation.DecryptString(Settings.key, user_type_crypt);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return user_type;
    }

   
    //full permision json
    public string permisions = "";
    
    // menus
    public string admin { get; set; }
    public string super_admin { get; set; }
    public string agent { get; set; }
    public string property_login { get; set; }
    public string url_app_zaggy { get; set; }


    public bool log_prices { get; set; }
    public bool users_deleted { get; set; }
    public bool functions { get; set; }
    public bool email_templ { get; set; }
    public bool faq { get; set; }
    public bool dashboard { get; set; }
    public bool users_list { get; set; }
    public bool objects_list { get; set; }
    public bool units_list { get; set; }
 
    public bool objects_status { get; set; }
    public bool objects_pictures { get; set; }
    public bool rents_list { get; set; }
 
    public bool rents_no_show_list { get; set; }
    public bool reviews_list { get; set; }
    public bool regions_list { get; set; }
    public bool invoces_list { get; set; }
    public bool contract_list { get; set; }

    

    public bool inquires_list { get; set; }
    public bool workers_list { get; set; }
    public bool workers_log { get; set; }
    public bool settings { get; set; }
    public bool reports { get; set; }


    // strings
    public string b2b { get; set; }
    public string countries { get; set; }
    public string invoce_unlock { get; set; }
 

    // edit
    public bool edit_rents { get; set; }
    public bool edit_units { get; set; }
    public bool edit_objects { get; set; }
    public bool edit_users { get; set; }
    public bool edit_reginos { get; set; }
    public bool edit_invoces { get; set; }

}

public class MetaHelper
{
    public string Title { get; set; }
    public string Description { get; set; }
}