namespace admin
{
    public class settings_web
    {
        public const string reseller_id = "12";
        public static bool is_dev = true;
       

        public static void is_dev_set()
        {
            is_dev = bl.sys.server.is_dev(); 
        }

    }

}
