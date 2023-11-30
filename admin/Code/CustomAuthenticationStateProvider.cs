
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    string UserId = "";
    string Password;
    string AuthenticationType = "Admin";

    //private ISessionStorageService _sessionStorageService;
    private ILocalStorageService _localStorageService;

    public CustomAuthStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public void LoadUser(string _UserId, string _Password)
    {
        UserId = _UserId;
        Password = _Password;
    }

    string getLng = "";
    string guid = "";
    string name = "";
    string time = "";

    /// <summary>
    /// čita podatke iz sessiona
    /// </summary>
    /// <returns></returns>
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            getLng = await _localStorageService.GetItemAsync<string>("lng");
            getLng = getLng ?? "hr";

            //CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(getLng);
            string culture = getLng;

            CultureInfo.CurrentUICulture = new CultureInfo(getLng);

            guid = await _localStorageService.GetItemAsync<string>("guid");
            name = await _localStorageService.GetItemAsync<string>("name");
            time = await _localStorageService.GetItemAsync<string>("time");
        }
        catch (Exception ex)
        {
            var a = ex.Message;
            getLng = getLng ?? "hr";
        }

        try
        {
            name = AesOperation.DecryptString(bl.B2B.zaggy.shared.key, name);

            if (string.IsNullOrWhiteSpace(name))
            {
                name = "_x";
            }
        }
        catch (Exception ex)
        {
            var a = ex.Message;
            name = "_x";
        }

        try
        {
            time = AesOperation.DecryptString(bl.B2B.zaggy.shared.key, time);
            if (System.DateTime.Now > DateTime.Parse(time).AddMinutes(bl.B2B.zaggy.shared.timer))
            {
                // ako je proslo x min - > logout
                name = "_x";
            }
        }

        catch (Exception)
        {
            //name = "_x";
        }

        ClaimsIdentity identity;

        var username = bl.B2B.zaggy.resellers_workers.worker_username(guid);

        if (username == name)
        {
            identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.NameIdentifier, guid),
            }, AuthenticationType);

        }
        else
        {
            //logout
            identity = new ClaimsIdentity();
        }

        var claimsPrincipal = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(claimsPrincipal));

    }

    /// <summary>
    /// Login User
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    public void MarkUserAsAuthenticated(string name, string guid)
    {
        var identity = new ClaimsIdentity(new[]
                                    {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.NameIdentifier, guid),
            }, AuthenticationType);

        var claimsPrincipal = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }

    /// <summary>
    /// Logout user
    /// </summary>
    /// <param name="name"></param>
    /// <param name="email"></param>
    public void MarkUserAsLogout()
    {
        _localStorageService.RemoveItemAsync("guid");
        _localStorageService.RemoveItemAsync("name");
        _localStorageService.RemoveItemAsync("time");
        _localStorageService.RemoveItemAsync("reseller_worker_id");
        _localStorageService.RemoveItemAsync("reseller_worker_perm");
        _localStorageService.RemoveItemAsync("reseller_worker_guid");
        //_sessionStorageService.RemoveItemAsync("email");
        //logout
        var identity = new ClaimsIdentity();
        var claimsPrincipal = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}

public static class AesOperation
{

    public static string EncryptString(string key, string plainText)
    {
        byte[] iv = new byte[16];
        byte[] array;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                    {
                        streamWriter.Write(plainText);
                    }

                    array = memoryStream.ToArray();
                }
            }
        }
        return Convert.ToBase64String(array);

        //var hash = bl.sys.security.hash_ids.crypt(plainText);
        //return hash;
    }

    public static string DecryptString(string key, string cipherText)
    {

        if (string.IsNullOrWhiteSpace(cipherText) == true) return null;
        if (cipherText == "null") return null;

        byte[] iv = new byte[16];
        byte[] buffer = Convert.FromBase64String(cipherText);

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream(buffer))
            {
                using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
        //var hash = bl.sys.security.hash_ids.de_crypt(cipherText);
        //return hash;
    }

}
