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
        var queryString = request.QueryString.ToString();
        var unescapedRequest = Uri.UnescapeDataString(queryString.Substring(1, queryString.Length - 1));
        
        var response = await _proxyAdapter.GetProxyResponseAsync(unescapedRequest);
        
        if (response.StatusCode == HttpStatusCode.InternalServerError)
            return new JsonResult("Internal server error")
            {
                StatusCode = 500
            };
        
        return new FileStreamResult(await response.Content.ReadAsStreamAsync(), "application/json");
    }
}