using Blazored.SessionStorage;

public class Session
{
    private ISessionStorageService _sessionStorageService;

    public async Task<string> gGuid()
    {
        //await _sessionStorageService.SetItemAsync("name", "John Smith");
        string guid = await _sessionStorageService.GetItemAsync<string>("guid");
        return guid;
    }



}
