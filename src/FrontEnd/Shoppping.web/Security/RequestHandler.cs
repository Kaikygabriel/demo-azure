using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Shoppping.web.Services;

namespace Shoppping.web.Security;

public class RequestHandler : DelegatingHandler
{
    private readonly LocalStorage _localStorage;

    public RequestHandler(LocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authModel = _localStorage.Get();
        if (authModel is not null)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authModel.Token);
        
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        
        return base.SendAsync(request, cancellationToken);
    }
}