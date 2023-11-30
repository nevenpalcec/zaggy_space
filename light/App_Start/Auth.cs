

using bl;

namespace light
{
    public class Auth
    {


        public static void check_user(string id)
        {
            check("users", id);
        }

        public static void check(string table, string id)
        {
            var s = System.Web.HttpContext.Current;
            var user_guid_header = s.Request.Headers["zaggy_user_key"].ToString();
            var user_id_header = s.Request.Headers["zaggy_user_id"].ToString();
            var url = s.Request.Url.AbsolutePath;

            try
            {

                // first check if we still have session
                if (table.is_null() == true || id.is_null() == true || user_id_header.is_null() == true || user_guid_header.is_null() == true || user_guid_header == "-1" || user_id_header == "-1")
                {
                    s.Response.Close();
                    throw new System.Exception("Bad? Nope. I’m the worst!");
                }

                else if (table == "users")
                {

                    var user_id_by_guid = bl.users.get_id_by_guid(user_guid_header);

                    if (id != user_id_header || user_id_by_guid != id)
                    {
                        s.Response.Close();
                        throw new System.Exception("You little rebel. I like you!");
                    }

                }

                else
                {

                    // go to db and check for users
                    var user_id = bl.users.get_user_id_universal(table, id);
                    var user_id_by_guid = bl.users.get_id_by_guid(user_guid_header);

                    if (user_id != user_id_header || user_id != user_id_by_guid)
                    {
                        s.Response.Close();
                        throw new System.Exception("I don’t need weapon, I’m one!");

                    }

                }

            }
            catch (System.Exception ex)
            {
                var msg = ex.Message;
                s.Response.Close();
                throw new System.Exception(ex.Message);
            }


        }


    }
}