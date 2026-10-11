using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace Shoppping.web.Security;

public class RequestHandler : DelegatingHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);
        
        return base.SendAsync(request, cancellationToken);
    }
}