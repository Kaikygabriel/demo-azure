using Microsoft.JSInterop;
using Shopping.Application.UsesCases.Users.Command.Response;

namespace Shoppping.web.Services;

public class LocalStorage
{
    private readonly ILocalStorageService _localStorageService;
    
    private readonly string _token = "token";
    private readonly string _refreshToken = "refreshToken";
    private readonly string _expiredToken = "expiredToken";
    private readonly string _expiredRefreshToken = "expiredRefreshToken";
    private readonly string _id = "id";
    
    public LocalStorage(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public void Insert(AuthUserResponse request)
    {
        Console.WriteLine("Inserindo");
        _localStorageService.SetItem(_token,request.Token);
        _localStorageService.SetItem(_refreshToken,request.RefreshToken);
        _localStorageService.SetItem(_expiredToken,request.ExpiredToken);
        _localStorageService.SetItem(_expiredRefreshToken,request.ExpiredRefreshToken);
        _localStorageService.SetItem(_id,request.Id);
    }

    public AuthUserResponse? Get()
    {
        var token = _localStorageService.GetItem<string>(_token);
        var refreshToken = _localStorageService.GetItem<string>(_refreshToken);
        var expiredToken = _localStorageService.GetItem<DateTime?>(_expiredToken);
        var expiredRefreshToken = _localStorageService.GetItem<DateTime?>(_expiredRefreshToken);
        var id = _localStorageService.GetItem<Guid?>(_id);

        if (token is null || refreshToken is null || expiredRefreshToken is null || expiredToken is null || id is null)
            return null;

        return new AuthUserResponse((Guid)id, token, refreshToken, (DateTime)expiredToken,(DateTime) expiredRefreshToken);
    }
}