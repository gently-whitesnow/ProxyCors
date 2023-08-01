using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace ProxyCors;

public class ProxyManager
{
    private readonly ProxyAdapter _proxyAdapter;

    public ProxyManager(ProxyAdapter proxyAdapter)
    {
        _proxyAdapter = proxyAdapter;
    }

    public async Task<IActionResult> GetProxyResponseAsync(HttpRequest request)
    {
        var requestString = request.Query["url"].ToString();

        if (string.IsNullOrEmpty(requestString))
            return new JsonResult("URL parameter is missing.")
            {
                StatusCode = 400
            };
        
        var response = await _proxyAdapter.GetProxyResponseAsync(requestString);
        if (response.StatusCode == HttpStatusCode.InternalServerError)
            return new JsonResult("Internal server error")
            {
                StatusCode = 500
            };
        
        return new FileStreamResult(await response.Content.ReadAsStreamAsync(), "application/json");
    }
}